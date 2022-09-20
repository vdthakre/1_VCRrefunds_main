using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using log4net;
using ASHosts.Interop;
using System.IO;

namespace VCRrefunds
{

	public class AccountFileManagerBridge  : AccountFileManager
	{
		public AccountFileManagerBridge(RefundAppVariables InitVariables): base(InitVariables)
		{

		}

		public string resultFileName
		{
			get { return _resultFileName; }
			set { _resultFileName = value; }
		}


		new public string GetResultFileName(DateTime thisTime)
		{
			return base.GetResultFileName(thisTime);
		}

		new public void WriteToFile(string message)
		{
			base.WriteToFile(message);
			return;
		}

		new public bool RenameFile(string FilepathOld, string FilepathNew)
		{
			return base.RenameFile(FilepathOld, FilepathNew);
		}

	}

	[TestFixture]
	public class TestAccountFileManager
	{

		[Test]
		public void testFTPFile()
		{
			RefundAppVariables InitVariables = new RefundAppVariables();
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.GroupName = "ImageRes";
			InitVariables.QueueName = "ETTS";
			InitVariables.UserPassword = "dt041993";
			InitVariables.UserID = "18615";
			InitVariables.ResultPath = System.AppDomain.CurrentDomain.BaseDirectory;
			InitVariables.logger = log;
			InitVariables.ImageFTPAccount = "accelya-dev";
			InitVariables.ImageFTPPassword = "Accelyadev@AS027";
			InitVariables.HostAddress = "transfer.qa.alaskaair.com";
			InitVariables.MvsDestFile = "'PROG.RFRECREF.IMAGE.REFUNDS'"; 
			

			AccountFileManagerBridge test = new AccountFileManagerBridge(InitVariables);
			string sVCRFileToday = "RF" + DateTime.Now.ToString("yyMMdd") + ".VCR";

			FileInfo fi = new FileInfo(sVCRFileToday);

			if(!fi.Exists)
			{
				//create a file
				StreamWriter SW;
				SW = File.CreateText(sVCRFileToday);
				SW.WriteLine("Just a test");
				SW.WriteLine("This is second line");
				SW.Close();
			}

			Assert.IsTrue(test.SendResultFile());

			string sVCRFileSent = "RF" + DateTime.Now.ToString("yyMMdd") + ".CMP";

			FileInfo fresult = new FileInfo(sVCRFileSent);

			Assert.IsTrue(fresult.Exists);

		}

		[Test]
		public void testRenameFile()
		{

			RefundAppVariables InitVariables = new RefundAppVariables();

			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.logger = log;
			AccountFileManagerBridge test = new AccountFileManagerBridge(InitVariables);


			//create a file
			StreamWriter SW;
			SW = File.CreateText(InitVariables.ResultPath  + "TestFile.txt");
			SW.WriteLine("Just a test");
			SW.WriteLine("This is second line");
			SW.Close();

			test.RenameFile(InitVariables.ResultPath + "TestFile.txt", InitVariables.ResultPath + "RenamedTestFile.txt");

			Assert.IsFalse(File.Exists(InitVariables.ResultPath + "TestFile.txt"));
			Assert.IsTrue(File.Exists(InitVariables.ResultPath + "RenamedTestFile.txt"));

			FileInfo fi = new FileInfo(InitVariables.ResultPath + "RenamedTestFile.txt");

			if(fi.Exists)
			{
				fi.Delete();
			}

		}

		[Test]
		public void testWriteFile()
		{
			RefundAppVariables InitVariables = new RefundAppVariables();
			InitVariables.ResultPath = System.AppDomain.CurrentDomain.BaseDirectory;
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.logger = log;

			AccountFileManagerBridge test = new AccountFileManagerBridge(InitVariables);

			//lets start with sunday
			DateTime testDatime = new DateTime(2008, 3, 23);
			Assert.AreEqual("RF080323.VCR", test.GetResultFileName(testDatime));

			test.resultFileName = "RF080323.VCR";

			long ticks = DateTime.Now.Ticks;

			test.WriteToFile("" + this.GetHashCode() + ticks);

			StreamReader SR;
			string S;
			string last = "";
			SR = File.OpenText(InitVariables.ResultPath + "\\" + "RF080323.VCR");
			S = SR.ReadLine();
			while(S != null)
			{
				Console.WriteLine(S);
				last = S;
				S = SR.ReadLine();
			}

			SR.Close();

			Assert.AreEqual("" + this.GetHashCode() + ticks, last);

		}

		[Test]
		public void testFileFormat()
		{
			RefundAppVariables InitVariables = new RefundAppVariables();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.logger = log;
			AccountFileManagerBridge test = new AccountFileManagerBridge(InitVariables);

			//lets start with sunday
			DateTime testDatime = new DateTime(2008, 3, 23);
			Assert.AreEqual("RF080324.VCR", test.GetResultFileName(testDatime));

			//Saturday 
			testDatime = new DateTime(2008, 3, 22);
			Assert.AreEqual("RF080324.VCR", test.GetResultFileName(testDatime));

			//friday after 12
			testDatime = new DateTime(2008, 3, 21, 12, 00, 1);
			Assert.AreEqual("RF080324.VCR", test.GetResultFileName(testDatime));

			//friday before 12
			testDatime = new DateTime(2008, 3, 21, 11, 59, 59);
			Assert.AreEqual("RF080321.VCR", test.GetResultFileName(testDatime));

			//Other date before 12 
			testDatime = new DateTime(2008, 3, 20, 11, 59, 59);
			Assert.AreEqual("RF080320.VCR", test.GetResultFileName(testDatime));

			testDatime = new DateTime(2008, 3, 20, 10, 59, 59);
			Assert.AreEqual("RF080320.VCR", test.GetResultFileName(testDatime));

			testDatime = new DateTime(2008, 3, 20, 1, 59, 59);
			Assert.AreEqual("RF080320.VCR", test.GetResultFileName(testDatime));

			testDatime = new DateTime(2008, 3, 20, 0, 0, 59);
			Assert.AreEqual("RF080320.VCR", test.GetResultFileName(testDatime));

			//Another date after 12
			testDatime = new DateTime(2008, 3, 20, 12, 0, 1);
			Assert.AreEqual("RF080321.VCR", test.GetResultFileName(testDatime));

			testDatime = new DateTime(2008, 3, 20, 12, 1, 1);
			Assert.AreEqual("RF080321.VCR", test.GetResultFileName(testDatime));

			testDatime = new DateTime(2008, 3, 20, 14, 0, 1);
			Assert.AreEqual("RF080321.VCR", test.GetResultFileName(testDatime));

			testDatime = new DateTime(2008, 3, 20, 23, 59, 1);
			Assert.AreEqual("RF080321.VCR", test.GetResultFileName(testDatime));


		}

	}
}
