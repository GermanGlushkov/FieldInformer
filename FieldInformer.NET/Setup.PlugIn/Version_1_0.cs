using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Setup.PlugIn
{
	public abstract class Version_1_0:VersionBase
	{
		public override byte MajorVersion
		{
			get { return 1;}
		}

		public override byte MinorVersion
		{
			get { return 0;}
		}

		
		protected string _sqlServer="";
		protected string _DBFINFBackupPath="";
		protected string _DBFINFCreate="";
		protected string _DBFINFDbPath="";
		protected string _DBFINFLogPath="";
		protected string _sqlJobStart="";
		protected int _sqlCommandTimeout=-1;

		protected string _olapServer="";
		protected string _olapHideHierarchies="";
		protected int _olapProcessorCount=-1;

		protected string _salesppIniPath="";

		protected string _routingConsolePath="";

		protected string _distrServiceUrl="";
		protected string _distrServiceUser="";
		protected string _distrServicePassword="";
		protected int _distrServiceTimeout=0;

		protected string _smtpSender="";
		protected string _smtpServer="";

		protected string _dataAccessServer="";

		


		protected override void doLoadConfigSql()
		{
			// --- SQL SERVER section
			this._sqlServer=ReadIniConfig("SQLSERVER" , "SERVER" , "");
			if(this._sqlServer=="")
				throw new Exception("Config: Invalid SQL SERVER identifier");			

			_DBFINFBackupPath=ReadIniConfig("SQLSERVER" , "DBFINF_BACKUP_PATH" , "");
			if(this._DBFINFBackupPath=="")
				throw new Exception("Config: Invalid DBFINF_BACKUP_PATH value");

			_DBFINFCreate=ReadIniConfig("SQLSERVER" , "DBFINF_CREATE" , "");

			_DBFINFDbPath=ReadIniConfig("SQLSERVER" , "DBFINF_DBPATH" , "");
			if(this._DBFINFDbPath.EndsWith(@"\"))
				this._DBFINFDbPath=this._DBFINFDbPath.Substring(0, this._DBFINFDbPath.Length-1);

			_DBFINFLogPath=ReadIniConfig("SQLSERVER" , "DBFINF_LOGPATH" , "");
			if(this._DBFINFLogPath.EndsWith(@"\"))
				this._DBFINFLogPath=this._DBFINFLogPath.Substring(0, this._DBFINFLogPath.Length-1);

			_sqlJobStart=ReadIniConfig("SQLSERVER" , "JOBSTART" , "");
			if(this._sqlJobStart=="")
				throw new Exception("Config: Invalid JOB START value");



			// --- OLAP SERVER section
			this._olapServer=ReadIniConfig("OLAPSERVER" , "SERVER" , "");
			if(this._olapServer=="")
				throw new Exception("Config: Invalid OLAP SERVER identifier");

			this._olapHideHierarchies=ReadIniConfig("OLAPSERVER" , "HIDE_HIERARCHIES" , "");



			// --- ROUTING section
			_routingConsolePath=ReadIniConfig("ROUTING" , "CONSOLE_PATH" , "");



			// --- SALESPP section
			_salesppIniPath=ReadIniConfig("SALESPP" , "INIPATH" , "");
			if(this._salesppIniPath=="")
				throw new Exception("Config: Invalid SALESPP INI path");



			// --- CONFIG section
			this._distrServiceUrl=ReadIniConfig("CONFIG" , "DISTR_SERVICE_URL" , "");
			if(this._distrServiceUrl=="")
				throw new Exception("Config: Invalid DISTR_SERVICE_URL value");

			this._distrServiceUser=ReadIniConfig("CONFIG" , "DISTR_SERVICE_USER" , "");

			this._distrServicePassword=ReadIniConfig("CONFIG" , "DISTR_SERVICE_PASSWORD" , "");

			this._distrServiceTimeout=int.Parse(ReadIniConfig("CONFIG" , "DISTR_SERVICE_TIMEOUT" , ""));
			if(this._distrServiceTimeout<-1)
				throw new Exception("Config: Invalid DISTR_SERVICE_TIMEOUT value");
		}


		protected override void doLoadConfigService()
		{

			// --- SQL SERVER section
			this._sqlServer=ReadIniConfig("SQLSERVER" , "SERVER" , "");
			if(this._sqlServer=="")
				throw new Exception("Config: Invalid SQL SERVER identifier");

			this._sqlCommandTimeout=int.Parse(ReadIniConfig("SQLSERVER" , "COMMAND_TIMEOUT" , "-1"));
			if(this._sqlCommandTimeout<0)
				throw new Exception("Config: Invalid COMMAND_TIMEOUT value");



			// --- OLAP SERVER section
			this._olapServer=ReadIniConfig("OLAPSERVER" , "SERVER" , "");
			if(this._olapServer=="")
				throw new Exception("Config: Invalid OLAP SERVER identifier");

			this._olapProcessorCount=int.Parse(ReadIniConfig("OLAPSERVER" , "PROCESSOR_COUNT" , "-1"));
			if(this._olapProcessorCount<=0)
				throw new Exception("Config: Invalid PROCESSOR_COUNT value");



			// --- CONFIG section
		}


		protected override void doLoadConfigWeb()
		{			
			// --- SMTP section
			this._smtpServer=ReadIniConfig("SMTP" , "SERVER" , "");
			if(this._smtpServer=="")
				throw new Exception("Config: Empty SMTP SERVER value");

			this._smtpSender=ReadIniConfig("SMTP" , "SENDER" , "");
			if(this._smtpSender=="")
				throw new Exception("Config: Empty SMTP SENDER value");



			// --- DATAACCESS section
			this._dataAccessServer=ReadIniConfig("DATAACCESS" , "SERVER" , "");
			if(this._dataAccessServer=="")
				throw new Exception("Config: Empty DATAACCESS SERVER value");
		}


		protected override void doWritebackConfigSql()
		{
			// --- SQLSERVER section
			WriteIniConfig("SQLSERVER" , "DBFINF_CREATE" , "False");
		}

		protected override void doWritebackConfigService()
		{
		}

		protected override void doWritebackConfigWeb()
		{
		}



		protected override void doInstallSql()
		{
			string targetDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.FullName;
			string installUtilDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
			InstallSql instSql=new InstallSql();

			//Analysis Services SP3 -----------------------------------------------------------------


			// DBSALESPP sql scripts -----------------------------------------------------------------
			instSql.Connect(_sqlServer , "DBSALESPP" , "sa" , this.SqlSaPassword);
			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBSALESPP_settings.txt" , false); //do not throw errors, in case of reinstall
			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBSALESPP_tables.txt" , true);
			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBSALESPP_views.txt" , true);
			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBSALESPP_sprocs.txt" , true);
			instSql.Disconnect();


			// DBFINF sql create dabase -----------------------------------------------------------------
			instSql.Connect(_sqlServer , "master" , "sa" , this.SqlSaPassword);
			if(this._DBFINFCreate.ToUpper()=="TRUE")
			{
				if(this._DBFINFDbPath!="")
					instSql.ExecuteSql(@"CREATE DATABASE [DBFINF]  ON (NAME = N'DBFINF_Data', FILENAME = N'" + this._DBFINFDbPath + @"\DBFINF_Data.MDF') LOG ON (NAME = N'DBFINF_Log', FILENAME = N'" + this._DBFINFLogPath + @"\DBFINF_Log.LDF')", true);
				else
					instSql.ExecuteSql(@"CREATE DATABASE [DBFINF]" , true); //default values
			}
			instSql.Disconnect();


			// DBFINF sql scripts -----------------------------------------------------------------
			instSql.Connect(_sqlServer , "DBFINF" , "sa" , this.SqlSaPassword);

			string msgs=instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBFINF_tables.txt" , false); // do not show errors, log them (in case of reinstall)
			if(msgs!="")
				this.Log(msgs);

			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBFINF_views.txt" , true);
			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBFINF_sprocs.txt" , true);
			
			//--
			string filePath=installUtilDir + @"\Version_1_0\sql\sql_scripts\DBFINF_settings.txt";
			System.IO.StreamReader sr=new StreamReader(filePath);
			string strCode=sr.ReadToEnd();
			sr.Close();

			strCode=strCode.Replace("###OLAP_SERVER###" , this._olapServer);

			System.IO.StreamWriter sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();

			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "DBFINF_settings.txt" , true);
			//--

			instSql.Disconnect();


			// DBSALESPP DSN must exist, do not create -----------------------------------------------------------------


			// NIGHTLY JOB DTS (edit and run vbs file) -----------------------------------------------------------------
			filePath=installUtilDir + @"\Version_1_0\sql\FI_nightly_job.vbs";
			sr=new StreamReader(filePath);
			strCode=sr.ReadToEnd();
			sr.Close();

			strCode=strCode.Replace("###SA_PASS###" , this.SqlSaPassword);
			strCode=strCode.Replace("###SPP_PASS###" , "spp" );
			strCode=strCode.Replace("###SPP_INI_PATH###" , this._salesppIniPath);
			strCode=strCode.Replace("###OLAP_SERVER###" , this._olapServer);
			strCode=strCode.Replace("###HIDE_HIERARCHIES###" , this._olapHideHierarchies);

			sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();

			System.Diagnostics.Process process=System.Diagnostics.Process.Start(filePath);
			process.WaitForExit();
			process.Close();
			

			// COPY CS.EXE -----------------------------------------------------------------
			filePath=installUtilDir + @"\Version_1_0\sql\SC.EXE"; 
			System.IO.File.Copy(filePath , System.Environment.SystemDirectory + @"\SC.EXE", true); //copy sc.exe


			// NIGHTLY JOB SCHEDULE (edit and run sql script) -----------------------------------------------------------------
			filePath=installUtilDir+ @"\Version_1_0\sql\sql_scripts\JOB.txt";
			sr=new StreamReader(filePath);
			strCode=sr.ReadToEnd();
			sr.Close();

			strCode=strCode.Replace("###BCK_PATH###" , this._DBFINFBackupPath);
			strCode=strCode.Replace("###SYSDIR###" , System.Environment.SystemDirectory);
			strCode=strCode.Replace("###CLIENT_PATH###" , targetDir);
			if(this._olapServer.ToUpper()!="LOCALHOST")
				strCode=strCode.Replace("###OLAP_SERVER###" , @"\\" + this._olapServer);
			else
				strCode=strCode.Replace("###OLAP_SERVER###" , "");
			strCode=strCode.Replace("###ROUTING_CONSOLE_PATH###" , this._routingConsolePath);
			strCode=strCode.Replace("###JOB_START###" , this._sqlJobStart);

			sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();

			instSql.Connect(_sqlServer , "master" , "sa" , this.SqlSaPassword);
			instSql.ExecuteSqlFile(@"Version_1_0\sql\sql_scripts" , "JOB.txt" , true);
			instSql.Disconnect();


			// ConoleClient CONFIG -----------------------------------------------------------------
			filePath=targetDir + @"\UI.ConsoleClient.exe.config";

			XmlDocument doc=new XmlDocument();
			doc.Load(filePath);
			XmlElement confEl=(XmlElement)doc.GetElementsByTagName("FIConfig")[0];
			foreach(XmlElement el in confEl.ChildNodes)
			{
				if(el.Name.ToUpper()=="ADD")
				{
					if (el.GetAttribute("key").ToUpper()=="DistributionServiceURL".ToUpper())
						el.SetAttribute("value", this._distrServiceUrl);
					else if (el.GetAttribute("key").ToUpper()=="DistributionServiceUser".ToUpper())
						el.SetAttribute("value", this._distrServiceUser);
					else if (el.GetAttribute("key").ToUpper()=="DistributionServicePassword".ToUpper())
						el.SetAttribute("value", this._distrServicePassword);
					else if (el.GetAttribute("key").ToUpper()=="DistributionServiceTimeout".ToUpper())
						el.SetAttribute("value", this._distrServiceTimeout.ToString());
				}
			}
			doc.Save(filePath);
		}



		protected override void doInstallService()
		{
			string targetDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.FullName;
			string installUtilDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

			//Analysis Services SP3 -----------------------------------------------------------------

			// PTS sp3 -----------------------------------------------------------------

			// MDAC 7 or higher  -----------------------------------------------------------------

			// stop, unreg service -----------------------------------------------------------------
			string filePath=System.Environment.SystemDirectory + @"\net.exe";
			System.Diagnostics.Process process=System.Diagnostics.Process.Start(filePath , @"stop ""fieldinformer.net service"" ");
			process.WaitForExit();
			process.Close();

			filePath=System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"installutil.exe" ;
			process=System.Diagnostics.Process.Start(filePath, @"-u """ + targetDir + @"\fi.winservices.exe""");
			process.WaitForExit();
			process.Close();


			// create DBSALESPP DSN -----------------------------------------------------------------
			Microsoft.Win32.RegistryKey key=Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\ODBC\ODBC.INI\DBSALESPP"); 
			key.SetValue("Database" , "DBSALESPP");
			key.SetValue("Description" , "Sales++ Data Source"); 
			key.SetValue("Driver" , System.Environment.SystemDirectory + @"\SQLSRV32.dll"); 
			key.SetValue("LastUser" , "spp");
			key.SetValue("Server" , this._sqlServer);
			key.Close();

			Microsoft.Win32.RegistryKey key2=Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources");
			key2.SetValue("DBSALESPP", "SQL Server");
			key2.Close();
			

			// restore OLAP DB -----------------------------------------------------------------
			filePath=installUtilDir + @"\Version_1_0\service\RestoreOlapCab.exe";
			process=System.Diagnostics.Process.Start(filePath);
			process.WaitForExit();
			process.Close();


			// edit OLAP data source (edit and run vbs) -----------------------------------------------------------------
			filePath=installUtilDir + @"\Version_1_0\service\EditOlapDataSource.vbs";
			System.IO.StreamReader sr=new StreamReader(filePath);
			string strCode=sr.ReadToEnd();
			sr.Close();

			strCode=strCode.Replace("###SPP_PASS###" , "spp" );

			System.IO.StreamWriter sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();

			process=System.Diagnostics.Process.Start(filePath);
			process.WaitForExit();
			process.Close();


			// --- write to CONFIG (FI.WinServices.exe) -----------------------------------------------------------------
			filePath=targetDir + @"\FI.WinServices.exe.config";

			XmlDocument doc=new XmlDocument();
			doc.Load(filePath);
			XmlElement confEl=(XmlElement)doc.GetElementsByTagName("FIConfig")[0];
			foreach(XmlElement el in confEl.ChildNodes)
			{
				if(el.Name.ToUpper()=="ADD")
				{
					if (el.GetAttribute("key").ToUpper()=="DA_CommandTimeout".ToUpper())
						el.SetAttribute("value", this._sqlCommandTimeout.ToString());
					else if (el.GetAttribute("key").ToUpper()=="DA_OlapProcessorPath".ToUpper())
						el.SetAttribute("value", targetDir + @"\OlapSystem.Processor.exe" );
					else if (el.GetAttribute("key").ToUpper()=="DA_OlapProcessorCount".ToUpper())
						el.SetAttribute("value", this._olapProcessorCount.ToString());
					else if (el.GetAttribute("key").ToUpper()=="DA_MeasuresHierarchyConfig".ToUpper())
						el.SetAttribute("value",  targetDir + @"\Config\Measures.config" );
				}
			}
			doc.Save(filePath);


			// --- write to CONFIG (FI.OlapProcessor.exe) -----------------------------------------------------------------
			// nothing to write

			// reg, start service -----------------------------------------------------------------
			filePath=System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"installutil.exe" ;
			process=System.Diagnostics.Process.Start(filePath, @" """ + targetDir + @"\fi.winservices.exe""");
			process.WaitForExit();
			process.Close();

			filePath=System.Environment.SystemDirectory + @"\net.exe";
			process=System.Diagnostics.Process.Start(filePath , @"start ""fieldinformer.net service"" ");
			process.WaitForExit();
			process.Close();


			// put FIXmlCellset , MSMDCOnnPool selfReg on -----------------------------------------------------------------		
		}


		protected override void doInstallWeb()
		{
			string targetDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.FullName;
			string installUtilDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

			// --- directory security?

			// --- write to CONFIG files (remoting)
			string filePath=targetDir + @"\web.config";
			System.IO.StreamReader sr=new StreamReader(filePath);
			string strCode=sr.ReadToEnd();
			sr.Close();

			strCode=strCode.Replace("tcp://localhost" , "tcp://" + this._dataAccessServer );

			System.IO.StreamWriter sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();



			// --- write to CONFIG files (FIConfig)
			filePath=targetDir + @"\web.config";

			XmlDocument doc=new XmlDocument();
			doc.Load(filePath);
			XmlElement confEl=(XmlElement)doc.GetElementsByTagName("FIConfig")[0];
			foreach(XmlElement el in confEl.ChildNodes)
			{
				if(el.Name.ToUpper()=="ADD")
				{
					if (el.GetAttribute("key").ToUpper()=="TempDir".ToUpper())
						el.SetAttribute("value", targetDir + @"\Temp" );
					else if (el.GetAttribute("key").ToUpper()=="LogPath".ToUpper())
						el.SetAttribute("value",  targetDir + @"\Temp\AppLog.txt" );
					else if (el.GetAttribute("key").ToUpper()=="SmtpServer".ToUpper())
						el.SetAttribute("value", this._smtpServer);
					else if (el.GetAttribute("key").ToUpper()=="SmtpSender".ToUpper())
						el.SetAttribute("value",  this._smtpSender);
				}
			}
			doc.Save(filePath);

			// put GraphCom selfReg on -----------------------------------------------------------------
		}
		


	}
}
