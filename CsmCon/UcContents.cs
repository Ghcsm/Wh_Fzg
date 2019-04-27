﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CsmCon
{
    public partial class UcContents : UserControl
    {
        public UcContents()
        {
            InitializeComponent();
        }
        public event CntSelectHandleG GoFous;
        public event CntSelectHandle OneClickGotoPage;
        public delegate void CntSelectHandleG(object sender, EventArgs e);
        public delegate void CntSelectHandle(object sender, EventArgs e);
        public static bool ModuleVisible { get; set; }
        public static bool ContentsEnabled { get; set; }
        public static int ArchMaxPage { get; set; }
        public static int ArchId { get; set; }

        public static int Mtmpid = 0;

        public static int PageCrren = 0;

        private void Init()
        {
            ClsConten.GetControl(panel1);
            this.chbModule.Checked = ModuleVisible;
            this.gr0.Enabled = ContentsEnabled;
            gr2.Enabled = ContentsEnabled;
            this.chbModule.Refresh();
            this.gr0.Refresh();
            Lvnameadd();
            if (ModuleVisible)
                ClsConten.LoadModule(LvModule);
            ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);

        }
        private void Lvnameadd()
        {
            if (ClsContenInfo.ContenCoList.Count <= 0)
                return;
            for (int i = 0; i < ClsContenInfo.ContenCoList.Count; i++) {
                string str = ClsContenInfo.ContenCoList[i];
                LvContents.Columns.Add(str);
            }
        }

        private void chbModule_CheckedChanged(object sender, EventArgs e)
        {
            if (chbModule.Checked) {
                this.LvContents.Width = this.gr1.Width - 176;
                this.LvModule.Visible = true;
                ClsConten.LoadModule(LvModule);
            }
            else {
                this.LvContents.Width = this.gr1.Width - 10;
                this.LvModule.Visible = false;
            }
        }


        #region 转跳
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                e.Handled = true;
            else if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                e.Handled = true;
            else if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }


        #endregion

        private bool istxt(bool id)
        {
            if (ArchId <= 0) {
                MessageBox.Show("请重新选择案卷!");
                return false;
            }
            if (!ClsConten.Gettxtzd(panel1)) {
                MessageBox.Show("标题及页码不能为空!");
                return false;
            }
            string pages = ClsContenInfo.Pagestmp;
            int p = ClsContenInfo.ContenCoList.IndexOf(ClsContenInfo.ContenPages);
            string pagestmp = "";
            if (id) {
                foreach (ListViewItem item in LvContents.Items) {
                    pagestmp = item.SubItems[p + 2].Text.ToString();
                    if (pages == pagestmp)
                    {
                        MessageBox.Show("页码已存在！");
                        return false;
                    }
                }
            }
            else {
                int tmpID = 0;
                if (Mtmpid == 0) {
                    MessageBox.Show("请选择目录");
                    LvContents.Focus();
                    return false;
                }
                foreach (ListViewItem item in LvContents.Items) {
                    pagestmp = item.SubItems[p + 2].Text.ToString();
                    tmpID = Convert.ToInt32(item.SubItems[1].Text);
                    if (pagestmp == pages && tmpID != Mtmpid) {
                        MessageBox.Show("起始页码已存在!");
                        return false;
                    }
                }
            }

            return true;
        }

        private void cleTxt()
        {
            foreach (Control c in panel1.Controls) {
                if (c is TextBox || c is ComboBox)
                    c.Text = "";
            }
        }


        private void ContentsEdit()
        {
            if (!istxt((false)))
                return;
            Dictionary<int, string> dic1 = new Dictionary<int, string>();
            Dictionary<int, string> dicxx = new Dictionary<int, string>();
            foreach (Control t in panel1.Controls) {
                if (t.Tag != null && t.Tag.ToString() != "") {
                    string str = t.Text.Trim();
                    dic1.Add(Convert.ToInt32(t.Tag), str);
                }
            }
            dicxx = dic1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            int id = Common.ContentsEdit(ClsContenInfo.ContenTable, ClsContenInfo.ContenCoList, dicxx, Mtmpid.ToString(), ArchId);
            if (id > 0)
                cleTxt();
            ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);
            txtCode.Focus();
        }
        private void AddTitle()
        {
            if (!istxt(true))
                return;
            Dictionary<int, string> dic1 = new Dictionary<int, string>();
            Dictionary<int, string> dicxx = new Dictionary<int, string>();
            foreach (Control t in panel1.Controls) {
                if (t.Tag != null && t.Tag.ToString() != "") {
                    string str = t.Text.Trim();
                    dic1.Add(Convert.ToInt32(t.Tag), str);
                }
            }
            dicxx = dic1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            int id = Common.ContentsInster(ClsContenInfo.ContenTable, ClsContenInfo.ContenCoList, dicxx, ArchId);
            if (id > 0)
                cleTxt();
            ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);
            txtCode.Focus();
        }

        private void DeleteContents()
        {
            if (LvContents.SelectedItems == null && LvContents.SelectedItems.Count <= 0)
                return;
            if (ArchId <= 0) {
                MessageBox.Show("请先选择档案!");
                return;
            }
            if (Mtmpid <= 0) {
                MessageBox.Show("请先选择目录!");
                return;
            }
            Common.ContentsDel(ClsContenInfo.ContenTable, Mtmpid, ArchId);
            ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            AddTitle();
            if (this.LvModule.Items.Count > 0)
                this.LvModule.Items[this.LvModule.Items.Count - 1].EnsureVisible();

        }

        public void LoadContents(int arid, int maxpage)
        {
            if (arid <= 0)
                return;
            ArchId = arid;
            ArchMaxPage = maxpage;
            ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);
        }
        public void LoadContents()
        {
            if (ArchId <= 0)
                return;
            ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (this.txtCode.Text.Trim().Length <= 0)
                    return;
                if (ClsContenInfo.LsModuleIndex.Count > 0 && ClsContenInfo.LsModule.Count > 0) {
                    string sttCode = this.txtCode.Text.Trim();
                    string str=ClsContenInfo.LsModule[ClsContenInfo.LsModuleIndex.IndexOf(sttCode)];
                    ClsConten.SetInfoTxt(panel1, str);
                }
            }
        }

        private void chkTspages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTspages.Checked) {
                ClsConten.LoadContents(ArchId, LvContents, chkTspages.Checked);
            }
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            ContentsEdit();
            if (this.LvModule.Items.Count > 0)
                this.LvModule.Items[this.LvModule.Items.Count - 1].EnsureVisible();
        }

        private void LvContents_Click(object sender, EventArgs e)
        {
            if (LvContents.SelectedItems != null && LvContents.SelectedItems.Count > 0) {
                Settxt();
                OneClickGotoPage?.Invoke(sender, e);
            }
        }

        private void Settxt()
        {
            for (int i = 1; i < LvContents.Columns.Count; i++) {
                string str = LvContents.SelectedItems[0].SubItems[i].Text;
                if (i == 1)
                    Mtmpid = Convert.ToInt32(str);
                else {
                    ClsConten.SetInfoTxt(panel1, (i - 1), str);
                }
            }
        }
        private void butDel_Click(object sender, EventArgs e)
        {
            DeleteContents();
            if (this.LvModule.Items.Count > 0)
                this.LvModule.Items[this.LvModule.Items.Count - 1].EnsureVisible();
        }
        public void OnChangContents(int page)
        {
            try {
                PageCrren = page;
                //  txtPage.Text = page.ToString();
                ListViewItem li = LvContents.Items.Cast<ListViewItem>().First(x => x.SubItems[2].Text == Convert.ToString(page));
                if (li != null) {
                    LvContents.SelectedItems.Clear();
                    li.Selected = true;
                }
            } catch { }

        }

        private void UcContents_Load(object sender, EventArgs e)
        {
            Init();
        }
    }
}