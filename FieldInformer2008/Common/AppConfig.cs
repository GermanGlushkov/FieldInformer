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
    public class AppConfig : IConfigurationSectionHandler 
	{
		public AppConfig()
		{
		}



		protected static string _defaultSection = "FIConfig";
		protected static NameValueCollection _config=null;		


		// this method is being called from Global.asas Application_Start 
		public static void OnApplicationStart()
		{
			_config=(NameValueCollection) System.Configuration.ConfigurationManager.GetSection( _defaultSection );
		}



		public static string ReadSetting( string key, string defaultValue , string section )
		{
			try
			{
				Object setting = null;

				if(_config==null)
                    _config = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection(_defaultSection);

				if (section!=null && section!="" && section!=_defaultSection)
				{
                    NameValueCollection settings = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection(section);

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

        public static int DA_MdxCommandTimeout
        {
            get { return int.Parse(ReadSetting("DA_MdxCommandTimeout", "7200")); }
        }



        public static string CompanyName
        {
            get { return ReadSetting("CompanyName", "FieldForce Solutions"); }
        }


		public static string AppName
		{
			get {return ReadSetting("AppName" , ""); }
		}

        public static byte DefaultCssStyle
        {
            get { return byte.Parse(ReadSetting("DefaultCssStyle", "1")); }
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


        public static int SmtpPort
        {
            get
            {
                int ret = 0;
                int.TryParse(ReadSetting("SmtpPort", "0"), out ret);
                return ret;
            }
        }

		public static string SmtpUserName
		{
			get {return ReadSetting("SmtpUserName" , ""); }
		}

		public static string SmtpPassword
		{
			get {return ReadSetting("SmtpPassword" , ""); }
		}


		public static int DA_OlapProcessorCount
		{
			get {return int.Parse(ReadSetting("DA_OlapProcessorCount" , "3")); }
		}




		public static bool ReportRunOnClick
		{
			get {return (ReadSetting("ReportRunOnClick" , "").ToUpper()=="TRUE"?true:false); }
		}

		/*
		static string __customNumberFormatString=null;
		public static string CustomNumberFormatString
		{
			get 
			{
				if(__customNumberFormatString==null)
					__customNumberFormatString=ReadSetting("CustomNumberFormatString" , ""); 
				return __customNumberFormatString;
			}
		}

		static System.Globalization.NumberFormatInfo __customNumberFormatInfo;
		public static System.Globalization.NumberFormatInfo CustomNumberFormatInfo
		{
			get 
			{
				if(__customNumberFormatInfo==null)
				{
					__customNumberFormatInfo=(System.Globalization.NumberFormatInfo)System.Globalization.NumberFormatInfo.CurrentInfo.Clone();

					string decimalSeparator=ReadSetting("CustomNumberDecimalSeparator" , "");
					if(decimalSeparator!="")
					{
						__customNumberFormatInfo.CurrencyDecimalSeparator=decimalSeparator;
						__customNumberFormatInfo.NumberDecimalSeparator=decimalSeparator;
					}

					string groupSeparator=ReadSetting("CustomNumberGroupSeparator" , "");
					if(groupSeparator!="")
					{
						__customNumberFormatInfo.CurrencyGroupSeparator=groupSeparator;
						__customNumberFormatInfo.NumberGroupSeparator=groupSeparator;
					}
				}
				return __customNumberFormatInfo;
			}
		}
		*/


		static string __numberFormatReplaceComma=null;
		public static string NumberFormatReplaceComma
		{
			get 
			{
				if(__numberFormatReplaceComma==null)
					__numberFormatReplaceComma=ReadSetting("NumberFormatReplaceComma" , ""); 
				return __numberFormatReplaceComma;
			}
		}

		static string __numberFormatReplaceDot=null;
		public static string NumberFormatReplaceDot
		{
			get 
			{
				if(__numberFormatReplaceDot==null)
					__numberFormatReplaceDot=ReadSetting("NumberFormatReplaceDot" , ""); 
				return __numberFormatReplaceDot;
			}
		}

		static string __numberFormatReplaceSpace=null;
		public static string NumberFormatReplaceSpace
		{
			get 
			{
				if(__numberFormatReplaceSpace==null)
					__numberFormatReplaceSpace=ReadSetting("NumberFormatReplaceSpace" , ""); 
				return __numberFormatReplaceSpace;
			}
		}
	}
}
