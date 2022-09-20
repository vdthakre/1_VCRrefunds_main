using System;
using System.IO;
using WinSCP;

namespace VCRrefunds
{
	public class AccountFileManager : IAccountFileManager
	{
		#region Internal Variables

		protected RefundAppVariables _AppVariables;
		protected string _resultFileName;
        public string SFtpFilename;
		public string ResultFileName
		{
			get { return _resultFileName; }			
		}
	

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
				InitVariables.logger.Info("Result file" + _resultFileName);

			}

		#endregion

		#region Public Methods
		
		public void AddAccountingLine(string processLine)
		{
			//Write to the file
			WriteToFile(processLine);

			//go back
			return;
		}

		//TODO
		public bool SendResultFile()
		{
			bool result = false;


			try
			{
                //Clean up
                CleanupDataFile(_AppVariables.ResultPath + "\\RF" + DateTime.Now.AddDays(-10).ToString("yyMMdd") + ".CMP");
                CleanupDataFile(_AppVariables.ResultPath + "\\MPERR" + DateTime.Now.AddDays(-10).ToString("yyMMdd") + ".SNT");

                //Get the name of the file to be sent
                string sVCRFileToday = _AppVariables.ResultPath + "\\RF" + DateTime.Now.ToString("yyMMdd") + ".VCR";
                SFtpFilename= "/AS_RF_Refunds" + DateTime.Now.ToString("yyMMdd")+".txt";
               
                //check that the file indeed exists.
                if (!File.Exists(sVCRFileToday))
				{
					_AppVariables.logger.Fatal("Failure: File " + sVCRFileToday + " does not exist. Aborting process.");
					return result;
				}

                //Send the file
                ////if(!FtpResultFile(_AppVariables, sVCRFileToday))
                ////	return result;

                if (!SftpResultFile(_AppVariables, sVCRFileToday))  // new code Divya
                   	return result;

                    //Rename the current file
                    RenameFile(sVCRFileToday, _AppVariables.ResultPath + "\\RF" + DateTime.Now.ToString("yyMMdd") + ".CMP");

   				//We got this far thing should have gone well
				result = true;


			}
			catch(Exception e)
			{
				_AppVariables.logger.Fatal("Failure: Exception while Sending file to accounting : " + e.Message + " " + e.StackTrace );
			}
			return result;
		}

		public bool FtpResultFile( RefundAppVariables variables, string sVCRFileName)
		{
			bool result = false;

			//Get the ftp client going
			FTPclient ftpClient = new FTPclient(variables.HostAddress,
				variables.ImageFTPAccount,
				variables.ImageFTPPassword);

			//check that the file indeed exists.
			if(!File.Exists(sVCRFileName))
			{
				variables.logger.Info("Failure: File " + sVCRFileName + " does not exist. Aborting process.");
				return result;
			}			
			//Send the file
			ftpClient.Append(sVCRFileName, variables.MvsDestFile);

			_AppVariables.logger.Info("File " + sVCRFileName + ". Sent to: " + variables.HostAddress);

			//We made it this far thing should be OK
			result = true;

			return result;
		}

		#endregion

		#region protected

		protected bool RenameFile(string FilepathOld, string FilepathNew)
		{
			bool result = false;

			FileInfo fi = new FileInfo(FilepathOld);
			FileInfo fnew = new FileInfo(FilepathNew);

			_AppVariables.logger.Info("Renaming file: " + FilepathOld + " with name " + FilepathNew);

			if(fi.Exists)			
			{				
				if(!fnew.Exists)
				{					
					fi.MoveTo(FilepathNew);
				}
				else
				{					
					WriteToFile(FilepathNew);
				}
			}

			//things went well
			result = true;

			return result;
		}

        protected bool CleanupDataFile(string filepath)
        {
            bool result = false;

            FileInfo fi = new FileInfo(filepath);
 
            _AppVariables.logger.Info("Rolling delete old data file.");

            if (fi.Exists)
            {
                fi.Delete();
            }

            result = true;

            return result;
        }

		protected void WriteToFile( string message)
		{
			StreamWriter SW;
			SW = File.AppendText(_AppVariables.ResultPath + "\\" + _resultFileName);
			System.Console.WriteLine(" Saving to file " + _AppVariables.ResultPath + "\\" + _resultFileName);
			SW.WriteLine(message);
			SW.Close();
		}
		
		
		protected string GetResultFileName(DateTime thisTime)
		{
			string result = ""; 
			 
			 if(thisTime.Hour >= 12 )
			{
				thisTime = thisTime.AddDays(1);
			}

			result = "RF" + thisTime.ToString("yyMMdd") + ".VCR";

			return result;
		}

        #endregion


        public bool SftpResultFile(RefundAppVariables variables, string sVCRFileName)
        {
            bool result = false;                      

            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.Protocol = Protocol.Sftp;
            sessionOptions.HostName = variables.HostAddress ;
            sessionOptions.UserName = variables.ImageFTPAccount ;
           // sessionOptions.Password = variables.ImageFTPPassword;
            sessionOptions.SshHostKeyFingerprint = variables.SSHHostKeyFingerprint;
            sessionOptions.SshPrivateKeyPath =variables.SSHPrivateKeyPath ;
            Session session = new Session();
            session.Open(sessionOptions);          

           
            //check that the file indeed exists.
            if (!File.Exists(sVCRFileName))
            {
                variables.logger.Info("Failure: File " + sVCRFileName + " does not exist. Aborting process.");
                return result;
            }
                                   

            variables.MvsDestFile = variables.MvsDestFile+SFtpFilename;

            if (session.FileExists(variables.MvsDestFile))
            {
                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;
                TransferOperationResult transferResult;
                var res = session.RemoveFile(variables.MvsDestFile);

                //This is for Getting/Downloading files from SFTP  
                transferResult = session.GetFiles(variables.MvsDestFile, sVCRFileName, false, transferOptions);
                //This is for Putting/Uploading file on SFTP  
                transferResult = session.PutFiles(sVCRFileName, variables.MvsDestFile, false, transferOptions);
                transferResult.Check();

            }
            else
            {
                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;
                TransferOperationResult transferResult;
                //This is for Getting/Downloading files from SFTP  
                transferResult = session.GetFiles( variables.MvsDestFile, sVCRFileName, false, transferOptions);
                //This is for Putting/Uploading file on SFTP  
                transferResult = session.PutFiles(sVCRFileName,variables.MvsDestFile , false, transferOptions);
                transferResult.Check();
            }

            _AppVariables.logger.Info("File " + sVCRFileName + ". Sent to: " + variables.HostAddress);

            //We made it this far thing should be OK
            result = true;

            return result;
        }
    }
}
