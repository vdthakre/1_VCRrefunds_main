using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using ASScreens.Interop;
using System.IO;

namespace VCRrefunds
{
	public class VCRrefundWorkFlow : IVCRrefundWorkFlow
	{
		protected RefundAppVariables _appVariables;
		protected IVCRrefundQueueManager _QueueManager;
		protected IAccountFileManager _AccountingFileManager;
		protected IVCRrefundQueueTranslator _QueueTranslator ;
		protected IRedepositMilesManager _RedepositManager;
		protected IRedepositErrorHandler _RedepositErrorHandler;

		//So no one can use it
		private VCRrefundWorkFlow()
		{

		}

		//So no one can use itAppVariables
		public VCRrefundWorkFlow(RefundAppVariables Variables)
		{
			_RedepositManager = new RedepositMilesManager(Variables);
			_appVariables = Variables;
			_QueueManager = new VCRrefundQueueManager(Variables);
			_AccountingFileManager = new AccountFileManager(Variables);
			_QueueTranslator = new VCRrefundQueueTranslator(Variables);
			_RedepositErrorHandler = new RedepositErrorHandler(Variables);

			//Check that we have everything that we need
			CheckInitVariables(Variables);

		}


		public bool CheckInitVariables(RefundAppVariables InitVariables)
		{

			bool result = false;			

			//If we don't have a logger we cannot continue
			if(InitVariables.logger == null)
			{
				ArgumentNullException e = new ArgumentNullException("Logger", "Missing system logger");
				throw e;
			}

			//We are done
			return result;

		}


		public bool Execute()
		{
			bool result = false;

			//Start connections, check variables etc
			if(!Start()) return result; 

			//Process the refunds
			if(!ProcessRecords()) return result;

			//Close connections, set logs etc...
			if(!End()) return result;

			//If we got this far things went well
			result = true;

			return result;

		}

		protected bool Start()
		{
			bool result = false;

			//Write the log header.
			if(!WriteLogHeader())
				return result;

			try
			{
				_appVariables.logger.Debug("Stablishing Sabre Connection");
				//First get connection going
				if(!_QueueManager.StablishConnection())
				{
					return result;
				}

				//If we got here most possibly things are going well
				result = true;
			}
			catch(Exception e)
			{
				_appVariables.logger.Fatal("Failure: Exception Stablishing Connection: " + e.StackTrace);
			}
			return result;
		}

		protected bool WriteLogHeader()
		{
			bool result = false;

			//We only put a header for the debug
			_appVariables.logger.Debug("Starting VCRrefunds process");

			//If we got here most possibly things are going well
			result = true;
			
			return result;
		}

