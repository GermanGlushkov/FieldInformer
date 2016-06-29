using System;
using System.Collections.Generic;
using System.Text;

namespace OlapSystem.Management
{
    public class Email
    {
        public static void SendMail(string Subject, string Body, string MailTo)
        {

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(AppConfig.SmtpSender);
            msg.To.Add(MailTo);
            msg.Subject = Subject;
            msg.Body = Body;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = AppConfig.SmtpServer;
            smtp.DeliveryMethod=System.Net.Mail.SmtpDeliveryMethod.Network;
            if (AppConfig.SmtpUserName != null && AppConfig.SmtpUserName != "")
                smtp.Credentials = new System.Net.NetworkCredential(AppConfig.SmtpUserName, AppConfig.SmtpPassword);
            
            smtp.Send(msg);
        }
    }
}
