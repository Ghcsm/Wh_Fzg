﻿using System;
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
    public partial class FrmPage : Form
    {
        public FrmPage()
        {
            InitializeComponent();
        }

        public int Archid = 0;
        private List<string> lspage = new List<string>();
        private void FrmPage_Shown(object sender, EventArgs e)
        {
            lbPage.Items.Clear();
            lspage.Clear();
            GetPage();
            txtNumpage.Focus();
        }

        private void butSavePage_Click(object sender, EventArgs e)
        {
            if (txtPage.Text.Trim().Length <= 0) {
                MessageBox.Show("页码不能为空!");
                txtPage.Focus();
                return;
            }
            if (txtPage.Text.IndexOf('-') < 0) {
                MessageBox.Show("页码必须包含横杠!");
                txtPage.Focus();
                return;
            }
            else {
                try {
                    string[] s = txtPage.Text.Trim().Split('-');
                    if (s.Length < 2) {
                        MessageBox.Show("此页码不正确!");
                        txtPage.Focus();
                        return;
                    }
                    else if (s[0].ToString().Trim().Length <= 0 || s[1].ToString().Trim().Length <= 0) {
                        MessageBox.Show("此页码不正确!");
                        txtPage.Focus();
                        return;
                    }
                } catch {
                    return;
                }
            }
            if (lspage.IndexOf(txtPage.Text.Trim()) >= 0) {
                MessageBox.Show("页码已经存在!");
                txtPage.Focus();
                return;
            }
            lspage.Add(txtPage.Text.Trim());
            lbPage.Items.Add(txtPage.Text.Trim());
            if (lbPage.Items.Count > 0)
                lbPage.SelectedIndex = lbPage.Items.Count - 1;

            txtPage.Text = "";
            txtPage.Focus();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (lbPage.Items.Count <= 0)
                return;
            if (lbPage.SelectedItems.Count <= 0) {
                MessageBox.Show("请选中删除项!");
                return;
            }
            int id = lbPage.SelectedIndex;
            if (id <= lbPage.Items.Count)
                lbPage.Items.RemoveAt(id);
            if (lspage.Count >= id)
                lspage.RemoveAt(id);
        }

        void SavePage()
        {
            string str = "";
            for (int i = 0; i < lbPage.Items.Count; i++) {
                string s = lbPage.Items[i].ToString();
                if (str.Trim().Length <= 0)
                    str += s;
                else
                    str += ";" + s;
            }
            int p = Convert.ToInt32(txtNumpage.Text.Trim()) + (lspage.Count);
            Common.SetInfoPages(Archid.ToString(), str, txtNumpage.Text.Trim(), p.ToString());
        }

        void GetPage()
        {
            if (Archid <= 0)
                return;
            DataTable dt= Common.GetInfoPages(Archid.ToString());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            string str = dt.Rows[0][0].ToString();
            string page = dt.Rows[0][1].ToString();
            if (str.Trim().Length <= 0)
                return;
            string[] s = str.Split(';');
            for (int i = 0; i < s.Length; i++) {
                string s1 = s[i];
                if (s1.Trim().Length <= 0)
                    continue;
                lspage.Add(s1);
                lbPage.Items.Add(s1);
            }
            txtNumpage.Text = page;
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (lbPage.Items.Count <= 0)
                return;
            if (Archid <= 0) {
                MessageBox.Show("案卷id获取失败,请重新打开!");
                return;
            }
            SavePage();
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butSavePage_Click(null, null);
            if (e.KeyChar==27)
                this.Close();
        }

        private void FrmPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtNumpage.Text.Trim().Length <= 0) {
                MessageBox.Show("数字页码不为能空");
                txtNumpage.Focus();
                e.Cancel = true;
                return;
            }
            else {
                int p;
                bool bl = int.TryParse(txtNumpage.Text.Trim(), out p);
                if (!bl) {
                    MessageBox.Show("页码不正确!");
                    txtNumpage.Focus();
                    e.Cancel = true;
                    return;
                }
                else if (p<=0)
                {
                    MessageBox.Show("数字页码不能为为0");
                    return;
                } 
            }
            SavePage();
        }

        private void txtNumpage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPage.Focus();
            if (e.KeyChar == 27)
                this.Close();
        }

        private void FrmPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                this.Close();
        }
    }
}