		//TODO
		protected bool ProcessRecords()
		{
			bool result = false;
			int totalRecords = 0;
			int processedRecords = 0;
			string processLine = "";
			RefundItem currentItem = null;
			bool retryLater = false;

			try
			{
				_appVariables.logger.Debug("Getting records");

				//We need to know how many records we are going to handle
				totalRecords = _QueueManager.GetTotalRecords();

				//We get the first one by just opening the queue
				currentItem = _QueueManager.OpenRefundQueue();

				_appVariables.logger.Info("Processing: " + totalRecords + " records.");

				//If we don't have anything to process we move on
				for(int index = 0; index < totalRecords; index++)
				{
					//if we have something 
					if(currentItem != null)
					{

						//Check to see if we have something valid
						if(currentItem.ValidRefundItem)
						{
							

							//Let's redeposit the miles first. Just in case we would want to leave the record in the queue
							_RedepositManager.RedepositRefundMiles(currentItem, ref retryLater);

                            //If there was an error we handle it
                            if (currentItem.RedepositCode != (int)MbrAccessErrorsEnum.NO_ERROR)
                            {

                                //We have to log the Refund error 
                                _RedepositErrorHandler.HandleError(currentItem);
                            }


							//If we are going to retry it..... we let customer service do that. So accounting gets the information right away
							if(retryLater)
							{
                                //if(KeepInQueue(currentItem))
								currentItem = _QueueManager.IgnoreAndGetNextItem();
								continue;
							}


							//If it is valid then get the line for accounting
							_QueueTranslator.TranslateToAccountingFormat(currentItem, ref processLine);

							if(processLine != null && processLine.Trim().Trim().Length > 0)
							{
								_AccountingFileManager.AddAccountingLine(processLine);
								processedRecords++;
							}

							//if there is a conjunctive ticket then we have to add a line with it.
							if(currentItem.ConjTktNumber2.Trim().Length > 0)
							{
								ProcessConjunctiveTickets(currentItem);

							}

						}

					}
					
					//Remove the item and get the next one
					currentItem = _QueueManager.RemoveAndGetNextItem();
					
				}

				_appVariables.logger.Info("A total of " + processedRecords + " records written to VCR Refund file " + _AccountingFileManager.ResultFileName);
			}
			catch(Exception e)
			{
				_appVariables.logger.Fatal("Failure: Execute Exception: " + e.Message + " " + e.StackTrace);

			}
			finally
			{
				_QueueManager.ExitQueue();
				//Close the connection
				_QueueManager.CloseConnection();
				
			}
			result = true;
			return result;
		}

        protected void WriteToFile(string message, string filename)
        {
            StreamWriter SW;
            SW = File.AppendText(filename);
            SW.WriteLine(message);
            SW.Close();
        }


		protected void ProcessConjunctiveTickets(RefundItem currentItem)
		{
			List<string> result = new List<string>();

			result = _QueueTranslator.TranslateToAccountingFormatConjunctive(currentItem);

			foreach(string tmp in result)
			{
				if(tmp != null && tmp.Trim().Trim().Length > 0)
				{
					_AccountingFileManager.AddAccountingLine(tmp);
				}
			}
		}
		protected bool KeepInQueue(RefundItem item)
		{
			bool result = false;

			//Check the amount of days the item has been in the queue
            if (string.IsNullOrEmpty(item.QueueDate))
                return false;

            try
            {
                string sabrecurrentDay =  AlaskaAir.Reservations.Framework.SabreDateTime.GetDate(DateTime.Now);

                //Only if it is the same day we keep to retry
                if( item.QueueDate.ToUpper().Contains(sabrecurrentDay.Trim().ToUpper() ))
                {
                    result = true;
                }
            }
            catch 
            {
                //We don't know so send the data to accounting
                result = false;
            }

			return result;
		}
		protected bool End()
		{
			
			bool result = false;

			_appVariables.logger.Info("VCRrefunds process completed");

			//So far we don't have to do much here

			result = true;

			return result;
		}

		protected DateTime GetQueueDate(string time)
		{
			DateTime resultDate = new DateTime();
			Regex lineRegx = new Regex(@"[0-9]\/[0-9]{2}[A-Z]{3}");
			System.IFormatProvider frmt = new System.Globalization.CultureInfo("en-US", true);
			ASScrUtils testUtil = new ASScrUtils();

			//check to make sure that we do have something 
			if(!lineRegx.IsMatch(time))
				return resultDate;

			//"PHX.SEA8GII 1729/05MAR
			//"123456789012345678901234567890
						
			//We use the asccreens util for this 			
			string dateStr =  lineRegx.Matches(time)[0].ToString().Substring(2,5);
			string temp = testUtil.AirDateToYYYYMMDDEx(dateStr, "-365");
			int year = int.Parse(temp.Substring(0,4));
			int month = int.Parse( temp.Substring(4,2));
			int day = int.Parse(temp.Substring(6,2));
			int hour = int.Parse(time.Substring(12, 2));
			int minute = int.Parse(time.Substring(14, 2));
			resultDate = new DateTime(year, month, day, hour, minute, 0, 0); 
			
			//Parse it.
			return resultDate;

		}
	}
}
