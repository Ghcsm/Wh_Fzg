using DAL;
using HLFtp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmxtbf
{
    public partial class FrmBack : Form
    {
        public FrmBack()
        {
            InitializeComponent();
        }

        HFTP ftp = new HFTP();

        #region 基本信息验证


        private bool istxt()
        {
            if (cBoxKf.Text.Length <= 0) {
                MessageBox.Show("请选择相关库房");
                cBoxKf.Focus();
                return false;
            }

            if (!rButSjk.Checked && !rButImg.Checked) {
                MessageBox.Show("请选择备份类型!");
                return false;
            }
            else if (rButImg.Checked) {
                if (!rButCy.Checked && !rButBak.Checked) {
                    MessageBox.Show("请选择备份方式!");
                    return false;
                }
                if (!rButKf.Checked && !rButBox.Checked) {
                    MessageBox.Show("请选择备份范围!");
                    return false;
                }
                else if (rButBox.Checked) {
                    if (!isNUm()) {
                        MessageBox.Show("请输入正确盒号范围!");
                        txtBox1.Focus();
                        return false;
                    }
                }
                if (!rButFtp.Checked && !rButDiskPath.Checked)
                    return false;
                else if (rButDiskPath.Checked) {
                    if (txtDiskPath.Text.Trim().Length <= 0) {
                        MessageBox.Show("请选择图像源路径!");
                        txtDiskPath.Focus();
                        return false;
                    }
                }
            }
            if (txtBackPath.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择备份路径!");
                txtBackPath.Focus();
                return false;
            }
            return true;
        }

        private bool isNUm()
        {
            try {
                if (txtBox1.Text.Trim().Length <= 0 || txtBox2.Text.Trim().Length <= 0)
                    return false;
                int a = int.Parse(txtBox1.Text.Trim());
                int b = int.Parse(txtBox2.Text.Trim());
                if (a == 0 || a < b)
                    return false;
                return true;

            } catch {
                return false;
            }
        }


        #endregion

        #region 初始化基本信息

        private void InIHouse()
        {

            List<V_HouseSet> HouseEc = new List<V_HouseSet>();
            DataTable dt = T_Sysset.GetHouseName();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                V_HouseSet House = new V_HouseSet();
                House.HouseID = Convert.ToInt32(dr["id"].ToString());
                House.HouseName = dr["HOUSENAME"].ToString();
                HouseEc.Add(House);
            }
            cBoxKf.Items.Clear();
            cBoxKf.Items.AddRange(HouseEc.ToArray());
            cBoxKf.SelectedItem = HouseEc;
            cBoxKf.SelectedIndex = 0;

        }

        private void FrmBack_Shown(object sender, EventArgs e)
        {
            ftp.PercentChane += new HLFtp.HFTP.PChangedHandle(Downjd);
            toolProBar.Size = new Size(Striptool.Width - 20, 16);
            InIHouse();
        }

        private void Downjd(object sender, PChangeEventArgs e)
        {
            this.Striptool.BeginInvoke(new Action(() =>
            {
                this.toolProBar.Visible = true;
                this.toolProBar.Minimum = 0;
                this.toolProBar.Maximum = (int)e.CountSize;
                this.toolProBar.Value = (int)e.TmpSize;
                if (e.CountSize == e.TmpSize) {
                    this.toolProBar.Visible = false;
                }
            }));
        }

        private void butSeletPath_Click(object sender, EventArgs e)
        {
            if (FbdPath.ShowDialog() == DialogResult.OK)
                txtBackPath.Text = FbdPath.SelectedPath;
            else txtBackPath.Text = "";
        }
        private void butArchSave_Click(object sender, EventArgs e)
        {
            if (rButDiskPath.Checked) {
                if (FbdPath.ShowDialog() == DialogResult.OK)
                    txtDiskPath.Text = FbdPath.SelectedPath;
                else txtDiskPath.Text = "";
            }
        }

        private void rButFtp_Click(object sender, EventArgs e)
        {
            txtDiskPath.Text = "";
        }
        #endregion


        private void butStart_Click(object sender, EventArgs e)
        {
            groupPanel1.Enabled = false;
            try {
                if (!istxt())
                    return;
                if (rButSjk.Checked) {
                    Exportsjk();
                    return;
                }
                BackImg();
            } catch (Exception ex) {
                Writelog(ex.ToString());
            }
        }


        private void Exportsjk()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            labxx1.Text = "正在备份...";
            Application.DoEvents();
            string n = DateTime.Now.ToString("yyyyMMddhhmm") + ".bak";
            string f = Path.Combine(txtBackPath.Text.Trim(), n);
            if (rButBak.Checked)
            {
                if (File.Exists(f))
                    File.Delete(f);
            }
            T_Sysset.BackSql(f);
            stopwatch.Stop();
            labxx1.Text = "备份完成耗时：" + stopwatch.Elapsed.TotalSeconds;
            labxx2.Text = "备份完整路径：" + f;
        }



        private void BackFile(string f, string b, string j)
        {
            try {
                string dir = f.Substring(0, 8);
                string Mdir = Path.Combine(txtBackPath.Text.Trim(), dir);
                string Mfile = Path.Combine(Mdir, f);
                if (File.Exists(Mfile))
                {
                    if (rButBak.Checked)
                        File.Delete(Mfile);
                    else return;
                }
                if (!rButFtp.Checked) {
                    string f1 = Path.Combine(txtDiskPath.Text.Trim(), dir, f);
                    if (!File.Exists(f1)) {
                        string s = string.Format("未找到源路径中图像文件,盒号{0},卷号{1};", b, j);
                        Writelog(s);
                        return;
                    }
                    if (!Directory.Exists(Mdir)) {
                        Directory.CreateDirectory(Mdir);
                    }
                    File.Copy(f1, Mfile);
                    return;
                }
                if (!ftp.CheckRemoteFile("ArchSave", f.Substring(0, 8), f)) {
                    string s = string.Format("使用Ftp传输时查找文件失败！第{0}盒,第{1}卷", b, j);
                    Writelog(s);
                    return;
                }
                if (!ftp.DownLoadFile("ArchSave", f.Substring(0, 8), Mfile, f)) {
                    string s = string.Format("使用Ftp传输时下载文件失败! 第{0}盒,第{1}卷", b, j);
                    Writelog(s);
                    return;
                }

            } catch (Exception e) {
                string s = string.Format(e + "第{0}盒,第{1}卷", b, j);
                Writelog(s);
            }

        }
        private void BackImg()
        {
            Task.Run(() =>
            {
                try {
                    DataTable dt = Common.GetBoxsnSql(txtBox1.Text.Trim(), txtBox2.Text.Trim(), rButCy.Checked);
                    if (dt == null || dt.Rows.Count <= 0) {
                        string s = "未发现要备份数据！";
                        Writelog(s);
                        return;
                    }
                    string b = string.Empty;
                    string j = string.Empty;
                    string f = string.Empty;
                    labxx1.Text = string.Format("共计 {0} 个文件", dt.Rows.Count);
                    if (rButFtp.Checked)
                        toolProBar.Visible = true;
                    else
                        toolProBar.Visible = false;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        i = 0;
                        b = dt.Rows[0][1].ToString();
                        j = dt.Rows[0][2].ToString();
                        f = dt.Rows[0][3].ToString();
                        if (f == null || f.Trim().Length <= 0) {
                            string s = string.Format("数据库中文件名长度不正确,第{0}盒,第{1}卷;", b, j);
                            Writelog(s);
                            dt.Rows.RemoveAt(0);
                            continue;
                        }
                        BackFile(f, b, j);
                        dt.Rows.RemoveAt(0);
                    }
                    this.BeginInvoke(new Action(() =>
                    {
                        labxx2.Text = string.Format("正在备份第{0} 盒 ,第{1}卷", b, j);
                        labxx3.Text = string.Format("剩余 {0} 个文件", dt.Rows.Count);
                    }));

                } catch (Exception e) {
                    Writelog(e.ToString());
                } finally {
                    groupPanel1.Enabled = true;
                    Striptool.Visible = false;
                }
            });
        }



        private void Writelog(string str)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string bf = "备份数据错误 ： ";
            string dt = DateTime.Now.ToString();
            try {
                string file = "log.txt";
                string filepath = Path.Combine(Application.StartupPath, file);
                if (!File.Exists(filepath)) {
                    fs = new FileStream(filepath, FileMode.Create);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(bf + str + " 操作时间 " + dt);
                    sw.Flush();
                }
                else {
                    fs = new FileStream(filepath, FileMode.Append);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(bf + str + " 操作时间 " + dt);
                    sw.Flush();
                }
            } catch { } finally {
                sw.Close();
                fs.Close();
            }

        }

        private void butLog_Click(object sender, EventArgs e)
        {
            string file = "log.txt";
            string filepath = Path.Combine(Application.StartupPath, file);
            if (!File.Exists(filepath)) {
                MessageBox.Show("日志文件不存在！");
            }
            else {
                Process p = Process.Start(filepath);
            }
        }


    }
}
