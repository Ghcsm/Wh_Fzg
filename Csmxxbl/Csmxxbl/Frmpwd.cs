using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmxxbl
{
    public partial class Frmpwd : Form
    {
        public Frmpwd()
        {
            InitializeComponent();
        }
       public static bool tf=false;
        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.butOK.Focus();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (this.txtPwd.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入密码!");
                return;
            }   
            string str = "admin";
            string date = DateTime.Now.ToString("yyyyMMdd");
            string pwd = str + date;
            string newpwd = this.txtPwd.Text.Trim();
            if (pwd != newpwd) {
                MessageBox.Show("密码错误，请勿随意修改录入权限!");
                this.txtPwd.Focus();
                tf=false;
                return;
            }
            tf = true;
            this.Close();
        }

        private void Frmpwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tf == false) {
                tf = false;
            }
        }

        private void Frmpwd_Shown(object sender, EventArgs e)
        {
            tf = false;
        }
    }
}
