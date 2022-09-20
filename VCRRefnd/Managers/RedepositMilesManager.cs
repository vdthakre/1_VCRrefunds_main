using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using VCRRefnd.MbrAccess;
using AlaskaAir.Reservations.Core;


namespace VCRrefunds
{
	public enum MbrAccessErrorsEnum
	{
		NO_ERROR = 0,
		MISSING_PASSENGER_NAME = 901,
		MAXIMUM_NINE_PASSENGER = 902,
		INVALID_AWARDTYPE = 903,
		MILEAGE_PLAN_UNAVAILABLE = 904,
		INCORRECT_MILEAGE_PLAN_NUMBER = 905,
		INVALID_AWARD_CODE = 906,
		INSUFFICIENT_MILES = 907,
		INVALID_AWARD_TYPE = 908,
		MILEAGE_PLAN_UNAVAILABLE_IMS = 910,
		FRAUD = 911,
		INVALID_CERTIFICATE_CODE = 912,
		INVALID_TICKET_NUMBER = 913,
		CERTIFICATE_ALREADY_REDEPOSIT = 914,
		INVALID_TICKET_ISSUE_DATE = 915,
		MISSING_PASSENGER_FIRSTNAME = 916,
		MISSING_PASSENGER_LASTNAME = 917,
		INVALID_STREETADDRESS = 918,
		INVALID_BILLCITY = 919,
		INVALID_BILLSTATEPROV = 920,
		INVALID_HOMEZIP = 921,
		INVALID_MPADDRESSCOUNTRY = 922,
		INVALID_MPPHONE = 923,
		INVALID_MAILFORMAT = 924,
		CHANNELNOTSUPPORTED = 925,
		INVALID_BIRTHDATE = 926,
		MP_ALREADYEXISTS = 927,
		REDEMPTION_NOT_ALLOWED = 928,
		DUPLICATE_TRANSACTION = 929,
		ENROLLMENT_FAILURE = 995,
		UNKNOWN_ERROR = 999
	}

	public class RedepositMilesManager : IRedepositMilesManager
	{
		protected RefundAppVariables _AppVariables;
		protected MPRedepositErrorHandler _MPErrorHandler;


		private RedepositMilesManager()
		{		
		}

		public RedepositMilesManager( RefundAppVariables variables )
		{
			_AppVariables = variables;

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}

		public string GetRedepositServiceVersion()
		{
			string result = "";

			//Get the proxy
			MbrAccess proxy = new MbrAccess();
			proxy.Url = _AppVariables.MbrAccessURL;

			result = proxy.Version();

			return result;
		}

		public void RedepositRefundMiles(RefundItem currentItem, ref bool RetryLater)
		{

			//Assume we don't have to retry
			RetryLater = false;

            MPRedepositMiles redeposit = new MPRedepositMiles(_AppVariables.MbrAccessURL, _AppVariables.MbrAccessUserName, _AppVariables.MbrAccessPassword);

            List<AwardCertificate> certificates = new List<AwardCertificate>();
            AwardCertificate cert = new AwardCertificate();
            AwardCertificate cert2 = new AwardCertificate();
			UserInfo user = new UserInfo();
			
			try{

				//assume the best
				currentItem.RedepositErrormessage = "";
				currentItem.RedepositCode = (int)MbrAccessErrorsEnum.NO_ERROR;

				//If we have a certificate number in the old location, 
				//we cannot do anything with it( we don't know the lenght of the number
				//so we could be dealing with a truncated number
				if(currentItem.OldMPCertificateNbr.Trim().Length > 0)
				{
					currentItem.RedepositErrormessage = "INVALID QUEUE LOCATION FOR MP CERTIFICATE NUMBER";
					currentItem.RedepositCode = (int) MbrAccessErrorsEnum.UNKNOWN_ERROR;
				}

				if(currentItem.CertificateNumber.Trim().Length < 1)
				{
					//There is nothing to do we go back. No need to retry either
					RetryLater = false;
					return;
				}

				//populate request
				cert.MPCertificateNumber = currentItem.CertificateNumber.Trim();
                certificates.Add(cert);

				//But if we have more.. go ahead
				if(currentItem.CertificateNumber2.Trim().Length > 0)
				{
                    cert2.MPCertificateNumber = currentItem.CertificateNumber2.Trim();
                    certificates.Add(cert2);
				}

                bool redepositResult = redeposit.RedepositMiles(certificates);

                //No Exceptions... we are OK
                currentItem.RedepositCode = (int)MbrAccessErrorsEnum.NO_ERROR;

                System.Console.WriteLine("Redeposit Miles successful");

			}catch( Exception e)
			{
                System.Console.Write("Redeposit Miles Failed");


                //If we have an regular execption we handle it.
                if (typeof(MPReportException) == e.GetType())
                {
                    //Check for errors
                    HandledRedepositErrors(((MPReportException)e).ErrorCode, ((MPReportException)e).Message, ref RetryLater, currentItem);
                }
                else
                {
                    //For now we are assuming that if we get an exception it was a connetivity issue
                    currentItem.RedepositCode = (int)MbrAccessErrorsEnum.UNKNOWN_ERROR;
                    currentItem.RedepositErrormessage = e.Message;
                    RetryLater = false;
                    _AppVariables.logger.Error("Failure: Not able to redeposit miles error: " + e.Message + "\n" + e.StackTrace);
                }
		    }

			return;
		
		}

		protected void HandledRedepositErrors( int ErrorCode,  string ErrorMessage, ref bool RetryLater, RefundItem currentItem)
		{

            currentItem.RedepositCode = ErrorCode;
            currentItem.RedepositErrormessage = ErrorMessage;
            RetryLater = false;

            //but if it is system Unavailable then we would try again
            if( ErrorCode == (int)MpreportErrors.MP_SYSTEM_UNAVAILABLE)
					RetryLater = true;

		}

        protected void WriteToFile(string message, string filename)
        {
            StreamWriter SW;
            SW = File.AppendText(filename);
            SW.WriteLine(message);
            SW.Close();
        }

				
	}
	
}
