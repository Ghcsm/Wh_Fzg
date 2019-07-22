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

namespace CsmCon
{
    public partial class UcContenModule : Form
    {
        public UcContenModule()
        {
            InitializeComponent();
        }

        private List<string> lscode = new List<string>();
        private string id;

        void LoadModule()
        {
            lvConten.Items.Clear();
            lscode.Clear();
            DataTable dt = Common.GetcontenModule();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                ListViewItem lvi = new ListViewItem();
                string type = dr["CoType"].ToString();
                string code = dr["CODE"].ToString();
                string title = dr["TITLE"].ToString();
                string titlelx = dr["TitleLx"].ToString();
                lvi.Text = type;
                lvi.SubItems.AddRange(new string[] { code, title, titlelx,dr["ID"].ToString() });
                this.lvConten.Items.Add(lvi);
                lscode.Add(type + code);
            }
        }

        void Addconten()
        {
            if (txtLb.Text.Trim().Length != 2) {
                MessageBox.Show("类别长度为2位!");
                txtLb.Focus();
                return;
            }
            if (txtNum.Text.Trim().Length <= 0) {
                MessageBox.Show("代码不正确!");
                txtNum.Focus();
                return;
            }
            if (txtTitle.Text.Trim().Length <= 0) {
                MessageBox.Show("标题长度不正确!");
                txtTitle.Focus();
                return;
            }
            if (lscode.IndexOf(txtLb.Text.Trim() + txtNum.Text.Trim()) >= 0) {
                MessageBox.Show("此类型的代码已存在!");
                txtNum.Focus();
                return;
            }
            try {
                Common.InserContenModule(txtLb.Text.Trim(), txtNum.Text.Trim(), txtTitle.Text.Trim());
                LoadModule();
                txtTitle.Text = "";
            } catch (Exception e) {
                MessageBox.Show("增加模版失败:" + e.ToString());
            }
            txtNum.Focus();
        }

        void DelConten()
        {
            if (id.Trim().Length <= 0)
                return;
            try {
                Common.DelContenModule(id);
                LoadModule();
            } catch (Exception e) {
                MessageBox.Show("删除数据失败:" + e.ToString());
            }

        }

        private void butNew_Click(object sender, EventArgs e)
        {
            Addconten();
        }

        private void UcContenModule_Shown(object sender, EventArgs e)
        {
            LoadModule();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (lvConten.Items.Count <= 0 || lvConten.SelectedItems.Count <= 0)
                return;
            DelConten();
        }

        private void lvConten_Click(object sender, EventArgs e)
        {
            if (lvConten.Items.Count <= 0 || lvConten.SelectedItems.Count <= 0)
                return;
            txtLb.Text = lvConten.SelectedItems[0].SubItems[0].Text;
            txtNum.Text = lvConten.SelectedItems[0].SubItems[1].Text;
            txtTitle.Text = lvConten.SelectedItems[0].SubItems[2].Text;
            id = lvConten.SelectedItems[0].SubItems[3].Text;
        }

        private void txtLb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                e.Handled = true;
            else if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                e.Handled = true;
            else if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void butNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butNew_Click(null, null);
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
    }
}
