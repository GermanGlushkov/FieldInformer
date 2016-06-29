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

	[TestFixture]
	public class EmailAddressTest 
	{

		private string address  	= "to@fakedomain.net";
		private string name			= "testname";

		
		[SetUp]
		protected void Init() 
		{
			SmtpConfig.VerifyAddresses= true;
		}
		
		[TearDown]
		protected void Destroy()
		{
			SmtpConfig.VerifyAddresses= false;
		}

		[Test]
		public void TestEmailAddress()
		{
			EmailAddress email = new EmailAddress(address, name);
			Assertion.AssertEquals(name, email.Name);
			Assertion.AssertEquals(address, email.Address);
		}

		[Test]
		public void TestFunkyEmailAddress()
		{
			string fakieaddress = "\"fakie name\" <Fake.Address@mail.com>"; 
			EmailAddress email = new EmailAddress(fakieaddress);

			Console.WriteLine("\r\n ----- EmailAddress Properties after parsing -----");
			Console.WriteLine("Fake Address: " + fakieaddress);
			Console.WriteLine("Address: " + email.Address);
			Console.WriteLine("Name: " + email.Name);
			Console.WriteLine("IsValid: " + email.IsValid);
			Console.WriteLine("Domain: " + email.Domain);
			Console.WriteLine("LocalPart: " + email.LocalPart);
			Console.WriteLine("MailBox: " + email.Mailbox);
			Console.WriteLine("QuotedString: " + email.QuotedString);
		}

		[Test]
		public void TestBadEmailAddress()
		{
			if (SmtpConfig.VerifyAddresses) {
				try
				{
					EmailAddress email = new EmailAddress("badaddresscom", name);
					Assertion.Fail("invalid email address was verified.");
				}
				catch(MalformedAddressException)
				{ /*pass*/ }
			}
		}

		[Test]
		public void TestLog()
		{
			try
			{
				Log l = new Log();
				l.logToTextFile(null, null, null);
				Assertion.Fail("Log file allowed null value.");
			}
			catch(System.Exception)
			{ /*pass*/ }
		}

	}
	
}