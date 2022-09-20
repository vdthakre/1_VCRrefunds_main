using System;
using System.Collections.Generic;
using System.Text;

namespace VCRrefunds
{
	public class MPRedepositErrorHandler : IMPRedepositErrorHandler
	{

		protected RefundAppVariables _AppVariables;		

		private MPRedepositErrorHandler()
		{		
		}

		public MPRedepositErrorHandler( RefundAppVariables variables )
		{
			_AppVariables = variables;
		}

	}
}
