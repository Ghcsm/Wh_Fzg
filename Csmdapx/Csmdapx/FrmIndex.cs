using CsmCon;
using DAL;
using HLFtp;
using HLjscom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsmImg;

namespace Csmdapx
{
    public partial class FrmIndex : Form
    {
        public FrmIndex()
        {
            InitializeComponent();
        }
        private gArchSelect gArch;
        Hljsimage Himg = new Hljsimage();
        HFTP ftp = new HFTP();
        private Pubcls pub;
        private int Yuan = 0;
        private MouseEventArgs exArgs;
        private void Init()
        {
            try {
                gArch = new gArchSelect
                {
                    GotoPages = true,
                    LoadFileBoole = true,
                    Dock = DockStyle.Fill
                };
                gArch.LineLoadFile += Garch_LineLoadFile;
                gr1.Controls.Add(gArch);
            } catch { }
        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            try {
                Init();
                Himg._Instimagtwain(this.ImgView, this.Handle, 0);
                Himg._Rectang(true);
                Getsqlkey();
                pub = new Pubcls();
            } catch (Exception ex) {
                MessageBox.Show("初始化失败请重新加载" + ex.ToString());
                Himg.Dispose();
            }
        }

        private void FrmIndex_Shown(object sender, EventArgs e)
        {
            Himg.Spage += new Hljsimage.ScanPage(GetPages);
            ftp.PercentChane += new HLFtp.HFTP.PChangedHandle(Downjd);
        }


        private void Garch_LineLoadFile(object sender, EventArgs e)
        {
            try {

                if (ImgView.Image == null && ClsIndex.ArchPos == null ||
                    ImgView.Image == null && ClsIndex.ArchPos.Trim().Length <= 0) {

                    if (ClsIndex.task)
                        return;
                    ClsIndex.task = true;
                    ClsIndex.ArchPos = gArch.ArchPos;
                    ClsIndex.Archid = gArch.Archid;
                    ClsIndex.RegPage = gArch.ArchRegPages;
                    Himg.RegPage = ClsIndex.RegPage;
                    toolArchno.Text = string.Format("当前卷号:{0}", ClsIndex.ArchPos);
                    gArch.butLoad.Enabled = false;
                    LoadArch();
                    txtPages.Focus();
                    return;
                }
                MessageBox.Show("请退出当前卷再进行操作！");
                gArch.Focus();
            } catch (Exception ex) {
                Cledata();
                MessageBox.Show(ex.ToString());
            }
        }
        #region ClickEve
        private void ImgView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Himg._ImgFill(e);
            txtPages.Focus();
        }

        private void toolStripInsterImg_Click(object sender, EventArgs e)
        {
            if (odgInsterFile.ShowDialog() == DialogResult.OK) {
                Himg._Insterpage(odgInsterFile.FileName);
                ClsIndex.MaxPage = Himg._CountPage();
            }
        }

        private void toolStripRevImg_Click(object sender, EventArgs e)
        {
            Himg._Fliprevimage(1);
        }

        private void toolStripTrimImg_Click(object sender, EventArgs e)
        {
            Frmwt wt = new Frmwt();
            wt.imgview.Image = ImgView.Image.Clone();
            wt.ShowDialog();
            if (Frmwt.wtid == 1) {
                ImgView.Image = wt.imgview.Image;
            }
        }

        private void toolStripFontDeep_Click(object sender, EventArgs e)
        {
            Himg._Imagefontcolor(2);
        }

        private void toolStripZhantie_Click(object sender, EventArgs e)
        {
            if (exArgs == null)
                return;
            Himg.PasteImg(exArgs);
            exArgs = null;
        }

        private void toolStripFontShall_Click(object sender, EventArgs e)
        {
            Himg._Imagefontcolor(3);
        }

        private void toolStripGotoPage_Click(object sender, EventArgs e)
        {
            FrmGoto Fgoto = new FrmGoto();
            FrmGoto.Maxpage = ClsIndex.MaxPage;
            Fgoto.ShowDialog();
            if (FrmGoto.Npage > 0) {
                Himg._Gotopage(FrmGoto.Npage);
            }
        }

        private void toolStripAutoSide_Click(object sender, EventArgs e)
        {
            Himg._AutoCrop();
        }

        private void toolStripFiltr_Click(object sender, EventArgs e)
        {
            Himg._ImgLd();
        }

