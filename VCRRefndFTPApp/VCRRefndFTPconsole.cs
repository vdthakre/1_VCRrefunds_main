using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using AlaskaAir.Framework.Security.Cryptography;

namespace VCRrefunds

{
	class VCRRefndFTPconsole
	{
		static void Main(string[] args)
		{
			VCRRefndFTP refundFTP = new VCRRefndFTP();
			
			//We could be running in test mode
			bool isTestEnv = false;

			//So check
			if(args.Length > 0)
				if(args[0].ToUpper().Equals("TESTENV"))
					isTestEnv = true;
			
			System.Console.Write("Starting Processing \n");
			//Run for real or test the environment
			if(isTestEnv)
			{
				System.Console.Write("Starting Environment Test \n");
				refundFTP.RunTestEnvironment();
			}
			else
			{
				System.Console.Write("Starting FTP Processing \n");
				refundFTP.RunFTPApp();
			}
			System.Console.Write("Done with Processing \n");
			return;
			

		}
	}

	public class VCRRefndFTP
	{
		protected string hostDeployType;
		protected RefundAppVariables appVariables;
		protected ILog log;
		protected VCRrefundWorkFlow workflow;

		public bool RunFTPApp()
		{
			bool result = false;
			appVariables = new RefundAppVariables();
			VCRrefundFTPWorkFlow workflow = null;

			////Get the loger
			log4net.Config.XmlConfigurator.Configure();
			log = LogManager.GetLogger("VCRrefundsLog");

			log.Info("Starting VCRrefundsFTP");
			log.Info("Info VCRrefundsFTP");
			appVariables.logger = log;

			//Check to make sure that there is no other instance running
			if(!GetAppVariables(appVariables))
				return result;

			//if we got all the variables we nees
			if(appVariables != null)
			{
				//Init the flow
				workflow = new VCRrefundFTPWorkFlow(appVariables);

				//Getit running
				if(workflow.Execute())
				{
					//Things went well
					result = true;
				}
			}

			return result;

		}

		public bool RunTestEnvironment()
		{
			bool result = false;
			VCRrefundWorkFlow workflow = null;

			//Set the variables
			appVariables = new RefundAppVariables();

			////Get the loger
			log4net.Config.XmlConfigurator.Configure();
			log = LogManager.GetLogger("VCRrefundsTest");
			appVariables.logger = log;

			//Start the record
			log.Debug("**********************************");
			log.Debug("Starting VCRrefundsFTP Environment Test");

			//Get the environment files
			if(!GetAppVariables(appVariables))
			{
				log.Fatal("Not able to access app variables");
				return result;
			}

			//Test to see if we can FTP 
			log.Debug("*****************");
			log.Debug("Testing FTP Connection");

			AccountFileManager AccountManager = new AccountFileManager(appVariables);
			log.Debug("p1");
			appVariables.MvsDestFile = "VCRrefEnvironmentTest.log";
			log.Debug("p2");
			if(!AccountManager.FtpResultFile(appVariables, appVariables.MvsDestFile))
			{
				log.Fatal("Not able to FTP test file");
				return result;
			}

			log.Debug("*****************");
			log.Debug("Testing Email Connection");
			//Send and email
			RedepositErrorHandler errorHandler = new RedepositErrorHandler(appVariables);
			appVariables.ErrorAddressesBcc = "";
			appVariables.ErrorAddressesCc = "";
			appVariables.ErrorAddressesTo = "alejandro.camargo@alaskaair.com";
			errorHandler.emailErrorReport(appVariables, "VCRrefEnvironmentTest.log");

			log.Debug("Ending Environment FTP Testing");
			log.Debug("**********************************");
			return result;

		}

