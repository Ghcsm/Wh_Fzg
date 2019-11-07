using CsmCon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using System.IO;
using System.Net;
using HLFtp;

namespace CsmInfoLook
{
    public partial class FrmInfolook : Form
    {
        public FrmInfolook()
        {
            InitializeComponent();
        }
        private ImgBrow imgBrow1;
        private int ArchID;

        private int Archid2;
        private string FileNameTmp = "";
        private string FileNameTmp2 = "";
        HFTP ftp = null;
        private int MaxPage;
        private int CrrPage;

        private void Init()
        {
            ImgBrow.ContentsEnabled = false;
            ImgBrow.ModuleVisible = false;
            ImgBrow.id = 0;
            imgBrow1 = new ImgBrow();
            imgBrow1.Spage += new ImgBrow.TransmitPar(ShowPage);
            imgBrow1.Dock = DockStyle.Fill;
            groupBox2.Controls.Add(imgBrow1);
        }

        private int ywid = 0;
        private void ShowPage(int page, int counpage)
        {
            toolslab_PagesCount.Text = string.Format("共{0}页", counpage);
            toolslab_PagesCrre.Text = string.Format("第{0}页", page);
            MaxPage = counpage;
            CrrPage = page;
            int ywidtmp = imgBrow1.Ywid;
            if (ywid == 0 || ywid != ywidtmp) {
                ywid = ywidtmp;
                if (ywid > 0)
                    ucDLInfo1.LoadInfo2(Archid2,ywid);
            }
        }


        private void rabQu_CheckedChanged(object sender, EventArgs e)
        {
            if (rabQu.Checked)
                txtBox2.Enabled = false;
            else
                txtBox2.Enabled = true;
            txtBox1.Focus();

        }

        private void FrmInfolook_Shown(object sender, EventArgs e)
        {
            txtBox1.Focus();
        }

        private void FrmInfolook_Load(object sender, EventArgs e)
        {
            Init();
            ftp = new HFTP();
            ftp.PercentChane += new HFTP.PChangedHandle(Downjd);
        }
        private void Downjd(object sender, PChangeEventArgs e)
        {
            this.toolStripProgress.Visible = true;
            this.toolStripProgress.Minimum = 0;
            this.toolStripProgress.Maximum = (int)e.CountSize;
            Application.DoEvents();
            this.toolStripProgress.Value = (int)e.TmpSize;
            if (e.CountSize == e.TmpSize) {
                this.toolStripProgress.Visible = false;
            }
        }

        private void txtBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtBox2.Focus();
        }

