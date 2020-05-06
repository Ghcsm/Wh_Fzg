using DAL;
using Spire.Xls;
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
        public delegate void ArchSelectHandleFocus(object sender, EventArgs e);
        public event CntSelectHandle OneClickGotoPage;
        public delegate void CntSelectHandleG(object sender, EventArgs e);
        public delegate void CntSelectHandle(object sender, EventArgs e, string title, string page);
        public event ArchSelectHandleFocus LineFocus;
        public static bool ModuleVisible { get; set; }
        public static bool ContentsEnabled { get; set; }
        public static int ArchMaxPage { get; set; }
        public static int ArchId { get; set; }
        public static int ArchCheckZt { get; set; } = 0;
        public static int ArchStat { get; set; } = 0;
        public static int Mtmpid { get; set; } = 0;
        private int CrragePage = 0;
        public string ywid = "0";

        public int Infoadd = 0;
        ClsContenInfo info = new ClsContenInfo();
        private Workbook work = null;
        Worksheet wsheek = null;
        private void Init()
        {
            if (Modulename == null)
                Modulename = "目录录入";
            //  info = new ClsContenInfo();

            info.GetControl(panel1, gr2, LvModule);
            this.chbModule.Checked = ModuleVisible;
            this.gr0.Enabled = ContentsEnabled;
            gr2.Enabled = ContentsEnabled;
            this.chbModule.Refresh();
            this.gr0.Refresh();
            Lvnameadd();
            if (ModuleVisible)
                info.LoadModule(LvModule);
            info.LoadContents(ArchId, LvContents, chkTspages.Checked, 0);

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

        public static void LoadModule(DataTable dt, ListView lsv, string str)
        {
            lsv.Items.Clear();
            if (dt != null && dt.Rows.Count > 0) {
                DataTable dt1 = null;
                if (str.Trim().Length > 0) {
                    try {
                        string s = "TITLE like '%" + str + "%'";
                        dt1 = dt.Select(s).CopyToDataTable();
                    } catch {
                        dt1 = null;
                    }
                    if (dt1 == null || dt1.Rows.Count <= 0)
                        return;
                    foreach (DataRow dr in dt1.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        string type = dr["CoType"].ToString();
                        string code = dr["Code"].ToString();
                        string title = dr["Title"].ToString();
                        string titlelx = dr["TitleLx"].ToString();
                        lvi.Text = code;
                        lvi.SubItems.AddRange(new string[] { title, titlelx, type });
                        lsv.BeginInvoke(new Action(() => { lsv.Items.Add(lvi); }));
                    }
                }
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
            //if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            //    e.Handled = true;
            if (e.KeyChar == 13)
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
            if (!info.istxt(panel1)) {
                MessageBox.Show("标题名称不能为空!");
                return false;
            }

            string pages = Getpage();
            if (pages.Trim().Length <= 0) {
                MessageBox.Show("起始页码不能为空!");
                return false;
            }
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

        private string Getpage()
        {
            foreach (Control c in panel1.Controls) {
                if (c is TextBox || c is ComboBox)
                    if (c.Tag != null) {
                        if (c.Tag.ToString() == "2")
                            return c.Text.Trim();
                    }

            }
            return "";
        }

        //东丽区 不清空业务id
        private void cleTxt()
        {
            foreach (Control c in panel1.Controls) {
                if (c is TextBox || c is ComboBox)
                    if (c.Tag != null) {
                        if (c.Tag.ToString() != "4")
                            c.Text = "";
                    }

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
            info.LoadContents(ArchId, LvContents, chkTspages.Checked, CrragePage);
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
            info.LoadContentsadd(ArchId, LvContents, chkTspages.Checked, Infoadd);
            //txtCode.Focus();
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
            info.LoadContents(ArchId, LvContents, chkTspages.Checked, CrragePage);
        }


        private void butAdd_Click(object sender, EventArgs e)
        {
            if (ArchStat >= (int)T_ConFigure.ArchStat.质检完 && ArchCheckZt == 0) {
                MessageBox.Show("案卷已经质检完成无法修改目录");
                return;
            }
            AddTitle();
            if (this.LvContents.Items.Count > 0 && CrragePage > 0) {
                if (CrragePage < LvContents.Items.Count - 1)
                {
                    LvContents.Items[CrragePage].Selected = true;
                    LvContents.Items[CrragePage].EnsureVisible();
                }
            }
            LineFocus?.Invoke(sender, new EventArgs());
            //info.Setinfofocus(panel1);
            txtCode.Text = "";
            txtCode.Focus();
        }

        public void Setinfofocus()
        {
            info.Setinfofocus(panel1);
        }

        //private List<string> LsPage = new List<string>();
        //private List<string> Lsywid = new List<string>();
        public bool IsGetywid()
        {
            if (LvContents.Items.Count <= 0) {
                MessageBox.Show("未发现目录!");
                return false;
            }
            for (int i = 0; i < LvContents.Items.Count; i++) {
                string ywid = LvContents.Items[i].SubItems[4].Text;
                if (ywid.Trim().Length <= 0) {
                    MessageBox.Show("业务ID不能为空!");
                    return false;
                }
            }
            string ml = LvContents.Items[0].SubItems[2].Text;
            if (ml.Trim() != "目录") {
                MessageBox.Show("第一条目录名称不正确!");
                return false;
            }
            return true;
        }


        public void LoadContents(int arid, int maxpage)
        {
            if (arid <= 0)
                return;
            ArchId = arid;
            ArchMaxPage = maxpage;
            info.LoadContents(ArchId, LvContents, chkTspages.Checked, CrragePage);
        }

        public void LoadContentsinfo(int arid, int maxpage)
        {
            if (arid <= 0)
                return;
            ArchId = arid;
            ArchMaxPage = maxpage;
            info.LoadContentsinfo(ArchId, LvContents, chkTspages.Checked);
        }



        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (this.txtCode.Text.Trim().Length <= 0)
                    return;
                cleTxt();
                if (info.LsModuleIndex.Count > 0 && info.LsModule.Count > 0) {
                    string sttCode = this.txtCode.Text.Trim();
                    // int id = info.LsModuleIndex.IndexOf(sttCode);
                    int id;
                    bool bl = int.TryParse(sttCode, out id);
                    if (!bl)
                        return;
                    //id -= 1;
                    id = info.LsModuleIndex.IndexOf(id.ToString().PadLeft(2,'0'));
                    if (id < 0)
                        return;
                    if (id > info.LsModule.Count - 1)
                        return;
                    string str = info.LsModule[id];
                    info.SetInfoTxt(panel1, str);
                }
            }
        }

        private void chkTspages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTspages.Checked) {
                info.LoadContents(ArchId, LvContents, chkTspages.Checked, CrragePage);
            }
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            //if (ArchCheckZt < 1)
            //{
            if (ArchStat >= (int)T_ConFigure.ArchStat.质检完 && ArchCheckZt == 0) {
                MessageBox.Show("案卷已经质检完成无法修改目录");
                return;
            }
            // }
            ContentsEdit();
            if (this.LvContents.Items.Count > 0) {
                if (CrragePage < this.LvContents.Items.Count)
                {
                    LvContents.Items[CrragePage].Selected = true;
                    LvContents.Items[CrragePage].EnsureVisible();
                }
                LvContents.Focus();
            }
        }

        private void LvContents_Click(object sender, EventArgs e)
        {
            if (LvContents.SelectedItems != null && LvContents.SelectedItems.Count>0) {
                Settxt(sender, e);
            }
        }

        private void Settxt(object sender, EventArgs e)
        {
            int pid = info.PagesWz;
            int tid = info.TitleWz;
            string page = "";
            string title = "";
            CrragePage = LvContents.SelectedItems[0].Index;
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
                if (i == 5)
                    ywid = str;
            }
            if (title.Trim().Length > 0)
                OneClickGotoPage?.Invoke(sender, e, title, page);
        }

        public void Settxt(string title)
        {
            int pid = info.PagesWz;
            int tid = info.TitleWz;
            info.SetInfoTxt(panel1, pid, CrragePage.ToString());
            info.SetInfoTxt(panel1, tid + 1, title);
        }
        public void Setocrtxt(string title)
        {
            int pid = info.PagesWz;
            int tid = info.TitleWz;
            info.SetinfoOcrtxt(panel1, pid, CrragePage.ToString(), tid + 1, title);
            //info.SetInfoTxt(panel1, pid, CrragePage.ToString());
            // info.SetInfoTxt(panel1, tid + 1, title);
        }

        public static void Setxtxtls(Panel p, int id, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == (id + 2).ToString()) {
                        ct.Text = str;
                    }
                }
            }
        }

        public static void Setadd(GroupBox g)
        {
            try {
                foreach (Control c in g.Controls) {
                    if (c is DevComponents.DotNetBar.ButtonX) {
                        if (c.Text.Contains("新增")) {
                            DevComponents.DotNetBar.ButtonX but = (DevComponents.DotNetBar.ButtonX)c;
                            but.PerformClick();
                            //SendKeys.Send("{Enter}");
                        }
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("此处错误:" + e.ToString());
            }
        }

        public static void SetXg(GroupBox g)
        {
            try {
                foreach (Control c in g.Controls) {
                    if (c is DevComponents.DotNetBar.ButtonX) {
                        if (c.Text.Contains("修改")) {
                            DevComponents.DotNetBar.ButtonX but = (DevComponents.DotNetBar.ButtonX)c;
                            but.PerformClick();
                            //SendKeys.Send("{Enter}");
                        }
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("此处错误:" + e.ToString());
            }
        }

        public static void Setinfo(Panel g, string str)
        {
            foreach (Control c in g.Controls) {
                if (c is ComboBox) {
                    if (c.Tag != null && c.Tag.ToString() == "4")
                        c.Text = str;
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
                    info.SetInfoTxtcls(panel1, info.PagesWz + 1, page.ToString());
                if (LvContents.Items.Count > 0) {
                    LvContents.Items[CrragePage].Selected = true;
                    LvContents.Items[CrragePage].EnsureVisible();
                }
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
            if (LvContents.SelectedItems.Count >0)
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

        private void txtCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCode.Text.Trim().Length <= 0)
                return;
            int b;
            bool bl = int.TryParse(txtCode.Text.Trim(), out b);
            if (!bl) {
                info.LoadModule1(LvModule, txtCode.Text.Trim());
            }
        }

        //void Drxls()
        //{
        //    OpenFileDialog openFile = new OpenFileDialog();
        //    openFile.Filter = "xls文件|*.xlsx";
        //    if (openFile.ShowDialog() == DialogResult.OK) {
        //        string xls = openFile.FileName;
        //        work = new Workbook();
        //        work.LoadFromFile(xls);
        //        wsheek = work.Worksheets[0];
        //        int rows = wsheek.LastRow;
        //        for (int i = 0; i < rows; i++) {
        //            string id = wsheek.Rows[i].Cells[0].Value;
        //            string wensn = wsheek.Rows[i].Cells[1].Value;
        //            string zrz = wsheek.Rows[i].Cells[2].Value;
        //            string tit = wsheek.Rows[i].Cells[3].Value;
        //            string date = wsheek.Rows[i].Cells[4].Value;
        //            string page = wsheek.Rows[i].Cells[5].Value;
        //                if (tit.Trim().Length <= 0)
        //                    continue;
        //                if (tit.Contains("备考表") || tit.Contains("题名"))
        //                    continue;
        //                if (page.Contains("页"))
        //                    continue;
        //                if (wensn.Trim().Length > 0)
        //                    wensn = wensn.Replace(" ", "").ToString().Replace("'", "").Replace("【","").Replace("】","");
        //                if (zrz.Trim().Length > 0)
        //                    zrz = zrz.Replace(" ", "").ToString().Replace("'", "");
        //                if (zrz.Contains("公诉科"))
        //                    zrz = zrz.Replace("公诉科", "");
        //                if (tit.Trim().Length > 0)
        //                    tit = tit.Replace(" ", "").ToString().Replace("'", "");
        //                if (tit == "讯问笔录")
        //                    tit = "讯问XXX笔录";
        //                if (tit == "讯问提纲")
        //                    tit = "讯问XXX提纲";
        //                if (date.Trim().Length > 0) {
        //                    date = date.Replace(" ", "").ToString().Replace("'", "");
        //                    date = Istime(date);
        //                }
        //                if (page.Trim().Length > 0) {
        //                    page = page.Replace(" ", "").ToString().Replace("'", "");
        //                    page = ispage(page);
        //                }
        //              if (tit.Trim().Length>0)
        //                Common.ContentsInster2(wensn, zrz, tit, date, page, ArchId);
        //            }
        //        info.LoadContentsadd(ArchId, LvContents, chkTspages.Checked, Infoadd);
        //    }
        //}

        string ispage(string p)
        {
            int id = p.IndexOf("-");
            string str = p;
            if (id >= 0)
                str = p.Substring(0, id);
            else {
                id = p.IndexOf("\\");
                if (id >= 0)
                    str = p.Substring(0, id);
            }
            return str;
        }


        string Istime(string str)
        {
            string time = "";
            DateTime dt;
            try {
                if (str.Contains(".")) {
                    bool bl = DateTime.TryParse(str, out dt);
                    if (bl) {
                        time = dt.ToString("yyyyMMdd");
                    }
                }
                else
                    time = str;
            } catch {
                time = str;
            }
            return time;
        }

        //private void chkDrxls_Click(object sender, EventArgs e)
        //{
        //    if (ArchId <= 0)
        //        return;
        //    if (chkDrxls.Checked)
        //        try {
        //            Drxls();
        //        } catch (Exception ex) {
        //            MessageBox.Show("导入失败:" + ex.ToString());
        //        }
        //    chkDrxls.Checked = false;
        //}

        //private void butqu_Click(object sender, EventArgs e)
        //{
        //    if (ArchId <= 0)
        //        return;
        //    Common.ContenJmqu(1, ArchId);
        //    info.LoadContentsadd(ArchId, LvContents, chkTspages.Checked, Infoadd);
        //}

        //private void butshi_Click(object sender, EventArgs e)
        //{

        //    if (ArchId <= 0)
        //        return;
        //    Common.ContenJmqu(2, ArchId);
        //    info.LoadContentsadd(ArchId, LvContents, chkTspages.Checked, Infoadd);
        //}
     

        private void LvContents_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Down)
            {
                CrragePage = LvContents.SelectedItems[0].Index;
                LvContents.Items[CrragePage].Selected = false;
                if (CrragePage< LvContents.Items.Count-1)
                LvContents.Items[CrragePage+1].Selected = true;
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                CrragePage = LvContents.SelectedItems[0].Index;
                LvContents.Items[CrragePage].Selected = false;
                if (CrragePage>=1)
                LvContents.Items[CrragePage -1].Selected = true;
            }
        }
    }
}
