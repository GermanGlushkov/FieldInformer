using System;

namespace Setup.PlugIn
{
	/// <summary>
	/// Summary description for IVersion.
	/// </summary>
	public interface IVersion
	{
		void SetConfigSql(string IniPath , string SaPassword);
		void SetConfigService(string IniPath , string SaPassword);
		void SetConfigWeb(string IniPath);

		void WritebackConfigSql(string IniPath);
		void WritebackConfigService(string IniPath);
		void WritebackConfigWeb(string IniPath);

		void CopyIniToCurrentLocation(string IniPath);

		void InstallSql();
		void InstallService();
		void InstallWeb();

		void Log(string Message);

		string InstallVersion { get;}
		string PreviousVersion { get; set;}

	}
}
