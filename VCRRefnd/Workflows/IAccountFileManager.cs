using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public interface IAccountFileManager
	{
		bool AddAccountingLine(string processLine);
	}
}
