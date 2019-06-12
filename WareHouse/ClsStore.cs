using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse
{
 public static  class ClsStore
    {
        public static DevComponents.DotNetBar.ButtonX[,] but_mjj = new DevComponents.DotNetBar.ButtonX[9, 9];
        public static DevComponents.DotNetBar.ButtonX[] but_boxsn = new DevComponents.DotNetBar.ButtonX[2000];
        public static DevComponents.DotNetBar.ButtonX[] but_juan = new DevComponents.DotNetBar.ButtonX[2000];
        public static object ObjHouseColRow { get; set; }
        public static object Objboxsn { get; set; }
        public static object ObjJuan { get; set; }
        public static int Houseid { get; set; }
        public static int Guisn { get; set; }
        public static int Absn = 0;
        public static int Liesn { get; set; }
        public static int Cengsn { get; set; }
        public static int Boxsn { get; set; }
        public static int Juansn { get; set; }
        public static int Archid { get; set; }
        public static string ArchPos { get; set; }
        public static int GMaxbox { get; set; }
        public static int SelectLiesn { get; set; }
        public static int SelectCengsn { get; set; }
        public static int SelectBoxsn { get; set; }
        public static int SelectJuansn { get; set; }
        public static int butmjjx = 0;
        public static int butmjjy = 0;
        public static int butboxx = 0;
        public static int butjunax = 0;

        public static bool Gdring { get; set; }
        public static bool Imgsys { get; set; }
        public static bool Imgys { get; set; }
        public static string Imgyszt { get; set; } = "0";

    }
}
