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
	public class AttachmentTest 
	{

		[SetUp]
		protected void Init() 
		{}

		[TearDown]
		protected void Destroy()
		{}


		[Test]
		public void TestAttachmentBin()
		{
			Attachment attachment = new Attachment(@"..\lib\test attachments\test.jpg");

			FileInfo originalFile = new FileInfo(attachment.FilePath);		
			FileInfo encodedFile = new FileInfo(attachment.EncodedFilePath);		

			if (!encodedFile.Exists)
			{
				Assertion.Fail("Attachment temp file does not exist.");
			}
			else
			{
				// make sure the encoded file is the right size (new file size == original file size/3*4 for base64)
				// we assume a delta of 8 bytes due to possible extra padding '=' characters (see rfc 2045)
				float delta 	= (float)8;
				float oldSize 	= (float)encodedFile.Length;
				float newSize	= (float)originalFile.Length/3*4;

				Assertion.AssertEquals(newSize , oldSize, delta);
				Assertion.AssertEquals((int)originalFile.Length, attachment.Size);
			}
		}

		[Test]
		public void TestAttachmentStream()
		{
			string filePath = @"..\lib\test attachments\test.jpg";
			Attachment attachment = new Attachment(new FileStream(filePath, FileMode.Open, FileAccess.Read), "Test Name");

			FileInfo originalFile = new FileInfo(filePath);		
			FileInfo encodedFile = new FileInfo(attachment.EncodedFilePath);		

			if (!encodedFile.Exists)
			{
				Assertion.Fail("Attachment temp file does not exist.");
			}
			else
			{
				// make sure the encoded file is the right size (new file size == original file size/3*4 for base64)
				// we assume a delta of 8 bytes due to possible extra padding '=' characters (see rfc 2045)
				float delta 	= (float)8;
				float oldSize 	= (float)encodedFile.Length;
				float newSize	= (float)originalFile.Length/3*4;

				Assertion.AssertEquals(newSize , oldSize, delta);
				Assertion.AssertEquals((int)originalFile.Length, attachment.Size);
			}
		}


		// below needs refactoring - ian
		[Test]
		public void TestAttachmentLongExtension()
		{
			Attachment attachment = new Attachment(@"..\lib\test attachments\test.longextension");

			FileInfo originalFile = new FileInfo(attachment.FilePath);		
			FileInfo encodedFile = new FileInfo(attachment.EncodedFilePath);		

			if (!encodedFile.Exists)
			{
				Assertion.Fail("Attachment temp file does not exist.");
			}
			else			{
				// make sure the encoded file is the right size (new file size == original file size/3*4 for base64)
				// we assume a delta of 8 bytes due to possible extra padding '=' characters (http://www.faqs.org/rfcs/rfc2045.html)
				float delta 	= (float)8;
				float oldSize 	= (float)encodedFile.Length;
				float newSize	= (float)originalFile.Length/3*4;

				Assertion.AssertEquals(newSize , oldSize, delta);
				Assertion.AssertEquals((int)originalFile.Length, attachment.Size);
			}
		}
		
		[Test]
		public void TestBadAttachment()
		{
			try
			{
				Attachment attachment = new Attachment(@"..\nonexistant.file");
				Assertion.Fail("TestBadAttachment() failed to throw System.Exception on non existant file");
			}
			catch(System.Exception)
			{
				// nothing	
			}
		}

	}

}