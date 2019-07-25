using DAL;
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
        public static string Modulename { get; set; }
        //public event CntSelectHandleG GoFous;
        public event CntSelectHandle OneClickGotoPage;
        public delegate void CntSelectHandleG(object sender, EventArgs e);
        public delegate void CntSelectHandle(object sender, EventArgs e, string title, string page);
        public static bool ModuleVisible { get; set; }
        public static bool ContentsEnabled { get; set; }
        public static int ArchMaxPage { get; set; }
        public static int ArchId { get; set; }
        public static int ArchCheckZt { get; set; } = 0;
        public static int ArchStat { get; set; } = 0;
        public static int Mtmpid { get; set; } = 0;
        private int CrragePage = 0;
        ClsContenInfo info = new ClsContenInfo();
        private void Init()
        {
            if (Modulename == null)
                Modulename = "目录录入";
            //  info = new ClsContenInfo();

            info.GetControl(panel1);
            this.chbModule.Checked = ModuleVisible;
            this.gr0.Enabled = ContentsEnabled;
            gr2.Enabled = ContentsEnabled;
            this.chbModule.Refresh();
            this.gr0.Refresh();
            Lvnameadd();
            if (ModuleVisible)
                info.LoadModule(LvModule);
            info.LoadContents(ArchId, LvContents, chkTspages.Checked);

        }
        private void Lvnameadd()
        {
            if (info.ContenCoList.Count <= 0)
                return;
            for (int i = 0; i < info.ContenCoList.Count; i++) {
                string str = info.ContenCoList[i];
                if (i == info.TitleWz + 2)
                    LvContents.Columns[i].Width = 200;
                else if (i > 1)
                    LvContents.Columns[i].Width = 100;
                LvContents.Columns.Add(str);
            }
        }

        private void chbModule_CheckedChanged(object sender, EventArgs e)
        {
            if (chbModule.Checked) {
                this.LvContents.Width = this.gr1.Width - 176;
                this.LvModule.Visible = true;
                splitCont.Panel2Collapsed = false;
                info.LoadModule(LvModule);
            }
            else {
                this.LvContents.Width = this.gr1.Width - 10;
                this.LvModule.Visible = false;
                splitCont.Panel2Collapsed = true;
            }
        }

        private void butModule_Click(object sender, EventArgs e)
        {
            UcContenModule module = new UcContenModule();
            module.ShowDialog();
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

        #region 目录操作


        private bool istxt(bool id)
        {
            if (ArchId <= 0) {
                MessageBox.Show("请重新选择案卷!");
                return false;
            }
            if (!info.Gettxtzd(panel1)) {
                MessageBox.Show("标题及页码不能为空!");
                return false;
            }
            string pages = info.Pagestmp;
            int p = info.PagesWz;
            string pagestmp = "";
            if (id) {
                foreach (ListViewItem item in LvContents.Items) {
                    pagestmp = item.SubItems[p + 2].Text.ToString();
                    if (pages == pagestmp) {
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
            int id = Common.ContentsEdit(info.ContenTable, info.ContenCoList, dicxx, Mtmpid.ToString(), ArchId);
            if (id > 0)
                cleTxt();
            info.LoadContents(ArchId, LvContents, chkTspages.Checked);
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
            int id = Common.ContentsInster(info.ContenTable, info.ContenCoList, dicxx, ArchId);
            if (id > 0)
                cleTxt();
            info.LoadContents(ArchId, LvContents, chkTspages.Checked);
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
            Common.ContentsDel(info.ContenTable, Mtmpid, ArchId);
            info.LoadContents(ArchId, LvContents, chkTspages.Checked);
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (ArchStat >= (int)T_ConFigure.ArchStat.质检完 && ArchCheckZt == 0) {
                MessageBox.Show("案卷已经质检完成无法修改目录");
                return;
            }
            AddTitle();
            //if (this.LvModule.Items.Count > 0)
            //    this.LvModule.Items[this.LvModule.Items.Count - 1].EnsureVisible();
        }

        public void LoadContents(int arid, int maxpage)
        {
            if (arid <= 0)
                return;
            ArchId = arid;
            ArchMaxPage = maxpage;
            info.LoadContents(ArchId, LvContents, chkTspages.Checked);
        }
        public void LoadContents()
        {
            if (ArchId <= 0)
                return;
            info.LoadContents(ArchId, LvContents, chkTspages.Checked);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (this.txtCode.Text.Trim().Length <= 0)
                    return;
                if (info.LsModuleIndex.Count > 0 && info.LsModule.Count > 0) {
                    string sttCode = this.txtCode.Text.Trim();
                   // int id = info.LsModuleIndex.IndexOf(sttCode);
                    int id;
                    bool bl = int.TryParse(sttCode, out id);
                    if (!bl)
                        return;
                    string str = info.LsModule[id];
                    info.SetInfoTxt(panel1, str);
                }
            }
        }

        private void chkTspages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTspages.Checked) {
                info.LoadContents(ArchId, LvContents, chkTspages.Checked);
            }
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            if (ArchStat >= (int)T_ConFigure.ArchStat.质检完 && ArchCheckZt == 0) {
                MessageBox.Show("案卷已经质检完成无法修改目录");
                return;
            }
            ContentsEdit();
            if (this.LvModule.Items.Count > 0)
                this.LvModule.Items[this.LvModule.Items.Count - 1].EnsureVisible();
        }

        private void LvContents_Click(object sender, EventArgs e)
        {
            if (LvContents.SelectedItems != null && LvContents.SelectedItems.Count > 0) {
                Settxt(sender, e);
            }
        }

        private void Settxt(object sender, EventArgs e)
        {
            int pid = info.PagesWz;
            int tid = info.TitleWz;
            string page = "";
            string title = "";
            for (int i = 1; i < LvContents.Columns.Count; i++) {
                string str = LvContents.SelectedItems[0].SubItems[i].Text;

                if (i == 1)
                    Mtmpid = Convert.ToInt32(str);
                else {
                    info.SetInfoTxt(panel1, (i - 1), str);
                }
                if (i == pid + 2)
                    page = str;
                else if (i == tid + 2)
                    title = str;
            }
            if (title.Trim().Length > 0)
                OneClickGotoPage?.Invoke(sender, e, title, page);

        }

        private void Settxt(string title)
        {
            int pid = info.PagesWz;
            int tid = info.TitleWz;
            info.SetInfoTxt(panel1, pid, CrragePage.ToString());
            info.SetInfoTxt(panel1, tid, title);
        }

        public static void Setxtxtls(Panel p,int id, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == (id+2).ToString()) {
                        ct.Text = str;
                    }
                }
            }
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (ArchStat >= (int)T_ConFigure.ArchStat.质检完 && ArchCheckZt == 0) {
                MessageBox.Show("案卷已经质检完成无法修改目录");
                return;
            }
            DeleteContents();
            if (this.LvModule.Items.Count > 0)
                this.LvModule.Items[this.LvModule.Items.Count - 1].EnsureVisible();
        }
        public void OnChangContents(int page)
        {
            try {
                int x = info.PageCount.IndexOf(page.ToString());
                CrragePage = x;
                if (x >= 0) {
                    LvContents.SelectedItems.Clear();
                    LvContents.Items[x].Selected = true;
                }
                else if (page > 0)
                    info.SetInfoTxt(panel1, info.PagesWz + 1, page.ToString());
            } catch { }
        }

        public void CloseConten()
        {
            LvContents.Items.Clear();
        }
        private void UcContents_Load(object sender, EventArgs e)
        {
            Init();
        }
        private void LvContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LvContents.Items.Count > 0)
                LvContents_Click(null, null);
        }

        void DoubleModuleAddConte()
        {
            if (CrragePage <= 0)
                return;
            string title = LvModule.SelectedItems[0].SubItems[0].Text;
            Settxt(title);
            AddTitle();
        }

        private void LvModule_DoubleClick(object sender, EventArgs e)
        {
            if (LvModule.SelectedItems.Count <= 0)
                return;
            DoubleModuleAddConte();
        }

        #endregion


    }
}
