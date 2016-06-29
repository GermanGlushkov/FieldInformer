using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace OlapSystem.Management
{
    public class AppConfig
    {



        public static string EventLogName
        {
            get { return ConfigurationManager.AppSettings["EventLogName"]; }
        }

        public static string EventLogSource
        {
            get { return ConfigurationManager.AppSettings["EventLogSource"]; }
        }




        public static string SmtpServer
        {
            get { return ConfigurationManager.AppSettings["SmtpServer"]; }
        }

        public static string SmtpSender
        {
            get { return ConfigurationManager.AppSettings["SmtpSender"]; }
        }

        public static string SmtpUserName
        {
            get { return ConfigurationManager.AppSettings["SmtpUserName"]; }
        }

        public static string SmtpPassword
        {
            get { return ConfigurationManager.AppSettings["SmtpPassword"]; }
        }


        public static string ReadSetting(string key, string defaultValue)
        {
            string ret = ConfigurationManager.AppSettings[key];
            return (ret == null ? defaultValue : key);
        }

    }
    
}
