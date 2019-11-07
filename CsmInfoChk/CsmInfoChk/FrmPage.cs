using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsmInfoChk
{
    public partial class FrmPage : Form
    {
        public FrmPage()
        {
            InitializeComponent();
        }

        private void butPage_Click(object sender, EventArgs e)
        {
            int p;
            bool b = int.TryParse(txtPage.Text.Trim(), out p);
            if (!b || p <= 0) {
                MessageBox.Show("默认页码不正确!");
                txtPage.Focus();
                return;
            }
            string file = Path.Combine(Application.StartupPath, "ScanConfig.ini");
            DAL.Writeini.Fileini = file;
            DAL.Writeini.Wirteini("ImageConten", "GoPage", p.ToString());
            this.Close();
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butPage.Focus();
        }

        private void FrmPage_Shown(object sender, EventArgs e)
        {
            string file = Path.Combine(Application.StartupPath, "ScanConfig.ini");
            DAL.Writeini.Fileini = file;
            string sj = DAL.Writeini.Readini("ImageConten", "GoPage").ToString();
            if (sj.Trim().Length > 0)
            {
                txtPage.Text = sj;
            }
        }
    }
}
