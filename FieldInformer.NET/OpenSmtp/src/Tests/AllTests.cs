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


	using System;
	using OpenSmtp.Mail;
	using NUnit.Core;
	using NUnit.Framework;

	/// <summary>TestSuite that runs all the sample Tests.</summary>
	public class AllTests {
		[Suite]
		public static TestSuite Suite {
			get 
			{
				TestSuite suite= new TestSuite("All Tests");
				suite.Add(new EmailAddressTest());
				suite.Add(new MailMessageTest());
				suite.Add(new AttachmentTest());
				suite.Add(new MailEncoderTest());
				suite.Add(new SmtpTest());
				return suite;
			}
		}
	}
}