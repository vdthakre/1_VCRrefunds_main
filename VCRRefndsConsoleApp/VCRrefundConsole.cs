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
	public class VCRrefundConsole
	{

		protected string hostDeployType;
		protected RefundAppVariables appVariables;
		protected ILog log;
		protected VCRrefundWorkFlow workflow;

		static int Main(string[] args)
		{

			try
			{
				VCRrefundConsole refundConsole = new VCRrefundConsole();

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
					System.Console.Write("Running test  \n");
					refundConsole.RunTestEnvironment();
					System.Console.Write("Done with Test  \n");
				}
				else
				{
					System.Console.Write("Run refunds  \n");
					refundConsole.RunRefunds();
				}
			}
			catch(Exception e)
			{
				System.Console.Write("Failure Exception : " + e.Message + "\n" + e.StackTrace);
			}
			return 0;

		}

		
		protected bool RunTestEnvironment()
		{
			bool result = false;			

			//Set the variables
			appVariables = new RefundAppVariables();

			////Get the loger
			log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("VCRrefundsLog");
			appVariables.logger = log;

			try
			{
				//Start the record
				log.Debug("***************** *****************");
				log.Debug("Starting VCRrefunds Environment Test v2.0 ");

				//Get the environment files
				if(!GetAppVariables(appVariables))
				{
					log.Fatal("Not able to access app variables");
					return result;
				}

				//Test to see if we can open Sabre and count the queu
				log.Debug("*****************");
				log.Debug("Testing Sabre Connection");
				VCRrefundQueueManager QueueManager = new VCRrefundQueueManager(appVariables);
				log.Debug("Stablishing Connection");
				if(!QueueManager.StablishConnection())
				{
					log.Fatal("Not able to connect to Sabre");
					return result;
				}

				log.Debug("*****************");
				//Get a count of the queue 
				log.Debug("Testing Queue " + appVariables.QueueName + " count: " + QueueManager.GetTotalRecords());

				log.Debug("Closing Connection");
				QueueManager.CloseConnection();
	

				//Check the version of mbracces
				RedepositMilesManager testMiles = new RedepositMilesManager(appVariables);
				log.Debug("*****************");
				log.Debug("Testing MBRaccess. Getting Web Service version : " + testMiles.GetRedepositServiceVersion());

				log.Debug("Ending Environment Testing");
				log.Debug("***************** *****************");
			}
			catch(Exception e)
			{
				log.Fatal("Failure: Exception at test " + e.Message + "\n" + e.StackTrace);
			}

			return result;

		}

		protected bool RunRefunds()
		{
			bool result = false;
			appVariables = new RefundAppVariables();			
			VCRrefundWorkFlow workflow = null;

			////Get the loger
			log4net.Config.XmlConfigurator.Configure();
			log = LogManager.GetLogger("VCRrefundsLog");

			log.Info("Starting VCRrefunds");

			appVariables.logger = log;
			
			//Check to make sure that there is no other instance running
			if(OtherInstanceRunning())
				return result;

			log.Debug("Done checking other instances");

			if(!GetAppVariables(appVariables))
				return result;

			log.Info("Done getting app variables");
			if(appVariables != null)
			{
				//Init the flow
				workflow = new VCRrefundWorkFlow(appVariables);

				//Getit running
				
				if(workflow.Execute())
				{
					//Things went well
					result = true;
				}
			}

			return result;

		}
	
		protected bool OtherInstanceRunning()
		{
			bool result = false;

			//Get the processes
			Process[] RunningProcesses = Process.GetProcessesByName("VCRRefnds");

			System.Console.Write("Number of Processes running " + RunningProcesses.Length);

			//If there is only one instance that is good.
			if(RunningProcesses.Length > 1)
			{
				result= true;
			}

			//Go back
			return result;
		}

		//TODO
		protected bool GetAppVariables(RefundAppVariables appVariables)
		{
				bool result = false;
				string setting = "";
			try
			{
				IDecryptor decrypt = new Decryptor();

				NameValueCollection mbrAccessSettings = (NameValueCollection)ConfigurationManager.GetSection("VCRRefundsSettings");

				//Get the path variables
				appVariables.SabreSysPath = System.AppDomain.CurrentDomain.BaseDirectory + appVariables.SabreFileName;
                string dataPath = Environment.GetEnvironmentVariable("AIRGROUP_DATA_ROOT");
                appVariables.ResultPath = dataPath + "\\Image\\VCRRefund";

				//Just in case let's assume we are in test
				appVariables.GroupName = "Image";
				appVariables.QueueName = "ETTS";
				appVariables.UserPassword = "dt041993";
				appVariables.UserID = "18615";

				appVariables.logger.Debug("Getting application variables");

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

				setting = mbrAccessSettings.Get("MbrAccessURL");
				if(setting != null)
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

				setting = mbrAccessSettings.Get("MbrAccessPassword");
				if(setting != null)
				{
					setting = decrypt.Decrypt(setting);
					appVariables.MbrAccessPassword = setting;
				}

				setting = mbrAccessSettings.Get("MbrAccessUserName");
				if(setting != null)
				{
					setting = decrypt.Decrypt(setting);
					appVariables.MbrAccessUserName = setting;
				}


				appVariables.logger.Debug("Checking app variables");

				if(appVariables.QueueName.Trim().Length < 1)
				{
					appVariables.logger.Fatal("Failure: Missing VCRrefunds app variable QueueName");
					result = false;
					return result;

				}

				if(appVariables.UserID.Trim().Length < 1)
				{
					appVariables.logger.Fatal("Failure: Missing VCRrefunds app variable UserID");
					result = false;
					return result;

				}

				if(appVariables.UserPassword.Trim().Length < 1)
				{
					appVariables.logger.Fatal("Failure: Missing VCRrefunds app variable UserPassword");
					result = false;
					return result;

				}

				if(appVariables.MbrAccessURL.Trim().Length < 1)
				{
					appVariables.logger.Fatal("Failure: Missing VCRrefunds app variable MbrAccessURL");
					result = false;
					return result;

				}
			}
			catch(Exception e)
			{
				System.Console.Write("Failure Exception : " + e.Message + "\n" + e.StackTrace);
			}
			result = true;
			return result;

		}


		protected void LoadHostDeployType()
		{
			hostDeployType = Environment.GetEnvironmentVariable("AIRGROUP_DEPLOY_TYPE");
		}

	}
}
