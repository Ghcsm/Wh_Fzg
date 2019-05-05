using DAL;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Csmborr
{
    public static class BorrMethod
    {
        public static List<string> lscol = new List<string>();
        public static int Archid { get; set; }
        public static string Boxsn { get; set; }
        public static string Archno { get; set; }
        public static string Filename { get; set; }
        public static bool Imgsys { get; set; }

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
                string arid = dr["Archid"].ToString();
                lvi.SubItems.AddRange(new string[] { arid });
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
            Getsys();
        }

        public static void Getsys()
        {
            Imgsys = false;
            DataTable dt = Common.GetOthersys();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            string str = DESEncrypt.DesDecrypt(dt.Rows[0][0].ToString());
            if (str.Contains("图像打印"))
                Imgsys = true;
        }

        public static void GetArchinfo(ListView lvData, ToolStripStatusLabel boxsn, ToolStripStatusLabel archno, ToolStripStatusLabel file)
        {
            Archid = Convert.ToInt32(lvData.SelectedItems[0].SubItems[lscol.Count + 1].Text);
            if (Archid <= 0)
                return;
            DataTable dt = Common.QuerboxsnInfo(Archid);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            Boxsn = dt.Rows[0][0].ToString();
            Archno = dt.Rows[0][1].ToString();
            boxsn.Text = string.Format("盒号：{0}", Boxsn);
            archno.Text = string.Format("卷号：{0}",Archno);
            Filename = dt.Rows[0][2].ToString();
            file.Text = Filename;
        }
    }
}
