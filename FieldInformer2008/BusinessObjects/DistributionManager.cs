using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;


namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for DistributionManager.
	/// </summary>
	public class DistributionManager
	{
        private StringCollection _sendingRequests = new StringCollection();
        private StringCollection _refreshCachedRequests = new StringCollection();

		// singleton pattern
		private DistributionManager()
		{
		}
		public static readonly DistributionManager Instance=new DistributionManager();
		// singleton pattern


		public void OnSystemRestart()
        {
            System.Diagnostics.StackTrace stack = new System.Diagnostics.StackTrace();
            Common.LogWriter.Instance.WriteEventLogEntry("DistributionManager OnSystemRestart\r\n" + stack.ToString(), 
                System.Diagnostics.EventLogEntryType.Warning);

			CleanupTempDir();
			
			FI.Common.DataAccess.IOlapSystemDA dac=DataAccessFactory.Instance.GetOlapSystemDA();
			dac.ResetOlapSystem();
		}

		private void CleanupTempDir()
		{
			if(!Directory.Exists(FI.Common.AppConfig.TempDir))
				return;

			string[] files=Directory.GetFiles(FI.Common.AppConfig.TempDir);
			if(files!=null)
			{
				foreach(string file in files)
					try
					{
						File.Delete(file);
					}
					catch
					{
						// ignore
					}
			}
		}

		/*
		public FI.Common.Data.FIDataTable GetMasterDistributionQueuePage(int CurrentPage , int RowCount , string FilterExpression , string SortExpression)
		{
			int StartIndex=(CurrentPage-1)*RowCount;

			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable table=null;

			table=dacObj.ReadMasterDistributionQueue(StartIndex , RowCount , FilterExpression , SortExpression);

			return table;
		}
		*/

		public FI.Common.Data.FIDataTable GetDistributionInfo(bool checkOnly)
		{
			// get from DA
			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			FI.Common.Data.FIDataTable ret=dacObj.GetDistributionInfo(false);

			// resolve distributions not sent according to schedule
			DataColumn checkStatusCol=ret.Columns["CheckStatus"];
			DataColumn timeCol=ret.Columns["LastTimestamp"];
			DataColumn statusCol=ret.Columns["LastStatus"];
			DataColumn freqTypeCol=ret.Columns["ScheduleType"];
			DataColumn freqValCol=ret.Columns["ScheduleValue"];
			foreach(DataRow r in ret.Rows)
			{
				if((string)r[checkStatusCol]!=string.Empty)
					continue; // already has check status
				if((string)r[timeCol]==string.Empty)
					continue; // no time to iterate from
				if((string)r[statusCol]!="Ok")
					continue; // last status is not ok

				DateTime date=DateTime.Parse(r[timeCol].ToString());
				date=date.AddDays(1);
				DateTime now=DateTime.Now;
				bool ok=true;
				while(date<=now)
				{
					string reqType=r[freqTypeCol].ToString();
					string reqVal=r[freqValCol].ToString();
					if(Distribution.IsScheduledForDate(reqType, reqVal, date))
					{
						ok=false;
						break;
					}
					date=date.AddDays(1);
				}

				if(!ok)
					r[checkStatusCol]="Schedule Failed";
			}
			return ret;
		}


		public void SendReport(Report report , Guid olapTaskGuid, Contact[] contacts , Report.ExportFormat Format, DateTime minCacheTimestamp, DateTime currentTimestamp, out bool isFromCache)
		{			
			isFromCache=false;

			if(contacts.Length==0)
				return;

            if (report.IsProxy)
                report.Open();

			string fileNamePattern=report.GetType().Name + "_" +  report.ID.ToString() + "_";
            string newFileName = fileNamePattern + currentTimestamp.ToString("yyyyMMddHHmmssfff") + "." + Format.ToString();
			string cacheLookupFileName=fileNamePattern + minCacheTimestamp.ToString("yyyyMMddHHmmssfff") + "." + Format.ToString();						
			string filePath=null;
			string reportString=null;

			// lookup cached report
            if (minCacheTimestamp != DateTime.MinValue && minCacheTimestamp != DateTime.MaxValue)
            {
                string[] lookupPaths = Directory.GetFiles(FI.Common.AppConfig.TempDir, fileNamePattern + "*." + Format.ToString());
                if (lookupPaths != null)
                {
                    foreach (string path in lookupPaths)
                    {
                        string file = Path.GetFileName(path);
                        if (file.Length == cacheLookupFileName.Length && file.CompareTo(cacheLookupFileName) > 0)
                        {
                            filePath = FI.Common.AppConfig.TempDir + @"\" + file;
                            isFromCache = true;
                            break;
                        }
                    }
                }
            }

            // no cache, execute
			if(filePath==null)
            {

                if (report.State == Report.StateEnum.Open)
                {
                    OlapReport olapRpt = report as OlapReport;
                    if (olapRpt != null)
                        olapRpt.Execute(olapTaskGuid);
                    else
                        report.Execute();
                }

                filePath = FI.Common.AppConfig.TempDir + @"\" + newFileName;
				report.Export(filePath, Format);
			}			


            // send to contacts
			foreach(Contact cnt in contacts)
			{
				if(Format==Report.ExportFormat.HTML && reportString==null)
				{
					if(cnt.DistributionFormat==Contact.DistributionFormatEnum.MessageBody || cnt.DistributionFormat==Contact.DistributionFormatEnum.Body_And_Attachment)
					{			
						StreamReader sr=new StreamReader(filePath, System.Text.Encoding.Unicode, true);
						if(sr!=null)
						{
							reportString=sr.ReadToEnd();
							sr.Close();
						}
					}
				}

				//send via email
				try
				{
					if(cnt.IsProxy)
						cnt.Fetch();

					// message object
                    System.Net.Mail.MailMessage msg=new System.Net.Mail.MailMessage();
                    msg.From=new System.Net.Mail.MailAddress(FI.Common.AppConfig.SmtpSender);
                    msg.To.Add(cnt.EMail);
                    msg.Subject=report.Name + " (" + report.Description + ")";
                    //OpenSmtp.Mail.MailMessage msg=new OpenSmtp.Mail.MailMessage();
                    //msg.From=new OpenSmtp.Mail.EmailAddress(FI.Common.AppConfig.SmtpSender);
                    //msg.To.Add(new OpenSmtp.Mail.EmailAddress(cnt.EMail));
                    //msg.Subject=report.Name + " (" + report.Description + ")";
					
					// attachment if ordered or report is not html
					if(cnt.DistributionFormat==Contact.DistributionFormatEnum.Attachment || 
						cnt.DistributionFormat==Contact.DistributionFormatEnum.Body_And_Attachment || 
						Format!=Report.ExportFormat.HTML)
					{
                        //OpenSmtp.Mail.Attachment att=new OpenSmtp.Mail.Attachment(filePath);
						//att.Encoding=System.Web.Mail.MailEncoding.UUEncode;
                        System.Net.Mail.Attachment att=new System.Net.Mail.Attachment(filePath);
						msg.Attachments.Add(att);
					}

					// message body (if retport is html) 
					if(Format==Report.ExportFormat.HTML && 
						(cnt.DistributionFormat==Contact.DistributionFormatEnum.MessageBody || 
						cnt.DistributionFormat==Contact.DistributionFormatEnum.Body_And_Attachment))
					{
                        //msg.HtmlBody=reportString;	
                        msg.Body = reportString;
                        msg.IsBodyHtml = true;
					}

//					msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "0"); //This is crucial. put 0 there
//					msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 90); 
                    
                    //OpenSmtp.Mail.SmtpConfig.LogToText=false;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Host = FI.Common.AppConfig.SmtpServer;
                    smtp.Timeout = 600000; // 10 minutes
                    if (FI.Common.AppConfig.SmtpPort > 0)
                        smtp.Port = FI.Common.AppConfig.SmtpPort;

                    //OpenSmtp.Mail.Smtp smtp=new OpenSmtp.Mail.Smtp();
                    //smtp.SendTimeout=600;
                    //smtp.Host=FI.Common.AppConfig.SmtpServer;
					if(FI.Common.AppConfig.SmtpUserName!=null && FI.Common.AppConfig.SmtpUserName!="")
					{
                        smtp.Credentials = new System.Net.NetworkCredential(FI.Common.AppConfig.SmtpUserName, FI.Common.AppConfig.SmtpPassword);
                        //smtp.Username=FI.Common.AppConfig.SmtpUserName;
                        //smtp.Password=FI.Common.AppConfig.SmtpPassword;
					}
                    smtp.Send(msg);
                    //smtp.SendMail(msg);
//					System.Web.Mail.SmtpMail.SmtpServer=FI.Common.AppConfig.SmtpServer;					
//					System.Web.Mail.SmtpMail.Send(msg);					
				}
				catch(Exception exc)
				{
					// because real exception is inside:
					while(exc.InnerException!=null)
					{
						exc=exc.InnerException;
					}

					Common.LogWriter.Instance.WriteEventLogEntry(exc);
					throw exc;
				}

			}

			
						

		}



		public void SendQueuedDistribution(decimal queueItemId)
		{			
			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();

			decimal distributionId=0;
			bool isFromCache=false;
			DateTime queueItemTimestamp=DateTime.MinValue;
			string status=null;
			try
			{
				dacObj.GetQueueItemInfo(queueItemId, out distributionId, out status, out queueItemTimestamp);			
				if(distributionId<=0)
					throw new  Exception("Invalid DistributionId");
				if(queueItemTimestamp==DateTime.MinValue)
					queueItemTimestamp=DateTime.MaxValue; // means no cache will be taken

                // user
                dacObj = DataAccessFactory.Instance.GetDistributionsDA();
                decimal userId = dacObj.GetDistributionOwnerId(distributionId);
                if (userId <= 0)
                    return;

                // status executing
                Guid olapTaskGuid = Guid.NewGuid();
				dacObj.WriteDistributionQueueExecuting(queueItemId , olapTaskGuid);

                // send report
                User user = new User(userId, true);
                Distribution distr = user.DistributionSystem.GetDistribution(distributionId, true);
                Report report = distr.Report;
                DateTime currentTimestamp = dacObj.GetCurrentTimestamp();
                SendReport(report, olapTaskGuid, new Contact[] { distr.Contact } , distr.Format, queueItemTimestamp, currentTimestamp, out isFromCache);

                // status ok
                dacObj.WriteDistributionQueueOk(queueItemId, isFromCache);
			}
			catch(Exception exc)
            {
				// check if not already canceled (cause it's being cnaceled forcibly, which might cause this exceptino (connection closed)
				dacObj.GetQueueItemInfo(queueItemId, out distributionId, out status, out queueItemTimestamp);
				if(status!="Canceled")
				{
					Common.LogWriter.Instance.WriteEventLogEntry(exc);

                    // status error
					dacObj.WriteDistributionQueueError(queueItemId , exc.Message);

//					// todo: check last 3 statuses, if not failed, requeue
//					bool requeue=!dacObj.HaveAllDistributionAttemptsFailed(distributionId, 3);
//					if(requeue)
//						dacObj.EnqueueDistribution(distributionId, "Requeue after error");
				}

                // throw
				throw exc;
			}

		}


		public void EnqueueDistribution(decimal distributionId)
		{
			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.EnqueueDistribution(distributionId, "");
		}

		public void CancelDistributionQueueItems(decimal distributionId)
		{
			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			decimal[] items=dacObj.GetActiveDistributionQueueItems(distributionId);
			if(items!=null)
				foreach(decimal itemId in items)
					CancelQueuedItem(itemId);
		}

		public bool CancelQueuedItem(decimal queueItemId)
		{
			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			return dacObj.CancelQueuedItem(queueItemId, "Canceled manually");
		}
		
		public void AsyncSendAllQueuedDistributions(bool force)
		{
			DataTable table=DataAccessFactory.Instance.GetUsersDA().ReadCompanies();
			if(table==null || table.Rows.Count==0)
				return;

			foreach(System.Data.DataRow row in table.Rows)
			{
				string comName=row["ShortName"].ToString();
                this.AsyncSendQueuedDistributions(comName, force);
			}
		}

		public void SendAllQueuedDistributions(bool force)
		{
			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			DataTable table=dacObj.ReadCompanies();
			if(table==null || table.Rows.Count==0)
				return;

			foreach(System.Data.DataRow row in table.Rows)
			{
				string comName=row["ShortName"].ToString();
                this.SendQueuedDistributions(comName, force);
			}
		}

        
		public void AsyncSendQueuedDistributions(string CompanyNameShort, bool force)
		{
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(AsyncSendQueuedDistributionsHelper), new object[] { CompanyNameShort, force });			
		}

		private void AsyncSendQueuedDistributionsHelper(object state)
		{
			try
			{
                object[] aState = state as object[];
				SendQueuedDistributions((string)aState[0], (bool)aState[1]);
			}
			catch(Exception exc)
			{
				// no exception in async method, just log
				FI.Common.LogWriter.Instance.WriteEventLogEntry(
					"AsyncSendQueuedDistributions exception:\r\n" +  
					exc.Message + "\r\n" + 
					(exc.InnerException==null ? "" : exc.InnerException.Message) + "\r\n" +
					exc.Source,
					System.Diagnostics.EventLogEntryType.Error);
			}
		}

        public void SendQueuedDistributions(string CompanyNameShort)
        {
            SendQueuedDistributions(CompanyNameShort, false);
        }

		public void SendQueuedDistributions(string CompanyNameShort, bool force)
		{
           
            int count = 0;
            try
            {
                Common.LogWriter.Instance.WriteEventLogEntry("Start SendQueuedDistributions. Company=" + CompanyNameShort);

                FI.Common.DataAccess.IDistributionsDA distrDao = DataAccessFactory.Instance.GetDistributionsDA();
                FI.Common.DataAccess.IUsersDA userDao = DataAccessFactory.Instance.GetUsersDA();

                bool alreadyExecuting = _sendingRequests.Contains(CompanyNameShort);
                lock (typeof(DistributionManager))
                {
                    // check if already being handled
                    if (!_sendingRequests.Contains(CompanyNameShort))
                        _sendingRequests.Add(CompanyNameShort);
                    else if(!force)
                    {
                        // check whether executing
                        Common.LogWriter.Instance.WriteEventLogEntry("Already processing SendQueuedDistributions. Company=" + CompanyNameShort,
                            System.Diagnostics.EventLogEntryType.Warning);
                        return;
                    }
                }

                decimal companyId = userDao.GetCompanyIdByShortName(CompanyNameShort);
                if (companyId <= 0)
                    Common.LogWriter.Instance.WriteEventLogEntry(String.Format("Unable to find by short name SendQueuedDistributions. Company={0}", CompanyNameShort),
                        System.Diagnostics.EventLogEntryType.Warning);

                int toSend = distrDao.GetQueuedDistributionsCount(companyId);
                //Common.LogWriter.Instance.WriteEventLogEntry(String.Format("Items to send SendQueuedDistribution. Company={0}, Count={1}", CompanyNameShort, toSend));

                int retryCount = 2;
                int retry = 0;
                if (companyId > 0)
                {
                    while (true)
                    {
                        decimal queueItemId = 0;
                        try
                        {
                            queueItemId = distrDao.ReadNextQueuedDistribution(companyId);
                            if (queueItemId <= 0)
                                break;

                            Common.LogWriter.Instance.WriteEventLogEntry(String.Format("Start send distibution SendQueuedDistributions. Company={0}, Id={1}", CompanyNameShort, queueItemId));
                            SendQueuedDistribution(queueItemId);
                            Common.LogWriter.Instance.WriteEventLogEntry(String.Format("End send distibution SendQueuedDistributions. Company={0}, Id={1}", CompanyNameShort, queueItemId));

                            count++;
                            retry = 0; // reset retry on success
                        }
                        catch (Exception exc)
                        {
                           Common.LogWriter.Instance.WriteEventLogEntry(String.Format("Error SendQueuedDistribution. Company={0}, Id={1}\r\n{2}\r\n{3}", CompanyNameShort, queueItemId, exc.Message, exc.StackTrace),
                                System.Diagnostics.EventLogEntryType.Error);

                            retry++;
                            // throw if retry count exceeded
                            if (retry >= retryCount)
                                throw exc;
                            else
                            {
                                // retry
                                Common.LogWriter.Instance.WriteEventLogWarning("Exception, retry follows\r\n" + exc.Message);
                                System.Threading.Thread.Sleep(15000); // wait 15 seconds 
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                _sendingRequests.Remove(CompanyNameShort);
                Common.LogWriter.Instance.WriteEventLogEntry(String.Format("Error SendQueuedDistributions. Company={0} \r\n{1}\r\n{2}", CompanyNameShort, exc.Message, exc.StackTrace),
                    System.Diagnostics.EventLogEntryType.Error);
            }
			finally
			{
                _sendingRequests.Remove(CompanyNameShort);
                Common.LogWriter.Instance.WriteEventLogEntry(string.Format("End SendQueuedDistributions: Company={0}, Count={1}", CompanyNameShort, count));
			}
		}


        public void RefreshCachedReports(string CompanyNameShort)
        {
            Common.LogWriter.Instance.WriteEventLogEntry("Start RefreshCachedReports: " + CompanyNameShort);

            lock (this)
            {
                // check if already being handled
                if (_refreshCachedRequests.Contains(CompanyNameShort))
                    return;
                _refreshCachedRequests.Add(CompanyNameShort);
            }

            int count = 0;
            try
            {
                FI.Common.DataAccess.IUsersDA userDao = DataAccessFactory.Instance.GetUsersDA();
                FI.Common.DataAccess.IOlapReportsDA rptDao = DataAccessFactory.Instance.GetOlapReportsDA();

                decimal companyId = userDao.GetCompanyIdByShortName(CompanyNameShort);
                if(companyId<=0)
                    return;

                Common.Data.FIDataTable tbl = rptDao.GetCashedReportsToRefresh(companyId);
                if (tbl != null)
                    foreach (DataRow row in tbl.Rows)
                    {
                        User usr=new User((decimal)row["user_id"], false);
                        OlapReport rpt=usr.ReportSystem.GetReport((decimal)row["rpt_id"], typeof(OlapReport), true) as OlapReport;
                        rpt.Execute();
                        count++;
                    }
            }
            catch (Exception exc)
            {
                Common.LogWriter.Instance.WriteEventLogEntry(exc);
                throw exc;
            }
            finally
            {
                _refreshCachedRequests.Remove(CompanyNameShort);
                Common.LogWriter.Instance.WriteEventLogEntry(string.Format("End RefreshCachedReports: Company={0}, Count={1}", CompanyNameShort, count));
            }

        }
		
		public void DeleteDistribution(decimal distributionId)
		{
			// cancel distribution items first
			CancelDistributionQueueItems(distributionId);

			// delete distribution
			FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
			dacObj.DeleteDistribution(distributionId);			
		}

		public void EnqueueScheduledDistributions(System.DateTime Date , string CompanyNameShort)
        {
			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			FI.Common.Data.FIDataTable table=dacObj.ReadUsers();
			if(table.Rows.Count==0)
				return;

			foreach(System.Data.DataRow row in table.Rows)
			{
				if( ((string)row["CompanyNameShort"]).ToUpper()==CompanyNameShort.ToUpper())
				{
					User user=new User((decimal)row["Id"], false);
					user.DistributionSystem.EnqueueDistributions(Date);					
				}
            }
		}
	}
}
