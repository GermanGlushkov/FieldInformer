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
using System.Collections;
using System.IO;
using System.Text;

	[TestFixture]
	public class MailMessageTest 
	{
		private MailMessage  msg;
		private EmailAddress senderEmail;
		private EmailAddress recipientEmail;
		private EmailAddress ccEmail;
		private string sender;
		private string recipient;
		private string cc;
		private string senderName;
		private string recipientName;
		private string ccName;
		private string subject;
		private string body;
		private string htmlBody;
		private string charset;


		[SetUp]
		protected void Init() 
		{
			sender 			= 	"from@fakedomain.net";
			recipient		= 	"to@fakedomain.net";
			cc				=	"cc@fakedomain.net";
			senderName 		= 	"FromName";
			recipientName	= 	"ToName";
			ccName			=	"ccName";
			subject			= 	"Mail Message Test\r\n";
			body			=	"Hello from MailMessageTest";
			htmlBody 		= 	"<HTML><HEAD></HEAD><BODY bgColor=\"#00ffff\"><b>Hello Jane. This is the body of the HTML mail message.</b></BODY></HTML>";
			charset			= 	"us-ascii";
			
			senderEmail 	= new EmailAddress(sender, senderName);
			recipientEmail 	= new EmailAddress(recipient, recipientName);
			ccEmail			= new EmailAddress(cc, ccName);

			msg 			= new MailMessage(senderEmail, recipientEmail);
			msg.AddRecipient("secondaddress@fakedomain.net", AddressType.To);
			msg.AddRecipient("thirdaddress@fakedomain.net", AddressType.To);
			msg.Subject 	= subject;
			msg.Body 		= body;
			msg.Charset		= charset;
			msg.Priority 	= MailPriority.High;

			msg.HtmlBody = htmlBody.ToString();
			msg.AddRecipient(ccEmail, AddressType.To);
			msg.AddRecipient(ccEmail, AddressType.Cc);
			msg.AddCustomHeader("X-Something", "Value");
			msg.AddCustomHeader("X-SomethingElse", "Value");
			msg.AddAttachment(@"..\lib\test attachments\test.jpg");
			msg.AddAttachment(@"..\lib\test attachments\test.htm");
			Attachment att = new Attachment(@"..\lib\test attachments\test.zip");
			msg.AddAttachment(att);

			
			msg.Notification = true;
		}
		
		[TearDown]
		protected void Destroy()
		{}
		

		[Test]
		public void TestFrom()
		{
			Assertion.AssertEquals(sender, msg.From.Address);
			Assertion.AssertEquals(senderName, msg.From.Name);
		}
	
		[Test]
		public void TestTo()
		{
			Assertion.Assert(msg.To.Contains(recipientEmail));
			Assertion.Assert(msg.To.Contains(ccEmail));
		}
		
		[Test]
		public void TestCC()
		{
			Assertion.Assert(msg.CC.Contains(ccEmail));
		}

		[Test]
		public void TestSubject()
		{
			Assertion.AssertEquals(subject, msg.Subject);
		}

		[Test]
		public void TestBody()
		{
			Assertion.AssertEquals(body, msg.Body);
		}
	
		[Test]
		public void TestCharset()
		{
			Assertion.AssertEquals(charset, msg.Charset);
		}

		[Test]
		public void TestNotification()
		{
			Assertion.Assert(msg.Notification);
		}

		[Test]
		public void TestPriority()
		{
			Assertion.Assert(msg.Priority == MailPriority.High);
		}
		
		[Test]
		public void TestHeaders()
		{
			string custom1 		= "customHeader";
			string custom2 		= "X-customeHeader2";
			string headerValue 	= "custom value";
			
			MailHeader testHeader = new MailHeader(custom1, headerValue);
			
			msg.AddCustomHeader(testHeader);
			Assertion.Assert(msg.CustomHeaders.Contains(testHeader));
			
			msg.AddCustomHeader(custom2, headerValue);
//			Assertion.Assert(msg.CustomHeaders.Contains(testHeader));
			
		}
				
		[Test]
		public void TestBodyFromFile()
		{
			string s = "Body of text file.\r\n";

			string filePath = @"..\lib\test attachments\testBody.txt";
			byte[] msgBody = Encoding.Default.GetBytes(s.ToString().ToCharArray());
			FileStream fout = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
			fout.Write(msgBody, 0, (int)msgBody.Length);
			fout.Close();

			msg.GetBodyFromFile(filePath);
			
			Assertion.AssertEquals(s.ToString(), msg.Body);
			
			FileInfo fi = new FileInfo(filePath);
			if (fi.Exists) { fi.Delete(); }
		}
		
		[Test]
		public void TestHtmlBodyFromFile()
		{
			string s = "<HTML><HEAD></HEAD><BODY bgColor=\"#00ffff\"><b>Hello Jane. This is the body of the HTML mail message.</b></BODY></HTML>";
			string filePath = @"..\lib\test attachments\testBody.html";
			byte[] msgBody = Encoding.ASCII.GetBytes(s.ToCharArray());
			FileStream fout = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
			fout.Write(msgBody, 0, (int)msgBody.Length);
			fout.Close();

			msg.GetHtmlBodyFromFile(filePath);
			
			Assertion.AssertEquals(s, msg.HtmlBody);
			
			FileInfo fi = new FileInfo(filePath);
			if (fi.Exists) { fi.Delete(); }
		}

		[Test]
		public void TestEmbededImages()
		{

			// This test must be visually checked, meaning you have to check the output yourself
			msg.AddImage(@"..\lib\test attachments\test.jpg","testimage");
			msg.AddImage(@"..\lib\test attachments\test2.jpg","testimage2");
			msg.HtmlBody="<body><table><tr><td><b>Here is an embedded IMAGE:<img src=\"cid:testimage\"></td></tr>\r\n<tr><td>Here's another: <img src=\"cid:testimage2\"></td></tr></table></body>";
			
			msg.Save(@"..\logs\testSaveEmbeded.eml");
		}


		[Test]
		public void TestReset()
		{
			try
			{
				msg.Reset();
			}
			catch(System.Exception e)
			{
				Assertion.Fail("TestReset() threw an System.Exception: " + e.Message);
			}
		}

		[Test]
		public void TestSave()
		{
			try
			{
				msg.Save(@"..\logs\testSave.eml");
			}
			catch(System.Exception e)
			{
				Assertion.Fail("TestSave() threw an System.Exception: " + e.Message);
			}
		}

		// test added to fix bug in contructors
		[Test]
		public void TestAllConstructors()
		{
			try
			{
				// test MailMessage(string, string) constructor
				MailMessage msg		= new MailMessage("administrator@fakedomain.net", "administrator@fakedomain.net");

				// test MailMessage(EamilAddress, EmailAddress) constructor
				EmailAddress from 	= new EmailAddress("sender@fakedomain.net", "Fake Sender");
				EmailAddress to		= new EmailAddress("recipient@fakedomain.net", "Fake Recipient");
				msg					= new MailMessage(from, to);
			}
			catch(SmtpException)
			{ Assertion.Fail("TestAllConstructors threw SmtpException"); }
		}

		[Test]
		public void TestCopy()
		{
				// test MailMessage(string, string) constructor
				MailMessage msg		= new MailMessage("administrator@fakedomain.net", "administrator@fakedomain.net");

				// test MailMessage(EamilAddress, EmailAddress) constructor
				EmailAddress from 	= new EmailAddress("sender@fakedomain.net", "Fake Sender");
				EmailAddress to		= new EmailAddress("recipient@fakedomain.net", "Fake Recipient");
				msg					= new MailMessage(from, to);
				msg.Body			= "test body for clone test";
				
				
				MailMessage msg2 = msg.Copy();
				
				Assertion.AssertEquals(msg.To, msg2.To);
				Assertion.AssertEquals(msg.From, msg2.From);
				Assertion.AssertEquals(msg.Body, msg2.Body);
		}
		
		[Test]
		public void TestAttachmentSort()
		{
				// test MailMessage(string, string) constructor
				MailMessage msg		= new MailMessage("administrator@fakedomain.net", "administrator@fakedomain.net");

				// test MailMessage(EamilAddress, EmailAddress) constructor
				EmailAddress from 	= new EmailAddress("sender@fakedomain.net", "Fake Sender");
				EmailAddress to		= new EmailAddress("recipient@fakedomain.net", "Fake Recipient");


				Attachment att = new Attachment(@"..\lib\test attachments\test.htm");
				msg.AddAttachment(att);

				Attachment att2 = new Attachment(@"..\lib\test attachments\test.gar");
				msg.AddAttachment(att2);
				
				Attachment att3 = new Attachment(@"..\lib\test attachments\test.zip");
				msg.AddAttachment(att3);
			
				Attachment att4 = new Attachment(@"..\lib\test attachments\test.longextension");
				msg.AddAttachment(att4);

				ArrayList attachments = msg.Attachments;
				attachments.Sort();
				
				Console.WriteLine("\r\n ----- MailMessage.Attachments after sorting -----");
				foreach (Attachment attachment in attachments)
				{
					Console.WriteLine(attachment.Name);
				}
		}
		
		
	}		
}