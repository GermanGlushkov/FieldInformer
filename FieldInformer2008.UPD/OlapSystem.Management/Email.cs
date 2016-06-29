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
            msg.From = new System.Net.Mail.MailAddress(FI.Common.AppConfig.SmtpSender);
            msg.To.Add(MailTo);
            msg.Subject = Subject;
            msg.Body = Body;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = FI.Common.AppConfig.SmtpServer;
            if (FI.Common.AppConfig.SmtpPort != 0)
                smtp.Port = FI.Common.AppConfig.SmtpPort;
            smtp.DeliveryMethod=System.Net.Mail.SmtpDeliveryMethod.Network;
            if (FI.Common.AppConfig.SmtpUserName != null && FI.Common.AppConfig.SmtpUserName != "")
                smtp.Credentials = new System.Net.NetworkCredential(FI.Common.AppConfig.SmtpUserName, FI.Common.AppConfig.SmtpPassword);
            
            smtp.Send(msg);
        }
    }
}
