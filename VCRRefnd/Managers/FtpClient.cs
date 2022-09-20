using System;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Reflection;

namespace VCRrefunds
{
    /// <summary>
    /// A wrapper class for .NET 4.0 FTP
    /// </summary>
    /// <remarks>
    /// This class does not hold open an FTP connection but
    /// instead is stateless: for each FTP request it
    /// connects, performs the request and disconnects.
    /// </remarks>
    public class FTPclient 
    {
		private string _hostname;
		private string _username;
		private string _password;
		private string _currentDirectory = "/";

        #region "CONSTRUCTORS"
        /// <summary>
        /// Blank constructor
        /// </summary>
        /// <remarks>Hostname, username and password must be set manually</remarks>
        public FTPclient()
        {
        }

        /// <summary>
        /// Constructor just taking the hostname
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <remarks></remarks>
        public FTPclient(string Hostname)
        {
            _hostname = Hostname;
        }

        /// <summary>
        /// Constructor taking hostname, username and password
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <param name="Username">Leave blank to use 'anonymous' but set password to your email</param>
        /// <param name="Password"></param>
        /// <remarks></remarks>
        public FTPclient(string Hostname, string Username, string Password)
        {
            _hostname = Hostname;
            _username = Username;
            _password = Password;
        }

        #endregion

		#region "Properties"

		/// <summary>
		/// Hostname
		/// </summary>
		/// <value></value>
		/// <remarks>Hostname can be in either the full URL format
		/// ftp://ftp.myhost.com or just ftp.myhost.com
		/// </remarks>
		public string Hostname
		{
			get
			{
				if(_hostname.StartsWith("ftp://"))
				{
					return _hostname;
				}
				else
				{
					return "ftp://" + _hostname;
				}
			}
			set
			{
				_hostname = value;
			}
		}

