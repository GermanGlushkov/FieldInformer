using System;
using OlapSystem.Management;

namespace OlapSystem.Console
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Program
	{
		static bool _toLog=false;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            //SendTodaysDistributions(new string[] { "N:SALESPP_MAS" });

            //string s1 = DynamicDimensionManager.BuildCaptionFromColumnName("GRP@#@ test~¨  ");
            //string s2 = DynamicDimensionManager.BuildCaptionFromColumnName("GRP@#@ te  st~¨  ");
            //string s3 = DynamicDimensionManager.BuildCaptionFromColumnName(" GRP@#@  t   e  st~¨  ");
            //string s4 = null;

            //Email.SendMail("test", "mytest", "german.glushkov@fieldforce.com");
            //SendTodaysDistributions(new string[] { "/NSALESPP_MAS"});

            //DatabaseManager dm = new DatabaseManager("10.2.0.1", "DBSALESPP_MAS_SFT",
            //    "10.2.0.1", "DBSALESPP_MAS_SFT", "spp", "spp", @"C:\Documents and Settings\German.Glushkov\My Documents\FF Customers\Masterfoods\salespp.ini");
            //dm.PreProcess();
            //dm.ProcessDatabase();






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
            System.Console.WriteLine("SendQueuedDistributions N:ShortCompanyName");
            System.Console.WriteLine("SendTodaysDistributions N:ShortCompanyName");
            System.Console.WriteLine("Ping");
            System.Console.WriteLine("PingProxy");
        }

        private static string GetArgumentByTag(string tag, string[] args)
        {
            if (args == null || tag == null || tag.Length == 0)
                return null;

            tag = (tag.EndsWith(":") ? tag : tag + ":").ToUpper();
            foreach (string s in args)
                if (s!=null && s.ToUpper().StartsWith(tag))
                    return s.Substring(tag.Length);

            return null;
        }

		private static void SendQueuedDistributions(string[] args)
		{
			// get company name
            string companyNameShort = GetArgumentByTag("N:", args);
            if (companyNameShort == null)
                return;

			// do stuff
			DistributionService.DistributionService distributionService=new DistributionService.DistributionService();
			distributionService.Url=AppConfig.ReadSetting("DistributionServiceURL" , "");
			distributionService.Timeout=int.Parse(AppConfig.ReadSetting("DistributionServiceTimeout" , "600000"));

			string user=AppConfig.ReadSetting("DistributionServiceUser" , "");
			string password=AppConfig.ReadSetting("DistributionServicePassword" , "");

			if(user!="")
				distributionService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				distributionService.Credentials=System.Net.CredentialCache.DefaultCredentials;

			distributionService.AsyncSendQueuedDistributions(companyNameShort);
		}

		private static void SendTodaysDistributions(string[] args)
		{
			// get company name
            string companyNameShort = GetArgumentByTag("N:", args);
            if (companyNameShort == null)
                return;

			// do stuff
			DistributionService.DistributionService distributionService=new DistributionService.DistributionService();
			distributionService.Url=AppConfig.ReadSetting("DistributionServiceURL" , "");
			distributionService.Timeout=int.Parse(AppConfig.ReadSetting("DistributionServiceTimeout" , "600000"));

			string user=AppConfig.ReadSetting("DistributionServiceUser" , "");
			string password=AppConfig.ReadSetting("DistributionServicePassword" , "");

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
			pingService.Url=AppConfig.ReadSetting("PingServiceURL" , "");
			pingService.Timeout=int.Parse(AppConfig.ReadSetting("PingServiceTimeout" , "300000"));

			string user=AppConfig.ReadSetting("PingServiceUser" , "");
			string password=AppConfig.ReadSetting("PingServicePassword" , "");
			string pingMdx=AppConfig.ReadSetting("PingMdx" , "");
			string pingMailTo=AppConfig.ReadSetting("PingMailTo" , "");

			if(user!="")						
				pingService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				pingService.Credentials=System.Net.CredentialCache.DefaultCredentials;

			pingService.PingOlapSystem(pingMdx , pingMailTo);
		}

		private static void PingProxy()
		{
			PingService.PingService pingService=new PingService.PingService();
			pingService.Url=AppConfig.ReadSetting("PingServiceURL" , "");
			pingService.Timeout=int.Parse(AppConfig.ReadSetting("PingServiceTimeout" , "600000"));

			string user=AppConfig.ReadSetting("PingServiceUser" , "");
			string password=AppConfig.ReadSetting("PingServicePassword" , "");
			string pingProxy=AppConfig.ReadSetting("PingServiceProxy" , "");
			string pingMdx=AppConfig.ReadSetting("PingMdx" , "");
			string pingMailTo=AppConfig.ReadSetting("PingMailTo" , "");

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

			string message="Command Executed - " + Command ;
            LogWriter.Instance.WriteEventLogEntry(message, System.Diagnostics.EventLogEntryType.Information);
		}

		private static void OnException(string Command, Exception exc)
		{
            Exception excToShow=(exc.InnerException==null ? exc : exc.InnerException);
            System.Console.WriteLine("Exception: " + excToShow.Message + "\r\n" + excToShow.StackTrace);

			LogWriter.Instance.WriteEventLogEntry(excToShow);

			string mailTo=AppConfig.ReadSetting("OnErrorMailTo" , "");
			if (mailTo!="")
				Email.SendMail(Command + " Execution Failed", excToShow.Message + "\r\n" + excToShow.StackTrace, mailTo);
		}



	}
}