        private void toolStripImportImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedDialog = new SaveFileDialog();
            if (savedDialog.ShowDialog() == DialogResult.OK) {
                string strfile = savedDialog.FileName;
                Himg._ImporPage(strfile);
            }
        }
        private void toolStripWidthsider_Click(object sender, EventArgs e)
        {
            Himg.SetImgWidthSide(1);
        }
        private void toolStripYzidthsider_Click(object sender, EventArgs e)
        {
            Himg.SetImgWidthSide(0);
        }

        private void toolStripSplit_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;
            FrmCombe combe = new FrmCombe();
            combe.ImgPage = ClsIndex.MaxPage;
            combe.file = ClsIndex.ScanFilePath;
            combe.ShowDialog();
        }

        private void toolStripCenter_Click(object sender, EventArgs e)
        {
            Himg._CenterImg();
        }

        private void toolStripDel_Click(object sender, EventArgs e)
        {
            if (txtPages.ReadOnly) {
                txtPages.ReadOnly = false;
                txtPages.Text = "";
                txtPages.SelectAll();
            }
            else {
                txtPages.Text = "已删除";
                txtPages.ReadOnly = true;
            }
        }

        private void toolStripCopy_Click(object sender, EventArgs e)
        {
            Himg._CopyImg();
        }

        private void toolStripRecov_Click(object sender, EventArgs e)
        {
            // Himg.LoadPage(ClsIndex.CrrentPage); ;
            Himg.HufuImg(ClsIndex.ScanFilePathtmp, ClsIndex.CrrentPage);
        }

        private void toolStripUppage_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null || ClsIndex.CrrentPage == 1)
                return;
            Himg._Pagenext(0);
        }

        private void toolStripDownPage_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;
            NexPage();
            txtPages.Focus();
        }

        void NexPage()
        {
            string txt = txtPages.Text.Trim();
            if (txt.Trim().Length <= 0 || txt.Trim() == "0") {
                MessageBox.Show("页码不能为空!");
                txtPages.Focus();
                txtPages.SelectAll();
                return;
            }
            //int p;
            //bool bl = int.TryParse(txt, out p);
            //if (bl) {
            //    if (p > ClsIndex.RegPage) {
            //        MessageBox.Show("页码不能大于登记页码!");
            //        txtPages.Focus();
            //        txtPages.SelectAll();
            //    }
            //}
            Himg._Oderpage(txt);
            if (ClsIndex.CrrentPage == ClsIndex.MaxPage)
                this.toolStripSave_Click(null, null);
            else
                Himg._Pagenext(1);

        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (Himg._Checkpage() == true) {
                if (MessageBox.Show("排序完成您确定要上传档案吗？", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK) {
                    string filetmp = ClsIndex.ScanFilePath;
                    int arid = ClsIndex.Archid;
                    string archpos = ClsIndex.ArchPos;
                    int regpage = ClsIndex.RegPage;
                    int tagpage = Himg.TagPage;
                    string file2 = ClsIndex.ScanFilePathtmp;
                    Dictionary<int, string> pageabc = new Dictionary<int, string>(Himg._PageAbc);
                    Dictionary<int, int> pagenum = new Dictionary<int, int>(Himg._PageNumber);
                    Dictionary<int, string> fuhao = new Dictionary<int, string>(Himg._PageFuhao);
                    Himg._PageAbc.Clear();
                    Himg._PageNumber.Clear();
                    //天津东丽专用，以后删除
                    Setpages(regpage, pageabc.Count, arid);
                    // Task.Run(() => { FtpUpFinish(tagpage, regpage, filetmp, arid, archpos, pageabc, pagenum, fuhao); });
                    Task.Run(new Action(() => { FtpUpFinish(file2, tagpage, regpage, filetmp, arid, archpos, pageabc, pagenum, fuhao); }));
                    Cledata();
                    txtPages.Text = "";
                    gArch.txtBoxsn.Focus();
                }
            }
        }

        void Setpages(int reg, int abc, int arid)
        {
            try {
                int p = reg - abc;
                Common.SetPages(p, arid);
            } catch {
            }
        }

        private void toolStripClose_Click(object sender, EventArgs e)
        {
            string filetmp = ClsIndex.ScanFilePath;
            int arid = ClsIndex.Archid;
            string archpos = ClsIndex.ArchPos;
            Dictionary<int, string> pageabc = new Dictionary<int, string>(Himg._PageAbc);
            Dictionary<int, int> pagenum = new Dictionary<int, int>(Himg._PageNumber);
            Dictionary<int, string> fuhao = new Dictionary<int, string>(Himg._PageFuhao);
            Himg._PageAbc.Clear();
            Himg._PageNumber.Clear();
            int tag = Himg.TagPage;
            int pages = ClsIndex.RegPage;
            string file2 = ClsIndex.ScanFilePathtmp;
            Cledata();
            // Task.Run(() => { FtpUpCanCel(filetmp, arid, archpos, pageabc, pagenum, pages, fuhao, tag); });
            Task.Run(new Action(() => { FtpUpCanCel(filetmp, file2, arid, archpos, pageabc, pagenum, pages, fuhao, tag); }));
            txtPages.Text = "";
            gArch.LvData.Focus();
        }

        private void toolStripBigPage_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(1);
            txtPages.Focus();
        }


        private void toolStripSamllPage_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(0);
            txtPages.Focus();
        }

        private void toolStripRoteImg_Click(object sender, EventArgs e)
        {
            Himg._Roteimage(1);
        }

        private void toolStripDeskew_Click(object sender, EventArgs e)
        {
            Himg._Deskewimage(0);
        }

        private void toolStripCleSide_Click(object sender, EventArgs e)
        {
            Himg._Sidecrop(1);
        }

        private void toolStripCut_Click(object sender, EventArgs e)
        {
            Himg._Sidecrop(2);
        }

        private void toolStripColorDeep_Click(object sender, EventArgs e)
        {
            Himg._Imagefontcolor(0);
        }

        private void toolStripColorShall_Click(object sender, EventArgs e)
        {
            Himg._Imagefontcolor(1);
        }

        private void toolStripInterSpeck_Click(object sender, EventArgs e)
        {
            Himg._Fillrect(0);
        }

        private void toolStripOutSpeck_Click(object sender, EventArgs e)
        {
            Himg._Fillrect(1);
        }

        private void toolStripCheckImg_Click(object sender, EventArgs e)
        {
            if (Himg._Checkpage()) {
                MessageBox.Show("校验完成未发现问题!");
            }
        }

        private void toolStripSider_Click(object sender, EventArgs e)
        {
            Himg._Clesider();
        }

        private void toolStripHole_Click(object sender, EventArgs e)
        {
            Bitmap b = Himg.Getbmp(ClsIndex.ScanFilePath, ClsIndex.CrrentPage);
            string sfile = ClsImg.CleHole(b);
            Himg._RelImage(sfile, ClsIndex.ScanFilePath);
        }

        private void toolStripYuan_Click(object sender, EventArgs e)
        {
            Yuan = 1;
        }

        private void ImgView_MouseClick(object sender, MouseEventArgs e)
        {
            exArgs = e;
        }
        private void FrmIndex_KeyDown(object sender, KeyEventArgs e)
        {
            pub.KeyShortDown(e, ClsIndex.lsinival, ClsIndex.Lsinikeys, ClsIndex.lssqlOpernum, ClsIndex.lsSqlOper, out ClsIndex.keystr);
            if (ClsIndex.keystr.Trim().Length > 0)
                KeysDownEve(ClsIndex.keystr.Trim());
            Keys keyCode = e.KeyCode;
            if (e.KeyCode == Keys.Enter)
                toolStripDownPage_Click(null, null);
            if (e.KeyCode == Keys.Escape) {
                gArch.txtBoxsn.Focus();
                gArch.txtBoxsn.SelectAll();
            }

        }
        private void ImgView_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right) {
                if (Himg.CopyImgid == 1) {
                    Himg.CombineFloater();
                    Himg._Rectang(true);
                    Himg.CopyImgid = 0;
                }
                else
                    toolStripCut_Click(sender, e);
            }
            else {
                if (Yuan == 0) {
                    Himg._Rectang(true);
                }
                else {
                    Himg._RectangYuan(true);
                    Yuan = 0;
                }
            }

        }
        private void toolStripSplitTag_Click(object sender, EventArgs e)
        {
            if (Himg.TagPage > 0) {
                Himg.TagPage = 0;
                toolStripSplitTag.ForeColor = Color.Black;
                return;
            }
            int p;
            bool bl = int.TryParse(txtPages.Text.Trim(), out p);
            if (!bl) {
                MessageBox.Show("页码不正常无法标记!");
                return;
            }
            Himg.TagPage = p;
            toolStripSplitTag.ForeColor = Color.Red;

        }

        #endregion

        #region Method

        void Getsqlkey()
        {
            Task.Run(() =>
            {
                ClsIndex.lsSqlOper.Clear();
                ClsIndex.lssqlOpernum.Clear();
                ClsIndex.lsinival.Clear();
                ClsIndex.Lsinikeys.Clear();
                DataTable dt = Common.GetSqlkey(this.Text);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string key = dt.Rows[i][0].ToString();
                    string val = dt.Rows[i][1].ToString();
                    ClsIndex.lsSqlOper.Add(key);
                    ClsIndex.lssqlOpernum.Add(val);
                }
                dt = Common.GetOpenkey(this.Text);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string strid = dt.Rows[i][0].ToString();
                    string strkey = dt.Rows[i][2].ToString();
                    string strnum = dt.Rows[i][3].ToString();
                    if (strid.Trim().Length > 0 && strkey.Trim().Length > 0 && strnum.Trim().Length > 0) {
                        ClsIndex.Lsinikeys.Add(strkey);
                        ClsIndex.lsinival.Add(strnum);
                        // ClsTwain.LsId.Add(strid);
                    }
                }
                SettxtTag();
            });
        }

        void SettxtTag()
        {
            this.BeginInvoke(new Action(() =>
            {
                ToolStripButton t = null;
                foreach (var item in toolstripmain1.Items) {
                    if (item is ToolStripButton) {
                        t = (ToolStripButton)item;
                        if (t.Text.Trim().Length > 0) {
                            int name = ClsIndex.lsSqlOper.IndexOf(t.Text.Trim());
                            if (name < 0)
                                continue;
                            string oper = ClsIndex.lssqlOpernum[name];
                            if (oper.Trim().Length <= 0)
                                continue;
                            int id = ClsIndex.Lsinikeys.IndexOf("V" + oper);
                            if (id < 0)
                                continue;
                            string val = ClsIndex.lsinival[id];
                            val = pub.GetkeyVal(val);
                            if (val.Trim().Length <= 0)
                                continue;
                            t.ToolTipText = "快捷键：" + val;
                        }
                    }
                }
                foreach (var item in toolstripmain2.Items) {
                    if (item is ToolStripButton) {
                        t = (ToolStripButton)item;
                        if (t.Text.Trim().Length > 0) {
                            int name = ClsIndex.lsSqlOper.IndexOf(t.Text.Trim());
                            if (name < 0)
                                continue;
                            string oper = ClsIndex.lssqlOpernum[name];
                            if (oper.Trim().Length <= 0)
                                continue;
                            int id = ClsIndex.Lsinikeys.IndexOf("V" + oper);
                            if (id < 0)
                                continue;
                            string val = ClsIndex.lsinival[id];
                            val = pub.GetkeyVal(val);
                            if (val.Trim().Length <= 0)
                                continue;
                            t.ToolTipText = "快捷键：" + val;
                        }
                    }
                }
            }));
        }



        void KeysDownEve(string key)
        {
            string[] str = key.Split(':');
            for (int i = 0; i < str.Length; i++) {
                string s = str[i];
                bool bl = false;
                foreach (var item in toolstripmain1.Items) {
                    if (item is ToolStripButton) {
                        ToolStripButton t = (ToolStripButton)item;
                        if (t.Text == s) {
                            t.PerformClick();
                            bl = true;
                        }
                    }
                }

                if (!bl) {
                    foreach (var item in toolstripmain2.Items) {
                        if (item is ToolStripButton) {
                            ToolStripButton t = (ToolStripButton)item;
                            if (t.Text == s) {
                                t.PerformClick();
                                bl = true;
                            }
                        }
                    }
                }
            }

        }

        private void GetPages(int page, int counpage)
        {
            ClsIndex.MaxPage = counpage;
            ClsIndex.CrrentPage = page;
            labPageCrrent.Text = string.Format("第  {0}   页", page);
            labPageCount.Text = string.Format("共  {0}   页", counpage);
            ShowPage();
        }
        private void ShowPage()
        {
            string txt = Himg._Readpage();
            string txtpage = txtPages.Text.Trim();
            if (txt == "-9999") {
                txt = "已删除";
                txtPages.ReadOnly = true;
            }
            else
                txtPages.ReadOnly = false;
            if (txt.Trim().Length <= 0) {
                if (txtpage.Trim().Length > 0) {
                    if (txtPages.Text.Trim().IndexOf('-') < 0) {
                        int p;
                        bool bl = int.TryParse(txtpage, out p);
                        if (bl)
                            txt = (p + 1).ToString();
                        else
                            txt = ClsIndex.CrrentPage.ToString();
                    }
                    else {
                        string[] str = txtPages.Text.Trim().Split('-');
                        int p;
                        bool bl = int.TryParse(str[0], out p);
                        if (bl)
                            txt = (p + 1).ToString();
                        else
                            txt = ClsIndex.CrrentPage.ToString();
                    }

                }
                else
                    txt = ClsIndex.CrrentPage.ToString();
            }
            txtPages.Text = txt;
            txtPages.SelectAll();
        }

        private void LoadArch()
        {
            if (Common.Gettask(ClsIndex.Archid) > 0) {
                MessageBox.Show("正在任务中请稍后!");
                Cledata();
                return;
            }
            int stat = Common.GetArchWorkState(ClsIndex.Archid);
            if (stat == (int)T_ConFigure.ArchStat.排序中) {
                if (MessageBox.Show("您确认要强行进入排序吗?", "强行进入排序", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK) {
                    Cledata();
                    return;
                }
            }
            if (stat >= (int)T_ConFigure.ArchStat.排序完) {
                MessageBox.Show("此卷已排序完成,不能再进入排序！");
                Cledata();
                return;
            }
            ReadDict();
        }
        public void ReadDict()
        {
            int maxpage = 1;
            try {
                Dictionary<int, int> pagenumber = new Dictionary<int, int>();
                Dictionary<int, string> pageabc = new Dictionary<int, string>();
                Dictionary<int, string> fuhao = new Dictionary<int, string>();
                DataTable dt = Common.ReadPageIndexInfo(ClsIndex.Archid);
                if (dt != null && dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    string PageIndexInfo = dr["PageIndexInfo"].ToString();
                    string Page = dr["ArchPage"].ToString();
                    if (!string.IsNullOrEmpty(PageIndexInfo)) {
                        string[] arrPage = PageIndexInfo.Split(';');
                        if (arrPage.Length > 0) {
                            for (int i = 0; i < arrPage.Length; i++) {
                                string[] str = arrPage[i].Trim().Split(':');
                                if (str.Length <= 0)
                                    continue;
                                int p = Convert.ToInt32(str[0]);
                                //if (p > page)
                                //    continue;
                                if (str[1].ToString() == "-9999")
                                    pagenumber.Add(p, Convert.ToInt32(str[1]));
                                else if (!isExists(str[1].ToString()) && str[1].IndexOf("-") < 0)
                                    pagenumber.Add(p, Convert.ToInt32(str[1]));
                                else if (str[1].IndexOf("-") >= 0)
                                    fuhao.Add(p, str[1].ToString());
                                else
                                    pageabc.Add(p, str[1].ToString());
                            }
                        }
                    }
                    if (Page.Trim().Length > 0) {
                        string[] s = Page.Split(';');
                        for (int i = 0; i < s.Length; i++) {
                            string s1 = s[i];
                            if (s1.Trim().Length <= 0)
                                continue;
                            Himg.Fuhao.Add(s1);
                        }
                    }
                    Himg._PageNumber = pagenumber;
                    Himg._PageAbc = pageabc;
                    Himg._PageFuhao = fuhao;
                    int n, a, f;
                    if (pagenumber.Count <= 0)
                        n = 0;
                    else
                        n = pagenumber.Keys.Max();
                    if (pageabc.Count <= 0)
                        a = 0;
                    else
                        a = pageabc.Keys.Max();
                    if (fuhao.Count <= 0)
                        f = 0;
                    else
                        f = fuhao.Keys.Max();
                    int[] array = { n, a, f };
                    if (array.Length <= 0)
                        maxpage = 1;
                    else
                        maxpage = array.Max();
                    if (maxpage == 0)
                        maxpage = 1;
                }
                else {
                    txtPages.Text = "1";
                    maxpage = 1;
                }
            } catch (Exception ex) {
                maxpage = 1;
                MessageBox.Show("排序页码读取错误：" + ex.ToString());
            } finally {
                LoadImgShow(maxpage);
            }
        }


        private async void LoadImgShow(int pages)
        {
            bool loadfile = await LoadFile();
            gArch.butLoad.Enabled = true;
            if (loadfile == true) {
                if (!File.Exists(ClsIndex.ScanFilePath)) {
                    Cledata();
                    return;
                }
                Himg.Filename = ClsIndex.ScanFilePath;
                Himg.LoadPage(pages);
                Getuser();
                return;
            }
            MessageBox.Show("文件加载失败或不存在!");
            Cledata();
        }

        private Task<bool> LoadFile()
        {
            return Task.Run(() =>
            {
                try {
                    Common.SetArchWorkState(ClsIndex.Archid, (int)T_ConFigure.ArchStat.排序中);
                    if (T_ConFigure.FtpStyle == 1) {
                        string localPath = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex, ClsIndex.ArchPos);
                        string localScanFile = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex, ClsIndex.ArchPos, T_ConFigure.ScanTempFile);
                        ClsIndex.ScanFilePath = localScanFile;
                        ClsIndex.ScanFilePathtmp = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex, ClsIndex.ArchPos, T_ConFigure.ScanTempFiletmp);
                        if (!Directory.Exists(localPath))
                            Directory.CreateDirectory(localPath);
                        if (File.Exists(localScanFile))
                            File.Delete(localScanFile);
                        if (File.Exists(ClsIndex.ScanFilePathtmp))
                            File.Delete(ClsIndex.ScanFilePathtmp);
                        if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.gArchScanPath, ClsIndex.ArchPos, T_ConFigure.ScanTempFile))) {
                            string sourcefile = Path.Combine(T_ConFigure.gArchScanPath, ClsIndex.ArchPos, T_ConFigure.ScanTempFile);
                            string goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, ClsIndex.ArchPos, T_ConFigure.ScanTempFile);
                            string path = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, ClsIndex.ArchPos);
                            if (ftp.FtpMoveFile(sourcefile, goalfile, path))
                                if ((FileMoveBool(localScanFile))) {
                                    File.Copy(localScanFile, ClsIndex.ScanFilePathtmp);
                                    return true;
                                }
                        }
                    }
                    else {
                        string localPath = Path.Combine(T_ConFigure.LocalTempPath, ClsIndex.ArchPos);
                        string localScanFile = Path.Combine(T_ConFigure.LocalTempPath, ClsIndex.ArchPos, T_ConFigure.ScanTempFile);
                        ClsIndex.ScanFilePathtmp = Path.Combine(T_ConFigure.LocalTempPath, ClsIndex.ArchPos, T_ConFigure.ScanTempFiletmp);
                        ClsIndex.ScanFilePath = localScanFile;
                        if (!Directory.Exists(localPath))
                            Directory.CreateDirectory(localPath);
                        if (File.Exists(localScanFile))
                            File.Delete(localScanFile);
                        if (File.Exists(ClsIndex.ScanFilePathtmp))
                            File.Delete(ClsIndex.ScanFilePathtmp);
                        if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.gArchScanPath, ClsIndex.ArchPos, T_ConFigure.ScanTempFile))) {
                            if (ftp.DownLoadFile(T_ConFigure.gArchScanPath, ClsIndex.ArchPos, localScanFile, T_ConFigure.ScanTempFile)) {
                                File.Copy(localScanFile, ClsIndex.ScanFilePathtmp);
                                return true;
                            }
                        }
                    }
                    ClsIndex.task = false;
                    Cledata();
                    return false;
                } catch (Exception e) {
                    MessageBox.Show("加载文件失败!" + e.ToString());
                    Common.SetArchWorkState(ClsIndex.Archid, (int)T_ConFigure.ArchStat.扫描完);
                    Cledata();
                    return false;
                }
            });
        }

        bool FileMoveBool(string files)
        {
            int id = 0;
            while (true) {
                if (File.Exists(files))
                    return true;
                else {
                    Thread.Sleep(300);
                    id += 1;
                    if (id > 100)
                        return false;
                }
            }
        }

        private void Getuser()
        {
            Task.Run(new Action(() =>
            {
                string Scanner = string.Empty;
                string scantime = string.Empty;
                string Indexer = string.Empty;
                string indextime = string.Empty;
                string Checker = string.Empty;
                string chktime = string.Empty;
                string enter = string.Empty;
                string entertime = string.Empty;
                DataTable dt = Common.GetOperator(ClsIndex.Archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                DataRow dr = dt.Rows[0];
                Scanner = dr["扫描"].ToString();
                scantime = dr["扫描时间"].ToString();
                Indexer = dr["排序"].ToString();
                indextime = dr["排序时间"].ToString();
                Checker = dr["质检"].ToString();
                chktime = dr["质检时间"].ToString();
                enter = dr["录入"].ToString();
                entertime = dr["录入时间"].ToString();
                this.BeginInvoke(new Action(() =>
                {
                    this.labScanUser.Text = string.Format("扫描：{0}", Scanner);
                    this.labIndexUser.Text = string.Format("排序：{0}", Indexer);
                    this.labCheckUser.Text = string.Format("质检：{0}", Checker);

                    toollabscan.Text = string.Format("扫描:{0}", Scanner);
                    toollabscantime.Text = string.Format("时间:{0}", scantime);
                    toollabindex.Text = string.Format("排序:{0}", Indexer);
                    toollabindextime.Text = string.Format("时间:{0}", indextime);
                    toollabcheck.Text = string.Format("质检:{0}", Checker);
                    toollabchecktime.Text = string.Format("时间:{0}", chktime);
                    toollabenter.Text = string.Format("录入:{0}", enter);
                    toollabentertime.Text = string.Format("时间:{0}", entertime);

                }));
                dt.Dispose();

            }));
        }

        private bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }

        private void Downjd(object sender, PChangeEventArgs e)
        {
            this.toolStrip1.BeginInvoke(new Action(() =>
            {
                this.toolProess.Visible = true;
                this.toolProess.Minimum = 0;
                this.toolProess.Maximum = (int)e.CountSize;
                this.toolProess.Value = (int)e.TmpSize;
                if (e.CountSize == e.TmpSize) {
                    this.toolProess.Visible = false;
                }
            }));
        }
        private void Cledata()
        {
            this.BeginInvoke(new Action(() =>
            {
                ClsIndex.ScanFilePathtmp = "";
                Himg._PageNumber.Clear();
                Himg._PageAbc.Clear();
                Himg._PageFuhao.Clear();
                Himg.Fuhao.Clear();
                Himg.Filename = "";
                Himg.RegPage = 0;
                ClsIndex.ScanFilePath = "";
                ImgView.Image = null;
                ClsIndex.Archid = 0;
                ClsIndex.ArchPos = "";
                ClsIndex.MaxPage = 0;
                ClsIndex.CrrentPage = 0;
                ClsIndex.RegPage = 0;
                labPageCrrent.Text = "第     页";
                labPageCount.Text = "共      页";
                labScanUser.Text = "扫描:";
                labIndexUser.Text = "排序:";
                labCheckUser.Text = "质检:";
                toolArchno.Text = "当前卷号:";
                ClsIndex.task = false;
                gArch.butLoad.Enabled = true;
                Himg.TagPage = 0;
                toolStripSplitTag.ForeColor = Color.Black;
            }));
        }

        private void FtpUpFinish(string file2, int tagpage, int regpage, string filetmp, int arid, string archpos, Dictionary<int, string> pageAbc, Dictionary<int, int> pagenumber, Dictionary<int, string> fuhao)
        {
            try {
                if (File.Exists(filetmp)) {
                    string PageIndexInfo = "";
                    string PageIndexInfoOk = "";

                    foreach (var item in pageAbc) {
                        if (PageIndexInfo.Trim().Length <= 0)
                            PageIndexInfo += item.Key + ":" + item.Value;
                        else
                            PageIndexInfo += ";" + item.Key + ":" + item.Value;

                    }
                    foreach (var item in pagenumber) {
                        if (PageIndexInfo.Trim().Length <= 0)
                            PageIndexInfo += item.Key + ":" + item.Value;
                        else
                            PageIndexInfo += ";" + item.Key + ":" + item.Value;

                        string vale = item.Value.ToString();
                    }
                    foreach (var item in fuhao) {
                        if (PageIndexInfo.Trim().Length <= 0)
                            PageIndexInfo += item.Key + ":" + item.Value;
                        else
                            PageIndexInfo += ";" + item.Key + ":" + item.Value;
                    }
                    //Dictionary<int, string> dicxxabc = pageAbc.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                    //int id = 0;
                    //foreach (var item in dicxxabc) {
                    //    string vale = item.Value;
                    //    if (vale == "-9999")
                    //        continue;
                    //    id += 1;
                    //    if (PageIndexInfoOk.Trim().Length <= 0)
                    //        PageIndexInfoOk += id + ":" + item.Value;
                    //    else
                    //        PageIndexInfoOk += ";" + id + ":" + item.Value;
                    //}
                    //Dictionary<int, int> dicxxnum = pagenumber.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                    //foreach (var item in dicxxnum) {
                    //    string vale = item.Value.ToString();
                    //    if (vale == "-9999")
                    //        continue;
                    //    if (PageIndexInfoOk.Trim().Length <= 0)
                    //        PageIndexInfoOk += item.Key + ":" + item.Value;
                    //    else
                    //        PageIndexInfoOk += ";" + item.Key + ":" + item.Value;
                    //}
                    //Dictionary<int, string> dicxxfh = fuhao.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                    //foreach (var item in dicxxfh) {
                    //    string vale = item.Value.ToString();
                    //    if (vale == "-9999")
                    //        continue;
                    //    if (PageIndexInfoOk.Trim().Length <= 0)
                    //        PageIndexInfoOk += item.Key + ":" + item.Value;
                    //    else
                    //        PageIndexInfoOk += ";" + item.Key + ":" + item.Value;
                    //}
                    PageIndexInfo = PageIndexInfo.Trim();
                    //PageIndexInfoOk = PageIndexInfoOk.Trim();
                    Common.SetIndexCancel(arid, PageIndexInfo);
                    string time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string IndexFileName = time + Common.TifExtension;
                    string RemoteDir = IndexFileName.Substring(0, 8);
                    if (T_ConFigure.FtpStyle == 1) {
                        if (!Directory.Exists(Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex)))
                            Directory.CreateDirectory(Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex));
                        string LocalIndexFile = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex,
                            IndexFileName);
                        Common.WiteUpTask(arid, archpos, IndexFileName, (int)T_ConFigure.ArchStat.排序完, regpage, filetmp, tagpage.ToString());
                        if (!Himg._OrderSave(tagpage, regpage, filetmp, LocalIndexFile, pageAbc, pagenumber, fuhao, out PageIndexInfoOk)) {
                            return;
                        }
                        string sourcefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, IndexFileName);
                        string goalfile = Path.Combine(T_ConFigure.FtpArchIndex, RemoteDir, IndexFileName);
                        string path = Path.Combine(T_ConFigure.FtpArchIndex, RemoteDir);
                        if (ftp.FtpMoveFile(sourcefile, goalfile, path)) {
                            Thread.Sleep(5000);
                            //Common.SetIndexCancel(arid, "");
                            Common.DelTask(arid);
                            Common.SetIndexFinish(arid, DESEncrypt.DesEncrypt(IndexFileName), (int)T_ConFigure.ArchStat.排序完, PageIndexInfoOk);
                            try {
                                File.Delete(filetmp);
                                File.Delete(file2);
                                Directory.Delete(Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpScan, archpos));
                            } catch { }
                            return;
                        }
                    }
                    else {
                        string LocalIndexFile = Path.Combine(@T_ConFigure.LocalTempPath, IndexFileName);
                        Common.WiteUpTask(arid, archpos, IndexFileName, (int)T_ConFigure.ArchStat.排序完, regpage, filetmp, tagpage.ToString());
                        if (!Himg._OrderSave(tagpage, regpage, filetmp, LocalIndexFile, pageAbc, pagenumber, fuhao, out PageIndexInfoOk)) {
                            return;
                        }
                        if (ftp.SaveRemoteFileUp(T_ConFigure.FtpArchIndex, RemoteDir, LocalIndexFile, IndexFileName)) {
                            Common.SetIndexFinish(arid, DESEncrypt.DesEncrypt(IndexFileName), (int)T_ConFigure.ArchStat.排序完, PageIndexInfoOk);
                            Common.DelTask(Convert.ToInt32(arid));
                            try {
                                File.Delete(filetmp);
                                File.Delete(file2);
                                File.Delete(LocalIndexFile);
                                Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                                string file = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                                if (ftp.FtpDelFile(Path.Combine(T_ConFigure.gArchScanPath, archpos,
                                    T_ConFigure.ScanTempFile)))
                                    ftp.FtpDelDir(Path.Combine(T_ConFigure.gArchScanPath, archpos));
                            } catch { }
                            return;
                        }
                        //发现上传时图像有错位现象
                        //string newfile = Path.Combine(T_ConFigure.FtpArchIndex, RemoteDir, IndexFileName);
                        //string newpath = Path.Combine(T_ConFigure.FtpArchIndex, RemoteDir);
                        //bool x = await ftp.FtpUpFile(LocalIndexFile, newfile, newpath);
                        //if (x) {
                        //    //Common.SetIndexCancel(arid, "");
                        //    Common.SetIndexFinish(arid, DESEncrypt.DesEncrypt(IndexFileName), (int)T_ConFigure.ArchStat.排序完, PageIndexInfoOk);
                        //    Common.DelTask(arid);
                        //    try {
                        //        File.Delete(filetmp);
                        //        File.Delete(LocalIndexFile);
                        //        Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                        //        if (ftp.FtpDelFile(Path.Combine(T_ConFigure.gArchScanPath, archpos,
                        //            T_ConFigure.ScanTempFile)))
                        //            ftp.FtpDelDir(Path.Combine(T_ConFigure.gArchScanPath, archpos));
                        //    } catch { }
                        //    return;
                        //}
                    }

                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                }
                else
                    Common.Writelog(arid, "排序完成退出时未找到文件!");
            } catch (Exception ex) {
                MessageBox.Show("上传失败!" + ex.ToString());
                Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                Common.Writelog(ClsIndex.Archid, "排序完成失败");
            } finally {
                GC.Collect();
            }
        }

        private void FtpUpCanCel(string filetmp, string filetmp2, int arid, string archpos, Dictionary<int, string> pageAbc, Dictionary<int, int> pagenumber, int pages, Dictionary<int, string> fuhao, int tag)
        {
            try {
                if (File.Exists(filetmp)) {
                    Common.WiteUpTask(arid, archpos, T_ConFigure.ScanTempFile, (int)T_ConFigure.ArchStat.扫描完, pages, filetmp, tag.ToString());
                    string PageIndexInfo = "";
                    foreach (var item in pageAbc) {
                        if (PageIndexInfo.Trim().Length <= 0)
                            PageIndexInfo += item.Key + ":" + item.Value;
                        else
                            PageIndexInfo += ";" + item.Key + ":" + item.Value;
                    }
                    foreach (var item in pagenumber) {
                        if (PageIndexInfo.Trim().Length <= 0)
                            PageIndexInfo += item.Key + ":" + item.Value;
                        else
                            PageIndexInfo += ";" + item.Key + ":" + item.Value;
                    }
                    foreach (var item in fuhao) {
                        if (PageIndexInfo.Trim().Length <= 0)
                            PageIndexInfo += item.Key + ":" + item.Value;
                        else
                            PageIndexInfo += ";" + item.Key + ":" + item.Value;
                    }
                    PageIndexInfo = PageIndexInfo.Trim();
                    Common.SetIndexCancel(arid, PageIndexInfo);
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourcefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, archpos, T_ConFigure.ScanTempFile);
                        string goalfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        string path = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                        if (ftp.FtpMoveFile(sourcefile, goalfile, path)) {
                            Thread.Sleep(5000);
                            Common.DelTask(arid);
                            try {
                                Directory.Delete(Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex, archpos));
                            } catch { }
                            return;
                        }
                    }
                    else {

                        if (ftp.SaveRemoteFileUp(T_ConFigure.gArchScanPath, archpos, filetmp, T_ConFigure.ScanTempFile)) {
                            Common.SetScanFinish(arid, pages, 1, (int)T_ConFigure.ArchStat.扫描完);
                            Common.DelTask(Convert.ToInt32(arid));
                            try {
                                File.Delete(filetmp);
                                File.Delete(filetmp2);
                                Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                            } catch {
                            }
                            return;
                        }
                        // string newfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        //  string newpath = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                        //bool x = await ftp.FtpUpFile(filetmp, newfile, newpath);
                        //if (x) {
                        //    Common.DelTask(arid);
                        //    try {
                        //        File.Delete(filetmp);
                        //        Directory.Delete(Path.Combine(Common.LocalTempPath, archpos));
                        //    } catch { }
                        //    return;
                        //}

                    }
                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                }
                else
                    Common.Writelog(arid, "排序退出时未找到文件!");

            } catch {
                Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                Common.Writelog(ClsIndex.Archid, "排序未完成失败");

            } finally {
                GC.Collect();
            }
        }

        private void txtPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)32)
                e.Handled = true;
            if (!Pdzd(txtPages.Text.Trim(), e))
                e.Handled = true;
        }

        bool Pdzd(string str, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)13 && e.KeyChar != (char)8 && e.KeyChar != (char)45) {
                if (txtPages.SelectedText.IndexOf('-') >= 0)
                    return false;
                if (txtPages.SelectedText != "") {
                    int p;
                    bool bl = int.TryParse(str, out p);
                    if (!bl)
                        str = "";
                }
                if (str.Trim().Length > 0)
                    return false;
            }
            else if (isExists(str) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                return false;
            return true;
        }






        #endregion


    }
}
