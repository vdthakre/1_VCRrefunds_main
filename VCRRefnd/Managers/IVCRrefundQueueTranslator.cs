using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	//Todo
	public interface IVCRrefundQueueTranslator
	{
		RefundItem TranslateFromQueue(string sabreResult);
		bool TranslateToAccountingFormat(RefundItem item, ref string resultString);
		List<string> TranslateToAccountingFormatConjunctive(RefundItem item);
	}
}