        private void txtBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butOk.Focus();
        }


        void LoadBox()
        {
            DataTable dt = null;
            if (rabBoxsn.Checked) {
                if (txtBox1.Text.Trim().Length <= 0 || txtBox2.Text.Trim().Length <= 0) {
                    MessageBox.Show("盒号范围不能为空!");
                    txtBox1.Focus();
                    return;
                }
                int b1, b2;
                bool bl1 = int.TryParse(txtBox1.Text.Trim(), out b1);
                bool bl2 = int.TryParse(txtBox2.Text.Trim(), out b2);
                if (!bl1 || !bl2) {
                    MessageBox.Show("请输入正确的盒号数值!");
                    txtBox1.Focus();
                    return;
                }
                if (b1 <= 0 || b2 <= 0) {
                    MessageBox.Show("数值不能为0");
                    txtBox1.Focus(); return;
                }
                if (b2 < b1) {
                    MessageBox.Show("终止盒号不能小于起始盒号!");
                    txtBox1.Focus(); return;
                }
                dt = Common.GetinfoLook(txtBox1.Text.Trim(), txtBox2.Text.Trim());
            }
            else {
                if (txtBox1.Text.Trim().Length <= 0) {
                    MessageBox.Show("区号不能为空!");
                    txtBox1.Focus(); return;
                }
                int p;
                bool bl = int.TryParse(txtBox1.Text.Trim(), out p);
                if (!bl && p <= 0) {
                    MessageBox.Show("区号数值不正确！");
                    txtBox1.Focus(); return;
                }
                dt = Common.GetinfoLook(txtBox1.Text.Trim());
            }
            LvBoxsnQu.Items.Clear();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            int i = 1;
            foreach (DataRow dr in dt.Rows) {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                string boxsn = dr["boxsn"].ToString();
                string archxq = dr["ArchXqStat"].ToString();
                string arid = dr["id"].ToString();
                string file = dr["IMGFILE"].ToString();
                lvi.SubItems.AddRange(new string[] { boxsn, archxq, arid, file });
                LvBoxsnQu.Items.Add(lvi);
                i++;
            }
            if (LvBoxsnQu.Items.Count <= 0)
                txtBox1.Focus();
            else
                LvBoxsnQu.Focus();

        }
        private void butOk_Click(object sender, EventArgs e)
        {
            LoadBox();
        }

        private void LoadFile()
        {
            butLoad.Enabled = false;
            try {
                if (Archid2 <= 0) {
                    MessageBox.Show("ID获取失败请重新选择案卷!");
                    return;
                }
                int ArchState = Common.GetArchCheckState(Archid2);
                if (ArchState != 1) {
                    MessageBox.Show("此卷档案未质检无法进行查阅！");
                    return;
                }
                string FileName = FileNameTmp2;
                string localPath = Path.Combine(Common.LocalTempPath, FileName.Substring(0, 8));
                string localCheckFile = Path.Combine(Common.LocalTempPath, FileName.Substring(0, 8), FileName);
                try {
                    if (!Directory.Exists(localPath)) {
                        Directory.CreateDirectory(localPath);
                    }

                    if (File.Exists(localCheckFile)) {
                        File.Delete(localCheckFile);
                    }
                } catch {
                }
                string filjpg = Path.Combine(Common.ArchSavePah, FileName.Substring(0, 8), FileName);
                if (ftp.FtpCheckFile(filjpg)) {
                    if (ftp.DownLoadFile(Common.ArchSavePah, FileName.Substring(0, 8), localCheckFile, FileName)) {
                        imgBrow1.LoadFile(Archid2, localCheckFile);
                        return;
                    }
                }
                MessageBox.Show("警告，文件不存在!");
                return;

            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            } finally {
                butLoad.Enabled = true;
                imgBrow1.Focus();
            }
        }

        private void LvBoxsnQu_Click(object sender, EventArgs e)
        {
            if (LvBoxsnQu.Items.Count <= 0)
                return;
            if (LvBoxsnQu.SelectedItems.Count <= 0)
                return;
            ArchID = Convert.ToInt32(LvBoxsnQu.SelectedItems[0].SubItems[3].Text);
            FileNameTmp = LvBoxsnQu.SelectedItems[0].SubItems[4].Text;
            imgBrow1.LoadConten(ArchID);
            ucDLInfo1.LoadInfo2(ArchID,0);
            cleFile();
        }

        private void toolBut_Qian_Click(object sender, EventArgs e)
        {
            imgBrow1.PrivePage();
        }

        private void toolBut_hou_Click(object sender, EventArgs e)
        {
            if (CrrPage < MaxPage)
                imgBrow1.NextPage();
            else
            {
                FileNameTmp2= "";
                imgBrow1.Close();
                ucDLInfo1.Closedt();
                LvBoxsnQu.Focus();
            }
        }

        private void toolBut_sizeD_Click(object sender, EventArgs e)
        {
            imgBrow1.BigPage();
        }

        private void toolBut_sizeX_Click(object sender, EventArgs e)
        {
            imgBrow1.SmallPage();
        }

        private void toolBut_close_Click(object sender, EventArgs e)
        {
            imgBrow1.Close();
        }

        private void LvBoxsnQu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (LvBoxsnQu.Focused)
                    butLoad.Focus();
            }
        }

        private void FrmInfolook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
                toolBut_hou_Click(null,null);
        }

        void cleFile()
        {
            if(FileNameTmp2.Trim().Length<=0)
                return;
            imgBrow1.Close();
            FileNameTmp2 = "";
            Archid2 = 0;
        }

        private void butLoad_Click(object sender, EventArgs e)
        {
            cleFile();
            FileNameTmp2 = DESEncrypt.DesDecrypt(FileNameTmp);
            Archid2 = ArchID;
            if (Archid2 <= 0 || FileNameTmp2.Trim().Length <= 0) {
                MessageBox.Show("ID号或文件名称获取失败!");
                LvBoxsnQu.Focus();
                return;
            }
            LoadFile();
        }

        private void LvBoxsnQu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LvBoxsnQu_Click(null, null);
        }
    }
}
