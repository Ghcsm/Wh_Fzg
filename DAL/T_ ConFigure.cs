using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class T_ConFigure
    {
      
        public const string ScanTempFile = "ScanTemp.Tif";
        public const string ScanTempFiletmp = "Scan1.Tif";
        public static string LocalTempPath = @"c:\Temp";
        public static string TmpScan = "Scan";
        public static string TmpIndex = "Index";
        public static string TmpSave = "Save";
        public static string gArchScanPath { get; set; }
        public static string FtpIP { get; set; }
        public static int FtpPort { get; set; }
        public static string FtpUser { get; set; }
        public static string FtpPwd { get; set; }
        public static string FtpArchScan { get; set; }
        public static string FtpArchIndex { get; set; }
        public static string FtpArchSave { get; set; }
        public static string FtpArchUpdate { get; set; }
        public static int FtpStyle { get; set; }
        public static string FtpFwqPath { get; set; }


        public static string FtpTmp { get; set; }
        public static string FtpTmpPath { get; set; }
        public static int FtpBakimgFwq { get; set; }
        public static int FtpBakimgBd { get; set; }


        public static string IPAddress { get; set; }

        public static string Moid { get; set; }
        public static string Mosn { get; set; }
        public static string Motm { get; set; }
        public static bool Bgsoft { get; set; }
        public static string Mwtime { get; set; }



        public enum ArchStat
        {
            无 = 0,
            扫描中 = 1,
            质检退回 = 2,
            扫描完 = 3,
            排序中 = 4,
            排序完 = 5,
            质检中 = 6,
            质检完 = 7,
            已关联 = 8,
            总检完=9
        }

        public static string SfName = "档案数字化管理系统";

        public static string SfCoName = "档案数字化管理系统";

        public static string Imgid = "211883860501001421116010749430779";
        //"5gyXVVG7v5NU4MGCWs/OGNwTVozQ06KZm443PVYA0nD295v7IGaC8g==";
    }

}
