using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace Csmxxbl
{
    public partial class FrmPage : Form
    {
        public FrmPage()
        {
            InitializeComponent();
        }

        public int Archid = 0;
        private List<string> lspage = new List<string>();
        private int Numid = 1;
        private void FrmPage_Shown(object sender, EventArgs e)
        {
            GetPage();
           
        }

        private string strnum = "";
       
       

        void SavePage(string page)
        {
            Common.SetInfoPages(Archid.ToString(), page, txtPage.Text.Trim().Trim());
        }

        void GetPage()
        {
            if (Archid <= 0)
                return;
            DataTable dt = Common.GetInfoPages(Archid.ToString());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            string str = dt.Rows[0][0].ToString();
            txtPage.Text = str;
        }
        

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void FrmPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            int p = 0;
            if (txtPage.Text.Trim().Length <= 0) {
                MessageBox.Show("页码不为能空");
                txtPage.Focus();
                e.Cancel = true;
                return;
            }
            else {
                try {
                    string[] s = txtPage.Text.Trim().Split('-');
                    for (int i = 0; i < s.Length; i++)
                    {
                        p += Convert.ToInt32(s[i]);
                    }
                    if (s.Length < 0) {
                        MessageBox.Show("页码不正确!");
                        return;
                    }
                } catch {
                    MessageBox.Show("页码不正确!");
                    return;
                }
            }
            SavePage(p.ToString());
        }



        private void FrmPage_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

    }
}
