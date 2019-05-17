using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
  public static  class Writeini
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);

        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);

        public static string Fileini { get; set; }

        public static void Wirteini(string Section, string Key, string Value)
        {
            try {
                WritePrivateProfileString(Section, Key, Value, Fileini);
            } catch {
                MessageBox.Show("写入配置文件失败!");
            }
        }

        public static string Readini(string Section, string Key)
        {
            try {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, Fileini);
                return temp.ToString();
            } catch {
                MessageBox.Show("读取配置文件失败!");
                return "";
            }
        }

        public static Boolean Wirtekey(string siction,string key, string val)
        {
            try {
                Wirteini(siction, key, val);
                return true;
            } catch {
                return false;
            }
        }

        public static Boolean Delkeyval(string siction, string key)
        {
            try {
                Wirteini(siction, key, null);
                return true;
            } catch {
                return false;
            }
        }
        public static Boolean Delkeyval(string siction, string key,string val)
        {
            try {
                Wirteini(siction, key, val);
                return true;
            } catch {
                return false;
            }
        }


        public static void GetAllKeyValues(string section,out List<string> keys,out List<string> value)
        {
            keys = new List<string>();
            value = new List<string>();
            byte[] b = new byte[100];//配置节下的所有信息
            GetPrivateProfileSection(section, b, b.Length, Fileini);
            string s = System.Text.Encoding.Default.GetString(b);//配置信息
            string[] tmp = s.Split((char)0);//Key\Value信息
            List<string> result = new List<string>();
            foreach (string r in tmp) {
                if (r != string.Empty)
                    result.Add(r);
            }
            for (int i = 0; i < result.Count; i++) {
                string[] item = result[i].Split(new char[] { '=' });//Key=Value格式的配置信息              
                if (item.Length > 2) {
                    keys.Add(item[0].Trim().ToString());
                    value.Add(result[i].Substring(keys[i].Length + 1));
                }
                if (item.Length == 2) {
                    keys.Add(item[0].Trim().ToString());
                    value.Add(item[1].ToString());
                }
                else if (item.Length == 1) {
                    keys.Add(item[0].Trim().ToString());
                    value.Add("");
                }
            }
        }
    }
}
