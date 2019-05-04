using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace Csmborr
{
    public static class BorrMethod
    {
        public static List<string> lscol = new List<string>();
        public static void Getdata(ListView lvq, string col, string czf, string gjz)
        {
            lvq.Items.Clear();
            DataTable dt = Common.QuerBorrData(col, czf, gjz);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            int i = 1;
            foreach (DataRow dr in dt.Rows) {
              
                ListViewItem lvi = new ListViewItem();
                string s1 = dr["borrtag"].ToString();
                if (s1 == "1")
                    lvi.ImageIndex = 1;
                else
                    lvi.ImageIndex = 0;
                lvi.Text = i.ToString();
                for (int t = 0; t < lscol.Count; t++) {
                    col = lscol[t];
                   string str = dr[col].ToString();
                    lvi.SubItems.AddRange(new string[] { str });
                }
                lvq.Items.Add(lvi);
                i++;
            }
        }


        public static void Getinfo(ListView lv, ComboBoxEx box)
        {
            lscol.Clear();
            box.Items.Clear();
            DataTable dt = T_Sysset.GetborrTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            string str = dt.Rows[0][2].ToString();
            if (str.Trim().Length <= 0)
                return;
            if (str.IndexOf(";") < 0)
                return;
            string[] s = str.Split(';');
            for (int i = 0; i < s.Length; i++) {
                lscol.Add(s[i].Trim());
                box.Items.Add(s[i].Trim());
                lv.Columns.Add(s[i].Trim());
            }
            lv.Columns.Add("Archid");
        }
    }
}
