using CsmCon;
using DAL;
using HLFtp;
using HLjscom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
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
            gArch.butLoad.Enabled = false;
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
            } finally {
                gArch.butLoad.Enabled = true;
            }
        }

        private void FrmTwain_Load(object sender, EventArgs e)
        {
            try {
                Init();
                Himg._Instimagtwain(this.ImgView, this.Handle, 1);
                Himg._Rectang(true);
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
            bool loadfile = await LoadFile();
            Himg.Filename = ClsTwain.ScanFileTmp;
            if (loadfile == true) {
                int pages = 1;
                if (!File.Exists(ClsTwain.ScanFileTmp)) {
                    Cledata();
                    return;
                }
                Himg.LoadPage(pages);
                Getuser();
                GetQspage();
            }
        }

        private Task<bool> LoadFile()
        {
            return Task.Run(() =>
            {
                try {
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
                                return true;
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
                    return false;

                } catch (Exception e) {
                    MessageBox.Show("加载文件失败!" + e.ToString());
                    Common.SetArchWorkState(ClsTwain.Archid, (int)T_ConFigure.ArchStat.无);
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
                DataTable dt = Common.GetOperator(ClsTwain.Archid);
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
                if (string.IsNullOrEmpty(PageIndexInfo))
                    return;
                string[] arrPage = PageIndexInfo.Split(';');
                if (arrPage == null || arrPage.Length <= 0)
                    return;
                foreach (string i in arrPage) {
                    LisPage.Add(i);
                }
                for (int i = 1; i < ClsTwain.RegPage; i++) {
                    if (LisPage.IndexOf(i.ToString()) < 0) {
                        if (Qspages.Trim().Length<=0)
                            Qspages += i.ToString();
                        else {
                            Qspages +="," +i.ToString();
                        }
                    }
                }
                this.BeginInvoke(new Action(() => { labQsPages.Text = "当前卷缺少： " + Qspages + " 页"; }));
            });

        }

        private void GetPages(int page, int counpage)
        {
            ClsTwain.MaxPage = counpage;
            if (page > counpage)
                page = counpage;
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
        }

        private async void FtpUp(string filetmp, string archpos, int maxpage, int arid)
        {
            try {
                if (File.Exists(filetmp)) {
                    Common.WiteUpTask(arid, archpos, T_ConFigure.ScanTempFile, (int)T_ConFigure.ArchStat.扫描中, maxpage, T_ConFigure.ScanTempFile);
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourcefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpScan, archpos, T_ConFigure.ScanTempFile);
                        string goalfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        string path = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                        if (ftp.FtpMoveFile(sourcefile, goalfile, path)) {
                            Common.DelTask(arid);
                            Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.扫描完);
                            try {
                                Directory.Delete(Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpScan, archpos));
                            } catch { }
                            return;
                        }
                    }
                    else {
                        string newfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        string newpath = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                        bool x = await ftp.FtpUpFile(filetmp, newfile, newpath);
                        if (x) {
                            Common.DelTask(arid);
                            Common.SetScanFinish(arid, maxpage, 1, (int)T_ConFigure.ArchStat.扫描完);
                            try {
                                File.Delete(filetmp);
                                Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                            } catch { }
                            return;
                        }

                    }
                }
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
            if (!gArch.GetFocus())
            {
                pub.KeyShortDown(e, ClsTwain.lsinival, ClsTwain.Lsinikeys, ClsTwain.lssqlOpernum, ClsTwain.lsSqlOper, out ClsTwain.keystr);
                if (ClsTwain.keystr.Trim().Length > 0)
                    KeysDownEve(ClsTwain.keystr.Trim());
            }
            Keys keyCode = e.KeyCode;
            if (e.KeyCode == Keys.Escape)
                gArch.LvData.Focus();
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
            Himg._Delepage(1);
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
            int maxpage = ClsTwain.MaxPage;
            int arid = ClsTwain.Archid;
            Cledata();
            Task.Run(new Action(() => { FtpUp(filetmp, archpos, maxpage, arid); }));
            gArch.LvData.Focus();
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

        //private void Keykuaij(object sender, KeyEventArgs e)
        //{
        //    Keys keyCode = e.KeyCode;
        //    switch (keyCode) {
        //        case Keys.Escape:
        //            toolColse_Click(sender, e);
        //            break;
        //        case Keys.Enter:
        //            toolScan_Click(sender, e);
        //            break;
        //        case Keys.Delete:
        //            toolDelPages_Click(sender, e);
        //            break;
        //        case Keys.PageDown:
        //            toolPagesDown_Click(sender, e);
        //            break;
        //        case Keys.PageUp:
        //            toolPagesUp_Click(sender, e);
        //            break;
        //        case Keys.Home:
        //            Himg._Gotopage(1);
        //            break;
        //        case Keys.End:
        //            Himg._Gotopage(ClsTwain.MaxPage);
        //            break;
        //        case Keys.NumPad9:
        //            rdFeedAuto.Checked = true;
        //            break;
        //        case Keys.NumPad6:
        //            rdFeedFlat.Checked = true;
        //            break;
        //        case Keys.NumPad0:
        //            if (chkDoublePages.Checked == false) {
        //                chkDoublePages.Checked = true;
        //            }
        //            else
        //                chkDoublePages.Checked = false;
        //            break;
        //        case Keys.NumPad1:
        //            toolColse_Click(sender, e);
        //            break;
        //        case Keys.NumPad3:
        //            comPagesSize.SelectedIndex = 2;
        //            break;
        //        case Keys.NumPad4:
        //            comPagesSize.SelectedIndex = 1;
        //            break;
        //        case Keys.Space:
        //            toolScan_Click(sender, e);
        //            break;
        //        case Keys.NumPad5:
        //            comPagesSize.SelectedIndex = 0;
        //            break;
        //    }
        //    ImgView.Focus();
        //}

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
                Writeini.GetAllKeyValues(this.Text, out ClsTwain.Lsinikeys, out ClsTwain.lsinival);
            });
        }
       
        void KeysDownEve(string key)
        {
            bool bl = false;
            switch (key) {
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
                    if (t.Text == key) {
                        t.PerformClick();
                    }
                }
            }
        }




        #endregion
    }
}
