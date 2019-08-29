using System.Collections.Generic;

namespace CsmZchk
{
    public static class Clscheck
    {
      
        public static int MaxPage;
        public static int CrrentPage;
        public static int RegPage;
        public static int Archid;
        public static string ArchPos;
        public static string ScanFilePath;
        public static string FileNametmp;

        public static List<string> Lsinikeys = new List<string>();
        public static List<string> lsinival = new List<string>();

        public static List<string> lsSqlOper = new List<string>();
        public static List<string> lssqlOpernum = new List<string>();

        public static string keystr = "";
        public static bool task { get; set; }

        public static bool infobl { get; set; }=false;

    }
}
