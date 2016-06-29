using System;
using System.Data;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for BusinessFacade.
	/// </summary>
	public class BusinessFacade:MarshalByRefObject
	{
		// singleton pattern
		private BusinessFacade()
		{
//			object o=FI.Common.DataAccess.OlapServices.ProcessorPool.Instance;
//			InitializeRemoting();
		}
		public static readonly BusinessFacade Instance=new BusinessFacade();
		// singleton pattern


		public FI.Common.Data.FIDataTable GetOlapProcessorInfo()
		{
			FI.Common.DataAccess.IOlapSystemDA olapDao=DataAccessFactory.Instance.GetOlapSystemDA();
			return olapDao.GetOlapProcessorInfo();
		}


		public void PingOlapSystem(string Mdx , string MailTo)
		{
			bool failure=false;

			System.IO.StringWriter sw=new System.IO.StringWriter();

			// get all companies
			FI.Common.DataAccess.IUsersDA usersDac=DataAccessFactory.Instance.GetUsersDA();
			System.Data.DataTable table=usersDac.ReadCompanies();
			if(table==null || table.Rows.Count==0)
				return;

			sw.WriteLine("<html>");
			sw.WriteLine("Failed Ping Queries (" + System.DateTime.Today.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() + ")");
			sw.WriteLine("<br><br>");
			sw.WriteLine("<table border=1 width=100%>");

			try
			{
				foreach(System.Data.DataRow row in table.Rows)
				{
					if( (bool)row["Ping"]==true)
					{
						string company=row["ShortName"].ToString();
						string server=row["OlapServer"].ToString();
						string database=row["OlapDatabase"].ToString();

						try
						{							
							string taskGuid=Guid.NewGuid().ToString();
							string taskTag=string.Format("Ping");

							FI.Common.DataAccess.IOlapSystemDA olapDac=DataAccessFactory.Instance.GetOlapSystemDA();
							olapDac.BuildCellset(Mdx , server , database, taskGuid, taskTag);
						}
						catch(Exception exc)
						{
							failure=true;

							sw.Write("<tr>");
							sw.Write("<td>");
							sw.Write(company);
							sw.Write("</td><td>");
							sw.Write(exc.Message);
							sw.Write("</td>");
							sw.WriteLine("</tr>");

							Common.LogWriter.Instance.WriteEventLogEntry(exc);
						}
					}
				}
			}
			catch(Exception exc) //more common exception
			{
				failure=true;

				sw.Write("<tr>");
				sw.Write(exc.Message);
				sw.Write("</td>");
				sw.WriteLine("</tr>");

				Common.LogWriter.Instance.WriteEventLogEntry(exc);
			}

			sw.WriteLine("</table></html>");


			//send via email
			if(failure)
			{
				try
				{
					// message
					OpenSmtp.Mail.MailMessage msg=new OpenSmtp.Mail.MailMessage();
					msg.From=new OpenSmtp.Mail.EmailAddress(FI.Common.AppConfig.SmtpSender);
					msg.To.Add(new OpenSmtp.Mail.EmailAddress(MailTo));
					msg.Subject="Failed Ping Queries";
					msg.Priority="High";
					msg.HtmlBody=sw.ToString();

					// smtp host
					OpenSmtp.Mail.Smtp smtp=new OpenSmtp.Mail.Smtp();
					smtp.Host=FI.Common.AppConfig.SmtpServer;
					if(FI.Common.AppConfig.SmtpUserName!=null && FI.Common.AppConfig.SmtpUserName!="")
					{						
						smtp.Username=FI.Common.AppConfig.SmtpUserName;
						smtp.Password=FI.Common.AppConfig.SmtpPassword;
					}
					smtp.SendMail(msg);
				}
				catch(Exception exc)
				{
					// because real exception is inside:
					while(exc.InnerException!=null)
					{
						exc=exc.InnerException;
					}
										
					throw exc;
				}
			}
		}



		public void ExecuteAllOlapReports(string CompanyNameShort, int millisecondsTimeout, string logPath)
		{			
			if(!System.IO.Path.IsPathRooted(logPath))
				logPath=FI.Common.AppConfig.TempDir + "\\" + logPath;

			FI.Common.DataAccess.IUsersDA dacObj=DataAccessFactory.Instance.GetUsersDA();
			FI.Common.Data.FIDataTable table=dacObj.ReadUsers();
			if(table.Rows.Count==0)
				return;
			table.DefaultView.Sort="Id asc";

			int errorCount=0;
			int rptCount=1;
			foreach(System.Data.DataRowView userRow in table.DefaultView)
			{
				if( ((string)userRow["CompanyNameShort"]).ToUpper()==CompanyNameShort.ToUpper())
				{
					User user=new User((decimal)userRow["Id"] , false);
					FI.Common.Data.FIDataTable t=user.ReportSystem.GetReportHeaders(typeof(OlapReport));		
					t.DefaultView.Sort="id asc";
					
					string userLog=string.Format("***************** User id={0}, login='{1}', password='{2}'", user.ID, user.Logon, user.Password);
					System.IO.StreamWriter sw=System.IO.File.AppendText(logPath);
					sw.WriteLine(userLog);
					sw.Close();

					foreach(DataRowView rptRow in t.DefaultView)
					{
						if((byte)rptRow["sharing_status"]==(byte)Report.SharingEnum.InheriteSubscriber || 
							(byte)rptRow["sharing_status"]==(byte)Report.SharingEnum.SnapshotSubscriber)
							continue;

						string log=string.Format("{0}\t#{1}\t", DateTime.Now.ToShortTimeString(), rptCount);
						try
						{
							OlapReport rpt=(OlapReport)user.ReportSystem.GetReport((decimal)rptRow["id"], typeof(OlapReport), true);
							log+=string.Format("OlapReport id={0}, name='{1}', description='{2}'", rpt.ID, rpt.Name, rpt.Description);

							rpt.BeginExecute();
							int milisecondCount=0;
							while(milisecondCount<millisecondsTimeout && rpt.State==Report.StateEnum.Executing)
							{
								System.Threading.Thread.Sleep(500);
								milisecondCount+=500;
							}
						
							if(rpt.State==Report.StateEnum.Executing)
							{
								rpt.CancelExecute();
								log+="\r\n\tcanceled on timeout";
							}
							else
							{
								rpt.EndExecute();
								log+="\r\n\tcompleted, cells=" + rpt.Cellset.Axis0PosCount*rpt.Cellset.Axis1PosCount;
							}
						}
						catch(Exception exc)
						{
							log+=string.Format("exception \r\n\t\t{0}", exc.Message);
							errorCount++;
						}
						rptCount++;
						
						sw=System.IO.File.AppendText(logPath);
						sw.WriteLine(log);
						sw.Close();
					}
				}
			}
		}


/*
		public void TestGetSchemaMembers()
		{
			DateTime time1=DateTime.Now;

			// ---------------------------------------------------------------------------
			string[] un=new string[10000];
			string[] n=new string[un.Length];
			Int16[] cc=new Int16[un.Length];
			Int16[] ld=new Int16[un.Length];

			for(int i=0;i<un.Length;i++)
			{
				un[i]="[Measures].[Units Ordered]";
			}

			FI.Common.DataAccess.IOlapSystemDA obj=new FI.Common.DataAccess.IOlapSystemDA();
			//obj.GetSchemaMembers("localhost" , "foodmart 2000" , "Warehouse" , un , out n , out  cc, out ld);
			// ---------------------------------------------------------------------------

			DateTime time2=DateTime.Now;
			string test=time2.Subtract(time1).ToString();

			test="";
		}




		public void TestGetSchemaMembersXML()
		{
			

			// ---------------------------------------------------------------------------
			string[] un=new string[10000];
			string[] n=new string[un.Length];
			Int16[] cc=new Int16[un.Length];
			Int16[] ld=new Int16[un.Length];

			string MemXML="";

			for(int i=0;i<un.Length;i++)
			{
				un[i]="[Measures].[Units Ordered]";
			}

			DateTime time1=DateTime.Now;
			FI.Common.DataAccess.IOlapSystemDA obj=new FI.Common.DataAccess.IOlapSystemDA();
			obj.GetSchemaMembersXML("localhost" , "foodmart 2000" , "Warehouse" , un , ref MemXML);
			// ---------------------------------------------------------------------------

			DateTime time2=DateTime.Now;
			string test=time2.Subtract(time1).ToString();
			int test2=MemXML.Length;
			
			time1=DateTime.Now;
			int j=0;
			FI.Common.Data.Auto.OlapMemSet ds=new FI.Common.Data.Auto.OlapMemSet();
			ds.ReadXml(new System.IO.StringReader(MemXML) , System.Data.XmlReadMode.IgnoreSchema );
			time2=DateTime.Now;
			test=time2.Subtract(time1).ToString();
			test="";
		}
*/





	}
}
