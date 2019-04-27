using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bgkj
{
    public partial class FrmUI : Form
    {
        public FrmUI()
        {
            InitializeComponent();
        }
        private List<int> lUserid = new List<int>();
        private List<string> lUserName = new List<string>();
        private List<string> lUserSys = new List<string>();
        private List<string> lUserOtherSys = new List<string>();
        private Point mPoint;

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtUser.Text.Trim().Length <= 0 || this.txtPwd.Text.Trim().Length <= 0) {
                MessageBox.Show("用户或密码不正确!");
                this.txtUser.Focus();
                return;
            }
            if (!T_Sysset.IsCheckUser(this.txtUser.Text.Trim(), this.txtPwd.Text.Trim())) {
                MessageBox.Show("用户或密码不正确!");
                this.txtUser.Focus();
                return;
            }
            Setusersys();
            FrmMain fmMain = new FrmMain();
            this.Hide();
            fmMain.ShowDialog();
        }

        private void Setusersys()
        {
            T_User.LoginName = this.txtUser.Text.Trim();
            T_User.UserId = lUserid[lUserName.IndexOf(this.txtUser.Text.Trim())];
            string str = lUserSys[lUserName.IndexOf(this.txtUser.Text.Trim())];
            string[] sys = DESEncrypt.DesDecrypt(str).Split(';');
            for (int i = 0; i < sys.Length; i++) {
                string s = DESEncrypt.DesEncrypt(sys[i]);
                T_User.UserSys.Add(s);
            }
            str = lUserOtherSys[lUserName.IndexOf(this.txtUser.Text.Trim())];
            sys = DESEncrypt.DesDecrypt(str).Split(';');
            for (int i = 0; i < sys.Length; i++) {
                string s = DESEncrypt.DesEncrypt(sys[i]);
                T_User.UserOtherSys.Add(s);
            }
            lUserid.Clear();
            lUserName.Clear();
            lUserSys.Clear();
            lUserOtherSys.Clear();
        }

        private void btnCle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmUI_Load(object sender, EventArgs e)
        {
            InitUser();
            this.Text = T_ConFigure.SfName;
        }

        void InitUser()
        {
            Task.Run(() =>
            {
                DataTable dt = T_Sysset.GetUser();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                foreach (DataRow dr in dt.Rows) {
                    string strUser = dr["UserName"].ToString();
                    int uid = Convert.ToInt32(dr["id"].ToString());
                    string usersys = dr["UserSys"].ToString();
                    string Uothersys = dr["OtherSys"].ToString();
                    this.txtUser.BeginInvoke(new Action(() =>
                    {
                        this.txtUser.Items.Add(strUser);
                    }));
                    lUserName.Add(strUser);
                    lUserid.Add(uid);
                    lUserSys.Add(usersys);
                    lUserOtherSys.Add(Uothersys);
                }
            });
            T_Sysset.GetSfname();
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                SendKeys.Send("{Tab}");
                txtPwd.Text = "";
            }

        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void panle_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void panle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
    }
}
