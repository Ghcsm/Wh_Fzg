using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmxxbl
{
    public static class ClsWriteini
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

        public static string strFilePath = Application.StartupPath + "\\Archselect.ini";//获取INI文件路径
        public static string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        public static void WriteInt(string zhi)
        {
            try {
                string strFile = Path.GetFileNameWithoutExtension(strFilePath);
                WritePrivateProfileString("Archselect", "Etag", zhi, strFilePath);
            } catch { }
        }

        public static string ReadIni()
        {
            try {
                string strFile = Path.GetFileNameWithoutExtension(strFilePath);
                string enter = (ContentValue(strFile, "Etag").ToString());
                if (enter.Trim().Length<=0)
                return "false";
                return enter;
            } catch {
                return "false";
            }
        }
    }
}
