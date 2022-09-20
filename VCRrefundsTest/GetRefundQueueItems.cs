using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ASHosts.Interop;
using AlaskaAir.Configuration;
using AlaskaAir.Framework;
using AlaskaAir.Reservations.Host;

namespace VCRrefunds
{
	public partial class GetRefundQueueItems : Form
	{
		protected ASHosts.Interop.ASHostSession _HostConnection;
		protected SabreManager _SabreManager;
		protected string _resultFileName;

		public GetRefundQueueItems()
		{
			InitializeComponent();

			_resultFileName = "" + DateTime.Now.ToString("MMddyyyy") + ".txt";

			txtFileName.Text = _resultFileName;
		}

		private void cmdGetRecords_Click(object sender, EventArgs e)
		{
			string nextItem = "";
			StringBuilder results = new StringBuilder();

			try
			{

				if(!StablishConnection())
					MessageBox.Show("Unable to connect to Sabre");

				_resultFileName = txtFileName.Text;

				_SabreManager = new SabreManager();

				_HostConnection.RecordScript(_resultFileName);

				_SabreManager.SetHostConnection(_HostConnection);

				string queueName = txtQueueName.Text;

				int queueCount = _SabreManager.QueueCount(queueName);

				DisplayAndSave("\nTotal items in the queue: " + queueCount);

				_SabreManager.QueueOpen(queueName, ref nextItem);
				results.Append("\n" + nextItem);

				DisplayAndSave("\n" + nextItem);

				//If we don't have anything to process we move on
				for(int index = 0; index < queueCount; index++)
				{
					_SabreManager.QueueIgnore(ref nextItem);
					results.Append("\n" + nextItem);
					
				}

				txtDisplay.AppendText( results.ToString());
				_SabreManager.QueueExit();
			}
			catch(Exception ex)
			{
				txtDisplay.AppendText("Exception: " + ex.Message);
			}
			finally
			{
				_HostConnection.Close();
			}

			
		}

		protected void DisplayAndSave(string message)
		{

			txtDisplay.AppendText(message);
			WriteToFile(message);

		}

		protected void WriteToFile(string message)
		{
			StreamWriter SW;
			SW = File.AppendText(_resultFileName);
			SW.WriteLine(message);
			SW.Close();
		}

		//Todo
		public bool StablishConnection()
		{
			bool result = false;
			long iRC;

			//Init the session
			txtDisplay.AppendText(" Creating ASHost session ");

			_HostConnection = new ASHostSession();

			var initVariables = new RefundAppVariables();
			iRC = _HostConnection.SessionInit(0, System.Windows.Forms.Application.StartupPath + "\\" + initVariables.SabreFileName, "Default", "");

			if(iRC != 0)
			{
				txtDisplay.AppendText("Cannot Init ASHosts session");
				Exception e = new Exception("Cannot Init ASHosts session");
				throw e;
			}

			//If logging in to tsts
			//string sabreResult;
			//_HostConnection.SendReceive(1, 45000, "¤¤tsts", out sabreResult);
			//Log in to Sabre
			txtDisplay.AppendText("Logging to Sabre");

			iRC = _HostConnection.SignIn(0, txtSabreId.Text, txtSabrePswd.Text, 1);
			if(iRC != 0)
			{
				txtDisplay.AppendText("Cannot Logging to Sabre");
				Exception e = new Exception("Cannot Logging to Sabre");
				throw e;
			}

			//_HostConnection.SendReceive(1, 45000, "QC/ETTS", out sabreResult);

			txtDisplay.AppendText("Logged into Sabre");

			//We got this far so things should be OK
			result = true;

			return result;
		}


	}
}