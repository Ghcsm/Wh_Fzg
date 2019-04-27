using DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace Csmyhgl
{
    public partial class FrmUser : Form
    {
        public FrmUser()
        {
            InitializeComponent();
        }

        private string userid { get; set; }
        private void FrmUser_Load(object sender, EventArgs e)
        {
            Loaduser();
        }

        private void FrmUser_Shown(object sender, EventArgs e)
        {
            GetModule();
            txtTime.Text = DateTime.Now.ToString();
        }

        private void Loaduser()
        {
            Cletxt();
            DataTable dt = T_Sysset.GetuserSys();
            dgvData.DataSource = null;
            if (dt != null && dt.Rows.Count > 0) {
                dgvData.DataSource = dt;
            }
        }

        private void GetModule()
        {
            try {
                DataTable dt = T_Sysset.GetModule();
                lbModule.Items.Clear();
                lvOtherModule.Items.Clear();
                if (dt != null && dt.Rows.Count > 0) {
                    lbModule.DataSource = dt;
                    lbModule.DisplayMember ="ModuleChName";
                    lbModule.ValueMember = "id";
                }
                dt = T_Sysset.GetOtherModule();
                if (dt != null && dt.Rows.Count > 0) {
                    lvOtherModule.DataSource = dt;
                    lvOtherModule.DisplayMember = "OtherModule";
                    lvOtherModule.ValueMember = "id";
                }
            } catch (Exception e) {
                MessageBox.Show("加载模块失败" + e.ToString());
            }

        }

        #region click

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtPwd2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtBz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
      

        #endregion

        private void ShowUsertxt()
        {
            Cletxt();
            userid = dgvData.SelectedRows[0].Cells[dgvData.ColumnCount-1].Value.ToString();
            txtUser.Text = dgvData.SelectedRows[0].Cells[0].Value.ToString();
            txtPhone.Text = dgvData.SelectedRows[0].Cells[1].Value.ToString();
            string usersys = DESEncrypt.DesDecrypt(dgvData.SelectedRows[0].Cells[2].Value.ToString());
            string othersys = DESEncrypt.DesDecrypt(dgvData.SelectedRows[0].Cells[3].Value.ToString());
            txtTime.Text = dgvData.SelectedRows[0].Cells[4].Value.ToString();
            txtBz.Text = dgvData.SelectedRows[0].Cells[5].Value.ToString();
            if (usersys.Length <= 0)
                return;
            string[] userSplit = usersys.Split(';');
            for (int i = 0; i < userSplit.Length; i++) {
                string str = userSplit[i];
                SetlboxChk(lbModule, str);
            }
            if (othersys.Length <= 0)
                return;
            string[] othersplit = othersys.Split(';');
            for (int i = 0; i < othersplit.Length; i++) {
                string str = othersplit[i];
                SetlboxChk(lvOtherModule, str);
            }

        }

        private void SetlboxChk(CheckedListBox lsb, string str)
        {
            for (int i = 0; i < lsb.Items.Count; i++) {
                string strid = lsb.GetItemText(lsb.Items[i]);
                if (strid == str)
                    lsb.SetItemChecked(i, true);
            }
        }
        private void Cletxt()
        {
            foreach (Control c in gr1.Controls) {
                if (c is TextBox)
                    c.Text = "";
            }
            for (int i = 0; i < lbModule.Items.Count; i++) {
                lbModule.SetItemChecked(i, false);
            }
            for (int i = 0; i < lvOtherModule.Items.Count; i++) {
                lvOtherModule.SetItemChecked(i, false);
            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            try {
                if (!isuser(0))
                    return;
                string usersys = UsersysStr();
                string othersys = OthersysStr();
                T_Sysset.AddUserSys(txtUser.Text.Trim(), txtPwd.Text.Trim(), txtPhone.Text.Trim(), txtTime.Text.Trim(), txtBz.Text.Trim(), usersys, othersys);
                Loaduser();
            } catch (Exception exception) {
                MessageBox.Show("添加用户失败!" + exception);
            }

        }

        private bool isuser(int user)
        {
            bool tf = true;
            if (txtUser.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入用户名!");
                txtUser.Focus();
                tf = false;
            }
            if (user == 0) {
                if (txtPwd.Text.Trim().Length <= 0 || txtPwd2.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入密码!");
                    txtPwd.Focus();
                    tf = false;
                }
                else {
                    if (txtPwd.Text.Trim() != txtPwd2.Text.Trim()) {
                        MessageBox.Show("两次密码不一致!");
                        tf = false;
                    }
                }
                if (T_Sysset.IsCheckUser(txtUser.Text.Trim())) {
                    MessageBox.Show("用户已存在！");
                    txtUser.Focus();
                    tf = false;
                }
            }
            else {
                if (!T_Sysset.IsCheckUser(txtUser.Text.Trim())) {
                    MessageBox.Show("用户不存在！");
                    txtUser.Focus();
                    tf = false;
                }
            }
            if (txtTime.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入入职时间!");
                txtTime.Focus();
                tf = false;
            }
            return tf;
        }

        private string UsersysStr()
        {
            string str = "";
            int a = 0;
            for (int i = 0; i < lbModule.Items.Count; i++) {
                if (lbModule.GetItemChecked(i)) {
                    a += 1;
                    if (a < lbModule.CheckedItems.Count)
                        str += lbModule.GetItemText(lbModule.Items[i]) + ";";
                    else
                        str += lbModule.GetItemText(lbModule.Items[i]);
                }
            }
            return str;
        }

        private string OthersysStr()
        {
            string str = "";
            int a = 0;
            for (int i = 0; i < lvOtherModule.Items.Count; i++) {
                if (lvOtherModule.GetItemChecked(i)) {
                    a += 1;
                    if (a < lvOtherModule.Items.Count)
                        str += lvOtherModule.GetItemText(lvOtherModule.Items[i]) + ";";
                    else
                        str += lvOtherModule.GetItemText(lvOtherModule.Items[i]);
                }
            }
            return str;
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            try {
                if (!isuser(1))
                    return;
                string usersys = UsersysStr();
                string othersys = OthersysStr();
                T_Sysset.UpUserSys(txtUser.Text.Trim(), txtPhone.Text.Trim(), txtTime.Text.Trim(), txtBz.Text.Trim(), usersys, othersys, userid);
                Loaduser();
            } catch (Exception exception) {
                MessageBox.Show("修改权限失败!" + exception);
            }

        }

        private void butDel_Click(object sender, EventArgs e)
        {
            try {
                if (!isuser(1))
                    return;
                T_Sysset.Deluser(txtUser.Text.Trim());
                Loaduser();
            } catch (Exception exception) {
                MessageBox.Show("删除用户失败!" + exception);
            }
        }

        private void dgvData_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count <= 0)
                return;
            if (dgvData.SelectedRows.Count <= 0)
                return;
            ShowUsertxt();
        }
    }
}
