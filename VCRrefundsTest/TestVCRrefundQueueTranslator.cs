using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using log4net;
using ASHosts.Interop;


namespace VCRrefunds
{

	public class VCRrefundQueueTranslatorBridge : VCRrefundQueueTranslator
	{

		public VCRrefundQueueTranslatorBridge(RefundAppVariables Variables):base(Variables)
		{

		}

        public void AddQueueLine(int lineNumber, string content, List<string> list)
        {
            base.AddQueueLine(lineNumber, content, list);
        }
		public new List<string> GetVCRrefundLines(string SabreResult)
		{
			return base.GetVCRrefundLines(SabreResult);
		}
		new public int GetLineNumber(string line)
		{
			return base.GetLineNumber(line);
		}
		new public string AppendStringKeepLength(string lineToAppend, string DestinationLine)
		{
			return base.AppendStringKeepLength(lineToAppend, DestinationLine);
		}
		new public void InitLinesList(ref List<string> lines)
		{
			base.InitLinesList(ref lines);
		}
		new public void PopulateRefundProperties(RefundItem item)
		{
			base.PopulateRefundProperties(item);
		}
		new public bool IsLastLine(string line)
		{
			return base.IsLastLine(line);
		}

	}

	[TestFixture]
	public class TestVCRrefundQueueTranslator
	{
        
		[Test]
        public void testAddQueueLine()
		{
			
			
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();						

            VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);
             

			RefundItem item = new RefundItem();

            test.AddQueueLine(0, "test", null);

            List<string> testList = new List<string>();
            test.AddQueueLine(0, "test", testList);

            testList.Add("Test");

            test.AddQueueLine(1, "1", testList);

            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test");
            testList.Add("Test"); 
            testList.Add("Test");

            test.AddQueueLine(0, "0", testList);
            test.AddQueueLine(1, "1", testList);
            test.AddQueueLine(2, "2", testList);
            test.AddQueueLine(3, "3", testList);
            test.AddQueueLine(4, "4", testList);
            test.AddQueueLine(5, "5", testList);
            test.AddQueueLine(6, "6", testList);
            test.AddQueueLine(7, "7", testList);
            test.AddQueueLine(8, "8", testList);
            test.AddQueueLine(9, "9", testList);
            test.AddQueueLine(10, "10", testList);
            test.AddQueueLine(11, "11", testList);
            test.AddQueueLine(12, "12", testList);

            Assert.IsTrue(testList[0] == "0");
            Assert.IsTrue(testList[1] == "1");
            Assert.IsTrue(testList[2] == "2");
            Assert.IsTrue(testList[3] == "3");
            Assert.IsTrue(testList[4] == "4");
            Assert.IsTrue(testList[5] == "5");
            Assert.IsTrue(testList[6] == "6");
            Assert.IsTrue(testList[7] == "7");
            Assert.IsTrue(testList[8] == "8");
            Assert.IsTrue(testList[9] == "9");
            Assert.IsTrue(testList[10] == "10");
            Assert.IsTrue(testList[11] == "11");
            Assert.IsTrue(testList[12] == "12");


		}
        

		[Test]
		public void testTranslateFromSabreQueue()
		{
			
			
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslator test = new VCRrefundQueueTranslator(InitVariables);
			log4net.Config.XmlConfigurator.Configure();
			ASHostSession session = new ASHostSession();
			long rc;
			string sabreResult;



			//session.RecordScript("Where.test");
			//session.PlayBackScript("Where.test");
			session.PlayBackScript("OriginalVCRrefTest.test");
			rc = session.SessionInit(1794, "ASPROD", "", "");
			rc = session.SignIn(0, "11362", "IcebUg77", 0);
			//session.TransactionLogFileName = "AsHost.log";
			session.SendReceive(1, 30000, "QC/ETTS", out sabreResult);
			session.SendReceive(1, 30000, "Q/ETTS", out sabreResult);

			RefundItem item = test.TranslateFromQueue(sabreResult);

			//Check that the lines are OK
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

			String toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GPAWJVASXXXXXXX1165             02721181801581   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");


		}

