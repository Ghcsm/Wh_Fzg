using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class T_User
    {

        public static int UserId
        {
            get;
            set;
        }

        public static string LoginName
        {
            get;
            set;
        }
        public static List<string> UserSys = new List<string>();
        public static List<string> UserOtherSys = new List<string>();
        public static List<string> UserMenu = new List<string>();
    }
}
