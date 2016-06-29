using System;
using System.Collections.Generic;
using System.Text;

namespace OlapSystem.Management
{
    public class Email
    {
        public static void SendMail(string Subject, string Body, string MailTo)
        {

            OpenSmtp.Mail.MailMessage msg = new OpenSmtp.Mail.MailMessage();
            msg.From = new OpenSmtp.Mail.EmailAddress(AppConfig.SmtpSender);
            msg.To.Add(new OpenSmtp.Mail.EmailAddress(MailTo));
            msg.Subject = Subject;
            msg.HtmlBody = Body;

            OpenSmtp.Mail.SmtpConfig.LogToText = false;
            OpenSmtp.Mail.Smtp smtp = new OpenSmtp.Mail.Smtp();
            smtp.SendTimeout = 600;
            smtp.Host = AppConfig.SmtpServer;
            if (AppConfig.SmtpUserName != null && AppConfig.SmtpUserName != "")
            {
                smtp.Username = AppConfig.SmtpUserName;
                smtp.Password = AppConfig.SmtpPassword;
            }
            smtp.SendMail(msg);
        }
    }
}
