using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public interface IAccountFileManager
	{
		void AddAccountingLine(string processLine);
		string ResultFileName { get;}
		bool SendResultFile();
	}
}
