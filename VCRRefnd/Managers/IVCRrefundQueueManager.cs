using System;
using System.Collections.Generic;
using System.Text;
using ASHosts.Interop;

namespace VCRrefunds
{
	public interface IVCRrefundQueueManager
	{
		void SetHostSession(ASHostSession session);
		RefundItem OpenRefundQueue();
		RefundItem RemoveAndGetNextItem();
		RefundItem IgnoreAndGetNextItem();
		bool ExitQueue();
		bool CheckInitVariables(RefundAppVariables InitVariables);
		bool StablishConnection();
		int GetTotalRecords();
		void CloseConnection();
		bool PutLinesIntoQueue(List<string> lines, string Queue);
	}
}
