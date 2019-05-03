using CsmCon;
using DAL;
using HLFtp;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Csmdacx
{
    public partial class FrmQuery : Form
    {
        public FrmQuery()
        {
            InitializeComponent();

            Init();
        }

        HFTP ftp = null;
        private ImgBrow imgBrow1;
        private void Init()
        {
            ImgBrow.ContentsEnabled = false;
            ImgBrow.ModuleVisible = false;
            ImgBrow.id = 0;
            imgBrow1 = new ImgBrow();
            imgBrow1.Spage += new ImgBrow.TransmitPar(ShowPage);
            imgBrow1.Dock = DockStyle.Fill;
            
            gr2.Controls.Add(imgBrow1);
        }

        private bool isTxt()
        {
            if (comboField.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询字段!");
                comboField.Focus();
                return false;
            }

            if (comboOper.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询操作符!");
                comboField.Focus();
                return false;
            }

            if (txtKey.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入查询关键字!");
                txtKey.Focus();
                return false;
            }
            return true;
        }

        private void QuerySql()
        {
            if (!isTxt())
                return;
            lvData.Items.Clear();
            try {
                string strOper = comboField.Text.Trim();
                string strfield = comboOper.Text.Trim();
                string strtxtkey = txtKey.Text.Trim();
                DataTable dt = Common.QueryData(strOper, strfield, strtxtkey);
                int i = 1;
                if (dt != null && dt.Rows.Count > 0) {
                    foreach (DataRow dr in dt.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        for (int t = 0; t < ClsQuerInfo.QuerTableList.Count; t++) {
                            string col = ClsQuerInfo.QuerTableList[t];
                            string str = dr[col].ToString();
                            lvi.SubItems.AddRange(new string[] { str });
                        }
                        string etag = dr["EnterTag"].ToString();
                        string arid = dr["Archid"].ToString();
                        lvi.SubItems.AddRange(new string[] { etag, arid });
                        lvData.Items.Add(lvi);
                        i++;
                    }

                }
            } catch (Exception ex) {
                MessageBox.Show("查询失败:" + ex.ToString());
            }
        }

        private void butSql_Click(object sender, EventArgs e)
        {
            QuerySql();
        }

        private void LoadFile()
        {
            try {
                if (ClsQuery.ArchID <= 0) {
                    MessageBox.Show("ID获取失败请重新选择案卷!");
                    return;
                }
                if (ClsQuery.FileNameTmp.Trim().Length <= 0) {
                    MessageBox.Show("文件名称获取失败!");
                    return;
                }
                int ArchState = Common.GetArchWorkState(ClsQuery.ArchID);
                if (ArchState < (int)Common.档案状态.质检完) {
                    MessageBox.Show("此卷档案未质检无法进行查阅！");
                    return;
                }
                string FileName = ClsQuery.FileNameTmp;
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
                            ImgBrow.Print = ClsQuery.Imgsys;
                            imgBrow1.LoadFile(ClsQuery.ArchID, localCheckFile);
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

        private void GetQuerCol()
        {
            comboField.Items.Clear();
            Common.GetQuerInfo();
            if (ClsQuerInfo.QuerTableList.Count <= 0)
                return;
            for (int i = 0; i < ClsQuerInfo.QuerTableList.Count; i++) {
                string str = ClsQuerInfo.QuerTableList[i];
                lvData.Columns.Add(str);
                comboField.Items.Add(str);
            }
            lvData.Columns.Add("EnterTag");
            lvData.Columns.Add("Archid");
        }

        private void FrmQuery_Shown(object sender, EventArgs e)
        {
            ftp = new HFTP();
            ftp.PercentChane += new HFTP.PChangedHandle(Downjd);
            GetQuerCol();
            Getsys();
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

        private void butOk_Click(object sender, EventArgs e)
        {
            if (lvData.SelectedItems != null && lvData.SelectedItems.Count > 0) {
                if (imgBrow1.ImgNull() == false)
                    LoadFile();
                else {
                    MessageBox.Show("请先关闭正在查看的案卷!");
                    return;
                }
            }

        }

        private void GetArchInfo()
        {
            int id = ClsQuerInfo.QuerTableList.Count;
            ClsQuery.ArchID = Convert.ToInt32(lvData.SelectedItems[0].SubItems[id + 2].Text);
            ClsQuery.Etag = lvData.SelectedItems[0].SubItems[id + 1].Text;
            if (ClsQuery.Etag == "1")
                ClsQuery.Etag = "一录";
            else ClsQuery.Etag = "二录";
            labEtag.Text = string.Format("录入:{0}", ClsQuery.Etag);
            DataTable dt = Common.QuerboxsnInfo(ClsQuery.ArchID);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ClsQuery.Boxsn = Convert.ToInt32(dt.Rows[0][0].ToString());
            ClsQuery.Archno = Convert.ToInt32(dt.Rows[0][1].ToString());
            ClsQuery.FileNameTmp = dt.Rows[0][2].ToString();
            string ys = dt.Rows[0][3].ToString();
            if (ys == "1")
                toolslabCheck.Text = "验收：完成";
            else if (ys == "2")
                toolslabCheck.Text = "验收：否决";
            else
                toolslabCheck.Text = "验收：未验收";
            labbox.Text = string.Format("盒号:{0}", ClsQuery.Boxsn);
            labjuan.Text = string.Format("卷号:{0}", ClsQuery.Archno);
            labfile.Text = string.Format("文件名称:{0}", ClsQuery.FileNameTmp);
        }

        private void GetUser()
        {
            string Scanner = string.Empty;
            string Indexer = string.Empty;
            string Checker = string.Empty;
            DataTable dt = Common.GetOperator(ClsQuery.ArchID);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            Scanner = dr["Scanner"].ToString();
            Indexer = dr["Indexer"].ToString();
            Checker = dr["Checker"].ToString();
            this.BeginInvoke(new Action(() =>
            {
                toolslab_scanuser.Text = string.Format("扫描：{0}", Scanner);
                toolslab_Indexuser.Text = string.Format("排序：{0}", Indexer);
                toolslab_Checkuser.Text = string.Format("质检：{0}", Checker);

            }));
        }

        private void ShowPage(int page, int counpage)
        {
            toolslab_PagesCount.Text = string.Format("共{0}页", counpage);
            toolslab_PagesCrre.Text = string.Format("第{0}页", page);
        }

        private void lvData_Click(object sender, EventArgs e)
        {
            if (lvData.SelectedItems != null && lvData.SelectedItems.Count > 0) {
                GetArchInfo();
                GetUser();
            }
        }

        private void toolStripButPrivePages_Click(object sender, EventArgs e)
        {
            imgBrow1.PrivePage();
        }

        private void toolStripButNextpage_Click(object sender, EventArgs e)
        {
            imgBrow1.NextPage();
        }

        private void toolStripButBigImg_Click(object sender, EventArgs e)
        {
            imgBrow1.BigPage();
        }

        private void toolStripButSizeImg_Click(object sender, EventArgs e)
        {
            imgBrow1.SmallPage();
        }

        private void toolStripButRoate_Click(object sender, EventArgs e)
        {
            imgBrow1.RotePage();
        }

        private void toolStripButPrint_Click(object sender, EventArgs e)
        {
            if (!ClsQuery.Imgsys) {
                MessageBox.Show("警告，您没有此项操作的权限！");
                return;
            }
            if (txtPages1.Text.Trim().Length <= 0 || txtPages2.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入打印范围!");
                txtPages1.Focus();
                return;
            }
            int p1 = Convert.ToInt32(txtPages1.Text.Trim());
            int p2 = Convert.ToInt32(txtPages2.Text.Trim());
            imgBrow1.PrintImg(p1, p2);
        }

        private void ImgClose()
        {
            imgBrow1.Close();
        }

        private void toolStripButClose_Click(object sender, EventArgs e)
        {
            ImgClose();
        }

        private void toolStripButTg_Click(object sender, EventArgs e)
        {
            if (imgBrow1.ImgNull() == true) {
                if (!ClsQuery.Imgys) {
                    MessageBox.Show("您没有权限验收图像!");
                    return;
                }
                Common.QuerSetCheckLog(ClsQuery.ArchID, ClsQuery.Boxsn, ClsQuery.Archno, 1, "查询->" + ClsQuery.Etag);
                MessageBox.Show("设置完成");
            }
        }

        private void toolStripButFj_Click(object sender, EventArgs e)
        {
            if (imgBrow1.ImgNull() == true) {
                if (!ClsQuery.Imgys) {
                    MessageBox.Show("您没有权限验收图像!");
                    return;
                }
                Common.QuerSetCheckLog(ClsQuery.ArchID, ClsQuery.Boxsn, ClsQuery.Archno, 2, "查询->" + ClsQuery.Etag);
                MessageBox.Show("设置完成");
            }
        }

        private void Getsys()
        {
            ClsQuery.Imgsys = false;
            ClsQuery.Imgys = false;
            DataTable dt = Common.GetOthersys();
            if (dt == null || dt.Rows.Count <= 0) {
                return;
            }
            string str = DESEncrypt.DesDecrypt(dt.Rows[0][0].ToString());
            if (str.Contains("图像打印"))
                ClsQuery.Imgsys = true;
            if (str.Contains("验收图像"))
                ClsQuery.Imgys = true;
        }

        private void FrmQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImgClose();
        }
    }
}
