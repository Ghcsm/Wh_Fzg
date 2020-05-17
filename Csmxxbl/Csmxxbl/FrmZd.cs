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
    public partial class FrmZd : Form
    {
        public FrmZd()
        {
            InitializeComponent();
        }

        private void FrmZd_Shown(object sender, EventArgs e)
        {
            txtzong.Focus();
        }

        private void txtzong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtml.Focus();
        }

        private void txtml_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                comQi.Focus();
        }

        private void comQi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtanjuan.Focus();
        }

        private void txtanjuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butRu.Focus();
        }

        private void butRu_Click(object sender, EventArgs e)
        {
            label5.Text = "信息:";
            if (txtzong.Text.Trim().Length <= 0 || txtml.Text.Trim().Length <= 0 || comQi.Text.Trim().Length <= 0 ||
                txtanjuan.Text.Trim().Length <= 0) {
                MessageBox.Show("请完成相关信息!");
                txtzong.Focus();
                return;
            }
            string str = txtzong.Text.Trim() + "-" + txtml.Text.Trim() + "-" + comQi.Text.Trim() + "-" +
                         txtanjuan.Text.Trim();
            if (str.Trim().Length <= 0) {
                MessageBox.Show("请完成相关信息!");
                txtzong.Focus();
                return;
            }
            int id = Common.Getzonginfo(str);
            if (id <= 0) {
                MessageBox.Show("此卷宗号数据库中不存在请两次检查");
                txtzong.Focus();
                return;
            }
            DataTable dt = Common.Getzonginfo2(str);
            if (dt != null && dt.Rows.Count > 0) {
                string use = dt.Rows[0][3].ToString();
                string time = dt.Rows[0][4].ToString();
                label5.Text = "信息:此卷已录入,用户:" + use + ",时间:" + time;
                return;
            }

            Common.InsterbingNum(str);
                txtanjuan.Focus();
            txtanjuan.SelectAll();
        }
    }
}
