using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace CsmCon
{
    public partial class UcDLInfo : UserControl
    {
        public UcDLInfo()
        {
            InitializeComponent();
        }

        private void Cle()
        {
            foreach (Control c in groupPanel1.Controls) {
                if (c is TextBox || c is ComboBox) {
                    c.Text = "";
                }
            }
        }

        private void SetInfo(int id, string str)
        {
            foreach (Control c in groupPanel1.Controls) {
                if (c is TextBox || c is ComboBox) {
                    if (c.Tag.ToString() == id.ToString()) {
                        c.Text = str;
                    }
                }
            }
        }

        public void LoadInfo(int arid, string sx)
        {
            Cle();
            DataTable dt = Common.GetcheckInfo(arid);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labcount.Text = string.Format("共{0}手", dt.Rows.Count.ToString());
            labcrr.Text = string.Format("当前第{0}手", 0);
            string s = "档案手续='" + sx+"'";
            DataTable dt1 = null;
            try {
                dt1 = dt.Select(s).CopyToDataTable();
            } catch {
                MessageBox.Show("此手信息获取不存在!");
                return;
            }
            labcrr.Text = string.Format("当前第{0}手", sx);
            for (int i = 0; i < dt1.Columns.Count; i++) {
                string s1 = dt1.Rows[0][i].ToString();
                if (s1.Trim().Length <= 0)
                    continue;
                SetInfo((i+1), s1);
            }


        }
    }
}
