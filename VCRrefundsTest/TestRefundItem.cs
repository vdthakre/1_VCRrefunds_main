using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;

namespace VCRrefunds
{	
	[TestFixture]
	public class TestRefundItem
	{

        [Test]
        public void test()
        {
            string input = "12";

            string res1 = input.Length <= 6 ? string.Format("{0,-5}", input) : input.Substring(0, 5);

            string empty = string.Format("{0,-5}", "");            
            string resultL = string.Format("{0,5}", input);
            string resultR = string.Format("{0,-5}", input);
        }
		[Test]
		public void testRefundItemProperties()
		{
			RefundItem test = new RefundItem();

			//test the constructor
			List<string> testList = test.QueueLines;

			Assert.AreEqual(testList.Count , 0);

            //Line 1
            Assert.AreEqual(test.Recloc, string.Format("{0,6}", ""));
            Assert.AreEqual(test.FOPType, string.Format("{0,2}", ""));
            Assert.AreEqual(test.CCNumber, string.Format("{0,24}", ""));
            Assert.AreEqual(test.TicketNumber, string.Format("{0,10}", ""));
            Assert.AreEqual(test.CouponNbrs, string.Format("{0,4}", ""));

            //Line 2
            Assert.AreEqual(test.PsgrName, string.Format("{0,30}", ""));
            Assert.AreEqual(test.MailingName, string.Format("{0,30}", ""));

            //Line 3
            Assert.AreEqual(test.MailingAddress1, string.Format("{0,30}", ""));

            //Line 4
            Assert.AreEqual(test.MailingAddress2, string.Format("{0,30}", ""));
            Assert.AreEqual(test.MailingCity, string.Format("{0,26}", ""));
            Assert.AreEqual(test.MailingState, string.Format("{0,4}", ""));

            //Line 20
            Assert.AreEqual(test.ZipCode, string.Format("{0,10}", ""));
            Assert.AreEqual(test.Country, string.Format("{0,11}", ""));
            Assert.AreEqual(test.RefundWaiverCode, string.Format("{0,1}", ""));
            Assert.AreEqual(test.InVolIndicator, string.Format("{0,1}", ""));
            Assert.AreEqual(test.RefundDraftNumber, string.Format("{0,10}", ""));
            Assert.AreEqual(test.DowngradeIndicator, string.Format("{0,1}", ""));

            //Line 6
            Assert.AreEqual(test.OriginalFareBasis, string.Format("{0,15}", ""));
            Assert.AreEqual(test.NewFareBasis, string.Format("{0,15}", ""));

            //Line 7
            Assert.AreEqual(test.OldMPCertificateNbr, string.Format("{0,8}", ""));
            Assert.AreEqual(test.CCTypeForMPRedepositFee, string.Format("{0,2}", ""));
            Assert.AreEqual(test.CCNumberMPRedepositFee, string.Format("{0,20}", ""));
            Assert.AreEqual(test.CCHolderNameMPRedepoFee, string.Format("{0,30}", ""));

            //Line 8
            Assert.AreEqual(test.CCExpDateMPRedepositFee, string.Format("{0,4}", ""));
            Assert.AreEqual(test.MPRedepositFeeAmount, string.Format("{0,8}", ""));
            Assert.AreEqual(test.CCApprovalCodeMPRedepositFee, string.Format("{0,6}", ""));
            Assert.AreEqual(test.AgentID, string.Format("{0,8}", ""));
            Assert.AreEqual(test.IMSRegion, string.Format("{0,4}", ""));

            //Line 9
            Assert.AreEqual(test.ConjTktNumber2, string.Format("{0,10}", ""));
            Assert.AreEqual(test.TktNbr2CouponNbrs, string.Format("{0,4}", ""));
            Assert.AreEqual(test.TktNbr3CouponNbrs, string.Format("{0,4}", ""));
            Assert.AreEqual(test.ConjTktNbr4, string.Format("{0,10}", ""));
            Assert.AreEqual(test.TktNbr4CouponNbrs, string.Format("{0,4}", ""));
            Assert.AreEqual(test.TktNbrAirlineCode, string.Format("   ", ""));

            //Line 10
            Assert.AreEqual(test.RefundType, string.Format("  ", ""));
            Assert.AreEqual(test.NewReissueTktNumber, string.Format("{0,13}", ""));
            Assert.AreEqual(test.OrigTktNbrInclAirlineCode, string.Format("{0,13}", ""));
            Assert.AreEqual(test.ExchTktBaseFareDollars, string.Format("{0,9}", ""));
            Assert.AreEqual(test.ExchTktBaseFareCents, string.Format("{0,2}", ""));

            //Line 11
            Assert.AreEqual(test.TaxType1, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars1, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents1, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType1b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars1b, string.Format("{0,7 }", ""));
            Assert.AreEqual(test.TaxAmountCents1b, string.Format("{0,2   }", ""));
            Assert.AreEqual(test.TaxType1c, string.Format("{0,3  }", ""));
            Assert.AreEqual(test.TaxAmountDollars1c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents1c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType1d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars1d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents1d, string.Format("{0,2}", ""));

            //Line 12		
            Assert.AreEqual(test.TaxType2, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars2, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents2, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType2b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars2b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents2b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType2c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars2c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents2c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType2d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars2d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents2d, string.Format("{0,2}", ""));

            //Line 13
            Assert.AreEqual(test.TaxType3, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars3, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents3, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType3b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars3b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents3b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType3c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars3c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents3c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType3d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars3d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents3d, string.Format("{0,2}", ""));

            //Line 14
            Assert.AreEqual(test.TaxType4, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars4, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents4, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType4b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars4b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents4b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType4c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars4c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents4c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType4d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars4d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents4d, string.Format("{0,2}", ""));


            //Line 15
            Assert.AreEqual(test.NewTktBaseFaredollars, string.Format("{0,9}", ""));
            Assert.AreEqual(test.NewTktBaseFarecents, string.Format("{0,2}", ""));

            //Line 16
            Assert.AreEqual(test.TaxType5, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars5, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents5, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType5b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars5b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents5b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType5c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars5c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents5c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType5d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars5d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents5d, string.Format("{0,2}", ""));

            //Line 17
            Assert.AreEqual(test.TaxType6, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars6, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents6, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType6b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars6b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents6b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType6c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars6c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents6c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType6d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars6d, string.Format("       ", ""));
            Assert.AreEqual(test.TaxAmtCents6d, string.Format("{0,2}", ""));

            //Line 18
            Assert.AreEqual(test.TaxType7, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars7, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents7, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType7b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars7b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents7b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType7c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars7c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents7c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType7d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars7d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents7d, string.Format("{0,2}", ""));

            //Line 19
            Assert.AreEqual(test.TaxType8, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountdollars8, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountcents8, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType8b, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars8b, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents8b, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType8c, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars8c, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmountCents8c, string.Format("{0,2}", ""));
            Assert.AreEqual(test.TaxType8d, string.Format("{0,3}", ""));
            Assert.AreEqual(test.TaxAmountDollars8d, string.Format("{0,7}", ""));
            Assert.AreEqual(test.TaxAmtCents8d, string.Format("{0,2}", ""));


            //Line 20
            Assert.AreEqual(test.AddCollectCCNumber, string.Format("{0,24}", ""));
            Assert.AreEqual(test.AddCollectCCExpDate, string.Format("{0,4}", ""));
            Assert.AreEqual(test.AddCollectCCApprovalCode, string.Format("{0,6}", ""));
            Assert.AreEqual(test.RefundabilityIndicator, string.Format(" ", ""));

            //Line 21
            Assert.AreEqual(test.EmailAddress, string.Format("{0,50}", ""));

            //Line 22
            Assert.AreEqual(test.AutopayIndicator, string.Format("{0,2}", ""));
            Assert.AreEqual(test.RefundAmount, string.Format("{0,11}", ""));


            //Line 23
            Assert.AreEqual(test.GiftCertificateIndicator, string.Format("{0,1}", ""));

            //Line 24
            Assert.AreEqual(test.CertificateNumber, string.Format("{0,12}", ""));
            Assert.AreEqual(test.RedepositErrormessage, string.Format("{0,48}", ""));

            //Line 24
            Assert.AreEqual(test.CertificateNumber2, string.Format("{0,12}", ""));
            Assert.AreEqual(test.RedepositErrormessage2, string.Format("{0,48}", ""));

            //line 25 
            Assert.AreEqual(test.RVCertificateCode, string.Format("{0,20}", ""));
            Assert.AreEqual(test.NonATLInd, string.Format("{0,1}", ""));



			test.Recloc = "AAAAAA";
			Assert.IsTrue(test.Recloc.Contains("AA"));
                              Assert.IsTrue(test.Recloc.Length == 6);

			test.FOPType = "AAAAAA";
			Assert.IsTrue(test.FOPType.Contains("AA"));
                              Assert.IsTrue(test.FOPType.Length == 2);

			test.CCNumber = "AAAAAA";
			Assert.IsTrue(test.CCNumber.Contains("AA"));
                              Assert.IsTrue(test.CCNumber.Length == 24);

			test.TicketNumber = "AAAAAA";
			Assert.IsTrue(test.TicketNumber.Contains("AA"));
                              Assert.IsTrue(test.TicketNumber.Length == 10);

			test.CouponNbrs = "AAAAAA";
			Assert.IsTrue(test.CouponNbrs.Contains("AA"));
                              Assert.IsTrue(test.CouponNbrs.Length == 4);

			test.PsgrName = "AAAAAA";
			Assert.IsTrue(test.PsgrName.Contains("AA"));
                              Assert.IsTrue(test.PsgrName.Length == 30);

			test.MailingName = "AAAAAA";
			Assert.IsTrue(test.MailingName.Contains("AA"));
                              Assert.IsTrue(test.MailingName.Length == 30);

			test.MailingAddress1 = "AAAAAA";
			Assert.IsTrue(test.MailingAddress1.Contains("AA"));
                              Assert.IsTrue(test.MailingAddress1.Length == 30);

			test.MailingAddress2 = "AAAAAA";
			Assert.IsTrue(test.MailingAddress2.Contains("AA"));
                              Assert.IsTrue(test.MailingAddress2.Length == 30);

			test.MailingCity = "AAAAAA";
			Assert.IsTrue(test.MailingCity.Contains("AA"));
                              Assert.IsTrue(test.MailingCity.Length == 26);

			test.MailingState = "AAAAAA";
			Assert.IsTrue(test.MailingState.Contains("AA"));
                              Assert.IsTrue(test.MailingState.Length == 4);

			test.ZipCode = "AAAAAA";
			Assert.IsTrue(test.ZipCode.Contains("AA"));
                              Assert.IsTrue(test.ZipCode.Length == 10);

			test.Country = "AAAAAA";
			Assert.IsTrue(test.Country.Contains("AA"));
                              Assert.IsTrue(test.Country.Length == 11);

			test.RefundWaiverCode = "AAAAAA";
			Assert.IsTrue(test.RefundWaiverCode.Contains("A"));
                              Assert.IsTrue(test.RefundWaiverCode.Length == 1);

			test.InVolIndicator = "AAAAAA";
			Assert.IsTrue(test.InVolIndicator.Contains("A"));
                              Assert.IsTrue(test.InVolIndicator.Length == 1);

			test.RefundDraftNumber = "AAAAAA";
			Assert.IsTrue(test.RefundDraftNumber.Contains("AA"));
                              Assert.IsTrue(test.RefundDraftNumber.Length == 10);

			test.DowngradeIndicator = "AAAAAA";
			Assert.IsTrue(test.DowngradeIndicator.Contains("A"));
                              Assert.IsTrue(test.DowngradeIndicator.Length == 1);

			test.OriginalFareBasis = "AAAAAA";
			Assert.IsTrue(test.OriginalFareBasis.Contains("AA"));
                              Assert.IsTrue(test.OriginalFareBasis.Length == 15);

			test.NewFareBasis = "AAAAAA";
			Assert.IsTrue(test.NewFareBasis.Contains("AA"));
                              Assert.IsTrue(test.NewFareBasis.Length == 15);

			test.OldMPCertificateNbr = "AAAAAA";
			Assert.IsTrue(test.OldMPCertificateNbr.Contains("AA"));
                              Assert.IsTrue(test.OldMPCertificateNbr.Length == 8);

			test.CCTypeForMPRedepositFee = "AAAAAA";
			Assert.IsTrue(test.CCTypeForMPRedepositFee.Contains("AA"));
                              Assert.IsTrue(test.CCTypeForMPRedepositFee.Length == 2);

			test.CCNumberMPRedepositFee = "AAAAAA";
			Assert.IsTrue(test.CCNumberMPRedepositFee.Contains("AA"));
                              Assert.IsTrue(test.CCNumberMPRedepositFee.Length == 20);

			test.CCHolderNameMPRedepoFee = "AAAAAA";
			Assert.IsTrue(test.CCHolderNameMPRedepoFee.Contains("AA"));
                              Assert.IsTrue(test.CCHolderNameMPRedepoFee.Length == 30);

			test.CCExpDateMPRedepositFee = "AAAAAA";
			Assert.IsTrue(test.CCExpDateMPRedepositFee.Contains("AA"));
                              Assert.IsTrue(test.CCExpDateMPRedepositFee.Length == 4);

			test.MPRedepositFeeAmount = "AAAAAA";
			Assert.IsTrue(test.MPRedepositFeeAmount.Contains("AA"));
                              Assert.IsTrue(test.MPRedepositFeeAmount.Length == 8);

			test.CCApprovalCodeMPRedepositFee = "AAAAAA";
			Assert.IsTrue(test.CCApprovalCodeMPRedepositFee.Contains("AA"));
                              Assert.IsTrue(test.CCApprovalCodeMPRedepositFee.Length == 6);

			test.AgentID = "AAAAAA";
			Assert.IsTrue(test.AgentID.Contains("AA"));
                              Assert.IsTrue(test.AgentID.Length == 8);

			test.IMSRegion = "AAAAAA";
			Assert.IsTrue(test.IMSRegion.Contains("AA"));
                              Assert.IsTrue(test.IMSRegion.Length == 4);

			test.ConjTktNumber2 = "AAAAAA";
			Assert.IsTrue(test.ConjTktNumber2.Contains("AA"));
                              Assert.IsTrue(test.ConjTktNumber2.Length == 10);

			test.TktNbr2CouponNbrs = "AAAAAA";
			Assert.IsTrue(test.TktNbr2CouponNbrs.Contains("AA"));
                              Assert.IsTrue(test.TktNbr2CouponNbrs.Length == 4);

			test.ConjTktNumber3 = "AAAAAA";
			Assert.IsTrue(test.ConjTktNumber3.Contains("AA"));
                              Assert.IsTrue(test.ConjTktNumber3.Length == 10);

			test.TktNbr3CouponNbrs = "AAAAAA";
			Assert.IsTrue(test.TktNbr3CouponNbrs.Contains("AA"));
                              Assert.IsTrue(test.TktNbr3CouponNbrs.Length == 4);

			test.ConjTktNbr4 = "AAAAAA";
			Assert.IsTrue(test.ConjTktNbr4.Contains("AA"));
                              Assert.IsTrue(test.ConjTktNbr4.Length == 10);

			test.TktNbr4CouponNbrs = "AAAAAA";
			Assert.IsTrue(test.TktNbr4CouponNbrs.Contains("AA"));
                              Assert.IsTrue(test.TktNbr4CouponNbrs.Length == 4);

			test.TktNbrAirlineCode = "AAAAAA";
			Assert.IsTrue(test.TktNbrAirlineCode.Contains("AA"));
                              Assert.IsTrue(test.TktNbrAirlineCode.Length == 3);

			test.RefundType = "AAAAAA";
			Assert.IsTrue(test.RefundType.Contains("AA"));
                              Assert.IsTrue(test.RefundType.Length == 2);

			test.NewReissueTktNumber = "AAAAAA";
			Assert.IsTrue(test.NewReissueTktNumber.Contains("AA"));
                              Assert.IsTrue(test.NewReissueTktNumber.Length == 13);

			test.OrigTktNbrInclAirlineCode = "AAAAAA";
			Assert.IsTrue(test.OrigTktNbrInclAirlineCode.Contains("AA"));
                              Assert.IsTrue(test.OrigTktNbrInclAirlineCode.Length == 13);

			test.ExchTktBaseFareDollars = "AAAAAA";
			Assert.IsTrue(test.ExchTktBaseFareDollars.Contains("AA"));
                              Assert.IsTrue(test.ExchTktBaseFareDollars.Length == 9);

			test.ExchTktBaseFareCents = "AAAAAA";
			Assert.IsTrue(test.ExchTktBaseFareCents.Contains("AA"));
                              Assert.IsTrue(test.ExchTktBaseFareCents.Length == 2);

			test.TaxType1 = "AAAAAA";
			Assert.IsTrue(test.TaxType1.Contains("AA"));
                              Assert.IsTrue(test.TaxType1.Length == 3);

			test.TaxAmountdollars1 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars1.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars1.Length == 7);

