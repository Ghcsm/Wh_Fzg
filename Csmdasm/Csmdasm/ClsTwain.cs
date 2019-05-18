using System.Collections.Generic;
using System.Windows.Forms;

namespace Csmdasm
{
    public static class ClsTwain
    {
        public static int MaxPage;
        public static int RegPage;
        public static int Archid;
        public static string ArchPos;
        public static bool Scanbool = false;
        public static string ScanFileTmp;
      
        public static List<string> Lsinikeys=new List<string>();
        public static List<string> lsinival = new List<string>();

        public static List<string> lsSqlOper = new List<string>();
        public static List<string> lssqlOpernum=new List<string>();

        public static string keystr = "";
    }
}
