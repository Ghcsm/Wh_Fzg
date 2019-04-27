using DAL;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLFtp;
using System.IO;
using HLjscom;

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
            int arid = Convert.ToInt32(dgData.Rows[0].Cells[1].Value);
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
                    string filepath = dgData.Rows[0].Cells[4].Value.ToString();
                    int stat = Convert.ToInt32(dgData.Rows[0].Cells[5].Value.ToString());
                    string pages = dgData.Rows[0].Cells[6].Value.ToString();
                    Task task = new Task(() =>
                    {
                        StatTask(typemodule, archid, archpos, filename, filepath, stat, pages);
                    });
                    task.Start();
                    task.Wait();
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
            this.toolStrip1.BeginInvoke(new Action(() =>
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

        private void StatTask(string typemodule, string archid, string archpos, string filename, string filepath, int archstat, string pages)
        {
            if (archstat == (int)T_ConFigure.ArchStat.扫描完) {
                if (ftp.SaveRemoteFileUp(T_ConFigure.gArchScanPath, archpos, filename, T_ConFigure.ScanTempFile)) {
                    Common.DelTask(Convert.ToInt32(archid));
                    Common.SetScanFinish(Convert.ToInt32(archid), Convert.ToInt32(pages), 1, archstat);
                    try {
                        File.Delete(filename);
                        Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                    } catch {
                    }
                    return;
                }
            }
            else if (archstat == (int)T_ConFigure.ArchStat.排序完) {

                string IndexFileName = Common.GetCurrentTime() + Common.TifExtension;
                string RemoteDir = IndexFileName.Substring(0, 8);
                string LocalIndexFile = Path.Combine(@T_ConFigure.LocalTempPath, IndexFileName);
                Task task = new Task(() => { Himg._OrderSave(filepath, LocalIndexFile); });
                task.Start();
                task.Wait();
                if (ftp.SaveRemoteFileUp(T_ConFigure.FtpArchIndex, RemoteDir, LocalIndexFile, IndexFileName)) {
                    Common.SetIndexFinish(Convert.ToInt32(archid), IndexFileName, archstat);
                    Common.DelTask(Convert.ToInt32(archid));
                    File.Delete(filepath);
                    File.Delete(LocalIndexFile);
                    Directory.Delete(Path.Combine(T_ConFigure.LocalTempPath, archpos));
                    if (ftp.KillRemotFile(T_ConFigure.gArchScanPath, archpos, T_ConFigure.ScanTempFile)) {
                        ftp.KillRemotDir(T_ConFigure.gArchScanPath, archpos);
                    }
                    return;
                }
            }
            else if (archstat == (int)T_ConFigure.ArchStat.质检完) {
                string RemoteDir = filename.Substring(0, 8);
                if (ftp.SaveRemoteFileUp(T_ConFigure.FtpArchSave, RemoteDir, filename, filepath)) {
                    Common.DelTask(Convert.ToInt32(archid));
                    Common.SetIndexFinish(Convert.ToInt32(archid), filename, archstat);
                    try {
                        File.Delete(filepath);
                        Directory.Delete(RemoteDir);
                    } catch { }

                }

            }
            //else if (archstat == (int)T_ConFigure.ArchStat.质检退回) {
            //    string RemoteDir = filename.Substring(0, 8);
            //    if (ftp.SaveRemoteFileUp(T_ConFigure.gArchScanPath, archpos, filepath, T_ConFigure.ScanTempFile)) {
            //        Common.DelTask(Convert.ToInt32(archid));
            //        Common.SetArchWorkState(Convert.ToInt32(archid), (int)T_ConFigure.ArchStat.质检退回);
            //        try {
            //            File.Delete(filepath);
            //            Directory.Delete(RemoteDir);
            //        } catch { }

            //    }
            //}
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            GetTask();
        }
    }
}
