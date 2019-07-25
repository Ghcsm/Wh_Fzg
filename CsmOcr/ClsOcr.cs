using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsmOcr
{
    public static class ClsOcr
    {

        public static int ocr { get; set; } = 0;

        public static int Archid { get; set; } = 0;

        public static string ArchFile { get; set; } = "";

        public static int Taskzt { get; set; } = 0;

        public static DataTable dt { get; set; } = null;

        public static int SelectTask { get; set; } = -1;

        public static int Stop { get; set; } = 0;

        public static string Boxsn { get; set; } = "";
        public static string Archno { get; set; } = "";

    }
}