		[Test]
		public void testTransOtherFromSabreQueue()
		{
			
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslator test = new VCRrefundQueueTranslator(InitVariables);
			log4net.Config.XmlConfigurator.Configure();
			ASHostSession session = new ASHostSession();
			long rc;
			string sabreResult;

			session.PlayBackScript("OtherTesttest.test");
			rc = session.SessionInit(1794, "ASPROD", "", "");
			rc = session.SignIn(0, "11362", "IcebUg77", 0);
			session.SendReceive(1, 30000, "QC/ETTS", out sabreResult);
			session.SendReceive(1, 30000, "Q/ETTS", out sabreResult);

			RefundItem item = test.TranslateFromQueue(sabreResult);

			//Check that the lines are OK
			Assert.AreEqual(item.QueueLines.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GBTWNX");

			String toAccountFormat = "";
			Assert.IsTrue( test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GBTWNX                          02721184800101234TESTER/MOORE                  MOORE TESTER                  111 SOUTH AUBURN WAY                                        AUBURN                     WA 98002                 1          0                              RR0272118480040027211848001094484      ZP 350      ZP 350      ZP 350      ZP 350      XF 300      XF 450      XF 450      XF 450      US 4856     AY 1000                                                                 83895      ZP 350      ZP 350      XF 300      XF 450      AY 500      US21540     US1705                                                                                                                                        YDEBRA.MOORE@HORIZONAIR.COM                        02000        N");


		}

		[Test]
		public void testLastline()
		{
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);

			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1657/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1729/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1729/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1729/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1729/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1729/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1730/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1730/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1730/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1730/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1730/05MAR"));
			Assert.IsTrue(test.IsLastLine("PHX.SEA8GII 1730/05MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1752/05MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1753/05MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1754/05MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1801/05MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1833/05MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 2214/05MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 2214/05MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1025/06MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1120/06MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1123/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1234/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1234/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1235/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1236/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1236/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1236/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1340/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1408/06MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1409/06MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1505/06MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1505/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1533/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1534/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1540/06MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1716/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1907/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1916/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1916/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1916/06MAR"));
			Assert.IsTrue(test.IsLastLine("BOI.SEA8GII 1926/06MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 0944/07MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1037/07MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1059/07MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1059/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1103/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1103/07MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1105/07MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1141/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1202/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1202/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1251/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1440/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1441/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1441/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1441/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1441/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1441/07MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1524/07MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1531/07MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1535/07MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1535/07MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 2246/07MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1140/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1146/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1159/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1220/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1300/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1308/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1308/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1308/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1329/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1329/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1334/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1334/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1335/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("EWR.SEA8GII 1336/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1350/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1350/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1752/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1752/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1800/10MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1800/10MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 0946/11MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 0946/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1026/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1026/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1027/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1028/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1028/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1039/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1047/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1056/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1110/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1110/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1110/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1111/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1111/11MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1143/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1241/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1610/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1657/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1658/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1658/11MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1659/11MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1037/12MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1219/12MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1311/12MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1500/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1756/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1758/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1758/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1758/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1807/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1809/12MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1854/12MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1854/12MAR"));
			Assert.IsTrue(test.IsLastLine("DPT.SEA8GII 1854/12MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1015/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1017/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1018/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1018/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1019/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1025/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1027/13MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1435/13MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1437/13MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1441/13MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1441/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1511/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1511/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1512/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1512/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1514/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1514/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1515/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1515/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1515/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1515/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1805/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1806/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1806/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1807/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1808/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1808/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1816/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1816/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1817/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1817/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1817/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1818/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1820/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1820/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1820/13MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1820/13MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1530/14MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1556/14MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1600/14MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1600/14MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1330/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1333/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1345/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1526/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1526/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1526/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1526/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1526/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1527/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1527/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1631/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1631/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1631/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1632/17MAR"));
			Assert.IsTrue(test.IsLastLine("NET.SEA5DWB 1741/17MAR"));
			Assert.IsTrue(test.IsLastLine("SEA.SEA8GII 1835/17MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1104/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1104/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1128/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1128/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1131/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1131/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1132/18MAR"));
			Assert.IsTrue(test.IsLastLine("QXQ.SEA8GII 1132/18MAR"));

		}
        
        [Test]
		public void testTransAllFromSabreQueue()
		{
			
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslator test = new VCRrefundQueueTranslator(InitVariables);			
			log4net.Config.XmlConfigurator.Configure();
			ASHostSession session = new ASHostSession();
			long rc;
			string sabreResult;

			session.PlayBackScript("OriginalVCRrefTest.test");
			rc = session.SessionInit(1794, "ASPROD", "", "");
			rc = session.SignIn(0, "11362", "IcebUg77", 0);
			session.SendReceive(1, 30000, "QC/ETTS", out sabreResult);
			session.SendReceive(1, 30000, "Q/ETTS", out sabreResult);

			RefundItem item = test.TranslateFromQueue(sabreResult);

			//Check that the lines are OK
			Assert.AreEqual(item.QueueLines.Count, 25);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GPAWJV");

			string toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GPAWJVASXXXXXXX1165             02721181801581   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");
			Assert.AreEqual(item.QueueDate, "1657/05MAR");			

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EHXWLEASXXXXXXX1165             02721181915821   HURT/BEN                                                                                                                                                                   F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OYLOFN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OYLOFNASXXXXXXX1165             02721181891401   HURT/BEN                                                                                                                                                                   F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXDAL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EHXDALASXXXXXXX1165             02721181902621   WHITE/BETTY ESCORT                                                                                                                                                         F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EHXWLEASXXXXXXX1165             02721181924141   WHITE/BETTYESCORT                                                                                                                                                          F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OYLOFN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OYLOFNASXXXXXXX1165             02721181902651   WHITE/BETTY ESCORT                                                                                                                                                         F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXDAL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EHXDALASXXXXXXX1165             02721181902631   JOHNSON/LINDA ATTENDANT                                                                                                                                                    F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EHXWLE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EHXWLEASXXXXXXX1165             02721181924151   JOHNSON/LINDAATTENDANT                                                                                                                                                     F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OYLOFN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OYLOFNASXXXXXXX1165             02721181902661   JOHNSON/LINDA ATTENDANT                                                                                                                                                    F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PNTYBF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PNTYBFASXXXXXXX1165             02721181915811   HURT/BENMR                                                                                                                                                                 F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PNTYBF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PNTYBFASXXXXXXX1165             02721181924181   WHITE/BETTEESCORT                                                                                                                                                          F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PNTYBF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PNTYBFASXXXXXXX1165             02721181924191   JOHNSON/LINDA ATTENDANT                                                                                                                                                    F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EKHDKP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EKHDKPASXXXXXXX1165             02721181922341   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GHQFXU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GHQFXUASXXXXXXX1165             02721181925151   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DFFYSH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DFFYSHASXXXXXXX1165             02721181932301   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FRQKXT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FRQKXT                          02721181944021   TEST/CAROL                    CAROL TEST                    1234 MAIN STS                                               KENT                       WA 98032                 1          0                              RR0272118194844027211819440228651      ZP 350      XF 450      US 2149     AY 250                                                                                                                                          13395      ZP 350      XF 450      US11005     AY 250                                                                                                                                                                            YCAROL.MOHN@ALASKAAIR.COM                          0116400      N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FQGCMR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FQGCMRASXXXXXXX1165             02721181956951   TESTER/YOUNG                  YOUNG TESTER                  PO BOX  68900                                               SEATTLE                   WA  98198                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HQKUVO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HQKUVOASXXXXXXX1165             02721181943441   TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HQKUVO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HQKUVOASXXXXXXX1165             02721181943451   TESTACCT/SUSIE                RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HLWASP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HLWASPASXXXXXXX1165             02721182111691   TESTER/KAY                    TEST                          PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JTRDTZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JTRDTZASXXXXXXX1165             02721182157911   TEST/KAY                      YOUNG TESTER                  20313 28TH AVE. S.                                          SEATTLE                   WA  98198                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IRHBQQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IRHBQQASXXXXXXX1165             02721182142841   TESTER/KAY                    TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LTQNAJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LTQNAJASXXXXXXX1165             02721182192971   DANDE/EDINA                   DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LTQNAJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LTQNAJASXXXXXXX1165             02721182192981   DANDE/SAM                     DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JZJYIPASXXXXXXX1165             02721182192991   DANDE/ZSOLT                   DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JZJYIPASXXXXXXX1165             02721182193011   MICKEY/MOUSE                  DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JZJYIPASXXXXXXX1165             02721182193021   MICKEY/DISNEY                 DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JZJYIP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JZJYIPASXXXXXXX1165             02721182193031   GOOFY/ME                      DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LTAJWD");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LTAJWDASXXXXXXX1165             02721182219181   TESTER/DANDE                  DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DIQSMK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DIQSMKASXXXXXXX1165             02721182223001   DANDE/EDINA                   DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MQIHYY");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MQIHYYASXXXXXXX1165             02721181232021   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IGKQEG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IGKQEGASXXXXXXX1165             02721182223221   TEST/KAY                      TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IGKQEG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IGKQEGASXXXXXXX1165             02721182223201   TEST/PAT                      TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MQIHYY");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MQIHYYASXXXXXXX1165             02721181232021   TESTER/KAY                    CHAD TESTER                   5555                                                        LOS CABOS                 ME  23400                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BJWTRK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BJWTRKASXXXXXXX1165             027211822423012  DANDE/EDINA                   ANDREA TESTER                 789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MTZSHO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MTZSHOASXXXXXXX1165             02721181335011   TESTER/KAY                    TEST                          PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JBRGVQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JBRGVQASXXXXXXX2839             027211822934912  TESTER/RACHEL                 RACHEL TESTER                 123 TEST                                                    SEATTLE                   WA  98198     US          1          0                              RA0272118231413027211822934922046      ZP 350      ZP 350      XF 450      XF 450      US 1654     AY 500                                                                                                                  26790      ZP 350      ZP 350      XF 450      XF 450      US12010     AY 500                                                                                                                  XXXXXXX2839             1211064954NRACHEL.ANDERSON@ALASKAAIR.COM                     01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GOEXFO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GOEXFOASXXXXXXX1165             027211819559412  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IYSNKI");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IYSNKIASXXXXXXX1165             027211819784612  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BSMPCK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BSMPCKASXXXXXXX1165             027211819730712  TESTACCOUNT/RICHARD           RICHARD TESTACCOUNT           PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IYTHMX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IYTHMXASXXXXXXX1165             027211819730312  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DLAZPP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DLAZPPASXXXXXXX1165             027211819809012  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BSMPTO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BSMPTOASXXXXXXX1165             027211819806112  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FYHQFM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FYHQFMASXXXXXXX1165             027211819474812  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GOAHWE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GOAHWEASXXXXXXX1165             027211819788112  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IYVCAJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IYVCAJASXXXXXXX1165             027211819470512  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BCLSGD");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BCLSGDASXXXXXXX1165             027211819784912  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IYSOTH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IYSOTHASXXXXXXX1165             027211819807412  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FYMMDQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FYMMDQASXXXXXXX1165             027211819659012  TESTACCOUNT/RICHARD           RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IYSNQA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IYSNQAASXXXXXXX1165             027211819729912  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IYUBZB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IYUBZBASXXXXXXX1165             027211819785012  TESTACCT/RICHARD              RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IBTSHR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IBTSHRASXXXXXXX1165             02721181983531   TESTACCT/SUSIE                RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KGAZKB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KGAZKBASXXXXXXX1165             02721181992301   TESTACCT/SUSIE                RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CQSZBS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CQSZBSASXXXXXXX1165             02721181988831   TESTACCT/SUZIE                RICHARD TESTACCT              PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JIXHTM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JIXHTMASXXXXXXX1165             02721182297721   TESTACCT/GARY                 GARY TESTACCT                 PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DOZNLA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DOZNLAASXXXXXXX1165             02721182463831   DANDE/TEST                    IMAGE TEST                    NONE                                                        SEATTLE                   WA  98168                 0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DZIDHX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DZIDHXASXXXXXXX1165             027211824974112  TESTER/EDINA                  JACQUELYN TESTER              PO BOX 11000                                                EASTSOUND                 WA  98245                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NGCJVO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NGCJVOASXXXXXXX1165             027211824917612  DANDE/TEST                    JACQUELYN TESTER              PO BOX 11000                                                EASTSOUND                 WA  98245                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NGCJVO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NGCJVOASXXXXXXX1165             027211824917712  DANDE/IMAGE                   JACQUELYN TESTER              PO BOX 11000                                                EASTSOUND                 WA  98245                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JOAEBB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JOAEBBASXXXXXXX1165             027211824946812  TESTER/KAY                    TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JOAEBB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JOAEBBASXXXXXXX1165             027211824946912  TESTER/PAT                    TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PXIELP");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PXIELPASXXXXXXX1165             02721182503501   IMAGE/TEST                    IMAGE TEST                    NONE                                                        SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NTIGQM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NTIGQMASXXXXXXX1165             02721182524271   TESTER/DANDE                  DANDE TESTER                  PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NIJFMZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NIJFMZASXXXXXXX1165             02721182527441   TESTER/KAY                    TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NIJFMZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NIJFMZASXXXXXXX1165             02721182544711   TESTER/PAT                    TESTER WALTER                 PO BOX 68900 23240            ALASKAAIR COM                 MEXICO                    ME  23400                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MTZMNE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MTZMNEASXXXXXXX1165             02721182547351   TESTER/KAY                    TEST                          PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IXIKTR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IXIKTRASXXXXXXX1165             027211794090612  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HVCFOG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HVCFOGASXXXXXXX1165             027211793720012  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HVCYYR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HVCYYRASXXXXXXX1165             027211793719112  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HVCZQA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HVCZQAASXXXXXXX1165             027211793992912  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PTQUWC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PTQUWCASXXXXXXX1165             027211794091812  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BAYLRU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BAYLRUASXXXXXXX1165             027211794091912  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HVCXRW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HVCXRWASXXXXXXX1165             027211793719912  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MCNZVA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MCNZVAASXXXXXXX1165             027211794092212  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CENMXO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CENMXOASXXXXXXX1165             027211794093212  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IXILGM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IXILGMASXXXXXXX1165             027211794092412  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PTRRMO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PTRRMOASXXXXXXX1165             027211794090812  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BAXKFX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BAXKFXASXXXXXXX1165             027211794091012  TESTACCT/SUSIE                SUSIE TESTACCT                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BWUJJM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BWUJJMVIXXXXXXXXXXXX1033        02721182626761   SAVIANO TWO/STACEY K                                                                                                                                                       A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KMCXOA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KMCXOAASXXXXXXX2839             027211826427613  TESTER/SHERMAN                TESTER                        PO BOX 68900                                                SEATTLE                   WA  98168     US          1          0                              RA0272118264311027211826427666171      ZP 350      ZP 350      XF 450      XF 450      US 2369     AY 500                                                                                                                  103728     ZP 350      ZP 350      XF 450      XF 300      AY 500      US21540     US1872                                                                                                      XXXXXXX2839             1209076198NCINDY.SHERMAN@ALASKAAIR.COM                       01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KMNSQT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KMNSQT                          027211826427713  TESTER/MOTHER                 TESTER                        PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118264908027211826427766171      ZP 350      ZP 350      XF 450      XF 450      US 2369     AY 500                                                                                                                  50804      ZP 350      ZP 000      XF 450      AY 500      US2770      US11796                                                                                                                                                   YCINDY.SHERMAN@ALASKAAIR.COM                       02000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KMNSQT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KMNSQT                          027211826427813  TESTER/FATHER                 TESTER                        PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118264347027211826427866171      ZP 350      ZP 350      XF 450      XF 450      US 2369     AY 500                                                                                                                  50804      ZP 350      ZP 000      XF 450      AY 500      US2770      US11796                                                                                                                                                   YCINDY.SHERMAN@ALASKAAIR.COM                       02000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BZSOCO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BZSOCOASXXXXXXX1165             027211828152312  MILLER/JULIAN                 SARA STARBUCK                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "ICWAHN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "ICWAHNASXXXXXXX1165             02721183570791   IMAGE/TEST                    IMAGE TEST                    NONE                                                        SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PCVGIM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PCVGIMASXXXXXXX1165             02721183625671   TESTER/KAYLARIE               CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BSDNYB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BSDNYBASXXXXXXX1165             02721183605861   JOHN/DOE                      MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IJYBLC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IJYBLCASXXXXXXX1165             02721183633521   TESTER/KAY                    CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MZSTOU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MZSTOUASXXXXXXX1165             02721183665691   IMAGE/TEST                    IAMGE TEST                    123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IOBCCH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IOBCCHASXXXXXXX1165             02721183688231   SMITH/JOHN                    IAMGE TEST                    123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IOBCCH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IOBCCHASXXXXXXX1165             02721183688251   SMITH/JOAN                    IAMGE TEST                    123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IOBCCH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IOBCCHASXXXXXXX1165             02721183688261   SMITH/DANIEL                  IAMGE TEST                    123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EMMEAF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EMMEAFASXXXXXXX1165             02721183709061   MOZART/WOLFGANG               MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EMMEAF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EMMEAFASXXXXXXX1165             02721183709071   MOZART/LEOPOLD                MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MZNAMG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MZNAMGCA                        02721176335211   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATLE                    WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MZNAMG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MZNAMGCA                        02721176335221   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATLE                    WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NWJXTB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NWJXTBCA                        02721176270461   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK RD                                           SEATTLE                   WA  98101                 0000050569 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NYFJXM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NYFJXMCA                        02721176300551   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GYIFWH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GYIFWHCA                        02721176309061   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DMQYYC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DMQYYCCA                        02721176327091   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DJGOYM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DJGOYMCA                        02721176294571   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HCXKKL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HCXKKLCA                        02721176268231   BUSHMAN/JASON                 SHELBY                        920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HHJFTX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HHJFTXCA                        02721176303181   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GBYRXU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GBYRXUCA                        02721176325241   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOOUTH ROCK RD                                          SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "POVUVT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "POVUVTCA                        02721176311071   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NXZYXB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NXZYXBCA                        02721176292481   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NWJXTB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NWJXTBCA                        02721176270471   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK RD                                           SEATTLE                   WA  98101                 0000050569 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NYFJXM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NYFJXMCA                        02721176300561   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GYIFWH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GYIFWHCA                        02721176309071   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DMQYYC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DMQYYCCA                        02721176327101   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DJGOYM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DJGOYMCA                        02721176294581   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HCXKKL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HCXKKLCA                        02721176268241   BUSHMAN/SHELBY                SHELBY                        920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HHJFTX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HHJFTXCA                        02721176303191   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GBYRXU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GBYRXUCA                        02721176325251   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOOUTH ROCK RD                                          SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "POVUVT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "POVUVTCA                        02721176311081   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NXZYXB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NXZYXBCA                        02721176292491   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IZQVQB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IZQVQBASXXXXXXX1165             02721183721691   BACH/JOHANN                   MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IZQVQB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IZQVQBASXXXXXXX1165             02721183721701   BACH/CHRISTOPH                MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KSWVIU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KSWVIUASXXXXXXX1165             02721183869061   SCHUBERT/FRANZ                MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KSWVIU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KSWVIUASXXXXXXX1165             02721183869071   SCHUBERT/HANS                 MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "INMVNT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "INMVNTASXXXXXXX1165             02721183898431   CHARPENTIER/MARC              MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "INMVNT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "INMVNTASXXXXXXX1165             02721183898441   CHARPENTIER/ANTONY            MIKE TEST                     123 MAIN                                                    AUBURN                    WA  98092                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LTRIAM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LTRIAMASXXXXXXX1165             02721184181481   DANDE/EDINA                   DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LTRIAM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LTRIAMASXXXXXXX1165             02721184181491   DANDE/TEST                    DANDE TESTER                  789 WASHINGTON TERRITORY                                    SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IPLVRM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IPLVRMCA                        02721184023011   BUSHMAN/JASON                 RUBEN GONZALEZ                760 S KIHEI RD                                              KIHEI                     HI  96753                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IPLVRM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IPLVRMCA                        02721184023021   BUSHMAN/SHELBY                RUBEN GONZALEZ                760 S KIHEI RD                                              KIHEI                     HI  96753                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GUXXPJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GUXXPJCA                        02721184021541   BUSHMAN/JASON                 SERGE KAHLON                  9334 SE PENNINGTON CT                                       PORTLAND                  OR  97086                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "ILTTFE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "ILTTFECA                        02721184033801   BUSHMAN/JASON                 JASON BUSHMAN                 SHLEBY BUSHMAN                10626 SE STEELE ST            PORTLAND                  OR  97266                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MKPMDG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MKPMDGCA                        02721184035221   BUSHMAN/SHELBY                SHELBY BUSHMAN                1234 FUN ST                                                 LOS ANGELES               CA  90356                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GUXXPJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GUXXPJCA                        02721184021551   BUSHMAN/SHELBY                SERGE KAHLON                  9334 SE PENNINGTON CT                                       PORTLAND                  OR  97086                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "ILTTFE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "ILTTFECA                        02721184033811   BUSHMAN/SHELBY                JASON BUSHMAN                 SHLEBY BUSHMAN                10626 SE STEELE ST            PORTLAND                  OR  97266                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LEQFRK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LEQFRKCA                        02721184032421   BUSHMAN/JASON                 SHELBY                        3115 SO 215TH STREET                                        TACOMA                    WA  9840511223           F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LFUAZA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LFUAZACA                        02721184044661   BUSHMAN/JASON                 JASON BUSHMAN                 3355 BUSHMAN STREET                                         BUSHMAN                   MT  90210                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LEQFRK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LEQFRKCA                        02721184032431   BUSHMAN/SHELBY                SHELBY                        3115 SO 215TH STREET                                        TACOMA                    WA  9840511223           F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IJYDIM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IJYDIMCA                        02721184010341   BUSHMAN/JASON                 JOHN DOE                      666 ELM ST                                                  OF NO RETURN              CA  666666               F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LBPTUE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LBPTUECA                        02721184008691   BUSHMAN/JASON                 JASON BUSHMAN                 13520 146TH AVE NW                                          SEATTLE                   WA  98059                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IIVNBL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IIVNBLCA                        02721183993501   BUSHMAN/JASON                 JASON BUSHMAN                 SHELBY BUSHMAN                504 RIDGE                     FAIRBANKS                 AK  99712                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LBDNBX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LBDNBXCA                        02721183995121   BUSHMAN/JASON                 FREDDY KRUGER                 1600 PENNSYLVANIA AVE                                       WASHINGTON                DC  98765                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IMOBBN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IMOBBNCA                        02721184048931   BUSHMAN/JASON                 NACHO MAMA                    1 TOSTITO ROAD                                              CHEESEY                   CO  80112                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IHNTRH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IHNTRHCA                        02721183975931   BUSHMAN/JASON                 JASON BUSHMAN                 1234 ANY ST                                                 CLINTON                   WA  98236                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NZOAZA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NZOAZACA                        02721183974181   BUSHMAN/JASON                 SHELLY BUSHMAN                920 SOLTH ROCK ROAD                                         SEATTLE                   WA  98101                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LFFBJQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LFFBJQCA                        02721184046031   BUSHMAN/JASON                 JASON BUSHMAN                 1234 5TH AVENUE                                             NEW YORK                  NY  12345                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CHNDEZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CHNDEZCA                        02721183961361   BUSHMAN/JASON                 JULIANA FERREZ                9999 AVENTURINE COURT                                       LAS VEGAS                 NV  89122                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LGQHCA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LGQHCACA                        02721184047491   BUSHMAN/JASON                 JASON BUSHMAN                 SHELBY BUSHMAN                555 WIONOT WAY                DENVE                     CO  80601                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LFUAZA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LFUAZACA                        02721184044671   BUSHMAN/SHELBY                JASON BUSHMAN                 3355 BUSHMAN STREET                                         BUSHMAN                   MT  90210                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IJYDIM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IJYDIMCA                        02721184010351   BUSHMAN/SHELBY                JOHN DOE                      666 ELM ST                                                  OF NO RETURN              CA  666666               F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IIVNBL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IIVNBLCA                        02721183993511   BUSHMAN/SHELBY                JASON BUSHMAN                 SHELBY BUSHMAN                504 RIDGE                     FAIRBANKS                 AK  99712                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LBPTUE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LBPTUECA                        02721184008701   BUSHMAN/SHELBY                JASON BUSHMAN                 13520 146TH AVE NW                                          SEATTLE                   WA  98059                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LBDNBX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LBDNBXCA                        02721183995131   BUSHMAN/SHELBY                FREDDY KRUGER                 1600 PENNSYLVANIA AVE                                       WASHINGTON                DC  98765                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LYFUIE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LYFUIECA                        02721183911711   BUSHMAN/JASON                 JASON BUSHMAN                 500 W BEAL ST                                               LINCOLN                   NE  68521                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IMOBBN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IMOBBNCA                        02721184048941   BUSHMAN/SHELBY                NACHO MAMA                    1 TOSTITO ROAD                                              CHEESEY                   CO  80112                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IHNTRH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IHNTRHCA                        02721183975941   BUSHMAN/SHELBY                JASON BUSHMAN                 1234 ANY ST                                                 CLINTON                   WA  98236                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NZOAZA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NZOAZACA                        02721183974191   BUSHMAN/SHELBY                SHELLY BUSHMAN                920 SOLTH ROCK ROAD                                         SEATTLE                   WA  98101                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LFFBJQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LFFBJQCA                        02721184046041   BUSHMAN/SHELBY                JASON BUSHMAN                 1234 5TH AVENUE                                             NEW YORK                  NY  12345                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LZOAUJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LZOAUJCA                        02721183972491   BUSHMAN/JASON                 JASON BUSHMAN                 721 SCOTTSDALE                                              SEATTLE                   WA  99008                F0112233445 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LGQHCA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LGQHCACA                        02721184047501   BUSHMAN/SHELBY                JASON BUSHMAN                 SHELBY BUSHMAN                555 WIONOT WAY                DENVE                     CO  80601                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LYFUIE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LYFUIECA                        02721183911721   BUSHMAN/SHELBY                JASON BUSHMAN                 500 W BEAL ST                                               LINCOLN                   NE  68521                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LZOAUJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LZOAUJCA                        02721183972501   BUSHMAN/SHELBY                JASON BUSHMAN                 721 SCOTTSDALE                                              SEATTLE                   WA  99008                F0112233445 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CHNDEZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CHNDEZCA                        02721183961371   BUSHMAN/SHELBY                JULIANA FERREZ                9999 AVENTURINE COURT                                       LAS VEGAS                 NV  89122                F011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CIQPAX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CIQPAXCA                        02721183962941   BUSHMAN/JASON                 JASON BUSHMAN                 123 MAIN STREET                                             FAIRBANKS                 AK  99703                F0123456789 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CIQPAX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CIQPAXCA                        02721183962951   BUSHMAN/SHELBY                JASON BUSHMAN                 123 MAIN STREET                                             FAIRBANKS                 AK  99703                F0123456789 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LZIDWE");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LZIDWEASXXXXXXX1165             027211841875312  TESTER/KAY                    CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KCKSQJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KCKSQJASXXXXXXX1165             027211842046912  TESTER/KAY                    CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EPSEWR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EPSEWRASXXXXXXX1165             027211841939612  TESTEER/KAY                   CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKEUUUASXXXXXXX1165             02721184224451   TEST/KAY                      CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKEUUUASXXXXXXX1165             02721184224461   TEST/PAT                      CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKEUUUASXXXXXXX1165             02721184224471   TEST/SAOIRSE                  CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKEUUUASXXXXXXX1165             02721184224481   TESTER/BEN                    CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKEUUU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKEUUUASXXXXXXX1165             02721184224491   TESTER/JOE                    CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IECWPC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IECWPC                          027211842265812  RABLIN/RICK                   ALASKA AIRLINES               16209 144TH AVE SE                                          RENTON                     WA 98058                 1          0                              RE0272118422834027211842265816558      ZP 350      ZP 350      XF 450      XF 450      US 1242     AY 500                                                                                                                  16558      ZP 350      ZP 350      XF 450      XF 450      US11242     AY 500                                                                                                                                                    NRICK.RABLIN@ALASKAAIR.COM                         01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NYCSRN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NYCSRNASXXXXXXX1165             027211842427112  TESTER/KAY                    CHAD TESTER                   13433 1ST AVE S                                             SEATTLE                   WA  98188                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JEESPT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JEESPTASXXXXXXX1165             02721184356511   TESTER/KAY                    TEST                          PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GQVLDB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GQVLDBASXXXXXXX2839             02721184326621234TESTER/SHERMAN                SHERMAN TESTER                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OUQEUS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OUQEUSASXXXXXXX2839             02721184334451234TESTER/SHERMAN                SHERMAN TESTER                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BZDBIV");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BZDBIVASXXXXXXX2839             02721184327891   TESTER/SHERMAN                SHERMAN TESTER                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PUHYSW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PUHYSWASXXXXXXX2839             0272118424610123 TESTER/SHERMAN                SHERMAN TESTER                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IMOXZH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IMOXZH                          027211843505912  RABLIN/RICK                   ALASKA AIRLINES               16209 144TH AVE SE                                          RENTON                     WA 98058                 1          0                              RE0272118466926027211843505943148      ZP 350      ZP 350      XF 450      XF 300      US 1902     AY 500                                                                                                                  43148      ZP 350      ZP 350      XF 450      XF 300      AY 500      US21540     US1362                                                                                                                                        NRICK.RABLIN@ALASKAAIR.COM                         01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NVOMPA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NVOMPAASXXXXXXX1165             027211847086912  RABLIN/RICK                   ALASKA AIRLINES               16209 144TH AVE SE                                          RENTON                    WA  98058     US          1          0                              RA0272118469050027211847086916558      ZP 350      ZP 350      XF 450      XF 450      US 1242     AY 500                                                                                                                  20279      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      US11521     AY 750                                                                                          XXXXXXX1165             1210124660NRICK.RABLIN@ALASKAAIR.COM                         01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NZOLUC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NZOLUCASXXXXXXX1165             02721184748631   IMAGE/EDINA                   IMAGE TEST                    NONE                                                        SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GBTWNX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GBTWNX                          02721184800101234TESTER/MOORE                  MOORE TESTER                  111 SOUTH AUBURN WAY                                        AUBURN                     WA 98002                 1          0                              RR0272118480040027211848001094484      ZP 350      ZP 350      ZP 350      ZP 350      XF 300      XF 450      XF 450      XF 450      US 4856     AY 1000                                                                 83895      ZP 350      ZP 350      XF 300      XF 450      AY 500      US21540     US1705                                                                                                                                        YDEBRA.MOORE@HORIZONAIR.COM                        02000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NJKKFL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NJKKFLASXXXXXXX1165             027211848783712  SOLO/HAN                      SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EKVSEB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EKVSEBASXXXXXXX1165             027211848908212  SOLO/HAN                      SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NKRWLA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NKRWLAASXXXXXXX1165             027211848542812  SOLO/HAN                      BROOKE STEWART                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "EQPMXD");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "EQPMXDASXXXXXXX1165             027211848713312  SOLO/HAN                      BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DQGYAQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DQGYAQASXXXXXXX1165             027211848735312  SOLO/HAN                      DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NPCPKT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NPCPKTASXXXXXXX1165             027211848805012  SOLO/HAN                      GINA DASEN                    PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DQHNIK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DQHNIKASXXXXXXX1165             02721184881251   BEAR/YOGI PATIENT                                                                                                                                                           0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DQHNIK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DQHNIKASXXXXXXX1165             02721184855371   JOHNSON/RUTH ATTENDANT                                                                                                                                                      0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DQHNIK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DQHNIKASXXXXXXX1165             02721184855381   RANGER/PARK ESCORT                                                                                                                                                          0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FKOLPA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FKOLPAASXXXXXXX1165             027211851965612  DUCK/DARKWING                 SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KZPJNU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KZPJNUASXXXXXXX1165             027211851795612  DUCK/DARKWING                 DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BQKAKB");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BQKAKBASXXXXXXX1165             027211851719812  DUCK/DARKWING                 SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "KAZBAX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "KAZBAXASXXXXXXX1165             027211851904812  DARKWING/DUCK                 DARKWING DUCK                 POBOX 68900                                                 SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PZHJFW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PZHJFWASXXXXXXX1165             027211851958612  DUCK/DARKWING                 BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FLFQPK");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FLFQPKASXXXXXXX1165             027211852043912  DUCK/DARKWING                 DARKWIG DUCK                  PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BRYXHU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BRYXHUASXXXXXXX1165             027211852088812  DARKWING/DUCK                 BROOKE STEWART                PO BOX 98900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LIPHMS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LIPHMSASXXXXXXX2839             027211853160213  TESTER/SHERMAN                TESTER                        PO BOX 68900                                                SEATTLE                   WA  98168     US          1          0                              RA0272118529784027211853160266171      ZP 350      ZP 350      XF 450      XF 450      US 2369     AY 500                                                                                                                  100991     ZP 350      ZP 350      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      XF 300      AY 1000     US21540     US14209                                         XXXXXXX2839             1209136426NCINDY.SHERMAN@ALASKAAIR.COM                       01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LEXFYA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LEXFYA                          027211852963212  TESTER/MOORE                  MOORE TESTER                  PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RE0272118529791027211852963226790      ZP 350      ZP 350      XF 450      XF 450      US 2010     AY 500                                                                                                                  26790      ZP 350      ZP 350      XF 450      XF 450      US12010     AY 500                                                                                                                                                    NDEBRA.MOORE@ALASKAAIR.COM                         01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GOZNVF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GOZNVF                          027211853160313  TESTER/IMA                    TESTER                        PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118531623027211853160366171      ZP 350      ZP 350      XF 450      XF 450      US 2369     AY 500                                                                                                                  35925      ZP 350      ZP 350      XF 450      XF 300      AY 500      US2770      US1575                                                                                                                                        YCINDY.SHERMAN@ALASKAAIR.COM                       02000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GOZNVF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GOZNVF                          027211853160413  TESTER/SILLY                  TESTER                        PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118529561027211853160466171      ZP 350      ZP 350      XF 450      XF 450      US 2369     AY 500                                                                                                                  35925      ZP 350      ZP 350      XF 450      XF 300      AY 500      US2770      US1575                                                                                                                                        YCINDY.SHERMAN@ALASKAAIR.COM                       02000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LKOHKT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LKOHKTASXXXXXXX1165             027211853219612  JOB/ANITA                     AMBER DEL RIO                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LKOHKT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LKOHKTASXXXXXXX1165             027211853219712  OAKIE/KARI                    AMBER DEL RIO                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BUGUKV");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BUGUKVASXXXXXXX1165             027211852917212  JOBB/ANITA                    DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BUGUKV");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BUGUKVASXXXXXXX1165             027211852917312  OAKIE/KARIE                   DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GOOHIT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GOOHITASXXXXXXX1165             027211853156012  JOBB/ANITA                    SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  95168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GOOHIT");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GOOHITASXXXXXXX1165             027211853156112  OAKIE/KARI                    SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  95168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CQFYFQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CQFYFQASXXXXXXX1165             027211853258812  OAKIE/KARI                    BROOKE STEWART                PO BOX 98168                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CQFYFQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CQFYFQASXXXXXXX1165             027211853258912  JOB/ANITA                     BROOKE STEWART                PO BOX 98168                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MTKLJW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MTKLJWASXXXXXXX1165             027211853095912  REMENTO/ZACK                  BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MTKLJW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MTKLJWASXXXXXXX1165             027211853096012  OAKIE/CARRIE                  BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CFNTCR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CFNTCRASXXXXXXX1165             027211854444712  WORD/SADIE                    DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGGHON");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGGHONASXXXXXXX1165             027211854386812  WONKDA/WILL                   DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGGHON");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGGHONASXXXXXXX1165             027211854386912  CRANE/ICHABOD                 DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OQLXSQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OQLXSQASXXXXXXX1165             027211854038712  WORD/SADIE                    SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HIVFDR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HIVFDRASXXXXXXX1165             027211854430812  WONKA/WILLY                   SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HIVFDR");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HIVFDRASXXXXXXX1165             027211854430912  CRANE/ICHABOD                 SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGPUOC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGPUOCASXXXXXXX1165             027211854434812  WONKA/WILLY                   SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGPUOC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGPUOCASXXXXXXX1165             027211854434912  CRANE/ICHABOD                 SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HHKQKV");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HHKQKVASXXXXXXX1165             027211854140512  WORD/SADIE                    SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HIVLIL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HIVLILASXXXXXXX1165             027211854032712  WONKA/WILLY                   BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HIVLIL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HIVLILASXXXXXXX1165             027211854032812  CRANE/ICHABOD                 BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGZAQG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGZAQGASXXXXXXX1165             027211854312012  WORD/SADIE                    BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGEZBI");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGEZBIASXXXXXXX1165             027211854524712  WORD/SADIE                    AMBER DEL RIO                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "HHKGRS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "HHKGRSASXXXXXXX1165             027211853951412  WORD/SADIE                    GINA DASEN                    PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGOJMO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGOJMOASXXXXXXX1165             027211853793212  WONKA/WILLY                   AMBER DEL RIO                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MGOJMO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MGOJMOASXXXXXXX1165             027211853793312  CRANE/ICHABOB                 AMBER DEL RIO                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LPPCLH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LPPCLH                          02721185989411234TESTER/STEPHENS               AS                            PO BOX 68900                                                SEATTLE                    WA 98198                 1          0                              RE0272118598547027211859894141674      ZP 350      ZP 350      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      XF 450      US 3126     AY 1000                                                     41674      ZP 350      ZP 350      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      XF 450      US13126     AY 1000                                                                                       NSUE.STEPHENS@ALASKAAIR.COM                        01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MVWFGM");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MVWFGMASXXXXXXX2839             0272118599514124 TESTER/MOORE                  MOORE TESTER                  PO BOX 68900                                                SEATTLE                   WA  98168     US          1          0                              RA0272118600075027211859951417488      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      US 1312     AY 750                                                                                          34139      ZP 350      ZP 350      XF 450      XF 450      US12561     AY 500                                                                                                                  XXXXXXX2839             1209140907NDEBRA.MOORE@HORIZONAIR.COM                        01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JGGPHD");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JGGPHD                          0272118599516124 TESTER/ANDMOORE               MOORE TESTER                  PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118597924027211859951617488      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      US 1312     AY 750                                                                                          14697      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      US11103     AY 750                                                                                                                            NDEBRA.MOORE@HORIZONAIR.COM                        01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JGGPHD");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JGGPHD                          0272118599517124 TESTER/SOMEMOORE              MOORE TESTER                  PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118597925027211859951717488      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      US 1312     AY 750                                                                                          14697      ZP 350      ZP 350      ZP 350      XF 450      XF 450      XF 450      US11103     AY 750                                                                                                                            NDEBRA.MOORE@HORIZONAIR.COM                        01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "PPSCLJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "PPSCLJASXXXXXXX1165             02721186959561   TESTER/RICKETT                AS                            XX                                                          SEATTLE                   WA  98198                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "BTQTOU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "BTQTOUASXXXXXXX1165             02721187005771   TESTER/RICKETT                AS                            XX                                                          SEATTLE                   WA  98198                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LHQEDX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LHQEDXASXXXXXXX1165             027211870091011  TESTER/RICKETT                AS                            XX                                                          SEATTLE                   WA  98198                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MQRVDG");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MQRVDGASXXXXXXX1165             027211870523112  BRASCO/DONNIE                 BROOKE STEWART                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IRDKAY");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IRDKAYASXXXXXXX1165             027211870484812  BRASCO/DONNIE                 SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FSRXCA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FSRXCAASXXXXXXX1165             027211870318312  BRASCO/DONNY                  DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "NAVAVV");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "NAVAVVASXXXXXXX1165             027211870485012  BRASCO/DONNIE                 GINA DASEN                    PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MQQOUZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MQQOUZASXXXXXXX1165             027211870317312  BRASCO/DONNIE                 SARA STARBUCK                 PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "IRDNXZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "IRDNXZASXXXXXXX1165             027211870190112  BRASCO/DONNIE                 BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FDJUVW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FDJUVWASXXXXXXX1165             027211870522912  BRASCO/DONNIE                 SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                A0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MXXCMN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MXXCMNCA                        02721186105631   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MXXCMN");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MXXCMNCA                        02721186105641   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKUOTA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKUOTACA                        02721186108511   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKUOTA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKUOTACA                        02721186108521   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MIAJRF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MIAJRFCA                        02721186111341   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CJPOLX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CJPOLXCA                        02721186107111   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CMQQBW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CMQQBWCA                        02721186074021   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKGQUH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKGQUHCA                        02721186054531   BUSHMAN/JASON                 SHELBY BUSHMAN                920 S ROCK ROAD                                             SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MILSBZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MILSBZCA                        02721186114281   BUSHMAN/JASON                 SHELBY BUSHMAN                920 S ROCK ROAD                                             SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GYBSJJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GYBSJJCA                        02721186075461   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK RD                                           SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GZYISQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GZYISQCA                        02721186094751   BUSHMAN/JASON                 SHELBY HUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LRMBRS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LRMBRSCA                        02721186112781   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MXRAMS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MXRAMSCA                        02721186104241   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH                                                   SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CLVQMF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CLVQMFCA                        02721186065721   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LRTTJU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LRTTJUCA                        02721186080921   BUSHMAN/JASON                 SHELBY BUSMAN                 920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MVRVUW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MVRVUWCA                        02721186072601   BUSHMAN/JASON                 SHELBY BUSHAM                 920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MIVXVF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MIVXVFCA                        02721186109961   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98010                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JKZMWJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JKZMWJCA                        02721186082391   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011122334450                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CHBDQI");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CHBDQICA                        02721186055981   BUSHMAN/JASON                 SHELBY BUSHMAN                920 SBROCK ROAD                                             SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MUKOSX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MUKOSXCA                        02721186067191   BUSHMAN/JASON                 SHELBY BUSHMAN                920 ROCK ROAD                                               SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CJPOLX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CJPOLXCA                        02721186107121   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MIAJRF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MIAJRFCA                        02721186111351   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CMQQBW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CMQQBWCA                        02721186074031   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GZYISQ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GZYISQCA                        02721186094761   BUSHMAN/SHELBY                SHELBY HUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "GYBSJJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "GYBSJJCA                        02721186075471   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK RD                                           SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MILSBZ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MILSBZCA                        02721186114291   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 S ROCK ROAD                                             SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CKGQUH");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CKGQUHCA                        02721186054541   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 S ROCK ROAD                                             SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LRTTJU");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LRTTJUCA                        02721186080931   BUSHMAN/SHELBY                SHELBY BUSMAN                 920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "LRMBRS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "LRMBRSCA                        02721186112791   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MXRAMS");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MXRAMSCA                        02721186104251   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH                                                   SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MVRVUW");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MVRVUWCA                        02721186072611   BUSHMAN/SHELBY                SHELBY BUSHAM                 920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CLVQMF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CLVQMFCA                        02721186065731   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MIVXVF");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MIVXVFCA                        02721186109971   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98010                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JKZMWJ");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JKZMWJCA                        02721186082401   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SOUTH ROCK ROAD                                         SEATTLE                   WA  98101                 011122334450                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MUKOSX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MUKOSXCA                        02721186067201   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 ROCK ROAD                                               SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "CHBDQI");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "CHBDQICA                        02721186055991   BUSHMAN/SHELBY                SHELBY BUSHMAN                920 SBROCK ROAD                                             SEATTLE                   WA  98101                 011223344550                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "MLPGRL");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "MLPGRL                          02721187122411   TESTER/GOLD DEV               FRIDA TESTER                  PO BOX 68900                                                SEATTLE                    WA 98168                 1          0                              RR0272118714044027211871224127163      ZP 350      XF 450      US 2037     AY 250                                                                                                                                          18605      ZP 350      XF 450      US11395     AY 250                                                                                                                                                                            NBHANU.MULLAPUDI@ALASKAAIR.COM                     01000        N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "FCNXSX");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "FCNXSXVIXXXXXXXXXXXX2294        02721187110161   CUP/PAPER                     STACEY SAVIANO                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          N");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OICLOA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OICLOAASXXXXXXX1165             027211874386112  ZUCKO/DANNY                   SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "OICLOA");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "OICLOAASXXXXXXX1165             027211874386212  OLESON/SANDY                  SHANNA COLE                   PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DBGPDI");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DBGPDIASXXXXXXX1165             027211874428512  ZUCKO/DANNY                   DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DBGPDI");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DBGPDIASXXXXXXX1165             027211874428612  OLESON/SANDY                  DETH INC                      PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DCULCC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DCULCCASXXXXXXX1165             027211874505312  ZUCKO/DANNY                   SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  68900                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "DCULCC");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "DCULCCASXXXXXXX1165             027211874505412  OLESON/SANDY                  SHERRIE SAMPLE                PO BOX 68900                                                SEATTLE                   WA  68900                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JTLXVO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JTLXVOASXXXXXXX1165             027211874506812  ZUCKO/DANNY                   BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");

			session.SendReceive(1, 30000, "QR", out sabreResult);

			item = test.TranslateFromQueue(sabreResult);

			//Check that we got the record locator and the certificate numbers
			Assert.AreEqual(item.Recloc.Trim(), "JTLXVO");

			toAccountFormat = "";
			Assert.IsTrue(test.TranslateToAccountingFormat(item, ref toAccountFormat));
			Assert.AreEqual(toAccountFormat, "JTLXVOASXXXXXXX1165             027211874506912  OLESON/SANDY                  BRANDY POIRIER                PO BOX 68900                                                SEATTLE                   WA  98168                F0          0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          U");


		}

		[Test]
		public void testGetLines()
		{
			
			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);
			log4net.Config.XmlConfigurator.Configure();
			ASHostSession session = new ASHostSession();
			long rc;
			string sabreResult;

			

			//session.RecordScript("Where.test");
			//session.PlayBackScript("Where.test");
			session.PlayBackScript("OriginalVCRrefTest.test");
			rc = session.SessionInit(1794, "ASPROD", "", "");
			rc = session.SignIn(0, "11362", "IcebUg77", 0);
			//session.TransactionLogFileName = "AsHost.log";
			session.SendReceive(1, 30000, "QC/ETTS", out sabreResult);
			session.SendReceive(1, 30000, "Q/ETTS", out sabreResult);

			RefundItem item = new RefundItem();
			

			List<string> resultList = test.GetVCRrefundLines(sabreResult);

			//First time around is not a good one.
			Assert.AreEqual(resultList.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);


			Assert.AreEqual(resultList[0], "GPAWJVASXXXXXXX1165             21181801581      ");
			Assert.AreEqual(resultList[1], "TESTER/KAY                    CHAD TESTER                   ");
			Assert.AreEqual(resultList[2], "5555                          ");
			Assert.AreEqual(resultList[3], "                              LOS CABOS                 ME  ");
			Assert.AreEqual(resultList[4], "23400                F0          0");
			Assert.AreEqual(resultList[5], "                              ");
			Assert.AreEqual(resultList[5], "                              ");
			Assert.AreEqual(resultList[6], "                                                            ");
			Assert.AreEqual(resultList[7], "                  11362   TEST");
			Assert.AreEqual(resultList[8], "                                             ");
			Assert.AreEqual(resultList[9], "                                       ");
			Assert.AreEqual(resultList[10], "                                                ");
			Assert.AreEqual(resultList[11], "                                                ");
			Assert.AreEqual(resultList[12], "                                                ");
			Assert.AreEqual(resultList[13], "                                    ");
			Assert.AreEqual(resultList[14], "           ");
			Assert.AreEqual(resultList[15], "                                                ");
			Assert.AreEqual(resultList[16], "                                                ");
			Assert.AreEqual(resultList[17], "                                                ");
			Assert.AreEqual(resultList[18], "                                    ");
			Assert.AreEqual(resultList[19], "                                   ");
			Assert.AreEqual(resultList[20], "                                                  ");
			Assert.AreEqual(resultList[21], "             ");
			Assert.AreEqual(resultList[22], "N");
			Assert.AreEqual(resultList[23], "1000000409  INVALID CERTIFICATE CODE.                       ");


			item.QueueLines = resultList;

			test.PopulateRefundProperties(item);

			Assert.AreEqual(item.CertificateNumber.Trim(), "1000000409");
			Assert.AreEqual(item.RedepositErrormessage.Trim(), "INVALID CERTIFICATE CODE.");

		}

		[Test]
		public void testTwoCerts()
		{

			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);
			log4net.Config.XmlConfigurator.Configure();
			ASHostSession session = new ASHostSession();
			long rc;
			string sabreResult;



			//session.RecordScript("Where.test");
			//session.PlayBackScript("Where.test");
			session.PlayBackScript("2certTest.test");
			rc = session.SessionInit(1794, "ASPROD", "", "");
			rc = session.SignIn(0, "11362", "IcebUg77", 0);
			//session.TransactionLogFileName = "AsHost.log";
			session.SendReceive(1, 30000, "QC/ETTS", out sabreResult);
			session.SendReceive(1, 30000, "Q/ETTS", out sabreResult);

			RefundItem item = new RefundItem();


			List<string> resultList = test.GetVCRrefundLines(sabreResult);

			//First time around is not a good one.
			Assert.AreEqual(resultList.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);


			Assert.AreEqual(resultList[0], "GPAWJVASXXXXXXX1165             21181801581      ");
			Assert.AreEqual(resultList[1], "TESTER/KAY                    CHAD TESTER                   ");
			Assert.AreEqual(resultList[2], "5555                          ");
			Assert.AreEqual(resultList[3], "                              LOS CABOS                 ME  ");
			Assert.AreEqual(resultList[4], "23400                F0          0");
			Assert.AreEqual(resultList[5], "                              ");
			Assert.AreEqual(resultList[5], "                              ");
			Assert.AreEqual(resultList[6], "                                                            ");
			Assert.AreEqual(resultList[7], "                  11362   TEST");
			Assert.AreEqual(resultList[8], "                                             ");
			Assert.AreEqual(resultList[9], "                                       ");
			Assert.AreEqual(resultList[10], "                                                ");
			Assert.AreEqual(resultList[11], "                                                ");
			Assert.AreEqual(resultList[12], "                                                ");
			Assert.AreEqual(resultList[13], "                                    ");
			Assert.AreEqual(resultList[14], "           ");
			Assert.AreEqual(resultList[15], "                                                ");
			Assert.AreEqual(resultList[16], "                                                ");
			Assert.AreEqual(resultList[17], "                                                ");
			Assert.AreEqual(resultList[18], "                                    ");
			Assert.AreEqual(resultList[19], "                                   ");
			Assert.AreEqual(resultList[20], "                                                  ");
			Assert.AreEqual(resultList[21], "             ");
			Assert.AreEqual(resultList[22], "N");
			Assert.AreEqual(resultList[23], "1000000409INVALID CERTIFICATE CODE.                       ");
			Assert.AreEqual(resultList[23], "1000000410  INVALID CERTIFICATE CODE.                       ");


			item.QueueLines = resultList;

			test.PopulateRefundProperties(item);

			Assert.AreEqual(item.CertificateNumber.Trim(), "1000000409");
			Assert.AreEqual(item.RedepositErrormessage.Trim(), "INVALID CERTIFICATE CODE.");

			Assert.AreEqual(item.CertificateNumber2.Trim(), "1000000410");
			Assert.AreEqual(item.RedepositErrormessage2.Trim(), "INVALID CERTIFICATE CODE.");


		}

        [Test]
		public void testInitLinesList()
		{
			RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);
			List<string> lines = new List<string>();
			
			test.InitLinesList(ref lines);
			Assert.AreEqual(lines.Count, VCRrefundQueueTranslator.LAST_LINE_INDEX + 1);

			Assert.AreEqual(lines[0], "                                                 ");
			Assert.AreEqual(lines[1], "                                                            ");
			Assert.AreEqual(lines[2], "                              ");
			Assert.AreEqual(lines[3], "                                                            ");
			Assert.AreEqual(lines[4], "                                  ");
			Assert.AreEqual(lines[5], "                              ");
			Assert.AreEqual(lines[5], "                              ");
			Assert.AreEqual(lines[6], "                                                            ");
			Assert.AreEqual(lines[7], "                              ");
			Assert.AreEqual(lines[8], "                                             ");
			Assert.AreEqual(lines[9], "                                       ");
			Assert.AreEqual(lines[10], "                                                ");
			Assert.AreEqual(lines[11], "                                                ");
			Assert.AreEqual(lines[12], "                                                ");
			Assert.AreEqual(lines[13], "                                    ");
			Assert.AreEqual(lines[14], "           ");
			Assert.AreEqual(lines[15], "                                                ");
			Assert.AreEqual(lines[16], "                                                ");
			Assert.AreEqual(lines[17], "                                                ");
			Assert.AreEqual(lines[18], "                                    ");
			Assert.AreEqual(lines[19], "                                   ");
			Assert.AreEqual(lines[20], "                                                  ");
			Assert.AreEqual(lines[21], "             ");
			Assert.AreEqual(lines[22], " ");
			Assert.AreEqual(lines[23], "                                                            ");


		}

		[Test]
		public void testAppendStringKeepLength()
		{

			RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);
			Assert.AreEqual("ABCDE12345", test.AppendStringKeepLength("ABCDE" ,"1234567890"));
			Assert.AreEqual("                    ", test.AppendStringKeepLength("", "                    "));
			Assert.AreEqual("", test.AppendStringKeepLength("", ""));
			Assert.AreEqual(" ", test.AppendStringKeepLength("", " "));
			Assert.AreEqual("123456              ", test.AppendStringKeepLength("123456", "                    "));
			Assert.AreEqual("12345678911234567892", test.AppendStringKeepLength("1234567891123456789212345678931234567894", "                    "));
		}

		[Test]
		public void testcheckForLineNumber()
		{

			RefundAppVariables InitVariables = new RefundAppVariables();
			VCRrefundQueueTranslatorBridge test = new VCRrefundQueueTranslatorBridge(InitVariables);


			Assert.AreEqual(1, test.GetLineNumber("ET1*EHXWLEASXXXXXXX1165             21181915821"));
			Assert.AreEqual(2, test.GetLineNumber("ET2*HURT/BEN"));
			Assert.AreEqual(3, test.GetLineNumber("ET3*"));
			Assert.AreEqual(4, test.GetLineNumber("ET4*"));
			Assert.AreEqual(5, test.GetLineNumber("ET5*                     F0          0"));
			Assert.AreEqual(6, test.GetLineNumber("ET6*"));
			Assert.AreEqual(7, test.GetLineNumber("ET7*"));
			Assert.AreEqual(8, test.GetLineNumber("ET8*                  60024   PROD"));
			Assert.AreEqual(9, test.GetLineNumber("ET9*"));
			Assert.AreEqual(10, test.GetLineNumber("ET10*"));
			Assert.AreEqual(11, test.GetLineNumber("ET11*"));
			Assert.AreEqual(12, test.GetLineNumber("ET12*"));
			Assert.AreEqual(13, test.GetLineNumber("ET13*"));
			Assert.AreEqual(14, test.GetLineNumber("ET14*"));
			Assert.AreEqual(15, test.GetLineNumber("ET15*"));
			Assert.AreEqual(16, test.GetLineNumber("ET16*"));
			Assert.AreEqual(17, test.GetLineNumber("ET17*"));
			Assert.AreEqual(18, test.GetLineNumber("ET18*"));
			Assert.AreEqual(19, test.GetLineNumber("ET19*"));
			Assert.AreEqual(20, test.GetLineNumber("ET20*"));
			Assert.AreEqual(21, test.GetLineNumber("ET21*"));
			Assert.AreEqual(22, test.GetLineNumber("ET22*"));
			Assert.AreEqual(23, test.GetLineNumber("ET23*N"));
			Assert.AreEqual(-1, test.GetLineNumber("PHX.SEA8GII 1729/05MAR"));
			Assert.AreEqual(-1, test.GetLineNumber("</ReturnValue><ReturnValue>0</ReturnValue></ReturnValues></PlayBack>"));
			Assert.AreEqual(-1, test.GetLineNumber(""));
			Assert.AreEqual(-1, test.GetLineNumber("asdas dsfds f"));


		}

        [Test]
        public void TestOtherSet()
        {

            RefundAppVariables initVars = new RefundAppVariables();
            log4net.Config.XmlConfigurator.Configure();
            ILog log;
            log = LogManager.GetLogger("VCRrefundsLogNunit");
            initVars.GroupName = "ImageRes";
            initVars.QueueName = "ETTS";
            initVars.UserPassword = "IcebUg86";
            initVars.UserID = "11362";
            initVars.SabreSysPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + initVars.SabreSysPath;
            initVars.logger = log;
            bool putItemsInqueue = false;

            VCRrefundQueueManager testM = new VCRrefundQueueManager(initVars);

            if (putItemsInqueue) testM.StablishConnection();


            //Based on the TestItemsInQueue test in the workflow

            RefundAppVariables InitVariables = new RefundAppVariables();
            VCRrefundQueueTranslator test = new VCRrefundQueueTranslator(InitVariables);

            List<string> lines = new List<string>();
            lines = new List<string>();

            lines.Add("ET1*BCTSXEAS*******1165             214258546012  ");
            lines.Add("ET2*BEACH/SANDI                   RICHARD TESTACCT");
            lines.Add("ET3*PO BOX 24948");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  9459    PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*RICHARD.TESTACCT/ALASKAAIR.COM");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*1000037318  INVALID CERTIFICATE CODE.");
            lines.Add("ET25*");

            RefundItem resItem = test.TranslateAcoountingQueueRefund(lines);

            test.UpdateRefundItemLines(resItem);


            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NKKMNCASXXXXXXX1165             21204883031");
            lines.Add("ET2*TESTER/KAY                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98186                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000001295  INVALID CERTIFICATE CODE.");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();

            lines.Add("ET1*NLUVTNASXXXXXXX1165             21204883081");
            lines.Add("ET2*TESTER/KAY                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NLUVTNASXXXXXXX1165             21204883091");
            lines.Add("ET2*TESTER/PAT                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NLUVTNASXXXXXXX1165             21204883101");
            lines.Add("ET2*TESTER/ANGEL                  TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*PQMUBUASXXXXXXX1165             21204883231");
            lines.Add("ET2*TESTER/KAY                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000001296  INVALID CERTIFICATE CODE.");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*PQMUBUASXXXXXXX1165             21204883241");
            lines.Add("ET2*TESTER/PAT                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000001297  THE MILEAGE PLAN SYSTEM IS UNAVAILABLE.");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*PNBVFVASXXXXXXX1165             21204883771");
            lines.Add("ET2*TESTER/KAY                    TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000001303  INVALID CERTIFICATE CODE.");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*PNBVFVASXXXXXXX1165             21204883801");
            lines.Add("ET2*TESTER/PATRICK                TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000001304  THE MILEAGE PLAN SYSTEM IS UNAVAILABLE.");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NMUCPMASXXXXXXX1165             21204883941");
            lines.Add("ET2*TESTER/KAY                    TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*POVEQR                          212055332212");
            lines.Add("ET2*TESTER/GOUIN                  ALASKA AIRLINES");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272120553613027212055332222140");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1660     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*22140");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*US11660     AY 500");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*CHRISTINE.GOUIN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*LHCFASASXXXXXXX2839             212055181413");
            lines.Add("ET2*TESTER/SHERMAN                SHERMAN TESTER");
            lines.Add("ET3*PO BOX 689");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272120551843027212055181416744");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1256     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*101424");
            lines.Add("ET16*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET17*XF 450      XF 300      US13316     AY 750");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1209013098N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NUQIJS                          212055181513");
            lines.Add("ET2*TESTER/SHERMAN A              SHERMAN TESTER");
            lines.Add("ET3*PO BOX 689");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272120552106027212055181516744");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1256     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*16744");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*US11256     AY 500");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NUQIJS                          212055181613");
            lines.Add("ET2*TESTER/SHERMAN B              SHERMAN TESTER");
            lines.Add("ET3*PO BOX 689");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272120552107027212055181616744");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1256     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*16744");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*US11256     AY 500");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*BZVOPYASXXXXXXX1165             212050967412");
            lines.Add("ET2*TESTACCOUNT/RICHARD           RICHARD TESTACCT");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  67906   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*GYZKLVASXXXXXXX1165             212061563412");
            lines.Add("ET2*OLSON/CARA                    ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*                              PHOENIX                   AZ");
            lines.Add("ET5*00000                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  89211   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FGRMMJASXXXXXXX1165             212029253312");
            lines.Add("ET2*PERSHALL/LARRY                ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*                              PHOENIX                   AZ");
            lines.Add("ET5*00000                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  40585   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*CBUKPTASXXXXXXX1165             212061316212");
            lines.Add("ET2*WINSTON/KAFIAH                ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  28080   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*DAIEAEASXXXXXXX1165             212061456812");
            lines.Add("ET2*MAIETTA/KAREN                 ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  0757    TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*ODQHFNASXXXXXXX1165             212061456512");
            lines.Add("ET2*WANJE/DAVID                   ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  37402   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*BFVUFAASXXXXXXX1165             212061562812");
            lines.Add("ET2*SUAREZGUERRA/ANNA             ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  33035   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*EWZKWRASXXXXXXX1165             212061528512");
            lines.Add("ET2*CRANE/GARY                    ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  30550   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FGRLAVASXXXXXXX1165             212061435712");
            lines.Add("ET2*BAUDER/BRUCE                  ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  58811   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*HNMUDNASXXXXXXX1165             212061456312");
            lines.Add("ET2*OLSOWSKY/TAD                  ALASKA AIRLINES");
            lines.Add("ET3*14601 S 50TH");
            lines.Add("ET4*");
            lines.Add("ET5*00000     PHOENIX    F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  60056   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*LHZAXNASXXXXXXX1165             21206260211");
            lines.Add("ET2*BEAR/YOGI");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                      0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  10145   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*LHZAXNASXXXXXXX1165             21206260301");
            lines.Add("ET2*JOHNSON/RUTHATTENDANT");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                      0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  10145   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*LHZAXNASXXXXXXX1165             21206260311");
            lines.Add("ET2*RANGER/PARKESCORT");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                      0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  10145   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*IOEZFFASXXXXXXX2839             212070986413");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272120711204027212070986431070");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2330     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*56558");
            lines.Add("ET16*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET17*XF 450      XF 450      US14242     AY 750");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1209056003N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FLTNYX                          212070986613");
            lines.Add("ET2*TESTER/SNOWY                  SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272120710889027212070986631070");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2330     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*10046");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*US1754      AY 500");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FLTNYX                          212070986513");
            lines.Add("ET2*SNOW/TESTER                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272120710890027212070986531070");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2330     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*10046");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*US1754      AY 500");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*METJSJASXXXXXXX2839             212071578012");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272120715806027212071578026790");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2010     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*45418");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 300");
            lines.Add("ET17*US11922     AY 500");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1209056176N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*DYOJWBCA                        21207032011");
            lines.Add("ET2*BUSHMAN/SHELBY                SHELBY BUSHMAN");
            lines.Add("ET3*920 SOUTH ROCK ROAD");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98101                 011223344550");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  38529");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*EQRNFUCA                        21207030671");
            lines.Add("ET2*BUSHMAN/JASON                 SHELBY BUSHMAN");
            lines.Add("ET3*1111000 LOLIPOP LN");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98391                 000000505690");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  26876");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*EQRNFUCA                        21207030681");
            lines.Add("ET2*BUSHMAN/SHELBY                SHELBY BUSHMAN");
            lines.Add("ET3*1111000 LOLIPOP LN");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98391                 000000505690");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  26876");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*EGSACHASXXXXXXX1165             21207718861");
            lines.Add("ET2*TESTER/DANDE                  DANDE TESTER");
            lines.Add("ET3*PO BOX 24948");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*GAXYLRASXXXXXXX1165             21207159031");
            lines.Add("ET2*ALASKA/MARILYN                TRAINER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  41281   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NXAICNASXXXXXXX1165             21204946841");
            lines.Add("ET2*ALASKA/TERESA                 TRAINER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                 0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  91761   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*NXGQDIASXXXXXXX1165             21207158991");
            lines.Add("ET2*ALASKA/NYLA                   TRAINER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  41085   TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*OGIGWDCA                        21208238791");
            lines.Add("ET2*TEST/IMAGE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  17351");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FDANHRAS*******1165             21417799101");
            lines.Add("ET2*TESTER/CHRIS                  AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121058AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16218919124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FUEMGHAS*******1165             21417799311");
            lines.Add("ET2*TESTER/RICKETT                AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121079AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16219319124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*DZOTIBAS*******1165             21417799541");
            lines.Add("ET2*TESTER/TOPHER                 AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121098AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16220619124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*CBDSKTAS*******1165             21417663081");
            lines.Add("ET2*DANDE/EDINA                   IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08119595AS*******1165         ALASKA");
            lines.Add("ET8*    50.00   16185385011   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*TSTPNRAS*******1165             213492165912");
            lines.Add("ET2*POTTER/HARRY                  070508 100834");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*TSTTWOAS*******1165             213492165912");
            lines.Add("ET2*TESTER/TESTY                  070508 100834");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*07268052");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");


            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FNRHMOAS*******1165             21416282681");
            lines.Add("ET2*TESTER/ETTWEIN                AS");
            lines.Add("ET3*XX");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08105154AS*******1165         CHRIS");
            lines.Add("ET8*    50.00   12666919124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*BPHMGEAS*******1165             21416283021");
            lines.Add("ET2*TESTER/RICKETT                AS");
            lines.Add("ET3*XX");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08105193AS*******1165         JOE");
            lines.Add("ET8*    50.00   12667419124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*KBEICHAS*******1165             21417630661");
            lines.Add("ET2*CAMARGO/ALEJANDRO             IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08119018AS*******1165         ALASKA");
            lines.Add("ET8*    50.00   16173885011   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*HDSIWTASXXXXXXX1165             21180002361");
            lines.Add("ET2*IMAGE/TEST                    IMAGE TEST");
            lines.Add("ET3*123 MAIN");
            lines.Add("ET4*                              AUBURN                    WA");
            lines.Add("ET5*98092                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000000272  INVALID CERTIFICATE CODE.");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FDANHRAS*******1165             21417799101");
            lines.Add("ET2*TESTER/CHRIS                  AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121058AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16218919124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FUEMGHAS*******1165             21417799311");
            lines.Add("ET2*TESTER/RICKETT                AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121079AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16219319124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*DZOTIBAS*******1165             21417799541");
            lines.Add("ET2*TESTER/TOPHER                 AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121098AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16220619124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*CBDSKTAS*******1165             21417663081");
            lines.Add("ET2*DANDE/EDINA                   IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08119595AS*******1165         ALASKA");
            lines.Add("ET8*    50.00   16185385011   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*TSTPNRAS*******1165             213492165912");
            lines.Add("ET2*POTTER/HARRY                  070508 101424");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*TSTTWOAS*******1165             213492165912");
            lines.Add("ET2*TESTER/TESTY                  070508 101424");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*07268052");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");


            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*FNRHMOAS*******1165             21416282681");
            lines.Add("ET2*TESTER/ETTWEIN                AS");
            lines.Add("ET3*XX");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08105154AS*******1165         CHRIS");
            lines.Add("ET8*    50.00   12666919124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*BPHMGEAS*******1165             21416283021");
            lines.Add("ET2*TESTER/RICKETT                AS");
            lines.Add("ET3*XX");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08105193AS*******1165         JOE");
            lines.Add("ET8*    50.00   12667419124   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();
            lines.Add("ET1*KBEICHAS*******1165             21417630661");
            lines.Add("ET2*CAMARGO/ALEJANDRO             IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08119018AS*******1165         ALASKA");
            lines.Add("ET8*    50.00   16173885011   PROD");
            lines.Add("ET9*");
            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");


            lines = new List<string>();


            lines.Add("ET1*DOQJBOASXXXXXXX2839             2123512879123");
            lines.Add("ET2*TESTER/SHERMAN                SHERMAN TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123512893027212351287925209");
            lines.Add("ET11*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET12*XF 450      XF 450      US 1891     AY 750");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*43348");
            lines.Add("ET16*ZP 350      ZP 000      ZP 000      ZP 350");
            lines.Add("ET17*ZP 350      XF 450      XF 450      XF 450");
            lines.Add("ET18*XF 450      AY 1000     US13252");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1010170693N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HUVBWKASXXXXXXX2839             212351218713");
            lines.Add("ET2*TESTER/SHERMAN                SHERMAN TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123513568027212351218742775");
            lines.Add("ET11*ZP 350      ZP 000      ZP 350      XF 450");
            lines.Add("ET12*XF 450      US 2565     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*89370");
            lines.Add("ET16*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET17*XF 450      XF 300      AY 750      US12970");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1010170847N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FHNMUVASXXXXXXX1165             212353801712");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                 0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JJUDPLASXXXXXXX1165             21235382031");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JJUDPLASXXXXXXX1165             21235382041");
            lines.Add("ET2*TESTER/PAT                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NVHLULASXXXXXXX1165             21235382391");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KZBOBGASXXXXXXX1165             21235382701");
            lines.Add("ET2*TESTER/KAY                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                N0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KZBOBGASXXXXXXX1165             21235382711");
            lines.Add("ET2*TESTER/PAT                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                N0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JKUHHPASXXXXXXX1165             212353832912");
            lines.Add("ET2*TESTER/KAY                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                N0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JKUHHPASXXXXXXX1165             212353833012");
            lines.Add("ET2*TESTER/PAT                    TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                N0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FKYGUVASXXXXXXX1165             21235383631");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KAUYLAASXXXXXXX1165             212353838612");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*DJOLQQASXXXXXXX1165             212354368212");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NDXKCYASXXXXXXX1165             212354372212");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                 0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MAFNSJASXXXXXXX2839             212354406912");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123543106027212354406926790");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2010     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*35162");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US12638");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1209181805N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GWGKGUASXXXXXXX1165             212354380612");
            lines.Add("ET2*TEST/KAY                      DANDE TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GWGKGUASXXXXXXX1165             212354380812");
            lines.Add("ET2*TEST/PAT                      DANDE TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KTCHTAASXXXXXXX1165             21235438991");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                N0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KHKTPJASXXXXXXX1165             212354393812");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MDVBYKASXXXXXXX1165             212354395312");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KILHLHASXXXXXXX1165             21235456461");
            lines.Add("ET2*SHEPHARD/TESTONLYTHREE        TEST");
            lines.Add("ET3*123 MAIN ST");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98116                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KZPHDYASXXXXXXX1165             21235488461234");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GCGWPGASXXXXXXX1165             21235490381");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98186                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NOBEDPASXXXXXXX1165             21236293471");
            lines.Add("ET2*DANDE/EDINA                   ALASKA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98188                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JUXLTRASXXXXXXX7774             21236305241");
            lines.Add("ET2*IMAGE/EDINA                   EDINA DANDE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LRFESIASXXXXXXX7774             21236305341");
            lines.Add("ET2*IMAGE/EDINA                   EDINA DANDE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98188                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*DGCYEOASXXXXXXX7774             21236305611");
            lines.Add("ET2*IMAGE/EDINA                   EDINA DANDE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98188                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PIGPHCASXXXXXXX7774             21236306631");
            lines.Add("ET2*IMAGE/TEST                    EDINA DANDE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98188                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KUBRVMASXXXXXXX1165             212354486212");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123633197027212354486213395");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1005     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*13395");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11005");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212213484N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JYMYMQASXXXXXXX2839             212363232113");
            lines.Add("ET2*SNOW/TESTER                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123634084027212363232126790");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2010     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*158046");
            lines.Add("ET16*ZP 350      ZP 000      ZP 000      ZP 350");
            lines.Add("ET17*XF 450      XF 300      AY 500      US18194");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1209213487N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HQBBLZ                          212363232213");
            lines.Add("ET2*TESTER/SNOWY                  SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123634097027212363232226790");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2010     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*23069");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11731");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HQBBLZ                          212363232313");
            lines.Add("ET2*SNOWY/TESTER                  SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123634098027212363232326790");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2010     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*23069");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11731");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BYDWDW                          212363804112");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123640052027212363804159348");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 4452     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*23814");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11786");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*0138200");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JMZPVL                          212364135112");
            lines.Add("ET2*TESTER/UMNR                   AS");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98198                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123641983027212364135127720");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2080     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*27720");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US12080");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*SUE.STEPHENS/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BGSDCO                          212363875212");
            lines.Add("ET2*TESTER/UMNR                   AS");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98198                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123642616027212363875227720");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2080     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*27720");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US12080");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*SUE.STEPHENS/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BGSDCO                          212364261612");
            lines.Add("ET2*TESTER/UMNR                   AS");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98198                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123645560027212364261627720");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2080     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*27720");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US12080");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*SUE.STEPHENS/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EVMWZWASXXXXXXX1165             21236694091234");
            lines.Add("ET2*IMAGE/EDINA                   IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*21236694101");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*DSEGNO                          212367015112");
            lines.Add("ET2*TESTER/MOORE                  MOORE TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123670156027212367015123070");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 400");
            lines.Add("ET12*US 1730     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*22140");
            lines.Add("ET16*ZP 350      ZP 350      XF 400      XF 450");
            lines.Add("ET17*AY 500      US11660");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*DEBRA.MOORE/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EERKSD                          212367179012");
            lines.Add("ET2*TESTER/STRAHM                 WEB TESTING INC");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                      1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123671522027212367179038884");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 400");
            lines.Add("ET12*US 2916     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*18232");
            lines.Add("ET16*ZP 350      ZP 350      XF 400      XF 450");
            lines.Add("ET17*AY 500      US11368");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*LYN.STRAHM/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EERKSDASXXXXXXX2839             212367152212");
            lines.Add("ET2*TESTER/STRAHM                 WEB TESTING INC");
            lines.Add("ET3*145 LOCKSMITH WAY");
            lines.Add("ET4*                              MEXICO CITY               COAH");
            lines.Add("ET5*58499     MX          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123670201027212367152218232");
            lines.Add("ET11*ZP 350      ZP 350      XF 400      XF 450");
            lines.Add("ET12*US 1368     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*21024");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 400");
            lines.Add("ET17*AY 500      US11576");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             0917224800N");
            lines.Add("ET21*LYN.STRAHM/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CQWYIBASXXXXXXX1165             21236792351");
            lines.Add("ET2*TEST/REISSUE                  TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  8191    PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IEOELR                          212367702712");
            lines.Add("ET2*TESTER/DYLAN                  ALASKA AIRLINES");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123677041027212367702722884");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1716     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*22884");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11716");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*STEPHANIE.LORENZ/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NUKFIMASXXXXXXX1165             21236792531");
            lines.Add("ET2*TEST/AUTOREISSUE              TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  8191    PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NVTSCYASXXXXXXX1165             212367927612");
            lines.Add("ET2*TEST/AUTOREISSUE              TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  8191    PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KRKWNJASXXXXXXX1165             21236881031");
            lines.Add("ET2*TEST/KAY                      KAY TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KQUASWASXXXXXXX1165             21235352151");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  46463   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000003756  INVALID CERTIFICATE CODE.");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KQUASWASXXXXXXX1165             21235352151");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  46463   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000003756  INVALID CERTIFICATE CODE.");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CPRNACASXXXXXXX1165             21237103081234");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KUBRVMASXXXXXXX1165             212363319712");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123709931027212363319713395");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1005     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*13395");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11005");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212236110N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KUBRVMASXXXXXXX1165             212370993112");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123709938027212370993113395");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1005     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*13395");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11005");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212236115N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*COOUNVCA                        21237140651");
            lines.Add("ET2*DINGWALL/SHAUN                TESTER WYATT");
            lines.Add("ET3*2468 222ND AVE SE");
            lines.Add("ET4*                              MAPLE HILLS               WA");
            lines.Add("ET5*98038                N0123456789 0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  78055   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*DQZTRKASXXXXXXX1165             21236772351");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11362   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*100039241   TESTING REFUNDS");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IKXUPHASXXXXXXX1165             21237157841");
            lines.Add("ET2*TESTER/WILLIAM                A TESTER");
            lines.Add("ET3*123 FAKE ST");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98144     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123715791027212371578430605");
            lines.Add("ET11*ZP 350      XF 450      US 2295     AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*35256");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US12644");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0210236217N");
            lines.Add("ET21*WILLIAM.HO/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LMAFFPASXXXXXXX1165             21236772411");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11      PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*100039242   TEST");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BFHHNZASXXXXXXX2839             21237180971");
            lines.Add("ET2*TESTER/GOUIN                  ALASKA AIRLINES");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212371662302721237180972967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             0417236267N");
            lines.Add("ET21*CHRISTINE.GOUIN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BFHHNZASXXXXXXX2839             212371662312");
            lines.Add("ET2*TESTER/GOUIN                  ALASKA AIRLINES");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212372008102721237166232967");
            lines.Add("ET11*ZP 350      ZP 350      US 223      AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*4642");
            lines.Add("ET16*ZP 350      AY 250      US1348");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             0417236319N");
            lines.Add("ET21*CHRISTINE.GOUIN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGPRYYASXXXXXXX1165             212375091212");
            lines.Add("ET2*CRANE/ICABOD                  ICHABAD CRAN");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGOZTWASXXXXXXX1165             212374948812");
            lines.Add("ET2*CRANE/ICHABOD                 MAGGIE HABERKORN");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129980  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CZKIQCASXXXXXXX1165             212375134212");
            lines.Add("ET2*CRANE/ICHABOD                 JUSTIN STRATTON");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                 0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  641420  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JVVBYQASXXXXXXX1165             212375074012");
            lines.Add("ET2*CRANE/ICHABOD                 JOSH PANTOJA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                 0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  138770  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JULNCVASXXXXXXX1165             212374977112");
            lines.Add("ET2*CRANE/ICHABOD                 QUINN GODECKE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  393700  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EDHCTJASXXXXXXX1165             212375095312");
            lines.Add("ET2*GARCIA/JOSE                   ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EDHCTJASXXXXXXX1165             212375095412");
            lines.Add("ET2*GARCIA/MARIA                  ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MZXQLXASXXXXXXX1165             212375379812");
            lines.Add("ET2*ZUCKO/DANNY                   JUSTIN STRATTON");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  641420  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JUAYBPASXXXXXXX1165             212375486312");
            lines.Add("ET2*ZUCKO/DANNY                   QUINN GODECKE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  393700  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MZXQLXASXXXXXXX1165             212375379912");
            lines.Add("ET2*OLESSON/SANDY                 JUSTIN STRATTON");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  641420  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JUAYBPASXXXXXXX1165             212375486412");
            lines.Add("ET2*OLESSON/SANDY                 QUINN GODECKE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  393700  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KLZMDPASXXXXXXX1165             212375141112");
            lines.Add("ET2*GARCIA/JOSE                   JUSTIN STRATTON");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  641420  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KLZMDPASXXXXXXX1165             212375141212");
            lines.Add("ET2*GARCIA/MARIA                  JUSTIN STRATTON");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  641420  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MZPBVVASXXXXXXX1165             212375527212");
            lines.Add("ET2*ZUCKO/DANNY                   JOSH PANTOJA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  138770  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MZPBVVASXXXXXXX1165             212375527312");
            lines.Add("ET2*OLESSON/SANDY                 JOSH PANTOJA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  138770  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CJMZCHASXXXXXXX1165             21237552231");
            lines.Add("ET2*MATLOCK/AMANDA                NONE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123753886027212375522331535");
            lines.Add("ET11*ZP 350      XF 450      US 2365     AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*55721");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US14179");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0709240497N");
            lines.Add("ET21*AMANDA.MATLOCK/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JUKRFVASXXXXXXX1165             21237548891");
            lines.Add("ET2*BING/MONICA                   QUINN GODECKE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  393700  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CLDMOKASXXXXXXX1165             212375534612");
            lines.Add("ET2*ZUCKO/DANNY                   ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CLDMOKASXXXXXXX1165             212375534712");
            lines.Add("ET2*OLESSON/SANDY                 ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LSMVQPASXXXXXXX1165             21237555501");
            lines.Add("ET2*BING/MONICA                   ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MDTEYFASXXXXXXX1165             212375625212");
            lines.Add("ET2*ZUCKO/DANNY                   LYNN SUNWALL");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  144180  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MDTEYFASXXXXXXX1165             212375625312");
            lines.Add("ET2*OLESSON/SANDY                 LYNN SUNWALL");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  144180  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JFTGMEASXXXXXXX1165             21237572021");
            lines.Add("ET2*BING/MONICA                   MAGGIE HABERKORN");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98468                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129980  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MDHJTKASXXXXXXX1165             21237584501");
            lines.Add("ET2*BING/MONICA                   LYNN SUNWALL");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  144180  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JAQXOTASXXXXXXX1165             21237585841");
            lines.Add("ET2*BING/MONICA                   LYNN SUNWALL");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  144180  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LBOJCQASXXXXXXX1165             21237616951");
            lines.Add("ET2*KAY/TEST                      KAY TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212376171302721237616952967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*6754");
            lines.Add("ET16*ZP 350      AY 250      US11006");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0709240808N");
            lines.Add("ET21*TEST/TEST.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JIUUCQ                          212375946512");
            lines.Add("ET2*TESTER/MOORE                  MOORE TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              FEDERAL WAY                WA");
            lines.Add("ET5*98023                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123763012027212375946523070");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1730     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*23070");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11730");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*DEBRA.MOORE/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FAZWDL                          212375956612");
            lines.Add("ET2*TESTER/RACHEL                 RACHEL TESTER");
            lines.Add("ET3*MY TOWN 1234");
            lines.Add("ET4*                              YELM                       WA");
            lines.Add("ET5*98597                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123763419027212375956631907");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 2393     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*23070");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11730");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*RACHEL.ANDERSON/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FAZWDLASXXXXXXX2839             212376341912");
            lines.Add("ET2*TESTER/RACHEL                 RACHEL TESTER");
            lines.Add("ET3*MY TOWN 1234");
            lines.Add("ET4*                              YELM                      WA");
            lines.Add("ET5*98597     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123764814027212376341923070");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 1730     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*60826");
            lines.Add("ET16*ZP 350      ZP 350      ZP 350      ZP 350");
            lines.Add("ET17*XF 450      XF 300      XF 450      XF 300");
            lines.Add("ET18*AY 1000     US12514");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1210240914N");
            lines.Add("ET21*RACHEL.ANDERSON/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NRPWPAASXXXXXXX2839             2123764441134");
            lines.Add("ET2*SNOWY/TESTER                  SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123765213027212376444155106");
            lines.Add("ET11*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET12*XF 450      XF 300      US 2234     AY 750");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*74177");
            lines.Add("ET16*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET17*AY 500      US12163");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1209240923N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NSADHS                          2123764438134");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123764115027212376443855106");
            lines.Add("ET11*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET12*XF 450      XF 300      US 2234     AY 750");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*12930");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US1970");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NSADHS                          2123764439134");
            lines.Add("ET2*SNOW/TESTER                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123764116027212376443955106");
            lines.Add("ET11*ZP 350      ZP 350      ZP 350      XF 450");
            lines.Add("ET12*XF 450      XF 300      US 2234     AY 750");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*12930");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US1970");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NVQEZIASXXXXXXX1165             21237654431");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KXYXSJ                          212376635112");
            lines.Add("ET2*RABLIN/RICK                   ALASKA AIRLINES");
            lines.Add("ET3*16209 144TH AVE SE");
            lines.Add("ET4*                              RENTON                     WA");
            lines.Add("ET5*98058                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212377076402721237663516754");
            lines.Add("ET11*ZP 350      ZP 350      US 1006     AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*RICK.RABLIN/ALASKAAIR.COM");
            lines.Add("ET22*015170");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KXYXSJ                          212376635212");
            lines.Add("ET2*RABLIN/DONNA                  ALASKA AIRLINES");
            lines.Add("ET3*16209 144TH AVE SE");
            lines.Add("ET4*                              RENTON                     WA");
            lines.Add("ET5*98058                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212377076502721237663526754");
            lines.Add("ET11*ZP 350      ZP 350      US 1006     AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*RICK.RABLIN/ALASKAAIR.COM");
            lines.Add("ET22*015170");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KXYXSJ                          212376635312");
            lines.Add("ET2*RABLIN/TYLER                  ALASKA AIRLINES");
            lines.Add("ET3*16209 144TH AVE SE");
            lines.Add("ET4*                              RENTON                     WA");
            lines.Add("ET5*98058                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212377076602721237663536754");
            lines.Add("ET11*ZP 350      ZP 350      US 1006     AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*RICK.RABLIN/ALASKAAIR.COM");
            lines.Add("ET22*015170");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KXPWWZ                          212376990712");
            lines.Add("ET2*RABLIN/RICK                   ALASKA AIRLINES");
            lines.Add("ET3*16209 144TH AVE SE");
            lines.Add("ET4*                              RENTON                     WA");
            lines.Add("ET5*98058                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212377261302721237699076754");
            lines.Add("ET11*ZP 350      ZP 350      US 1006     AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*RICK.RABLIN/ALASKAAIR.COM");
            lines.Add("ET22*015170");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LNVKKLASXXXXXXX1165             21237860921");
            lines.Add("ET2*IMAGE/EDINA                   IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GXSXEOASXXXXXXX1165             21237860591");
            lines.Add("ET2*IMAGE/EDINA                   IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NJTOVG                          212379929412");
            lines.Add("ET2*TEST/S                        WEB TEST");
            lines.Add("ET3*PO BOX 12355");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98198                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212379893602721237992942967");
            lines.Add("ET11*ZP 350      ZP 350      US 223      AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*SHARRYN.BOLGER/ALASKAAIR.COM");
            lines.Add("ET22*01600");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BLHUOGASXXXXXXX7774             21238043361");
            lines.Add("ET2*TEST/KAY");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*NQVJOKASXXXXXXX1165             21238043761");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BLGBKIASXXXXXXX1165             212380433012");
            lines.Add("ET2*TEST/REISSUE                  TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              MILTON                    WA");
            lines.Add("ET5*98354                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  8191    PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*ISNJJIASXXXXXXX1165             21238824241");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212388332302721238824242967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             1111283694N");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PMTOHQ                          212388418712");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212388232702721238841872967");
            lines.Add("ET11*ZP 350      ZP 350      US 223      AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01600");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PMTOHQASXXXXXXX1165             21238823271");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212388392802721238823272967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             1111283721N");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PMTOHQ                          212388392812");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212388422502721238839282967");
            lines.Add("ET11*ZP 350      ZP 350      US 223      AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01600");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PMTOHQASXXXXXXX1165             21238842251");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212388450802721238842252967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             1111283746N");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PMTOHQ                          212388450812");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212388600202721238845082967");
            lines.Add("ET11*ZP 350      ZP 350      US 223      AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01600");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BUVJOIASXXXXXXX1165             21238880611");
            lines.Add("ET2*BUELLEN/FERRIS                JOSH PANTOJA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*68900                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  138770  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BTLXUSASXXXXXXX1165             212388781112");
            lines.Add("ET2*BUELLER/FERRIS                JUSTIN STRATTON");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  641420  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PIRZARASXXXXXXX1165             212388472412");
            lines.Add("ET2*BUELLEN/FERRIS                ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*OLQOJHASXXXXXXX1165             212388759612");
            lines.Add("ET2*BUELLER/FERRIS                MAGGIE HABERKORN");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129980  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CFWFRPASXXXXXXX1165             212388781912");
            lines.Add("ET2*BUELLER/FERRIS                QUINN GODECKE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  393700  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CIEXASASXXXXXXX1165             212388786512");
            lines.Add("ET2*BUELLER/FERRIS                LYNN SUNWALL");
            lines.Add("ET3*PO BOX68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  144180  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FEUZLSASXXXXXXX1165             212388474412");
            lines.Add("ET2*BUELLER/FERRIS                ANNA MAILLARD");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  129760  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*OMELAZASXXXXXXX1165             212388789812");
            lines.Add("ET2*BUELLER/FERRIS                QUINN GODECKE");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  393700  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BVCRERASXXXXXXX1165             212388823112");
            lines.Add("ET2*BUELLER/FERRIS                JOSH PANTOJA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*68900                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  138770  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*PMPWMNASXXXXXXX1165             21238879751");
            lines.Add("ET2*IMAGE/GRETAT                  GRETA T IMAGE");
            lines.Add("ET3*PO BOX 48309");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  144180  TRNG");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HUKYQD                          212392198312");
            lines.Add("ET2*TESTER/GOUIN                  ALASKA AIRLINES");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123924924027212392198329674");
            lines.Add("ET11*ZP 350      ZP 350      US 2226     AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*29674");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US12226");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*CHRISTINE.GOUIN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FLTRSXASXXXXXXX7774             21239292631");
            lines.Add("ET2*IMAGE/EDINA                   EDINA IMAGE");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HUKYQDASXXXXXXX1165             212392492412");
            lines.Add("ET2*TESTER/GOUIN                  ALASKA");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123930328027212392492429674");
            lines.Add("ET11*ZP 350      ZP 350      US 2226     AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*29674");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US12226");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0709295316N");
            lines.Add("ET21*CHRISTINE.GOUIN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GSJERKASXXXXXXX1165             21239322071");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MXQMWOASXXXXXXX1165             21239327251");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MBTYSL                          212393615012");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123938054027212393615059348");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 4452     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*59348");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US14452");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MBTYSL                          212393805412");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123936992027212393805459348");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 4452     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*18418");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US11382");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IYAECM                          21239547461");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98166                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212395683402721239547467349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IYAECMASXXXXXXX1165             21239568341");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212395675402721239568347349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212306165N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HIFFSIASXXXXXXX7774             21239577471");
            lines.Add("ET2*TESTER/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HIFFSIASXXXXXXX7774             21239577481");
            lines.Add("ET2*TESTER/LORI");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HIKMBYASXXXXXXX2839             212395852312");
            lines.Add("ET2*TESTER/SHERMAN                SHERMAN TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123958533027212395852348060");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 300");
            lines.Add("ET12*US 1680     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*93130");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 300");
            lines.Add("ET17*AY 500      US11810");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1210306173N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IRFPKIASXXXXXXX1165             212395780412");
            lines.Add("ET2*TESTER/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IYAECMASXXXXXXX1165             21239567541");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212395693302721239567547349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212306187N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IYAECMASXXXXXXX1165             21239569331");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212395884302721239569337349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212306190N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IYAECMASXXXXXXX1165             21239588431");
            lines.Add("ET2*VINTHER/DAVID                 DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              BURIEN                    WA");
            lines.Add("ET5*98166     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212395816802721239588437349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             0212306195N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IGGBPRASXXXXXXX1165             212395790212");
            lines.Add("ET2*TEST/PAM");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*ISATWNASXXXXXXX1165             212395790012");
            lines.Add("ET2*TESTER/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*ISATWNASXXXXXXX1165             212395790112");
            lines.Add("ET2*TEST/LORI");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HUMDOVASXXXXXXX2839             212395858113");
            lines.Add("ET2*TESTER/SHERMAN                SHERMAN TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA0272123957601027212395858148659");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 300");
            lines.Add("ET12*US 1681     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*59228");
            lines.Add("ET16*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET17*AY 500      US11712");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             1210306197N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GVHDIHASXXXXXXX1165             212395794513");
            lines.Add("ET2*TEST/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IZKYDBASXXXXXXX1165             21239579941");
            lines.Add("ET2*TEST/A");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IZKYDBASXXXXXXX1165             21239579951");
            lines.Add("ET2*TEST/B");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IZKYDBASXXXXXXX1165             21239579971");
            lines.Add("ET2*TEST/C");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IZKYDBASXXXXXXX1165             21239579981");
            lines.Add("ET2*TEST/D");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IZKYDBASXXXXXXX1165             21239579991");
            lines.Add("ET2*TEST/E");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IZKYDBASXXXXXXX1165             21239580001");
            lines.Add("ET2*TEST/F");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GXQAFY                          212395858213");
            lines.Add("ET2*TESTER/SHERMAN A              SHERMAN TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123958262027212395858248659");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 300");
            lines.Add("ET12*US 1681     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*48659");
            lines.Add("ET16*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET17*AY 500      US11681");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GXQAFY                          212395858313");
            lines.Add("ET2*TESTER/SHERMAN B              SHERMAN TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE0272123958263027212395858348659");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 300");
            lines.Add("ET12*US 1681     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*48659");
            lines.Add("ET16*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET17*AY 500      US11681");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*CINDY.SHERMAN/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IUFSUYASXXXXXXX1165             21239600541");
            lines.Add("ET2*TEST/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IUFSUYASXXXXXXX1165             21239600551");
            lines.Add("ET2*TEST/LORI");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BDUXIEASXXXXXXX1165             21239600821");
            lines.Add("ET2*TEST/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GXDVZI                          21239597721");
            lines.Add("ET2*SMITH/EIGHT                   DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98166                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212396009702721239597727349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GXDVZI                          21239597731");
            lines.Add("ET2*SMITH/FIVE                    DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98166                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212396009802721239597737349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GXDVZI                          21239597741");
            lines.Add("ET2*SMITH/SIX                     DAVIDS CARD");
            lines.Add("ET3*16020 23RD AVE SW");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98166                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RE027212396009902721239597747349");
            lines.Add("ET11*ZP 350      XF 450      US 551      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*7349");
            lines.Add("ET16*ZP 350      XF 450      AY 250      US1551");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  N");
            lines.Add("ET21*DAVID.VINTHER/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IKYQLGASXXXXXXX1165             21239601661234");
            lines.Add("ET2*TEST/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IKYQLGASXXXXXXX1165             21239601671234");
            lines.Add("ET2*TEST/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000004175  CERTIFICATES ALREADY REDEPOSITED.");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*ILMKIN                          212396024112");
            lines.Add("ET2*TEST/ANGIE");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*ILMKIN                          212396024212");
            lines.Add("ET2*TEST/LORI");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HZXWEW                          212396127812");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212396032802721239612782967");
            lines.Add("ET11*ZP 350      ZP 350      US 223      AY 500");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*3154");
            lines.Add("ET16*ZP 350      AY 250      US1236");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01400");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HZXWEW                          21239603281");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR027212396097602721239603283154");
            lines.Add("ET11*ZP 350      US 236      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      AY 250      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01200");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HZXWEWASXXXXXXX1165             21239609761");
            lines.Add("ET2*TESTER/HEDFORS                TESTER");
            lines.Add("ET3*P.O. BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212396102202721239609762967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*2967");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US1223");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX1165             1111306279N");
            lines.Add("ET21*JEFF.HEDFORS/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*U");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JQFQCKASXXXXXXX7774             212396256512");
            lines.Add("ET2*TESTER/KAY");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IBDCAH                          21239626781234");
            lines.Add("ET2*TESTER/GOUIN                  ALASKAAIR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR02721239629430272123962678131764");
            lines.Add("ET11*ZP 350      ZP 350      ZP 350      ZP 350");
            lines.Add("ET12*XF 300      XF 450      XF 450      XF 450");
            lines.Add("ET13*US 7336     AY 1000");
            lines.Add("ET14*");
            lines.Add("ET15*132693");
            lines.Add("ET16*ZP 350      ZP 350      ZP 350      ZP 350");
            lines.Add("ET17*XF 450      XF 450      XF 450      XF 300");
            lines.Add("ET18*AY 1000     US16047");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*CHRISTINE.GOUIN/ALASKAAIR.COM");
            lines.Add("ET22*01360");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GGLKZA                          212396380212");
            lines.Add("ET2*TESTER/SNOW                   SNOW TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98168                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123965844027212396380261210");
            lines.Add("ET11*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET12*US 4590     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*29581");
            lines.Add("ET16*ZP 350      ZP 350      XF 450      XF 450");
            lines.Add("ET17*AY 500      US12219");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*MOLLY.SNOW/ALASKAAIR.COM");
            lines.Add("ET22*0134000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LNENVQASXXXXXXX1165             21239663511");
            lines.Add("ET2*TESTER/DANDE                  DANDE TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LNMCXWASXXXXXXX1165             21239267921");
            lines.Add("ET2*TESTER/DANDE                  EDINA TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LNMCXWASXXXXXXX1165             21239267931");
            lines.Add("ET2*TESTER/EDINA                  EDINA TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*LNMCXWASXXXXXXX1165             21239267941");
            lines.Add("ET2*TESTER/SAM                    EDINA TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296611");
            lines.Add("ET2*CANDY/SOUR                    MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296701");
            lines.Add("ET2*CANDY/COTTON                  MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296621");
            lines.Add("ET2*LO/CARMEL                     MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296631");
            lines.Add("ET2*BAR/SNICKERS                  MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296651");
            lines.Add("ET2*GOODBAR/MR                    MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296671");
            lines.Add("ET2*CHOCOLATE/HERSHEY             MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*EGWZZFASXXXXXXX1165             21239296691");
            lines.Add("ET2*FINGER/BUTTER                 MR GOODBAR");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  85011   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GMXLOPASXXXXXXX1165             21239670901");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BKEPHIASXXXXXXX1165             21239670961");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HLTQIWASXXXXXXX1165             21239671021");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IHOOFMASXXXXXXX1165             21239671501");
            lines.Add("ET2*TESTER/KAYLARIE               TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JQVRGTASXXXXXXX7774             21239672001");
            lines.Add("ET2*TEST/KAY");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*IJGJCTASXXXXXXX7774             21239700831");
            lines.Add("ET2*TEST/KAY");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*GPYEVQASXXXXXXX7774             212397010212");
            lines.Add("ET2*TEST/KAY");
            lines.Add("ET3*");
            lines.Add("ET4*");
            lines.Add("ET5*                     F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HPSXEMASXXXXXXX1165             21239701981234");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*212397019912");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JTPLMUASXXXXXXX1165             21239703121");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JUWKBMASXXXXXXX1165             21239703201");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JVJPVBASXXXXXXX1165             212397036813");
            lines.Add("ET2*TEST/KAY                      TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JYQMUS                          212397303212");
            lines.Add("ET2*TEST/SHARRYN                  SHARRYN M TEST");
            lines.Add("ET3*PO BOX 12345");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98107                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123970672027212397303232449");
            lines.Add("ET11*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET12*US 1221     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*32449");
            lines.Add("ET16*ZP 350      XF 300      AY 250      US11221");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*SHARRYN.BOLGER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JYQMUS                          212397303312");
            lines.Add("ET2*TWO/PAX                       SHARRYN M TEST");
            lines.Add("ET3*PO BOX 12345");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98107                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123970673027212397303332449");
            lines.Add("ET11*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET12*US 1221     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*32449");
            lines.Add("ET16*ZP 350      XF 300      AY 250      US11221");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*SHARRYN.BOLGER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*JYQMUS                          212397303412");
            lines.Add("ET2*THREE/PAX                     SHARRYN M TEST");
            lines.Add("ET3*PO BOX 12345");
            lines.Add("ET4*                              SEATTLE                    WA");
            lines.Add("ET5*98107                 1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RR0272123970674027212397303432449");
            lines.Add("ET11*ZP 350      ZP 350      XF 300      XF 450");
            lines.Add("ET12*US 1221     AY 500");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*32449");
            lines.Add("ET16*ZP 350      XF 300      AY 250      US11221");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*                                  Y");
            lines.Add("ET21*SHARRYN.BOLGER/ALASKAAIR.COM");
            lines.Add("ET22*02000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FDANHRAS*******1165             21417799101");
            lines.Add("ET2*TESTER/CHRIS                  AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121058AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16218919124   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FUEMGHAS*******1165             21417799311");
            lines.Add("ET2*TESTER/RICKETT                AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121079AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16219319124   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*DZOTIBAS*******1165             21417799541");
            lines.Add("ET2*TESTER/TOPHER                 AS");
            lines.Add("ET3*20313 28TH AVE S");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08121098AS*******1165         TESTING");
            lines.Add("ET8*    50.00   16220619124   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*CBDSKTAS*******1165             21417663081");
            lines.Add("ET2*DANDE/EDINA                   IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08119595AS*******1165         ALASKA");
            lines.Add("ET8*    50.00   16185385011   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*TSTPNRAS*******1165             213492165912");
            lines.Add("ET2*POTTER/HARRY                  300708 145100");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*TSTTWOAS*******1165             213492165912");
            lines.Add("ET2*TESTER/TESTY                  300708 145100");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*07268052");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*FNRHMOAS*******1165             21416282681");
            lines.Add("ET2*TESTER/ETTWEIN                AS");
            lines.Add("ET3*XX");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08105154AS*******1165         CHRIS");
            lines.Add("ET8*    50.00   12666919124   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*BPHMGEAS*******1165             21416283021");
            lines.Add("ET2*TESTER/RICKETT                AS");
            lines.Add("ET3*XX");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98198                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08105193AS*******1165         JOE");
            lines.Add("ET8*    50.00   12667419124   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*KBEICHAS*******1165             21417630661");
            lines.Add("ET2*CAMARGO/ALEJANDRO             IMAGE TEST");
            lines.Add("ET3*NONE");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*08119018AS*******1165         ALASKA");
            lines.Add("ET8*    50.00   16173885011   PROD");
            lines.Add("ET9*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*HDSIWTASXXXXXXX1165             21180002361");
            lines.Add("ET2*IMAGE/TEST                    IMAGE TEST");
            lines.Add("ET3*123 MAIN");
            lines.Add("ET4*                              AUBURN                    WA");
            lines.Add("ET5*98092                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  11362   TEST");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*1000000272  INVALID CERTIFICATE CODE.");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");





            lines = new List<string>();


            lines.Add("ET1*MTVBILASXXXXXXX1165             21241607671");
            lines.Add("ET2*TEST/WALTER                   WALTER TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();


            lines.Add("ET1*GRBUFCASXXXXXXX1165             21241617942");
            lines.Add("ET2*TEST/WALTER                   TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                 0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();


            lines.Add("ET1*PUSEWKASXXXXXXX2839             21241847011");
            lines.Add("ET2*TESTER/WEB                    SHARRYN M TEST");
            lines.Add("ET3*PO BOX 12345");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98107     US          1          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*");
            lines.Add("ET9*");
            lines.Add("ET10*RA027212419119702721241847012967");
            lines.Add("ET11*ZP 350      US 223      AY 250");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*6754");
            lines.Add("ET16*ZP 350      ZP 350      AY 500      US11006");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*XXXXXXX2839             0110055162N");
            lines.Add("ET21*SHARRYN.BOLGER/ALASKAAIR.COM");
            lines.Add("ET22*01000");
            lines.Add("ET23*N");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();


            lines.Add("ET1*ILWCXXASXXXXXXX1165             21241930471");
            lines.Add("ET2*TEST/KAY                      KAY TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();


            lines.Add("ET1*IYTECNASXXXXXXX1165             21241911822");
            lines.Add("ET2*TEST/KAY                      KAY TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();


            lines.Add("ET1*MRWVBIASXXXXXXX1165             21241605921");
            lines.Add("ET2*TEST/WALTER                   WALTER TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();


            lines.Add("ET1*MRWVBIASXXXXXXX1165             21241605931");
            lines.Add("ET2*TEST/KAY                      WALTER TEST");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                F0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*U");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");



            lines = new List<string>();

            lines.Add("ET1*JTEDZHASXXXXXXX1165             21241979801234");
            lines.Add("ET2*TEST/KAY                      KAY TESTER");
            lines.Add("ET3*PO BOX 68900");
            lines.Add("ET4*                              SEATTLE                   WA");
            lines.Add("ET5*98168                A0          0");
            lines.Add("ET6*");
            lines.Add("ET7*");
            lines.Add("ET8*                  34150   PROD");
            lines.Add("ET9*2124197981134 212419798212");
            lines.Add("ET10*");
            lines.Add("ET11*");
            lines.Add("ET12*");
            lines.Add("ET13*");
            lines.Add("ET14*");
            lines.Add("ET15*");
            lines.Add("ET16*");
            lines.Add("ET17*");
            lines.Add("ET18*");
            lines.Add("ET19*");
            lines.Add("ET20*");
            lines.Add("ET21*");
            lines.Add("ET22*");
            lines.Add("ET23*N");
            lines.Add("ET24*");

            resItem = test.TranslateAcoountingQueueRefund(lines);
            test.UpdateRefundItemLines(resItem);

            CompareAccountingLines(lines, resItem.QueueLines);
            if (putItemsInqueue) testM.PutLinesIntoQueue(resItem.QueueLines, "ETTS");

            testM.CloseConnection();


        }
        
        public void CompareAccountingLines( List<string> original , List<string> newlist)
        {
            for (int index = 0; index < original.Count; index++)
            {
                Assert.AreEqual(original[index].Trim(), newlist[index].Trim());
            }
        }

    }
}
