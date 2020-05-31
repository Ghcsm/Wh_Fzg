using DAL;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLFtp;
using System.IO;
using HLjscom;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Jdshow
{
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }
        HFTP ftp = new HFTP();
        Hljsimage Himg = new Hljsimage();
        private int stop;
        private void FrmTask_Shown(object sender, EventArgs e)
        {
            ftp.PercentChane += new HLFtp.HFTP.PChangedHandle(Downjd);
            Task.Run(new Action(() => { GetTask(); }));
        }

        private void isUser()
        {
            if (T_User.LoginName == "Admin")
                butDel.Visible = true;
            else
                butDel.Visible = false;
        }

        private void GetTask()
        {
            DataTable dt = Common.GetTask();
            dgData.DataSource = null;
            if (dt != null && dt.Rows.Count > 0) {
                this.dgData.BeginInvoke(new Action(() =>
                {
                    dgData.DataSource = dt;
                }));
            }
        }

        private void FrmTask_Load(object sender, EventArgs e)
        {
            isUser();
        }

        private void DelTask()
        {
            int id = dgData.CurrentRow.Index;
            int arid = Convert.ToInt32(dgData.SelectedRows[id].Cells[1].Value);
            if (arid <= 0)
                return;
            Common.DelTask(arid);
            GetTask();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (dgData.Rows.Count <= 0)
                return;
            if (dgData.SelectedRows.Count <= 0)
                return;
            if (MessageBox.Show("您确定要删除当前未上传成功的任务吗？", "警告", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK) {
                DelTask();
            }
        }

        private void istxt(int id)
        {
            if (id == 0) {
                stop = 0;
                butStart.Text = "暂停";
                butDel.Enabled = false;
                labjd.Visible = true;
                pbgUpdata.Visible = true;
            }
            else {
                butStart.Text = "开始";
                butDel.Enabled = true;
                labjd.Visible = false;
                pbgUpdata.Visible = false;
                stop = 0;
            }
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            istxt(0);
            try {
                if (stop == 0)
                    stop = 1;
                else
                    stop = 0;
                for (int i = 0; i < dgData.Rows.Count; i++) {
                    if (stop == 0)
                        return;
                    string typemodule = dgData.Rows[0].Cells[0].Value.ToString();
                    string archid = dgData.Rows[0].Cells[1].Value.ToString();
                    string archpos = dgData.Rows[0].Cells[2].Value.ToString();
                    string filename = dgData.Rows[0].Cells[3].Value.ToString();
                    string filepath = dgData.Rows[0].Cells[5].Value.ToString();
                    int stat = Convert.ToInt32(dgData.Rows[0].Cells[4].Value.ToString());
                    string pages = dgData.Rows[0].Cells[6].Value.ToString();
                    int tagpage = Convert.ToInt32(dgData.Rows[0].Cells[7].Value.ToString());
                    if (!File.Exists(filepath)) {
                        // MessageBox.Show("ID号:" + archid + ",盒号卷号：" + archpos + ",文件不存在!");
                        Common.DelTask(Convert.ToInt32(archid));
                        dgData.Rows.RemoveAt(0);
                        continue;
                    }
                    Task task = new Task(() =>
                    {
                        StatTask(typemodule, archid, archpos, filename, filepath, stat, pages, tagpage);
                    });
                    task.Start();
                    dgData.Rows.RemoveAt(0);
                    i = 0;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            } finally {
                istxt(1);
            }
        }


        private void Downjd(object sender, PChangeEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.pbgUpdata.Visible = true;
                this.pbgUpdata.Minimum = 0;
                this.pbgUpdata.Maximum = (int)e.CountSize;
                this.pbgUpdata.Value = (int)e.TmpSize;
                if (e.CountSize == e.TmpSize) {
                    this.pbgUpdata.Visible = false;
                }
            }));
        }

        private void StatTask(string typemodule, string archid, string archpos, string filename, string filepath, int archstat, string pages, int tagpage)
        {
            if (archstat <= (int)T_ConFigure.ArchStat.扫描完) {
                if (ftp.SaveRemoteFileUp(T_ConFigure.gArchScanPath, archpos, filepath, filename)) {
                    Common.DelTask(Convert.ToInt32(archid));
                    Common.SetScanFinish(Convert.ToInt32(archid), Convert.ToInt32(pages), 1, archstat);
                    try {
                        File.Delete(filepath);
                        Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                    } catch {
                    }
                    return;
                }
            }
            else if (archstat <= (int)T_ConFigure.ArchStat.排序完) {
                string pageinfo = "";
                string[] Pagetmp;
                Dictionary<int, string> abc = new Dictionary<int, string>();
                List<int> userid = new List<int>();
                List<int> A0 = new List<int>();
                List<int> A1 = new List<int>();
                List<int> A2 = new List<int>();
                List<int> A3 = new List<int>();
                List<int> A4 = new List<int>();
                ReadDict(Convert.ToInt32(archid), out abc, out Pagetmp);
                string time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string IndexFileName = time + Common.TifExtension;
                string RemoteDir = IndexFileName.Substring(0, 8);
                string LocalIndexFile = Path.Combine(@T_ConFigure.LocalTempPath, IndexFileName);
                if (!Himg._OrderSave(tagpage, Convert.ToInt32(pages), filepath, LocalIndexFile, abc, out pageinfo, out userid, out A0, out A1, out A2, out A3, out A4, Pagetmp)) {
                    return;
                }
                else {
                    int p0 = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0;
                    for (int i = 0; i < userid.Count; i++) {
                        int uid = userid[i];
                        int a0 = A0[i];
                        int a1 = A1[i];
                        int a2 = A2[i];
                        int a3 = A3[i];
                        int a4 = A4[i];
                        p0 += a0;
                        p1 += a1;
                        p2 += a2;
                        p3 += a3;
                        p4 += a4;
                        Common.SetScanPage(Convert.ToInt32(archid), a0, a1, a2, a3, a4, uid);
                    }
                    Common.UpdaeImgPage(Convert.ToInt32(archid), p0, p1, p2, p3, p4);
                }
                if (ftp.SaveRemoteFileUp(T_ConFigure.FtpArchIndex, RemoteDir, LocalIndexFile, IndexFileName)) {
                    Common.SetIndexFinish(Convert.ToInt32(archid), DESEncrypt.DesEncrypt(IndexFileName), archstat, pageinfo);
                    Common.DelTask(Convert.ToInt32(archid));
                    try {
                        File.Delete(filepath);
                        File.Delete(LocalIndexFile);
                        Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                        string file = Path.Combine(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile);
                        if (ftp.FtpCheckFile(file)) {
                            ftp.FtpDelFile(file);
                        }
                    } catch { }
                    return;
                }
            }
            else if (archstat == (int)T_ConFigure.ArchStat.质检完) {
                string RemoteDir = filename.Substring(0, 8);
                if (ftp.SaveRemoteFileUp(T_ConFigure.FtpArchSave, RemoteDir, filepath, filename)) {
                    Common.DelTask(Convert.ToInt32(archid));
                    Common.SetCheckFinish(Convert.ToInt32(archid), DESEncrypt.DesEncrypt(filename), 1, (int)T_ConFigure.ArchStat.质检完);
                    try {
                        File.Delete(filepath);
                        Directory.Delete(Path.GetDirectoryName(filepath));
                    } catch { }

                }

            }
        }


        public void ReadDict(int arid, out Dictionary<int, string> abc, out string[] Pagetmp)
        {
            abc = new Dictionary<int, string>();
            Pagetmp = new[] {"", "", ""};
            DataTable dt = Common.ReadPageIndexInfo(arid);
            if (dt != null && dt.Rows.Count > 0) {
                DataRow dr = dt.Rows[0];
                string PageIndexInfo = dr["PageIndexInfo"].ToString();
                string Page = dr["ArchPage"].ToString();
                if (!string.IsNullOrEmpty(PageIndexInfo)) {
                    string[] arrPage = PageIndexInfo.Split(';');
                    if (arrPage.Length > 0) {
                        for (int i = 0; i < arrPage.Length; i++) {
                            int id = i + 1;
                            abc.Add(id, arrPage[i].ToString());
                        }
                    }
                }
                if (Page.Trim().Length > 0) {
                    Pagetmp = Page.Split('-');
                }
            }
        }
        private bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }


        private void butUpdate_Click(object sender, EventArgs e)
        {
            GetTask();
        }
    }
}
