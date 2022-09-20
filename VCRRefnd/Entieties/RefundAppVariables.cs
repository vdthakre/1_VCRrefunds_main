using System;
using System.Collections.Generic;
using System.Configuration;
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
		protected string _SabreFileName;
		protected string _SabreSysPath;
		protected ILog _logger;
		protected string _ResultPath;
		protected string _MbrAccessURL;
		protected string _HostAddress;
		protected string _ImageFTPAccount;
		protected string _ImageFTPPassword;
        protected string _SSHHostKeyFingerprint;
        protected string _SSHPrivateKeyPath;
        protected string _MvsDestFile;
		protected string _LocalJclFile;
		protected string _SiteSubmit;
		protected string _MvsJclFile;
		protected string _ErrorAddressesFriendlyFrom;
		protected string _ErrorAddressesFrom;
		protected string _ErrorAddressesTo;
		protected string _ErrorAddressesCc;
		protected string _ErrorAddressesBcc;
		protected string _ErrorAddressesError;
		protected string _ErrorSubject;
		protected string _ErrorSMTPServer;
        protected string _MbrAccessUserName;
        protected string _MbrAccessPassword;
	
		public RefundAppVariables()
		{
			//Init our variables
			_GroupName = "Image";
			_QueueName = "ETTS";
			_UserPassword = "dt041993";
			_UserID = "18615";
			_SabreFileName = GetSabreConfigFileName();
			_SabreSysPath = _SabreFileName;
			_ResultPath = "";

			//FTP variables
			//_HostAddress = "159.49.233.58";
			//_ImageFTPAccount = "image";
			//_ImageFTPPassword = "imagepw";
			//_MvsDestFile = "''PROG.RFRECREF.IMAGE.REFUNDS''";
			//_LocalJclFile = "VCRREFNDTEST.JCL";
			//_SiteSubmit = "SUBMIT";
			//_MvsJclFile = "'PROG.VCRREFND.JCL'";


            //SFTP variables
            _HostAddress = "transfer.qa.alaskaair.com";
            _ImageFTPAccount = "accelya-dev";
            _ImageFTPPassword = "Accelyadev@AS027";
            _MvsDestFile = "''PROG.RFRECREF.IMAGE.REFUNDS''";
            _LocalJclFile = "VCRREFNDTEST.JCL";
            _SiteSubmit = "SUBMIT";
            _MvsJclFile = "'PROG.VCRREFND.JCL'";

            //Error handling variables
            _ErrorAddressesFriendlyFrom = "Refund Auto Mail";
			_ErrorAddressesFrom = "ace.imageres@alaskaair.com";
			_ErrorAddressesTo = "ace.imageres@alaskaair.com";
			_ErrorAddressesCc = "ace.imageres@alaskaair.com";
			_ErrorAddressesBcc = "ace.imageres@alaskaair.com";
			_ErrorAddressesError = "";
			_ErrorSubject = "Redeposit/Fee Errors";

            //Mbraccess credentials not init to prevent from locking excisting ones
            _MbrAccessUserName = "";
            _MbrAccessPassword = "";
		}

        private string GetSabreConfigFileName()
        {
            string sabreIniFileName;
            const string AIRGROUP_DEPLOY_TYPE_ENV_VAR = "AIRGROUP_DEPLOY_TYPE";
            var deployType = Environment.GetEnvironmentVariable(AIRGROUP_DEPLOY_TYPE_ENV_VAR); // should have these values: DEV,TEST,QA,PROD
            switch (deployType.ToUpper())
            {
                case "DEV":
                    sabreIniFileName = "RfndSABRE_DEV.INI";
                    break;
                case "TEST":
                    sabreIniFileName = "RfndSABRE_TEST.INI";
                    break;
                case "QA":
                    sabreIniFileName = "RfndSABRE_QA.INI";
                    break;
                case "PROD":
                    sabreIniFileName = "RfndSABRE_PROD.INI";
                    break;
                default:
                    throw new ConfigurationErrorsException($"Expected environment variable AIRGROUP_DEPLOY_TYPE to have one of these values: DEV,TEST,QA,PROD. Actual:{deployType}");
            }
            return sabreIniFileName;
        }

		public string ErrorSMTPServer
		{
			get { return _ErrorSMTPServer; }
			set { _ErrorSMTPServer = value; }
		}

		public string HostAddress
		{
			get { return _HostAddress; }
			set { _HostAddress = value; }
		}


        public string ImageFTPAccount
		{
			get { return _ImageFTPAccount; }
			set { _ImageFTPAccount = value; }
		}


		public string ImageFTPPassword
		{
			get { return _ImageFTPPassword; }
			set { _ImageFTPPassword = value; }
		}


        public string SSHHostKeyFingerprint
        {
            get { return _SSHHostKeyFingerprint; }
            set { _SSHHostKeyFingerprint = value; }
        }
        public string SSHPrivateKeyPath
        {
            get { return _SSHPrivateKeyPath; }
            set { _SSHPrivateKeyPath = value; }
        }

        public string MvsDestFile
		{
			get { return _MvsDestFile; }
			set { _MvsDestFile = value; }
		}


		public string LocalJclFile
		{
			get { return _LocalJclFile; }
			set { _LocalJclFile = value; }
		}


		public string SiteSubmit
		{
			get { return _SiteSubmit; }
			set { _SiteSubmit = value; }
		}


		public string MvsJclFile
		{
			get { return _MvsJclFile; }
			set { _MvsJclFile = value; }
		}

		public string MbrAccessURL
		{
			get { return _MbrAccessURL; }
			set { _MbrAccessURL = value; }
		}

		public ILog logger
		{
			get { return _logger; }
			set { _logger = value; }
		}

		public string SabreFileName
		{
			get { return _SabreFileName; }
			set { _SabreFileName = value; }
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


		public string ErrorAddressesFriendlyFrom
		{
			get { return _ErrorAddressesFriendlyFrom; }
			set { _ErrorAddressesFriendlyFrom = value; }
		}

		public string ErrorAddressesFrom
		{
			get { return _ErrorAddressesFrom; }
			set { _ErrorAddressesFrom = value; }
		}

		public string ErrorAddressesTo
		{
			get { return _ErrorAddressesTo; }
			set { _ErrorAddressesTo = value; }
		}

		public string ErrorAddressesCc
		{
			get { return _ErrorAddressesCc; }
			set { _ErrorAddressesCc = value; }
		}

		public string ErrorAddressesBcc
		{
			get { return _ErrorAddressesBcc; }
			set { _ErrorAddressesBcc = value; }
		}

		public string ErrorAddressesError
		{
			get { return _ErrorAddressesError; }
			set { _ErrorAddressesError = value; }
		}

		public string ErrorSubject
		{
			get { return _ErrorSubject; }
			set { _ErrorSubject = value; }
		}


        public string MbrAccessPassword
        {
            get { return _MbrAccessPassword; }
            set { _MbrAccessPassword = value; }
        }


        public string MbrAccessUserName
        {
            get { return _MbrAccessUserName; }
            set { _MbrAccessUserName = value; }
        }
        

	}
}
