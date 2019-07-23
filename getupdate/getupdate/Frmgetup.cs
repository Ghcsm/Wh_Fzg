using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Threading;

namespace getupdate
{
    public partial class Frmgetup : Form
    {
        public Frmgetup()
        {
            InitializeComponent();
        }
        getupdate.HFTP ftp = new getupdate.HFTP();

        public static int id = 0;
        public static string ftppath = "";
        private void Downjd(object sender, getupdate.PChangeEventArgs e)
        {
            this.Tools_jd.Visible = true;
            this.Tools_jd.Minimum = 0;
            this.Tools_jd.Maximum = (int)e.CountSize;
            Application.DoEvents();
            this.Tools_jd.Value = (int)e.TmpSize;
            if (e.CountSize == e.TmpSize) {
                this.Tools_jd.Visible = false;
            }
        }
        private void Killproess()
        {
            try {
                if (id == 0) {
                    MessageBox.Show("获取ftp失败无法更新！");
                    Application.Exit();
                    return;
                }
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes) {
                    if (p.ProcessName == "bgkj") {
                        p.Kill();
                        Application.DoEvents();
                    }
                }
                Thread.Sleep(200);
                foreach (Process p in processes) {
                    if (p.ProcessName == "bgkj") {
                        p.Kill();
                        Application.DoEvents();
                    }
                }
                ftpUpdate();
            } catch {
                MessageBox.Show("关闭程序失败无法完成更新！");
                Application.Exit();
                return;
            }
        }


        private void ftpUpdate()
        {
            try {
                string str = null;
                string localF = Application.StartupPath;
                string[] GetList = ftp.GetFileList(ftppath);
                for (int i = 0; i < GetList.Length; i++) {
                    string Filename = GetList[i];
                    string Localfile = localF + "\\" + Filename;
                    if (Filename != "getupdate") {
                        if (ftp.DownLoadFile(ftppath, str, Localfile, Filename)) { }
                    }
                    Application.DoEvents();
                }
            } catch  {
                
            } finally {
                try {
                    string appName = Application.StartupPath + "\\" + "bgkj.exe";
                    Process.Start(appName);
                    Application.Exit();
                } catch {
                    Application.Exit();
                }
            }
        }

        public static void getxml()
        {
            try {
                string file = Application.StartupPath + "\\" + "bgkj.exe.config";
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = file;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                SQLHelper.connstr = Des.DesDecrypt(config.ConnectionStrings.ConnectionStrings["ConStr"].ConnectionString);

            } catch {
                MessageBox.Show("读取配置文件错误,或无配置信息，请写入新的配置信息!");
                Application.Exit();
            }
        }



        private void Frmgetup_Load(object sender, EventArgs e)
        {
            ftp.PercentChane += new HFTP.PChangedHandle(Downjd);
        }

        private void Frmgetup_Shown(object sender, EventArgs e)
        {
            Killproess();
        }
    }
}
