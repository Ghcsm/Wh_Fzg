using CsmCon;
using DAL;
using HLFtp;
using HLjscom;
using System;
using System.Data;
using System.IO;
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
            Init();
        }

        Hljsimage Himg = new Hljsimage();
        HFTP ftp = new HFTP();
        gArchSelect gArch;
        UcContents ucContents1;
        private int archzt = 0;
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

        private void UcContents1_OneClickGotoPage(object sender, EventArgs e)
        {
            int p = 0;
            try {
                p = Convert.ToInt32(UcContents.PageMl);
            } catch { }
            if (p > 0 && p<Clscheck.MaxPage)
                Himg._Gotopage(p);

        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            try {
                Himg._Instimagtwain(this.ImgView, this.Handle, 0);
                Himg._Rectang(true);
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
            try {
                Cledata();
                if (File.Exists(Clscheck.ScanFilePath)) {
                    string path = Clscheck.ScanFilePath.Substring(0, 8);
                    File.Delete(Clscheck.ScanFilePath);
                    Directory.Delete(path);
                }
            } catch {
            }
            gArch.LvData.Focus();
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

        private void ImgView_KeyDown(object sender, KeyEventArgs e)
        {
            Keykuaij(sender, e);
            Keys keyCode = e.KeyCode;
            if (e.KeyCode == Keys.Escape)
                gArch.LvData.Focus();
        }

        private void FrmIndex_KeyDown(object sender, KeyEventArgs e)
        {
            Keykuaij(sender, e);
            Keys keyCode = e.KeyCode;
            if (e.KeyCode == Keys.Escape)
                gArch.LvData.Focus();
        }
        private void toolStripRepair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要返工本卷档案吗？", "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK) {
                string filetmp = Clscheck.ScanFilePath;
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

        private void Ispages()
        {
            if (Clscheck.RegPage != Clscheck.MaxPage)
                toollbInfo.Text = "当前登记页码和图像页码不一致!";
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
        }

        private Task<bool> LoadFile(int stsa)
        {
            return Task.Run(() =>
            {
                try {
                    archzt = stsa;
                    if (T_ConFigure.FtpStyle == 1) {
                        string localPath = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8));
                        string localScanFile = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8),
                            Clscheck.FileNametmp);
                        Clscheck.ScanFilePath = localScanFile;
                        if (!Directory.Exists(localPath)) {
                            Directory.CreateDirectory(localPath);
                        }
                        if (File.Exists(localScanFile)) {
                            File.Delete(localScanFile);
                        }
                        string sourefile = Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                        string goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                        string path = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8));
                        if (stsa == 1) {
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    return true;
                                }
                            }
                        }
                        else if (stsa == 2) {
                            sourefile = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                            goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
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
                            goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
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
                    return false;

                } catch (Exception e) {
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
            ImgView.Image = null;
            Clscheck.Archid = 0;
            Clscheck.ArchPos = "";
            labPageCrrent.Text = "第     页";
            labPageCount.Text = "共      页";
            labScanUser.Text = "扫描:";
            labIndexUser.Text = "排序:";
            labCheckUser.Text = "质检:";
            toolArchno.Text = "当前卷号:";
        }

        private async void FtpUpFinish(string filetmp, int arid, string filename, int pages)
        {
            try {
                if (File.Exists(filetmp)) {
                    Common.WiteUpTask(arid, "", filename, (int)T_ConFigure.ArchStat.质检完, pages, filetmp);
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                        string goalfile = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                        string path = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8));
                        if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                            Common.DelTask(arid);
                            Common.SetCheckFinish(arid, filename, 1, (int)T_ConFigure.ArchStat.质检完, "", "");
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
                            Common.SetCheckFinish(arid, filename, 1, (int)T_ConFigure.ArchStat.质检完, "", "");
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
                string RadpageIndexinfo = "";
                for (int page = RadpageIndexinfo.Length + 1; page <= Clscheck.MaxPage; page++) {
                    string page2 = (page - RadpageIndexinfo.Length).ToString();
                    PageIndexInfo += page + ":" + page2 + " ";
                }
                PageIndexInfo = PageIndexInfo.Trim();
                string sourefile = "";
                if (archzt == 1)
                    sourefile = Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                else
                    sourefile = Path.Combine(T_ConFigure.FtpArchIndex, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                string goalfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                string path = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                    Common.Writelog(Clscheck.Archid, "质检退!");
                    Common.SetCheckFinish(arid, "", 2, (int)T_ConFigure.ArchStat.扫描完, PageIndexInfo, "");
                }
                if (T_ConFigure.FtpStyle != 1) {
                    try {
                        File.Delete(filetmp);
                    } catch { }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                Common.Writelog(Clscheck.Archid, "质检退回失败!");
            }
        }


        private void Keykuaij(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            switch (keyCode) {
                case Keys.F2:
                    toolStripRepair_Click(sender, e);
                    break;
                case Keys.Escape:
                    toolStripClose_Click(sender, e);
                    break;
                case Keys.Enter:
                    toolStripDownPage_Click(sender, e);
                    break;
                case Keys.Space:
                    toolStripDownPage_Click(sender, e);
                    break;
                case Keys.PageDown:
                    toolStripDownPage_Click(sender, e);
                    break;
                case Keys.PageUp:
                    toolStripUppage_Click(sender, e);
                    break;
                case Keys.E:
                    toolStripRoteImg_Click(sender, e);
                    break;
                case Keys.D:
                    toolStripDeskew_Click(sender, e);
                    break;
                case Keys.F:
                    toolStripCleSide_Click(sender, e);
                    break;
                case Keys.C:
                    toolStripColorShall_Click(sender, e);
                    break;
                case Keys.G:
                    toolStripInterSpeck_Click(sender, e);
                    break;
                case Keys.T:
                    toolStripOutSpeck_Click(sender, e);
                    break;
                case Keys.V:
                    toolStripCenter_Click(sender, e);
                    break;
                case Keys.A:
                    toolStripBigPage_Click(sender, e);
                    break;
                case Keys.S:
                    toolStripSamllPage_Click(sender, e);
                    break;
                case Keys.Z:
                    Himg._RoteimgWt(ImgView, 0);
                    break;
                case Keys.X:
                    Himg._RoteimgWt(ImgView, 1);
                    break;
            }

        }




        #endregion


    }
}
