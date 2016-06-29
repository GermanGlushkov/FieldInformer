using System.Configuration;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;

namespace FI.Common
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class AppConfig: IConfigurationSectionHandler
	{
		public AppConfig()
		{
		}



		protected static string _defaultSection = "FIConfig";
		protected static NameValueCollection _config=null;


		// this method is being called from Global.asas Application_Start 
		public static void OnApplicationStart()
		{
			_config=(NameValueCollection) System.Configuration.ConfigurationSettings.GetConfig( _defaultSection );
		}



		public static string ReadSetting( string key, string defaultValue , string section )
		{
			try
			{
				Object setting = null;

				if(_config==null)
					_config=(NameValueCollection) System.Configuration.ConfigurationSettings.GetConfig( _defaultSection );

				if (section!=null && section!="" && section!=_defaultSection)
				{
					NameValueCollection settings =(NameValueCollection) System.Configuration.ConfigurationSettings.GetConfig( section );

					if ( settings != null )
						setting = settings[key];
				}
				else
				{
					if ( _config != null )
						setting = _config[key];
				}

				return (setting == null) ? defaultValue : (string)setting;

				
			}
			catch(Exception exc)
			{
				return defaultValue;
			}
			catch
			{
				return defaultValue;
			}
		}


		public static string ReadSetting( string key, string defaultValue )
		{
			return ReadSetting( key, defaultValue, _defaultSection );
		}



		public Object Create(Object parent, object configContext, XmlNode section)
		{
			NameValueCollection settings;
                    
			try
			{
				NameValueSectionHandler baseHandler = new NameValueSectionHandler();
				settings = (NameValueCollection)baseHandler.Create(parent, configContext, section);
			}
			catch(Exception exc)
			{
				Common.LogWriter.Instance.WriteEventLogEntry(exc);
				settings = null;
			}
        
			return settings;
		}




		public static string EventLogName
		{
			get {return ReadSetting("EventLogName" , ""); }
		}

		public static string EventLogSource
		{
			get {return ReadSetting("EventLogSource" , ""); }
		}


		public static string DA_ConnectionString
		{
			get {return ReadSetting("DA_ConnectionString" , ""); }
		}

		public static string DA_OltpConnectionString
		{
			get {return ReadSetting("DA_OltpConnectionString" , ""); }
		}


		public static int DA_CommandTimeout
		{
			get {return int.Parse(ReadSetting("DA_CommandTimeout" , "30")); }
		}

		
		public static string DA_MeasuresHierarchyConfig
		{
			get {return ReadSetting("DA_MeasuresHierarchyConfig" , ""); }
		}


		public static string AppName
		{
			get {return ReadSetting("AppName" , ""); }
		}

		public static string GlobalStyleDir
		{
			get {return ReadSetting("GlobalStyleDir" , ""); }
		}


		public static bool HideCustomReports
		{
			get {return (ReadSetting("HideCustomReports" , "").ToUpper()=="TRUE"?true:false); }
		}

		public static bool AuditPageHits
		{
			get {return (ReadSetting("AuditPageHits" , "").ToUpper()=="TRUE"?true:false); }
		}

		public static string TempDir
		{
			get {return ReadSetting("TempDir" , ""); }
		}

		public static string TempVirtualDir
		{
			get {return ReadSetting("TempVirtualDir" , ""); }
		}

		public static string DA_OlapQueryDir
		{
			get {return ReadSetting("DA_OlapQueryDir" , ""); }
		}

		public static string LogPath
		{
			get {return ReadSetting("LogPath" , ""); }
		}

		public static string AppVirtualDir
		{
			get {return ReadSetting("AppVirtualDir" , ""); }
		}

		public static bool IsDebugMode
		{
			get {return bool.Parse(ReadSetting("IsDebugMode" , "false")); }
		}

		public static string SmtpSender
		{
			get {return ReadSetting("SmtpSender" , ""); }
		}

		public static string SmtpServer
		{
			get {return ReadSetting("SmtpServer" , ""); }
		}

		public static string SmtpUserName
		{
			get {return ReadSetting("SmtpUserName" , ""); }
		}

		public static string SmtpPassword
		{
			get {return ReadSetting("SmtpPassword" , ""); }
		}



		public static string DA_OlapProcessorPath
		{
			get {return ReadSetting("DA_OlapProcessorPath" , ""); }
		}

		public static int DA_OlapProcessorCount
		{
			get {return int.Parse(ReadSetting("DA_OlapProcessorCount" , "")); }
		}

		/*
		public static int DA_OlapProcessingQueueCount
		{
			get {return int.Parse(ReadSetting("DA_OlapProcessingQueueCount" , "1")); }
		}
		*/

		/*
		public static int DA_OlapQueueTimeout
		{
			get {return int.Parse(ReadSetting("DA_OlapQueueTimeout" , "")); }
		}
		*/


		/*
		public static string ReportExportStyle
		{
			get 
			{
				string setting=ReadSetting("ReportExportStyle" , "");
                return setting.Replace("##GlobalStyleDir##" , FI.Common.AppConfig.GlobalStyleDir);			
			}
		}
		*/



		public static bool ReportRunOnClick
		{
			get {return (ReadSetting("ReportRunOnClick" , "").ToUpper()=="TRUE"?true:false); }
		}


	}
}