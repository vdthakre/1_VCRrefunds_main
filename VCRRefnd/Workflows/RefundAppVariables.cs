using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace VCRrefunds
{
	public class RefundAppVariables 
	{

		protected string _GroupName;
		protected string _QueueName;
		protected string _UserPassword;
		protected string _UserID;
		protected string _LogPath;
		protected string _SabreSysPath;
		private ILog _logger;
		private string _ResultPath;

		public RefundAppVariables()
		{
			//Init our variables
			_GroupName = "ImageRes";
			_QueueName = "ETTS";
			_UserPassword = "R0b0tic";
			_UserID = "6007";
			_SabreSysPath = "RfndSABRE.INI";
			_ResultPath = "";

		}

		public ILog logger
		{
			get { return _logger; }
			set { _logger = value; }
		}

		public string SabreSysPath
		{
			get { return _SabreSysPath; }
			set { _SabreSysPath = value; }
		}

		public string LogPath
		{
			get { return _LogPath; }
			set { _LogPath = value; }
		}

		public string ResultPath
		{
			get { return _ResultPath; }
			set { _ResultPath = value; }
		}

		public string UserID
		{
			get { return _UserID;}
			set { _UserID = value;}
		}
		
		public string UserPassword
		{
			get { return _UserPassword;}
			set { _UserPassword = value;}
		}
		
		public string QueueName
		{
			get { return _QueueName;}
			set { _QueueName = value;}
		}
		
		public string GroupName
		{
			get { return _GroupName;}
			set { _GroupName = value;}
		}
	
	}
}
