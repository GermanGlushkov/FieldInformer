<%@ WebService Language="C#" Class="SoapAdapter" %>

using System;
using System.Reflection;
using System.Web.Services;
using OpenSmtp.Mail;


[ WebService (Namespace="http://sourceforge.net/openSmtp") ]
public class SoapAdapter : WebService {

	private Smtp smtp;
	private MailMessage msg;
	private EmailAddress sender;
	private EmailAddress recipient;
	
	[WebMethod]   
	public String Send(string senderEmail, string recipientEmail, 
				string subject, string body, string host, int port) 
	{
		try
		{
			EmailAddress senderAddress = new EmailAddress(senderEmail);
			EmailAddress recipientAddress = new EmailAddress(recipientEmail);
			
			msg 		= new MailMessage(senderAddress, recipientAddress);
			msg.ReplyTo = senderAddress;
			msg.Subject = subject;
			msg.Body 	= body;
			
			Smtp smtp 	= new Smtp(host, port);
			smtp.SendMail(msg);			
		
			return "Message delivered.";
		}
		catch(SmtpException se)
		{
			return "error: " + se.Message;
		}
	}


}

