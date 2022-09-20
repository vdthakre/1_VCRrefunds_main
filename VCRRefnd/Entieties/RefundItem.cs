using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public class RefundItem
	{
		protected int _RedepositCode;
		protected string _RedepositMessage;
		protected string _QueueDate;
	
		protected bool _ValidRefundItem;

		protected List<string> _QueueLines;
		//Line 1
		protected string _Recloc; //(6 chars)
		protected string _FOPType; //(2 chars)
		protected string _CCNumber; //(24 chars)
		protected string _TicketNumber; //(10 chars)
		protected string _CouponNbrs; //(4 chars)

		//Line 2
		protected string _PsgrName; //(30 chars)
		protected string _MailingName; //(30 chars)

		//Line 3
		protected string _MailingAddress1; //(30 chars)

		//Line 4
		protected string _MailingAddress2; //(30 chars)
		protected string _MailingCity; //(26 chars)
		protected string _MailingState; //(4 chars)

		//Line 20
		protected string _ZipCode; //(10 chars)
		protected string _Country; //(11 chars)
		protected string _RefundWaiverCode; //(1 char)
		protected string _InVolIndicator; //(1 char)
		protected string _RefundDraftNumber; //(10 chars)
		protected string _DowngradeIndicator; //(1 char)

		//Line 6
		protected string _OriginalFareBasis; //(15 chars)
		protected string _NewFareBasis; //(15 chars)

		//Line 7
		protected string _OldMPCertificateNbr; //(8 chars)
		protected string _CCTypeForMPRedepositFee; //(2 chars)
		protected string _CCNumberMPRedepositFee; //(20 chars)
		protected string _CCHolderNameMPRedepoFee; //(30 chars)

		//Line 8
		protected string _CCExpDateMPRedepositFee; //(4 chars)
		protected string _MPRedepositFeeAmount; //(8 chars)
		protected string _CCApprovalCodeMPRedepositFee; //(6 chars)
		protected string _AgentID; //(8 chars)
		protected string _IMSRegion; //(4 chars)

		//Line 9
		protected string _ConjTktNumber2; //(10 chars)
		protected string _TktNbr2CouponNbrs; //(4 chars)
        protected string _ConjTktNumber3; //(10 chars)
		protected string _TktNbr3CouponNbrs; //(4 chars)
		protected string _ConjTktNbr4; //(10 chars)
		protected string _TktNbr4CouponNbrs; //(4 chars)
		protected string _TktNbrAirlineCode; //(3chars)

		//Line 10
		protected string _RefundType; //(2 chars)
		protected string _NewReissueTktNumber; //(13 chars)
		protected string _OrigTktNbrInclAirlineCode;//(13 chars)
		protected string _ExchTktBaseFareDollars; //(9 chars)
		protected string _ExchTktBaseFareCents; //(2 chars)


		//Line 11
		protected string _TaxType1; //(3 chars)
		protected string _TaxAmountdollars1; //(7 chars)
		protected string _TaxAmountcents1; //(2 chars)
		protected string _TaxType1b; //(3 chars)
		protected string _TaxAmountDollars1b; //(7 chars) 
		protected string _TaxAmountCents1b; //(2 chars)   
		protected string _TaxType1c; //(3 chars)  
		protected string _TaxAmountDollars1c; //(7 chars)
		protected string _TaxAmountCents1c; //(2 chars)
		protected string _TaxType1d; //(3 chars)
		protected string _TaxAmountDollars1d; //(7 chars)
		protected string _TaxAmtCents1d; //(2 chars)

		//Line 12		
		protected string _TaxType2; //(3 chars)
		protected string _TaxAmountdollars2; //(7 chars)
		protected string _TaxAmountcents2; //(2 chars)
		protected string _TaxType2b; //(3 chars)
		protected string _TaxAmountDollars2b; //(7 chars) 
		protected string _TaxAmountCents2b; //(2 chars)   
		protected string _TaxType2c; //(3 chars)  
		protected string _TaxAmountDollars2c; //(7 chars)
		protected string _TaxAmountCents2c; //(2 chars)
		protected string _TaxType2d; //(3 chars)
		protected string _TaxAmountDollars2d; //(7 chars)
		protected string _TaxAmtCents2d; //(2 chars)

		//Line 13
		protected string _TaxType3; //(3 chars)
		protected string _TaxAmountdollars3; //(7 chars)
		protected string _TaxAmountcents3; //(2 chars)
		protected string _TaxType3b; //(3 chars)
		protected string _TaxAmountDollars3b; //(7 chars) 
		protected string _TaxAmountCents3b; //(2 chars)   
		protected string _TaxType3c; //(3 chars)  
		protected string _TaxAmountDollars3c; //(7 chars)
		protected string _TaxAmountCents3c; //(2 chars)
		protected string _TaxType3d; //(3 chars)
		protected string _TaxAmountDollars3d; //(7 chars)
		protected string _TaxAmtCents3d; //(2 chars)

			//Line 14
		protected string _TaxType4; //(3 chars)
		protected string _TaxAmountdollars4; //(7 chars)
		protected string _TaxAmountcents4; //(2 chars)
		protected string _TaxType4b; //(3 chars)
		protected string _TaxAmountDollars4b; //(7 chars) 
		protected string _TaxAmountCents4b; //(2 chars)   
		protected string _TaxType4c; //(3 chars)  
		protected string _TaxAmountDollars4c; //(7 chars)
		protected string _TaxAmountCents4c; //(2 chars)
		protected string _TaxType4d; //(3 chars)
		protected string _TaxAmountDollars4d; //(7 chars)
		protected string _TaxAmtCents4d; //(2 chars)


		//Line 15
		protected string _NewTktBaseFaredollars; //(9 chars)
		protected string _NewTktBaseFarecents; //(2 chars)

		//Line 16
		protected string _TaxType5; //(3 chars)
		protected string _TaxAmountdollars5; //(7 chars)
		protected string _TaxAmountcents5; //(2 chars)
		protected string _TaxType5b; //(3 chars)
		protected string _TaxAmountDollars5b; //(7 chars) 
		protected string _TaxAmountCents5b; //(2 chars)   
		protected string _TaxType5c; //(3 chars)  
		protected string _TaxAmountDollars5c; //(7 chars)
		protected string _TaxAmountCents5c; //(2 chars)
		protected string _TaxType5d; //(3 chars)
		protected string _TaxAmountDollars5d; //(7 chars)
		protected string _TaxAmtCents5d; //(2 chars)

		//Line 17
		protected string _TaxType6; //(3 chars)
		protected string _TaxAmountdollars6; //(7 chars)
		protected string _TaxAmountcents6; //(2 chars)
		protected string _TaxType6b; //(3 chars)
		protected string _TaxAmountDollars6b; //(7 chars) 
		protected string _TaxAmountCents6b; //(2 chars)   
		protected string _TaxType6c; //(3 chars)  
		protected string _TaxAmountDollars6c; //(7 chars)
		protected string _TaxAmountCents6c; //(2 chars)
		protected string _TaxType6d; //(3 chars)
		protected string _TaxAmountDollars6d; //(7 chars)
		protected string _TaxAmtCents6d; //(2 chars)

		//Line 18
		protected string _TaxType7; //(3 chars)
		protected string _TaxAmountdollars7; //(7 chars)
		protected string _TaxAmountcents7; //(2 chars)
		protected string _TaxType7b; //(3 chars)
		protected string _TaxAmountDollars7b; //(7 chars) 
		protected string _TaxAmountCents7b; //(2 chars)   
		protected string _TaxType7c; //(3 chars)  
		protected string _TaxAmountDollars7c; //(7 chars)
		protected string _TaxAmountCents7c; //(2 chars)
		protected string _TaxType7d; //(3 chars)
		protected string _TaxAmountDollars7d; //(7 chars)
		protected string _TaxAmtCents7d; //(2 chars)

		//Line 19
		protected string _TaxType8; //(3 chars)
		protected string _TaxAmountdollars8; //(7 chars)
		protected string _TaxAmountcents8; //(2 chars)
		protected string _TaxType8b; //(3 chars)
		protected string _TaxAmountDollars8b; //(7 chars) 
		protected string _TaxAmountCents8b; //(2 chars)   
		protected string _TaxType8c; //(3 chars)  
		protected string _TaxAmountDollars8c; //(7 chars)
		protected string _TaxAmountCents8c; //(2 chars)
		protected string _TaxType8d; //(3 chars)
		protected string _TaxAmountDollars8d; //(7 chars)
		protected string _TaxAmtCents8d; //(2 chars)


		//Line 20
		protected string _AddCollectCCNumber; //(24 Chars)
		protected string _AddCollectCCExpDate; //(4 Chars)
		protected string _AddCollectCCApprovalCode; //(6 chars)
		protected string _RefundabilityIndicator; //(1 char)

		//Line 21
		protected string _EmailAddress; //(50 Chars)

		//Line 22
		protected string _AutopayIndicator; //(2 chars )
		protected string _RefundAmount; //(11 Chars)

		//Line 23
		protected string _GiftCertificateIndicator; //(1 char)

		//Line 24
		protected string _CertificateNumber; //(12 Char)
		protected string _RedepositErrormessage; //(48 Char)

		//Line 24
		protected string _CertificateNumber2; //(12 Char)
		protected string _RedepositErrormessage2; //(48 Char)

		//Line 25
		protected string _RVCertificateCode; //(20 Char)
		protected string _NonATLInd; //(1 char)

	
		//last line
		protected string _City;
	
		public RefundItem()
		{

			_RedepositCode = 0;
			_RedepositMessage = "";
			_QueueDate = "";

			//Init the 
			_QueueLines = new List<string>(26);
			_ValidRefundItem = false;

            //Line 1
            _Recloc = string.Format("{0,-6}", "");
            _FOPType = string.Format("{0,-2}", "");
            _CCNumber = string.Format("{0,-24}", "");
            _TicketNumber = string.Format("{0,-10}", "");
            _CouponNbrs = string.Format("{0,-4}", "");

            //Line 2
            _PsgrName = string.Format("{0,-30}", "");
            _MailingName = string.Format("{0,-30}", "");

            //Line 3
            _MailingAddress1 = string.Format("{0,-30}", "");

            //Line 4
            _MailingAddress2 = string.Format("{0,-30}", "");
            _MailingCity = string.Format("{0,-26}", "");
            _MailingState = string.Format("{0,-4}", "");

            //Line 20
            _ZipCode = string.Format("{0,-10}", "");
            _Country = string.Format("{0,-11}", "");
            _RefundWaiverCode = string.Format("{0,-1}", "");
            _InVolIndicator = string.Format("{0,-1}", "");
            _RefundDraftNumber = string.Format("{0,-10}", "");
            _DowngradeIndicator = string.Format("{0,-1}", "");

            //Line 6
            _OriginalFareBasis = string.Format("{0,-15}", "");
            _NewFareBasis = string.Format("{0,-15}", "");

            //Line 7
            _OldMPCertificateNbr = string.Format("{0,-8}", "");
            _CCTypeForMPRedepositFee = string.Format("{0,-2}", "");
            _CCNumberMPRedepositFee = string.Format("{0,-20}", "");
            _CCHolderNameMPRedepoFee = string.Format("{0,-30}", "");

            //Line 8
            _CCExpDateMPRedepositFee = string.Format("{0,-4}", "");
            _MPRedepositFeeAmount = string.Format("{0,-8}", "");
            _CCApprovalCodeMPRedepositFee = string.Format("{0,-6}", "");
            _AgentID = string.Format("{0,-8}", "");
            _IMSRegion = string.Format("{0,-4}", "");

            //Line 9
            _ConjTktNumber2 = string.Format("{0,-10}", "");
            _TktNbr2CouponNbrs = string.Format("{0,-4}", "");
            _TktNbr3CouponNbrs = string.Format("{0,-4}", "");
            _ConjTktNbr4 = string.Format("{0,-10}", "");
            _TktNbr4CouponNbrs = string.Format("{0,-4}", "");
            _TktNbrAirlineCode = string.Format("{0,-3}", "");
            _ConjTktNumber3 = string.Format("{0,-10}", "");

            //Line 10
            _RefundType = string.Format("{0,-2}", "");
            _NewReissueTktNumber = string.Format("{0,-13}", "");
            _OrigTktNbrInclAirlineCode = string.Format("{0,-13}", "");
            _ExchTktBaseFareDollars = string.Format("{0,9}", "");
            _ExchTktBaseFareCents = string.Format("{0,-2}", "");

            //Line 11
            _TaxType1 = string.Format("{0,-3}", "");
            _TaxAmountdollars1 = string.Format("{0,7}", "");
            _TaxAmountcents1 = string.Format("{0,-2}", "");
            _TaxType1b = string.Format("{0,-3}", "");
            _TaxAmountDollars1b = string.Format("{0,7 }", "");
            _TaxAmountCents1b = string.Format("{0,-2   }", "");
            _TaxType1c = string.Format("{0,-3  }", "");
            _TaxAmountDollars1c = string.Format("{0,7}", "");
            _TaxAmountCents1c = string.Format("{0,-2}", "");
            _TaxType1d = string.Format("{0,-3}", "");
            _TaxAmountDollars1d = string.Format("{0,7}", "");
            _TaxAmtCents1d = string.Format("{0,-2}", "");

            //Line 12		
            _TaxType2 = string.Format("{0,-3}", "");
            _TaxAmountdollars2 = string.Format("{0,7}", "");
            _TaxAmountcents2 = string.Format("{0,-2}", "");
            _TaxType2b = string.Format("{0,-3}", "");
            _TaxAmountDollars2b = string.Format("{0,7}", "");
            _TaxAmountCents2b = string.Format("{0,-2}", "");
            _TaxType2c = string.Format("{0,-3}", "");
            _TaxAmountDollars2c = string.Format("{0,7}", "");
            _TaxAmountCents2c = string.Format("{0,-2}", "");
            _TaxType2d = string.Format("{0,-3}", "");
            _TaxAmountDollars2d = string.Format("{0,7}", "");
            _TaxAmtCents2d = string.Format("{0,-2}", "");

            //Line 13
            _TaxType3 = string.Format("{0,-3}", "");
            _TaxAmountdollars3 = string.Format("{0,7}", "");
            _TaxAmountcents3 = string.Format("{0,-2}", "");
            _TaxType3b = string.Format("{0,-3}", "");
            _TaxAmountDollars3b = string.Format("{0,7}", "");
            _TaxAmountCents3b = string.Format("{0,-2}", "");
            _TaxType3c = string.Format("{0,-3}", "");
            _TaxAmountDollars3c = string.Format("{0,7}", "");
            _TaxAmountCents3c = string.Format("{0,-2}", "");
            _TaxType3d = string.Format("{0,-3}", "");
            _TaxAmountDollars3d = string.Format("{0,7}", "");
            _TaxAmtCents3d = string.Format("{0,-2}", "");

            //Line 14
            _TaxType4 = string.Format("{0,-3}", "");
            _TaxAmountdollars4 = string.Format("{0,7}", "");
            _TaxAmountcents4 = string.Format("{0,-2}", "");
            _TaxType4b = string.Format("{0,-3}", "");
            _TaxAmountDollars4b = string.Format("{0,7}", "");
            _TaxAmountCents4b = string.Format("{0,-2}", "");
            _TaxType4c = string.Format("{0,-3}", "");
            _TaxAmountDollars4c = string.Format("{0,7}", "");
            _TaxAmountCents4c = string.Format("{0,-2}", "");
            _TaxType4d = string.Format("{0,-3}", "");
            _TaxAmountDollars4d = string.Format("{0,7}", "");
            _TaxAmtCents4d = string.Format("{0,-2}", "");


            //Line 15
            _NewTktBaseFaredollars = string.Format("{0,9}", "");
            _NewTktBaseFarecents = string.Format("{0,-2}", "");

            //Line 16
            _TaxType5 = string.Format("{0,-3}", "");
            _TaxAmountdollars5 = string.Format("{0,7}", "");
            _TaxAmountcents5 = string.Format("{0,-2}", "");
            _TaxType5b = string.Format("{0,-3}", "");
            _TaxAmountDollars5b = string.Format("{0,7}", "");
            _TaxAmountCents5b = string.Format("{0,-2}", "");
            _TaxType5c = string.Format("{0,-3}", "");
            _TaxAmountDollars5c = string.Format("{0,7}", "");
            _TaxAmountCents5c = string.Format("{0,-2}", "");
            _TaxType5d = string.Format("{0,-3}", "");
            _TaxAmountDollars5d = string.Format("{0,7}", "");
            _TaxAmtCents5d = string.Format("{0,-2}", "");

            //Line 17
            _TaxType6 = string.Format("{0,-3}", "");
            _TaxAmountdollars6 = string.Format("{0,7}", "");
            _TaxAmountcents6 = string.Format("{0,-2}", "");
            _TaxType6b = string.Format("{0,-3}", "");
            _TaxAmountDollars6b = string.Format("{0,7}", "");
            _TaxAmountCents6b = string.Format("{0,-2}", "");
            _TaxType6c = string.Format("{0,-3}", "");
            _TaxAmountDollars6c = string.Format("{0,7}", "");
            _TaxAmountCents6c = string.Format("{0,-2}", "");
            _TaxType6d = string.Format("{0,-3}", "");
            _TaxAmountDollars6d = string.Format("{0,7}", "");
            _TaxAmtCents6d = string.Format("{0,-2}", "");

            //Line 18
            _TaxType7 = string.Format("{0,-3}", "");
            _TaxAmountdollars7 = string.Format("{0,7}", "");
            _TaxAmountcents7 = string.Format("{0,-2}", "");
            _TaxType7b = string.Format("{0,-3}", "");
            _TaxAmountDollars7b = string.Format("{0,7}", "");
            _TaxAmountCents7b = string.Format("{0,-2}", "");
            _TaxType7c = string.Format("{0,-3}", "");
            _TaxAmountDollars7c = string.Format("{0,7}", "");
            _TaxAmountCents7c = string.Format("{0,-2}", "");
            _TaxType7d = string.Format("{0,-3}", "");
            _TaxAmountDollars7d = string.Format("{0,7}", "");
            _TaxAmtCents7d = string.Format("{0,-2}", "");

            //Line 19
            _TaxType8 = string.Format("{0,-3}", "");
            _TaxAmountdollars8 = string.Format("{0,7}", "");
            _TaxAmountcents8 = string.Format("{0,-2}", "");
            _TaxType8b = string.Format("{0,-3}", "");
            _TaxAmountDollars8b = string.Format("{0,7}", "");
            _TaxAmountCents8b = string.Format("{0,-2}", "");
            _TaxType8c = string.Format("{0,-3}", "");
            _TaxAmountDollars8c = string.Format("{0,7}", "");
            _TaxAmountCents8c = string.Format("{0,-2}", "");
            _TaxType8d = string.Format("{0,-3}", "");
            _TaxAmountDollars8d = string.Format("{0,7}", "");
            _TaxAmtCents8d = string.Format("{0,-2}", "");


            //Line 20
            _AddCollectCCNumber = string.Format("{0,-24}", "");
            _AddCollectCCExpDate = string.Format("{0,-4}", "");
            _AddCollectCCApprovalCode = string.Format("{0,-6}", "");
            _RefundabilityIndicator = string.Format("{0,-1}", "");

            //Line 21
            _EmailAddress = string.Format("{0,-50}", "");

            //Line 22
            _AutopayIndicator = string.Format("{0,-2}", "");
            _RefundAmount = string.Format("{0,-11}", "");


            //Line 23
            _GiftCertificateIndicator = string.Format("{0,-1}", "");

            //Line 24
            _CertificateNumber = string.Format("{0,-12}", "");
            _RedepositErrormessage = string.Format("{0,-48}", "");

            //Line 24
            _CertificateNumber2 = string.Format("{0,-12}", "");
            _RedepositErrormessage2 = string.Format("{0,-48}", "");

            //line 25 
            _RVCertificateCode = string.Format("{0,-20}", "");
            _NonATLInd = string.Format("{0,-1}", "");


            _City = string.Format("{0,-3}", "");
			//Last line
			_QueueDate = "";
		}

		public string City
		{
			get { return _City; }
            set { _City = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3);}
		}

		public string QueueDate
		{
			get { return _QueueDate; }
			set { _QueueDate = value; }
		}

		public string RedepositMessage
		{
			get { return _RedepositMessage; }
			set { _RedepositMessage = value; }
		}

		public int RedepositCode
		{
			get { return _RedepositCode; }
			set { _RedepositCode = value; }
		}

		public List<string> QueueLines
		{
			get { return _QueueLines; }
			set { _QueueLines = value; }
		}

        //Recloc
        public string Recloc
        {
            get { return _Recloc; }
            set { _Recloc = value.Length <= 6 ? string.Format("{0,-6}", value) : value.Substring(0, 6); }
        }

        public bool ValidRefundItem
        {
            get { return _ValidRefundItem; }
            set { _ValidRefundItem = value; }
        }

        //FOPType
        public string FOPType
        {
            get { return _FOPType; }
            set { _FOPType = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
        }

        public string AddCollectCCExpDate
        {
            get { return _AddCollectCCExpDate; }
            set { _AddCollectCCExpDate = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }

        //CCNumber
        public string CCNumber
        {
            get { return _CCNumber; }
            set { _CCNumber = value.Length <= 24 ? string.Format("{0,-24}", value) : value.Substring(0, 24); }
        }

        //TicketNumber
        public string TicketNumber
        {
            get { return _TicketNumber; }
            set { _TicketNumber = value.Length <= 10 ? string.Format("{0,-10}", value) : value.Substring(0, 10); }
        }

        //CouponNbrs
        public string CouponNbrs
        {
            get { return _CouponNbrs; }
            set { _CouponNbrs = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }


        //PsgrName
        public string PsgrName
        {
            get { return _PsgrName; }
            set { _PsgrName = value.Length <= 30 ? string.Format("{0,-30}", value) : value.Substring(0, 30); }
        }

        //MailingName
        public string MailingName
        {
            get { return _MailingName; }
            set { _MailingName = value.Length <= 30 ? string.Format("{0,-30}", value) : value.Substring(0, 30); }
        }


        //MailingAddress1
        public string MailingAddress1
        {
            get { return _MailingAddress1; }
            set { _MailingAddress1 = value.Length <= 30 ? string.Format("{0,-30}", value) : value.Substring(0, 30); }
        }


        //MailingAddress2
        public string MailingAddress2
        {
            get { return _MailingAddress2; }
            set { _MailingAddress2 = value.Length <= 30 ? string.Format("{0,-30}", value) : value.Substring(0, 30); }
        }

        //MailingCity
        public string MailingCity
        {
            get { return _MailingCity; }
            set { _MailingCity = value.Length <= 26 ? string.Format("{0,-26}", value) : value.Substring(0, 26); }
        }

        //MailingState
        public string MailingState
        {
            get { return _MailingState; }
            set { _MailingState = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }


        //ZipCode
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value.Length <= 10 ? string.Format("{0,-10}", value) : value.Substring(0, 10); }
        }

        //Country
        public string Country
        {
            get { return _Country; }
            set { _Country = value.Length <= 11 ? string.Format("{0,-11}", value) : value.Substring(0, 11); }
        }

        //RefundWaiverCode
        public string RefundWaiverCode
        {
            get { return _RefundWaiverCode; }
            set { _RefundWaiverCode = value.Length <= 1 ? string.Format("{0,-1}", value) : value.Substring(0, 1); }
        }

        //InVolIndicator
        public string InVolIndicator
        {
            get { return _InVolIndicator; }
            set { _InVolIndicator = value.Length <= 1 ? string.Format("{0,-1}", value) : value.Substring(0, 1); }
        }

        //RefundDraftNumber
        public string RefundDraftNumber
        {
            get { return _RefundDraftNumber; }
            set { _RefundDraftNumber = value.Length <= 10 ? string.Format("{0,-10}", value) : value.Substring(0, 10); }
        }

        //DowngradeIndicator
        public string DowngradeIndicator
        {
            get { return _DowngradeIndicator; }
            set { _DowngradeIndicator = value.Length <= 1 ? string.Format("{0,-1}", value) : value.Substring(0, 1); }
        }


        //OriginalFareBasis
        public string OriginalFareBasis
        {
            get { return _OriginalFareBasis; }
            set { _OriginalFareBasis = value.Length <= 15 ? string.Format("{0,-15}", value) : value.Substring(0, 15); }
        }

        //NewFareBasis
        public string NewFareBasis
        {
            get { return _NewFareBasis; }
            set { _NewFareBasis = value.Length <= 15 ? string.Format("{0,-15}", value) : value.Substring(0, 15); }
        }


        //OldMPCertificateNbr
        public string OldMPCertificateNbr
        {
            get { return _OldMPCertificateNbr; }
            set { _OldMPCertificateNbr = value.Length <= 8 ? string.Format("{0,-8}", value) : value.Substring(0, 8); }
        }

        //CCTypeForMPRedepositFee
        public string CCTypeForMPRedepositFee
        {
            get { return _CCTypeForMPRedepositFee; }
            set { _CCTypeForMPRedepositFee = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
        }

        //CCNumberMPRedepositFee
        public string CCNumberMPRedepositFee
        {
            get { return _CCNumberMPRedepositFee; }
            set { _CCNumberMPRedepositFee = value.Length <= 20 ? string.Format("{0,-20}", value) : value.Substring(0, 20); }
        }

        //CCHolderNameMPRedepoFee
        public string CCHolderNameMPRedepoFee
        {
            get { return _CCHolderNameMPRedepoFee; }
            set { _CCHolderNameMPRedepoFee = value.Length <= 30 ? string.Format("{0,-30}", value) : value.Substring(0, 30); }
        }


        //CCExpDateMPRedepositFee
        public string CCExpDateMPRedepositFee
        {
            get { return _CCExpDateMPRedepositFee; }
            set { _CCExpDateMPRedepositFee = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }

        //MPRedepositFeeAmount
        public string MPRedepositFeeAmount
        {
            get { return _MPRedepositFeeAmount; }
            set { _MPRedepositFeeAmount = value.Length <= 8 ? string.Format("{0,-8}", value) : value.Substring(0, 8); }
        }

        //CCApprovalCodeMPRedepositFee
        public string CCApprovalCodeMPRedepositFee
        {
            get { return _CCApprovalCodeMPRedepositFee; }
            set { _CCApprovalCodeMPRedepositFee = value.Length <= 6 ? string.Format("{0,-6}", value) : value.Substring(0, 6); }
        }

        //AgentID
        public string AgentID
        {
            get { return _AgentID; }
            set { _AgentID = value.Length <= 8 ? string.Format("{0,-8}", value) : value.Substring(0, 8); }
        }

        //IMSRegion
        public string IMSRegion
        {
            get { return _IMSRegion; }
            set { _IMSRegion = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }


        //ConjTktNumber2
        public string ConjTktNumber2
        {
            get { return _ConjTktNumber2; }
            set { _ConjTktNumber2 = value.Length <= 10 ? string.Format("{0,-10}", value) : value.Substring(0, 10); }
        }


        public string ConjTktNumber3
        {
            get { return _ConjTktNumber3; }
            set { _ConjTktNumber3 = value.Length <= 10 ? string.Format("{0,-10}", value) : value.Substring(0, 10); }
        }

        //Tkt
        public string TktNbr2CouponNbrs
        {
            get { return _TktNbr2CouponNbrs; }
            set { _TktNbr2CouponNbrs = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }


        //Tkt
        public string TktNbr3CouponNbrs
        {
            get { return _TktNbr3CouponNbrs; }
            set { _TktNbr3CouponNbrs = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }

        //ConjTkt
        public string ConjTktNbr4
        {
            get { return _ConjTktNbr4; }
            set { _ConjTktNbr4 = value.Length <= 10 ? string.Format("{0,-10}", value) : value.Substring(0, 10); }
        }

        //Tkt
        public string TktNbr4CouponNbrs
        {
            get { return _TktNbr4CouponNbrs; }
            set { _TktNbr4CouponNbrs = value.Length <= 4 ? string.Format("{0,-4}", value) : value.Substring(0, 4); }
        }

        //Tkt
        public string TktNbrAirlineCode
        {
            get { return _TktNbrAirlineCode; }
            set { _TktNbrAirlineCode = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
		}



            //RefundType
            public string RefundType
            {
                get { return _RefundType; }
                set { _RefundType = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //NewReissueTktNumber
            public string NewReissueTktNumber
            {
                get { return _NewReissueTktNumber; }
                set { _NewReissueTktNumber = value.Length <= 13 ? string.Format("{0,-13}", value) : value.Substring(0, 13); }
            }

            //OrigTkt
            public string OrigTktNbrInclAirlineCode
            {
                get { return _OrigTktNbrInclAirlineCode; }
                set { _OrigTktNbrInclAirlineCode = value.Length <= 13 ? string.Format("{0,-13}", value) : value.Substring(0, 13); }
            }

            //ExchTktBaseFareDollars
            public string ExchTktBaseFareDollars
            {
                get { return _ExchTktBaseFareDollars; }
                set { _ExchTktBaseFareDollars = value.Length <= 9 ? string.Format("{0,-9}", value) : value.Substring(0, 9); }
            }

            //ExchTktBaseFareCents
            public string ExchTktBaseFareCents
            {
                get { return _ExchTktBaseFareCents; }
                set { _ExchTktBaseFareCents = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }



            //AddCollectCCNumber
            public string AddCollectCCNumber
            {
                get { return _AddCollectCCNumber; }
                set { _AddCollectCCNumber = value.Length <= 24 ? string.Format("{0,-24}", value) : value.Substring(0, 24); }
            }


            //AddCollectCCApprovalCode
            public string AddCollectCCApprovalCode
            {
                get { return _AddCollectCCApprovalCode; }
                set { _AddCollectCCApprovalCode = value.Length <= 6 ? string.Format("{0,-6}", value) : value.Substring(0, 6); }
            }

            //RefundabilityIndicator
            public string RefundabilityIndicator
            {
                get { return _RefundabilityIndicator; }
                set { _RefundabilityIndicator = value.Length <= 1 ? string.Format("{0,-1}", value) : value.Substring(0, 1); }
            }



            //Line 11
            //TaxType1
            public string TaxType1
            {
                get { return _TaxType1; }
                set { _TaxType1 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars1
            public string TaxAmountdollars1
            {
                get { return _TaxAmountdollars1; }
                set { _TaxAmountdollars1 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents1
            public string TaxAmountcents1
            {
                get { return _TaxAmountcents1; }
                set { _TaxAmountcents1 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType1b
            public string TaxType1b
            {
                get { return _TaxType1b; }
                set { _TaxType1b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars1b
            public string TaxAmountDollars1b
            {
                get { return _TaxAmountDollars1b; }
                set { _TaxAmountDollars1b = value.Length <= 7 ? string.Format("{0,-7 }", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents1b
            public string TaxAmountCents1b
            {
                get { return _TaxAmountCents1b; }
                set { _TaxAmountCents1b = value.Length <= 2 ? string.Format("{0,-2   }", value) : value.Substring(0, 2); }
            }

            //TaxType1c
            public string TaxType1c
            {
                get { return _TaxType1c; }
                set { _TaxType1c = value.Length <= 3 ? string.Format("{0,-3  }", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars1c
            public string TaxAmountDollars1c
            {
                get { return _TaxAmountDollars1c; }
                set { _TaxAmountDollars1c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents1c
            public string TaxAmountCents1c
            {
                get { return _TaxAmountCents1c; }
                set { _TaxAmountCents1c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType1d
            public string TaxType1d
            {
                get { return _TaxType1d; }
                set { _TaxType1d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars1d
            public string TaxAmountDollars1d
            {
                get { return _TaxAmountDollars1d; }
                set { _TaxAmountDollars1d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents1d
            public string TaxAmtCents1d
            {
                get { return _TaxAmtCents1d; }
                set { _TaxAmtCents1d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 12		
            //TaxType2
            public string TaxType2
            {
                get { return _TaxType2; }
                set { _TaxType2 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars2
            public string TaxAmountdollars2
            {
                get { return _TaxAmountdollars2; }
                set { _TaxAmountdollars2 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents2
            public string TaxAmountcents2
            {
                get { return _TaxAmountcents2; }
                set { _TaxAmountcents2 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType2b
            public string TaxType2b
            {
                get { return _TaxType2b; }
                set { _TaxType2b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars2b
            public string TaxAmountDollars2b
            {
                get { return _TaxAmountDollars2b; }
                set { _TaxAmountDollars2b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents2b
            public string TaxAmountCents2b
            {
                get { return _TaxAmountCents2b; }
                set { _TaxAmountCents2b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType2c
            public string TaxType2c
            {
                get { return _TaxType2c; }
                set { _TaxType2c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars2c
            public string TaxAmountDollars2c
            {
                get { return _TaxAmountDollars2c; }
                set { _TaxAmountDollars2c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents2c
            public string TaxAmountCents2c
            {
                get { return _TaxAmountCents2c; }
                set { _TaxAmountCents2c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType2d
            public string TaxType2d
            {
                get { return _TaxType2d; }
                set { _TaxType2d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars2d
            public string TaxAmountDollars2d
            {
                get { return _TaxAmountDollars2d; }
                set { _TaxAmountDollars2d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents2d
            public string TaxAmtCents2d
            {
                get { return _TaxAmtCents2d; }
                set { _TaxAmtCents2d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 13
            //TaxType3
            public string TaxType3
            {
                get { return _TaxType3; }
                set { _TaxType3 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars3
            public string TaxAmountdollars3
            {
                get { return _TaxAmountdollars3; }
                set { _TaxAmountdollars3 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents3
            public string TaxAmountcents3
            {
                get { return _TaxAmountcents3; }
                set { _TaxAmountcents3 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType3b
            public string TaxType3b
            {
                get { return _TaxType3b; }
                set { _TaxType3b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars3b
            public string TaxAmountDollars3b
            {
                get { return _TaxAmountDollars3b; }
                set { _TaxAmountDollars3b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents3b
            public string TaxAmountCents3b
            {
                get { return _TaxAmountCents3b; }
                set { _TaxAmountCents3b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType3c
            public string TaxType3c
            {
                get { return _TaxType3c; }
                set { _TaxType3c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars3c
            public string TaxAmountDollars3c
            {
                get { return _TaxAmountDollars3c; }
                set { _TaxAmountDollars3c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents3c
            public string TaxAmountCents3c
            {
                get { return _TaxAmountCents3c; }
                set { _TaxAmountCents3c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType3d
            public string TaxType3d
            {
                get { return _TaxType3d; }
                set { _TaxType3d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars3d
            public string TaxAmountDollars3d
            {
                get { return _TaxAmountDollars3d; }
                set { _TaxAmountDollars3d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents3d
            public string TaxAmtCents3d
            {
                get { return _TaxAmtCents3d; }
                set { _TaxAmtCents3d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 14
            //TaxType4
            public string TaxType4
            {
                get { return _TaxType4; }
                set { _TaxType4 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars4
            public string TaxAmountdollars4
            {
                get { return _TaxAmountdollars4; }
                set { _TaxAmountdollars4 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents4
            public string TaxAmountcents4
            {
                get { return _TaxAmountcents4; }
                set { _TaxAmountcents4 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType4b
            public string TaxType4b
            {
                get { return _TaxType4b; }
                set { _TaxType4b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars4b
            public string TaxAmountDollars4b
            {
                get { return _TaxAmountDollars4b; }
                set { _TaxAmountDollars4b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents4b
            public string TaxAmountCents4b
            {
                get { return _TaxAmountCents4b; }
                set { _TaxAmountCents4b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType4c
            public string TaxType4c
            {
                get { return _TaxType4c; }
                set { _TaxType4c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars4c
            public string TaxAmountDollars4c
            {
                get { return _TaxAmountDollars4c; }
                set { _TaxAmountDollars4c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents4c
            public string TaxAmountCents4c
            {
                get { return _TaxAmountCents4c; }
                set { _TaxAmountCents4c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType4d
            public string TaxType4d
            {
                get { return _TaxType4d; }
                set { _TaxType4d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars4d
            public string TaxAmountDollars4d
            {
                get { return _TaxAmountDollars4d; }
                set { _TaxAmountDollars4d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents4d
            public string TaxAmtCents4d
            {
                get { return _TaxAmtCents4d; }
                set { _TaxAmtCents4d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }



            //Line 15
            //NewTktBaseFaredollars
            public string NewTktBaseFaredollars
            {
                get { return _NewTktBaseFaredollars; }
                set { _NewTktBaseFaredollars = value.Length <= 9 ? string.Format("{0,-9}", value) : value.Substring(0, 9); }
            }

            //NewTktBaseFarecents
            public string NewTktBaseFarecents
            {
                get { return _NewTktBaseFarecents; }
                set { _NewTktBaseFarecents = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 16
            //TaxType5
            public string TaxType5
            {
                get { return _TaxType5; }
                set { _TaxType5 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars5
            public string TaxAmountdollars5
            {
                get { return _TaxAmountdollars5; }
                set { _TaxAmountdollars5 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents5
            public string TaxAmountcents5
            {
                get { return _TaxAmountcents5; }
                set { _TaxAmountcents5 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType5b
            public string TaxType5b
            {
                get { return _TaxType5b; }
                set { _TaxType5b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars5b
            public string TaxAmountDollars5b
            {
                get { return _TaxAmountDollars5b; }
                set { _TaxAmountDollars5b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents5b
            public string TaxAmountCents5b
            {
                get { return _TaxAmountCents5b; }
                set { _TaxAmountCents5b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType5c
            public string TaxType5c
            {
                get { return _TaxType5c; }
                set { _TaxType5c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars5c
            public string TaxAmountDollars5c
            {
                get { return _TaxAmountDollars5c; }
                set { _TaxAmountDollars5c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents5c
            public string TaxAmountCents5c
            {
                get { return _TaxAmountCents5c; }
                set { _TaxAmountCents5c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType5d
            public string TaxType5d
            {
                get { return _TaxType5d; }
                set { _TaxType5d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars5d
            public string TaxAmountDollars5d
            {
                get { return _TaxAmountDollars5d; }
                set { _TaxAmountDollars5d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents5d
            public string TaxAmtCents5d
            {
                get { return _TaxAmtCents5d; }
                set { _TaxAmtCents5d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 17
            //TaxType6
            public string TaxType6
            {
                get { return _TaxType6; }
                set { _TaxType6 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars6
            public string TaxAmountdollars6
            {
                get { return _TaxAmountdollars6; }
                set { _TaxAmountdollars6 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents6
            public string TaxAmountcents6
            {
                get { return _TaxAmountcents6; }
                set { _TaxAmountcents6 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType6b
            public string TaxType6b
            {
                get { return _TaxType6b; }
                set { _TaxType6b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars6b
            public string TaxAmountDollars6b
            {
                get { return _TaxAmountDollars6b; }
                set { _TaxAmountDollars6b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents6b
            public string TaxAmountCents6b
            {
                get { return _TaxAmountCents6b; }
                set { _TaxAmountCents6b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType6c
            public string TaxType6c
            {
                get { return _TaxType6c; }
                set { _TaxType6c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars6c
            public string TaxAmountDollars6c
            {
                get { return _TaxAmountDollars6c; }
                set { _TaxAmountDollars6c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents6c
            public string TaxAmountCents6c
            {
                get { return _TaxAmountCents6c; }
                set { _TaxAmountCents6c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType6d
            public string TaxType6d
            {
                get { return _TaxType6d; }
                set { _TaxType6d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars6d
            public string TaxAmountDollars6d
            {
                get { return _TaxAmountDollars6d; }
                set { _TaxAmountDollars6d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents6d
            public string TaxAmtCents6d
            {
                get { return _TaxAmtCents6d; }
                set { _TaxAmtCents6d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 18
            //TaxType7
            public string TaxType7
            {
                get { return _TaxType7; }
                set { _TaxType7 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars7
            public string TaxAmountdollars7
            {
                get { return _TaxAmountdollars7; }
                set { _TaxAmountdollars7 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents7
            public string TaxAmountcents7
            {
                get { return _TaxAmountcents7; }
                set { _TaxAmountcents7 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType7b
            public string TaxType7b
            {
                get { return _TaxType7b; }
                set { _TaxType7b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars7b
            public string TaxAmountDollars7b
            {
                get { return _TaxAmountDollars7b; }
                set { _TaxAmountDollars7b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents7b
            public string TaxAmountCents7b
            {
                get { return _TaxAmountCents7b; }
                set { _TaxAmountCents7b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType7c
            public string TaxType7c
            {
                get { return _TaxType7c; }
                set { _TaxType7c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars7c
            public string TaxAmountDollars7c
            {
                get { return _TaxAmountDollars7c; }
                set { _TaxAmountDollars7c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents7c
            public string TaxAmountCents7c
            {
                get { return _TaxAmountCents7c; }
                set { _TaxAmountCents7c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType7d
            public string TaxType7d
            {
                get { return _TaxType7d; }
                set { _TaxType7d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars7d
            public string TaxAmountDollars7d
            {
                get { return _TaxAmountDollars7d; }
                set { _TaxAmountDollars7d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents7d
            public string TaxAmtCents7d
            {
                get { return _TaxAmtCents7d; }
                set { _TaxAmtCents7d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }


            //Line 19
            //TaxType8
            public string TaxType8
            {
                get { return _TaxType8; }
                set { _TaxType8 = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountdollars8
            public string TaxAmountdollars8
            {
                get { return _TaxAmountdollars8; }
                set { _TaxAmountdollars8 = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountcents8
            public string TaxAmountcents8
            {
                get { return _TaxAmountcents8; }
                set { _TaxAmountcents8 = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType8b
            public string TaxType8b
            {
                get { return _TaxType8b; }
                set { _TaxType8b = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars8b
            public string TaxAmountDollars8b
            {
                get { return _TaxAmountDollars8b; }
                set { _TaxAmountDollars8b = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents8b
            public string TaxAmountCents8b
            {
                get { return _TaxAmountCents8b; }
                set { _TaxAmountCents8b = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType8c
            public string TaxType8c
            {
                get { return _TaxType8c; }
                set { _TaxType8c = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars8c
            public string TaxAmountDollars8c
            {
                get { return _TaxAmountDollars8c; }
                set { _TaxAmountDollars8c = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmountCents8c
            public string TaxAmountCents8c
            {
                get { return _TaxAmountCents8c; }
                set { _TaxAmountCents8c = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //TaxType8d
            public string TaxType8d
            {
                get { return _TaxType8d; }
                set { _TaxType8d = value.Length <= 3 ? string.Format("{0,-3}", value) : value.Substring(0, 3); }
            }

            //TaxAmountDollars8d
            public string TaxAmountDollars8d
            {
                get { return _TaxAmountDollars8d; }
                set { _TaxAmountDollars8d = value.Length <= 7 ? string.Format("{0,-7}", value) : value.Substring(0, 7); }
            }

            //TaxAmtCents8d
            public string TaxAmtCents8d
            {
                get { return _TaxAmtCents8d; }
                set { _TaxAmtCents8d = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }



            //EmailAddress
            public string EmailAddress
            {
                get { return _EmailAddress; }
                set { _EmailAddress = value.Length <= 50 ? string.Format("{0,-50}", value) : value.Substring(0, 50); }
            }


            //AutopayIndicator
            public string AutopayIndicator
            {
                get { return _AutopayIndicator; }
                set { _AutopayIndicator = value.Length <= 2 ? string.Format("{0,-2}", value) : value.Substring(0, 2); }
            }

            //RefundAmount
            public string RefundAmount
            {
                get { return _RefundAmount; }
                set { _RefundAmount = value.Length <= 11 ? string.Format("{0,-11}", value) : value.Substring(0, 11); }
            }


            //GiftCertificateIndicator
            public string GiftCertificateIndicator
            {
                get { return _GiftCertificateIndicator; }
                set { _GiftCertificateIndicator = value.Length <= 1 ? string.Format("{0,-1}", value) : value.Substring(0, 1); }
            }


            //CertificateNumber
            public string CertificateNumber
            {
                get { return _CertificateNumber; }
                set { _CertificateNumber = value.Length <= 12 ? string.Format("{0,-12}", value) : value.Substring(0, 12); }
            }

            //RedepositErrormessage
            public string RedepositErrormessage
            {
                get { return _RedepositErrormessage; }
                set { _RedepositErrormessage = value.Length <= 48 ? string.Format("{0,-48}", value) : value.Substring(0, 48); }
            }

            //CertificateNumber2
            public string CertificateNumber2
            {
                get { return _CertificateNumber2; }
                set { _CertificateNumber2 = value.Length <= 12 ? string.Format("{0,-12}", value) : value.Substring(0, 12); }
            }

            //RedepositErrormessage
            public string RedepositErrormessage2
            {
                get { return _RedepositErrormessage2; }
                set { _RedepositErrormessage2 = value.Length <= 48 ? string.Format("{0,-48}", value) : value.Substring(0, 48); }
            }

            public string NonATLInd
            {
                get { return _NonATLInd; }
                set { _NonATLInd = value.Length <= 1 ? string.Format("{0,-1}", value) : value.Substring(0, 1); }
            }



		public string RVCertificateCode
		{
			get { return _RVCertificateCode; }
			set { _RVCertificateCode = value.Length <= 20 ? string.Format("{0,-20}", value) : value.Substring(0, 20); }
		}

	}
}
