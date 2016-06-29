using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Setup.PlugIn
{
	public class Version_1_2:Version_1_0
	{
		private string _olapRestoreDbOnError="";

		public override byte MajorVersion
		{
			get { return 1;}
		}

		public override byte MinorVersion
		{
			get { return 2;}
		}

		protected override void doLoadConfigSql()
		{
			// load base
			base.doLoadConfigSql();

			// load			
			_olapRestoreDbOnError=ReadIniConfig("OLAPSERVER" , "RESTORE_DB_ON_ERROR" , "");
			if(_olapRestoreDbOnError=="")
				throw new Exception("Config: Empty RESTORE_DB_ON_ERROR value");
			if(_olapRestoreDbOnError=="0" || _olapRestoreDbOnError.ToUpper()=="FALSE")
				_olapRestoreDbOnError="";
		}

		protected override void doLoadConfigService()
		{
			// load base
			base.doLoadConfigService();
		}

		protected override void doLoadConfigWeb()
		{			
			// load base
			base.doLoadConfigWeb();
		}



		protected override void doInstallSql()
		{
			// prev version
			if(base.ValidateInstall(false))
				base.doInstallSql();

			string targetDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.FullName;
			string installUtilDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
			InstallSql instSql=new InstallSql();

			// DBSALESPP sql scripts -----------------------------------------------------------------
			instSql.Connect(_sqlServer , "DBSALESPP" , "sa" , this.SqlSaPassword);
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBSALESPP_settings.txt" , true); 
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBSALESPP_tables.txt" , true);
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBSALESPP_views.txt" , true);
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBSALESPP_sprocs.txt" , true);
			instSql.Disconnect();

			// DBFINF sql scripts -----------------------------------------------------------------
			instSql.Connect(_sqlServer , "DBFINF" , "sa" , this.SqlSaPassword);
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBFINF_tables.txt" , true); 
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBFINF_views.txt" , true);
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "DBFINF_sprocs.txt" , true);
			instSql.Disconnect();


			// NIGHTLY JOB DTS (edit and run vbs file) -----------------------------------------------------------------
			string filePath=installUtilDir + @"\Version_1_2\sql\FI_nightly_job.vbs";
			StreamReader sr=new StreamReader(filePath);
			string strCode=sr.ReadToEnd();
			sr.Close();

			strCode=strCode.Replace("###SA_PASS###" , this.SqlSaPassword);
			strCode=strCode.Replace("###SPP_PASS###" , "spp" );
			strCode=strCode.Replace("###SPP_INI_PATH###" , this._salesppIniPath);
			strCode=strCode.Replace("###OLAP_SERVER###" , this._olapServer);
			strCode=strCode.Replace("###HIDE_HIERARCHIES###" , this._olapHideHierarchies);

			StreamWriter sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();

			System.Diagnostics.Process process=System.Diagnostics.Process.Start(filePath);
			process.WaitForExit();
			process.Close();


			// NIGHTLY JOB SCHEDULE (edit and run sql script) -----------------------------------------------------------------
			filePath=installUtilDir+ @"\Version_1_2\sql\sql_scripts\JOB.txt";
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
			strCode=strCode.Replace("###RESTORE_DB_ON_ERROR###" , this._olapRestoreDbOnError);
			strCode=strCode.Replace("###JOB_START###" , this._sqlJobStart);

			sw=new StreamWriter(filePath , false); //not append
			sw.Write(strCode);
			sw.Close();

			instSql.Connect(_sqlServer , "master" , "sa" , this.SqlSaPassword);
			instSql.ExecuteSqlFile(@"Version_1_2\sql\sql_scripts" , "JOB.txt" , true);
			instSql.Disconnect();
		}

		protected override void doInstallService()
		{						
			// prev version
			if(base.ValidateInstall(false))
				base.doInstallService();
		}

		protected override void doInstallWeb()
		{			
			// prev version
			if(base.ValidateInstall(false))
				base.doInstallWeb();
		}


		protected override void doWritebackConfigSql()
		{
			// nothing to writeback
		}

		protected override void doWritebackConfigService()
		{
			// nothing to writeback
		}

		protected override void doWritebackConfigWeb()
		{
			// nothing to writeback
		}




	}
}
