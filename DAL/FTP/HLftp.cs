using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using FluentFTP;

namespace HLFtp
{
    public class HFTP
    {
        public delegate void PChangedHandle(object sender, PChangeEventArgs e);
        public delegate void PChangedHandleNum(PChangeEventArgs e);

        private string FtpUser;

        private int FtpPort;

        private string FtpPwd;

        private string FtpIp;

        // private string FtpPath;

        private FtpWebRequest FtpWr;
      
        public event HFTP.PChangedHandle PercentChane;

        public static void GetFtpInfo()
        {
            try {
                string strSql = "select * from M_FtpInfo ";
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql);
                if (dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    DAL.T_ConFigure.FtpIP = dr["IP"].ToString();
                    DAL.T_ConFigure.FtpPort = Convert.ToInt16(dr["Port"]);
                    DAL.T_ConFigure.FtpUser = dr["UserName"].ToString();
                    DAL.T_ConFigure.FtpPwd = DESEncrypt.DesDecrypt(dr["PWD"].ToString());
                    DAL.T_ConFigure.FtpArchScan = dr["ArchScan"].ToString();
                    DAL.T_ConFigure.FtpArchIndex = dr["ArchIndex"].ToString();
                    DAL.T_ConFigure.FtpArchSave = dr["ArchSave"].ToString();
                    DAL.T_ConFigure.FtpArchUpdate = dr["UpdatePath"].ToString();
                    DAL.T_ConFigure.FtpTmp = dr["FtpTmp"].ToString();
                    DAL.T_ConFigure.FtpTmpPath = dr["FtpTmpPath"].ToString();
                    DAL.T_ConFigure.FtpBakimgFwq = Convert.ToInt32(dr["FtpBakimgFwq"].ToString());
                    DAL.T_ConFigure.FtpBakimgBd = Convert.ToInt32(dr["FtpBakimgBd"].ToString());
                    DAL.T_ConFigure.FtpStyle = Convert.ToInt32(dr["FtpStyle"].ToString());
                    DAL.T_ConFigure.FtpFwqPath = dr["FtpFwqPath"].ToString();
                }
            } catch { }

        }

        public HFTP()
        {
            FtpUser = DAL.T_ConFigure.FtpUser;
            FtpIp = DAL.T_ConFigure.FtpIP;
            FtpPwd = DAL.T_ConFigure.FtpPwd;
            FtpPort = DAL.T_ConFigure.FtpPort;
        }

        #region oldff


        private bool Existfile(string filename)
        {
            long fileLength = this.GetFileLength(filename);
            return fileLength != 0L;
        }

        public bool CheckRemoteFile(string directory, string ChildDir, string FileName)
        {
            bool result;
            try {
                directory = directory + "\\" + ChildDir + "\\" + FileName;
                string fullName = this.getFullName(directory, false);
                this.Connect(fullName);
                this.FtpWr.Method = "NLST";
                this.FtpWr.UseBinary = true;
                try {
                    this.FtpWr.UseBinary = true;
                    FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                    ftpWebResponse.Close();
                    result = true;
                } catch {
                    result = false;
                }
            } catch {
                result = false;
            }
            return result;
        }


        public bool CheckFile(string directory, string ChildDir, string FileName)
        {
            try {
                directory = directory + "\\" + ChildDir + "\\" + FileName;
                if (Existfile(directory) == false) {
                    return false;
                }
                return true;
            } catch {
                return false;
            }
        }



        private bool directoryIsNull(string directory)
        {
            bool result;
            try {
                string[] fileList = this.GetFileList(directory);
                if (fileList.Length == 0) {
                    result = true;
                }
                else {
                    result = false;
                }
            } catch {
                result = false;
            }
            return result;
        }

