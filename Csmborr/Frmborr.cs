﻿
using DAL;
using HLFtp;
using System;
using System.IO;
using System.Windows.Forms;

namespace Csmborr
{
    public partial class FrmBorr : Form
    {
        public FrmBorr()
        {
            InitializeComponent();
        }

        HFTP ftp = null;
        private bool istxt()
        {
            if (comborrbcol.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询字段!");
                comborrbcol.Focus();
                return false;
            }

            if (comborrbczf.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择操作符!");
                comborrbczf.Focus();
                return false;
            }
            if (txtborrgjz.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入关键字!");
                txtborrgjz.Focus();
                return false;
            }
            return true;
        }

        private FrmImgshow imgshow;

        private void butQuer_Click(object sender, EventArgs e)
        {
            if (!istxt())
                return;
            string col = comborrbcol.Text.Trim();
            string czf = comborrbczf.Text.Trim();
            string gjz = txtborrgjz.Text.Trim();
            BorrMethod.Getdata(lvborrQuer, col, czf, gjz);
        }


        private void FrmBorr_Shown(object sender, EventArgs e)
        {
            BorrMethod.Getinfo(lvborrQuer, comborrbcol);
            ftp = new HFTP();
            ftp.PercentChane += new HFTP.PChangedHandle(Downjd);
        }

        private void lvborrQuer_Click(object sender, EventArgs e)
        {
            if (lvborrQuer.SelectedItems.Count <= 0)
                return;
            BorrMethod.GetArchinfo(lvborrQuer, toolslb_box, toolslb_archno, toolslb_File);
        }


        private void Downjd(object sender, PChangeEventArgs e)
        {
            this.toolproess.Visible = true;
            this.toolproess.Minimum = 0;
            this.toolproess.Maximum = (int)e.CountSize;
            Application.DoEvents();
            this.toolproess.Value = (int)e.TmpSize;
            if (e.CountSize == e.TmpSize) {
                this.toolproess.Visible = false;
            }
        }


        private void LoadFile()
        {
            try {
                if (BorrMethod.Archid <= 0) {
                    MessageBox.Show("ID获取失败请重新选择案卷!");
                    return;
                }
                if (BorrMethod.Filename.Trim().Length <= 0) {
                    MessageBox.Show("文件名称获取失败!");
                    return;
                }
                int ArchState = Common.GetArchWorkState(BorrMethod.Archid);
                if (ArchState < (int)Common.档案状态.质检完) {
                    MessageBox.Show("此卷档案未质检无法进行查阅！");
                    return;
                }
                string FileName = BorrMethod.Filename;
                string localPath = Path.Combine(Common.LocalTempPath, FileName.Substring(0, 8));
                string localCheckFile = Path.Combine(Common.LocalTempPath, FileName.Substring(0, 8), FileName);
                try {
                    if (!Directory.Exists(localPath)) {
                        Directory.CreateDirectory(localPath);
                    }
                    if (File.Exists(localCheckFile)) {
                        File.Delete(localCheckFile);
                    }
                } catch { }
                if (ArchState == (int)(Common.档案状态.质检完)) {
                    string filjpg = Path.Combine(Common.ArchSavePah, FileName.Substring(0, 8), FileName);
                    if (ftp.FtpCheckFile(filjpg)) {
                        if (ftp.DownLoadFile(Common.ArchSavePah, FileName.Substring(0, 8), localCheckFile, FileName)) {
                            FrmImgshow.Arhcid =BorrMethod.Archid;
                            FrmImgshow.Filename = localCheckFile;
                            FrmImgshow.ImgPrint = BorrMethod.Imgsys;
                            imgshow = new FrmImgshow();
                            if (imgshow == null || imgshow.IsDisposed) {
                                imgshow = new FrmImgshow();
                            }
                            imgshow.Activate();
                            imgshow.ShowDialog();
                            return;
                        }
                    }
                    MessageBox.Show("警告，文件不存在!");
                    return;
                }
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void lvborrQuer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                contMenu.Visible = true;
                contMenu.Location = e.Location;
            }
        }

        private void lvborrQuer_MouseUp(object sender, MouseEventArgs e)
        {
            if (lvborrQuer.SelectedItems.Count <= 0)
                contMenu.Visible = false;
        }

        private void butImgload_Click(object sender, EventArgs e)
        {
            contMenu.Visible = false;
            if (BorrMethod.Archid <= 0)
                return;
            if (BorrMethod.Filename.Trim().Length <= 0) {
                MessageBox.Show("图像文件名获取失败!");
                return;
            }
            LoadFile();
        }
        private void butitemArchgh_Click(object sender, EventArgs e)
        {
            contMenu.Visible = false;
            FrmArchBorr borr = new FrmArchBorr();
            borr.ShowDialog();
        }

        private void butitemArchjy_Click(object sender, EventArgs e)
        {
            contMenu.Visible = false;
            FrmArchBorr borr = new FrmArchBorr();
            borr.ShowDialog();
        }
    }
}
