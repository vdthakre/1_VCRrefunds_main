using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VCRrefunds
{
	public class AccountFileManager : IAccountFileManager
	{
		#region Internal Variables

		protected RefundAppVariables _AppVariables;
		protected string _resultFileName;
		
		#endregion
		
		#region Constructors

			//We don't want to create one of this without having all the info
			private AccountFileManager()
			{

			}

			public AccountFileManager(RefundAppVariables InitVariables)
			{

				_AppVariables = InitVariables;
				_resultFileName = GetResultFileName(DateTime.Now);

			}

		#endregion

		#region Public Methods

		//Todo
		public bool AddAccountingLine(string processLine)
		{
			return false;
		}

		protected void WriteToFile( string message)
		{
			StreamWriter SW;
			SW = File.AppendText(_AppVariables.ResultPath + _resultFileName);
			SW.WriteLine("This Line Is Appended");
			SW.Close();
		}
		
		
		protected string GetResultFileName(DateTime thisTime)
		{
			string result = ""; 
			 
			
			if( thisTime.DayOfWeek == DayOfWeek.Sunday	)
			{
				thisTime = thisTime.AddDays(1);
			}
			else if(thisTime.DayOfWeek == DayOfWeek.Saturday)
			{
				thisTime = thisTime.AddDays(2);
			}
			else if(thisTime.Hour > 12 && thisTime.DayOfWeek == DayOfWeek.Friday)
			{
				thisTime = thisTime.AddDays(3);
			}
			else if(thisTime.Hour > 12 )
			{
				thisTime = thisTime.AddDays(1);
			}

			result = "RF" + thisTime.ToString("yyMMdd") + ".VCR";

			return result;
		}

		#endregion
	}
}
