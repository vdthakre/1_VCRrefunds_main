using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Mail;

namespace VCRrefunds
{
	public class RedepositErrorHandler : IRedepositErrorHandler
	{
		protected RefundAppVariables _AppVariables;
		string _resultFileName;

		protected RedepositErrorHandler()
		{

		}

		public RedepositErrorHandler( RefundAppVariables AppVariables)
		{
			_AppVariables = AppVariables;
			_resultFileName = GetResultFileName(DateTime.Now);
		}

		//TODO
		public bool SendErrorReport()
		{
			bool result = false;

			DateTime yesterday = DateTime.Now.AddDays(-1);
			//Check to see if we have an error file to send
			string sErrorFileName = _AppVariables.ResultPath + "\\" + "MPERR" + yesterday.ToString("yyMMdd") + ".TXT";
			string sSentFileName = _AppVariables.ResultPath + "\\" + "MPERR" + yesterday.ToString("yyMMdd") + ".SNT";

			try
			{

				//If we don't have a file then we go back
				if(!File.Exists(sErrorFileName))
				{
					_AppVariables.logger.Info("No MP Error report: " + sErrorFileName + " found.");
					result = true;
					return result;
				}

				//Send the file
				emailErrorReport(_AppVariables, sErrorFileName);

				//if things went well we should rename the file
				RenameFile(sErrorFileName, sSentFileName);
				
				//if we got here most probably everything went well
				result = true;				

			}
			catch(Exception ex)
			{
				//Log the error
				_AppVariables.logger.Fatal("Failure: Error sending MPErr Report: " + ex.Message + " " + ex.StackTrace);
				throw ex;				
			}

			return result;
		}

		public void emailErrorReport(RefundAppVariables variables, string attachmentName)
		{
			//Yesterday
			DateTime yesterday = DateTime.Now.AddDays(-1);

			//Now we are going to email the file
			MailMessage message = new MailMessage(
				variables.ErrorAddressesFrom,
				variables.ErrorAddressesTo,
				variables.ErrorSubject, variables.ErrorSubject);

			//Put the bcc addresses
			char[] delimiter = { ';', ',' };

			if(variables.ErrorAddressesBcc.Trim().Length > 0)
			{
				string[] bccList = variables.ErrorAddressesBcc.Split(delimiter);
				foreach(string str in bccList)
				{
					message.Bcc.Add(str);
				}
			}

			if(variables.ErrorAddressesCc.Trim().Length > 0)
			{
				string[] ccList = variables.ErrorAddressesCc.Split(delimiter);
				foreach(string ccStr in ccList)
				{
					message.CC.Add(ccStr);
				}
			}
			//Get the attachement data
			byte[] data = File.ReadAllBytes(attachmentName);
			MemoryStream ms = new MemoryStream(data);
			message.Attachments.Add(new Attachment(ms, attachmentName));

			//set the client
			SmtpClient emailClient = new SmtpClient(variables.ErrorSMTPServer);

			//Send the email
			emailClient.Send(message);

		}

		public bool HandleError(RefundItem item)
		{
			bool result = true;
			string certnumber = "";

			//If we have an error we record it 
			if( item.RedepositCode != 0)
			{
				WriteToFile("");
				WriteToFile("** REDEPOSIT FAILED **");
				certnumber = item.CertificateNumber;

				//If we have an old style cert number
				if( (certnumber.Trim().Length < 1) && (item.OldMPCertificateNbr.Trim().Length > 0))
					certnumber = item.OldMPCertificateNbr;

				WriteToFile("CERTNBR        - " + certnumber);
				WriteToFile("AGENTID        - " + item.AgentID);
				WriteToFile("RECLOC         - " + item.Recloc);
				WriteToFile("FAILURE REASON - " + item.RedepositErrormessage);
				WriteToFile("");

			}

			result = true;
			return result;
		}

		protected bool RenameFile(string FilepathOld, string FilepathNew)
		{
			bool result = false;

			FileInfo fi = new FileInfo(FilepathOld);
			FileInfo fnew = new FileInfo(FilepathNew);

			if(fi.Exists)
			{
				if(fnew.Exists)
				{
					fnew.Delete();										
				}

				fi.MoveTo(FilepathNew);
			}

			//things went well
			result = true;

			return result;
		}


		protected void WriteToFile(string message)
		{
			StreamWriter SW;
			SW = File.AppendText(_AppVariables.ResultPath + "\\" + _resultFileName);
			SW.WriteLine(message);
			SW.Close();
		}

		protected string GetResultFileName(DateTime thisTime)
		{
			string result = "";


			if(thisTime.DayOfWeek == DayOfWeek.Sunday)
			{
				thisTime = thisTime.AddDays(1);
			}
			else if(thisTime.DayOfWeek == DayOfWeek.Saturday)
			{
				thisTime = thisTime.AddDays(2);
			}
			else if(thisTime.Hour >= 12 && thisTime.DayOfWeek == DayOfWeek.Friday)
			{
				thisTime = thisTime.AddDays(3);
			}
			else if(thisTime.Hour >= 12)
			{
				thisTime = thisTime.AddDays(1);
			}

			result = "MPERR" + thisTime.ToString("yyMMdd") + ".TXT";

			return result;
		}


	}
}
