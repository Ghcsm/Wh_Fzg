using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmdasm
{
   public static class ClsWriteIni
    {

        static string ImgScanFile = Path.Combine(Application.StartupPath, "ScanConfig.ini");
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);

        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);

        public static void WriteScan()
        {
            try
            {
              string strSec = Path.GetFileNameWithoutExtension(ImgScanFile);
                WritePrivateProfileString(strSec, "DouPage", IniInfo.PageDodule, ImgScanFile);
                WritePrivateProfileString(strSec, "PageSize", IniInfo.PageSize, ImgScanFile);
                WritePrivateProfileString(strSec, "Color",  IniInfo.PageColor,ImgScanFile);
                WritePrivateProfileString(strSec, "Dpi", IniInfo.PageDpi, ImgScanFile);
                WritePrivateProfileString(strSec, "ImgDirection", IniInfo.ImgDirection, ImgScanFile);
                WritePrivateProfileString(strSec, "FeedModule", IniInfo.FeedModule, ImgScanFile);
                WritePrivateProfileString(strSec, "ScanModule", IniInfo.ScanModule, ImgScanFile);
            }
            catch { }
        }

        public static void Getscan()
        {
            string strSec = Path.GetFileNameWithoutExtension(ImgScanFile);
            IniInfo.PageDodule = ContentValue(strSec, "DouPage");
            IniInfo.PageSize = ContentValue(strSec, "PageSize");
            IniInfo.PageColor = ContentValue(strSec, "Color");
            IniInfo.PageDpi = ContentValue(strSec, "Dpi");
            IniInfo.ImgDirection = ContentValue(strSec, "ImgDirection");
            IniInfo.FeedModule = ContentValue(strSec, "FeedModule");
            IniInfo.ScanModule = ContentValue(strSec, "ScanModule");
        }

        private static string ContentValue(string Section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, ImgScanFile);
            return temp.ToString();
        }

    }

    public static class IniInfo
    {
        public static string PageDodule { get; set; }
        public static string PageSize { get; set; }
        public static string PageColor { get; set; }
        public static string PageDpi { get; set; }
        public static string ImgDirection { get; set; }
        public static string FeedModule { get; set; }
        public static string ScanModule { get; set; }
    }
}
