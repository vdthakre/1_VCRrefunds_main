using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using log4net;
using ASHosts.Interop;


namespace VCRrefunds
{

	public class VCRrefundQueueManagerBridge :  VCRrefundQueueManager
	{


		public VCRrefundQueueManagerBridge( RefundAppVariables variables): base( variables)
		{
			_AppVariables = variables;			
		}
		
		public ASHosts.Interop.ASHostSession HostConnection
		{
			get { return _HostConnection; }
			set { _HostConnection = value; }
		}
	

	}

	[TestFixture]
	public class TestVCRrefundQueueManager
	{

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void testQueueContructorNoLog()
		{
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			InitVariables.GroupName = "ImageRes";
			InitVariables.QueueName = "ETTS";
			InitVariables.UserPassword = "R0b0tic";
			InitVariables.UserID = "6007";

			//It should throw an exception
			VCRrefundQueueManager testQueue = new VCRrefundQueueManager(InitVariables);


		}

		[Test]
		public void testQueueSabreConnection()
		{
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			//Get the loging going
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLog");


			InitVariables.GroupName = "ImageRes";
			InitVariables.QueueName = "ETTS";
			InitVariables.UserPassword = "R0b0tic";
			InitVariables.UserID = "6007";
			InitVariables.logger = log;
			InitVariables.SabreSysPath = System.Windows.Forms.Application.StartupPath + "\\" + InitVariables.SabreFileName;

			VCRrefundQueueManager testQueue = new VCRrefundQueueManager(InitVariables);

			Assert.IsTrue(testQueue.StablishConnection());

		}

		[Test]
		public void testQueueSabreCountAndGet()
		{
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			ASHostSession session = new ASHostSession();

			//session.RecordScript("Where.test");
			//session.PlayBackScript("Where.test");
			session.PlayBackScript("OriginalVCRrefTest.test");
			//rc = session.SessionInit(1794, "ASPROD", "", "");
			//rc = session.SignIn(0, "11362", "IcebUg77", 0);
			//session.TransactionLogFileName = "AsHost.log";
			//session.SendReceive(1, 30000, "QC/ETTS", out sabreResult);

			log = LogManager.GetLogger("VCRrefundsLog");


			InitVariables.GroupName = "ImageRes";
			InitVariables.QueueName = "ETTS";
			InitVariables.UserPassword = "IcebUg77";
			InitVariables.UserID = "11362";
			InitVariables.logger = log;
			InitVariables.SabreSysPath = System.Windows.Forms.Application.StartupPath + "\\" + InitVariables.SabreFileName;

			VCRrefundQueueManagerBridge testQueue = new VCRrefundQueueManagerBridge(InitVariables);

			testQueue.SetHostSession(session);

			Assert.IsTrue(testQueue.StablishConnection());

			Assert.AreEqual(testQueue.GetTotalRecords(), 285);

			
			//Open queue and get the next item
			RefundItem item = testQueue.OpenRefundQueue();

			Assert.AreEqual(item.QueueLines.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);
			Assert.AreEqual(item.QueueLines[0], "GPAWJVASXXXXXXX1165             21181801581      ");
			Assert.AreEqual(item.QueueLines[1], "TESTER/KAY                    CHAD TESTER                   ");
			Assert.AreEqual(item.QueueLines[2], "5555                          ");
			Assert.AreEqual(item.QueueLines[3], "                              LOS CABOS                 ME  ");
			Assert.AreEqual(item.QueueLines[4], "23400                F0          0");
			Assert.AreEqual(item.QueueLines[5], "                              ");
			Assert.AreEqual(item.QueueLines[5], "                              ");
			Assert.AreEqual(item.QueueLines[6], "                                                            ");
			Assert.AreEqual(item.QueueLines[7], "                  11362   TEST");
			Assert.AreEqual(item.QueueLines[8], "                                             ");
			Assert.AreEqual(item.QueueLines[9], "                                       ");
			Assert.AreEqual(item.QueueLines[10], "                                                ");
			Assert.AreEqual(item.QueueLines[11], "                                                ");
			Assert.AreEqual(item.QueueLines[12], "                                                ");
			Assert.AreEqual(item.QueueLines[13], "                                    ");
			Assert.AreEqual(item.QueueLines[14], "           ");
			Assert.AreEqual(item.QueueLines[15], "                                                ");
			Assert.AreEqual(item.QueueLines[16], "                                                ");
			Assert.AreEqual(item.QueueLines[17], "                                                ");
			Assert.AreEqual(item.QueueLines[18], "                                    ");
			Assert.AreEqual(item.QueueLines[19], "                                   ");
			Assert.AreEqual(item.QueueLines[20], "                                                  ");
			Assert.AreEqual(item.QueueLines[21], "             ");
			Assert.AreEqual(item.QueueLines[22], "N");
			Assert.AreEqual(item.QueueLines[23], "1000000409  INVALID CERTIFICATE CODE.                       ");

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GPAWJV");
			Assert.AreEqual(item.CertificateNumber.Trim(), "1000000409");
			Assert.AreEqual(item.RedepositErrormessage.Trim(), "INVALID CERTIFICATE CODE.");
			
			
			item = testQueue.RemoveAndGetNextItem();

			Assert.AreEqual(item.QueueLines.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);
			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");
			Assert.AreEqual(item.CertificateNumber.Trim(), "");
			Assert.AreEqual(item.RedepositErrormessage.Trim(), "");
			Assert.AreEqual(item.FOPType.Trim(), "AS");
			Assert.AreEqual(item.CCNumber.Trim(), "XXXXXXX1165");


		}

