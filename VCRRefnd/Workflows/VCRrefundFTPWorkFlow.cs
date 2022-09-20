using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

namespace VCRrefunds
{
	public class VCRrefundFTPWorkFlow : IVCRrefundFTPWorkFlow
	{
		protected IAccountFileManager _AccountingFileManager;
		protected IRedepositErrorHandler _RedepositErrorHandler;
		protected RefundAppVariables _AppVariables;

		//So no one can use it
		private VCRrefundFTPWorkFlow()
		{

		}

		//So no one can use itAppVariables
		public VCRrefundFTPWorkFlow(RefundAppVariables Variables)
		{
			_AccountingFileManager = new AccountFileManager(Variables);
			_RedepositErrorHandler = new RedepositErrorHandler(Variables);
			_AppVariables = Variables;

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

		//TODO
		public bool Execute()
		{
			bool result = false;
			bool ftpResult = false;

			//Send the file to accounting
			result = SendResultFile();
				
			//Send the error report (If it fails first time I try again)
			ftpResult = SendMPErrorReport();

			//
			result = result && ftpResult;
			

			//Out of here
			return result;

		}

		protected bool SendMPErrorReport()
		{
			bool result = false;

			result = _RedepositErrorHandler.SendErrorReport();
			
			if(!result)
			{
				_AppVariables.logger.Fatal("Failure: Retry Sending Error MP report");

				//Let's try again
				if(!_RedepositErrorHandler.SendErrorReport())
				{
					_AppVariables.logger.Fatal("Failure: Second try failed. MP error report was not sent");
				}
				else
				{
					result = true;
				}
			}

			return result;
		}


		protected bool SendResultFile()
		{
			bool result = false;

			if(!_AccountingFileManager.SendResultFile())
			{
				_AppVariables.logger.Fatal("Failure Sending Result file to accounting");

				//Something went wrong we should let IMAGE Res folder know
				AdviseVCRRefundError();
			}
			else
			{
				result = true;
			}

			return result;
		}

		protected void AdviseVCRRefundError()
		{

		}

	}
}
