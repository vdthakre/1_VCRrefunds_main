using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using log4net;
using AlaskaAir.Configuration;
using System.IO;


namespace VCRrefunds
{
	public class RedepositErrorHandlerBridge : RedepositErrorHandler
	{
		public RedepositErrorHandlerBridge(RefundAppVariables initVars)
			: base(initVars)
		{
		}

	}

	[TestFixture]
	public class TestRedepositErrorHandler
	{

		[Test]
		public void testEmailFile()
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
			InitVariables.ErrorAddressesCc = "";
			InitVariables.ErrorSubject = "TEST";
			InitVariables.ErrorAddressesBcc = "Alejandro.camargo@alaskaair.com;image.res@AlaskaAir.com";
			//InitVariables.ErrorAddressesBcc = "ImageRes@alaskaair.com";

			RedepositErrorHandlerBridge test = new RedepositErrorHandlerBridge(InitVariables);
			DateTime yesterday = DateTime.Now.AddDays(-1);
			//Check to see if we have an error file to send
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

			Assert.IsTrue(test.SendErrorReport());

			string sSentFileName = InitVariables.ResultPath + "\\" + "MPERR" + yesterday.ToString("yyMMdd") + ".SNT";

			FileInfo fsent = new FileInfo(sSentFileName);

			Assert.IsTrue(!fi.Exists);

		}
	}
}
