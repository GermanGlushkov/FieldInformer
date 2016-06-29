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
		private StringCollection _sendingRequests=new StringCollection();

		// singleton pattern
		private DistributionManager()
		{
		}
		public static readonly DistributionManager Instance=new DistributionManager();
		// singleton pattern


		public void OnSystemRestart()
		{			
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


		public void SendReport(Report report , Contact[] contacts , Report.ExportFormat Format, DateTime getCachedFrom, out bool isFromCache)
		{			
			isFromCache=false;

			if(report.IsProxy)
				throw new Exception("Report cannot be Proxy");

			if(contacts.Length==0)
				return;
								
			string fileNamePattern=report.GetType().Name + "_" +  report.ID.ToString() + "_";			
			string fileName=fileNamePattern + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + Format.ToString();
			string cacheLookupFileName=fileNamePattern + getCachedFrom.ToString("yyyyMMddHHmmss") + "." + Format.ToString();						
			string filePath=null;
			string reportString=null;

			// lookup cached report
			string[] lookupPaths=Directory.GetFiles(FI.Common.AppConfig.TempDir, fileNamePattern + "*." + Format.ToString());
			if(lookupPaths!=null)
			{
				foreach(string path in lookupPaths)
				{
					string file=Path.GetFileName(path);
					if(file.Length==cacheLookupFileName.Length && file.CompareTo(cacheLookupFileName)>0)
					{
						filePath=FI.Common.AppConfig.TempDir+ @"\" + file;		
						isFromCache=true;
						break;
					}
				}
			}
			if(filePath==null)
			{
				filePath=FI.Common.AppConfig.TempDir+ @"\" + fileName;
				report.Export(filePath, Format);
			}			


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
					OpenSmtp.Mail.MailMessage msg=new OpenSmtp.Mail.MailMessage();
					msg.From=new OpenSmtp.Mail.EmailAddress(FI.Common.AppConfig.SmtpSender);
					msg.To.Add(new OpenSmtp.Mail.EmailAddress(cnt.EMail));
					msg.Subject=report.Name + " (" + report.Description + ")";
					
					// attachment if ordered or report is not html
					if(cnt.DistributionFormat==Contact.DistributionFormatEnum.Attachment || 
						cnt.DistributionFormat==Contact.DistributionFormatEnum.Body_And_Attachment || 
						Format!=Report.ExportFormat.HTML)
					{
						OpenSmtp.Mail.Attachment att=new OpenSmtp.Mail.Attachment(filePath);
						//att.Encoding=System.Web.Mail.MailEncoding.UUEncode;
						msg.Attachments.Add(att);
					}

					// message body (if retport is html) 
					if(Format==Report.ExportFormat.HTML && 
						(cnt.DistributionFormat==Contact.DistributionFormatEnum.MessageBody || 
						cnt.DistributionFormat==Contact.DistributionFormatEnum.Body_And_Attachment))
					{
						msg.HtmlBody=reportString;	
					}

//					msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "0"); //This is crucial. put 0 there
//					msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout", 90); 

					OpenSmtp.Mail.SmtpConfig.LogToText=false;

					OpenSmtp.Mail.Smtp smtp=new OpenSmtp.Mail.Smtp();
					smtp.SendTimeout=600;
					smtp.Host=FI.Common.AppConfig.SmtpServer;
					if(FI.Common.AppConfig.SmtpUserName!=null && FI.Common.AppConfig.SmtpUserName!="")
					{						
						smtp.Username=FI.Common.AppConfig.SmtpUserName;
						smtp.Password=FI.Common.AppConfig.SmtpPassword;
					}
					smtp.SendMail(msg);
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
			DateTime timestamp=DateTime.MinValue;
			string status=null;
			try
			{
				dacObj.GetQueueItemInfo(queueItemId, out distributionId, out status, out timestamp);			
				if(distributionId<=0)
					throw new  Exception("Invalid DistributionId");
				if(timestamp==DateTime.MinValue)
					timestamp=DateTime.MaxValue; // means no cache will be taken

				Guid olapTaskGuid=Guid.NewGuid();
				dacObj.WriteDistributionQueueExecuting(queueItemId , olapTaskGuid);								
				SendDistribution(distributionId, timestamp, olapTaskGuid, out isFromCache);				
			}
			catch(Exception exc)
			{
				// check if not already canceled (cause it's being cnaceled forcibly, which might cause this exceptino (connection closed)
				dacObj.GetQueueItemInfo(queueItemId, out distributionId, out status, out timestamp);
				if(status!="Canceled")
				{
					Common.LogWriter.Instance.WriteEventLogEntry(exc);
					dacObj.WriteDistributionQueueError(queueItemId , exc.Message);

//					// todo: check last 3 statuses, if not failed, requeue
//					bool requeue=!dacObj.HaveAllDistributionAttemptsFailed(distributionId, 3);
//					if(requeue)
//						dacObj.EnqueueDistribution(distributionId, "Requeue after error");
				}

				throw exc;
			}

			dacObj.WriteDistributionQueueOk(queueItemId , isFromCache);
		}


		public void SendDistribution(decimal distributionId, DateTime getCacheFrom, Guid olapTaskGuid, out bool isFromCache)
		{
			try
			{
				isFromCache=false;

				FI.Common.DataAccess.IDistributionsDA dacObj=DataAccessFactory.Instance.GetDistributionsDA();
				decimal userId=dacObj.GetDistributionOwnerId(distributionId);
				if(userId<=0)
					return;

				User user=new User(userId, true);
				Distribution distr=user.DistributionSystem.GetDistribution(distributionId, true);
				Report report=distr.Report;

				if(report.IsProxy)
					report.Open();

				if(report.State==Report.StateEnum.Open)
				{
					OlapReport olapRpt=report as OlapReport;
					if(olapRpt!=null)
						olapRpt.Execute(olapTaskGuid);
					else
						report.Execute();
				}

				Contact contact=distr.Contact;
				if(contact.IsProxy)
					contact.Fetch();


				SendReport(report , new Contact[] {contact} , distr.Format, getCacheFrom, out isFromCache);
			}
			catch(Exception exc)
			{
				Common.LogWriter.Instance.WriteEventLogEntry(exc);				
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
		
		public void AsyncSendAllQueuedDistributions()
		{
			DataTable table=DataAccessFactory.Instance.GetUsersDA().ReadCompanies();
			if(table==null || table.Rows.Count==0)
				return;

			foreach(System.Data.DataRow row in table.Rows)
			{
				string comName=row["ShortName"].ToString();
				this.AsyncSendQueuedDistributions(comName);
			}
		}

		public void SendAllQueuedDistributions()
		{
			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			DataTable table=dacObj.ReadCompanies();
			if(table==null || table.Rows.Count==0)
				return;

			foreach(System.Data.DataRow row in table.Rows)
			{
				string comName=row["ShortName"].ToString();
				this.SendQueuedDistributions(comName);
			}
		}

		public void AsyncSendQueuedDistributions(string CompanyNameShort)
		{
			System.Threading.ThreadPool.QueueUserWorkItem (new System.Threading.WaitCallback(AsyncSendQueuedDistributionsHelper), CompanyNameShort);			
		}

		private void AsyncSendQueuedDistributionsHelper(object CompanyNameShort)
		{
			try
			{
				SendQueuedDistributions((string)CompanyNameShort);
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
			
			lock(this)
			{
				// check if already being handled
				if(_sendingRequests.Contains(CompanyNameShort))
					return;
				_sendingRequests.Add(CompanyNameShort);
			}

			Exception lastExc=null;
			decimal lastExcQueueItemId=-1;
			try
			{
				FI.Common.DataAccess.IDistributionsDA distrDao=DataAccessFactory.Instance.GetDistributionsDA();
				FI.Common.DataAccess.IUsersDA userDao=DataAccessFactory.Instance.GetUsersDA();			

				decimal companyId=userDao.GetCompanyIdByShortName(CompanyNameShort);
				if(companyId>0)
				{
					while(true)
					{
						decimal queueItemId=distrDao.ReadNextQueuedDistribution(companyId);
						if(queueItemId<=0)
							break;

						try
						{
							SendQueuedDistribution(queueItemId);
						}
						catch(Exception exc)
						{
							// throw if exception occured twice for same queue item
							if(lastExcQueueItemId==queueItemId)
								throw exc;		

							lastExcQueueItemId=queueItemId;
						}
					}
				}
			}
			finally
			{
				_sendingRequests.Remove(CompanyNameShort);
			}

			if(lastExc!=null)
				throw lastExc;
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
					User user=new User((decimal)row["Id"] , true);
					user.DistributionSystem.EnqueueDistributions(Date);					
				}
			}
		}
	}
}
