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

namespace Csmdapx
{
    public partial class FrmRgb : Form
    {
        public FrmRgb()
        {
            InitializeComponent();
        }

        public string R = "-1";
        public string G = "-1";
        public string B = "-1";
        public string Sc = "20";


        bool Isnum(string str)
        {
            if (str.Trim().Length <= 0)
                return false;
            int p;
            bool bl = int.TryParse(str, out p);
            if (bl) {
                if (p >= 0 && p <= 255)
                    return true;
            }
            return false;
        }

        private void txtR_Leave(object sender, EventArgs e)
        {
            if (txtR.Text.Trim().Length <= 0)
                return;
            if (!Isnum(txtR.Text.Trim())) {
                MessageBox.Show("值范围只能在0-255");
                txtR.Focus();
                return;
            }

            R = txtR.Text.Trim();
        }

        private void txtG_Leave(object sender, EventArgs e)
        {
            if (txtG.Text.Trim().Length <= 0)
                return;
            if (!Isnum(txtG.Text.Trim())) {
                MessageBox.Show("值范围只能在0-255");
                txtG.Focus();
                return;
            }
            G = txtG.Text.Trim();
        }

        private void txtB_Leave(object sender, EventArgs e)
        {
            if (txtB.Text.Trim().Length <= 0)
                return;
            if (!Isnum(txtB.Text.Trim())) {
                MessageBox.Show("值范围只能在0-255");
                txtB.Focus();
                return;
            }
            B = txtB.Text.Trim();
        }

        private void txtSc_Leave(object sender, EventArgs e)
        {
            if (txtSc.Text.Length <= 0)
                return;
            int p;
            bool bl = int.TryParse(txtSc.Text.Trim(), out p);
            if (!bl)
            {
                MessageBox.Show("请输入正确的数值!");
                txtSc.Focus();
                return;
            }
            if (p <= 0)
            {
                MessageBox.Show("请输入正确的数值!");
                txtSc.Focus();
                return;
            }
            Sc = txtSc.Text.Trim();
        }


        void WiteCs()
        {
            string file = Path.Combine(Application.StartupPath, "ScanConfig.ini");
            DAL.Writeini.Fileini = file;
            DAL.Writeini.Wirteini("ImageIndex", "AutoRectR", R);
            DAL.Writeini.Wirteini("ImageIndex", "AutoRectG", G);
            DAL.Writeini.Wirteini("ImageIndex", "AutoRectB", B);
            DAL.Writeini.Wirteini("ImageIndex", "AutoRectSc", Sc);
        }

        void ReadIni()
        {
            string file = Path.Combine(Application.StartupPath, "ScanConfig.ini");
            DAL.Writeini.Fileini = file;
            string sj = DAL.Writeini.Readini("ImageIndex", "AutoRectR").ToString();
            if (sj.Trim().Length > 0) {
                R = sj;
                txtR.Text = sj;
            }
            string bj = DAL.Writeini.Readini("ImageIndex", "AutoRectG").ToString();
            if (bj.Trim().Length > 0) {
                G = bj;
                txtG.Text = bj;
            }
            string yz = DAL.Writeini.Readini("ImageIndex", "AutoRectB").ToString();
            if (yz.Trim().Length > 0) {
                B = yz;
                txtB.Text = yz;
            }
            string sc1 = DAL.Writeini.Readini("ImageIndex", "AutoRectSc").ToString();
            if (sc1.Trim().Length > 0) {
                 Sc= sc1;
                txtSc.Text = Sc;
            }
        }

        private void FrmRgb_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sc = txtSc.Text.Trim();
            if (R != "-1" && G != "-1" && B != "-1" && Sc!="20")
            {
                R = txtR.Text.Trim();
                G = txtG.Text.Trim();
                B = txtB.Text.Trim();
                WiteCs();
            }
        }

        private void FrmRgb_Shown(object sender, EventArgs e)
        {
            ReadIni();
            txtR.Focus();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            txtR.Text = "0";
            txtG.Text = "0";
            txtB.Text = "0";
            txtSc.Text = "20";
        }
    }
}
