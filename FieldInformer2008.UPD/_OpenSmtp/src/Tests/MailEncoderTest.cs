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
using System.Drawing;
using System.Text;

	[TestFixture]
	public class MailEncoderTest 
	{

		[SetUp]
		protected void Init() 
		{}
		
		[TearDown]
		protected void Destroy()
		{}


		//WARNING: !!! "..\lib\test attachments\test.jpg" Must exist for this test to pass properly !!!
		[Test]
		public void TestBase64File()
		{
			string imagePath = @"..\lib\test attachments\test.jpg";
			string encodedImagePath = @"..\lib\test attachments\base64_EncodedImage.txt";
			string returnedImagePath = @"..\lib\test attachments\base64_returned.jpg";
			
			Bitmap image = new Bitmap(imagePath);
			MailEncoder.ConvertToBase64(imagePath, encodedImagePath);

			MailEncoder.ConvertFromBase64(encodedImagePath, returnedImagePath);
			Bitmap returnedImage = new Bitmap(returnedImagePath);

			Assertion.AssertEquals("Returned image from Base64 test has incorrect height.", image.Height, returnedImage.Height);
			Assertion.AssertEquals("Returned image from Base64 test has incorrect width.", image.Width, returnedImage.Width);

			image.Dispose();
			returnedImage.Dispose();
			GC.Collect();
		}

		[Test]
		public void TestQuotedPrintable()
		{
			StringBuilder s = new StringBuilder();
			s.Append("space before crlf: \r\n"); 		
			s.Append("tab before crlf:	\r\n");
			s.Append("no spaces before this crlf:\r\n");
			s.Append("above 126:ËÇÅÃ\r\n");
			s.Append("equal sign:=\r\n");
			s.Append("null, form feed, backspace (before 32):\0\f\b\r\n");
			s.Append("over 76 chars: 12345678901234567890123456789012345678901234567890123456789012345678901234567890");

			Console.WriteLine("\r\n----- QP Encoded ------\r\n" + MailEncoder.ConvertToQP(s.ToString(), "ISO-8859-1"));
		}
	}
}