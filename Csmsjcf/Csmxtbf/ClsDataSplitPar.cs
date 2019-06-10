using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevComponents.DotNetBar;

namespace Csmsjcf
{
  public static class ClsDataSplitPar
    {
        public static string ClsdirTable { get; set; }
        public static int ClsdirDirsn { get; set; }
        public static string ClsdirCol  {get;set;}
        public static List<string> Clsdircolleg = new List<string>();
        public static string ClsdirMl  {get;set;}
        public static string ClsdirMlpage { get; set; }
        public static int ClsdirPageZero { get; set; }

        public static string ClsFileTable { get; set; }
        public static int ClsFilesn { get; set; }
        public static int ClsFileNmaecd{ get; set; }
        public static string ClsFileNameQian { get; set; }
        public static string ClsFileNameHou { get; set; }
        public static string ClsFileNamecol { get; set; }
        public static bool ClsFilezero { get; set; }
        public static string ClsFileDlname { get; set; }

        public static List<string> ClsExportTable = new List<string>();
        public static List<string> ClsExportCol = new List<string>();
        public static List<string> ClsExportColLeg = new List<string>();
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
        public static int DirNamesnconten { get; set; } = 0;
        public static int dirNamesnpages { get; set; } = 0;
        public static int FileNamesn { get; set; }
        public static string OcrPath { get; set; }
        public static bool Ocrpdf { get; set; }
        public static int Doublecor { get; set; }
        public static int Watermark { get; set; }
        public static int Waterwith { get; set; }
        public static int Waterheiht { get; set; }
        public static int WaterFontsize { get; set; }
        public static string WaterStrImg { get; set; }
        public static string WaterFontColor { get; set; }
        public static int Watertmd { get; set; }
        public static int Waterwz { get; set; }
        public static int FileFomat { get; set; }
        public static int Ftp { get; set; }
        public static string YimgPath { get; set; }
        public static string MimgPath { get; set; }
        public static string XlsPath { get; set; }
        public static int Taskxc { get; set; }

        public static List<string> FileFormat = new List<string>();

        public static List<string> TaskBoxCounttmp = new List<string>();

        public static List<string> TaskBoxCount = new List<string>();
        public static List<DataRow> TaskBoxCountcol = new List<DataRow>();

        public static object Filelock=new object();

        public static int StopTag = 0;
        public static int Task { get; set; }
    }

  
}
