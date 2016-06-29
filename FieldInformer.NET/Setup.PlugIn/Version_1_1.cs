using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Setup.PlugIn
{
	public class Version_1_1:Version_1_0
	{
		public Version_1_1():base()
		{
		}

		public new string InstallVersion
		{
			get { return "1.1";}
		}

		#region SET CONFIG
		public override void SetConfigSql(string IniPath, string SaPassword)
		{
			//base
			base.SetConfigSql(IniPath , SaPassword);

			//added
		}

		public override void SetConfigService(string IniPath, string SaPassword)
		{
			//base
			base.SetConfigService(IniPath , SaPassword);

			//added 
		}

		public override void SetConfigWeb(string IniPath)
		{
			//base
			base.SetConfigWeb(IniPath);

			//added 
		}

		#endregion

		#region VALIDATE CONFIG		
		protected override void ValidateSqlConfig()
		{
			//base
			base.ValidateSqlConfig();

			//added 
		}

		protected override void ValidateServiceConfig()
		{
			//base
			base.ValidateServiceConfig();

			//added 
		}

		protected override void ValidateWebConfig()
		{
			//base
			base.ValidateWebConfig();

			//added 
		}

		#endregion

		#region WRITE CONFIG
		public override void WritebackConfigSql(string IniPath)
		{
			//base
			base.WritebackConfigSql(IniPath);

			//added 
		}

		public override void WritebackConfigService(string IniPath)
		{
			//base
			base.WritebackConfigService(IniPath);

			//added 
		}

		public override void WritebackConfigWeb(string IniPath)
		{
			//base
			base.WritebackConfigWeb(IniPath);

			//added 
		}

		#endregion


		#region INSTALL
		public override void InstallWeb()
		{
			//base
			base.InstallWeb();


			// check if version already installed
			if(this.InstallVersion.CompareTo(this.PreviousVersion)<=0)  // if version already installed
				return;
			//validate config
			this.ValidateWebConfig();
		}

		public override void InstallService()
		{
			//base
			base.InstallService();

			// check if version already installed
			if(this.InstallVersion.CompareTo(this.PreviousVersion)<=0)  // if version already installed
				return;
			//validate config
			this.ValidateWebConfig();
		}

		public override void InstallSql()
		{
			//base
			base.InstallSql();

			// check if version already installed
			if(this.InstallVersion.CompareTo(this.PreviousVersion)<=0)  // if version already installed
				return;
			//validate config
			this.ValidateWebConfig();
		}

		#endregion



	}
}
