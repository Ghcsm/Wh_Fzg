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

namespace Csmborr
{
    public partial class FrmArchBorr : Form
    {
        public FrmArchBorr()
        {
            InitializeComponent();
        }
        public static int Archid { get; set; }
        public static string Boxsn { get; set; }
        public static string Archno { get; set; }
        private string worktype = "";

        #region goto


        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
        private void combsex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtsfz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtlxfs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void combarchlx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt2jyyt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt2time_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt2page_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txt2sm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        #endregion

        private bool istxt()
        {
            if (txtname.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入姓名!");
                txtname.Focus();
                return false;
            }
            if (txtsfz.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入身份证号码!");
                txtsfz.Focus();
                return false;
            }
            else if (txtsfz.Text.Trim().Length != 15 || txtsfz.Text.Trim().Length != 18) {
                MessageBox.Show("身份证号码不正确！");
                txtsfz.Focus();
                return false;
            }
            if (txtlxfs.Text.Trim().Length <= 0) {
                MessageBox.Show("联系方式不能为空!");
                txtlxfs.Focus();
                return false;
            }

            if (combarchlx.Text.Trim().Length <= 0) {
                MessageBox.Show("档案类型不能为空!");
                combarchlx.Focus();
                return false;
            }
            if (txt2time.Text.Trim().Length <= 0) {
                MessageBox.Show("时间不能为空!");
                txt2time.Focus();
                return false;
            }
            if (rabfy.Checked)
                worktype = rabfy.Text;
            else if (rablook.Checked)
                worktype = rablook.Text;
            else if (rabjieyong.Checked)
                worktype = rabjieyong.Text;
            else if (rabguihuan.Checked)
                worktype = rabguihuan.Text;
            else if (rabyijao.Checked)
                worktype = rabyijao.Text;
            return true;
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (!istxt())
                return;
            string name = txtname.Text.Trim();
            string sex = combsex.Text.Trim();
            string sfz = txtsfz.Text.Trim();
            string lxfs = txtlxfs.Text.Trim();
            string add = txtadd.Text.Trim();
            string archty = combarchlx.Text.Trim();
            string yt = txt2jyyt.Text.Trim();
            string time = txt2time.Text.Trim();
            string page = txt2page.Text.Trim();
            string bz = txt2sm.Text.Trim();
            Common.SaveborrInfo(name, sex, sfz, lxfs, add, worktype, archty, yt, time, page, bz, Archid.ToString(),
                Boxsn, Archno);
            MessageBox.Show("提交成功!");
        }

        private void FrmArchBorr_Shown(object sender, EventArgs e)
        {
            combsex.SelectedIndex = 0;
            combarchlx.SelectedIndex = 0;
            txt2time.Text = DateTime.Now.ToString("u");
        }


    }
}
