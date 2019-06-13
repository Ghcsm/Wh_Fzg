using CsmCon;
using DAL;
using HLFtp;
using HLjscom;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmdajc
{
    public partial class FrmCheck : Form
    {
        public FrmCheck()
        {
            InitializeComponent();

        }

        Hljsimage Himg = new Hljsimage();
        HFTP ftp = new HFTP();
        gArchSelect gArch;
        UcContents ucContents1;
        private int archzt = 0;
        private Pubcls pub;
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
                UcContents.Modulename = this.Text;
                UcContents.ArchId = Clscheck.Archid;
                UcContents.ContentsEnabled = true;
                UcContents.ModuleVisible = false;
                UcContents.ArchCheckZt = 1;
                ucContents1 = new UcContents();
                {
                    ucContents1.Dock = DockStyle.Fill;
                }
                ucContents1.OneClickGotoPage += UcContents1_OneClickGotoPage;
                gr2.Controls.Add(ucContents1);
            } catch (Exception ex) {
                MessageBox.Show("窗体控件初始化失败:" + ex.ToString());
            }
        }


        private void UcContents1_OneClickGotoPage(object sender, EventArgs e, string title, string page)
        {
            labConten.Text = title;
            int p = 0;
            try {
                p = Convert.ToInt32(page);
            } catch { }
            if (p > 0 && p < Clscheck.MaxPage)
                Himg._Gotopage(p);

        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            try {
                Init();
                Himg._Instimagtwain(this.ImgView, this.Handle, 0);
                Himg._Rectang(true);
                Writeini.Fileini = Path.Combine(Application.StartupPath, "Csmkeyval.ini");
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
                if (ImgView.Image == null && Clscheck.ArchPos == null ||
                    ImgView.Image == null && Clscheck.ArchPos.Trim().Length <= 0) {
                    if (Clscheck.task)
                        return;
                    Clscheck.task = true;
                    Clscheck.ArchPos = gArch.ArchPos;
                    Clscheck.Archid = gArch.Archid;
                    Clscheck.RegPage = gArch.ArchRegPages;
                    Clscheck.FileNametmp = gArch.ArchImgFile;
                    toolArchno.Text = string.Format("当前卷号:{0}", Clscheck.ArchPos);
                    gArch.butLoad.Enabled = false;
                    LoadArch();
                    ImgView.Focus();
                    return;
                }
                MessageBox.Show("请退出当前卷再进行操作！");
                gArch.Focus();
            } catch (Exception exception) {
                Cledata();
                MessageBox.Show(exception.ToString());
            } finally {
                gArch.butLoad.Enabled = true;
            }

        }

        #region ClickEve

        private void ImgView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                toolStripCut_Click(sender, e);
            else
                Himg._Rectang(true);

        }
        private void ImgView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Himg._ImgFill(e);
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

        private void toolStripFontShall_Click(object sender, EventArgs e)
        {
            Himg._Imagefontcolor(3);
        }

        private void toolStripGotoPage_Click(object sender, EventArgs e)
        {
            //FrmGoto Fgoto = new FrmGoto();
            //FrmGoto.Maxpage = MaxPage;
            //Fgoto.ShowDialog();
            //if (FrmGoto.Npage > 0) {
            //    Himg._Gotopage(FrmGoto.Npage);
            //}
        }

        private void toolStripAutoSide_Click(object sender, EventArgs e)
        {
            Himg._AutoCrop();
        }

        private void toolStripFiltr_Click(object sender, EventArgs e)
        {
            Himg._ImgLd();
        }

        private void toolStripCenter_Click(object sender, EventArgs e)
        {
            Himg._CenterImg();
        }


        private void toolStripUppage_Click(object sender, EventArgs e)
        {
            if (ImgView.Image != null && Clscheck.CrrentPage > 1) {
                Thread.Sleep(100);
                Himg._Pagenext(0);
                ucContents1.OnChangContents(Clscheck.CrrentPage);
            }
        }

        private void toolStripDownPage_Click(object sender, EventArgs e)
        {
            NextPage();
        }
        private void NextPage()
        {
            try {
                if (Clscheck.CrrentPage != Clscheck.MaxPage) {
                    Himg._SavePage();
                    Thread.Sleep(100);
                    Himg._Pagenext(1);
                    ucContents1.OnChangContents(Clscheck.CrrentPage);
                }
                else if (Clscheck.CrrentPage == Clscheck.MaxPage) {
                    Himg._SavePage();
                    Thread.Sleep(100);
                    toolStripSave_Click(null, null);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;
            if (Clscheck.MaxPage != Clscheck.RegPage) {
                MessageBox.Show("登记页码和图像页码不一致无法完成质检!");
                return;
            }
            if (MessageBox.Show("质检完成您确定要上传档案吗？", "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK) {
                string filepath = Clscheck.ScanFilePath;
                string filename = Clscheck.FileNametmp;
                int arid = Clscheck.Archid;
                int pages = Clscheck.MaxPage;
                Cledata();
                Task.Run(new Action(() => { FtpUpFinish(filepath, arid, filename, pages); }));
                gArch.LvData.Focus();
            }
        }

        private void toolStripClose_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;
            int arid = Clscheck.Archid;
            string filename = Clscheck.FileNametmp;
            string filepath = Clscheck.ScanFilePath;
            Cledata();
            try {
                if (T_ConFigure.FtpStyle == 1)
                    Imgclose(arid, filename);
                else {
                    if (File.Exists(filepath)) {
                        File.Delete(filepath);
                        Directory.Delete(Path.GetDirectoryName(filepath));
                    }
                }
            } catch {
            }
            gArch.LvData.Focus();
        }

        void Imgclose(int arid, string filename)
        {
            Task.Run(new Action(() =>
            {
                string goalfile = "";
                string path = "";
                string sourefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave,
                    filename.Substring(0, 8), filename);
                if (archzt == 1) {
                    goalfile = Path.Combine(T_ConFigure.FtpArchIndex, filename.Substring(0, 8),
                        filename);
                    path = Path.Combine(T_ConFigure.FtpArchIndex, filename.Substring(0, 8));
                }
                else {
                    goalfile = Path.Combine(T_ConFigure.FtpArchSave, filename.Substring(0, 8),
                        filename);
                    path = Path.Combine(T_ConFigure.FtpArchSave, filename.Substring(0, 8));
                }
                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                    if (archzt == 1)
                        Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.排序完);
                    else
                        Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.质检完);
                }
            }));
        }

        private void toolStripBigPage_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(1);
        }

        private void toolStripSamllPage_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(0);
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

        private void FrmIndex_KeyDown(object sender, KeyEventArgs e)
        {
            pub.KeyShortDown(e, Clscheck.lsinival, Clscheck.Lsinikeys, Clscheck.lssqlOpernum, Clscheck.lsSqlOper, out Clscheck.keystr);
            if (Clscheck.keystr.Trim().Length > 0)
                KeysDownEve(Clscheck.keystr.Trim());
            Keys keyCode = e.KeyCode;
            if (e.KeyCode == Keys.Escape)
                gArch.LvData.Focus();
        }
        private void toolStripRepair_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;
            if (MessageBox.Show("您确定要返工本卷档案吗？", "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK) {
                string filetmp = Clscheck.FileNametmp;
                int archid = Clscheck.Archid;
                string archpos = Clscheck.ArchPos;
                Cledata();
                Task.Run(new Action(() =>
                {
                    Repair(filetmp, archid, archpos);
                }));
                gArch.LvData.Focus();
            }
        }

        #endregion

        #region Method


        void Getsqlkey()
        {
            Task.Run(() =>
            {
                Clscheck.lsSqlOper.Clear();
                Clscheck.lssqlOpernum.Clear();
                Clscheck.lsinival.Clear();
                Clscheck.Lsinikeys.Clear();
                DataTable dt = Common.GetSqlkey(this.Text);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string key = dt.Rows[i][0].ToString();
                    string val = dt.Rows[i][1].ToString();
                    Clscheck.lsSqlOper.Add(key);
                    Clscheck.lssqlOpernum.Add(val);
                }
                Writeini.GetAllKeyValues(this.Text, out Clscheck.Lsinikeys, out Clscheck.lsinival);
            });
        }


        void KeysDownEve(string key)
        {
            bool bl = false;
            foreach (var item in toolstripmain1.Items) {
                if (item is ToolStripButton) {
                    ToolStripButton t = (ToolStripButton)item;
                    if (t.Text == key) {
                        t.PerformClick();
                        bl = true;
                    }
                }
            }

            if (!bl) {
                foreach (var item in toolstripmain2.Items) {
                    if (item is ToolStripButton) {
                        ToolStripButton t = (ToolStripButton)item;
                        if (t.Text == key) {
                            t.PerformClick();
                            bl = true;
                        }
                    }
                }
            }
        }
        private void Ispages()
        {
            if (Clscheck.RegPage != Clscheck.MaxPage) {
                toollbInfo.Text = "当前登记页码和图像页码不一致!";
                toollbInfo.BackColor = Color.Red;
            }
            else
                toollbInfo.Text = "";
        }

        private void LoadContents()
        {
            ucContents1.LoadContents(Clscheck.Archid, Clscheck.RegPage);
        }

        private void GetPages(int page, int counpage)
        {
            Clscheck.MaxPage = counpage;
            Clscheck.CrrentPage = page;
            labPageCrrent.Text = string.Format("第  {0}   页", page);
            labPageCount.Text = string.Format("共  {0}   页", counpage);
        }

        private void LoadArch()
        {
            int stattmp = 0;
            if (Common.Gettask(Clscheck.Archid) > 0) {
                MessageBox.Show("正在任务中请稍后!");
                Cledata();
                return;
            }
            int stat = Common.GetArchWorkState(Clscheck.Archid);
            if (stat < (int)T_ConFigure.ArchStat.排序完) {
                MessageBox.Show("此卷未排序,不能进入质检！");
                Cledata();
                return;
            }
            if (stat == (int)T_ConFigure.ArchStat.排序完)
                stattmp = 1;
            else if (stat == (int)T_ConFigure.ArchStat.质检完)
                stattmp = 2;
            if (stat == (int)T_ConFigure.ArchStat.质检中) {
                if (MessageBox.Show("您确认要强行进入质检吗?", "强行进入质检", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK) {
                    Cledata();
                    return;
                }
            }
            LoadImgShow(1, stattmp);
        }

        private async void LoadImgShow(int pages, int stattmp)
        {
            try {
                bool loadfile = await LoadFile(stattmp);
                gArch.butLoad.Enabled = true;
                if (loadfile == true) {
                    if (!File.Exists(Clscheck.ScanFilePath)) {
                        Cledata();
                        return;
                    }
                    Himg.Filename = Clscheck.ScanFilePath;
                    Himg.LoadPage(pages);
                    LoadContents();
                    Getuser();
                    Ispages();
                    return;
                }
                MessageBox.Show("文件加载失败或不存在!");
                Cledata();
            } catch {
                Cledata();
            }

        }

        private Task<bool> LoadFile(int stsa)
        {
            return Task.Run(() =>
            {
                try {
                    archzt = stsa;
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourefile = "";
                        string goalfile = "";
                        string path = "";
                        string localPath = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpSave, Clscheck.FileNametmp.Substring(0, 8));
                        string localScanFile = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpSave, Clscheck.FileNametmp.Substring(0, 8),
                              Clscheck.FileNametmp);
                        Clscheck.ScanFilePath = localScanFile;
                        if (!Directory.Exists(localPath)) {
                            Directory.CreateDirectory(localPath);
                        }
                        if (File.Exists(localScanFile)) {
                            File.Delete(localScanFile);
                        }
                        sourefile = Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                        goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                        path = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave, Clscheck.FileNametmp.Substring(0, 8));
                        if (stsa == 1) {
                            sourefile = Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    return true;
                                }
                            }
                        }
                        else if (stsa == 2) {
                            sourefile = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    return true;
                                }
                            }
                        }
                        else {
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    archzt = 1;
                                    return true;
                                }
                            }
                            sourefile = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    archzt = 2;
                                    return true;
                                }
                            }
                        }
                    }
                    else {
                        string localPath = Path.Combine(T_ConFigure.LocalTempPath, Clscheck.FileNametmp.Substring(0, 8));
                        string localScanFile = Path.Combine(T_ConFigure.LocalTempPath, Clscheck.FileNametmp.Substring(0, 8),
                            Clscheck.FileNametmp);
                        Clscheck.ScanFilePath = localScanFile;
                        if (!Directory.Exists(localPath)) {
                            Directory.CreateDirectory(localPath);
                        }
                        if (File.Exists(localScanFile)) {
                            File.Delete(localScanFile);
                        }
                        if (stsa == 1) {
                            if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp))) {
                                if (ftp.DownLoadFile(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8),
                                    localScanFile,
                                    Clscheck.FileNametmp)) {
                                    return true;
                                }
                                return false;
                            }
                        }
                        else if (stsa == 2) {
                            if (MessageBox.Show("已质检完成是否重新质检？", "警告", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                                if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8),
                                    Clscheck.FileNametmp))) {
                                    if (ftp.DownLoadFile(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8),
                                        localScanFile,
                                        Clscheck.FileNametmp)) {
                                        return true;
                                    }

                                }
                            }
                        }
                        else {
                            if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8),
                                Clscheck.FileNametmp))) {
                                if (ftp.DownLoadFile(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8),
                                    localScanFile,
                                    Clscheck.FileNametmp)) {
                                    archzt = 1;
                                    return true;
                                }
                            }
                            else if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8),
                                 Clscheck.FileNametmp))) {
                                if (ftp.DownLoadFile(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8),
                                    localScanFile,
                                    Clscheck.FileNametmp)) {
                                    archzt = 2;
                                    return true;
                                }
                            }
                        }
                    }
                    Cledata();
                    return false;

                } catch (Exception e) {
                    Cledata();
                    MessageBox.Show("加载文件失败!" + e.ToString());
                    if (stsa == 2)
                        Common.SetArchWorkState(Clscheck.Archid, (int)T_ConFigure.ArchStat.质检完);
                    else
                        Common.SetArchWorkState(Clscheck.Archid, (int)T_ConFigure.ArchStat.排序完);
                    return false;
                }
            });
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
                DataTable dt = Common.GetOperator(Clscheck.Archid);
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
            this.Invoke(new Action(() =>
            {
                ImgView.Image = null;
                Clscheck.Archid = 0;
                Clscheck.ArchPos = "";
                Clscheck.RegPage = 0;
                Clscheck.FileNametmp = "";
                toolArchno.Text = "当前卷号:";
                labPageCrrent.Text = "第     页";
                labPageCount.Text = "共      页";
                Clscheck.ScanFilePath = "";
                Clscheck.task = false;
            }));

        }

        private async void FtpUpFinish(string filetmp, int arid, string filename, int pages)
        {
            try {
                if (File.Exists(filetmp)) {
                    Common.WiteUpTask(arid, "", filename, (int)T_ConFigure.ArchStat.质检完, pages, filetmp);
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave, filename.Substring(0, 8), filename);
                        string goalfile = Path.Combine(T_ConFigure.FtpArchSave, filename.Substring(0, 8), filename);
                        string path = Path.Combine(T_ConFigure.FtpArchSave, filename.Substring(0, 8));
                        if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                            Common.DelTask(arid);
                            Common.SetCheckFinish(arid, DESEncrypt.DesEncrypt(filename), 1, (int)T_ConFigure.ArchStat.质检完, "");
                            return;
                        }
                    }
                    else {
                        string RemoteDir = filename.Substring(0, 8);
                        string localPath = Path.Combine(T_ConFigure.LocalTempPath, filename.Substring(0, 8));
                        string newfile = Path.Combine(T_ConFigure.FtpArchSave, RemoteDir, filename);
                        string newpath = Path.Combine(T_ConFigure.FtpArchSave, RemoteDir);
                        bool x = await ftp.FtpUpFile(filetmp, newfile, newpath);
                        if (x) {
                            Common.DelTask(arid);
                            Common.SetCheckFinish(arid, DESEncrypt.DesEncrypt(filename), 1, (int)T_ConFigure.ArchStat.质检完, "");
                            try {
                                File.Delete(filetmp);
                                Directory.Delete(localPath);
                            } catch { }
                            return;
                        }
                    }
                    if (archzt == 1)
                        Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.排序完);
                    else
                        Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.质检完);
                }
            } catch {
                if (archzt == 1)
                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.排序完);
                else
                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.质检完);
            } finally {
                GC.Collect();
            }
        }

        private void Repair(string filetmp, int arid, string archpos)
        {
            try {
                Common.Writelog(Clscheck.Archid, "质检返工!");
                string PageIndexInfo = "";
                for (int i = 1; i <= Clscheck.MaxPage; i++) {
                    if (PageIndexInfo.Trim().Length <= 0)
                        PageIndexInfo += i.ToString();
                    else PageIndexInfo += ";" + i.ToString();
                }
                PageIndexInfo = PageIndexInfo.Trim();
                string sourefile = "";
                if (T_ConFigure.FtpStyle == 2) {
                    if (archzt == 1)
                        sourefile = Path.Combine(T_ConFigure.FtpArchIndex, filetmp.Substring(0, 8), filetmp);
                    else
                        sourefile = Path.Combine(T_ConFigure.FtpArchSave, filetmp.Substring(0, 8), filetmp);
                }
                else
                    sourefile = Path.Combine(T_ConFigure.FtpFwqPath,T_ConFigure.TmpSave, filetmp.Substring(0, 8), filetmp);
                string goalfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                string path = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                    Common.Writelog(Clscheck.Archid, "质检退!");
                    Common.SetCheckFinish(arid, "", 2, (int)T_ConFigure.ArchStat.扫描完, PageIndexInfo);
                }
                if (T_ConFigure.FtpStyle != 1) {
                    try {
                        File.Delete(filetmp);
                    } catch { }
                }
            } catch {
                Common.Writelog(Clscheck.Archid, "质检退回失败!");
            }
        }

        #endregion


    }
}
