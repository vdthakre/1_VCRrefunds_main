using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;

namespace VCRrefunds
{
	[TestFixture]
	public class TestFTP
	{

		[Test]
		public void testUploadFile()
		{

			FTPclient test = new FTPclient("159.49.233.51", "image", "imagepw");

			test.Upload("testftp.txt", "testUpload.txt");
		}

		[Test]
		public void testApendFile()
		{

			FTPclient test = new FTPclient("159.49.233.51", "image", "imagepw");

			test.Append("testftp.txt", "testUpload.txt");
		}

	}
}