        public bool DownLoadFile(string ServerMl, string ChildDir, string LocalFile, string FtpFile)
        {
            bool result;
            try {
                FtpFile = ServerMl + "\\" + ChildDir + "\\" + FtpFile;
                this.Connect(this.getFullName(FtpFile, false));
                this.FtpWr.KeepAlive = true;
                this.FtpWr.Method = "RETR";
                FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                Stream responseStream = ftpWebResponse.GetResponseStream();
                long fileLength = this.GetFileLength(FtpFile);
                long num = 0L;
                int num2 = 32768;
                byte[] buffer = new byte[num2];
                if (File.Exists(LocalFile)) {
                    File.Delete(LocalFile);
                }
                int i = responseStream.Read(buffer, 0, num2);
                FileStream fileStream = new FileStream(LocalFile, FileMode.Create);
                while (i > 0) {
                    fileStream.Write(buffer, 0, i);
                    if (PercentChane != null) {
                        num += (long)i;
                        PercentChane(null, new PChangeEventArgs(fileLength, num));
                    }

                    i = responseStream.Read(buffer, 0, num2);
                }
                responseStream.Close();
                fileStream.Close();
                ftpWebResponse.Close();
                result = true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        public bool SaveRemoteFile(string ServerDirectory, string ChildDir, string LocalFile, string FtpFile)
        {
            string ServerDir = ServerDirectory + "\\" + ChildDir;
            FtpFile = ServerDirectory + "\\" + ChildDir + "\\" + FtpFile;
            if (!this.CheckRemoteDir(ServerDir)) {
                this.CreateDir(ServerDir);
            }
            if (this.Existfile(FtpFile)) {
                this.KillFile(FtpFile);
            }
            bool result;
            if (!File.Exists(LocalFile)) {
                result = true;
            }
            else {
                FileInfo fileInfo = new FileInfo(LocalFile);
                if (fileInfo.Length == 0L) {
                    result = true;
                }
                else {
                    string fullName = this.getFullName(FtpFile, false);
                    this.Connect(fullName);
                    this.FtpWr.Method = "STOR";
                    long num = 0L;
                    long totalsize = this.FtpWr.ContentLength = fileInfo.Length;
                    int num2 = 32768;
                    byte[] buffer = new byte[num2];
                    FileStream fileStream = fileInfo.OpenRead();
                    try {
                        Stream requestStream = this.FtpWr.GetRequestStream();
                        for (int num3 = fileStream.Read(buffer, 0, num2); num3 != 0; num3 = fileStream.Read(buffer, 0, num2)) {
                            num += (long)num3;
                            requestStream.Write(buffer, 0, num3);
                            if (this.PercentChane != null) {
                                this.PercentChane(null, new PChangeEventArgs(totalsize, num));
                            }
                        }
                        requestStream.Close();
                        fileStream.Close();
                        result = true;
                    } catch {
                        result = false;
                    }
                }
            }
            return result;
        }
        public bool SaveRemoteFileUp(string ServerDirectory, string ChildDir, string LocalFile, string FtpFile)
        {
            string ServerDir = ServerDirectory + "\\" + ChildDir;
            FtpFile = ServerDirectory + "\\" + ChildDir + "\\" + FtpFile;
            if (!this.CheckRemoteDir(ServerDir)) {
                this.CreateDir(ServerDir);
            }
            if (this.Existfile(FtpFile)) {
                this.KillFile(FtpFile);
            }
            bool result;
            if (!File.Exists(LocalFile)) {
                result = true;
            }
            else {
                FileInfo fileInfo = new FileInfo(LocalFile);
                if (fileInfo.Length == 0L) {
                    result = true;
                }
                else {
                    string fullName = this.getFullName(FtpFile, false);
                    this.Connect(fullName);
                    this.FtpWr.Method = "STOR";
                    long num = 0L;
                    long totalsize = this.FtpWr.ContentLength = fileInfo.Length;
                    int num2 = 32768;
                    byte[] buffer = new byte[num2];
                    FileStream fileStream = fileInfo.OpenRead();
                    try {
                        Stream requestStream = this.FtpWr.GetRequestStream();
                        for (int num3 = fileStream.Read(buffer, 0, num2); num3 != 0; num3 = fileStream.Read(buffer, 0, num2)) {
                            num += (long)num3;
                            requestStream.Write(buffer, 0, num3);
                        }
                        requestStream.Close();
                        fileStream.Close();
                        result = true;
                    } catch {
                        result = false;
                    }
                }
            }
            return result;
        }

        private bool KillFile(string filename)
        {
            string fullName = this.getFullName(filename, false);
            this.Connect(fullName);
            this.FtpWr.Method = "DELE";
            this.FtpWr.UseBinary = true;
            bool result;
            try {
                FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                ftpWebResponse.Close();
                result = true;
            } catch {
                result = false;
            }
            return result;
        }


        private bool Rfile(string filename)
        {
            string fullName = this.getFullName(filename, false);
            this.Connect(fullName);
            this.FtpWr.Method = WebRequestMethods.Ftp.Rename;
            this.FtpWr.RenameTo = "Scan1.Tif";
            this.FtpWr.UseBinary = true;
            bool result;
            try {
                FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                ftpWebResponse.Close();
                result = true;
            } catch {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// ÷ÿ√¸√˚
        /// </summary>
        /// <param name="ServerDirectory"></param>
        /// <param name="ChildDir"></param>
        /// <param name="FtpFile"></param>
        /// <returns></returns>
        public bool RemFile(string ServerDirectory, string ChildDir, string FtpFile)
        {
            bool result;
            try {
                FtpFile = ServerDirectory + "\\" + ChildDir + "\\" + FtpFile;
                if (!this.Existfile(FtpFile)) {
                    return true;
                }
                else {
                    result = Rfile(FtpFile);
                }
            } catch {
                result = false;
            }
            return result;
        }


        public bool KillRemotFile(string ServerDirectory, string ChildDir, string FtpFile)
        {
            bool result;
            try {
                FtpFile = ServerDirectory + "\\" + ChildDir + "\\" + FtpFile;
                if (!this.Existfile(FtpFile)) {
                    return true;
                }
                else {
                    result = KillFile(FtpFile);
                }
            } catch {
                result = false;
            }
            return result;
        }



        public bool KillRemotDir(string ServerDirectory, string ChildDir)
        {
            bool result;
            try {
                string FtpDir = ServerDirectory + "\\" + ChildDir;
                if (!this.CheckRemoteDir(FtpDir)) {
                    result = true;
                }
                else {
                    string fullName = this.getFullName(FtpDir, true);
                    this.Connect(fullName);
                    this.FtpWr.Method = "RMD";
                    this.FtpWr.UseBinary = true;
                    try {
                        FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                        ftpWebResponse.Close();
                        result = true;
                    } catch {
                        result = false;
                    }
                }
            } catch {
                result = false;
            }
            return result;
        }

        public string[] GetFileLis(string directoryname)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string fullName = this.getFullName(directoryname, true);
            this.Connect(fullName);
            this.FtpWr.Method = "NLST";
            this.FtpWr.UseBinary = true;
            FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
            StreamReader streamReader = new StreamReader(ftpWebResponse.GetResponseStream(), Encoding.Default);
            for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine()) {
                stringBuilder.Append(text);
                stringBuilder.Append("\n");
            }
            stringBuilder.Remove(stringBuilder.ToString().LastIndexOf('\n'), 1);
            streamReader.Close();
            ftpWebResponse.Close();
            return stringBuilder.ToString().Split(new char[]
            {
                '\n'
            });
        }

        private long GetFileLength(string filename)
        {
            string fullName = this.getFullName(filename, false);
            this.Connect(fullName);
            this.FtpWr.Method = "SIZE";
            this.FtpWr.UseBinary = true;
            long result;
            try {
                FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                StreamReader streamReader = new StreamReader(ftpWebResponse.GetResponseStream(), Encoding.UTF8);
                long contentLength = ftpWebResponse.ContentLength;
                ftpWebResponse.Close();
                result = contentLength;
            } catch {
                result = 0L;
            }
            return result;
        }

        private bool CheckRemoteDir(string directory)
        {
            bool result;
            try {
                string fullName = this.getFullName(directory, true);
                this.Connect(fullName);
                this.FtpWr.Method = "NLST";
                this.FtpWr.UseBinary = true;
                try {
                    FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                    ftpWebResponse.Close();
                    result = true;
                } catch {
                    result = false;
                }
            } catch {
                result = false;
            }
            return result;
        }

        private bool CreateDir(string directory)
        {
            bool result;
            if (this.CheckRemoteDir(directory)) {
                result = true;
            }
            else {
                string fullName = this.getFullName(directory, true);
                this.Connect(fullName);
                this.FtpWr.Method = "MKD";
                try {
                    FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
                    ftpWebResponse.Close();
                    result = true;
                } catch {
                    result = false;
                }
            }
            return result;
        }

        public string[] GetFileList(string directoryname)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string fullName = this.getFullName(directoryname, true);
            this.Connect(fullName);
            this.FtpWr.Method = "NLST";
            this.FtpWr.UseBinary = true;
            FtpWebResponse ftpWebResponse = (FtpWebResponse)this.FtpWr.GetResponse();
            StreamReader streamReader = new StreamReader(ftpWebResponse.GetResponseStream(), Encoding.Default);
            for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine()) {
                stringBuilder.Append(text);
                stringBuilder.Append("\n");
            }
            stringBuilder.Remove(stringBuilder.ToString().LastIndexOf('\n'), 1);
            streamReader.Close();
            ftpWebResponse.Close();
            return stringBuilder.ToString().Split(new char[]
            {
                '\n'
            });
        }

