using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Pubcls
    {

        public void KeyShortDown(System.Windows.Forms.KeyEventArgs e, List<string> lsinival,
            List<string> Lsinikeys, List<string> lssqlOpernum, List<string> lsSqlOper, out string strkey)
        {
            strkey = "";

            if (lsinival.Count <= 0 || Lsinikeys.Count <= 0 || lssqlOpernum.Count <= 0 || lsSqlOper.Count <= 0)
                return;
            StringBuilder keyValue = new StringBuilder
            {
                Length = 0
            };
            keyValue.Append("");
            if (e.Control) {
                keyValue.Append("1-");
            }
            else if (e.Alt) {
                keyValue.Append("2-");
            }
            else if (e.Shift) {
                keyValue.Append("3-");
            }
            else {
                keyValue.Append("0-");
            }
            if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                (e.KeyValue >= 112 && e.KeyValue <= 123) ||
                e.KeyValue >= 96 && e.KeyValue == 101)   //F1-F12
            {
                keyValue.Append(e.KeyValue);
            }
            else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
                keyValue.Append(e.KeyValue.ToString().Substring(1));
            else if (e.KeyValue == 13 || e.KeyValue == 27 || e.KeyValue == 32 || e.KeyValue == 46)
                keyValue.Append(e.KeyValue.ToString());
            string str = keyValue.ToString();

            int x = lsinival.IndexOf(str);
            if (x < 0)
                return;
                for (int i = x; i < lsinival.Count; i++) {
                   if (keyValue.ToString()!= lsinival[i].ToString())
                       continue;
                if (x >= 0) {
                    str = Lsinikeys[i].Remove(0, 1);
                    x = lssqlOpernum.IndexOf(str);
                }
                if (x >= 0) {
                    str = lsSqlOper[x];
                    if (strkey.Trim().Length <= 0)
                        strkey = str;
                    else
                        strkey += ":" + str;
                }
            }
            return;
        }




        public string GetkeyVal(string key)
        {
            try {
                string str = key;
                string[] KeyV = key.Split(new char[] { '-' });
                if (KeyV[0].Trim().ToString() == "1") {
                    str = "Ctrl+";
                }
                else if (KeyV[0].Trim().ToString() == "2") {
                    str = "Alt+";
                }
                else if (KeyV[0].Trim().ToString() == "3") {
                    str = "Shift+";
                }
                else {
                    str = "";
                }
                int nk = Convert.ToInt32(KeyV[1].Trim());
                str += ((char)nk).ToString();
                if (nk == 13)
                    str = "回车";
                else if (nk == 32)
                    str = "空格";
                else if (nk == 27)
                    str = "Ese";
                else if (nk == 46)
                    str = "Del";
                return str;
            } catch {
                return "";
            }

        }

    }
}
