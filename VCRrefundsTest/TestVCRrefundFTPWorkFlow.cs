using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using log4net;
using System.IO;
using AlaskaAir.Configuration;


namespace VCRrefunds
{

	[TestFixture]
	public class TestVCRrefundFTPWorkFlow
	{
		[Test]
		public void testExecute()
		{


			RefundAppVariables InitVariables = new RefundAppVariables();
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.GroupName = "ImageRes";
			InitVariables.QueueName = "ETTS";
			InitVariables.UserPassword = "IcebUg77";
			InitVariables.UserID = "11362";
			InitVariables.ResultPath = System.AppDomain.CurrentDomain.BaseDirectory;
			InitVariables.logger = log;
			InitVariables.ImageFTPAccount = "image";
			InitVariables.ImageFTPPassword = "imagepw";
			InitVariables.HostAddress = "159.49.233.51";
			InitVariables.MvsDestFile = "'PROG.RFRECREF.IMAGE.REFUNDS'";
			InitVariables.ErrorSMTPServer = "outbound.alaskaair.com";
			InitVariables.ErrorAddressesFrom = "Alejandro.camargo@alaskaair.com";
			InitVariables.ErrorAddressesTo = "Alejandro.camargo@alaskaair.com";
			InitVariables.ErrorSubject = "TEST";

			//make the files
			DateTime yesterday = DateTime.Now.AddDays(-1);
			string sErrorFileName = InitVariables.ResultPath + "\\" + "MPERR" + yesterday.ToString("yyMMdd") + ".TXT";
			FileInfo fi = new FileInfo(sErrorFileName);
			if(!fi.Exists)
			{
				//create a file
				StreamWriter SW;
				SW = File.CreateText(sErrorFileName);
				SW.WriteLine("Just an email test");
				SW.WriteLine("This is second line");
				SW.Close();
			}

			string sVCRFileToday = "RF" + DateTime.Now.ToString("yyMMdd") + ".VCR";
			FileInfo fiRF = new FileInfo(sVCRFileToday);
			if(!fiRF.Exists)
			{
				//create a file
				StreamWriter SW;
				SW = File.CreateText(sVCRFileToday);
				SW.WriteLine("Just a test");
				SW.WriteLine("This is second line");
				SW.Close();
			}

			VCRrefundFTPWorkFlow test = new VCRrefundFTPWorkFlow(InitVariables);
			Assert.IsTrue(test.Execute());

			string sVCRFileSent = "RF" + DateTime.Now.ToString("yyMMdd") + ".CMP";

			FileInfo fresult = new FileInfo(sVCRFileSent);

			Assert.IsTrue(fresult.Exists);


		}

	}
}