        private string getFullName(string whatname, bool folder)
        {
            string text = whatname.Trim().Replace("\\", "/");
            string text2 = string.Concat(new string[]
            {
                "ftp://",
                this.FtpIp,
                ":",
                this.FtpPort.ToString(),
                "/",
                text
            });
            if (folder) {
                if (text.Length != 0) {
                    if (text.Substring(text.Length - 1, 1) != "/") {
                        text2 += "/";
                    }
                }
            }
            return text2;
        }

        private void Connect(string path)
        {
            this.FtpWr = (FtpWebRequest)WebRequest.Create(new Uri(path));
            this.FtpWr.UseBinary = false;
            this.FtpWr.Credentials = new NetworkCredential(this.FtpUser, this.FtpPwd);
        }


        #endregion

        #region newff

        public bool FtpMoveFile(string oldfile, string newfile, string newdir)
        {
            try {
                using (FtpClient Ftp = new FtpClient(T_ConFigure.FtpIP, T_ConFigure.FtpPort, T_ConFigure.FtpUser, T_ConFigure.FtpPwd)) {
                    if (!Ftp.DirectoryExists(newdir))
                        Ftp.CreateDirectory(newdir);
                    return Ftp.MoveFile(oldfile, newfile);
                }

            } catch {
                return false;
            }
        }

