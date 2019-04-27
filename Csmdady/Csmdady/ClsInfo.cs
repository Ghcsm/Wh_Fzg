using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Csmdady
{
   public static class ClsInfo
    {
        public static int ToNum(string columnName)
        {
            if (!Regex.IsMatch(columnName.ToUpper(), @"[A-Z]+") && !Regex.IsMatch(columnName.ToUpper(), @"[a-z]+"))
                return 0;
            int index = 0;
            char[] chars = columnName.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index - 1;
        }
        public static string ToName(int index)
        {
            if (index < 0) {return ""; }

            List<string> chars = new List<string>();
            do {
                if (chars.Count > 0) index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return String.Join(string.Empty, chars.ToArray());
        }
    }
}
