using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;

namespace Csmdady
{
    public static class ClsPrintInfo
    {
        public static string PrintTable { get; set; }
        public static List<string> lsinfoXy = new List<string>();
        public static List<string> lsinfoShow = new List<string>();
        public static List<string> lsSpecCol = new List<string>();
        public static List<string> lsSpecFont = new List<string>();
        public static List<string> lsSpecFontsize = new List<string>();
        public static List<string> lsSpecFontcolo=new List<string>();
        public static List<string> lsSpecFontbuld = new List<string>();
        public static List<string> lsSpecFontcolshow = new List<string>();
        public static List<string> lsSpecFontLine = new List<string>();
        public static int x { get; set; }
        public static int y{get;set;}
        public static int Fontsize { get; set; }
        public static string FontColor { get; set; }
        public static string FontName { get; set; }
        public static string FontBold { get; set; }
        public static Boolean PrintcolName { get; set; }
        public static Boolean printLine { get; set; }
        public static int Tagid { get; set; }
        public static Dictionary<int, string> PrintXy=new Dictionary<int, string>();
        public static int Archid { get; set; }
        public static  int Boxsn { get; set; }
        public static DataTable ArchInfoDataTable { get; set; }
      
        public static DataTable PrintInfo { get; set; }
        

        public static List<string>Lsitems=new List<string>();

    }

    public static class ClsPrintInfoDocument
    {
      public static PrintDocument printDocument=new PrintDocument();
        public static PrintDocument PrintConten = new PrintDocument();

    }

    public static class ClsPrintConten
    {
        //目录字段
        public static List<string> PrintContenCol = new List<string>();
        //xls表格
        public static List<string> printContenXls = new List<string>();
        //是否递增
        public static List<string> PrintContenDz = new List<string>();
        //页码
        public static List<string> PrintContenPage = new List<string>();
        //全部设置
        public static List<string> PrintContenAll = new List<string>();
        public static string PrintContenTable = "";
        public static string PrintContenSn = "";
        public static string PrintContenPagesn = "";
        public static int PrintContenPageMode = 0;
    }
}
