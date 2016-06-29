namespace OpenSmtp.Mail.Test {

/******************************************************************************
	Copyright 2001-2004 Ian Stallings
	OpenSmtp.Net is free software; you can redistribute it and/or modify
	it under the terms of the Lesser GNU General Public License as published by
	the Free Software Foundation; either version 2 of the License, or
	(at your option) any later version.

	OpenSmtp.Net is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	Lesser GNU General Public License for more details.

	You should have received a copy of the Lesser GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
/*******************************************************************************/



using NUnit.Core;
using NUnit.Framework;
using OpenSmtp.Mail;
using System;
using System.IO;
using System.Text;

	[TestFixture]
	public class SmtpTest 
	{
			private int 			smtpPort;
			private string 			smtpHost;
			private int				recieveTimeout;
			private int				sendTimeout;

			private string 			subject;
			private string 			body;
			private StringBuilder	htmlBody;

			private Smtp 			smtp;
			private MailMessage 	msg;
			private EmailAddress	senderAddress;
			private EmailAddress 	replyToAddress;
			private EmailAddress 	recipientAddress;
			private EmailAddress 	ccAddress;
			private EmailAddress	bccAddress;
			
			[SetUp]
			protected void Init() 
			{
				subject 		= "Test Subject. International chars: ËÇÅÃÄÄÅÂÀèéêëìíîïñaÿc + 功能超强得不得了";
				body 			= "Hello Jane.\r\n This is the body of the mail message. \r\nInternational chars: ËÇÅÃÄÄÅÂÀèéêë\r\nìíîïñaÿc";		
				
				htmlBody = new StringBuilder();
				htmlBody.Append("<HTML><HEAD></HEAD><BODY bgColor=\"#00ffff\"><b>Hello Jane. This is the body of the HTML mail message. International chars: ËÇÅÃÄÄÅÂÀèéêëìíîïñaÿc</b></BODY></HTML>");
				
				senderAddress 		= new EmailAddress("sender@localhost", "John Sender");
				recipientAddress 	= new EmailAddress("administrator@localhost", "Jane Doe");
				
				replyToAddress 		= new EmailAddress("replyTo@localhost", "ReplyTo Name");
				ccAddress 		= new EmailAddress("ccAddress@localhost", "CC Name");
				bccAddress		= new EmailAddress("bccAddress@localhost", "BCC Name");
				
				msg 			= new MailMessage("test@fakedomain.net", "test@fakedomain.net");

				smtpHost 		= "localhost";
				smtpPort 		= 25;
				
				smtp			= new Smtp();
				smtp.Host 		= smtpHost;
				smtp.Port 		= smtpPort; 

				// Add Smtp event listener
				SmtpEventListener listener = new SmtpEventListener(smtp);
			}
			
			[TearDown]
			protected void Destroy()
			{
				smtp = null;
				
			}
			
			// ---------------------------------------------------------------			
			[Test]
			public void TestSmtpHost()
			{
				smtp.Host = smtpHost;
				Assertion.AssertEquals(smtpHost, smtp.Host);
			}

			[Test]
			public void TestSmtpPort()
			{
				smtp.Port = smtpPort;
				Assertion.AssertEquals(smtpPort, smtp.Port);
			}
			
			[Test]
			public void TestSendTimeout()
			{
				smtp.SendTimeout = sendTimeout;
				Assertion.AssertEquals(sendTimeout, smtp.SendTimeout);
			}

			[Test]
			public void TestRecieveTimeout()
			{
				smtp.RecieveTimeout = recieveTimeout;
				Assertion.AssertEquals(recieveTimeout, smtp.RecieveTimeout);
			}
			
			[Test]
			public void TestSend()
			{
				try
				{
					Console.WriteLine("\r\n ----- Smtp Test Below -----");
					
					msg.Subject = subject;
					msg.Body = body;
					msg.AddImage(@"..\lib\test attachments\test.jpg","testimage");
					msg.AddImage(@"..\lib\test attachments\test2.jpg","testimage2");
      					msg.HtmlBody="<body><table><tr><td><b>Here is an embedded IMAGE:<img src=\"cid:testimage\"></td></tr>\r\n<tr><td>Here's another: <img src=\"cid:testimage2\"></td></tr></table></body>";

					
					msg.AddRecipient(ccAddress, AddressType.Cc);
					msg.AddRecipient(bccAddress, AddressType.Bcc);
					
					msg.AddAttachment(@"..\lib\test attachments\test.jpg");
					msg.AddAttachment(new Attachment(new FileStream(@"..\lib\test attachments\test.htm", FileMode.Open, FileAccess.Read), "test.htm"));
					
					msg.AddCustomHeader("X-FakeTestHeader", "Fake Value");
					msg.AddCustomHeader("X-AnotherFakeTestHeader", "Fake Value");
					msg.Notification = true;
					msg.Charset = "ISO-8859-1";
					msg.Priority = MailPriority.Low;
					

					smtp.Username = "testuser";
					smtp.Password = "testuser";
					

					for(int i = 0; i<1; i++)
					{
						smtp.SendMail(msg);
					}
					
				
				}
				catch(SmtpException se)
				{
					Assertion.Fail("TestSend() threw a SmtpException: " + se.Message);
				}
				catch(System.Exception e)
				{
					Assertion.Fail("TestSend() threw a System.Exception: " + e.Message + "; Target: " + e.TargetSite);
				}
			}
			

		[Test]
    		public void TestSmtpEvents()
    		{
				// Create a class that listens to the smtp events.
				//SmtpEventListener listener = new SmtpEventListener(smtp);
				//smtp.SendMail(msg);
    		}

		[Test]
		public void TestReset()
		{
			try
			{
				smtp.Reset();
			}
			catch(System.Exception e)
			{
				Assertion.Fail("TestReset() threw an System.Exception: " + e.Message);
			}
    		}
    		
    	
    	}


	class SmtpEventListener 
	{

		private Smtp smtp;

		public SmtpEventListener(Smtp smtp) 
		{
			this.smtp = smtp;
			// Add "ListChanged" to the Changed event on "List".
			this.smtp.Connected 				+=	new EventHandler(SmtpConnected);
			this.smtp.Authenticated				+=	new EventHandler(SmtpAuthenticated);
			this.smtp.StartedMessageTransfer	+= 	new EventHandler(SmtpStartedTransfer);
			this.smtp.EndedMessageTransfer		+=	new EventHandler(SmtpEndedTransfer);
			this.smtp.Disconnected 				+= 	new EventHandler(SmtpDisconnected);
		}	

		private void SmtpConnected(object sender, EventArgs e) 
		{
			Console.WriteLine("!!! Smtp.Connected event has fired !!!");
		}

		private void SmtpAuthenticated(object sender, EventArgs e) 
		{
			Console.WriteLine("!!! Smtp.Authenticated event has fired !!!");
		}

		private void SmtpStartedTransfer(object sender, EventArgs e) 
		{
			Console.WriteLine("!!! Smtp.StartedMessageTransfer event has fired !!!");
		}

		private void SmtpEndedTransfer(object sender, EventArgs e) 
		{
			Console.WriteLine("!!! Smtp.EndedMessageTransfer event has fired !!!");
		}

		private void SmtpDisconnected(object sender, EventArgs e) 
		{
			Console.WriteLine("!!! Smtp.Disconnected event has fired !!!");
		}

		public void Detach() 
		{
			// Detach the event and delete the list
			this.smtp.Connected 				-= new EventHandler(SmtpConnected);
			this.smtp.Authenticated 			-= new EventHandler(SmtpAuthenticated);
			this.smtp.StartedMessageTransfer	-= new EventHandler(SmtpStartedTransfer);
			this.smtp.EndedMessageTransfer		-= new EventHandler(SmtpEndedTransfer);
			this.smtp.Disconnected 				-= new EventHandler(SmtpDisconnected);
			this.smtp = null;
		}
	}
		
}