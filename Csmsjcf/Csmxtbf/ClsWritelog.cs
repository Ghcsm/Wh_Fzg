using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csmsjcf
{
    public static class ClsWritelog
    {
        public static void Writelog(string path, string str)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string dt = DateTime.Now.ToString();
            try {
                string file = "log.txt";
                string filepath = Path.Combine(path, file);
                if (!File.Exists(filepath)) {
                    fs = new FileStream(filepath, FileMode.Create);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(str + " 操作时间 " + dt);
                    sw.Flush();
                }
                else {
                    fs = new FileStream(filepath, FileMode.Append);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(str + " 操作时间 " + dt);
                    sw.Flush();
                }
                sw.Close();
                fs.Close();
            } catch {
                sw.Close();
                fs.Close();
            } 
        }
    }
}
