using CsmCon;
using DAL;
using HLFtp;
using HLjscom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CsmOcr
{
    public partial class FrmOcr : Form
    {
        public FrmOcr()
        {
            InitializeComponent();
        }

        private gArchSelect gArch;
        HFTP ftp = new HFTP();
        int Archid;
        string FileNametmp = "";
        private Hljsimage Himg;
        private List<string> lsConten = new List<string>();
        private List<string> lsContenlx = new List<string>();
        private List<string> lsocrconte = new List<string>();
        private void Init()
        {
            try {
                gArch = new gArchSelect();
                gArch.GotoPages = true;
                gArch.LoadFileBoole = true;
                gArch.Dock = DockStyle.Fill;
                gArch.butLoad.Enabled = false;
                gr1.Controls.Add(gArch);
            } catch { }
        }

        private void FrmOcr_Load(object sender, EventArgs e)
        {
            Init();
            ftp.PercentChane += new HLFtp.HFTP.PChangedHandle(Downjd);
        }

        private void Downjd(object sender, PChangeEventArgs e)
        {
            this.toolStrip1.Invoke(new Action(() =>
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


        private int Getywid(string str)
        {
            if (lsocrconte.Count <= 0)
                return 1;
            int id = lsocrconte.FindAll((ex) => { return ex == str; }).Count;
            if (id < 0)
                return 1;
            return id;
        }

        private void Addconten(string arid, string conten, string contenlx, string page, string ywid)
        {
            try {
                Common.ContenAddocr(arid, conten, contenlx, page, ywid);
            } catch { }
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

        private bool LoadFileImg(string filetmp)
        {
            try {
                if (T_ConFigure.FtpStyle == 1) {
                    string sourefile = "";
                    string goalfile = "";
                    string path = "";
                    string localPath = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpSave, filetmp.Substring(0, 8));
                    string localScanFile = Path.Combine(T_ConFigure.FtpTmpPath, T_ConFigure.TmpSave, filetmp.Substring(0, 8),
                          FileNametmp);
                    FileNametmp = localScanFile;
                    if (!Directory.Exists(localPath)) {
                        Directory.CreateDirectory(localPath);
                    }
                    if (File.Exists(localScanFile)) {
                        File.Delete(localScanFile);
                    }
                    sourefile = Path.Combine(T_ConFigure.FtpArchIndex, filetmp.Substring(0, 8), filetmp);
                    goalfile = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave, filetmp.Substring(0, 8), filetmp);
                    path = Path.Combine(T_ConFigure.FtpTmp, T_ConFigure.TmpSave, filetmp.Substring(0, 8));
                    sourefile = Path.Combine(T_ConFigure.FtpArchIndex, filetmp.Substring(0, 8), filetmp);
                    if (ftp.FtpCheckFile(sourefile)) {
                        if (ftp.FtpMoveFile(sourefile, goalfile, path))
                            return (FileMoveBool(localScanFile));
                    }
                }
                else {
                    string localPath = Path.Combine(T_ConFigure.LocalTempPath, filetmp.Substring(0, 8));
                    string localScanFile = Path.Combine(T_ConFigure.LocalTempPath, filetmp.Substring(0, 8),
                        filetmp);
                    FileNametmp = localScanFile;
                    if (!Directory.Exists(localPath)) {
                        Directory.CreateDirectory(localPath);
                    }
                    if (File.Exists(localScanFile)) {
                        File.Delete(localScanFile);
                    }
                    if (ftp.FtpCheckFile(Path.Combine(T_ConFigure.FtpArchIndex, filetmp.Substring(0, 8), filetmp))) {
                        if (ftp.DownLoadFile(T_ConFigure.FtpArchIndex, filetmp.Substring(0, 8),
                            localScanFile,
                            filetmp)) {
                            return true;
                        }
                        return false;
                    }
                }
                return false;

            } catch {
                return false;
            }
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

        void LoadModule()
        {
            lsConten.Clear();
            lsContenlx.Clear();
            DataTable dt = Common.GetcontenModule();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                ListViewItem lvi = new ListViewItem();
                string title = dr["TITLE"].ToString();
                string titlelx = dr["TitleLx"].ToString();
                if (title.Trim().Length <= 0)
                    continue;
                lsConten.Add(title);
                lsContenlx.Add(titlelx);

            }
        }

        void LoadTaskW()
        {
            datGrivew.DataSource = null;
            toolslabTaskCount.Text = "共计:0条";
            DataTable dt = Common.GetOcrTask();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            datGrivew.DataSource = dt;
            toolslabTaskCount.Text = string.Format("共计{0}条", datGrivew.RowCount.ToString());
        }

        void LoadTaskWAll()
        {
            datGrivew.DataSource = null;
            toolslabTaskCount.Text = "共计:0条";
            DataTable dt = Common.GetOcrTaskAll();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            datGrivew.DataSource = dt;
            toolslabTaskCount.Text = string.Format("共计{0}条", datGrivew.RowCount.ToString());
        }

        private void rabWzx_Click(object sender, EventArgs e)
        {
            LoadTaskW();
        }

        private void rabAllTabk_Click(object sender, EventArgs e)
        {
            LoadTaskWAll();
        }

        private void FrmOcr_Shown(object sender, EventArgs e)
        {
            Himg = new Hljsimage();
            combOcr.SelectedIndex = 0;
            Task.Run(new Action(() => { LoadModule(); }));
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            butStart.Enabled = false;
            int ocr = combOcr.SelectedIndex;
            Action Act = StatTask;
            Act.BeginInvoke(null, null);
        }

        void StatTask()
        {
            Task t = null;
          
            if (rabZdTabk.Checked) {
                Archid = gArch.Archid;
                if (Archid <= 0) {
                    MessageBox.Show("请选择档案!");
                    return;
                }
                string filetmp = gArch.ArchImgFile;
                if (filetmp.Trim().Length <= 0) {
                    MessageBox.Show("文件名称长度错误!");
                    return;
                }
                if (Common.Gettask(Archid) > 0) {
                    MessageBox.Show("此卷图像任务中请稍候!");
                    return;
                }
                int stat = Common.GetArchWorkState(Archid);
                if (stat >= (int)T_ConFigure.ArchStat.质检中) {
                    MessageBox.Show("此卷档案正在质检中或已质检完成！");
                    return;
                }
                toolsTaskzx.Text = "正在执行:";
                t = Task.Run(new Action(() => { OcrTask(filetmp, ocr); }));
            }
            else if (rabWzx.Checked || rabAllTabk.Checked) {
                if (datGrivew.RowCount <= 0) {
                    MessageBox.Show("任务池中未发现任务");
                    return;
                }
                if (datGrivew.SelectedRows.Count > 0) {
                    int id = datGrivew.CurrentRow.Index;
                    string file = GettaskFilename(id);
                    if (file.Trim().Length <= 0) {
                        MessageBox.Show("文件名不正确!");
                        return;
                    }
                    toolsTaskzx.Text = "正在执行:";
                    t = Task.Run(new Action(() => { OcrTask(file, ocr); }));
                }
                else {
                    toolsTaskzx.Visible = true;
                    for (int i = 0; i < datGrivew.RowCount; i++) {
                        string file = GettaskFilename(i);
                        if (file.Trim().Length <= 0) {
                            continue;
                        }
                        toolsTaskzx.Text = "正在执行:" + i.ToString();
                        t = Task.Run(new Action(() => { OcrTask(file, ocr); }));
                    }
                }
            }
            Task.WaitAll(t);
            toolsTaskzx.Visible = false;
            butStart.Enabled = true;
        }

        string GettaskFilename(int id)
        {
            try {
                if (datGrivew.Rows[id].Cells[1].Value == null)
                    return "";
                string file = datGrivew.Rows[id].Cells[1].Value.ToString();
                file = DESEncrypt.DesDecrypt(file);
                string boxsn = datGrivew.Rows[id].Cells[2].ToString();
                string archno = datGrivew.Rows[id].Cells[3].ToString();
                if (file.Trim().Length <= 0)
                    return "";
                return file;
            } catch {
                return "";
            }

        }

        void OcrTask(string file, int ocr)
        {
            if (!LoadFileImg(file))
                return;
            lsocrconte.Clear();
            List<string> str = new List<string>();
            List<string> pages = new List<string>();
            Himg._OcrRecttxt(FileNametmp, out str, out pages, ocr);
            if (str.Count <= 0)
                return;
            for (int i = 0; i < str.Count; i++) {
                string strtmp = str[i];
                strtmp = RegexCh(strtmp);
                for (int t = 0; t < lsConten.Count; t++) {
                    string s = lsConten[t];
                    if (strtmp.Contains(s)) {
                        lsocrconte.Add(s);
                        string lx = lsContenlx[t];
                        string ywid = Getywid(s).ToString();
                        Addconten(Archid.ToString(), s, lx, i.ToString(), ywid);
                        break;
                    }
                }
            }
            try {
                if (File.Exists(FileNametmp))
                    File.Delete(FileNametmp);

            } catch {
            }
        }
    }
}