        public Task<bool> FtpUpFile(string oldfile, string newfile, string path)
        {
            return Task.Run(() =>
             {
                 try {
                     using (FtpClient Ftp = new FtpClient(T_ConFigure.FtpIP, T_ConFigure.FtpPort, T_ConFigure.FtpUser, T_ConFigure.FtpPwd)) {
                         if (!Ftp.DirectoryExists(path))
                             Ftp.CreateDirectory(path);
                         if (FtpCheckFile(newfile))
                             FtpDelFile(newfile);
                         return Ftp.UploadFile(oldfile, newfile);
                     }

                 } catch {
                     return false;
                 }
             });
        }

        public Task<bool> FtpDownFile(string loadfile, string sourfile)
        {
            return Task.Run(() =>
            {
                try {
                    using (FtpClient Ftp = new FtpClient(T_ConFigure.FtpIP, T_ConFigure.FtpPort, T_ConFigure.FtpUser,
                        T_ConFigure.FtpPwd)) {
                        int countsize = 0;
                        Progress<FtpProgress> progress = new Progress<FtpProgress>();
                        progress.ProgressChanged += (sender1, percent) =>
                        {
                           
                        };
                       
                        countsize = (int)Ftp.GetFileSize(sourfile);
                        return Ftp.DownloadFile(loadfile, sourfile, FtpLocalExists.Overwrite, FtpVerify.Delete, progress);
                    }
                } catch {
                    return false;
                }
            });
        }
        
        public bool FtpCheckFile(string file)
        {
            try {
                using (FtpClient Ftp = new FtpClient(T_ConFigure.FtpIP, T_ConFigure.FtpPort, T_ConFigure.FtpUser,
                    T_ConFigure.FtpPwd)) {
                    return Ftp.FileExists(file);
                }
            } catch {
                return false;
            }
        }

        public bool FtpDelDir(string dir)
        {
            try {
                using (FtpClient Ftp = new FtpClient(T_ConFigure.FtpIP, T_ConFigure.FtpPort, T_ConFigure.FtpUser,
                    T_ConFigure.FtpPwd)) {
                    Ftp.DeleteDirectory(dir);
                    return true;
                }
            } catch {
                return false;
            }
        }

        public bool FtpDelFile(string file)
        {
            try {
                using (FtpClient Ftp = new FtpClient(T_ConFigure.FtpIP, T_ConFigure.FtpPort, T_ConFigure.FtpUser,
                    T_ConFigure.FtpPwd)) {
                    Ftp.DeleteFile(file);
                    return true;
                }
            } catch {
                return false;
            }
        }



        #endregion
    }
}