			test.TaxAmountcents1 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents1.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents1.Length == 2);

			test.TaxType1b = "AAAAAA";
			Assert.IsTrue(test.TaxType1b.Contains("AA"));
                              Assert.IsTrue(test.TaxType1b.Length == 3);

			test.TaxAmountDollars1b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars1b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars1b.Length == 7 );

			test.TaxAmountCents1b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents1b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents1b.Length == 2   );

			test.TaxType1c = "AAAAAA";
			Assert.IsTrue(test.TaxType1c.Contains("AA"));
                              Assert.IsTrue(test.TaxType1c.Length == 3  );

			test.TaxAmountDollars1c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars1c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars1c.Length == 7);

			test.TaxAmountCents1c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents1c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents1c.Length == 2);

			test.TaxType1d = "AAAAAA";
			Assert.IsTrue(test.TaxType1d.Contains("AA"));
                              Assert.IsTrue(test.TaxType1d.Length == 3);

			test.TaxAmountDollars1d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars1d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars1d.Length == 7);

			test.TaxAmtCents1d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents1d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents1d.Length == 2);

			test.TaxType2 = "AAAAAA";
			Assert.IsTrue(test.TaxType2.Contains("AA"));
                              Assert.IsTrue(test.TaxType2.Length == 3);

			test.TaxAmountdollars2 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars2.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars2.Length == 7);

			test.TaxAmountcents2 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents2.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents2.Length == 2);

			test.TaxType2b = "AAAAAA";
			Assert.IsTrue(test.TaxType2b.Contains("AA"));
                              Assert.IsTrue(test.TaxType2b.Length == 3);

			test.TaxAmountDollars2b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars2b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars2b.Length == 7);

			test.TaxAmountCents2b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents2b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents2b.Length == 2);

			test.TaxType2c = "AAAAAA";
			Assert.IsTrue(test.TaxType2c.Contains("AA"));
                              Assert.IsTrue(test.TaxType2c.Length == 3);

			test.TaxAmountDollars2c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars2c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars2c.Length == 7);

			test.TaxAmountCents2c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents2c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents2c.Length == 2);

			test.TaxType2d = "AAAAAA";
			Assert.IsTrue(test.TaxType2d.Contains("AA"));
                              Assert.IsTrue(test.TaxType2d.Length == 3);

			test.TaxAmountDollars2d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars2d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars2d.Length == 7);

			test.TaxAmtCents2d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents2d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents2d.Length == 2);

			test.TaxType3 = "AAAAAA";
			Assert.IsTrue(test.TaxType3.Contains("AA"));
                              Assert.IsTrue(test.TaxType3.Length == 3);

			test.TaxAmountdollars3 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars3.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars3.Length == 7);

			test.TaxAmountcents3 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents3.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents3.Length == 2);

			test.TaxType3b = "AAAAAA";
			Assert.IsTrue(test.TaxType3b.Contains("AA"));
                              Assert.IsTrue(test.TaxType3b.Length == 3);

			test.TaxAmountDollars3b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars3b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars3b.Length == 7);

			test.TaxAmountCents3b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents3b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents3b.Length == 2);

			test.TaxType3c = "AAAAAA";
			Assert.IsTrue(test.TaxType3c.Contains("AA"));
                              Assert.IsTrue(test.TaxType3c.Length == 3);

			test.TaxAmountDollars3c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars3c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars3c.Length == 7);

			test.TaxAmountCents3c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents3c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents3c.Length == 2);

			test.TaxType3d = "AAAAAA";
			Assert.IsTrue(test.TaxType3d.Contains("AA"));
                              Assert.IsTrue(test.TaxType3d.Length == 3);

			test.TaxAmountDollars3d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars3d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars3d.Length == 7);

			test.TaxAmtCents3d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents3d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents3d.Length == 2);

			test.TaxType4 = "AAAAAA";
			Assert.IsTrue(test.TaxType4.Contains("AA"));
                              Assert.IsTrue(test.TaxType4.Length == 3);

			test.TaxAmountdollars4 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars4.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars4.Length == 7);

			test.TaxAmountcents4 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents4.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents4.Length == 2);

			test.TaxType4b = "AAAAAA";
			Assert.IsTrue(test.TaxType4b.Contains("AA"));
                              Assert.IsTrue(test.TaxType4b.Length == 3);

			test.TaxAmountDollars4b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars4b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars4b.Length == 7);

			test.TaxAmountCents4b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents4b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents4b.Length == 2);

			test.TaxType4c = "AAAAAA";
			Assert.IsTrue(test.TaxType4c.Contains("AA"));
                              Assert.IsTrue(test.TaxType4c.Length == 3);

			test.TaxAmountDollars4c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars4c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars4c.Length == 7);

			test.TaxAmountCents4c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents4c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents4c.Length == 2);

			test.TaxType4d = "AAAAAA";
			Assert.IsTrue(test.TaxType4d.Contains("AA"));
                              Assert.IsTrue(test.TaxType4d.Length == 3);

			test.TaxAmountDollars4d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars4d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars4d.Length == 7);

			test.TaxAmtCents4d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents4d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents4d.Length == 2);

			test.NewTktBaseFaredollars = "AAAAAA";
			Assert.IsTrue(test.NewTktBaseFaredollars.Contains("AA"));
                              Assert.IsTrue(test.NewTktBaseFaredollars.Length == 9);

			test.NewTktBaseFarecents = "AAAAAA";
			Assert.IsTrue(test.NewTktBaseFarecents.Contains("AA"));
                              Assert.IsTrue(test.NewTktBaseFarecents.Length == 2);

			test.TaxType5 = "AAAAAA";
			Assert.IsTrue(test.TaxType5.Contains("AA"));
                              Assert.IsTrue(test.TaxType5.Length == 3);

			test.TaxAmountdollars5 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars5.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars5.Length == 7);

			test.TaxAmountcents5 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents5.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents5.Length == 2);

			test.TaxType5b = "AAAAAA";
			Assert.IsTrue(test.TaxType5b.Contains("AA"));
                              Assert.IsTrue(test.TaxType5b.Length == 3);

			test.TaxAmountDollars5b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars5b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars5b.Length == 7);

			test.TaxAmountCents5b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents5b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents5b.Length == 2);

			test.TaxType5c = "AAAAAA";
			Assert.IsTrue(test.TaxType5c.Contains("AA"));
                              Assert.IsTrue(test.TaxType5c.Length == 3);

			test.TaxAmountDollars5c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars5c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars5c.Length == 7);

			test.TaxAmountCents5c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents5c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents5c.Length == 2);

			test.TaxType5d = "AAAAAA";
			Assert.IsTrue(test.TaxType5d.Contains("AA"));
                              Assert.IsTrue(test.TaxType5d.Length == 3);

			test.TaxAmountDollars5d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars5d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars5d.Length == 7);

			test.TaxAmtCents5d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents5d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents5d.Length == 2);

			test.TaxType6 = "AAAAAA";
			Assert.IsTrue(test.TaxType6.Contains("AA"));
                              Assert.IsTrue(test.TaxType6.Length == 3);

			test.TaxAmountdollars6 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars6.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars6.Length == 7);

			test.TaxAmountcents6 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents6.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents6.Length == 2);

			test.TaxType6b = "AAAAAA";
			Assert.IsTrue(test.TaxType6b.Contains("AA"));
                              Assert.IsTrue(test.TaxType6b.Length == 3);

			test.TaxAmountDollars6b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars6b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars6b.Length == 7);

			test.TaxAmountCents6b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents6b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents6b.Length == 2);

			test.TaxType6c = "AAAAAA";
			Assert.IsTrue(test.TaxType6c.Contains("AA"));
                              Assert.IsTrue(test.TaxType6c.Length == 3);

			test.TaxAmountDollars6c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars6c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars6c.Length == 7);

			test.TaxAmountCents6c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents6c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents6c.Length == 2);

			test.TaxType6d = "AAAAAA";
			Assert.IsTrue(test.TaxType6d.Contains("AA"));
                              Assert.IsTrue(test.TaxType6d.Length == 3);

			test.TaxAmountDollars6d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars6d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars6d.Length == 7);

			test.TaxAmtCents6d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents6d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents6d.Length == 2);

			test.TaxType7 = "AAAAAA";
			Assert.IsTrue(test.TaxType7.Contains("AA"));
                              Assert.IsTrue(test.TaxType7.Length == 3);

			test.TaxAmountdollars7 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars7.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars7.Length == 7);

			test.TaxAmountcents7 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents7.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents7.Length == 2);

			test.TaxType7b = "AAAAAA";
			Assert.IsTrue(test.TaxType7b.Contains("AA"));
                              Assert.IsTrue(test.TaxType7b.Length == 3);

			test.TaxAmountDollars7b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars7b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars7b.Length == 7);

			test.TaxAmountCents7b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents7b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents7b.Length == 2);

			test.TaxType7c = "AAAAAA";
			Assert.IsTrue(test.TaxType7c.Contains("AA"));
                              Assert.IsTrue(test.TaxType7c.Length == 3);

			test.TaxAmountDollars7c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars7c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars7c.Length == 7);

			test.TaxAmountCents7c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents7c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents7c.Length == 2);

			test.TaxType7d = "AAAAAA";
			Assert.IsTrue(test.TaxType7d.Contains("AA"));
                              Assert.IsTrue(test.TaxType7d.Length == 3);

			test.TaxAmountDollars7d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars7d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars7d.Length == 7);

			test.TaxAmtCents7d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents7d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents7d.Length == 2);

			test.TaxType8 = "AAAAAA";
			Assert.IsTrue(test.TaxType8.Contains("AA"));
                              Assert.IsTrue(test.TaxType8.Length == 3);

			test.TaxAmountdollars8 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountdollars8.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountdollars8.Length == 7);

			test.TaxAmountcents8 = "AAAAAA";
			Assert.IsTrue(test.TaxAmountcents8.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountcents8.Length == 2);

			test.TaxType8b = "AAAAAA";
			Assert.IsTrue(test.TaxType8b.Contains("AA"));
                              Assert.IsTrue(test.TaxType8b.Length == 3);

			test.TaxAmountDollars8b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars8b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars8b.Length == 7);

			test.TaxAmountCents8b = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents8b.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents8b.Length == 2);

			test.TaxType8c = "AAAAAA";
			Assert.IsTrue(test.TaxType8c.Contains("AA"));
                              Assert.IsTrue(test.TaxType8c.Length == 3);

			test.TaxAmountDollars8c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars8c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars8c.Length == 7);

			test.TaxAmountCents8c = "AAAAAA";
			Assert.IsTrue(test.TaxAmountCents8c.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountCents8c.Length == 2);

			test.TaxType8d = "AAAAAA";
			Assert.IsTrue(test.TaxType8d.Contains("AA"));
                              Assert.IsTrue(test.TaxType8d.Length == 3);

			test.TaxAmountDollars8d = "AAAAAA";
			Assert.IsTrue(test.TaxAmountDollars8d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmountDollars8d.Length == 7);

			test.TaxAmtCents8d = "AAAAAA";
			Assert.IsTrue(test.TaxAmtCents8d.Contains("AA"));
                              Assert.IsTrue(test.TaxAmtCents8d.Length == 2);

			test.AddCollectCCNumber = "AAAAAA";
			Assert.IsTrue(test.AddCollectCCNumber.Contains("AA"));
                              Assert.IsTrue(test.AddCollectCCNumber.Length == 24);

			test.AddCollectCCExpDate = "AAAAAA";
			Assert.IsTrue(test.AddCollectCCExpDate.Contains("AA"));
                              Assert.IsTrue(test.AddCollectCCExpDate.Length == 4);

			test.AddCollectCCApprovalCode = "AAAAAA";
			Assert.IsTrue(test.AddCollectCCApprovalCode.Contains("AA"));
                              Assert.IsTrue(test.AddCollectCCApprovalCode.Length == 6);

			test.RefundabilityIndicator = "AAAAAA";
			Assert.IsTrue(test.RefundabilityIndicator.Contains("A"));
                              Assert.IsTrue(test.RefundabilityIndicator.Length == 1);

			test.EmailAddress = "AAAAAA";
			Assert.IsTrue(test.EmailAddress.Contains("AA"));
                              Assert.IsTrue(test.EmailAddress.Length == 50);

			test.AutopayIndicator = "AAAAAA";
			Assert.IsTrue(test.AutopayIndicator.Contains("AA"));
                              Assert.IsTrue(test.AutopayIndicator.Length == 2);

            test.RefundAmount = "";            
            Assert.IsTrue(test.RefundAmount.Length == 11);
            Assert.IsTrue(test.RefundAmount == "           ");
            

			test.RefundAmount = "AAAAAA";
			Assert.IsTrue(test.RefundAmount.Contains("AA"));
                              Assert.IsTrue(test.RefundAmount.Length == 11);

			test.GiftCertificateIndicator = "AAAAAA";
			Assert.IsTrue(test.GiftCertificateIndicator.Contains("A"));
                              Assert.IsTrue(test.GiftCertificateIndicator.Length == 1);

			test.CertificateNumber = "AAAAAA";
            Assert.IsTrue(test.CertificateNumber.Contains("AAAAAA      "));
                              Assert.IsTrue(test.CertificateNumber.Length == 12);

			test.RedepositErrormessage = "AAAAAA";
			Assert.IsTrue(test.RedepositErrormessage.Contains("AA"));
                              Assert.IsTrue(test.RedepositErrormessage.Length == 48);

			test.CertificateNumber2 = "AAAAAA";
			Assert.IsTrue(test.CertificateNumber2.Contains("AA"));
                              Assert.IsTrue(test.CertificateNumber2.Length == 12);

			test.RedepositErrormessage2 = "AAAAAA";
			Assert.IsTrue(test.RedepositErrormessage2.Contains("AA"));
                              Assert.IsTrue(test.RedepositErrormessage2.Length == 48);

			test.RVCertificateCode = "AAAAAA";
			Assert.IsTrue(test.RVCertificateCode.Contains("AA"));
                              Assert.IsTrue(test.RVCertificateCode.Length == 20);

			test.NonATLInd = "AAAAAA";
			Assert.IsTrue(test.NonATLInd.Contains("A"));
                              Assert.IsTrue(test.NonATLInd.Length == 1);


		}

	}
}
