using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csmtool
{
    public static class Toolskeys
    {

        //添加系统定义
        public static List<string> lsModule = new List<string>();
        public static List<string>LsnewOper=new List<string>();
        public static List<string>LsnewOperNum=new List<string>();


        //定义快捷键
        public static List<string> LskeyModule = new List<string>();
        public static List<string> LskeyOper = new List<string>();
        public static List<string> LskeyOpernum = new List<string>();

        public static DataTable dtkeys { get; set; }

        public static bool isbool { get; set; }


        public static  int KeyAscill = 0;


        public static List<string> Lsinikey = new List<string>();
        public static List<string> LsiniCz = new List<string>();
        public static List<string> LsId = new List<string>();


    }
}
