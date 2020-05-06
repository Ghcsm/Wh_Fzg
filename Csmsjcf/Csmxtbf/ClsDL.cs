using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csmsjcf
{
    public static class ClsDL
    {

        public static int Houseid { get; set; } = 1;
        public static int Zlcy { get; set; } = 0;
        public static string FilePath { get; set; } = "";
        public static string NewPath { get; set; } = "";

        public static string Zdtime { get; set; } = "";

        public static int Fanwei { get; set; } = 0;
        public static string Boxsn { get; set; } = "0";
        public static string Quhao { get; set; } = "0";
        public static string Boxsn2 { get; set; } = "0";
        public static string Archid { get; set; } = "0";
        public static string ArchFile { get; set; } = "";
        public static int xls { get; set; } = 0;
        public static string jpgpdf { get; set; } = "";
        public static int lsh { get; set; } = 0;

        public static string BoxsnTag { get; set; } = "";

        public static int Ftp { get; set; } = 0;
        public static string FtpPath { get; set; } = "";

        public static string xmlname { get; set; } = "";
        public static string ewmname { get; set; } = "";
        public static DataTable dtboxsn { get; set; } = null;
        public static List<string> Lsboxsn = new List<string>();

        public static int lx { get; set; } = 0;

        public static bool Pcbox { get; set; } = false;

        public static List<string> Lspcbox = new List<string>();



    }
}
