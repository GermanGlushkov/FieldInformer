using System;

namespace Setup.PlugIn
{
	public abstract class VersionBase
	{
		// static
#warning here is place where version should be changed
		public static VersionBase LatestVersion=new Version_1_2();


		private string _iniPath;
		private string _sqlSaPass;		
		private byte _overrideMajorVersion=0;
		private byte _overrideMinorVersion=0;
		private FI.Common.IniReader _iniReader;
		private bool _configSqlLoaded=false;
		private bool _configServiceLoaded=false;
		private bool _configWebLoaded=false;

		abstract public byte MajorVersion {get;}
		abstract public byte MinorVersion {get;}

		abstract protected void doLoadConfigSql();
		abstract protected void doLoadConfigService();
		abstract protected void doLoadConfigWeb();

		abstract protected void doWritebackConfigSql();
		abstract protected void doWritebackConfigService();
		abstract protected void doWritebackConfigWeb();

		abstract protected void doInstallSql();
		abstract protected void doInstallService();
		abstract protected void doInstallWeb();


		public string VersionString
		{
			get {return this.MajorVersion.ToString() + "." + this.MinorVersion.ToString();}
		}

		public byte OverrideMajorVersion
		{ 
			get { return _overrideMajorVersion;}
		}

		public byte OverrideMinorVersion
		{ 
			get { return _overrideMinorVersion;}
		}

		public string OverrideVersionString
		{
			get {return this.OverrideMajorVersion.ToString() + "." + this.OverrideMinorVersion.ToString();}
		}

		public string IniPath
		{
			get { return _iniPath;}
			set
			{
				if(_iniPath==value)
					return;
				
				if(System.IO.File.Exists(value)==false)
					throw new Exception("Read Ini: file not exists " + value);

				_iniPath=value;				
				_iniReader=new FI.Common.IniReader(value);				
			}
		}

		public string SqlSaPassword
		{
			get { return _sqlSaPass;}
			set
			{
				_sqlSaPass=value;
			}
		}

		public bool ConfigSqlLoaded
		{ 
			get { return _configSqlLoaded;}
		}

		public bool ConfigServiceLoaded
		{ 
			get { return _configServiceLoaded;}
		}

		public bool ConfigWebLoaded
		{ 
			get { return _configWebLoaded;}
		}

		public void LoadConfigSql()
		{
			if(_configSqlLoaded)
				return;

			try
			{
				this.GetOverrideVersion();
				this.doLoadConfigSql();
			}
			catch(Exception exc)
			{
				this.Log(exc);
			}
			_configSqlLoaded=true;
		}

		public void LoadConfigService()
		{
			if(_configServiceLoaded)
				return;

			try
			{
				this.GetOverrideVersion();
				this.doLoadConfigService();
			}
			catch(Exception exc)
			{
				this.Log(exc);
			}
			_configServiceLoaded=true;
		}

		public void LoadConfigWeb()
		{
			if(_configWebLoaded)
				return;

			try
			{
				this.GetOverrideVersion();
				this.doLoadConfigWeb();
			}
			catch(Exception exc)
			{
				this.Log(exc);
			}
			_configWebLoaded=true;
		}

		public void InstallSql()
		{			
			try
			{
				this.ValidateInstall(true); // will throw exception

				LoadConfigSql();
				doInstallSql();
				WritebackConfigSql();
			}
			catch(Exception exc)
			{
				this.Log(exc);
				throw exc;
			}
		}

		public void InstallService()
		{			
			try
			{
				this.ValidateInstall(true); // will throw exception

				LoadConfigService();
				doInstallService();
				WritebackConfigService();
			}
			catch(Exception exc)
			{
				this.Log(exc);
				throw exc;
			}
		}

		public void InstallWeb()
		{
			try
			{
				this.ValidateInstall(true); // will throw exception

				LoadConfigWeb();
				doInstallWeb();
				WritebackConfigWeb();
			}
			catch(Exception exc)
			{
				this.Log(exc);
				throw exc;
			}
		}

		protected void WriteIniConfig(string section , string key, string value)
		{
			_iniReader.Write(section, key, value);
		}

		protected string ReadIniConfig(string section , string key, string defaultValue)
		{
			return _iniReader.ReadString(section, key , defaultValue);
		}

		protected bool ValidateInstall(bool throwException)
		{
			GetOverrideVersion();

			bool vaild=true;
			if(this.OverrideMajorVersion>this.MajorVersion)
				vaild=false;
			if(this.OverrideMajorVersion==this.MajorVersion && this.OverrideMinorVersion>this.MinorVersion)
				vaild=false;

			if(!vaild && throwException)
				throw new Exception("Newer version already installed");
			return vaild;
		}

		private void GetOverrideVersion()
		{						
			// --- get from INFO section
			string ver=this.ReadIniConfig("INFO",  "VERSION" , "");
			try
			{
				int i=ver.IndexOf(".");
				_overrideMajorVersion=byte.Parse(ver.Substring(0, i));
				_overrideMinorVersion=byte.Parse(ver.Substring(i+1));
			}
			catch(Exception exc)
			{
				_overrideMajorVersion=0;
				_overrideMinorVersion=0;
			}			
		}

		private void WritebackConfigSql()
		{
			// --- INFO section
			WriteIniConfig("INFO" , "VERSION" , this.MajorVersion.ToString() + "." + this.MinorVersion.ToString());

			this.doWritebackConfigSql();
		}

		private void WritebackConfigService()
		{
			// --- INFO section
			WriteIniConfig("INFO" , "VERSION" , this.MajorVersion.ToString() + "." + this.MinorVersion.ToString());

			this.doWritebackConfigService();
		}

		private void WritebackConfigWeb()
		{
			// --- INFO section
			WriteIniConfig("INFO" , "VERSION" , this.MajorVersion.ToString() + "." + this.MinorVersion.ToString());

			this.doWritebackConfigService();
		}

		private void CopyIniToCurrentLocation()
		{
			string iniFileName=System.IO.Path.GetFileName(IniPath);
			string destPath=System.IO.Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName +  @"\" + iniFileName;
			if(IniPath.ToUpper().Equals(destPath.ToUpper())==false)
				System.IO.File.Copy(IniPath , destPath , true);
		}

		protected virtual void Log(string Message)
		{
			string logPath=System.Reflection.Assembly.GetExecutingAssembly().Location + ".log";
			System.IO.StreamWriter sw=System.IO.File.AppendText(logPath);
			sw.WriteLine(System.DateTime.Now.ToShortDateString() + " " +  System.DateTime.Now.ToShortTimeString() + "\t" + Message);
			sw.Close();
		}

		protected virtual void Log(Exception exc)
		{
			Log(exc.Message + "\r\n" + exc.StackTrace);
		}
	}

}