		protected bool GetAppVariables(RefundAppVariables appVariables)
		{

			bool result = false;
			string setting = "";
            NameValueCollection mbrAccessSettings = (NameValueCollection) ConfigurationManager.GetSection("VCRRefundsSettings");
            IDecryptor decrypt = new Decryptor();

			//Get the path variables
			appVariables.SabreSysPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string dataPath = Environment.GetEnvironmentVariable("AIRGROUP_DATA_ROOT");
            appVariables.ResultPath = dataPath + "\\Image\\VCRRefund";

			//Just in case let's assume we are in test
			appVariables.GroupName = "Image";
			appVariables.QueueName = "ETTS";
			appVariables.UserPassword = "R0b0tic";
			appVariables.UserID = "6007";

			//Get the values
			setting = mbrAccessSettings.Get("GroupName");
			if(setting != null)
				appVariables.GroupName = setting;

			setting = mbrAccessSettings.Get("QueueName");
			if(setting != null)
				appVariables.QueueName = setting;

			setting = mbrAccessSettings.Get("UserPassword");
			if(setting != null)
				appVariables.UserPassword = setting;

			setting = mbrAccessSettings.Get("UserID");
			if(setting != null)
				appVariables.UserID = setting;

			setting = mbrAccessSettings.Get("MbrAccessPassword");
            if (setting != null)
            {
                setting = decrypt.Decrypt(setting);
                appVariables.MbrAccessPassword = setting;
            }

            setting = mbrAccessSettings.Get("MbrAccessUserName");
            if (setting != null)
            {
                setting = decrypt.Decrypt(setting);
                appVariables.MbrAccessUserName = setting;
            }

            setting = mbrAccessSettings.Get("MbrAccessURL");
            if (setting != null)
                appVariables.MbrAccessURL = setting + "?subscription-key=" + appVariables.MbrAccessPassword;

			setting = mbrAccessSettings.Get("HostAddress");
			if(setting != null)
				appVariables.HostAddress = setting;

			setting = mbrAccessSettings.Get("ImageFTPAccount");
			if(setting != null)
				appVariables.ImageFTPAccount = setting;

			setting = mbrAccessSettings.Get("ImageFTPPassword");
			if(setting != null)
				appVariables.ImageFTPPassword = setting;

            setting = mbrAccessSettings.Get("SshHostKeyFingerprint");
            if (setting != null)
                appVariables.SSHHostKeyFingerprint = setting;

            setting = mbrAccessSettings.Get("SshPrivateKeyPath");
            if (setting != null)
                appVariables.SSHPrivateKeyPath = setting;
            
            setting = mbrAccessSettings.Get("MvsDestFile");
			if(setting != null)
				appVariables.MvsDestFile = setting;

			setting = mbrAccessSettings.Get("LocalJclFile");
			if(setting != null)
				appVariables.LocalJclFile = setting;

			setting = mbrAccessSettings.Get("SiteSubmit");
			if(setting != null)
				appVariables.SiteSubmit = setting;

			setting = mbrAccessSettings.Get("MvsJclFile");
			if(setting != null)
				appVariables.MvsJclFile = setting;

			setting = mbrAccessSettings.Get("ErrorAddressesFriendlyFrom");
			if(setting != null)
				appVariables.ErrorAddressesFriendlyFrom = setting;

			setting = mbrAccessSettings.Get("ErrorAddressesFrom");
			if(setting != null)
				appVariables.ErrorAddressesFrom = setting;

			setting = mbrAccessSettings.Get("ErrorAddressesTo");
			if(setting != null)
				appVariables.ErrorAddressesTo = setting;

			setting = mbrAccessSettings.Get("ErrorAddressesCc");
			if(setting != null)
				appVariables.ErrorAddressesCc = setting;

			setting = mbrAccessSettings.Get("ErrorAddressesBcc");
			if(setting != null)
				appVariables.ErrorAddressesBcc = setting;

			setting = mbrAccessSettings.Get("ErrorAddressesError");
			if(setting != null)
				appVariables.ErrorAddressesError = setting;

			setting = mbrAccessSettings.Get("ErrorSubject");
			if(setting != null)
				appVariables.ErrorSubject = setting;

			setting = mbrAccessSettings.Get("ErrorSMTPServer");
			if(setting != null)
				appVariables.ErrorSMTPServer = setting;


			if(appVariables.ErrorAddressesTo.Trim().Length < 1)
			{
				appVariables.logger.Fatal("Failure: Missing VCRrefundFTP app variable ErrorAddressesTo");
				result = false;
				return result;

			}

			if(appVariables.ErrorSMTPServer.Trim().Length < 1)
			{
				appVariables.logger.Fatal("Failure: Missing VCRrefundFTP app variable SMTP server");
				result = false;
				return result;

			}

			if(appVariables.ImageFTPAccount.Trim().Length < 1)
			{
				appVariables.logger.Fatal("Failure: Missing VCRrefundFTP app variable ImageFTPAccount");
				result = false;
				return result;

			}

			if(appVariables.ImageFTPPassword.Trim().Length < 1)
			{
				appVariables.logger.Fatal("Failure: Missing VCRrefundFTP app variable ImageFTPPassword");
				result = false;
				return result;
			}

			result = true;



			//if we made it this far then it should be ok
			result = true;

			return result;
		}

		protected void LoadHostDeployType()
		{
			hostDeployType = Environment.GetEnvironmentVariable("AIRGROUP_DEPLOY_TYPE");
		}
	}
}
