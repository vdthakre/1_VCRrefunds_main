using System;
using System.Collections.Generic;
using System.Text;
using ASHosts.Interop;
using AlaskaAir.Reservations.Core;
using AlaskaAir.Reservations.Framework;
using AlaskaAir.Reservations.Host;


namespace VCRrefunds
{
	public class VCRrefundQueueManager : IVCRrefundQueueManager
	{
		#region Internal Variables

		
		protected RefundAppVariables _AppVariables;
		protected ASHosts.Interop.ASHostSession _HostConnection;
		protected SabreManager _SabreManager;
		protected IVCRrefundQueueTranslator _QueueTranslator;

		#endregion

		#region Constructors

		//We don't want to create one of this without having all the info
			protected VCRrefundQueueManager()
			{

			}

			public VCRrefundQueueManager(RefundAppVariables InitVariables)
			{
				
				_AppVariables = InitVariables;
				
				_HostConnection = new ASHostSession();
				
				_SabreManager = new SabreManager();

				_SabreManager.SetHostConnection(_HostConnection);
				_QueueTranslator = new VCRrefundQueueTranslator(InitVariables);

				//Check that we have everything that we need
				CheckInitVariables(InitVariables);

			}

		#endregion

		#region Public Methods

			public void SetHostSession(ASHostSession session)
			{
				_HostConnection = session;
				_SabreManager.SetHostConnection(session);
			}

			public RefundItem OpenRefundQueue()
			{
				RefundItem result = null;
				string sabreResult = "";

				//Open the queu and get the first item
				if(!_SabreManager.QueueOpen(_AppVariables.QueueName, ref sabreResult))
					return result;

				if(sabreResult.IndexOf("END OF DISPLAY FOR REQUESTED DATA") > -1 || sabreResult.IndexOf("QUEUE SELECTED WAS EMPTY") > -1)
				{
					//Nothing in the queue we are done.
					return result;
				}

				//translate the item
				result = _QueueTranslator.TranslateFromQueue(sabreResult);

				return result;
				
			}

			public bool PutLinesIntoQueue(List<string> lines, string Queue)
			{
				//assume the worst 
				bool result = false;

				if( Queue.Length < 1 || lines.Count < 1)
				{
					return result;
				}

				foreach( string str in lines)
				{
					_SabreManager.Send5Message(str);
				}

				_SabreManager.PushToQueue(Queue);
				_SabreManager.QueueExit();

				result = true;

				return result;
			}

			public RefundItem RemoveAndGetNextItem()
			{
				RefundItem result = null;
				string sabreResult = "";

				//Open the queu and get the first item
				if(!_SabreManager.QueueRemove( ref sabreResult))
					return result;

				//No need to try if there is nothing there 
				if(sabreResult == null)
				{
					return result;
				}				

				//Todo throw exception
				if(sabreResult.IndexOf("END OF DISPLAY FOR REQUESTED DATA") > -1 || sabreResult.IndexOf("QUEUE SELECTED WAS EMPTY") > -1)
				{
					return result;
				}

				//translate the item
				result = _QueueTranslator.TranslateFromQueue(sabreResult);

				return result;

			}

			public RefundItem IgnoreAndGetNextItem()
			{
				RefundItem result = null;
				string sabreResult = "";

				//Open the queu and get the first item
				if(!_SabreManager.QueueIgnore(ref sabreResult))
					return result;

				if(sabreResult.IndexOf("END OF DISPLAY FOR REQUESTED DATA") > -1 || sabreResult.IndexOf("QUEUE SELECTED WAS EMPTY") > -1)
				{
					return result;
				}

				//translate the item
				result = _QueueTranslator.TranslateFromQueue(sabreResult);

				return result;

			}

			public bool CheckInitVariables(RefundAppVariables InitVariables)
			{

				bool result = false;			

				//If we don't have a logger we cannot continue
				if(InitVariables.logger == null)
				{
					ArgumentNullException e = new ArgumentNullException("Logger", "Missing system logger");
					throw e;
				}

				//We are done
				return result;

			}

			public bool ExitQueue()
			{
				bool result = false;

				if(!_SabreManager.QueueExit())
					return result;

				result = true;
				
				return result;
			}
			//Todo
			public void CloseConnection()
			{
				//Init the session
				_AppVariables.logger.Debug("Closing ASHost session ");
				
				_HostConnection.Close();
				
			}
			

			//Todo
			public bool StablishConnection()
			{
				bool result = false;
				long iRC;

				//Init the session
				_AppVariables.logger.Info("Creating ASHost session ");
				
				iRC = _HostConnection.SessionInit(0,  _AppVariables.SabreSysPath, "Default", "");
				
				if(iRC != 0)
				{
					_AppVariables.logger.Error("Cannot Init ASHosts session");
					Exception e = new Exception("Cannot Init ASHosts session");
					throw e;
				}

				//If logging in to tsts
				string sabreResult;
				//_HostConnection.SendReceive(1, 45000, "¤¤tsts", out sabreResult);
				//Log in to Sabre
				_AppVariables.logger.Info("Logging to Sabre");
				
				iRC = _HostConnection.SignIn(0, _AppVariables.UserID, _AppVariables.UserPassword, 1);
				if(iRC != 0)
				{
					_AppVariables.logger.Error("Cannot Logging to Sabre");
					Exception e = new Exception("Cannot Logging to Sabre");
					throw e;
				}

				//_HostConnection.SendReceive(1, 45000, "QC/ETTS", out sabreResult);

				_AppVariables.logger.Info("Logged into Sabre");

				//We got this far so things should be OK
				result = true;

				return result;
			}

			public int GetTotalRecords()
			{
				int result = 0;

				//Ask the manager
				result = _SabreManager.QueueCount(_AppVariables.QueueName);

				return result;

			}
		#endregion
	}
}
