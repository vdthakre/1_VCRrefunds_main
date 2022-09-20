using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public interface IRedepositMilesManager
	{
		void RedepositRefundMiles( RefundItem currentItem, ref bool RetryLater);
	}
}
