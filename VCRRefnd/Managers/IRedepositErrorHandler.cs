using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public interface IRedepositErrorHandler
	{
		bool HandleError(RefundItem currentItem);
		bool SendErrorReport();
	}
}
