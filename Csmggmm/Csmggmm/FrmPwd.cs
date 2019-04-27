using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DAL;

namespace Csmggmm
{
    public partial class FrmPwd : Form
    {
        public FrmPwd()
        {
            InitializeComponent();
        }

        private void butPwd_Click(object sender, EventArgs e)
        {
            try {
                if (!istxt())
                    return;
                T_Sysset.UpUserPwd(txtNewPwd.Text.Trim());
                MessageBox.Show("修改密码完成!");
            } catch (Exception exception) {
                MessageBox.Show("修改密码失败!" + exception);
            }
        }


        private bool istxt()
        {
            if (txtOldpwd.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入旧密码!");
                txtOldpwd.Focus();
                return false;
            }

            if (txtNewPwd.Text.Trim().Length <= 0 || txtNewPwd2.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入新密码或重复新密码!");
                txtNewPwd.Focus();
                return false;
            }
            else {
                if (txtNewPwd.Text.Trim() != txtNewPwd2.Text.Trim()) {
                    MessageBox.Show("两次密码不一致!");
                    txtNewPwd.Focus();
                    return false;
                }
            }

            if (!T_Sysset.IsCheckUser(T_User.LoginName, txtOldpwd.Text.Trim())) {
                MessageBox.Show("旧密码不正确!");
                txtOldpwd.Focus();
                return false;
            }
            return true;
        }

        private void txtOldpwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtNewPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtNewPwd2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
    }
}
