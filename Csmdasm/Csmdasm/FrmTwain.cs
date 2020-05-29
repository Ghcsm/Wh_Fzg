using CsmCon;
using CsmImg;
using DAL;
using HLFtp;
using HLjscom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmdasm
{
    public partial class FrmTwain : Form
    {
        public FrmTwain()
        {
            InitializeComponent();
        }

        #region InitCs

        private gArchSelect gArch;
        Hljsimage Himg = new Hljsimage();
        HFTP ftp = new HFTP();
        private Pubcls pub;
        private int pages = 0;
        private int maxpage = 0;
        private List<int> UserAll = new List<int>();
        private List<int> Workint = new List<int>();

        #endregion

        #region Init

        private void Init()
        {
            try {
                gArch = new gArchSelect();
                gArch.GotoPages = true;
                gArch.LoadFileBoole = true;
                gArch.Dock = DockStyle.Fill;
                gArch.LineLoadFile += Garch_LineLoadFile;
                gr1.Controls.Add(gArch);
            } catch { }
        }

        private void Garch_LineLoadFile(object sender, EventArgs e)
        {

            try {
                if (ImgView.Image == null && ClsTwain.ArchPos == null ||
                    ImgView.Image == null && ClsTwain.ArchPos.Trim().Length <= 0) {
                    ClsTwain.ArchPos = gArch.ArchPos;
                    ClsTwain.Archid = gArch.Archid;
                    ClsTwain.RegPage = gArch.ArchRegPages;
                    labArchNo.Text = string.Format("当前卷号:{0}", ClsTwain.ArchPos);
                    LoadArch();
                    ImgView.Focus();
                    return;
                }
                MessageBox.Show("请退出当前卷再进行操作！");
                gArch.Focus();
            } catch (Exception ex) {
                Cledata();
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmTwain_Load(object sender, EventArgs e)
        {
            try {
                Init();
                Himg._Instimagtwain(this.ImgView, this.Handle, 1);
                Himg._Rectang(true);
                Himg.Userid = T_User.UserId;
                Writeini.Fileini = Path.Combine(Application.StartupPath, "Csmkeyval.ini");
                Getsqlkey();
                pub = new Pubcls();
            } catch (Exception ex) {
                MessageBox.Show("初始化失败请重新加载" + ex.ToString());
                Himg.Dispose();
            }
        }

        #endregion

        #region LoadcsandImg

        private async void LoadArch()
        {
            if (ClsTwain.task)
                return;
            ClsTwain.task = true;
            gArch.butLoad.Enabled = false;
            if (Common.Gettask(ClsTwain.Archid) > 0) {
                MessageBox.Show("正在任务中请稍后!");
                Cledata();
                return;
            }
            int stat = Common.GetArchWorkState(ClsTwain.Archid);
            if (stat >= (int)T_ConFigure.ArchStat.排序完) {
                MessageBox.Show("此卷已排序完成,不能再进入排序！");
                Cledata();
                return;
            }
            if (stat == (int)T_ConFigure.ArchStat.扫描中) {
                if (MessageBox.Show("您确认要强行进入扫描吗?", "强行进入扫描", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK) {
                    Cledata();
                    return;
                }
            }
            Himg.UserScanPage = 0;
            Common.WriteArchlog(ClsTwain.Archid, "进入案卷");
            bool loadfile = await LoadFile();
            Himg.Filename = ClsTwain.ScanFileTmp;
            gArch.butLoad.Enabled = true;
            if (loadfile == true) {
                int pages = 1;
                if (!File.Exists(ClsTwain.ScanFileTmp)) {
                    Cledata();
                    return;
                }
                Himg.LoadPage(pages);
            }
            Getuser();
            GetQspage();
            GetScanPage();
        }

        private Task<bool> LoadFile()
        {
            return Task.Run(() =>
            {
                try {
                    ClsTwain.task = true;
                    Common.SetArchWorkState(ClsTwain.Archid, (int)T_ConFigure.ArchStat.扫描中);
                    if (T_ConFigure.FtpStyle == 1) {
                        string localPath = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpScan, ClsTwain.ArchPos);
                        string localScanFile =
                            Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpScan, ClsTwain.ArchPos, T_ConFigure.ScanTempFile);
                        ClsTwain.ScanFileTmp = localScanFile;
                        if (!Directory.Exists(localPath)) {
                            Directory.CreateDirectory(localPath);
                        }
                        if (File.Exists(localScanFile)) {
                            File.Delete(localScanFile);
                        }
                        if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.gArchScanPath, ClsTwain.ArchPos, T_ConFigure.ScanTempFile))) {
                            string sourcefile = Path.Combine(T_ConFigure.gArchScanPath, ClsTwain.ArchPos, T_ConFigure.ScanTempFile);
                            string goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpScan, ClsTwain.ArchPos, T_ConFigure.ScanTempFile);
                            string path = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpScan, ClsTwain.ArchPos);
                            if (ftp.FtpMoveFile(sourcefile, goalfile, path))
                                return (FileMoveBool(localScanFile));
                        }
                    }
                    else {
                        string localPath = Path.Combine(T_ConFigure.LocalTempPath, ClsTwain.ArchPos);
                        string localScanFile =
                            Path.Combine(T_ConFigure.LocalTempPath, ClsTwain.ArchPos, T_ConFigure.ScanTempFile);
                        ClsTwain.ScanFileTmp = localScanFile;
                        if (!Directory.Exists(localPath)) {
                            Directory.CreateDirectory(localPath);
                        }
                        if (File.Exists(localScanFile)) {
                            File.Delete(localScanFile);
                        }
                        if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.gArchScanPath, ClsTwain.ArchPos, T_ConFigure.ScanTempFile))) {
                            if (ftp.DownLoadFile(T_ConFigure.gArchScanPath, ClsTwain.ArchPos, localScanFile,
                                T_ConFigure.ScanTempFile))
                                return true;
                        }

                    }
                    ClsTwain.task = false;
                    return false;
                } catch (Exception e) {
                    MessageBox.Show("加载文件失败!" + e.ToString());
                    Common.SetArchWorkState(ClsTwain.Archid, (int)T_ConFigure.ArchStat.无);
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
                    if (id > 10)
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
                DataTable dt = Common.GetOperator(ClsTwain.Archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                DataRow dr = dt.Rows[0];
                // Scanner = dr["扫描"].ToString();
                //scantime = dr["扫描时间"].ToString();
                Indexer = dr["排序"].ToString();
                indextime = dr["排序时间"].ToString();
                Checker = dr["质检"].ToString();
                chktime = dr["质检时间"].ToString();
                enter = dr["录入"].ToString();
                entertime = dr["录入时间"].ToString();
                foreach (DataRow d in dt.Rows) {
                    string s = d["扫描"].ToString();
                    if (!Scanner.Contains(s)) {
                        Scanner += s + ",";
                        scantime += d["扫描时间"].ToString() + ",";
                    }
                }
                this.BeginInvoke(new Action(() =>
                {
                    this.labScanUser.Text = string.Format("扫描：{0}", Scanner);
                    this.labIndexUser.Text = string.Format("排序：{0}", Indexer);
                    this.labCheckUser.Text = string.Format("质检：{0}", Checker);

                    toollabscan.Text = string.Format("扫描:{0}", Scanner);
                    toollabscantime.Text = string.Format("时间:{0}", scantime);
                    toollabIndex.Text = string.Format("排序:{0}", Indexer);
                    toollabindextime.Text = string.Format("时间:{0}", indextime);
                    toollabcheck.Text = string.Format("质检:{0}", Checker);
                    toollabchecktime.Text = string.Format("时间:{0}", chktime);
                    toollabenter.Text = string.Format("录入:{0}", enter);
                    toollabentertime.Text = string.Format("时间:{0}", entertime);

                }));
                dt.Dispose();
            }));
        }

        private void GetQspage()
        {
            Task.Run(() =>
            {
                List<string> LisPage = new List<string>();
                string Qspages = "";
                DataTable dt = Common.ReadPageIndexInfo(ClsTwain.Archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                string PageIndexInfo = dt.Rows[0][0].ToString();
                string pageg = dt.Rows[0][2].ToString();
                if (string.IsNullOrEmpty(PageIndexInfo))
                    return;
                string[] arrPage = PageIndexInfo.Split(';');
                if (arrPage == null || arrPage.Length <= 0)
                    return;
                foreach (string i in arrPage) {
                    string[] s = i.Split(':');
                    LisPage.Add(s[1].ToString());
                }
                string[] pagegl = pageg.Split(';');
                foreach (string i in pagegl) {
                    if (LisPage.IndexOf(i) < 0) {
                        if (Qspages.Trim().Length <= 0)
                            Qspages += i.ToString();
                        else {
                            Qspages += "," + i.ToString();
                        }
                    }
                }
                for (int i = 1; i < ClsTwain.RegPage - pagegl.Length; i++) {
                    if (LisPage.IndexOf(i.ToString()) < 0) {
                        if (Qspages.Trim().Length <= 0)
                            Qspages += i.ToString();
                        else {
                            Qspages += "," + i.ToString();
                        }
                    }
                }
                this.BeginInvoke(new Action(() => { labQsPages.Text = "当前卷缺少： " + Qspages + " 页"; }));
            });

        }

        private void GetScanPage()
        {
            UserAll.Clear();
            Workint.Clear();
            Task.Run(() =>
            {
                try {
                    DataTable dt = Common.ReadScanPage(ClsTwain.Archid);
                    if (dt == null || dt.Rows.Count <= 0)
                        return;
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        string u = dt.Rows[i][0].ToString();
                        string w = dt.Rows[i][1].ToString();
                        int w1;
                        bool bl = int.TryParse(w, out w1);
                        if (!bl)
                            continue;
                        UserAll.Add(Convert.ToInt32(u));
                        Workint.Add(Convert.ToInt32(w));
                    }
                } catch {

                }
            });
        }

        private void GetPages(int page, int counpage)
        {
            ClsTwain.MaxPage = counpage;
            if (page > counpage)
                page = counpage;
            //if (maxpage == 0)
            //    maxpage = counpage;
            //else if (counpage > maxpage)
            //    pages += 1;
            labPagesCrrent.Text = string.Format("第  {0}   页", page);
            labPagesCount.Text = string.Format("共  {0}   页", counpage);
            GetImgInfo();
        }

        private void GetScanCs()
        {
            try {
                ClsWriteIni.Getscan();
                if (IniInfo.PageDodule.Length > 0)
                    chkDoublePages.Checked = Convert.ToBoolean(IniInfo.PageDodule);
                if (IniInfo.PageSize.Length > 0)
                    comPagesSize.SelectedIndex = Convert.ToInt32(IniInfo.PageSize);
                else
                    comPagesSize.SelectedIndex = 1;
                if (IniInfo.ImgDirection.Length > 0) {
                    if (IniInfo.ImgDirection == "0")
                        rdVerPages.Checked = true;
                    else
                        rdHorpages.Checked = true;
                }
                else
                    rdVerPages.Checked = true;
                if (IniInfo.PageColor.Length > 0) {
                    if (IniInfo.PageColor == "黑白")
                        rdColorWithe.Checked = true;
                    else if (IniInfo.PageColor == "灰度")
                        rdColorGray.Checked = true;
                    else
                        rdColorRed.Checked = true;
                }
                else {
                    rdColorRed.Checked = true;
                }
                if (IniInfo.FeedModule.Length > 0) {
                    if (IniInfo.FeedModule == "1")
                        rdFeedFlat.Checked = true;
                    else
                        rdFeedAuto.Checked = true;
                }
                else {
                    rdFeedAuto.Checked = true;
                }
                if (IniInfo.PageDpi.Length > 0)
                    comBoxDpi.Text = IniInfo.PageDpi;
                else
                    comBoxDpi.SelectedIndex = 0;
                if (IniInfo.ScanModule.Length > 0)
                    comBoxScanMode.SelectedIndex = Convert.ToInt32(IniInfo.ScanModule);
                else
                    comBoxScanMode.SelectedIndex = 0;
            } catch { }
        }

        private void GetImgInfo()
        {
            try {
                labDpi.Text = string.Format("分辨率:{0}", ImgView.Image.XResolution.ToString());
                labPagesSize.Text = string.Format("幅面:{0}", ImgView.Image.ImageSize.ToString());
                if (ImgView.Image.BitsPerPixel == 1) {
                    labColor.Text = string.Format("格式:{0}", "黑白");
                }
                else if (ImgView.Image.BitsPerPixel == 8) {
                    labColor.Text = string.Format("格式:{0}", "灰度");
                }
                else {
                    labColor.Text = string.Format("格式:{0}", "彩色");
                }

            } catch { }
        }


        private void Cledata()
        {
            this.BeginInvoke(new Action(() =>
            {
                Himg.Filename = "";
                Himg.RegPage = 0;
                ImgView.Image = null;
                ClsTwain.Archid = 0;
                ClsTwain.ArchPos = "";
                ClsTwain.MaxPage = 0;
                ClsTwain.RegPage = 0;
                Himg.SetpageZero();
                labPagesCrrent.Text = "第     页";
                labPagesCount.Text = "共      页";
                labScanUser.Text = "扫描:";
                labIndexUser.Text = "排序:";
                labCheckUser.Text = "质检:";
                labArchNo.Text = "当前卷号:";
                labQsPages.Text = "当前卷缺少:";
                ClsTwain.task = false;
                Himg.UserScanPage = 0;
                gArch.butLoad.Enabled = true;
            }));
        }

        private void FtpUp(string filetmp, string archpos, int maxpage, int arid, int regpage, bool bl)
        {
            try {
                Common.WriteArchlog(arid, "退出案卷");
                if (File.Exists(filetmp)) {
                    Common.WiteUpTask(arid, archpos, T_ConFigure.ScanTempFile, (int)T_ConFigure.ArchStat.扫描完, maxpage, filetmp, "0");
                    if (bl) {
                        List<string> lsfile = new List<string>();
                        Himg._SplitImgScan(filetmp, out lsfile);
                        ClsImg.CleHole(lsfile);
                        string f = Himg.MergeImg(lsfile).Trim();
                        if (f.Length > 0) {
                            File.Delete(filetmp);
                            filetmp = f;
                        }
                    }
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourcefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpScan, archpos, T_ConFigure.ScanTempFile);
                        string goalfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        string path = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                        if (ftp.FtpMoveFile(sourcefile, goalfile, path)) {
                            Thread.Sleep(5000);
                            if (maxpage >= regpage)
                                Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.扫描完);
                            else
                                Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.无);
                            Common.DelTask(arid);
                            try {
                                Directory.Delete(Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpScan, archpos));
                            } catch { }
                            return;
                        }
                    }
                    else {
                        if (ftp.SaveRemoteFileUp(T_ConFigure.gArchScanPath, archpos, filetmp, T_ConFigure.ScanTempFile)) {
                            if (maxpage >= regpage)
                                Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.扫描完);
                            else
                                Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.无);
                            Common.DelTask(Convert.ToInt32(arid));
                            try {
                                File.Delete(filetmp);
                                Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                            } catch {
                            }
                            return;
                        }
                        // 新传输模式发现图像有错位现象
                        // string newfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        //string newpath = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                        //bool x = await ftp.FtpUpFile(filetmp, newfile, newpath);
                        //if (x) {
                        //    Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.扫描完);
                        //    Common.DelTask(arid);
                        //    try {
                        //        File.Delete(filetmp);
                        //        Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                        //    } catch { }
                        //    return;
                        //}

                    }
                }
                else
                    Common.WriteArchlog(ClsTwain.Archid, "退出时未找到图像文件");
                Common.SetScanFinish(arid, maxpage, 0, (int)T_ConFigure.ArchStat.无);
            } catch {
                Common.SetScanFinish(arid, maxpage, 0, (int)T_ConFigure.ArchStat.无);
            } finally {
                GC.Collect();
            }
        }


        #endregion

        #region butEven

        private void ImgView_Click(object sender, EventArgs e)
        {
            Himg.Rectcls();
        }

        private void FrmTwain_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gArch.GetFocus()) {
                pub.KeyShortDown(e, ClsTwain.lsinival, ClsTwain.Lsinikeys, ClsTwain.lssqlOpernum, ClsTwain.lsSqlOper, out ClsTwain.keystr);
                if (ClsTwain.keystr.Trim().Length > 0)
                    KeysDownEve(ClsTwain.keystr.Trim());
            }
            Keys keyCode = e.KeyCode;
            if (!gArch.txtBoxsn.Focused) {
                if (e.KeyCode == Keys.NumPad3)
                    comPagesSize.SelectedIndex = 2;
                else if (e.KeyCode == Keys.NumPad4)
                    comPagesSize.SelectedIndex = 1;
                else if (e.KeyCode == Keys.NumPad0) {
                    if (chkDoublePages.Checked)
                        chkDoublePages.Checked = false;
                    else
                        chkDoublePages.Checked = true;
                }
            }

            if (e.KeyCode == Keys.Escape) {
                gArch.txtBoxsn.Focus();
                gArch.txtBoxsn.SelectAll();
            }
        }

        private void toolSelectTwain_Click(object sender, EventArgs e)
        {
            Himg._Twainscan(0);
        }

        private void toolShowPar_Click(object sender, EventArgs e)
        {
            Himg._Twainscan(2);
        }

        private void FrmTwain_Shown(object sender, EventArgs e)
        {
            Himg.Spage += new Hljsimage.ScanPage(GetPages);
            ftp.PercentChane += new HLFtp.HFTP.PChangedHandle(Downjd);
            GetScanCs();
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

        private void toolPagesUp_Click(object sender, EventArgs e)
        {
            Himg._Pagenext(0);
            ImgView.Focus();
        }

        private void toolPagesDown_Click(object sender, EventArgs e)
        {
            Himg._Pagenext(1);
            ImgView.Focus();
        }

        private void toolImgBig_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(1);
            ImgView.Focus();
        }

        private void toolImgSmall_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(0);
            ImgView.Focus();
        }

        private void toolImgRoter_Click(object sender, EventArgs e)
        {
            Himg._Roteimage(1);
            ImgView.Focus();
        }

        private void toolDelPages_Click(object sender, EventArgs e)
        {
            Himg._Delepage();
            if (ImgView.Image == null) {
                try {
                    if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.gArchScanPath, ClsTwain.ArchPos,
                        T_ConFigure.ScanTempFile))) {
                        ftp.FtpDelFile(Path.Combine(T_ConFigure.gArchScanPath, ClsTwain.ArchPos,
                            T_ConFigure.ScanTempFile));
                    }
                } catch { }
            }
            ImgView.Focus();
        }

        private void toolInster_Click(object sender, EventArgs e)
        {
            if (this.ImgView.Image != null) {
                if (this.oFdInsterFile.ShowDialog() == DialogResult.OK) {
                    Himg._Insterpage(this.oFdInsterFile.FileName);
                }
            }
            ImgView.Focus();
        }

        private void toolGotoPages_Click(object sender, EventArgs e)
        {
            Frmnum nu = new Frmnum();
            nu.ShowDialog();
            int page = Frmnum.Page;
            if (page > 0 && page <= ClsTwain.MaxPage) {
                Himg._Gotopage(Frmnum.Page);
            }
            ImgView.Focus();
        }

        private void toolScan_Click(object sender, EventArgs e)
        {
            if (ClsTwain.ArchPos == null || ClsTwain.ArchPos.Trim().Length <= 0) {
                MessageBox.Show("请先加载相关案卷！");
                return;
            }
            try {
                if (!ClsTwain.Scanbool) {
                    Twscan();
                }
            } catch (Exception ex) {
                MessageBox.Show("请检查是否安装驱动或连接扫描仪!" + ex.ToString());
            } finally {
                ClsTwain.Scanbool = false;
            }
            ImgView.Focus();
        }

        private void toolColse_Click(object sender, EventArgs e)
        {
            string filetmp = ClsTwain.ScanFileTmp;
            string archpos = ClsTwain.ArchPos;
            //int maxpage = ClsTwain.MaxPage;
            int maxpage = GetPages();
            int arid = ClsTwain.Archid;
            int regpage = ClsTwain.RegPage;
            bool blimg = chkImg.Checked;
            Cledata();
            Task.Run(new Action(() => { FtpUp(filetmp, archpos, maxpage, arid, regpage, blimg); }));
            gArch.LvData.Focus();
        }

        private int GetPages()
        {
            int id = UserAll.IndexOf(T_User.UserId);
            if (id < 0)
                return Himg.UserScanPage;
            else {
                int c = Workint[id];
                c = c + Himg.UserScanPage;
                return c;
            }
        }

        #endregion

        #region CsSet

        private void chkDoublePages_Click(object sender, EventArgs e)
        {
            IniInfo.PageDodule = chkDoublePages.Checked.ToString();
            ClsWriteIni.WriteScan();
        }

        private void rdVerPages_Click(object sender, EventArgs e)
        {
            IniInfo.ImgDirection = "0";
            ClsWriteIni.WriteScan();
        }

        private void rdHorpages_Click(object sender, EventArgs e)
        {
            IniInfo.ImgDirection = "1";
            ClsWriteIni.WriteScan();
        }

        private void comPagesSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniInfo.PageSize = comPagesSize.SelectedIndex.ToString();
            ClsWriteIni.WriteScan();
        }

        private void rdColorRed_Click(object sender, EventArgs e)
        {
            IniInfo.PageColor = "彩色";
            ClsWriteIni.WriteScan();
        }

        private void rdColorGray_Click(object sender, EventArgs e)
        {
            IniInfo.PageColor = "灰度";
            ClsWriteIni.WriteScan();
        }

        private void rdColorWithe_Click(object sender, EventArgs e)
        {
            IniInfo.PageColor = "黑白";
            ClsWriteIni.WriteScan();
        }

        private void rdFeedAuto_Click(object sender, EventArgs e)
        {
            IniInfo.FeedModule = "0";
            ClsWriteIni.WriteScan();
        }

        private void rdFeedFlat_Click(object sender, EventArgs e)
        {
            IniInfo.FeedModule = "1";
            ClsWriteIni.WriteScan();
        }

        private void comBoxDpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniInfo.PageDpi = comBoxDpi.Text.Trim();
            ClsWriteIni.WriteScan();
        }

        private void comBoxScanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniInfo.ScanModule = comBoxScanMode.SelectedIndex.ToString();
            ClsWriteIni.WriteScan();
        }


        #endregion

        #region MyRegion

        private void Twscan()
        {
            ClsTwain.Scanbool = true;
            Himg.Scanms = comBoxScanMode.SelectedIndex;
            Himg._Duplexpage(chkDoublePages.Checked);
            if (comPagesSize.SelectedIndex == 1) {
                if (rdVerPages.Checked) {
                    Himg._SetimgFx(1, 0);
                }
                else {
                    Himg._SetimgFx(0, 0);
                }

            }
            else if (comPagesSize.SelectedIndex == 2) {
                Himg._Setpagesize(2);
            }
            else if (comPagesSize.SelectedIndex == 0) {
                if (rdVerPages.Checked) {
                    Himg._SetimgFx(2, 0);
                }
                else {
                    Himg._SetimgFx(3, 0);
                }
            }
            if (rdColorRed.Checked) {
                Himg._Setcolor(24);
            }
            else if (rdColorGray.Checked) {
                Himg._Setcolor(8);
            }
            else {
                Himg._Setcolor(24);
            }
            Himg._Setdpi(Convert.ToInt32(comBoxDpi.Text.Trim()));
            Himg._Twainscan(1);
        }


        void Getsqlkey()
        {
            Task.Run(() =>
            {
                ClsTwain.lsSqlOper.Clear();
                ClsTwain.lssqlOpernum.Clear();
                ClsTwain.lsinival.Clear();
                ClsTwain.Lsinikeys.Clear();
                DataTable dt = Common.GetSqlkey(this.Text);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string key = dt.Rows[i][0].ToString();
                    string val = dt.Rows[i][1].ToString();
                    ClsTwain.lsSqlOper.Add(key);
                    ClsTwain.lssqlOpernum.Add(val);
                }
                dt = Common.GetOpenkey(this.Text);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string strid = dt.Rows[i][0].ToString();
                    string strkey = dt.Rows[i][2].ToString();
                    string strnum = dt.Rows[i][3].ToString();
                    if (strid.Trim().Length > 0 && strkey.Trim().Length > 0 && strnum.Trim().Length > 0) {
                        ClsTwain.Lsinikeys.Add(strkey);
                        ClsTwain.lsinival.Add(strnum);
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
                foreach (var item in toolStrip1.Items) {
                    if (item is ToolStripButton) {
                        t = (ToolStripButton)item;
                        if (t.Text.Trim().Length > 0) {
                            int name = ClsTwain.lsSqlOper.IndexOf(t.Text.Trim());
                            if (name < 0)
                                continue;
                            string oper = ClsTwain.lssqlOpernum[name];
                            if (oper.Trim().Length <= 0)
                                continue;
                            int id = ClsTwain.Lsinikeys.IndexOf("V" + oper);
                            if (id < 0)
                                continue;
                            string val = ClsTwain.lsinival[id];
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
                string k = str[i];
                bool bl = false;
                switch (k) {
                    case "双面选择":
                        if (chkDoublePages.Checked == false) {
                            chkDoublePages.Checked = true;
                        }
                        else
                            chkDoublePages.Checked = false;

                        bl = true;
                        break;
                    case "纵向":
                        rdVerPages.Checked = true;
                        bl = true;
                        break;
                    case "横向":
                        rdHorpages.Checked = true;
                        bl = true;
                        break;
                    case "A3":
                        comPagesSize.SelectedIndex = 2;
                        bl = true;
                        break;
                    case "A4":
                        comPagesSize.SelectedIndex = 1;
                        bl = true;
                        break;
                    case "B5":
                        comPagesSize.SelectedIndex = 0;
                        bl = true;
                        break;
                    case "彩色":
                        rdColorRed.Checked = true;
                        bl = true;
                        break;
                    case "灰度":
                        rdColorGray.Checked = true;
                        bl = true;
                        break;
                    case "黑白":
                        rdColorWithe.Checked = true;
                        bl = true;
                        break;
                    case "ADF":
                        rdFeedAuto.Checked = true;
                        bl = true;
                        break;
                    case "平板":
                        rdFeedFlat.Checked = true;
                        bl = true;
                        break;
                }
                if (bl)
                    return;
                foreach (var item in toolStrip1.Items) {
                    if (item is ToolStripButton) {
                        ToolStripButton t = (ToolStripButton)item;
                        if (t.Text == k) {
                            t.PerformClick();
                        }
                    }
                }
            }
        }




        #endregion
    }
}
