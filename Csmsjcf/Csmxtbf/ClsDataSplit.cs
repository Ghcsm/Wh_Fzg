using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;

namespace Csmsjcf
{
  public static class ClsDataSplit
    {
        public static string ClsdirTable { get; set; }
        public static int ClsdirDirsn { get; set; }
        public static string ClsdirCol  {get;set;}
        public static string ClsdirMl  {get;set;}

        public static string ClsFileTable { get; set; }
        public static int ClsFilesn { get; set; }
        public static int ClsFileNmaecd{ get; set; }
        public static string ClsFileNameQian { get; set; }
        public static string ClsFileNameHou { get; set; }
        public static string ClsFileNamecol { get; set; }
        public static bool ClsFilezero { get; set; }

        public static List<string> ClsExportTable = new List<string>();
        public static List<string> ClsExportCol = new List<string>();
        public static List<string> ClsExportxlsid = new List<string>();
    }

  public static class ClsFrmInfoPar
    {
        public static string LogPath { get; set; }
        public static int Houseid { get; set; }
        public static int ConverMode { get; set; }
        public static int ExportType { get; set; }
        public static int OneJuan { get; set; }
        public static int DirNamesn { get; set; }
        public static int FileNamesn { get; set; }
        public static int Ocr { get; set; }
        public static int Watermark { get; set; }
        public static int FileFomat { get; set; }
        public static int Ftp { get; set; }
        public static string YimgPath { get; set; }
        public static string MimgPath { get; set; }
        public static string XlsPath { get; set; }
        public static int Taskxc { get; set; }

        public static List<string> FileFormat = new List<string>();
        public static string WaterStrImg { get; set; }
        public static string WaterFontsize { get; set; }
        public static string Waterfont { get; set; }
        public static int WaterFontColor { get; set; }

        public static List<string> TaskBoxCounttmp = new List<string>();

        public static List<string> TaskBoxCount = new List<string>();

     
    }

  
}
