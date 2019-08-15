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
    public partial class FrmSharePenSet : Form
    {
        public FrmSharePenSet()
        {
            InitializeComponent();
        }

        public static int shuliang = 0;
        public static int Banj = 0;
        public static int Yuzhi = 0;

        private void butHf_Click(object sender, EventArgs e)
        {
            txtNum.Text = "60";
            txtBanj.Text = "250";
            txtYuzhi.Text = "0";
        }

        bool Istxt()
        {
            bool sl = int.TryParse(txtNum.Text.Trim(), out shuliang);
            bool bj = int.TryParse(txtBanj.Text.Trim(), out Banj);
            bool yz = int.TryParse(txtYuzhi.Text.Trim(), out Yuzhi);
            if (!sl || !bj || !yz) {
                MessageBox.Show("参数设置不正确!");
                txtNum.Focus();
                return false;
            }
            if (shuliang < 0 && shuliang > 500) {
                MessageBox.Show("数量值范围不正确!");
                txtNum.Focus();
                return false;
            }
            if (Banj < 1 && Banj > 250) {
                MessageBox.Show("半径值范围不正确!");
                txtBanj.Focus();
                return false;
            }
            if (Yuzhi < 0 && Yuzhi > 250) {
                MessageBox.Show("阈值范围不正确!");
                txtYuzhi.Focus();
                return false;
            }
            return true;
        }

        void WiteCs()
        {
            string file = Path.Combine(Application.StartupPath, "ScanConfig.ini");
            DAL.Writeini.Fileini = file;
            DAL.Writeini.Wirteini("ImageIndex", "Amount", shuliang.ToString());
            DAL.Writeini.Wirteini("ImageIndex", "Radius", Banj.ToString());
            DAL.Writeini.Wirteini("ImageIndex", "Threshold", Yuzhi.ToString());
        }


        void ReadIni()
        {
            string file = Path.Combine(Application.StartupPath, "ScanConfig.ini");
            DAL.Writeini.Fileini = file;
            string sj = DAL.Writeini.Readini("ImageIndex", "Amount").ToString();
            if (sj.Trim().Length > 0)
            {
                shuliang = Convert.ToInt32(sj);
                txtNum.Text = sj;
            }
            string bj = DAL.Writeini.Readini("ImageIndex", "Radius").ToString();
            if (bj.Trim().Length > 0)
            {
                Banj = Convert.ToInt32(bj);
                txtBanj.Text = bj;
            }
            string yz = DAL.Writeini.Readini("ImageIndex", "Threshold").ToString();
            if (yz.Trim().Length > 0)
            {
                Yuzhi = Convert.ToInt32(yz);
                txtYuzhi.Text = yz;
            }
        }


        private void FrmSharePenSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Istxt())
                e.Cancel = true;
            WiteCs();
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
                SendKeys.Send("{Tab}");
        }

        private void txtBanj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtYuzhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void FrmSharePenSet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==27)
                this.Close();
        }

        private void FrmSharePenSet_Shown(object sender, EventArgs e)
        {
            ReadIni();
        }
    }
}