		/// <summary>
		/// Username property
		/// </summary>
		/// <value></value>
		/// <remarks>Can be left blank, in which case 'anonymous' is returned</remarks>
		public string Username
		{
			get
			{
				return (_username == "" ? "anonymous" : _username);
			}
			set
			{
				_username = value;
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		/// <summary>
		/// The CurrentDirectory value
		/// </summary>
		/// <remarks>Defaults to the root '/'</remarks>
		public string CurrentDirectory
		{
			get
			{
				//return directory, ensure it ends with /
				return _currentDirectory + ((_currentDirectory.EndsWith("/")) ? "" : "/").ToString();
			}
			set
			{
				if(!value.StartsWith("/"))
				{
					throw (new ApplicationException("Directory should start with /"));
				}
				_currentDirectory = value;
			}
		}

		#endregion

        #region "Upload: File transfer TO ftp server"

        /// <summary>
        /// Copy a local file to the FTP server
        /// </summary>
        /// <param name="localFilename">Full path of the local file</param>
        /// <param name="targetFilename">Target filename, if required</param>
        /// <returns></returns>
        /// <remarks>If the target filename is blank, the source filename is used
        /// (assumes current directory). Otherwise use a filename to specify a name
        /// or a full path and filename if required.</remarks>
        public void Upload(string localFilename, string targetFilename)
        {
            //1. check source
            if (!File.Exists(localFilename))
            {
                throw (new ApplicationException("File " + localFilename + " not found"));
            }
            //copy to FI
            FileInfo fi = new FileInfo(localFilename);

            Upload(fi, targetFilename);
        }

		/// <summary>
		/// Append a local file to the FTP server
		/// </summary>
		/// <param name="localFilename">Full path of the local file</param>
		/// <param name="targetFilename">Target filename, if required</param>
		/// <returns></returns>
		/// <remarks>If the target filename is blank, the source filename is used
		/// (assumes current directory). Otherwise use a filename to specify a name
		/// or a full path and filename if required.</remarks>
		public void Append(string localFilename, string targetFilename)
		{
			//1. check source
			if(!File.Exists(localFilename))
			{
				throw (new ApplicationException("File " + localFilename + " not found"));
			}
			//copy to FI
			FileInfo fi = new FileInfo(localFilename);

			Append(fi, targetFilename);
		}

        /// <summary>
        /// Upload a local file to the FTP server
        /// </summary>
        /// <param name="fi">Source file</param>
        /// <param name="targetFilename">Target filename (optional)</param>
        /// <returns></returns>
		public void EraseFromDisk(string localFilename)
		{
           if (!File.Exists(localFilename))
            {
                throw (new ApplicationException("File " + localFilename + " not found"));
            }

			FileInfo fileInfo = new FileInfo(localFilename);

			fileInfo.Delete();
		}

		private static bool MyCertValidationCb(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		  {
			  return true;
		  }


		private void Upload(FileInfo fileInfo, string targetFilename)
        {
            //copy the file specified to target file: target file can be full path or just filename (uses current dir)

            //1. check target
            string target;
           
                // treat as filename only, use current directory
            target = CurrentDirectory + targetFilename;
          
            string URI = Hostname + target;
            //perform copy
            System.Net.FtpWebRequest ftp = GetRequest(URI);
			
            //Set request to upload a file in binary
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
			ftp.UseBinary = true;
			ftp.UsePassive = false;

			// see if we should be using ssl/
			//if(AutoConfigure.Get("SendFTPOverSSL").ToUpper() == "TRUE")
			//{
			//    ftp.UsePassive = true;
			//    ftp.EnableSsl = true;
			//    ServicePointManager.ServerCertificateValidationCallback = delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
			//}

			//// This approves any Server Certificate (self-signed)
			
			//Notify FTP of the expected size
            ftp.ContentLength = fileInfo.Length;
			// Provide the WebPermission Credintials
			ftp.Credentials = new NetworkCredential(_username, _password);
			
			//create byte array to store: ensure at least 1 byte!
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //open file for reading
            using (FileStream fs = fileInfo.OpenRead())
            {
                try
                {
                    //open request to send
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                       rs.Close();
                   }
                }
				catch (Exception ex)
                {
					throw ex;
                }
                finally
                {
                    //ensure file closed
                    fs.Close();
                }
            }

           ftp = null;
            }

         private static void SetMethodRequiresCWD()
        {
            Type requestType = typeof(FtpWebRequest);
            FieldInfo methodInfoField = requestType.GetField("m_MethodInfo", BindingFlags.NonPublic | BindingFlags.Instance);
            Type methodInfoType = methodInfoField.FieldType;


            FieldInfo knownMethodsField = methodInfoType.GetField("KnownMethodInfo", BindingFlags.Static | BindingFlags.NonPublic);
            Array knownMethodsArray = (Array)knownMethodsField.GetValue(null);

            FieldInfo flagsField = methodInfoType.GetField("Flags", BindingFlags.NonPublic | BindingFlags.Instance);

            int MustChangeWorkingDirectoryToPath = 0x100;
            foreach (object knownMethod in knownMethodsArray)
            {
                int flags = (int)flagsField.GetValue(knownMethod);
                flags |= MustChangeWorkingDirectoryToPath;
                flagsField.SetValue(knownMethod, flags);
            }
        }

		private void Append(FileInfo fileInfo, string targetFilename)
		{
            //copy the file specified to target file: target file can be full path or just filename (uses current dir)

            //1. check target
            string target;

            // treat as filename only, use current directory 
            //(also make it into an absolute path instead of relative C# sends a / to the mainframe
            // and the mainframe doesn't like it.
            target = "/%2F" + targetFilename;

            string URI = Hostname + target;

            //System.Net.FtpWebRequest class behaves differently in .Net Framework 4 vs .Net Framework 3.5 with CWD commands
            SetMethodRequiresCWD();

            //perform copy			
            System.Net.FtpWebRequest ftp = GetRequest(URI);

            //Set request to upload a file in binary
            ftp.Method = System.Net.WebRequestMethods.Ftp.AppendFile;
            ftp.UseBinary = false;
            ftp.UsePassive = false;


            try
            {

                StreamReader sourceStream = new StreamReader(fileInfo.Directory + "\\" + fileInfo.Name);
                byte[] fileContents = System.Text.Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();

                //Notify FTP of the expected size
                ftp.ContentLength = fileContents.Length;

                //Provide the WebPermission Credintials
                ftp.Credentials = new NetworkCredential(_username, _password);

                //Get the destination stream
                Stream rs = ftp.GetRequestStream();

                //Write the data
                rs.Write(fileContents, 0, fileContents.Length);

                rs.Close();
            }

			catch(Exception ex)
			{				
				throw ex;
			}

            ftp = null;
		}

        #endregion

        #region "private supporting fns"

		/// <summary>
		/// Get the basic FtpWebRequest object with the
		/// common settings and security
		/// </summary>
		/// <param name="URI"></param>
		/// <returns></returns>
		private FtpWebRequest GetRequest(string URI)
		{
			//create request
			FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
			//Set the login details
			result.Credentials = GetCredentials();
			//Do not keep alive (stateless mode)
			result.KeepAlive = false;
			return result;
		}

        /// <summary>
        /// Get the credentials from username/password
        /// </summary>
        private System.Net.ICredentials GetCredentials()
        {
            return new System.Net.NetworkCredential(Username, Password);
        }

        /// <summary>
        /// returns a full path using CurrentDirectory for a relative file reference
        /// </summary>
        private string GetFullPath(string file)
        {
            if (file.Contains("/"))
            {
                return AdjustDir(file);
            }
            else
            {
                return this.CurrentDirectory + file;
            }
        }

        /// <summary>
        /// Amend an FTP path so that it always starts with /
        /// </summary>
        /// <param name="path">Path to adjust</param>
        /// <returns></returns>
        /// <remarks></remarks>
		private string AdjustDir(string path)
		{
			return ((path.StartsWith("/")) ? "" : "/").ToString() + path;
		}

        #endregion
    }
}

