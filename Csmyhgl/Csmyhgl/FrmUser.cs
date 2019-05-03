using DAL;
using System;
using System.Collections.Generic;
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
        List<string> lsuser = new List<string>();

        private void FrmUser_Shown(object sender, EventArgs e)
        {
            Loaduser();
            GetModule();
            ModuleSys();
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
            DataTable dt = T_Sysset.GetMenuSetid();
            DataTable dtz = T_Sysset.GetModule();
            DataTable dtzz;
            if (dt == null || dt.Rows.Count <= 0)
                return;
            if (dtz == null || dtz.Rows.Count <= 0)
                return;
            TreeNode ProvinceNode = null, znNode;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string name = dt.Rows[i][0].ToString();
                string tag = dt.Rows[i][1].ToString();
                ProvinceNode = new TreeNode();
                ProvinceNode.Text = name;
                ProvinceNode.Tag = tag;
                trModule.Nodes.Add(ProvinceNode);
                dtzz = dtz.Select("ModuleInt=" + tag).CopyToDataTable();
                for (int j = 0; j < dtzz.Rows.Count; j++) {
                    znNode = new TreeNode();
                    znNode.Text = dtzz.Rows[j][0].ToString(); //显示节点的文本  
                    znNode.Tag = tag + "-" + dtzz.Rows[j][1].ToString();
                    ProvinceNode.Nodes.Add(znNode);
                }
                ProvinceNode.ExpandAll();
            }
            ProvinceNode.Nodes[0].EnsureVisible();
        }

        private void ModuleSys()
        {
            DataTable dt = T_Sysset.GetOtherModule();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            lvOtherModule.DataSource = dt;
            lvOtherModule.DisplayMember = "OtherModule";
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
            userid = dgvData.SelectedRows[0].Cells[dgvData.ColumnCount - 1].Value.ToString();
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
                if (str.Trim().Length <= 0)
                    continue;
                for (int j = 0; j < trModule.Nodes.Count; j++) {
                    foreach (TreeNode trn in trModule.Nodes[j].Nodes) {
                        if (trn.Text == str) {
                            trn.Checked = true;
                        }
                    }
                }
            }
            if (othersys.Length <= 0)
                return;
            string[] othersplit = othersys.Split(';');
            for (int i = 0; i < othersplit.Length; i++) {
                string str = othersplit[i];
                if (str.Trim().Length <= 0)
                    continue;
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
            for (int i = 0; i < trModule.Nodes.Count; i++) {
                trModule.Nodes[i].Checked = false;
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
                string usermenu = Usermenu();
                string usersys = Usersysstr();
                string othersys = OthersysStr();
                T_Sysset.AddUserSys(txtUser.Text.Trim(), txtPwd.Text.Trim(), txtPhone.Text.Trim(), txtTime.Text.Trim(), txtBz.Text.Trim(), usersys, othersys, usermenu);
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

        private string Usermenu()
        {
            string str = "";
            List<string> lsmenu = new List<string>();
            lsuser.Clear();
            for (int i = 0; i <trModule.Nodes.Count; i++) {
                if (trModule.Nodes[i].Checked) {
                    lsmenu.Add(trModule.Nodes[i].Text);
                    foreach (TreeNode trn in trModule.Nodes[i].Nodes) {
                        if (trn.Checked)
                            lsuser.Add(trn.Text);

                    }
                }
            }
            for (int i = 0; i < lsmenu.Count ; i++) {
                if (i < lsmenu.Count-1)
                    str += lsmenu[i] + ";";
                else
                    str += lsmenu[i];
            }
            return str;
        }

        private string Usersysstr()
        {
            string str = "";
            for (int i = 0; i < lsuser.Count; i++) {
                if (i < lsuser.Count-1)
                    str += lsuser[i] + ";";
                else str += lsuser[i];
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
                string usermenu = Usermenu();
                string usersys = Usersysstr();
                string othersys = OthersysStr();
                T_Sysset.UpUserSys(txtUser.Text.Trim(), txtPhone.Text.Trim(), txtTime.Text.Trim(), txtBz.Text.Trim(), usersys, othersys, userid, usermenu);
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
        bool check = false;
        private void trModule_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (check == false)
                setchild(e.Node);
            setparent(e.Node);
            check = false;
        }

        private void setchild(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes) {
                child.Checked = node.Checked;
            }
            check = true;
        }

        private void setparent(TreeNode node)
        {
            if (node.Parent != null) {
                if (node.Checked) {
                    foreach (TreeNode brother in node.Parent.Nodes) {
                        if (brother.Checked)
                            break;
                    }
                    node.Parent.Checked = true;
                }
                else {
                    foreach (TreeNode brother in node.Parent.Nodes) {
                        if (brother.Checked)
                            return;
                    }
                    node.Parent.Checked = false;
                }
            }
        }
    }
}
