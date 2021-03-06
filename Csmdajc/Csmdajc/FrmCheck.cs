﻿using CsmCon;
using DAL;
using HLFtp;
using HLjscom;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
        UcDLInfo ucdL;
        UcInfoEnter ucInfo;
        private int ImgNum = 0;
        private int archzt = 0;
        private Pubcls pub;
        private MouseEventArgs exArgs;
        private int Round = 0;
        private int ImgSize = 0;
        private List<int> strpage = new List<int>();
        private string indexpage = "";
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
                gArch.LineClickLoadInfo += GArch_LineClickLoadInfo;
                gr1.Controls.Add(gArch);
                // gr2.Visible = false;
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
                ucContents1.LineFocus += UcContents1_LineFocus;
                gr1_1.Controls.Add(ucContents1);
                Clscheck.infobl = Common.GetConteninfoblchk();
                if (Clscheck.infobl) {
                    splitCont.Panel2Collapsed = false;
                    Infoshow();
                }
                else
                    splitCont.Panel2Collapsed = true;
            } catch (Exception ex) {
                MessageBox.Show("窗体控件初始化失败:" + ex.ToString());
            }
        }

        private void UcContents1_LineFocus(object sender, EventArgs e)
        {
            ImgView.Focus();
        }

        private void GArch_LineClickLoadInfo(object sender, EventArgs e)
        {
            //LoadContents();
            Clscheck.Archid = gArch.Archid;
            Clscheck.RegPage = gArch.ArchRegPages;
            int arid = Clscheck.Archid;
            if (arid <= 0)
                return;
            UcContents.ArchId = arid;
            UcContents.ArchMaxPage = Clscheck.RegPage;
            // UcContents.ArchStat = Convert.ToInt32(gArchSelect1.Archstat);
            ucContents1.LoadContents(arid, UcContents.ArchMaxPage);
          //  int p;
          //  ucInfo.LoadInfo(arid, 1, "案卷信息", out p);
            gArch.LvData.Focus();
        }

        void Infoshow()
        {
            //ucdL = new UcDLInfo();
            //ucdL.Dock = DockStyle.Fill;
            //gr1_2.Controls.Add(ucdL);
            //ucdL.LoadInfo(Clscheck.Archid);
            ucInfo = new UcInfoEnter();
            ucInfo.Dock = DockStyle.Fill;
            gr1_2.Controls.Add(ucInfo);
            ucInfo.GetInfoCol();
        }


        private void UcContents1_OneClickGotoPage(object sender, EventArgs e, string title, string page)
        {
            labConten.Text = title;
            int p = 0;
            bool bl = int.TryParse(page, out p);
            if (!bl || p <= 0)
                return;
            if (p > 0 && p <= Clscheck.MaxPage) {
                int id = 0;
                for (int i = 0; i < strpage.Count; i++) {
                    int s = strpage[i];
                    if (s < p)
                        id += 1;
                }
                Himg._Gotopage(p + id);
            }
            //string ywid = ucContents1.ywid;
            //ucdL.Getywid(ywid);
        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            try {
                Init();
                Himg._Instimagtwain(this.ImgView, this.Handle, 0);
                Himg._Rectang(true);
                //Writeini.Fileini = Path.Combine(Application.StartupPath, "Csmkeyval.ini");
                Getsqlkey();
                pub = new Pubcls();
                //  Startocr();
                GetContenInfo();
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
            strpage.Clear();
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
            }
        }

        void Startocr()
        {
            if (!Himg._StartOcr(0)) {
                MessageBox.Show("Ocr识别模块启动失败!");
                return;
            }
        }

        #region ClickEve

        private  void toolStripPrint_Click(object sender, EventArgs e)
        {
            //int stat = Common.GetArchWorkState(Clscheck.Archid);
            //if (stat < (int)T_ConFigure.ArchStat.质检完) {
            //    MessageBox.Show("未质检不允许打印目录!");
            //    return;
            //}
            Task.Run(() => { GetArchContenWriteXlsPrint(Clscheck.Archid); });

        }
        private void toolStripSharePenSet_Click(object sender, EventArgs e)
        {
            FrmSharePenSet penset = new FrmSharePenSet();
            penset.ShowDialog();
        }

        private void toolStripSharePen_ButtonClick(object sender, EventArgs e)
        {
            int sj = FrmSharePenSet.shuliang;
            int bj = FrmSharePenSet.shuliang;
            int yz = FrmSharePenSet.Yuzhi;
            Himg._Sharpen(sj, bj, yz);
        }
        private void toolStripMagin_Click(object sender, EventArgs e)
        {
            ImgSize = 1;
            Himg._ImgMagni(true);
        }
        private void toolStripRound_Click(object sender, EventArgs e)
        {
            Round = 1;
        }

        private void toolStripOcr_Click(object sender, EventArgs e)
        {
            //string txt = Himg._OcrRecttxt();
            //if (txt.Length > 0) {
            //    txt = (RegexCh(txt)).Replace("天津市", "").Replace("东丽区", "");
            //    ucContents1.Setocrtxt(txt);
            //}
        }
        private string RegexCh(string s)
        {
            string str = "";
            Regex reg = new Regex("[\u4e00-\u9fa5]");
            foreach (Match v in reg.Matches(s)) {
                str += v.ToString().Replace("一", "");
            }
            return str;
        }

        private void ImgView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                if (Himg.CopyImgid == 1) {
                    Himg.CombineFloater();
                    Himg._Rectang(true);
                    Himg.CopyImgid = 0;
                }
                else if (ImgSize > 0)
                    Himg._ImgMagni(false);
                else
                    toolStripCut_Click(sender, e);
                ImgSize = 0;
            }
            else if (ImgSize == 0) {
                if (Round == 0)
                    Himg._Rectang(true);
                else {
                    Himg._RectangYuan(true);
                    Round = 0;
                }
            }
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
            Frmgoto Fgoto = new Frmgoto();
            Frmgoto.Maxpage = Clscheck.MaxPage;
            Fgoto.ShowDialog();
            if (Frmgoto.Npage > 0) {
                Himg._Gotopage(Frmgoto.Npage);
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

        private void toolStripCenter_Click(object sender, EventArgs e)
        {
            Himg._CenterImg();
        }


        private void toolStripUppage_Click(object sender, EventArgs e)
        {
            if (ImgView.Image != null && Clscheck.CrrentPage > 1) {
                if (ImgNum >= 1)
                    ImgNum -= 1;

                Himg._Pagenext(0);
                Thread.Sleep(10);
                ShowPage();
                //  int p = Clscheck.CrrentPage;
                //int id = 0;
                //for (int i = 0; i < strpage.Count; i++) {
                //    int s = strpage[i];
                //    if (s < p)
                //        id += 1;
                //}
                int indx;
                bool bl = int.TryParse(indexpage, out indx);
                if (bl)
                    ucContents1.OnChangContents(indx);
            }
        }

        private void toolStripDownPage_Click(object sender, EventArgs e)
        {
            NextPage();
            ShowPage();
            int indx;
            bool bl = int.TryParse(indexpage, out indx);
            if (bl)
                ucContents1.OnChangContents(indx);
        }
        private void NextPage()
        {
            try {
                if (Clscheck.CrrentPage != Clscheck.MaxPage) {
                    ImgNum += 1;
                    Himg._SavePage();
                    Thread.Sleep(50);
                    Himg._Pagenext(1);
                    //int p = Clscheck.CrrentPage;
                    //int id = 0;
                    //for (int i = 0; i < strpage.Count; i++) {
                    //    int s = strpage[i];
                    //    if (s < p)
                    //        id += 1;
                    //}
                }
                else if (Clscheck.CrrentPage == Clscheck.MaxPage) {
                    ImgNum += 1;
                    Himg._SavePage();
                    Thread.Sleep(50);
                    toolStripSave_Click(null, null);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

        }

        //东丽区专用
        bool Entertag()
        {
            int entag = Common.GetEnterinfo(Clscheck.Archid);
            int conte = Common.Getconteninfo(Clscheck.Archid);
            if (entag != conte) {
                MessageBox.Show("信息录入手续" + entag.ToString() + " 目录录入业务id总计:" + conte.ToString() + " 不一致");
                return false;
            }
            return true;
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;

            if (Clscheck.MaxPage != Clscheck.RegPage) {
                MessageBox.Show("登记页码和图像页码不一致无法完成质检!");
                return;
            }

            if (ImgNum < Clscheck.MaxPage - 1) {
                MessageBox.Show("未仔细检查此卷档案，请重新质检!");
                return;
            }
            //东丽区专用 
            //if (!ucContents1.IsGetywid() || !Entertag())
            //    return;
            if (MessageBox.Show("质检完成您确定要上传档案吗？", "提示", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK) {
                string filepath = Clscheck.ScanFilePath;
                string filename = Clscheck.FileNametmp;
                int arid = Clscheck.Archid;
                int pages = Clscheck.MaxPage;
                int tag = Himg.TagPage;
                Cledata();
                Task.Run(new Action(() => { FtpUpFinish(filepath, arid, filename, pages, tag); }));
                gArch.txtBoxsn.Focus();
            }
        }

        private void toolStripClose_Click(object sender, EventArgs e)
        {
            if (ImgView.Image == null)
                return;
            int arid = Clscheck.Archid;
            string filename = Clscheck.FileNametmp;
            string filepath = Clscheck.ScanFilePath;
            Common.WriteArchlog(arid, "未质检完退出!");
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
                    Thread.Sleep(5000);
                    //if (archzt == 1)
                    //    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.排序完);
                    //else
                    //    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.质检完);
                }
            }));
        }

        private void toolStripBigPage_Click(object sender, EventArgs e)
        {
            Himg._Sizeimge(1);
        }
        private void toolStripYzidthsider_Click(object sender, EventArgs e)
        {
            Himg.SetImgWidthSide(0);
        }

        private void toolStripZwidthsider_Click(object sender, EventArgs e)
        {
            Himg.SetImgWidthSide(1);
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

        private void toolStripcopy_Click(object sender, EventArgs e)
        {
            Himg.CopyImg();
        }
        private void toolStripPase_Click(object sender, EventArgs e)
        {
            if (exArgs == null)
                return;
            Himg.PasteImg(exArgs);
            exArgs = null;
        }
        private void ImgView_MouseClick(object sender, MouseEventArgs e)
        {
            exArgs = e;

        }
        private void ImgView_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
           // Himg._ImgFill(e);
        }

        private void ImgView_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                toolStripCut_Click(sender, e);
            else
                Himg._Rectang(true);

        }

        private void FrmIndex_KeyDown(object sender, KeyEventArgs e)
        {
            pub.KeyShortDown(e, Clscheck.lsinival, Clscheck.Lsinikeys, Clscheck.lssqlOpernum, Clscheck.lsSqlOper, out Clscheck.keystr);
            if (Clscheck.keystr.Trim().Length > 0)
                KeysDownEve(Clscheck.keystr.Trim());
            Keys keyCode = e.KeyCode;
            //if (e.KeyCode == Keys.ShiftKey)
            //    ucContents1.Setinfofocus();
            if (e.KeyCode == Keys.Escape) {
                gArch.txtBoxsn.Focus();
                gArch.txtBoxsn.SelectAll();
            }

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
                string file = Clscheck.ScanFilePath;
                int page = Clscheck.MaxPage;
                Cledata();
                Task.Run(new Action(() =>
                {
                    Repair(filetmp, archid, archpos, page);
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
                dt = Common.GetOpenkey(this.Text);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string strid = dt.Rows[i][0].ToString();
                    string strkey = dt.Rows[i][2].ToString();
                    string strnum = dt.Rows[i][3].ToString();
                    if (strid.Trim().Length > 0 && strkey.Trim().Length > 0 && strnum.Trim().Length > 0) {
                        Clscheck.Lsinikeys.Add(strkey);
                        Clscheck.lsinival.Add(strnum);
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
                            int name = Clscheck.lsSqlOper.IndexOf(t.Text.Trim());
                            if (name < 0)
                                continue;
                            string oper = Clscheck.lssqlOpernum[name];
                            if (oper.Trim().Length <= 0)
                                continue;
                            int id = Clscheck.Lsinikeys.IndexOf("V" + oper);
                            if (id < 0)
                                continue;
                            string val = Clscheck.lsinival[id];
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
                            int name = Clscheck.lsSqlOper.IndexOf(t.Text.Trim());
                            if (name < 0)
                                continue;
                            string oper = Clscheck.lssqlOpernum[name];
                            if (oper.Trim().Length <= 0)
                                continue;
                            int id = Clscheck.Lsinikeys.IndexOf("V" + oper);
                            if (id < 0)
                                continue;
                            string val = Clscheck.lsinival[id];
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
            // ucContents1.LoadContents(gArch.Archid, gArch.ArchRegPages);
        }


        public void ReadDict()
        {
            try {
                Dictionary<int, int> pagenumber = new Dictionary<int, int>();
                Dictionary<int, string> pageabc = new Dictionary<int, string>();
                Dictionary<int, string> fuhao = new Dictionary<int, string>();
                DataTable dt = Common.ReadPageIndexInfo(Clscheck.Archid);
                if (dt != null && dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    string PageIndexInfo = dr["PageIndexInfo"].ToString();
                    int page = Convert.ToInt32(dr["pages"].ToString());
                    string arpage = dr["archpage"].ToString();
                    if (!string.IsNullOrEmpty(PageIndexInfo)) {
                        string[] arrPage = PageIndexInfo.Split(';');
                        if (arrPage.Length > 0) {
                            for (int i = 0; i < arrPage.Length; i++) {
                                string[] str = arrPage[i].Trim().Split(':');
                                if (str.Length <= 0)
                                    continue;
                                int p = Convert.ToInt32(str[0]);
                                if (p > page)
                                    continue;
                                if (!isExists(str[1].ToString()) && str[1].IndexOf("-") < 0)
                                    pagenumber.Add(p, Convert.ToInt32(str[1]));
                                else if (str[1].IndexOf("-") >= 0)
                                    fuhao.Add(p, str[1].ToString());
                                else
                                    pageabc.Add(p, str[1].ToString());
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(arpage)) {
                        string[] arrPage = arpage.Split(';');
                        if (arrPage.Length > 0) {
                            for (int i = 0; i < arrPage.Length; i++) {
                                string[] str = arrPage[i].Trim().Split('-');
                                if (str.Length > 0)
                                    strpage.Add(Convert.ToInt32(str[0]));
                            }
                        }
                    }
                    Himg._PageNumber = pagenumber;
                    Himg._PageAbc = pageabc;
                    Himg._PageFuhao = fuhao;
                }
            } catch { }
        }

        private bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }
        private void GetPages(int page, int counpage)
        {
            Clscheck.MaxPage = counpage;
            Clscheck.CrrentPage = page;
            labPageCrrent.Text = string.Format("第  {0}   页", page);
            labPageCount.Text = string.Format("共  {0}   页", counpage);
        }

        private void ShowPage()
        {
            try {
                string txt = Himg._Readpage();
                indexpage = txt;
                labpage.Text = string.Format("第 {0} 页", txt);
            } catch { }

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
            else if (stat >= (int)T_ConFigure.ArchStat.质检完)
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
                    Common.WriteArchlog(Clscheck.Archid,"进入案卷质检");
                    Himg.Filename = Clscheck.ScanFilePath;
                    Himg.LoadPage(pages);
                    ReadDict();
                    LoadContents();
                    //ucdL.LoadInfo(Clscheck.Archid);
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
                                if (ftp.FtpMoveFile(sourefile, goalfile, path))
                                    return (FileMoveBool(localScanFile));
                            }
                        }
                        else if (stsa == 2) {
                            sourefile = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path))
                                    return (FileMoveBool(localScanFile));
                            }
                        }
                        else {
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    archzt = 1;
                                    return (FileMoveBool(localScanFile));
                                }
                            }
                            sourefile = Path.Combine(T_ConFigure.FtpArchSave, Clscheck.FileNametmp.Substring(0, 8), Clscheck.FileNametmp);
                            if (ftp.FtpCheckFile(sourefile)) {
                                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                                    archzt = 2;
                                    return (FileMoveBool(localScanFile));
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
                string zj = string.Empty;
                string zjtime = string.Empty;
                string ml = string.Empty;
                string mltime = string.Empty;
                DataTable dt = Common.GetOperator(Clscheck.Archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                DataRow dr = dt.Rows[0];
                //Scanner = dr["扫描"].ToString();
                //scantime = dr["扫描时间"].ToString();
                Indexer = dr["排序"].ToString();
                indextime = dr["排序时间"].ToString();
                Checker = dr["质检"].ToString();
                chktime = dr["质检时间"].ToString();
                enter = dr["录入"].ToString();
                entertime = dr["录入时间"].ToString();
                ml = dr["目录"].ToString();
                mltime = dr["目录时间"].ToString();
                foreach (DataRow d in dt.Rows) {

                    string s = d["扫描"].ToString();
                    if (!Scanner.Contains(s)) {
                        Scanner += s + ",";
                        scantime += d["扫描时间"].ToString() + ",";
                    }
                }
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
                    toollabinfochk.Text = string.Format("目录:{0}", ml);
                    toollabinfochktime.Text = string.Format("目录时间:{0}", mltime);
                    toollabezchk.Text = string.Format("总检:{0}", zj);
                    toollabzchktime.Text = string.Format("总检时间:{0}", zjtime);

                }));
                //dt = Common.GetOperatorchk(Clscheck.Archid);
                //if (dt == null || dt.Rows.Count <= 0)
                //    return;
                //this.BeginInvoke(new Action(() =>
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++) {
                //        string user = dt.Rows[i][0].ToString();
                //        string time = dt.Rows[i][1].ToString();
                //        toollabinfochk.Text = string.Format("信息质检:{0}", user);
                //        toollabinfochktime.Text = string.Format("时间:{0}", time);

                //    }
                //}));
                //dt.Dispose();
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
                //ucdL.Cleinfo();
                Himg.Filename = "";
                Himg.RegPage = 0;
                ImgView.Image = null;
                Clscheck.Archid = 0;
                Clscheck.ArchPos = "";
                Clscheck.RegPage = 0;
                Clscheck.FileNametmp = "";
                Clscheck.ScanFilePath = "";
                toolArchno.Text = "当前卷号:";
                labPageCrrent.Text = "第     页";
                labPageCount.Text = "共      页";
                Clscheck.ScanFilePath = "";
                Clscheck.task = false;
                gArch.butLoad.Enabled = true;
                ImgNum = 0;
            }));

        }

        private void FtpUpFinish(string filetmp, int arid, string filename, int pages, int tag)
        {
            try {
                Common.WriteArchlog(arid, "质检完成退出!");
                if (File.Exists(filetmp)) {
                    Common.WiteUpTask(arid, "", filename, (int)T_ConFigure.ArchStat.质检完, pages, filetmp, tag.ToString());
                    if (T_ConFigure.FtpStyle == 1) {
                        string sourefile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave, filename.Substring(0, 8), filename);
                        string goalfile = Path.Combine(T_ConFigure.FtpArchSave, filename.Substring(0, 8), filename);
                        string path = Path.Combine(T_ConFigure.FtpArchSave, filename.Substring(0, 8));
                        if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                            Thread.Sleep(5000);
                            Common.SetCheckFinish(arid, DESEncrypt.DesEncrypt(filename), 1, (int)T_ConFigure.ArchStat.质检完);
                            Common.DelTask(arid);
                            return;
                        }
                    }
                    else {
                        string RemoteDir = filename.Substring(0, 8);
                        if (ftp.SaveRemoteFileUp(T_ConFigure.FtpArchSave, RemoteDir, filetmp, filename)) {
                            Common.DelTask(arid);
                            Common.SetCheckFinish(arid, DESEncrypt.DesEncrypt(filename), 1, (int)T_ConFigure.ArchStat.质检完);
                            try {
                                File.Delete(filetmp);
                                Directory.Delete(Path.GetDirectoryName(filetmp));
                            } catch { }
                            return;

                        }
                        //发现图像上传时有错位现象
                        //string newfile = Path.Combine(T_ConFigure.FtpArchSave, RemoteDir, filename);
                        //string newpath = Path.Combine(T_ConFigure.FtpArchSave, RemoteDir);
                        //bool x = await ftp.FtpUpFile(filetmp, newfile, newpath);
                        //if (x) {
                        //    Common.SetCheckFinish(arid, DESEncrypt.DesEncrypt(filename), 1, (int)T_ConFigure.ArchStat.质检完);
                        //    Common.DelTask(arid);
                        //    try {
                        //        File.Delete(filetmp);
                        //        Directory.Delete(localPath);
                        //    } catch { }
                        //    return;
                        //}
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

        private void Repair(string filetmp, int arid, string archpos, int page)
        {
            try {
                Common.WriteArchlog(arid, "质检返工!");
                //if (ftp.SaveRemoteFileUp(T_ConFigure.gArchScanPath, archpos, filetmp, T_ConFigure.ScanTempFile)) {
                //    Common.SetScanFinish(arid, page, 1, (int)T_ConFigure.ArchStat.扫描完);
                //    Common.DelTask(Convert.ToInt32(arid));
                //    try {
                //        File.Delete(filetmp);
                //        Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                //    } catch {
                //    }
                //    return;
                //}
                string sourefile = "";
                if (T_ConFigure.FtpStyle == 0) {
                    if (archzt == 1)
                        sourefile = Path.Combine(T_ConFigure.FtpArchIndex, filetmp.Substring(0, 8), filetmp);
                    else
                        sourefile = Path.Combine(T_ConFigure.FtpArchSave, filetmp.Substring(0, 8), filetmp);
                }
                else
                    sourefile = Path.Combine(T_ConFigure.FtpFwqPath, T_ConFigure.TmpSave, filetmp.Substring(0, 8), filetmp);
                string goalfile = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                string path = Path.Combine(T_ConFigure.gArchScanPath, archpos);
                if (ftp.FtpMoveFile(sourefile, goalfile, path)) {
                    Common.SetArchWorkState(arid, (int)T_ConFigure.ArchStat.扫描完);
                    Common.Writelog(arid, "质检退!");
                    Common.SetCheckFinish(arid, "", 2, (int)T_ConFigure.ArchStat.扫描完);
                }
                if (T_ConFigure.FtpStyle == 1) {
                    try {
                        File.Delete(filetmp);
                    } catch { }
                }
            } catch {
                Common.WriteArchlog(arid, "质检退回失败!");
            }
        }





        #endregion

        #region PrintConte



        private void GetContenInfo()
        {
            ClsPrintInfo.PrintInfo = T_Sysset.GetGensetPrint();
            if (ClsPrintInfo.PrintInfo == null || ClsPrintInfo.PrintInfo.Rows.Count <= 0)
                return;
            DataRow dr = ClsPrintInfo.PrintInfo.Rows[0];
            ClsPrintConten.PrintContenTable = dr["PrintContenTable"].ToString();
            string str = dr["PrintContenInfo"].ToString();
            if (str.Length > 0) {
                string[] a = str.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    string[] c = b.Split(':');
                    if (c.Length > 0) {
                        if (c[0].IndexOf("SN0") >= 0) {
                            string[] f = b.Split(':');
                            ClsPrintConten.PrintContenSn = f[1];
                        }
                        else {
                            string d = c[3];
                            if (d == "True") {
                                ClsPrintConten.PrintContenPagesn = c[0];
                                ClsPrintConten.PrintContenPageMode = Convert.ToInt32(c[4]);
                            }
                            ClsPrintConten.PrintContenCol.Add(c[0]);
                            ClsPrintConten.printContenXls.Add(c[1]);
                            ClsPrintConten.PrintContenDz.Add(c[2]);
                            ClsPrintConten.PrintContenPage.Add(c[3]);
                        }
                    }
                    ClsPrintConten.PrintContenAll.Add(b);
                }
            }
        }

        private void WriteLog(string str)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string dt = DateTime.Now.ToString();
            try {
                string file = "打印日志.txt";
                string filepath = Path.Combine(Application.StartupPath, file);
                if (!File.Exists(filepath)) {
                    fs = new FileStream(filepath, FileMode.Create);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(str + " 操作时间 " + dt);
                    sw.Flush();
                }
                else {
                    fs = new FileStream(filepath, FileMode.Append);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(str + " 操作时间 " + dt);
                    sw.Flush();
                }
            } catch { } finally {
                sw.Close();
                fs.Close();
            }
        }

        private bool GetArchContenWriteXlsPrint(int archid)
        {
            DataTable ArchConten = Common.GetPrintConten(archid, ClsPrintConten.PrintContenTable, ClsPrintConten.PrintContenCol, ClsPrintConten.PrintContenPagesn);
            if (ArchConten == null || ArchConten.Rows.Count <= 0) {
                string str = "ID号:" + archid + "目录获取失败!";
                WriteLog(str);
                return false;
            }
            int countpage = Common.GetArchPages(archid);
            try {
                Workbook work = new Workbook();
                Worksheet wsheek = null;
                work.LoadFromFile(Path.Combine(Application.StartupPath, "目录.xls"));
                wsheek = work.Worksheets[0];
                string strsn = ClsPrintConten.PrintContenSn;
                //获取起始行和列
                int rowsn = 0;
                int colsn = 0;
                if (strsn.Trim().Length > 1) {
                    rowsn = Convert.ToInt32(strsn.Remove(0, 1));
                    colsn = ToNum(strsn.Substring(0, 1)) + 1;
                }
                int arow = 0;
                int bcol = 0;
                int dz = 0;
                for (int i = 0; i < ArchConten.Rows.Count; i++) {
                    dz = 0;
                    for (int j = 0; j < ArchConten.Columns.Count; j++) {
                        string str = ArchConten.Rows[i][j].ToString();
                        if (j == 4)
                            str = str.PadLeft(3, '0');
                        if (j == 1) {
                            string s = str.Substring(0, 1);
                            if (s.Contains("\\") || s.Contains("/"))
                                str = str.Substring(1, str.Length - 1);
                            s = str.Substring(str.Length - 1, 1);
                            if (s.Contains("\\") || s.Contains("/"))
                                str = str.Substring(0, str.Length - 1);
                        }
                        if (rowsn > 0)
                            wsheek.Range[rowsn + i, colsn].Text = (i + 1).ToString();
                        if (ClsPrintConten.printContenXls.Count > 0) {
                            if (dz < ClsPrintConten.printContenXls.Count) {
                                arow = Convert.ToInt32(ClsPrintConten.printContenXls[dz].Remove(0, 1));
                                bcol = ToNum(ClsPrintConten.printContenXls[dz].Substring(0, 1)) + 1;
                            }
                            if (dz < ClsPrintConten.PrintContenPage.Count && ClsPrintConten.PrintContenPage[dz] == "True") {
                                if (ClsPrintConten.PrintContenPageMode == 2) {
                                    int p = Convert.ToInt32(str);
                                    int p1 = 0;
                                    try {
                                        if (i != ArchConten.Rows.Count - 1) {
                                            p1 = Convert.ToInt32(ArchConten.Rows[i + 1][j].ToString());
                                            if (p == p1)
                                                str = p + "-" + p1;
                                            else {
                                                str = p + "-" + (p1 - 1);
                                            }
                                        }
                                        else {
                                            p1 = countpage;
                                            if (p == p1)
                                                str = p + "-" + p1;
                                            else {
                                                str = p + "-" + (p1 - 1);
                                            }
                                        }
                                    } catch {
                                        string str1 = "ID号:" + archid + "目录页码不正确";
                                        WriteLog(str1);
                                        return false;
                                    }
                                }
                                else {
                                    if (i == ArchConten.Rows.Count - 1) {
                                        //int p = Convert.ToInt32(str);
                                        int p1 = countpage;
                                        str = str.PadLeft(3, '0') + "-" + p1.ToString().PadLeft(3, '0');
                                    }
                                }
                                wsheek.Range[arow + i, bcol].Text = str;
                            }
                            //else if (dz < ClsPrintConten.PrintContenDz.Count && ClsPrintConten.PrintContenDz[dz] == "True")
                            //    wsheek.Range[arow + i, bcol].Text = str;
                            //else if (arow == i && j == bcol) {
                            //    wsheek.Range[arow + i, bcol].Text = str;
                            //}
                            else if (ClsPrintConten.PrintContenDz[dz] == "False") {
                                string s = ClsPrintConten.printContenXls[dz].ToString();
                                if (str.Trim().Length > 0)
                                    wsheek.Range[s].Text = str.PadLeft(4, '0');
                            }
                            else if (ClsPrintConten.PrintContenDz[dz] == "True")
                                wsheek.Range[arow + i, bcol].Text = str;
                        }
                        dz += 1;
                    }
                }
                rowsn = wsheek.LastRow;
                string fontname = wsheek.Rows[6].Cells[3].Style.Font.FontName;
                double fontsize = wsheek.Rows[6].Cells[3].Style.Font.Size;
                CellRange range = wsheek.Range["A6" + ":H" + rowsn];
                range.BorderInside(LineStyleType.Thin, ExcelColors.Black);
                range.BorderAround(LineStyleType.Thin, ExcelColors.Black);
                range.Style.Font.Size = fontsize;
                range.Style.Font.FontName = fontname;
                range.HorizontalAlignment = HorizontalAlignType.Center;
                range.VerticalAlignment = VerticalAlignType.Center;
                range.Style.WrapText = true;
                range.AutoFitRows();
                for (int i = 6; i < rowsn; i++) {
                    double row = wsheek.Rows[i].RowHeight;
                    if (row < 50)
                        wsheek.Rows[i].SetRowHeight(50, true);
                    wsheek.Range["E" + (i + 1) + ":F" + (i + 1)].Merge();
                }
                wsheek.PageSetup.PrintTitleRows = "$1:$5";
                work.PrintDocument.Print();
                return true;
            } catch (Exception e) {
                string str = "ID号:" + archid + ":" + e;
                WriteLog(str);
                return false;
            }
        }

        private int ToNum(string columnName)
        {
            if (!Regex.IsMatch(columnName.ToUpper(), @"[A-Z]+") && !Regex.IsMatch(columnName.ToUpper(), @"[a-z]+"))
                return 0;
            int index = 0;
            char[] chars = columnName.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index - 1;
        }
        private string ToName(int index)
        {
            if (index < 0) { return ""; }

            List<string> chars = new List<string>();
            do {
                if (chars.Count > 0) index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return String.Join(string.Empty, chars.ToArray());
        }

        #endregion


    }
}
