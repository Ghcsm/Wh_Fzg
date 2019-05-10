using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsmCon
{
    class ClsWriteini
    {
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        public string strFilePath = Application.StartupPath + "\\Archselect.ini";//获取INI文件路径
        public string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        public void WriteInt()
        {
            try
            {
                ClsIni.strFile = Path.GetFileNameWithoutExtension(strFilePath);
                WritePrivateProfileString(ClsIni.strFile, "Archsn", ClsIni.Archbox, strFilePath);
                WritePrivateProfileString(ClsIni.strFile, "ArchQX", ClsIni.ArchNo, strFilePath);
                WritePrivateProfileString(ClsIni.strFile, "Rabchk", ClsIni.Rabchk, strFilePath);
            }
            catch { }
        }

        public void ReadIni()
        {
            if (File.Exists(strFilePath))
            {
                ClsIni.strFile = Path.GetFileNameWithoutExtension(strFilePath);
                ClsIni.Archbox = (ContentValue(ClsIni.strFile, "Archsn").ToString());
                ClsIni.ArchNo = (ContentValue(ClsIni.strFile, "ArchQX").ToString());
            }
        }
    }

    public static class ClsIni{

        public static string strFile { get; set; }
        public static string Archbox { get; set; }
        public static string ArchNo { get; set; }
        public static string Rabchk { get; set; }

    }

}
