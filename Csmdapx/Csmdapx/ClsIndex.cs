using System.Collections.Generic;

namespace Csmdapx
{
    public static class ClsIndex
    {
        public static int MaxPage;
        public static int CrrentPage;
        public static int RegPage;
        public static int Archid;
        public static string ArchPos;
        public static string ScanFilePath;
        public static string ScanFilePathtmp;

        public static int RgbR = 0;
        public static int RgbG = 0;
        public static int RgbB = 0;
        public static int Sc = 20;


        public static List<string> Lsinikeys = new List<string>();
        public static List<string> lsinival = new List<string>();

        public static List<string> lsSqlOper = new List<string>();
        public static List<string> lssqlOpernum = new List<string>();

        public static string keystr= "";
        public static bool task { get; set; }
    }
}
