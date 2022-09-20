using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using log4net;
using ASHosts.Interop;
using VCRrefunds.MbrAccessWS;

namespace VCRrefunds
{

	[TestFixture]
	public class TestRedepositMilesManager
	{
		[Test]
		public void Redeposit_CycleRedeemRedeposit()
		{

			MbrAccess proxy = new MbrAccess();
			RedeemAward_Request requestRedeem = new RedeemAward_Request();
			RedeemAward_Response responseRedeem = new RedeemAward_Response();
			Passenger pax = new Passenger();
			PassengerCollection passengers = new PassengerCollection();
			Passenger[] thearray = new Passenger[] { pax };
			UserInfo userinf = new UserInfo();

			proxy.Url = "http://image.test.webservice.alaskaair.com/MbrAccessSolar/MbrAccess.asmx";


			requestRedeem.ProductName = "ASYDOS; ASYDO; S";
			requestRedeem.MemberNumber = "96866534";
			requestRedeem.Passengers = new PassengerCollection();
			requestRedeem.User = userinf;
			requestRedeem.User.SabreAAACity = "SEA";
			requestRedeem.User.SabreAgentSign = "11362";
			requestRedeem.User.UserId = "11362";

			pax.FirstName = "Young";
			pax.LastName = "Tester";
			passengers.Items = new Passenger[] { pax };

			requestRedeem.Passengers = passengers;

			responseRedeem = proxy.RedeemAward(requestRedeem);

			Assert.IsTrue(responseRedeem.Certificates.Items.Length > 0);
			Assert.IsTrue(responseRedeem.Certificates.Items[0].CertificateNumber.Trim().Length > 0);

			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
            InitVariables.MbrAccessURL = @"https://api.test.insideaag.com/1/CustomerLoyalty/MemberAccess/";
            InitVariables.MbrAccessPassword = "78YS8ADkPD^htZ^";
            InitVariables.MbrAccessUserName= "0FOYLXNQ80YU";
			RedepositMilesManager redep = new RedepositMilesManager(InitVariables);
			RefundItem item = new RefundItem();
			bool retry = false;
			item.AgentID = "11362";
			item.CertificateNumber = responseRedeem.Certificates.Items[0].CertificateNumber;

			redep.RedepositRefundMiles(item, ref retry);
			Assert.AreEqual(item.RedepositCode, (int) MbrAccessErrorsEnum.NO_ERROR);
			Assert.IsFalse(retry);

		}

		[Test]
		public void RedepositInvalidCert()
		{

			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
            InitVariables.MbrAccessURL = @"https://api.test.insideaag.com/1/CustomerLoyalty/MemberAccess/";
            InitVariables.MbrAccessPassword = "78YS8ADkPD^htZ^";
            InitVariables.MbrAccessUserName = "0FOYLXNQ80YU";
			
			log4net.Config.XmlConfigurator.Configure();			
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.logger = log;
			
			RedepositMilesManager redep = new RedepositMilesManager(InitVariables);
			RefundItem item = new RefundItem();
			bool retry = false;
			item.AgentID = "11362";
			item.CertificateNumber = "000000000";
			item.CertificateNumber2 = "000001234";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.AreEqual(item.RedepositCode, 7);
			Assert.IsFalse(retry);
			Assert.AreEqual(item.RedepositErrormessage, "Invalid Certificate Code Certificate 000000000 was not found or is invalid.\nCertificate 000001234 was not found or is invalid.\n");
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "000000258";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "1000000409";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457774";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457775";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			//Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457776";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457777";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			//Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457778";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457780";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			//Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457781";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457782";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			//Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457783";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457784";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			//Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457785";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);
			item.CertificateNumber = "93457787";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			//Assert.IsTrue(item.RedepositErrormessage.Contains( "Invalid Certificate Code") );
			Assert.AreEqual(item.RedepositCode, 7);

		}


		[Test]
		public void RedepositInvalidCertAlready()
		{

			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			InitVariables.MbrAccessURL = "http://image.test.webservice.alaskaair.com/MbrAccessSolar/MbrAccess.asmx";
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.logger = log;

			RedepositMilesManager redep = new RedepositMilesManager(InitVariables);
			RefundItem item = new RefundItem();
			bool retry = false;
			item.AgentID = "11362";
			item.CertificateNumber = "93435037";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsFalse(retry);
			Assert.AreEqual(item.RedepositErrormessage, "Certificate Already Redeposit");
			Assert.AreEqual(item.RedepositCode, 914);



		}

		[Test]
		public void RedepositWrongURl()
		{

			VCRrefunds.RefundAppVariables InitVariables = new RefundAppVariables();
			InitVariables.MbrAccessURL = "http://imageeeee.test.webservice.alaskaair.com/MbrAccess/MbrAccess.asmx";

			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLogNunit");
			InitVariables.logger = log;

			RedepositMilesManager redep = new RedepositMilesManager(InitVariables);
			RefundItem item = new RefundItem();
			bool retry = false;
			item.AgentID = "11362";
			item.CertificateNumber = "0000000";

			redep.RedepositRefundMiles(item, ref retry);
			Assert.IsTrue(retry);
			Assert.AreEqual(item.RedepositCode, 999);

		}

	}
}
