using System;
using OlapSystem.Management;
using OlapSystem.Management.OlapDb;

namespace OlapSystem.Console
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Program
	{
		static bool _toLog=false;
        static DateTime _callStart = DateTime.MinValue;
        static TimeSpan _callStopWait = new TimeSpan(0, 0, 15);
        static string _callCommand = "";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            //args=new string[] { "SendTodaysDistributions",  "N:Abile", "Log" };

            //string s1 = DynamicDimensionManager.BuildCaptionFromColumnName("GRP@#@ test~¨  ");
            //string s2 = DynamicDimensionManager.BuildCaptionFromColumnName("GRP@#@ te  st~¨  ");
            //string s3 = DynamicDimensionManager.BuildCaptionFromColumnName(" GRP@#@  t   e  st~¨  ");
            //string s4 = null;

            //Email.SendMail("test", "mytest", "german.glushkov@fieldforce.com");
            //Email.SendMail("test", "mytest", "german.glushkov@gmail.com");
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
                    if (command.ToUpper() == "ProcessOlapDb".ToUpper())
                        ProcessOlapDb(args);
					else if(command.ToUpper()=="SendTodaysDistributions".ToUpper())
						SendTodaysDistributions(args);
					else if(command.ToUpper()=="SendQueuedDistributions".ToUpper())
                        SendQueuedDistributions(args);
                    else if (command.ToUpper() == "ProcessBackgroundJobs".ToUpper())
                        ProcessBackgroundJobs(args);
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
			}
		}


        private static void HelpCommand()
        {
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("ProcessOlapDb OS:OlapServer ODB:OlapDb DSS:DataSourceServer DSDB:DataSpourceDb DSU:DataSourceUser DSP:DataSourcePassword INIP:SalesppIniPath");
            System.Console.WriteLine("SendQueuedDistributions N:ShortCompanyName");
            System.Console.WriteLine("SendTodaysDistributions N:ShortCompanyName");
            System.Console.WriteLine("ProcessBackgroundJobs N:ShortCompanyName");
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

        private static void ProcessOlapDb(string[] args)
        {
            string olapServer = GetArgumentByTag("OS:", args);
            if (olapServer == null)
                olapServer = "localhost";

            string olapDB = GetArgumentByTag("ODB:", args);
            if (olapDB == null)
                throw new ArgumentException("Agrument missing: OlapDb");

            string dsServer = GetArgumentByTag("DSS:", args);
            if (dsServer == null)
                dsServer = ".";

            string dsDB = GetArgumentByTag("DSDB:", args);
            if (dsDB == null)
                throw new ArgumentException("Agrument missing: DataSourceDb");

            string dsUser = GetArgumentByTag("DSU:", args);
            if (dsUser == null)
                dsUser = "spp";

            string dsPwd = GetArgumentByTag("DSP:", args);
            if (dsPwd == null)
                dsPwd = "spp";

            string sppIniPath = GetArgumentByTag("INIP:", args);
            if (sppIniPath == null)
                sppIniPath = "";


            DatabaseManager dm = new DatabaseManager(olapServer, olapDB, dsServer, dsDB, dsUser, dsPwd, sppIniPath);
            dm.PreProcess();
            dm.ProcessOlapDatabase();
            if (_toLog)
                FI.Common.LogWriter.Instance.WriteEventLogEntry(string.Format("ProcessOlapDb: server '{0}', db '{1}'", olapServer, olapDB));
        }

		private static void SendQueuedDistributions(string[] args)
		{
			// get company name
            string companyNameShort = GetArgumentByTag("N:", args);
            if (companyNameShort == null)
                return;

			// do stuff
			DistributionService.DistributionService distributionService=new DistributionService.DistributionService();
            distributionService.Url = FI.Common.AppConfig.ReadSetting("DistributionServiceURL", "");
			distributionService.Timeout=int.Parse(FI.Common.AppConfig.ReadSetting("DistributionServiceTimeout" , "600000"));

			string user=FI.Common.AppConfig.ReadSetting("DistributionServiceUser" , "");
            string password = FI.Common.AppConfig.ReadSetting("DistributionServicePassword", "");

			if(user!="")
				distributionService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				distributionService.Credentials=System.Net.CredentialCache.DefaultCredentials;

            _callStart = DateTime.Now;
            _callCommand = "SendQueuedDistributionsAsync";
            distributionService.SendQueuedDistributionsAsync(companyNameShort);
            distributionService.SendQueuedDistributionsCompleted += new DistributionService.SendQueuedDistributionsCompletedEventHandler(distributionService_AsyncCallCompleted);
            if (_toLog)
                FI.Common.LogWriter.Instance.WriteEventLogEntry(_callCommand + " start. Company=" + companyNameShort);
            while ((DateTime.Now - _callStart)<_callStopWait)
                System.Threading.Thread.Sleep(1000);
            distributionService.Dispose();
		}

		private static void SendTodaysDistributions(string[] args)
		{
			// get company name
            string companyNameShort = GetArgumentByTag("N:", args);
            if (companyNameShort == null)
                return;

			// do stuff
			DistributionService.DistributionService distributionService=new DistributionService.DistributionService();
            distributionService.Url = FI.Common.AppConfig.ReadSetting("DistributionServiceURL", "");
            distributionService.Timeout = int.Parse(FI.Common.AppConfig.ReadSetting("DistributionServiceTimeout", "600000"));

            string user = FI.Common.AppConfig.ReadSetting("DistributionServiceUser", "");
            string password = FI.Common.AppConfig.ReadSetting("DistributionServicePassword", "");

			if(user!="")
				distributionService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				distributionService.Credentials=System.Net.CredentialCache.DefaultCredentials;

            _callStart = DateTime.Now;
            _callCommand = "ProcessDistributionJobsAsync";
            distributionService.ProcessDistributionJobsAsync(companyNameShort);
            distributionService.ProcessDistributionJobsCompleted += new DistributionService.ProcessDistributionJobsCompletedEventHandler(distributionService_AsyncCallCompleted);
            if (_toLog)
                FI.Common.LogWriter.Instance.WriteEventLogEntry(_callCommand + " start. Company=" + companyNameShort);
            while ((DateTime.Now - _callStart) < _callStopWait)
                System.Threading.Thread.Sleep(1000);
            distributionService.Dispose();
		}

        static void distributionService_AsyncCallCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                OnException(_callCommand, e.Error);
            else if(_toLog)
                FI.Common.LogWriter.Instance.WriteEventLogEntry(_callCommand + " end. Time=" + (DateTime.Now-_callStart).ToString());
            _callStart = DateTime.MinValue;
            _callCommand = "";
        }


        private static void ProcessBackgroundJobs(string[] args)
        {
            // get company name
            string companyNameShort = GetArgumentByTag("N:", args);
            if (companyNameShort == null)
                return;

            // do stuff
            DistributionService.DistributionService distributionService = new DistributionService.DistributionService();
            distributionService.Url = FI.Common.AppConfig.ReadSetting("DistributionServiceURL", "");
            distributionService.Timeout = int.Parse(FI.Common.AppConfig.ReadSetting("DistributionServiceTimeout", "600000"));

            string user = FI.Common.AppConfig.ReadSetting("DistributionServiceUser", "");
            string password = FI.Common.AppConfig.ReadSetting("DistributionServicePassword", "");

            if (user != "")
                distributionService.Credentials = new System.Net.NetworkCredential(user, password);
            else
                distributionService.Credentials = System.Net.CredentialCache.DefaultCredentials;

            _callStart = DateTime.Now;
            _callCommand = "ProcessBackgroundJobsAsync";
            distributionService.ProcessBackgroundJobsAsync(companyNameShort);
            distributionService.ProcessBackgroundJobsCompleted += new DistributionService.ProcessBackgroundJobsCompletedEventHandler(distributionService_AsyncCallCompleted);
            if (_toLog)
                FI.Common.LogWriter.Instance.WriteEventLogEntry(_callCommand + " start. Company=" + companyNameShort);
            while ((DateTime.Now - _callStart) < _callStopWait)
                System.Threading.Thread.Sleep(1000);
            distributionService.Dispose();

            //distributionService.Dispose();
        }

		private static void Ping()
		{
			PingService.PingService pingService=new PingService.PingService();
            pingService.Url = FI.Common.AppConfig.ReadSetting("PingServiceURL", "");
            pingService.Timeout = int.Parse(FI.Common.AppConfig.ReadSetting("PingServiceTimeout", "300000"));

            string user = FI.Common.AppConfig.ReadSetting("PingServiceUser", "");
            string password = FI.Common.AppConfig.ReadSetting("PingServicePassword", "");
            string pingMdx = FI.Common.AppConfig.ReadSetting("PingMdx", "");
            string pingMailTo = FI.Common.AppConfig.ReadSetting("PingMailTo", "");

			if(user!="")						
				pingService.Credentials=new System.Net.NetworkCredential(user, password);
			else
				pingService.Credentials=System.Net.CredentialCache.DefaultCredentials;

            pingService.PingOlapSystem(pingMdx, pingMailTo);
		}

		private static void PingProxy()
		{
			PingService.PingService pingService=new PingService.PingService();
            pingService.Url = FI.Common.AppConfig.ReadSetting("PingServiceURL", "");
            pingService.Timeout = int.Parse(FI.Common.AppConfig.ReadSetting("PingServiceTimeout", "600000"));

            string user = FI.Common.AppConfig.ReadSetting("PingServiceUser", "");
            string password = FI.Common.AppConfig.ReadSetting("PingServicePassword", "");
            string pingProxy = FI.Common.AppConfig.ReadSetting("PingServiceProxy", "");
            string pingMdx = FI.Common.AppConfig.ReadSetting("PingMdx", "");
            string pingMailTo = FI.Common.AppConfig.ReadSetting("PingMailTo", "");

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

            string mailTo = FI.Common.AppConfig.ReadSetting("OnErrorMailTo", "");
			if (mailTo!="")
				Email.SendMail(Command + " Execution Failed", excToShow.Message + "\r\n" + excToShow.StackTrace, mailTo);
		}



	}
}
