﻿using DAL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (!Setusersys()) {
                txtUser.Focus();
                return;
            }

            try {
                FrmMain fmMain = new FrmMain();
                this.Hide();
                Wirteini();
                fmMain.ShowDialog();
            } catch {
                Application.Exit();
            }
        }

        void Wirteini()
        {
            try {
                string file = Path.Combine(Application.StartupPath, "Archselect.ini");
                Writeini.Fileini = file;
                Writeini.Wirteini("username", "user", txtUser.Text.Trim());
            } catch { }

        }

        void Readini()
        {
            try
            {
                string file = Path.Combine(Application.StartupPath, "Archselect.ini");
                Writeini.Fileini = file;
                string user = Writeini.Readini("username", "user");
                txtUser.Text = user;
            }
            catch 
            {}
        }

        private bool Setusersys()
        {
            try {
                int id = -1;
                T_User.LoginName = this.txtUser.Text.Trim();
                try {
                    id = ClsSetInfopar.lUserName.IndexOf(T_User.LoginName);
                    if (id < 0) {
                        MessageBox.Show("用户不存在!");
                        return false;
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                    return false;

                }
                T_User.UserId = ClsSetInfopar.lUserid[id];
                string str = ClsSetInfopar.lUserSys[ClsSetInfopar.lUserName.IndexOf(T_User.LoginName)];
                string[] sys = DESEncrypt.DesDecrypt(str).Split(';');
                for (int i = 0; i < sys.Length; i++) {
                    string s = DESEncrypt.DesEncrypt(sys[i]);
                    T_User.UserSys.Add(s);
                }
                str = ClsSetInfopar.lUserOtherSys[ClsSetInfopar.lUserName.IndexOf(T_User.LoginName)];
                sys = DESEncrypt.DesDecrypt(str).Split(';');
                for (int i = 0; i < sys.Length; i++) {
                    string s = DESEncrypt.DesEncrypt(sys[i]);
                    T_User.UserOtherSys.Add(s);
                }
                str = ClsSetInfopar.lUsermenu[ClsSetInfopar.lUserName.IndexOf(T_User.LoginName)];
                sys = DESEncrypt.DesDecrypt(str).Split(';');
                for (int i = 0; i < sys.Length; i++) {
                    string s = DESEncrypt.DesEncrypt(sys[i]);
                    T_User.UserMenu.Add(s);
                }
                ClsSetInfopar.lUserid.Clear();
                ClsSetInfopar.lUserName.Clear();
                ClsSetInfopar.lUserSys.Clear();
                ClsSetInfopar.lUserOtherSys.Clear();
                ClsSetInfopar.lUsermenu.Clear();
                return true;
            } catch (Exception e) {
                MessageBox.Show("权限读取失败:" + e.ToString());
                return false;
            }
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
                if (dt == null || dt.Rows.Count <= 0) {
                    this.BeginInvoke(new Action(() =>
                    {
                        lbinfo.Visible = true;
                        lbinfo.Refresh();
                    }));

                    return;
                }
                foreach (DataRow dr in dt.Rows) {
                    string strUser = dr["UserName"].ToString();
                    int uid = Convert.ToInt32(dr["id"].ToString());
                    string usersys = dr["UserSys"].ToString();
                    string Uothersys = dr["OtherSys"].ToString();
                    string usermenu = dr["Usermenu"].ToString();
                    this.txtUser.BeginInvoke(new Action(() =>
                    {
                        this.txtUser.Items.Add(strUser);
                    }));
                    ClsSetInfopar.lUserName.Add(strUser);
                    ClsSetInfopar.lUserid.Add(uid);
                    ClsSetInfopar.lUserSys.Add(usersys);
                    ClsSetInfopar.lUserOtherSys.Add(Uothersys);
                    ClsSetInfopar.lUsermenu.Add(usermenu);
                }

                txtUser.BeginInvoke(new Action(() => { txtUser.Focus(); }));
            });

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
            ClsSetInfopar.mPoint = new Point(e.X, e.Y);
        }

        private void panle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                this.Location = new Point(this.Location.X + e.X - ClsSetInfopar.mPoint.X, this.Location.Y + e.Y - ClsSetInfopar.mPoint.Y);
            }
        }

        private void FrmUI_Shown(object sender, EventArgs e)
        {
            Readini();
        }
    }
}
