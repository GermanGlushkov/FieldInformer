using System;

namespace FI.UI.ConsoleClient
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static bool _toLog=false;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
//			Class1.SendMail("test", "mytest", "german.glushkov@fieldforce.com");

//			SendTodaysDistributions(new string[] { "/NSALESPP_MAS"});

			if(args==null || args.Length==0)
			{
				HelpCommand();
				return;
			}

			// check if log
			foreach(string command in args)
				if(command.ToUpper()=="Log".ToUpper())
				{
					_toLog=true;
					break;
				}

			// exec commands
			foreach(string command in args)
			{
				try
				{
					if(command.ToUpper()=="SendTodaysDistributions".ToUpper())
						SendTodaysDistributions(args);
					else if(command.ToUpper()=="SendQueuedDistributions".ToUpper())
						SendQueuedDistributions(args);
					else if(command.ToUpper()=="ArchiveOlapDb".ToUpper())
						ArchiveOlapDb(args);
					else if(command.ToUpper()=="RestoreOlapDb".ToUpper())
						RestoreOlapDb(args);
					else if(command.ToUpper()=="Ping".ToUpper())
						Ping();
					else if(command.ToUpper()=="PingProxy".ToUpper())
						PingProxy();
					else if(command=="?" || command.ToUpper()=="HELP")
						HelpCommand();	
					else
						continue;
				}
				catch(Exception exc)
				{
					OnException(command, exc);
					throw exc;
				}

				Log(command);
			}
		}

		private static void HelpCommand()
		{
			System.Console.WriteLine("Commands:");
			System.Console.WriteLine("Log");
			System.Console.WriteLine("SendQueuedDistributions /NShortCompanyName");
			System.Console.WriteLine("SendTodaysDistributions /NShortCompanyName");
			System.Console.WriteLine("Ping");
			System.Console.WriteLine("PingProxy");
			System.Console.WriteLine("ArchiveOlapDb [/N:OlapDbName] [/P:ArchivePath]");
			System.Console.WriteLine("RestoreOlapDb [/P:ArchivePath]");
		}

		private static void ArchiveOlapDb(string[] args)
		{
			// get dbName and archivePath
			string dbName=null;
			string archivePath=null;
			foreach(string s in args)
			{
				if(s.ToUpper().StartsWith("/N:"))
					dbName=s.Substring(3);
				else if(s.ToUpper().StartsWith("/P:"))
					archivePath=s.Substring(3);
			}

			// do stuff
			MsmdarchWrapper.ArchiveOlapDb(dbName, archivePath);
		}

		private static void RestoreOlapDb(string[] args)
		{
			// get archivePath
			string archivePath=null;
			foreach(string s in args)
			{
				if(s.ToUpper().StartsWith("/P:"))
				{
					archivePath=s.Substring(3);
					break;
				}
			}

			// do stuff
			MsmdarchWrapper.RestoreOlapDb(archivePath);
		}


		private static void SendQueuedDistributions(string[] args)
		{
			// get company name
			string companyNameShort=null;
			foreach(string s in args)
				if(s.ToUpper().StartsWith("/N"))
				{
					companyNameShort=s.Substring(2);
					break;
				}

			// do stuff
			DistributionService.DistributionService distributionService=new DistributionService.DistributionService();
			distributionService.Url=FI.Common.AppConfig.ReadSetting("DistributionServiceURL" , "");
			distributionService.Timeout=int.Parse(FI.Common.AppConfig.ReadSetting("DistributionServiceTimeout" , "600000"));

			string user=FI.Common.AppConfig.ReadSetting("DistributionServiceUser" , "");
			string password=FI.Common.AppConfig.ReadSetting("DistributionServicePassword" , "");

			if(user!="")
				distributionService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				distributionService.Credentials=System.Net.CredentialCache.DefaultCredentials;

			distributionService.AsyncSendQueuedDistributions(companyNameShort);
		}

		private static void SendTodaysDistributions(string[] args)
		{
			// get company name
			string companyNameShort=null;
			foreach(string s in args)
				if(s.ToUpper().StartsWith("/N"))
				{
					companyNameShort=s.Substring(2);
					break;
				}

			// do stuff
			DistributionService.DistributionService distributionService=new DistributionService.DistributionService();
			distributionService.Url=FI.Common.AppConfig.ReadSetting("DistributionServiceURL" , "");
			distributionService.Timeout=int.Parse(FI.Common.AppConfig.ReadSetting("DistributionServiceTimeout" , "600000"));

			string user=FI.Common.AppConfig.ReadSetting("DistributionServiceUser" , "");
			string password=FI.Common.AppConfig.ReadSetting("DistributionServicePassword" , "");

			if(user!="")
				distributionService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				distributionService.Credentials=System.Net.CredentialCache.DefaultCredentials;

			distributionService.EnqueueScheduledDistributions(companyNameShort);
			distributionService.AsyncSendQueuedDistributions(companyNameShort);
		}

		private static void Ping()
		{
			PingService.PingService pingService=new PingService.PingService();
			pingService.Url=FI.Common.AppConfig.ReadSetting("PingServiceURL" , "");
			pingService.Timeout=int.Parse(FI.Common.AppConfig.ReadSetting("PingServiceTimeout" , "300000"));

			string user=FI.Common.AppConfig.ReadSetting("PingServiceUser" , "");
			string password=FI.Common.AppConfig.ReadSetting("PingServicePassword" , "");
			string pingMdx=FI.Common.AppConfig.ReadSetting("PingMdx" , "");
			string pingMailTo=FI.Common.AppConfig.ReadSetting("PingMailTo" , "");

			if(user!="")						
				pingService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				pingService.Credentials=System.Net.CredentialCache.DefaultCredentials;

			pingService.PingOlapSystem(pingMdx , pingMailTo);
		}

		private static void PingProxy()
		{
			PingService.PingService pingService=new PingService.PingService();
			pingService.Url=FI.Common.AppConfig.ReadSetting("PingServiceURL" , "");
			pingService.Timeout=int.Parse(FI.Common.AppConfig.ReadSetting("PingServiceTimeout" , "600000"));

			string user=FI.Common.AppConfig.ReadSetting("PingServiceUser" , "");
			string password=FI.Common.AppConfig.ReadSetting("PingServicePassword" , "");
			string pingProxy=FI.Common.AppConfig.ReadSetting("PingServiceProxy" , "");
			string pingMdx=FI.Common.AppConfig.ReadSetting("PingMdx" , "");
			string pingMailTo=FI.Common.AppConfig.ReadSetting("PingMailTo" , "");

			if(pingProxy!="")
			{
				pingService.Proxy=new System.Net.WebProxy(pingProxy);
			}

			if(user!="")
				pingService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				pingService.Credentials=System.Net.CredentialCache.DefaultCredentials;

			pingService.PingOlapSystem(pingMdx , pingMailTo);
		}

		private static void Log(string Command)
		{
			if(!_toLog)
				return;

			string logPath=System.Reflection.Assembly.GetExecutingAssembly().Location.ToString() + "log.txt";
			string message="Command Executed - " + Command ;
			FI.Common.LogWriter.Instance.WriteLogEntry(message , logPath);
		}

		private static void OnException(string Command, Exception exc)
		{
			Common.LogWriter.Instance.WriteEventLogEntry(exc);

			string mailTo=FI.Common.AppConfig.ReadSetting("OnErrorMailTo" , "");
			if (mailTo!="")
				SendMail(Command + " Execution Failed", exc.Message , mailTo);
		}


		private static void SendMail(string Subject , string Body , string MailTo)
		{
			
			OpenSmtp.Mail.MailMessage msg=new OpenSmtp.Mail.MailMessage();
			msg.From=new OpenSmtp.Mail.EmailAddress(FI.Common.AppConfig.SmtpSender);
			msg.To.Add(new OpenSmtp.Mail.EmailAddress(MailTo));
			msg.Subject=Subject;					
			msg.HtmlBody=Body;				

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
		}

	}
}