		[Test]
		public void testQueueManagerWholeTestfile()
		{
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			ASHostSession session = new ASHostSession();

			session.PlayBackScript("OriginalVCRrefTest.test");
			log = LogManager.GetLogger("VCRrefundsLog");

			InitVariables.GroupName = "ImageRes";
			InitVariables.QueueName = "ETTS";
			InitVariables.UserPassword = "IcebUg77";
			InitVariables.UserID = "11362";
			InitVariables.logger = log;
			InitVariables.SabreSysPath = System.Windows.Forms.Application.StartupPath + "\\" + InitVariables.SabreFileName;

			VCRrefundQueueManagerBridge testQueue = new VCRrefundQueueManagerBridge(InitVariables);

			testQueue.SetHostSession(session);

			Assert.IsTrue(testQueue.StablishConnection());

			Assert.AreEqual(testQueue.GetTotalRecords(), 285);


			//Open queue and get the next item
			RefundItem item = testQueue.OpenRefundQueue();
			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GPAWJV");
			Assert.AreEqual(item.CertificateNumber.Trim(), "1000000409");
			Assert.AreEqual(item.RedepositErrormessage.Trim(), "INVALID CERTIFICATE CODE.");


			item = testQueue.RemoveAndGetNextItem();

			Assert.AreEqual(item.QueueLines.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);
			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");
			Assert.AreEqual(item.CertificateNumber.Trim(), "");
			Assert.AreEqual(item.RedepositErrormessage.Trim(), "");
			Assert.AreEqual(item.FOPType.Trim(), "AS");
			Assert.AreEqual(item.CCNumber.Trim(), "XXXXXXX1165");

			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OYLOFN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EHXDAL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OYLOFN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EHXDAL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OYLOFN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PNTYBF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PNTYBF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PNTYBF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EKHDKP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GHQFXU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DFFYSH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FRQKXT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FQGCMR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HQKUVO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HQKUVO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HLWASP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JTRDTZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IRHBQQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LTQNAJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LTQNAJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LTAJWD");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DIQSMK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MQIHYY");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IGKQEG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IGKQEG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MQIHYY");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BJWTRK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MTZSHO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JBRGVQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GOEXFO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IYSNKI");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BSMPCK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IYTHMX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DLAZPP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BSMPTO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FYHQFM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GOAHWE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IYVCAJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BCLSGD");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IYSOTH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FYMMDQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IYSNQA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IYUBZB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IBTSHR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KGAZKB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CQSZBS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JIXHTM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DOZNLA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DZIDHX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NGCJVO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NGCJVO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JOAEBB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JOAEBB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PXIELP");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NTIGQM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NIJFMZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NIJFMZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MTZMNE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IXIKTR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HVCFOG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HVCYYR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HVCZQA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PTQUWC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BAYLRU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HVCXRW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MCNZVA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CENMXO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IXILGM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PTRRMO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BAXKFX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BWUJJM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KMCXOA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KMNSQT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KMNSQT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BZSOCO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "ICWAHN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PCVGIM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BSDNYB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IJYBLC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MZSTOU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IOBCCH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IOBCCH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IOBCCH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EMMEAF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EMMEAF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MZNAMG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MZNAMG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NWJXTB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NYFJXM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GYIFWH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DMQYYC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DJGOYM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HCXKKL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HHJFTX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GBYRXU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "POVUVT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NXZYXB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NWJXTB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NYFJXM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GYIFWH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DMQYYC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DJGOYM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HCXKKL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HHJFTX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GBYRXU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "POVUVT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NXZYXB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IZQVQB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IZQVQB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KSWVIU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KSWVIU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "INMVNT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "INMVNT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LTRIAM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LTRIAM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IPLVRM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IPLVRM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GUXXPJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "ILTTFE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MKPMDG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GUXXPJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "ILTTFE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LEQFRK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LFUAZA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LEQFRK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IJYDIM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LBPTUE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IIVNBL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LBDNBX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IMOBBN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IHNTRH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NZOAZA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LFFBJQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CHNDEZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LGQHCA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LFUAZA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IJYDIM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IIVNBL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LBPTUE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LBDNBX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LYFUIE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IMOBBN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IHNTRH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NZOAZA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LFFBJQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LZOAUJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LGQHCA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LYFUIE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LZOAUJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CHNDEZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CIQPAX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CIQPAX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LZIDWE");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KCKSQJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EPSEWR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IECWPC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NYCSRN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JEESPT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GQVLDB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OUQEUS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BZDBIV");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PUHYSW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IMOXZH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NVOMPA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NZOLUC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GBTWNX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NJKKFL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EKVSEB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NKRWLA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "EQPMXD");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DQGYAQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NPCPKT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DQHNIK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DQHNIK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DQHNIK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FKOLPA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KZPJNU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BQKAKB");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "KAZBAX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PZHJFW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FLFQPK");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BRYXHU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LIPHMS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LEXFYA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GOZNVF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GOZNVF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LKOHKT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LKOHKT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BUGUKV");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BUGUKV");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GOOHIT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GOOHIT");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CQFYFQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CQFYFQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MTKLJW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MTKLJW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CFNTCR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGGHON");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGGHON");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OQLXSQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HIVFDR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HIVFDR");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGPUOC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGPUOC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HHKQKV");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HIVLIL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HIVLIL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGZAQG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGEZBI");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "HHKGRS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGOJMO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MGOJMO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LPPCLH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MVWFGM");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JGGPHD");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JGGPHD");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "PPSCLJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "BTQTOU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LHQEDX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MQRVDG");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IRDKAY");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FSRXCA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "NAVAVV");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MQQOUZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "IRDNXZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FDJUVW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MXXCMN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MXXCMN");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKUOTA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKUOTA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MIAJRF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CJPOLX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CMQQBW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKGQUH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MILSBZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GYBSJJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GZYISQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LRMBRS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MXRAMS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CLVQMF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LRTTJU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MVRVUW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MIVXVF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JKZMWJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CHBDQI");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MUKOSX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CJPOLX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MIAJRF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CMQQBW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GZYISQ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "GYBSJJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MILSBZ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CKGQUH");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LRTTJU");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "LRMBRS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MXRAMS");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MVRVUW");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CLVQMF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MIVXVF");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JKZMWJ");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MUKOSX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "CHBDQI");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "MLPGRL");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "FCNXSX");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OICLOA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "OICLOA");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DBGPDI");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DBGPDI");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DCULCC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "DCULCC");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JTLXVO");
			item = testQueue.RemoveAndGetNextItem();
			Assert.AreEqual(item.Recloc.Trim(), "JTLXVO");

			item = testQueue.RemoveAndGetNextItem();
			Assert.IsNull(item);
			

		}

	}
}
