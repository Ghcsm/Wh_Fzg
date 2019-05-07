using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class T_addModule
    {
        public static string T_id { get; set; }
        public static string T_sn { get; set; }
        public static string T_time { get; set; }
        public static string T_timecs { get; set; }
        public static string T_moduleName { get; set; }
        public static string T_moduleChName { get; set; }
        public static string T_moduleInt { get; set; }
        public static int T_moduleImgIdx { get; set; }
        public static string T_moduleFileName { get; set; }

        public static List<string> ModuleColSet = new List<string>();


    }
}
