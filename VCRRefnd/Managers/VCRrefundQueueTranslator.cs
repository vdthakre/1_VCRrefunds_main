using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VCRrefunds
{
	public class VCRrefundQueueTranslator : IVCRrefundQueueTranslator
	{
		public const int LAST_LINE_INDEX = 26;

		#region Internal Variables
		
		protected RefundAppVariables _AppVariables;

		#endregion

		#region Constructors

		//We don't want to create one of this without having all the info
		private VCRrefundQueueTranslator()
		{

		}

		public VCRrefundQueueTranslator(RefundAppVariables InitVariables)
		{
			_AppVariables = InitVariables;
		}

		#endregion

        public bool UpdateRefundItemLines(RefundItem item)
        {
            bool result = false;

            //if we don't have an item...
            if (item == null)
                return result;

            //Let's clean the lines.. and start fresh
            item.QueueLines = new List<string>(26);

            //Hmmmmm.... here we build the lines
            item.QueueLines.Add("ET1*" + item.Recloc + item.FOPType + item.CCNumber + item.TicketNumber + item.CouponNbrs);
            item.QueueLines.Add("ET2*" + item.PsgrName + item.MailingName);
            item.QueueLines.Add("ET3*" + item.MailingAddress1);
            item.QueueLines.Add("ET4*" + item.MailingAddress2 + item.MailingCity + item.MailingState);
            item.QueueLines.Add("ET5*" + item.ZipCode + item.Country + item.RefundWaiverCode + item.InVolIndicator + item.RefundDraftNumber + item.DowngradeIndicator);
            item.QueueLines.Add("ET6*" + item.OriginalFareBasis + item.NewFareBasis);
            item.QueueLines.Add("ET7*" + item.OldMPCertificateNbr + item.CCTypeForMPRedepositFee + item.CCNumberMPRedepositFee + item.CCHolderNameMPRedepoFee);
            item.QueueLines.Add("ET8*" + item.CCExpDateMPRedepositFee + item.MPRedepositFeeAmount + item.CCApprovalCodeMPRedepositFee + item.AgentID + item.IMSRegion);
            item.QueueLines.Add("ET9*" + item.ConjTktNumber2 + item.TktNbr2CouponNbrs + item.ConjTktNumber3 + item.TktNbr3CouponNbrs + item.ConjTktNbr4 + item.TktNbr4CouponNbrs + item.TktNbrAirlineCode);
            item.QueueLines.Add("ET10*" + item.RefundType + item.NewReissueTktNumber + item.OrigTktNbrInclAirlineCode + item.ExchTktBaseFareDollars + item.ExchTktBaseFareCents);
            item.QueueLines.Add("ET11*" + item.TaxType1 + item.TaxAmountdollars1 + item.TaxAmountcents1 + item.TaxType1b + item.TaxAmountDollars1b + item.TaxAmountCents1b + item.TaxType1c + item.TaxAmountDollars1c + item.TaxAmountCents1c + item.TaxType1d + item.TaxAmountDollars1d + item.TaxAmtCents1d);
            item.QueueLines.Add("ET12*" + item.TaxType2 + item.TaxAmountdollars2 + item.TaxAmountcents2 + item.TaxType2b + item.TaxAmountDollars2b + item.TaxAmountCents2b + item.TaxType2c + item.TaxAmountDollars2c + item.TaxAmountCents2c + item.TaxType2d + item.TaxAmountDollars2d + item.TaxAmtCents2d);
            item.QueueLines.Add("ET13*" + item.TaxType3 + item.TaxAmountdollars3 + item.TaxAmountcents3 + item.TaxType3b + item.TaxAmountDollars3b + item.TaxAmountCents3b + item.TaxType3c + item.TaxAmountDollars3c + item.TaxAmountCents3c + item.TaxType3d + item.TaxAmountDollars3d + item.TaxAmtCents3d);
            item.QueueLines.Add("ET14*" + item.TaxType4 + item.TaxAmountdollars4 + item.TaxAmountcents4 + item.TaxType4b + item.TaxAmountDollars4b + item.TaxAmountCents4b + item.TaxType4c + item.TaxAmountDollars4c + item.TaxAmountCents4c + item.TaxType4d + item.TaxAmountDollars4d + item.TaxAmtCents4d);
            item.QueueLines.Add("ET15*" + item.NewTktBaseFaredollars + item.NewTktBaseFarecents);
            item.QueueLines.Add("ET16*" + item.TaxType5 + item.TaxAmountdollars5 + item.TaxAmountcents5 + item.TaxType5b + item.TaxAmountDollars5b + item.TaxAmountCents5b + item.TaxType5c + item.TaxAmountDollars5c + item.TaxAmountCents5c + item.TaxType5d + item.TaxAmountDollars5d + item.TaxAmtCents5d);
            item.QueueLines.Add("ET17*" + item.TaxType6 + item.TaxAmountdollars6 + item.TaxAmountcents6 + item.TaxType6b + item.TaxAmountDollars6b + item.TaxAmountCents6b + item.TaxType6c + item.TaxAmountDollars6c + item.TaxAmountCents6c + item.TaxType6d + item.TaxAmountDollars6d + item.TaxAmtCents6d);
            item.QueueLines.Add("ET18*" + item.TaxType7 + item.TaxAmountdollars7 + item.TaxAmountcents7 + item.TaxType7b + item.TaxAmountDollars7b + item.TaxAmountCents7b + item.TaxType7c + item.TaxAmountDollars7c + item.TaxAmountCents7c + item.TaxType7d + item.TaxAmountDollars7d + item.TaxAmtCents7d);
            item.QueueLines.Add("ET19*" + item.TaxType8 + item.TaxAmountdollars8 + item.TaxAmountcents8 + item.TaxType8b + item.TaxAmountDollars8b + item.TaxAmountCents8b + item.TaxType8c + item.TaxAmountDollars8c + item.TaxAmountCents8c + item.TaxType8d + item.TaxAmountDollars8d + item.TaxAmtCents8d);
            item.QueueLines.Add("ET20*" + item.AddCollectCCNumber + item.AddCollectCCExpDate + item.AddCollectCCApprovalCode + item.RefundabilityIndicator);
            item.QueueLines.Add("ET21*" + ReplaceInvalidCrsEmailChar(item.EmailAddress));
            item.QueueLines.Add("ET22*" + item.AutopayIndicator + item.RefundAmount);
            item.QueueLines.Add("ET23*" + item.GiftCertificateIndicator);
            item.QueueLines.Add("ET24*" + item.CertificateNumber + item.RedepositErrormessage);
            item.QueueLines.Add("ET25*" + item.CertificateNumber2 + item.RedepositErrormessage2);
            item.QueueLines.Add("ET26*" + item.RVCertificateCode + item.NonATLInd);


            result = true;

            return result;
        }

        public string ReplaceInvalidCrsEmailChar(string emailAddress)
        {
            string validEmail = emailAddress;

            validEmail = validEmail.Replace("@", "/");
            validEmail = validEmail.Replace("!", "<");
            validEmail = validEmail.Replace("%", ">");
            validEmail = validEmail.Replace("_", ",");

            return validEmail;
        }


        protected void AddQueueLine( int lineNumber, string content, List<string> list)
        {

            
            //check for nulls
            if (content == null)
                return;
            if (list == null)
                return;
            
            //No need for negative numbers
            if (lineNumber < 0)
                return;

            if (lineNumber > list.Count - 1)
                return;

            //things look OK then
            list[lineNumber] = content;


        }

		public bool TranslateToAccountingFormat(RefundItem item, ref string resultString)
		{
			bool result = false;
			StringBuilder theBuilder = new StringBuilder();
			string email = "";            

			//if we don't have an item...
			if( item == null)
				return result;

			//if we don't have all the lines...
			if( item.QueueLines.Count < 23)
				return result;

			//If we don't have the stock number the we add it 
			if(item.TktNbrAirlineCode.Trim().Length == 0)
			{
				item.TktNbrAirlineCode = "027";				
			}

			//We put the stock number in front of the ticket number and remove 3 chars in the back
			theBuilder.Append(item.QueueLines[0].Substring(0, 32) + item.TktNbrAirlineCode + item.QueueLines[0].Substring(32, item.QueueLines[0].Length - 32  - item.TktNbrAirlineCode.Length));
			
			theBuilder.Append(item.QueueLines[1]);
			theBuilder.Append(item.QueueLines[2]);
			theBuilder.Append(item.QueueLines[3]);
			theBuilder.Append(item.QueueLines[4]);
			theBuilder.Append(item.QueueLines[5]);
			theBuilder.Append(item.QueueLines[9]);
			theBuilder.Append(item.QueueLines[10]);
			theBuilder.Append(item.QueueLines[11]);
			theBuilder.Append(item.QueueLines[12]);
			theBuilder.Append(item.QueueLines[13]);
			theBuilder.Append(item.QueueLines[14]);
			theBuilder.Append(item.QueueLines[15]);
			theBuilder.Append(item.QueueLines[16]);
			theBuilder.Append(item.QueueLines[17]);
			theBuilder.Append(item.QueueLines[18]);
			theBuilder.Append(item.QueueLines[19]);
			email = item.QueueLines[20];

			//Replace sabre symbols 
			email = email.Replace("/", "@");
			email = email.Replace("<", "!");
			email = email.Replace(">", "%");
			email = email.Replace(",", "_");

			theBuilder.Append(email);

			theBuilder.Append(item.QueueLines[21]);			
			theBuilder.Append(item.QueueLines[22]);
			theBuilder.Append(item.QueueLines[25]);


			//We are done building it
			resultString = theBuilder.ToString();

			//we made it this far things might be OK
			result = true;

			return result;
		}

		public List<string> TranslateToAccountingFormatConjunctive(RefundItem item)
		{
			List<string> result = new List<string>();
			StringBuilder theBuilder = new StringBuilder();
			string email = "";
			string paddedTicket = "";

			//if we don't have an item...
			if(item == null)
				return result;

			//if we don't have all the lines...
			if(item.QueueLines.Count < 23)
				return result;

			//If we don't have the stock number the we add it 
			if(item.TktNbrAirlineCode.Trim().Length == 0)
			{
				item.TktNbrAirlineCode = "027";
			}

			//The conjunctive tickets are in line 9
            string[] pieces = { item.ConjTktNumber2 + item.TktNbr2CouponNbrs, item.ConjTktNumber3 + item.TktNbr3CouponNbrs, item.ConjTktNbr4 + item.TktNbr4CouponNbrs };

			foreach(string tmp in pieces)
			{
				
				//Get the padded ticket
				paddedTicket = AppendStringKeepLength(tmp.Trim(), "              ");

				//Just in case make sure we don't have an empty string
				if(paddedTicket.Trim().Length > 0)
				{
					theBuilder = new StringBuilder();
					//We put the stock number in front of the ticket number and remove 3 chars in the back
					theBuilder.Append(item.QueueLines[0].Substring(0, 32) + item.TktNbrAirlineCode + paddedTicket);

					theBuilder.Append(item.QueueLines[1]);
					theBuilder.Append(item.QueueLines[2]);
					theBuilder.Append(item.QueueLines[3]);
					theBuilder.Append(item.QueueLines[4]);
					theBuilder.Append(item.QueueLines[5]);
					theBuilder.Append(item.QueueLines[9]);
					theBuilder.Append(item.QueueLines[10]);
					theBuilder.Append(item.QueueLines[11]);
					theBuilder.Append(item.QueueLines[12]);
					theBuilder.Append(item.QueueLines[13]);
					theBuilder.Append(item.QueueLines[14]);
					theBuilder.Append(item.QueueLines[15]);
					theBuilder.Append(item.QueueLines[16]);
					theBuilder.Append(item.QueueLines[17]);
					theBuilder.Append(item.QueueLines[18]);
					theBuilder.Append(item.QueueLines[19]);
					email = item.QueueLines[20];

					//Replace sabre symbols 
					email = email.Replace("/", "@");
					email = email.Replace("<", "!");
					email = email.Replace(">", "%");
					email = email.Replace(",", "_");

					theBuilder.Append(email);

					theBuilder.Append(item.QueueLines[21]);
					theBuilder.Append(item.QueueLines[22]);
					theBuilder.Append(item.QueueLines[25]);


					//We are done building it
					result.Add(theBuilder.ToString());
				}
			}

			return result;
		}
	
		public RefundItem TranslateAcoountingQueueRefund(List<string> sabreResult)
		{
			RefundItem result = new RefundItem();
			StringBuilder sabreResultBuilder = new StringBuilder();
			int lineCounter = 0;

			//If there is nothing to check then we stop here
			if(sabreResult == null)
				return null;

			if(sabreResult.Count < 1)
				return null;

			//We are going to simulate the sabreresult from the queue
			foreach(string tmp in sabreResult)
			{
				lineCounter++;

				if(lineCounter == 1)
				{
					sabreResultBuilder.Append(tmp);
				}
				else
				{
					sabreResultBuilder.Append("\n" + tmp);
				}
			}

			result = TranslateFromQueue(sabreResultBuilder.ToString());

			return result;
		}
		
		public RefundItem TranslateFromQueue(string sabreResult)
		{
			RefundItem result = new RefundItem();

			if(sabreResult.Trim().Length < 1 || !((sabreResult.IndexOf("ET1*") > -1)
				&& (sabreResult.IndexOf("ET2*") > -1) && (sabreResult.IndexOf("ET3*") > -1)
				&& (sabreResult.IndexOf("ET4*") > -1) && (sabreResult.IndexOf("ET5*") > -1)
				&& (sabreResult.IndexOf("ET6*") > -1)))
			{
				result.ValidRefundItem = false;
				return result;
			}else 
			{
				result.ValidRefundItem = true;
			}


			//First and most important get the lines
			result.QueueLines = GetVCRrefundLines(sabreResult);

			//Then parse the individual bits
			PopulateRefundProperties(result);

			//We should be done with the translation now.
			return result;

		}

		protected List<string> GetVCRrefundLines(string SabreResult)
		{
			List<string> result = new List<string>();			

			//If there is nothing there we get out of her quick
			if(SabreResult == null)
				return result;

			//Also if ther are no lines no reason to do anything
			if(SabreResult.Trim().Length < 1 || !((SabreResult.IndexOf("ET1*") > -1)
					&& (SabreResult.IndexOf("ET2*") > -1) && (SabreResult.IndexOf("ET3*") > -1)
					&& (SabreResult.IndexOf("ET4*") > -1) && (SabreResult.IndexOf("ET5*") > -1)
					&& (SabreResult.IndexOf("ET6*") > -1)))
			{
				try
				{
					_AppVariables.logger.Fatal("Failure: Invalid VCRREfunds Queue Record: " + SabreResult);
					return result;
				}
				catch
				{
					TypeLoadException ex = new TypeLoadException("Failure: Invalid VCRrefunds Queue Record: " + SabreResult);
					throw ex;
				}
				
			}

			//First we have to init the result list to have the correct amount of lines
			InitLinesList(ref result);

			//We are going to split the 
			char[] splitter  = {'\n'};
			string[] split =  SabreResult.Split(splitter);

			//Now we get the line number is there is such

			int lineNumber = -1;
			string tempStr = "";

			foreach( string nextLine in split)
			{
				lineNumber = GetLineNumber(nextLine);
				tempStr = nextLine;

				if(lineNumber > 0 && lineNumber <= result.Count)
				{
					tempStr = nextLine.Replace("ET" + lineNumber + "*", "");
					result[lineNumber - 1] = AppendStringKeepLength(tempStr, result[lineNumber - 1]);

				}
				else
				{
					//checked To see if it last line
					if( IsLastLine(nextLine))
						result[LAST_LINE_INDEX] = nextLine;
				}
			}

			//The last should have had the time
			
			return result;
		}

		protected bool IsLastLine(string line)
		{
			bool result = false; 
			Regex lineRegx = new Regex(@"[A-Z]{3}\.\w");

			if(lineRegx.IsMatch(line))
				result = true;

			return result;

		}

		protected string AppendStringKeepLength(string lineToAppend, string DestinationLine)
		{

			StringBuilder workingBuilder = new StringBuilder(lineToAppend);
			workingBuilder.Append(DestinationLine);
			workingBuilder.Remove(workingBuilder.Length - lineToAppend.Length, lineToAppend.Length);
			return workingBuilder.ToString();
		}

		protected int GetLineNumber(string line)
		{
			int result = -1;
			string linenumber = "";

			Regex lineRegx = new Regex(@"ET[01]?[0-9]?[0-9]\*");

			if(lineRegx.IsMatch(line))
			{
				//Get the line number but only one if there is such
				linenumber = lineRegx.Matches(line)[0].ToString().Trim();

				//Remove the letters 
				linenumber = linenumber.Replace("ET", "");
				linenumber = linenumber.Replace("*", "");

				//And make it into an integer
				if(linenumber.Length > 0)
				{
					result = int.Parse(linenumber);
				}
			}

			//Go back
			return result;

		}

		//'******************************************************************************************
		//'**  This is what the accounting record that the backroom process will create.
		//'**
		//'**           05  IT-REF-PNR-NUMBER             PIC  X(06).
		//'**           05  IT-REF-PAYMENT-CODE           PIC  X(02).
		//'**           05  IT-REF-CREDIT-CARD-NUMBER     PIC  X(24).
		//'**           05  IT-REF-PNR-INFO.
		//'**               10  IT-REF-TICKET-NUMBER      PIC  X(10).
		//'**               10  IT-REF-COUPONS            PIC  X(04).
		//'** -----------------------------------------------------------------------
		//'**               10  IT-REF-PAX-NAME           PIC  X(30).
		//'**           05  IT-REF-MAIL-NAME              PIC  X(30).
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-ADDRESS1               PIC  X(30).
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-ADDRESS2               PIC  X(30).
		//'**           05  IT-REF-CITY                   PIC  X(26).
		//'**           05  IT-REF-STATE                  PIC  X(04).
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-ZIP-CODE               PIC  X(10).
		//'**           05  IT-REF-ZIP-CODE-REDEF REDEFINES
		//'**               IT-REF-ZIP-CODE.
		//'**               10  IT-REF-ZIP-FIRST5         PIC  9(05).
		//'**               10  IT-REF-ZIP-DASH           PIC  9(01).
		//'**               10  IT-REF-ZIP-LAST4          PIC  9(04).
		//'**           05  IT-REF-COUNTRY                PIC  X(11).
		//'**           05  IT-REF-CC-WAIVER              PIC  X(01).
		//'**           05  IT-REF-INVOLUNTARY            PIC  X(01).
		//'**           05  IT-REF-DRAFT-NUMBER           PIC  X(10).
		//'**           05  IT-REF-DOWNGRADE              PIC  X(01).
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-ORIG-FAREBASIS         PIC  X(15).
		//'**           05  IT-REF-NEW-FAREBASIS          PIC  X(15).
		//'******************************************************************************************
		//'** This is the Mileage Plan record the backroom will create.
		//'**
		//'**           05  IN-PAYMENT-REASON             PIC  X(02). 'Don't send
		//'**           05  IN-CERTIFICATE-TABLE OCCURS 9 TIMES.
		//'**               10  IN-CERTIFICATE-ID         PIC  X(08).
		//'**           05  IN-CARD-TYPE                  PIC  X(02).
		//'**           05  IN-CREDIT-CARD-NUMBER         PIC  X(20).
		//'**           05  IN-CARD-NAME                  PIC  X(30).
		//'** -----------------------------------------------------------------------
		//'**           05  IN-CARD-EXPIRATION-YYMM.
		//'**               10  IN-CARD-EXPIRATION-YY     PIC  9(02).
		//'**               10  IN-CARD-EXPIRATION-MM     PIC  9(02).
		//'**           05  IN-PAYMENT-AMOUNT             PIC  S9(05)V99.
		//'**           05  IN-VALIDATION-NUMBER          PIC  X(06).
		//'**           05  IN-AGENT                      PIC  X(08).
		//'** -----------------------------------------------------------------------
		//'** This is the CET information that will be used to create additional backroom records.
		//'**           05  IT-REF-PNR-INFO.-CONJUNCTIVE-TICKETS OCCURS 3 TIMES
		//'**               10  IT-REF-TICKET-NUMBER      PIC  X(10).
		//'**               10  IT-REF-COUPONS            PIC  X(04).
		//'** The last 3 chars on this SABRE line relates to the first 3 chars of the
		//'** IT-REF-TICKET-NUMBER item on line 1 of the Acctg feed
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-TYPE                   PIC  X(02)
		//'**               88 IT-REF-TYPE-REFUND         VALUE 'RF'
		//'**               88 IT-REF-TYPE-REISSUE-ADD    VALUE 'RA'
		//'**               88 IT-REF-TYPE-REISSUE-EVEN   VALUE 'RE'
		//'**               88 IT-REF-TYPE-REISSUE-REFUND VALUE 'RR'
		//'**           05  IT-REF-REISSUE-TKT-NBR        PIC  X(13)
		//'**           05  IT-REF-REISSUE-ORIG-TKT-NBR   PIC  X(13)
		//'**           05  IT-REF-REISSUE-OLD-FARE       PIC  9(09)V9(02)
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-OLD-TAXES OCCURS 15 TIMES
		//'**               10  IT-REF-OLD-TAX-TYPE       PIC  X(03)
		//'**               10  IT-REF-OLD-TAX-AMT        PIC  9(07)V9(02)
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-REISSUE-NEW-FARE       PIC  9(09)V9(02)
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-NEW-TAXES OCCURS 15 TIMES
		//'**               10  IT-REF-NEW-TAX-TYPE       PIC  X(03)
		//'**               10  IT-REF-NEW-TAX-AMT        PIC  9(07)V9(02)
		//'** -----------------------------------------------------------------------
		//'**           05  IT-REF-NEW-CHARGES-CC-NBR     PIC  X(24)
		//'**           05  IT-REF-NEW-CHRG-EXP-DATE      PIC  X(04)
		//'**           05  IT-REF-NEW-CHRG-APPROVALC     PIC  X(06)
		//'**           05  IT-REF-REFUNDABILITY-IND      PIC  X(01)
		//'**           05  IT-REF-EMAIL                  PIC  X(50)
		//'**           05  IT-REF-ACTION-CODE            PIC  X(02).
		//'**           05  IT-REF-ASCOM-QUOTED-AMT       PIC  9(09)V9(02).
		//'**           05  IT-REF-GIFT-CERT-IND          PIC  X(01).
		//'**           05  FILLER                        PIC  X(22)
		//'==========================================================================================

		protected void PopulateRefundProperties(RefundItem item)
		{


			string tempStr = "";

			//Line	1
			tempStr = item.QueueLines[0];
			item.Recloc = tempStr.Substring(0, 6);
			item.FOPType = tempStr.Substring(6, 2);
			item.CCNumber = tempStr.Substring(8, 24);
			item.TicketNumber = tempStr.Substring(32, 10);
			item.CouponNbrs = tempStr.Substring(42, 4);

			//Line	2
			tempStr = item.QueueLines[1];
			item.PsgrName = tempStr.Substring(0, 30);
			item.MailingName = tempStr.Substring(30, 30);

			//Line	3
			tempStr = item.QueueLines[2];
			item.MailingAddress1 = tempStr.Substring(0, 30);

			//Line	4
			tempStr = item.QueueLines[3];
			item.MailingAddress2 = tempStr.Substring(0, 30);
			item.MailingCity = tempStr.Substring(30, 26);
			item.MailingState = tempStr.Substring(56, 4);

			//Line	5
			tempStr = item.QueueLines[4];
			item.ZipCode = tempStr.Substring(0, 10);
			item.Country = tempStr.Substring(10, 11);
			item.RefundWaiverCode = tempStr.Substring(21, 1);
			item.InVolIndicator = tempStr.Substring(22, 1);
			item.RefundDraftNumber = tempStr.Substring(23, 10);
			item.DowngradeIndicator = tempStr.Substring(33, 1);

			//Line	6
			tempStr = item.QueueLines[5];
			item.OriginalFareBasis = tempStr.Substring(0, 15);
			item.NewFareBasis = tempStr.Substring(15, 15);

			//Line	7
			tempStr = item.QueueLines[6];
			item.OldMPCertificateNbr = tempStr.Substring(0, 8);
			item.CCTypeForMPRedepositFee = tempStr.Substring(8, 2);
			item.CCNumberMPRedepositFee = tempStr.Substring(10, 20);
			item.CCHolderNameMPRedepoFee = tempStr.Substring(30, 30);

			//Line	8
			tempStr = item.QueueLines[7];
			item.CCExpDateMPRedepositFee = tempStr.Substring(0, 4);
			item.MPRedepositFeeAmount = tempStr.Substring(4, 8);
			item.CCApprovalCodeMPRedepositFee = tempStr.Substring(12, 6);
			item.AgentID = tempStr.Substring(18, 8);
			item.IMSRegion = tempStr.Substring(26, 4);

			//Line	9
			tempStr = item.QueueLines[8];
			item.ConjTktNumber2 = tempStr.Substring(0, 10);
			item.TktNbr2CouponNbrs = tempStr.Substring(10, 4);
            item.ConjTktNumber3 = tempStr.Substring(14, 10);
			item.TktNbr3CouponNbrs = tempStr.Substring(24, 4);
			item.ConjTktNbr4 = tempStr.Substring(28, 10);
			item.TktNbr4CouponNbrs = tempStr.Substring(38, 4);
            item.TktNbrAirlineCode = tempStr.Substring(42, 3);

			//Line	10
			tempStr = item.QueueLines[9];
			item.RefundType = tempStr.Substring(0, 2);
			item.NewReissueTktNumber = tempStr.Substring(2, 13);
            item.OrigTktNbrInclAirlineCode = tempStr.Substring(15, 13);
			item.ExchTktBaseFareDollars = tempStr.Substring(28, 9);
			item.ExchTktBaseFareCents = tempStr.Substring(37, 2);

			//Line	11
			tempStr = item.QueueLines[10];
			item.TaxType1 = tempStr.Substring(0, 3);
			item.TaxAmountdollars1 = tempStr.Substring(3, 7);
			item.TaxAmountcents1 = tempStr.Substring(10, 2);
			item.TaxType1b = tempStr.Substring(12, 3);
			item.TaxAmountDollars1b = tempStr.Substring(15, 7);
			item.TaxAmountCents1b = tempStr.Substring(22, 2);
			item.TaxType1c = tempStr.Substring(24, 3);
			item.TaxAmountDollars1c = tempStr.Substring(27, 7);
			item.TaxAmountCents1c = tempStr.Substring(34, 2);
			item.TaxType1d = tempStr.Substring(36, 3);
			item.TaxAmountDollars1d = tempStr.Substring(39, 7);
			item.TaxAmtCents1d = tempStr.Substring(46, 2);

			//Line	12
			tempStr = item.QueueLines[11];
			item.TaxType2 = tempStr.Substring(0, 3);
			item.TaxAmountdollars2 = tempStr.Substring(3, 7);
			item.TaxAmountcents2 = tempStr.Substring(10, 2);
			item.TaxType2b = tempStr.Substring(12, 3);
			item.TaxAmountDollars2b = tempStr.Substring(15, 7);
			item.TaxAmountCents2b = tempStr.Substring(22, 2);
			item.TaxType2c = tempStr.Substring(24, 3);
			item.TaxAmountDollars2c = tempStr.Substring(27, 7);
			item.TaxAmountCents2c = tempStr.Substring(34, 2);
			item.TaxType2d = tempStr.Substring(36, 3);
			item.TaxAmountDollars2d = tempStr.Substring(39, 7);
			item.TaxAmtCents2d = tempStr.Substring(46, 2);

			//Line	13
			tempStr = item.QueueLines[12];
			item.TaxType3 = tempStr.Substring(0, 3);
			item.TaxAmountdollars3 = tempStr.Substring(3, 7);
			item.TaxAmountcents3 = tempStr.Substring(10, 2);
			item.TaxType3b = tempStr.Substring(12, 3);
			item.TaxAmountDollars3b = tempStr.Substring(15, 7);
			item.TaxAmountCents3b = tempStr.Substring(22, 2);
			item.TaxType3c = tempStr.Substring(24, 3);
			item.TaxAmountDollars3c = tempStr.Substring(27, 7);
			item.TaxAmountCents3c = tempStr.Substring(34, 2);
			item.TaxType3d = tempStr.Substring(36, 3);
			item.TaxAmountDollars3d = tempStr.Substring(39, 7);
			item.TaxAmtCents3d = tempStr.Substring(46, 2);

			//Line	14
			tempStr = item.QueueLines[13];
			item.TaxType4 = tempStr.Substring(0, 3);
			item.TaxAmountdollars4 = tempStr.Substring(3, 7);
			item.TaxAmountcents4 = tempStr.Substring(10, 2);
			item.TaxType4b = tempStr.Substring(12, 3);
			item.TaxAmountDollars4b = tempStr.Substring(15, 7);
			item.TaxAmountCents4b = tempStr.Substring(22, 2);
			item.TaxType4c = tempStr.Substring(24, 3);
			item.TaxAmountDollars4c = tempStr.Substring(27, 7);
			item.TaxAmountCents4c = tempStr.Substring(34, 2);
			//item.TaxType4d = tempStr.Substring(36, 3);
			//item.TaxAmountDollars4d = tempStr.Substring(39, 7);
			//item.TaxAmtCents4d = tempStr.Substring(46, 2);

			//Line	15
			tempStr = item.QueueLines[14];
			item.NewTktBaseFaredollars = tempStr.Substring(0, 9);
			item.NewTktBaseFarecents = tempStr.Substring(9, 2);

			//Line	16
			tempStr = item.QueueLines[15];
			item.TaxType5 = tempStr.Substring(0, 3);
			item.TaxAmountdollars5 = tempStr.Substring(3, 7);
			item.TaxAmountcents5 = tempStr.Substring(10, 2);
			item.TaxType5b = tempStr.Substring(12, 3);
			item.TaxAmountDollars5b = tempStr.Substring(15, 7);
			item.TaxAmountCents5b = tempStr.Substring(22, 2);
			item.TaxType5c = tempStr.Substring(24, 3);
			item.TaxAmountDollars5c = tempStr.Substring(27, 7);
			item.TaxAmountCents5c = tempStr.Substring(34, 2);
			item.TaxType5d = tempStr.Substring(36, 3);
			item.TaxAmountDollars5d = tempStr.Substring(39, 7);
			item.TaxAmtCents5d = tempStr.Substring(46, 2);

			//Line	17
			tempStr = item.QueueLines[16];
			item.TaxType6 = tempStr.Substring(0, 3);
			item.TaxAmountdollars6 = tempStr.Substring(3, 7);
			item.TaxAmountcents6 = tempStr.Substring(10, 2);
			item.TaxType6b = tempStr.Substring(12, 3);
			item.TaxAmountDollars6b = tempStr.Substring(15, 7);
			item.TaxAmountCents6b = tempStr.Substring(22, 2);
			item.TaxType6c = tempStr.Substring(24, 3);
			item.TaxAmountDollars6c = tempStr.Substring(27, 7);
			item.TaxAmountCents6c = tempStr.Substring(34, 2);
			item.TaxType6d = tempStr.Substring(36, 3);
			item.TaxAmountDollars6d = tempStr.Substring(39, 7);
			item.TaxAmtCents6d = tempStr.Substring(46, 2);

			//Line	18
			tempStr = item.QueueLines[17];
			item.TaxType7 = tempStr.Substring(0, 3);
			item.TaxAmountdollars7 = tempStr.Substring(3, 7);
			item.TaxAmountcents7 = tempStr.Substring(10, 2);
			item.TaxType7b = tempStr.Substring(12, 3);
			item.TaxAmountDollars7b = tempStr.Substring(15, 7);
			item.TaxAmountCents7b = tempStr.Substring(22, 2);
			item.TaxType7c = tempStr.Substring(24, 3);
			item.TaxAmountDollars7c = tempStr.Substring(27, 7);
			item.TaxAmountCents7c = tempStr.Substring(34, 2);
			item.TaxType7d = tempStr.Substring(36, 3);
			item.TaxAmountDollars7d = tempStr.Substring(39, 7);
			item.TaxAmtCents7d = tempStr.Substring(46, 2);

			//Line	19
			tempStr = item.QueueLines[18];
			item.TaxType8 = tempStr.Substring(0, 3);
			item.TaxAmountdollars8 = tempStr.Substring(3, 7);
			item.TaxAmountcents8 = tempStr.Substring(10, 2);
			item.TaxType8b = tempStr.Substring(12, 3);
			item.TaxAmountDollars8b = tempStr.Substring(15, 7);
			item.TaxAmountCents8b = tempStr.Substring(22, 2);
			item.TaxType8c = tempStr.Substring(24, 3);
			item.TaxAmountDollars8c = tempStr.Substring(27, 7);
			item.TaxAmountCents8c = tempStr.Substring(34, 2);
			//item.TaxType8d = tempStr.Substring(36, 3);
			//item.TaxAmountDollars8d = tempStr.Substring(39, 7);
			//item.TaxAmtCents8d = tempStr.Substring(46, 2);

			//Line	20
			tempStr = item.QueueLines[19];
			item.AddCollectCCNumber = tempStr.Substring(0, 24);
			item.AddCollectCCExpDate = tempStr.Substring(24, 4);
			item.AddCollectCCApprovalCode = tempStr.Substring(28, 6);
			item.RefundabilityIndicator = tempStr.Substring(34, 1);

			//Line	21
			tempStr = item.QueueLines[20];
			item.EmailAddress = tempStr.Substring(0, 30);

			//Line	22
			tempStr = item.QueueLines[21];
			item.AutopayIndicator = tempStr.Substring(0, 2);
			item.RefundAmount = tempStr.Substring(2, 11);

			//Line	23
			tempStr = item.QueueLines[22];
			item.GiftCertificateIndicator = tempStr.Substring(0, 1);

			//This reg expression returns the MP Certnumber in the front of the message
			//The Cert number should at least have 8 numbers and at the most 12
            Regex lineRegx = new Regex(@"[0-9]{9}[0-9]?[0-9]?[0-9]?");
			
			//Line	24
			tempStr = item.QueueLines[23];
			if(lineRegx.IsMatch(tempStr))
			{
				item.CertificateNumber = lineRegx.Match(tempStr).ToString();
				item.RedepositErrormessage = tempStr.Replace(item.CertificateNumber, "").Trim();
			}

			//Line	25
			tempStr = item.QueueLines[24];
			if(lineRegx.IsMatch(tempStr))
			{
				item.CertificateNumber2 = lineRegx.Match(tempStr).ToString();
				item.RedepositErrormessage2 = tempStr.Replace(item.CertificateNumber, "").Trim();
			}

			//Line	25
			tempStr = item.QueueLines[25];
			item.RVCertificateCode = tempStr.Substring(0, 20);
			item.NonATLInd = tempStr.Substring(21, 1);				

			//Get the time
			tempStr = item.QueueLines[LAST_LINE_INDEX];
			item.City = tempStr.Substring(0, 3);
			//item.City = tempStr.Substring(3, 3);
			item.QueueDate = tempStr.Substring(12);

		}

		protected void InitLinesList(ref List<string> lines)
		{

			lines = new List<string>();

			//put the empty spaces
			//Line 1
			lines.Add(new string(' ', 49));
			//Line 2
			lines.Add(new string(' ', 60));
			//Line 3
			lines.Add(new string(' ', 30));
			//Line 4
			lines.Add(new string(' ', 60));
			//Line 5
			lines.Add(new string(' ', 34));
			//Line 6
			lines.Add(new string(' ', 30));
			//Line 7
			lines.Add(new string(' ', 60));
			//Line 8
			lines.Add(new string(' ', 30));
			//Line 9
			lines.Add(new string(' ', 45));
			//Line 10
			lines.Add(new string(' ', 39));
			//Line 11
			lines.Add(new string(' ', 48));
			//Line 12
			lines.Add(new string(' ', 48));
			//Line 13
			lines.Add(new string(' ', 48));
			//Line 14
			lines.Add(new string(' ', 36));
			//Line 15
			lines.Add(new string(' ', 11));
			//Line 16
			lines.Add(new string(' ', 48));
			//Line 17
			lines.Add(new string(' ', 48));
			//Line 18
			lines.Add(new string(' ', 48));
			//Line 19
			lines.Add(new string(' ', 36));
			//Line 20
			lines.Add(new string(' ', 35));
			//Line 21
			lines.Add(new string(' ', 50));
			//Line 22
			lines.Add(new string(' ', 13));
			//Line 23
			lines.Add(new string(' ', 1));
			//Line 24
			lines.Add(new string(' ', 60));
			//Line 25
			lines.Add(new string(' ', 60));
			//Line 26
			lines.Add(new string(' ', 22));
			//Line 27 Date
			lines.Add(new string(' ', 60));


		}

	}
}
