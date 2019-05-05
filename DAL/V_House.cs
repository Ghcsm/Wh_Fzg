using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class V_HouseName
    {
        public static string HouseSetName { get; set; }
        public static string HouseSetType { get; set; }
        public static  int MaxBoxNum { get; set; }
        public static int HouseSetid { get; set; }

    }

    public static class V_HouseSetCs
    {
        public static int Id { get; set; }
        public static string HouseName { get; set; }
        public  static int Houseid { get; set; }
        public static int HouseGui { get; set; }
        public static int HouseCol { get; set; }
        public static int HouseRow { get; set; }
        public static int Housebox { get; set; }
        public static int Housejuan { get; set; }
        public static int HouseboxMax { get; set; }
    }

    public class V_HouseSet
    {
        public int ID { get; set; }

        public int HouseID
        {
            get;
            set;
        }

        public string HouseName
        {
            get;
            set;
        }

        public  int HouseBox
        {
            get;
            set;
        }

        public override string ToString()
        {
            return HouseName.ToString();
        }

    }

    
}
