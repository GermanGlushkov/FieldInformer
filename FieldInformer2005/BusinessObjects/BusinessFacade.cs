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
