using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public class SendVCRefundsWorkFlow : ISendVCRefundsWorkFlow
	{
		protected RefundAppVariables _AppVariables;
		protected IAccountFileManager _AccountingFileManager;
		protected IRedepositErrorHandler _RedepositErrorHandler;

		private SendVCRefundsWorkFlow()
		{

		}
		private SendVCRefundsWorkFlow(RefundAppVariables appVariables)
		{
			_AppVariables = appVariables;
			_AccountingFileManager = new AccountFileManager(appVariables);
			_RedepositErrorHandler = new RedepositErrorHandler(appVariables);
		}

		public bool Execute()
		{
			bool result = false;

			//Send the information to accounting
			//_AccountingFileManager.SendAccountingFile();

			//Send the errors
			//_RedepositErrorHandler.SendErrorReport();

			//If we made it here we are OK
			result = true;

			return result;

		}

	}
}
