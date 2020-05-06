using DAL;
using HLFtp;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsmImg;
using System.Data.SqlClient;
using System.Linq;
using HLjscom;

namespace Csmtool
{
    public partial class Frmtool : Form
    {
        public Frmtool()
        {
            InitializeComponent();
        }

        private HLFtp.HFTP ftp;
        Hljsimage Himg = new Hljsimage();

        #region Archstat

        private bool Istxt(int id)
        {
            int zd = 0;
            if (rabBoxsn.Checked) {
                if (txtBoxsn.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入盒号！");
                    txtBoxsn.Focus();
                    return false;
                }
                if (id > 0) {
                    if (txtBoxsn.Text.Trim().Length > 0 && txtArchno.Text.Trim().Length > 0)
                        zd = Common.GetArchCheckState(txtBoxsn.Text.Trim(), txtArchno.Text.Trim());
                }

            }
            if (rabArchid.Checked) {
                if (txtBoxsn.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入Archid号！");
                    txtBoxsn.Focus();
                    return false;
                }
                else if (id > 0)
                    zd = Common.GetArchCheckState(Convert.ToInt32(txtBoxsn.Text.Trim()));
            }
            if (rabArchcol.Checked) {
                if (txtBoxsn.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入Archid号！");
                    txtBoxsn.Focus();
                    return false;
                }
                else if (id > 0)
                    zd = Common.GetArchCheckState(txtBoxsn.Text.Trim());
            }
            if (id > 0) {
                if (chkAllstat.Checked || rabcleCheck.Checked || zd == 1) {
                    string str = DESEncrypt.DesEncrypt("质检状态");
                    if (T_User.UserOtherSys.IndexOf(str) < 0) {
                        MessageBox.Show("您没有清空质检状态的权限!");
                        return false;
                    }
                }
            }
            if (id == 0) {
                if (txtArchno.Text.Trim().Length <= 0) {
                    MessageBox.Show("卷号不能为空!");
                    txtArchno.Focus();
                    return false;
                }
            }
            return true;
        }

        private void combtjSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combtjSql.SelectedIndex != 4) {
                labcheck.Visible = false;
                labarchcount.Visible = false;
            }
            if (combtjSql.SelectedIndex == 5) {
                labImgpath.Visible = true;
                txtImgPath.Visible = true;
                butImgPath.Visible = true;
            }
            else {
                labImgpath.Visible = false;
                txtImgPath.Visible = false;
                butImgPath.Visible = false;
            }
        }
        void CleScanstat(int arid, string boxsn, string archno)
        {
            Common.ClearScanWrok(arid);
            string file = Path.Combine(T_ConFigure.gArchScanPath, boxsn + "-" + archno, T_ConFigure.ScanTempFile);
            if (ftp.FtpCheckFile(file)) {
                try {
                    ftp.FtpDelFile(file);
                } catch (Exception e) {
                    MessageBox.Show("清空图像失败:" + e.ToString());
                }
            }
            MessageBox.Show("操作完成!");
        }

        void Cleinfo(int arch, int id)
        {
            if (id == 1) {
                Common.ClearScanWrok(arch);
                Common.ClearInfoWrok(arch, 1);
                Common.ClearInfoWrok(arch, 2);
                Common.ClearInfoWrok(arch, 3);
                Common.CleaPagesinfo(arch);
                return;
            }
            if (rabcleInfobl.Checked)
                Common.ClearInfoWrok(arch, 1);
            else if (rabcleConten.Checked)
                Common.ClearInfoWrok(arch, 2);
            else if (rabcleCheck.Checked)
                Common.ClearInfoWrok(arch, 3);
            if (radclePage.Checked)
                Common.CleaPagesinfo(arch);
            MessageBox.Show("操作完成");
        }


        private void butStart_Click(object sender, EventArgs e)
        {
            if (!Istxt(1))
                return;
            DataTable dt;
            if (rabBoxsn.Checked)
                dt = Common.QueryBoxsn(txtBoxsn.Text.Trim(), txtArchno.Text.Trim());
            else if (rabArchid.Checked)
                dt = Common.QueryBoxsn(Convert.ToInt32(txtBoxsn.Text.Trim()));
            else
                dt = Common.QueryBoxsnid(txtBoxsn.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0) {
                MessageBox.Show("清空失败，未查到此卷信息!");
                txtBoxsn.Focus();
                return;
            }
            DataRow dr = dt.Rows[0];
            int arid = Convert.ToInt32(dr["id"].ToString());
            string boxsn = dr["boxsn"].ToString();
            string archno = dr["Archno"].ToString();
            if (chkAllstat.Checked) {
                CleScanstat(arid, boxsn, archno);
                Cleinfo(arid, 1);
                return;
            }
            if (rabcleScan.Checked)
                CleScanstat(arid, boxsn, archno);
            else if (radclePage.Checked)
                Cleinfo(arid, 0);
            else
                Cleinfo(arid, 0);

        }


        void Querinfo()
        {
            DataTable dt;
            if (rabBoxsn.Checked)
                dt = Common.QueryBoxsn(txtBoxsn.Text.Trim(), txtArchno.Text.Trim());
            else if (rabArchid.Checked)
                dt = Common.QueryBoxsn(Convert.ToInt32(txtBoxsn.Text.Trim()));
            else
                dt = Common.QueryBoxsnid(txtBoxsn.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0) {
                MessageBox.Show("未查到此卷信息!");
                txtBoxsn.Focus();
                labbox.Text = "";
                labarchno.Text = "";
                labarchid.Text = "";
                labArchcol.Text = "";
                return;
            }
            DataRow dr = dt.Rows[0];
            string arid = dr["id"].ToString();
            string boxsn = dr["boxsn"].ToString();
            string archno = dr["Archno"].ToString();
            string archcol = dr["ArchImportID"].ToString();
            labbox.Text = boxsn;
            labarchno.Text = archno;
            labarchid.Text = arid;
            labArchcol.Text = archcol;
        }

        private void butArchStat_Click(object sender, EventArgs e)
        {
            if (!Istxt(0))
                return;
            Querinfo();
        }

        private void rabBoxsn_CheckedChanged(object sender, EventArgs e)
        {
            if (rabBoxsn.Checked)
                txtArchno.Enabled = true;
            else txtArchno.Enabled = false;
            txtBoxsn.Focus();

        }
        #endregion

        #region Archcount

        bool istjtxt()
        {
            if (rabtjBoxsn.Checked) {
                if (txtTjBoxsn1.Text.Trim().Length <= 0 || txtTjBoxsn2.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入盒号范围!");
                    txtTjBoxsn1.Focus();
                    return false;
                }
            }
            if (rabtjCol.Checked) {
                if (txtTjBoxsn1.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入字段号范围!");
                    txtTjBoxsn1.Focus();
                    return false;
                }
            }
            if (combtjSql.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询条件!");
                combtjSql.Focus();
                return false;
            }

            if (combtjSql.SelectedIndex == 5) {
                if (txtImgPath.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择最终图像文件路径!");
                    return false;
                }
            }
            return true;
        }
        private void rabtjBoxsn_CheckedChanged(object sender, EventArgs e)
        {
            if (rabtjBoxsn.Checked)
                txtTjBoxsn2.Enabled = true;
            else txtTjBoxsn2.Enabled = false;
            txtTjBoxsn1.Focus();
        }
        private void butImgPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog flDialog = new FolderBrowserDialog();
            if (flDialog.ShowDialog() == DialogResult.OK)
                txtImgPath.Text = flDialog.SelectedPath;
            else
                txtImgPath.Text = "";
        }

        //检测档案单工序状态
        void Quersql()
        {
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetArchQuerstat(combtjSql.Text.Trim(), txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim(), "");
            else
                dt = Common.GetArchQuerstat(combtjSql.Text.Trim(), "", "", txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            dgvTjdata.DataSource = null;
            dgvTjdata.DataSource = dt;

        }

        //检测文件是否存在
        void QuerFile()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetArchQuerFile(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetArchQuerFile(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;

            for (int i = 0; i < dt.Rows.Count; i++) {
                string file = DESEncrypt.DesDecrypt(dt.Rows[i][0].ToString());
                if (file.Trim().Length <= 0)
                    continue;
                string boxsn = dt.Rows[i][1].ToString();
                string arno = dt.Rows[i][2].ToString();
                string pathfile = Path.Combine("archsave", file.Substring(0, 8), file);
                if (!ftp.FtpCheckFile(pathfile)) {
                    if (dgvTjdata.Rows.Count == 0) {
                        dgvTjdata.Columns.Add("file", "文件");
                        dgvTjdata.Columns.Add("boxsn", "盒号");
                        dgvTjdata.Columns.Add("archno", "卷号");
                    }
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = file;
                    dgvTjdata.Rows[index].Cells[1].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[2].Value = arno;

                }
                dgvTjdata.Refresh();
            }
        }

        //按区检测档案状态
        void QuerQuZt()
        {
            dgvTjdata.DataSource = null;
            labcheck.Visible = false;
            labarchcount.Visible = false;
            DataTable dt = null;
            if (rabtjCol.Checked)
                dt = Common.GetQuzt(txtTjBoxsn1.Text.Trim().PadLeft(4, '0'));
            else
                dt = Common.GetQuzt(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0) {
                MessageBox.Show("未查到相关信息!");
                return;
            }
            dgvTjdata.DataSource = dt;
            labarchcount.Visible = true;
            labarchcount.Text = string.Format("共{0}卷", dt.Rows.Count);
            DataTable dt1 = dt.Select("质检=1").CopyToDataTable();
            if (dt1 == null || dt.Rows.Count <= 0)
                return;
            labcheck.Visible = true;
            labcheck.Text = string.Format("质检{0}卷", dt1.Rows.Count);

        }
        //检测几手档案与业务id是否一致
        void CheckYwid()
        {
            dgvTjdata.DataSource = null;
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetArchYwid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetArchYwid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i][0].ToString());
                string boxsn = dt.Rows[0][1].ToString();
                string archo = dt.Rows[0][2].ToString();
                if (id <= 0)
                    continue;
                int entag = Common.GetEnterinfo(id);
                int conte = Common.Getconteninfo(id);
                if (entag != conte) {
                    if (dgvTjdata.Rows.Count == 0) {
                        dgvTjdata.Columns.Add("boxsn", "盒号");
                        dgvTjdata.Columns.Add("archno", "卷号");
                        dgvTjdata.Columns.Add("sx", "共几手");
                        dgvTjdata.Columns.Add("ywid", "业务id");
                    }
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[1].Value = archo;
                    dgvTjdata.Rows[index].Cells[2].Value = entag;
                    dgvTjdata.Rows[index].Cells[3].Value = conte;

                }
                dgvTjdata.Refresh();
            }

        }

        //检测图像页码是否不一致
        void CheckPage()
        {
            dgvTjdata.DataSource = null;
            dgvTjdata.Rows.Clear();
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetArchPage(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetArchPage(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;

            for (int i = 0; i < dt.Rows.Count; i++) {
                Application.DoEvents();
                string file = DESEncrypt.DesDecrypt(dt.Rows[i][0].ToString());
                string page = dt.Rows[i][1].ToString();
                string boxsn = dt.Rows[i][2].ToString();
                string arno = dt.Rows[i][3].ToString();
                if (file.Trim().Length <= 0)
                    continue;
                if (dgvTjdata.Columns.Count == 0) {
                    dgvTjdata.Columns.Add("boxsn", "盒号");
                    dgvTjdata.Columns.Add("archno", "卷号");
                    dgvTjdata.Columns.Add("imgpage", "图像页码");
                    dgvTjdata.Columns.Add("Page", "登记页码");
                }
                string pathfile = Path.Combine(txtImgPath.Text.Trim(), file.Substring(0, 8), file);
                if (!File.Exists(pathfile)) {
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[1].Value = arno;
                    dgvTjdata.Rows[index].Cells[2].Value = 0;
                    dgvTjdata.Rows[index].Cells[3].Value = page;
                }
                else {
                    Himg.Filename = pathfile;
                    int imgpage = Himg._CountPage();
                    int Page = Convert.ToInt32(page);
                    if (imgpage != Page) {
                        int index = dgvTjdata.Rows.Add();
                        dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                        dgvTjdata.Rows[index].Cells[1].Value = arno;
                        dgvTjdata.Rows[index].Cells[2].Value = imgpage;
                        dgvTjdata.Rows[index].Cells[3].Value = page;
                    }
                }

                dgvTjdata.Refresh();
            }
        }

        //按区统计卷数
        void GetQuarchstat()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetArchLx(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetArchLx(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            dgvTjdata.DataSource = dt;
            DataTable dt1 = null;
            try {
                dt1 = dt.Select("ArchLx='C'").CopyToDataTable();
                if (dt1 != null || dt1.Rows.Count > 0) {
                    labarchcount.Visible = true;
                    labarchcount.Text = "所有权:" + dt1.Rows.Count;
                }
            } catch { }

            try {
                dt1 = dt.Select("ArchLx='F'").CopyToDataTable();
                if (dt1 != null || dt1.Rows.Count > 0) {
                    labcheck.Visible = true;
                    labcheck.Text = "查封卷:" + dt1.Rows.Count;
                }
            } catch { }

            try {
                dt1 = dt.Select("ArchLx='Y'").CopyToDataTable();
                if (dt1 != null || dt1.Rows.Count > 0) {
                    labImgpath.Visible = true;
                    labImgpath.Text = "抵押卷:" + dt1.Rows.Count;
                }
            } catch { }
            dt1.Dispose();
        }

        //检测减去目录页数是否空
        private void Checkcontenpage()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetcheckContenPage(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetcheckContenPage(txtTjBoxsn1.Text.Trim());
            dgvTjdata.DataSource = dt;
        }

        //检测地号不一致
        private void CheckDisn()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labarchcount.Visible = true;
            for (int i = 0; i < dt.Rows.Count; i++) {
                labarchcount.Text = String.Format("正在执行{0}卷", (i + 1).ToString());
                Application.DoEvents();
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string xqsn = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                List<string> dsn = new List<string>();
                DataTable dtid = Common.GetInfoDisn(arid);
                if (dtid == null || dtid.Rows.Count <= 0)
                    continue;
                for (int t = 0; t < dtid.Rows.Count; t++) {
                    string d = dtid.Rows[t][0].ToString();
                    if (d.Trim().Length <= 0)
                        continue;
                    d = d.Replace("-", "");
                    if (d == "字")
                        continue;
                    if (d == "东丽字")
                        continue;
                    d = d.Replace("东丽字", "");
                    dsn.Add(d.Trim());
                }
                if (dsn.Count <= 1)
                    continue;
                dsn = dsn.Distinct().ToList();
                if (dsn.Count <= 1)
                    continue;

                if (dgvTjdata.Rows.Count == 0) {
                    dgvTjdata.Columns.Add("boxsn", "盒号");
                    dgvTjdata.Columns.Add("xqsn", "小区代号");
                }
                int index = dgvTjdata.Rows.Add();
                dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                dgvTjdata.Rows[index].Cells[1].Value = xqsn;

            }
            labarchcount.Text = "共0卷";
            labarchcount.Visible = false;
        }

        //检测最后一手14位
        void CheckYwid14()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labarchcount.Visible = true;
            for (int i = 0; i < dt.Rows.Count; i++) {
                labarchcount.Text = String.Format("正在执行{0}卷", (i + 1).ToString());
                Application.DoEvents();
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string xqsn = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                DataTable dtarid = Common.GetInfoall(arid);
                if (dtarid == null || dtarid.Rows.Count <= 0)
                    continue;
                string ywid = dtarid.Rows[0][0].ToString();
                if (ywid.Trim().Length != 14) {
                    if (dgvTjdata.Rows.Count == 0) {
                        dgvTjdata.Columns.Add("boxsn", "盒号");
                        dgvTjdata.Columns.Add("xqsn", "小区代号");
                        dgvTjdata.Columns.Add("sjsn", "收件编号");
                    }
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[1].Value = xqsn;
                    dgvTjdata.Rows[index].Cells[2].Value = ywid;
                }
            }
            labarchcount.Text = "共0卷";
            labarchcount.Visible = false;
        }

        //检测第一页是否目录
        void CheckConten()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labarchcount.Visible = true;
            for (int i = 0; i < dt.Rows.Count; i++) {
                labarchcount.Text = String.Format("正在执行{0}卷", (i + 1).ToString());
                Application.DoEvents();
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string xqsn = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                int id = Common.GetContepage(arid);
                if (id == 0) {
                    if (dgvTjdata.Rows.Count == 0) {
                        dgvTjdata.Columns.Add("boxsn", "盒号");
                        dgvTjdata.Columns.Add("xqsn", "小区代号");
                    }
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[1].Value = xqsn;
                }
            }
            labarchcount.Text = "共0卷";
            labarchcount.Visible = false;
        }

        //检测排序中标记类型档案
        void CheckTagPage()
        {
            dgvTjdata.DataSource = null;
            DataTable dt = Common.GetBoxsnPage(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string str = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string lx = dt.Rows[i][2].ToString();
                if (str.Trim().Length <= 0)
                    continue;
                string[] s = str.Split(';');
                string a = s[1];
                if (a.Trim().Length <= 0)
                    continue;
                string[] b = a.Split(':');
                if (b.Length <= 0)
                    continue;
                string x = b[1];
                int id;
                bool bl = int.TryParse(x, out id);
                if (!bl) {
                    a = s[2];
                    if (a.Trim().Length <= 0)
                        continue;
                    b = a.Split(':');
                    if (b.Length <= 0)
                        continue;
                    x = b[1];
                    bl = int.TryParse(x, out id);
                    if (!bl)
                        continue;

                }
                if (id > 10) {
                    if (dgvTjdata.Rows.Count == 0) {
                        dgvTjdata.Columns.Add("boxsn", "盒号");
                        dgvTjdata.Columns.Add("xqsn", "小区代号");
                    }
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[1].Value = lx;
                }
            }
        }

        //检测是否录目录
        void CheckContenCount()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string lx = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                int t = Common.GetcontenCount(arid);
                if (t <= 1) {
                    if (dgvTjdata.Rows.Count == 0) {
                        dgvTjdata.Columns.Add("boxsn", "盒号");
                        dgvTjdata.Columns.Add("xqsn", "小区代号");
                    }
                    int index = dgvTjdata.Rows.Add();
                    dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                    dgvTjdata.Rows[index].Cells[1].Value = lx;
                }
            }
        }

        void CheckContenXt()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labarchcount.Visible = true;
            labcheck.Visible = true;
            string oldcoonten = "";
            for (int i = 0; i < dt.Rows.Count; i++) {
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string lx = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                labarchcount.Text = string.Format("共{0}卷", dt.Rows.Count.ToString());
                labcheck.Text = string.Format("第{0}", i.ToString());
                Application.DoEvents();
                DataTable dtcon = Common.GetcontenTitpage(arid);
                if (dtcon == null || dtcon.Rows.Count <= 0)
                    continue;
                for (int t = 0; t < dtcon.Rows.Count; t++) {
                    oldcoonten = dtcon.Rows[t][0].ToString();
                    string newcon = dtcon.Rows[t][0].ToString();
                    if (t < dtcon.Rows.Count - 1)
                        newcon = dtcon.Rows[t + 1][0].ToString();
                    else
                        newcon = "";
                    if (newcon == oldcoonten) {
                        if (dgvTjdata.Columns.Count == 0) {
                            dgvTjdata.Columns.Add("boxsn", "盒号");
                            dgvTjdata.Columns.Add("xqsn", "第几条");
                            dgvTjdata.Columns.Add("conten", "目录");
                        }
                        int index = dgvTjdata.Rows.Add();
                        dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                        dgvTjdata.Rows[index].Cells[1].Value = t.ToString();
                        dgvTjdata.Rows[index].Cells[2].Value = newcon;
                    }
                }
            }
        }


        //检测审批日期
        void CheckSptime()
        {
            dgvTjdata.DataSource = null;
            DataTable dt;
            if (rabtjBoxsn.Checked)
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim());
            else
                dt = Common.GetBoxsnid(txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labarchcount.Visible = true;
            labcheck.Visible = true;
            labarchcount.Text = string.Format("共{0}卷", dt.Rows.Count.ToString());
            for (int i = 0; i < dt.Rows.Count; i++) {
                labcheck.Text = string.Format("第{0}", i.ToString());
                Application.DoEvents();
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string lx = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                DataTable dt1 = Common.GetSptime(arid);
                if (dt1 == null || dt1.Rows.Count <= 0)
                    continue;
                for (int t = 0; t < dt1.Rows.Count; t++) {
                    string date = dt1.Rows[t][0].ToString();
                    string dasx = dt1.Rows[t][1].ToString();
                    if (date.Trim().Length <= 0)
                        continue;
                    if (!isDate(date, arid, dasx)) {
                        if (dgvTjdata.Rows.Count == 0) {
                            dgvTjdata.Columns.Add("boxsn", "盒号");
                            dgvTjdata.Columns.Add("xqsn", "小区代号");
                            dgvTjdata.Columns.Add("dasx", "档案手续");
                        }
                        int index = dgvTjdata.Rows.Add();
                        dgvTjdata.Rows[index].Cells[0].Value = boxsn;
                        dgvTjdata.Rows[index].Cells[1].Value = lx;
                        dgvTjdata.Rows[index].Cells[2].Value = dasx;
                    }
                }

            }
            labarchcount.Visible = false;
            labcheck.Visible = false;
        }

        bool isDate(string str, string arid, string sx)
        {
            try {
                string n = "";
                string y = "";
                string d = "";
                string[] d2 = str.Split('-');
                if (d2.Length < 1)
                    return false;
                if (d2.Length == 3) {
                    n = d2[0];
                    y = d2[1];
                    d = d2[2];
                }
                else if (d2.Length == 2) {
                    n = d2[0];
                    y = d2[1];
                }
                else if (d2.Length == 1) {
                    n = d2[0];
                }
                if (chkUpdatetime.Checked) {
                    int id1 = 0;
                    if (n.Trim().Length == 4) {
                        int p;
                        bool bl = int.TryParse(n, out p);
                        if (!bl)
                            return false;
                        if (p < 1980 || p > 2020)
                            return false;
                    }
                    else
                        return false;

                    if (y.Trim().Length < 2) {
                        y = y.Trim().PadLeft(2, '0');
                        id1 = 1;
                    }
                    if (d.Trim().Length < 2 && d.Trim().Length > 0) {
                        d = d.Trim().PadLeft(2, '0');
                        id1 = 1;
                    }
                    string dtime = "";
                    if (y.Trim().Length > 0 && d.Trim().Length > 0) {
                        dtime = n + "-" + y + "-" + d;
                    }
                    else if (y.Trim().Length > 0 && d.Trim().Length <= 0) {
                        dtime = n + "-" + y;
                    }
                    try {
                        if (id1 == 1) {
                            DateTime dt1;
                            if (DateTime.TryParse(dtime, out dt1)) {
                                Common.UpContenSptime(arid, sx, dtime);
                                return true;
                            }
                        }
                    } catch {
                        return false;
                    }
                }
                DateTime dt;
                if (DateTime.TryParse(str, out dt)) {
                    int p;
                    bool bl = int.TryParse(n, out p);
                    if (!bl)
                        return false;
                    if (p < 1980 || p > 2020)
                        return false;
                    else if (y.Trim().Length > 0 && y.Trim().Length != 2)
                        return false;
                    else if (d.Trim().Length > 0 && d.Trim().Length != 2)
                        return false;
                    return true;
                }
                else
                    return false;
            } catch {
                return false;
            }
        }


        void IsSqlinfo()
        {

            if (combtjSql.SelectedIndex == 3) {
                QuerFile();
                return;
            }
            else if (combtjSql.SelectedIndex == 4) {
                QuerQuZt();
                return;
            }
            else if (combtjSql.SelectedIndex == 5) {
                CheckPage();
                return;
            }
            else if (combtjSql.SelectedIndex == 6) {
                CheckYwid();
                return;
            }
            else if (combtjSql.SelectedIndex == 7) {
                GetQuarchstat();
                return;
            }
            else if (combtjSql.SelectedIndex == 8) {
                Checkcontenpage();
                return;
            }
            else if (combtjSql.SelectedIndex == 9) {
                CheckDisn();
                return;
            }
            else if (combtjSql.SelectedIndex == 10) {
                CheckYwid14();
                return;
            }
            else if (combtjSql.SelectedIndex == 11) {
                CheckConten();
                return;
            }
            else if (combtjSql.SelectedIndex == 12) {
                CheckTagPage();
                return;
            }
            else if (combtjSql.SelectedIndex == 13) {
                CheckContenCount();
                return;
            }
            else if (combtjSql.SelectedIndex == 14) {
                CheckContenXt();
                return;
            }
            else if (combtjSql.SelectedIndex == 15) {
                CheckSptime();
                return;
            }
            Quersql();
        }
        private void buttjStart_Click(object sender, EventArgs e)
        {
            dgvTjdata.DataSource = null;
            dgvTjdata.Columns.Clear();
            dgvTjdata.Rows.Clear();
            buttjStart.Enabled = false;
            try {
                if (!istjtxt())
                    return;
                IsSqlinfo();
            } catch (Exception exception) {
                MessageBox.Show(exception.ToString());
            } finally {
                buttjStart.Enabled = true;
                //labarchcount.Visible = false;
                //labcheck.Visible = false;
            }

        }


        void Exportxls(string file)
        {
            buttjxls.Enabled = false;
            Workbook work = new Workbook();
            Worksheet wsheek = null;
            try {
                work.LoadFromFile(file);
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                if (rows == 0)
                    rows = 1;
                for (int t = 0; t < dgvTjdata.Rows.Count; t++) {
                    for (int c = 0; c < dgvTjdata.Columns.Count; c++) {
                        wsheek.Range[rows + t, c + 1].Text = dgvTjdata.Rows[t].Cells[c].ToString();
                    }
                }

                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
                MessageBox.Show("导出成功!");
            } catch (Exception ex) {
                MessageBox.Show("导出失败:" + ex.ToString());
            } finally {
                buttjxls.Enabled = true;
            }
        }

        private void buttjxls_Click(object sender, EventArgs e)
        {
            if (dgvTjdata.Rows.Count <= 0)
                return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xls文件|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
                Exportxls(sfd.FileName);
        }
        #endregion

        #region keys
        //获取所有模块
        void GetModul()
        {
            Task.Run(() =>
            {
                DataTable dt = T_Sysset.GetModulezhname();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string str = dt.Rows[i][0].ToString();
                    if (str.Trim().Length <= 0)
                        continue;
                    this.BeginInvoke(new Action(() =>
                    {
                        combKeyNewModulList.Items.Add(str);
                        Toolskeys.lsModule.Add(str);
                    }));
                }
            });
        }

        //将已经添加的模块添加到自定义commodul
        void Getkeys()
        {
            Toolskeys.LskeyModule.Clear();
            combKeyZdymodlx.Items.Clear();
            Task.Run(() =>
            {
                Toolskeys.dtkeys = Common.GetkeysInfo();
                if (Toolskeys.dtkeys == null || Toolskeys.dtkeys.Rows.Count <= 0)
                    return;
                for (int i = 0; i < Toolskeys.dtkeys.Rows.Count; i++) {
                    string strmod = Toolskeys.dtkeys.Rows[i][0].ToString();
                    if (strmod.Trim().Length <= 0)
                        continue;
                    this.BeginInvoke(new Action(() =>
                    {
                        if (Toolskeys.LskeyModule.IndexOf(strmod) < 0) {
                            Toolskeys.LskeyModule.Add(strmod);
                            combKeyZdymodlx.Items.Add(strmod);
                        }

                    }));
                }
            });
        }

        //选择模块时添加 操作及操作值
        void GetkeysOper(int x, int id)
        {
            string str = "";
            if (Toolskeys.isbool == true)
                return;
            Toolskeys.isbool = true;
            try {
                Toolskeys.LskeyOper.Clear();
                combKeyzdyoperlx.Items.Clear();
                Toolskeys.LsnewOper.Clear();
                Toolskeys.LsnewOperNum.Clear();
                combKeyNewOperList.Items.Clear();
                combKeyNewOpernumlist.Items.Clear();
                Toolskeys.LskeyOpernum.Clear();
                if (id > 0)
                    str = Toolskeys.LskeyModule[x];
                else
                    str = combKeyNewModulList.Text.Trim();
                str = "Module='" + str + "'";
                DataTable dt = Toolskeys.dtkeys.Select(str).CopyToDataTable();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                if (id > 0) {
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        string oper = dt.Rows[i][1].ToString();
                        string opernum = dt.Rows[i][2].ToString();
                        Toolskeys.LskeyOper.Add(oper);
                        combKeyzdyoperlx.Items.Add(oper);
                        Toolskeys.LskeyOpernum.Add(opernum);
                    }
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string oper = dt.Rows[i][1].ToString();
                    string opernum = dt.Rows[i][2].ToString();
                    Toolskeys.LsnewOper.Add(oper);
                    combKeyNewOperList.Items.Add(oper);
                    Toolskeys.LsnewOperNum.Add(opernum);
                    combKeyNewOpernumlist.Items.Add(opernum);
                }

            } catch {

            } finally {
                Toolskeys.isbool = false;
            }

        }



        bool Iskeystxt()
        {
            if (T_User.UserId != 1) {
                MessageBox.Show("此功能只能Admin管理员操作!");
                return false;
            }
            if (combKeyNewModulList.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择模块!");
                combKeyNewModulList.Focus();
                return false;
            }

            if (combKeyNewOperList.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入操作类型!");
                combKeyNewOperList.Focus();
                return false;
            }
            else {
                if (Toolskeys.LsnewOper.IndexOf(combKeyNewOperList.Text.Trim()) >= 0) {
                    MessageBox.Show("此操作名称已经存在请更改!");
                    combKeyNewOperList.Focus();
                    return false;
                }
            }
            if (combKeyNewOpernumlist.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入操作值!");
                combKeyNewOpernumlist.Focus();
                return false;
            }
            else {
                if (Toolskeys.LsnewOperNum.IndexOf(combKeyNewOpernumlist.Text.Trim()) >= 0) {
                    MessageBox.Show("此操作值已存在!");
                    combKeyNewOpernumlist.Focus();
                    return false;
                }
            }
            return true;
        }

        void KeysSqladd()
        {
            Common.KeysInster(combKeyNewModulList.Text.Trim(), combKeyNewOperList.Text.Trim(), combKeyNewOpernumlist.Text.Trim());
            Getkeys();
            GetkeysOper(0, 0);
            MessageBox.Show("操作成功!");
            combKeyNewOperList.Focus();
        }
        private void butKeyNew_Click(object sender, EventArgs e)
        {
            if (!Iskeystxt())
                return;
            KeysSqladd();
        }

        void keysDel()
        {
            if (combKeyNewModulList.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择模块列表!");
                combKeyNewModulList.Focus();
                return;
            }
            if (combKeyNewOperList.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择操作选项！");
                combKeyNewOperList.Focus();
                return;
            }
            if (combKeyNewOpernumlist.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择操作值!");
                combKeyNewOpernumlist.Focus();
                return;
            }
            Common.Keysdel(combKeyNewModulList.Text.Trim(), combKeyNewOperList.Text.Trim(), combKeyNewOpernumlist.Text.Trim());
            Getkeys();
            GetkeysOper(0, 0);
            MessageBox.Show("操作成功!");
            combKeyNewOperList.Text = "";
            combKeyNewOpernumlist.Text = "";
        }

        private void butKeysDel_Click(object sender, EventArgs e)
        {
            keysDel();
        }

        void GetkeysnumNewList(int x)
        {
            combKeyNewOperList.SelectedIndex = x;
            combKeyNewOpernumlist.SelectedIndex = x;
        }

        private void combKeyZdymodlx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = combKeyZdymodlx.SelectedIndex;
            GetkeysOper(x, 1);
            Getinikeyval();

        }

        private void combKeyNewModulList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = combKeyNewModulList.SelectedIndex;
            GetkeysOper(x, 0);
        }

        private void combKeyNewOperList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetkeysnumNewList(combKeyNewOperList.SelectedIndex);
        }

        private void combKeyNewOpernumlist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar))) {
                e.Handled = true;
            }
        }


        private void txtKeysZdyKeys_KeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder
            {
                Length = 0
            };
            keyValue.Append("");
            if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                (e.KeyValue >= 112 && e.KeyValue <= 123) ||
                e.KeyValue >= 96 && e.KeyValue == 111)   //F1-F12
            {
                keyValue.Append(e.KeyCode);
            }
            else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
                keyValue.Append(e.KeyCode.ToString().Substring(1));
            else if (e.KeyValue == 13 || e.KeyValue == 27 || e.KeyValue == 32 || e.KeyValue == 46)
                keyValue.Append(e.KeyCode.ToString().Substring(1));
            this.ActiveControl.Text = "";
            this.ActiveControl.Text = keyValue.ToString();
            if (keyValue.ToString() == "") {
                Toolskeys.KeyAscill = 0;
            }
            else {
                Toolskeys.KeyAscill = e.KeyValue;
            }
        }

        private void txtKeysZdyKeys_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtKeysZdyKeys_KeyUp(object sender, KeyEventArgs e)
        {
            string str = this.ActiveControl.Text.TrimEnd();
            int len = str.Length;
            if (len >= 1 && str.Substring(str.Length - 1) == "+") {
                this.ActiveControl.Text = "";
            }
            txtKeysZdyKeys.Focus();
        }

        string IsInikeysval()
        {
            string str = "";
            if (combKeyZdymodlx.Text.Trim().Length <= 0) {
                MessageBox.Show("模块类型不能为空!");
                combKeyZdymodlx.Focus();
                return str;
            }
            if (combKeyzdyoperlx.Text.Trim().Length <= 0) {
                MessageBox.Show("操作类型不能为空!");
                combKeyzdyoperlx.Focus();
                return str;
            }
            if (txtKeysZdyKeys.Text.Trim().Length <= 0 || Toolskeys.KeyAscill == 0) {
                MessageBox.Show("预设置快捷键不能为空!");
                txtKeysZdyKeys.Focus();
                return str;
            }
            str = combKeyzdyTkey.SelectedIndex.ToString() + "-" + Toolskeys.KeyAscill.ToString();
            return str;
        }

        string IsInikeysval2()
        {
            string str = "";
            if (combKeyZdymodlx.Text.Trim().Length <= 0) {
                MessageBox.Show("模块类型不能为空!");
                combKeyZdymodlx.Focus();
                return str;
            }
            if (combKeyzdyoperlx.Text.Trim().Length <= 0) {
                MessageBox.Show("操作类型不能为空!");
                combKeyzdyoperlx.Focus();
                return str;
            }
            str = combKeyzdyTkey.SelectedIndex.ToString() + "-" + Toolskeys.KeyAscill.ToString();
            return str;
        }

        string Isinikey()
        {
            string str = "";
            if (combKeyZdymodlx.Text.Trim().Length <= 0) {
                MessageBox.Show("模块类型不能为空!");
                combKeyZdymodlx.Focus();
                return str;
            }
            if (combKeyzdyoperlx.Text.Trim().Length <= 0) {
                MessageBox.Show("操作类型不能为空!");
                combKeyzdyoperlx.Focus();
                return str;
            }
            str = combKeyzdyoperlx.Text.Trim();
            int x = Toolskeys.LskeyOper.IndexOf(str);
            if (x < 0)
                return "";
            string key = Toolskeys.LskeyOpernum[x];
            if (key.Trim().Length <= 0)
                return "";
            str = "V" + key;
            return str;
        }


        void Getinikeyval()
        {
            txtKeysZdyKeys.Text = "";
            labkeys.Text = "未注册";
            try {
                Toolskeys.Lsinikey.Clear();
                Toolskeys.LsiniCz.Clear();
                Toolskeys.LsId.Clear();
                DataTable dt = Common.GetOpenkey(combKeyZdymodlx.Text.Trim());
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string strid = dt.Rows[i][0].ToString();
                    string strkey = dt.Rows[i][2].ToString();
                    string strnum = dt.Rows[i][3].ToString();
                    if (strid.Trim().Length > 0 && strkey.Trim().Length > 0 && strnum.Trim().Length > 0) {
                        Toolskeys.Lsinikey.Add(strkey);
                        Toolskeys.LsiniCz.Add(strnum);
                        Toolskeys.LsId.Add(strid);
                    }
                }

                if (Toolskeys.Lsinikey.Count > 0 && Toolskeys.LsiniCz.Count > 0 && Toolskeys.LsId.Count > 0) {
                    LbKey.Items.Clear();
                    for (int i = 0; i < Toolskeys.Lsinikey.Count; i++) {
                        string str = Toolskeys.Lsinikey[i].ToString().Replace("V", "");
                        int x = Toolskeys.LskeyOpernum.IndexOf(str);
                        if (x < 0)
                            continue;
                        string s = Toolskeys.LskeyOper[x];
                        string c = Toolskeys.LsiniCz[i];
                        string[] KeyV = c.Split(new char[] { '-' });
                        if (KeyV[0].Trim().ToString() == "1") {
                            c = "Ctrl+";
                        }
                        else if (KeyV[0].Trim().ToString() == "2") {
                            c = "Alt+";
                        }
                        else if (KeyV[0].Trim().ToString() == "3") {
                            c = "Shift+";
                        }
                        else {
                            c = "";
                        }
                        int nk = Convert.ToInt32(KeyV[1].Trim());
                        str = ((char)nk).ToString();
                        if (nk == 13)
                            str = "回车";
                        else if (nk == 32)
                            str = "空格";
                        else if (nk == 27)
                            str = "Ese";
                        else if (nk == 46)
                            str = "Del";
                        string strs = s + ":" + c + str;
                        LbKey.Items.Add(strs);
                    }
                }

            } catch {
                MessageBox.Show("读取快捷键失败!");
            }
        }

        void SelectKey()
        {
            txtKeysZdyKeys.Text = "";
            labkeys.ForeColor = Color.Black;
            labkeys.Text = "未注册";
            if (combKeyzdyoperlx.Text.Length <= 0 || Toolskeys.LsiniCz.Count <= 0)
                return;
            string str = "V" + Toolskeys.LskeyOpernum[combKeyzdyoperlx.SelectedIndex];
            int x = Toolskeys.Lsinikey.IndexOf(str);
            if (x >= 0) {
                str = Toolskeys.LsiniCz[x];
                string[] KeyV = str.Split(new char[] { '-' });
                if (KeyV[0].Trim().ToString() == "1") {
                    str = "Ctrl+";
                }
                else if (KeyV[0].Trim().ToString() == "2") {
                    str = "Alt+";
                }
                else if (KeyV[0].Trim().ToString() == "3") {
                    str = "Shift+";
                }
                else {
                    str = "";
                }
                int nk = Convert.ToInt32(KeyV[1].Trim());
                str += ((char)nk).ToString();
                if (nk == 13)
                    str = "回车";
                else if (nk == 32)
                    str = "空格";
                else if (nk == 27)
                    str = "Ese";
                else if (nk == 46)
                    str = "Del";
                labkeys.ForeColor = Color.Red;
                labkeys.Text = str;
            }
        }

        //保存快捷键
        void Savekey(string keys, string val)
        {
            try {
                int x = Toolskeys.LsiniCz.IndexOf(val);
                if (x >= 0) {
                    string id = Toolskeys.LsId[x];
                    if (!chkkeys.Checked) {
                        if (MessageBox.Show("此快捷键已注册，是否强制重新注册？", "警告", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                            Common.UpdatekeyOper(combKeyZdymodlx.Text.Trim(), keys, val, id);
                            MessageBox.Show("注册成功!");
                            txtKeysZdyKeys.Focus();
                            return;
                        }
                    }
                    return;
                }
                Common.InsterkeyOper(combKeyZdymodlx.Text.Trim(), keys, val);
                MessageBox.Show("注册成功!");
                txtKeysZdyKeys.Focus();
                return;
            } catch (Exception e) {
                MessageBox.Show("注册失败:" + e.ToString());
            } finally {
                Getinikeyval();
            }
        }

        private void butKeysZdyAdd_Click(object sender, EventArgs e)
        {
            string val = IsInikeysval().Trim();
            string key = Isinikey().Trim();
            if (val.Length <= 0 || key.Length <= 0)
                return;
            Savekey(key, val);
        }


        private void combKeyzdyoperlx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combKeyzdyoperlx.SelectedIndex >= 0)
                SelectKey();
        }

        private void butKeysZdydel_Click(object sender, EventArgs e)
        {
            string str = Isinikey().Trim();
            if (str.Length <= 0)
                return;
            Common.Keysdelvale(combKeyZdymodlx.Text.Trim(), str);
            Getinikeyval();
            string s = "清空快键键:" + str;
            Common.Writelog(0, s);
            MessageBox.Show("清除成功!");
            txtKeysZdyKeys.Focus();

        }
        #endregion
        private void Frmtool_Shown(object sender, EventArgs e)
        {
            ftp = new HFTP();
            GetModul();
            Getkeys();
            combKeyzdyTkey.SelectedIndex = 0;
            //string str = Path.Combine(Application.StartupPath, "CsmKeyVal.ini");
            //Writeini.Fileini = str;

        }
        #region imgcombe


        private void butImgadd_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK) {
                lbImgPath.Items.Add(openFile.FileName);
            }
        }

        private void butImgDel_Click_1(object sender, EventArgs e)
        {
            if (lbImgPath.Items.Count <= 0)
                return;
            try {
                int id = lbImgPath.SelectedIndex;
                lbImgPath.Items.RemoveAt(id);
            } catch { }
        }

        void Istxt(bool bl)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (!bl) {
                    butImgadd.Enabled = false;
                    butImgDel.Enabled = false;
                    butCombe.Enabled = false;
                    butImgSave.Enabled = false;
                    pictgif.Visible = true;
                    return;
                }
                butImgadd.Enabled = true;
                butImgDel.Enabled = true;
                butCombe.Enabled = true;
                butImgSave.Enabled = true;
                pictgif.Visible = false;
                return;
            }));
        }

        List<string> lsfile = new List<string>();
        private void butCombe_Click(object sender, EventArgs e)
        {
            if (lbImgPath.Items.Count <= 0) {
                MessageBox.Show("请先添加图像路径!");
                return;
            }
            lsfile.Clear();
            for (int i = 0; i < lbImgPath.Items.Count; i++) {
                string str = lbImgPath.Items[i].ToString();
                if (str.Trim().Length <= 0)
                    continue;
                lsfile.Add(str);
            }
            if (lsfile.Count <= 0)
                return;
            Istxt(false);
            pictImg.Image = null;
            Action Act = Imgcombe;
            Act.BeginInvoke(null, null);
        }

        void Imgcombe()
        {
            Task t = Task.Run(() =>
              {
                  try {
                      Bitmap bmp = ClsImg.ImgPj(lsfile);
                      if (bmp != null) {
                          this.Invoke(new Action(() => { pictImg.Image = bmp; }));
                      }
                      else
                          MessageBox.Show("拼接失败");
                  } catch (Exception e) {
                      MessageBox.Show("拼接失败:" + e.ToString());
                  } finally {
                      this.BeginInvoke(new Action(() => { pictgif.Visible = false; }));
                  }
              });
            Task.WaitAll(t);
            Istxt(true);
        }

        private void butImgSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK) {
                string file = saveFile.FileName;
                pictImg.Image.Save(file + ".jpg", ImageFormat.Jpeg);
                if (File.Exists(file)) {
                    MessageBox.Show("保存完成！");
                    pictImg.Image = null;
                    return;
                }
                MessageBox.Show("保存失败!");
            }
        }

        #endregion

        private List<string> strfile = new List<string>();
        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog flDialog = new FolderBrowserDialog();
            if (flDialog.ShowDialog() == DialogResult.OK) {
                strfile.Clear();
                listBox1.Items.Clear();
                var dir = new DirectoryInfo(flDialog.SelectedPath);
                FileInfo[] dirname = dir.GetFiles("*.jpg");
                for (int i = 0; i < dirname.Length; i++) {
                    string str = dirname[i].FullName;
                    strfile.Add(str);
                    listBox1.Items.Add(str);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入tf文件名!");
                return;
            }
            string tif = Path.Combine(@"D:\", textBox1.Text.Trim() + ".tif");
            Himg.jpgTotif(strfile, tif);
            MessageBox.Show("完成");
        }

        #region toolsqit

        bool istxtqit()
        {
            if (txttoolsB1.Text.Trim().Length <= 0 || txttoolsB2.Text.Trim().Length <= 0) {
                MessageBox.Show("盒号不能为空!");
                txttoolsB1.Focus();
                return false;
            }
            else {
                int b1, b2;
                bool bl1 = int.TryParse(txttoolsB1.Text.Trim(), out b1);
                bool bl2 = int.TryParse(txttoolsB2.Text.Trim(), out b2);
                if (!bl1 || !bl2) {
                    MessageBox.Show("盒号范围不正确!");
                    txttoolsB1.Focus();
                    return false;
                }
            }
            return true;
        }

        private void buttools_Click(object sender, EventArgs e)
        {
            if (T_User.UserId != 1) {
                MessageBox.Show("此功能只能管理员使用!");
                txttoolsB1.Focus();
                return;
            }
            if (!istxtqit())
                return;
            Common.SetNewboxsn(txttoolsB1.Text.Trim(), txttoolsB2.Text.Trim());
            MessageBox.Show("更改完成");

        }

        bool Isconten()
        {
            int b, p;
            bool bl = int.TryParse(txtContenboxsn.Text.Trim(), out b);
            bool pl = int.TryParse(txtContePage.Text.Trim(), out p);
            if (!bl || !pl) {
                MessageBox.Show("盒号或者页码不正确!");
                txtContenboxsn.Focus();
                return false;
            }
            return true;
        }
        private void butContenPage_Click(object sender, EventArgs e)
        {
            if (!Isconten())
                return;
            Common.SetContenPage(txtContenboxsn.Text.Trim(), txtContePage.Text.Trim());
            MessageBox.Show("修改完成!");
        }


        #endregion

        #region chkConten

        bool IstxtConten()
        {
            if (txtTitle.Text.Trim().Length <= 0 || txtQbox.Text.Trim().Length <= 0 || txtZbox.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入标题或盒号范围!");
                txtTitle.Focus();
                return false;
            }
            int b1, b2;
            bool bl = int.TryParse(txtQbox.Text.Trim(), out b1);
            bool bl2 = int.TryParse(txtZbox.Text.Trim(), out b2);
            if (!bl || !bl2) {
                MessageBox.Show("盒号范围值不正确!");
                txtQbox.Focus();
                return false;
            }
            if (b1 <= 0 || b2 <= 0) {
                MessageBox.Show("盒号范围值不正确!");
                txtQbox.Focus();
                return false;
            }
            if (b1 > b2) {
                MessageBox.Show("起始号号不能大于终止盒号!");
                txtQbox.Focus();
                return false;
            }
            if (chkPage.Checked) {
                if (txtPage.Text.Trim().Length <= 0) {
                    MessageBox.Show("检测页数不能为空!");
                    txtPage.Focus();
                    return false;
                }
                int p0;
                bool b = int.TryParse(txtPage.Text.Trim(), out p0);
                if (!b || p0 <= 0) {
                    MessageBox.Show("页码值不正确!");
                    txtPage.Focus();
                    return false;
                }
            }
            return true;
        }

        void ChkContenSql()
        {
            DataTable dt = null;
            if (chkXq.Checked)
                dt = dt = Common.GetboxsnFwXq(txtQbox.Text.Trim());
            else
                dt = Common.GetboxsnFw(txtQbox.Text.Trim(), txtZbox.Text.Trim());
            bool bl = chkLastConten.Checked;
            string gjz = txtTitle.Text.Trim();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            labCount.Text = string.Format("共{0}卷", dt.Rows.Count.ToString());
            for (int i = 0; i < dt.Rows.Count; i++) {
                string arid = dt.Rows[i][0].ToString();
                string boxsn = dt.Rows[i][1].ToString();
                string xq = dt.Rows[i][2].ToString();
                if (arid.Trim().Length <= 0)
                    continue;
                Application.DoEvents();
                labsy.Text = string.Format("第{0}卷", i.ToString());
                int ywid = Common.Getconteninfo(Convert.ToInt32(arid));
                if (ywid == 0)
                    continue;
                for (int y = 1; y < ywid; y++) {
                    DataTable dt2 = Common.GetcontenMaxtitle(arid, bl, y);
                    if (dt2 == null || dt2.Rows.Count <= 0)
                        continue;
                    for (int t = 0; t < dt2.Rows.Count; t++) {
                        string str = dt2.Rows[t][0].ToString();
                        if (rabContenNotBh.Checked) {
                            if (!str.Contains(gjz)) {
                                if (dgvContenSql.Rows.Count == 0) {
                                    dgvContenSql.Columns.Add("boxsn", "盒号");
                                    dgvContenSql.Columns.Add("xqsn", "小区代号");
                                    dgvContenSql.Columns.Add("title", "标题");
                                    dgvContenSql.Columns.Add("ywid", "第几手");
                                }
                                int index = dgvContenSql.Rows.Add();
                                dgvContenSql.Rows[index].Cells[0].Value = boxsn;
                                dgvContenSql.Rows[index].Cells[1].Value = xq;
                                dgvContenSql.Rows[index].Cells[2].Value = str;
                                dgvContenSql.Rows[index].Cells[3].Value = y.ToString();
                            }
                        }
                        else {
                            if (str.Contains(gjz)) {
                                if (dgvContenSql.Rows.Count == 0) {
                                    dgvContenSql.Columns.Add("boxsn", "盒号");
                                    dgvContenSql.Columns.Add("xqsn", "小区代号");
                                    dgvContenSql.Columns.Add("title", "标题");
                                    dgvContenSql.Columns.Add("ywid", "第几手");
                                }
                                int index = dgvContenSql.Rows.Add();
                                dgvContenSql.Rows[index].Cells[0].Value = boxsn;
                                dgvContenSql.Rows[index].Cells[1].Value = xq;
                                dgvContenSql.Rows[index].Cells[2].Value = str;
                                dgvContenSql.Rows[index].Cells[3].Value = y.ToString();
                            }
                        }
                    }
                }
            }
            labsy.Text = "";
            labCount.Text = "";
        }

        void ChkContenSqlPage()
        {
            butContenSelect.Enabled = false;
            dgvContenSql.Rows.Clear();
            try {
                DataTable dt = null;
                if (chkXq.Checked)
                    dt = Common.GetboxsnFwXq(txtQbox.Text.Trim());
                else
                    dt = Common.GetboxsnFw(txtQbox.Text.Trim(), txtZbox.Text.Trim());
                bool bl = chkLastConten.Checked;
                string gjz = txtTitle.Text.Trim();
                string Pagejc = txtPage.Text.Trim();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                labCount.Text = "共计:" + dt.Rows.Count;
                Application.DoEvents();
                int p1, p2, p0 = 0;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string arid = dt.Rows[i][0].ToString();
                    string boxsn = dt.Rows[i][1].ToString();
                    string xq = dt.Rows[i][2].ToString();
                    string Pages = dt.Rows[i][3].ToString();
                    if (arid.Trim().Length <= 0)
                        continue;
                    labsy.Text = "第:" + (i + 1).ToString();
                    Application.DoEvents();
                    DataTable dt2 = Common.GetcontenTmp(arid);
                    if (dt2 == null || dt2.Rows.Count <= 0)
                        continue;
                    for (int t = 0; t < dt2.Rows.Count; t++) {
                        string str = dt2.Rows[t][0].ToString();
                        string page = dt2.Rows[t][1].ToString();
                        if (rabContenBh.Checked) {
                            if (str.Contains(gjz)) {
                                try {
                                    string p = dt2.Rows[t + 1][1].ToString();
                                    p1 = Convert.ToInt32(page);
                                    p2 = Convert.ToInt32(p);
                                    p0 = p2 - p1;

                                } catch {
                                    p1 = Convert.ToInt32(page);
                                    p2 = Convert.ToInt32(Pages);
                                    p0 = p2 - p1 + 1;
                                }
                            }
                            else
                                continue;
                        }
                        else if (rabContenNotBh.Checked) {
                            if (!str.Contains(gjz)) {
                                try {
                                    string p = dt2.Rows[t + 1][1].ToString();
                                    p1 = Convert.ToInt32(page);
                                    p2 = Convert.ToInt32(p);
                                    p0 = p2 - p1;

                                } catch {
                                    p1 = Convert.ToInt32(page);
                                    p2 = Convert.ToInt32(Pages);
                                    p0 = p2 - p1 + 1;
                                }
                            }
                            else
                                continue;
                        }

                        if (dgvContenSql.Rows.Count == 0) {
                            dgvContenSql.Columns.Add("boxsn", "盒号");
                            dgvContenSql.Columns.Add("xqsn", "小区代号");
                            dgvContenSql.Columns.Add("title", "标题");
                        }
                        else {
                            if (!chkMax.Checked && !chkPage.Checked && !chkXy.Checked)
                                continue;
                            if (chkPage.Checked) {
                                if (Pagejc.ToString() != p0.ToString())
                                    continue;
                            }
                            else if (chkXy.Checked) {
                                if (p0 >= Convert.ToInt32(Pagejc))
                                    continue;
                            }
                            else if (chkMax.Checked) {
                                if (Convert.ToInt32(Pagejc) >= p0)
                                    continue;
                            }
                            int index = dgvContenSql.Rows.Add();
                            dgvContenSql.Rows[index].Cells[0].Value = boxsn;
                            dgvContenSql.Rows[index].Cells[1].Value = xq;
                            dgvContenSql.Rows[index].Cells[2].Value = str;
                        }
                    }
                }
            } catch (Exception s) {
                MessageBox.Show(s.ToString());
            } finally {
                butContenSelect.Enabled = true;
                labsy.Text = "";
                labCount.Text = "";
            }
        }
        private void butContenSelect_Click(object sender, EventArgs e)
        {
            if (!IstxtConten())
                return;
            dgvContenSql.Columns.Clear();
            butContenSelect.Enabled = false;
            if (chkPage.Checked || chkMax.Checked || chkXy.Checked) {
                ChkContenSqlPage();
                return;
            }
            ChkContenSql();
            butContenSelect.Enabled = true;
        }
        #endregion



        private void chkPage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPage.Checked) {
                {
                    chkLastConten.Checked = false;
                    chkMax.Checked = false;
                    chkXq.Checked = false;
                }
            }
        }
        private void chkMax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMax.Checked) {
                chkLastConten.Checked = false;
                chkPage.Checked = false;
                chkXy.Checked = false;
            }
        }

        private void chXy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkXy.Checked) {
                chkLastConten.Checked = false;
                chkMax.Checked = false;
                chkPage.Checked = false;
            }
        }

        #region boxsnCount

        private List<string> lsboxsn = new List<string>();
        private void butBoxsnadd_Click(object sender, EventArgs e)
        {

            string str = txtTjbox.Text.Trim();
            if (str.Length <= 0) {
                MessageBox.Show("盒号不能为空");
                txtTjbox.Focus();
                return;
            }
            int b;
            bool bl;
            if (str.IndexOf("-") >= 0) {
                string[] a = str.Split('-');
                if (a.Length <= 0)
                    return;
                int min = Convert.ToInt32(a[0]);
                int max = Convert.ToInt32(a[1]);
                for (int i = min; i <= max; i++) {
                    string s = i.ToString();
                    bl = int.TryParse(s, out b);
                    if (!bl || b <= 0) {
                        MessageBox.Show("盒号不正确!");
                        txtTjbox.Focus();
                        return;
                    }
                    if (lsboxsn.IndexOf(b.ToString()) >= 0) {
                        MessageBox.Show("盒号已存在不允许重新添加!");
                        txtTjbox.Focus();
                        return;
                    }
                    lsboxsn.Add(s);
                    LbboxCount.Items.Add(s);
                }
                txtTjbox.Text = "";
                txtTjbox.Focus();
                return;
            }
            bl = int.TryParse(str, out b);
            if (!bl || b <= 0) {
                MessageBox.Show("盒号不正确!");
                txtTjbox.Focus();
                return;
            }
            if (lsboxsn.IndexOf(b.ToString()) >= 0) {
                MessageBox.Show("盒号已存在不允许重新添加!");
                txtTjbox.Focus();
                return;
            }
            lsboxsn.Add(str);
            LbboxCount.Items.Add(str);
            txtTjbox.Text = "";
            txtTjbox.Focus();
        }

        private void butboxCle_Click(object sender, EventArgs e)
        {
            LbboxCount.Items.Clear();
            lsboxsn.Clear();
        }

        private void txtTjbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butBoxsnadd_Click(null, null);
        }
        #endregion

        private void butBoxsnCount_Click(object sender, EventArgs e)
        {
            if (lsboxsn.Count <= 0) {
                MessageBox.Show("请先添加盒号!");
                txtTjbox.Focus();
                return;
            }
            butBoxsnCount.Enabled = false;
            butBoxsnadd.Enabled = false;
            butboxCle.Enabled = false;
            try {
                int count = 0;
                for (int i = 0; i < lsboxsn.Count; i++) {
                    Application.DoEvents();
                    string b = lsboxsn[i];
                    int p = Common.GetPages(b);
                    count += p;
                }
                labboxcount.Text = "共计:" + count.ToString();
            } catch { } finally {
                butBoxsnCount.Enabled = true;
                butBoxsnadd.Enabled = true;
                butboxCle.Enabled = true;
            }
        }

        bool isnian()
        {
            bool tf = false;
            if (txtNian.Text.Trim().Length <= 0 || txtNianjuan1.Text.Trim().Length <= 0 ||
                txtNianJuan2.Text.Trim().Length <= 0) {
                MessageBox.Show("年份或卷号不正确!");
                txtNian.Focus();
                return tf;
            }
            int boxsn, archno1, archno2;
            bool bl = int.TryParse(txtNian.Text.Trim(), out boxsn);
            if (!bl || boxsn <= 0) {
                MessageBox.Show("盒号不正确!");
                txtNian.Focus();
                return tf;
            }
            bl = int.TryParse(txtNianjuan1.Text.Trim(), out archno1);
            if (!bl || archno1 <= 0) {
                MessageBox.Show("起始卷号不正确!");
                txtNianjuan1.Focus();
                return tf;
            }
            bl = int.TryParse(txtNianJuan2.Text.Trim(), out archno2);
            if (!bl || archno2 <= 0) {
                MessageBox.Show("终止卷号不正确!");
                txtNianJuan2.Focus();
                return tf;
            }
            return true;
        }

        private void butAdddata_Click(object sender, EventArgs e)
        {
            if (!isnian())
                return;
            int box = Convert.ToInt32(txtNian.Text.Trim());
            int arch1 = Convert.ToInt32(txtNianjuan1.Text.Trim());
            int arch2 = Convert.ToInt32(txtNianJuan2.Text.Trim());
            butAdddata.Enabled = false;
            txtNian.Enabled = false;
            txtNianjuan1.Enabled = false;
            txtNianJuan2.Enabled = false;
            try
            {
                for (int i = arch1; i <= arch2; i++)
                {

                    labnianstat.Text = "正在执行:" + i.ToString();
                    Application.DoEvents();
                    Common.ArchGrounding2(1, 1, 1, 1, 1, 1, i.ToString(), box.ToString());
                }

                labnianstat.Text = "完成";
            }
            catch 
            {}
            finally
            {
                labnianstat.Text = "完成";
                butAdddata.Enabled = true;
                txtNian.Enabled = true;
                txtNianjuan1.Enabled = true;
                txtNianJuan2.Enabled = true;
            }
            
        }

        private void txtNian_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtNianjuan1.Focus();
        }

        private void txtNianjuan1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtNianJuan2.Focus();
        }

        private void txtNianJuan2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butAdddata.Focus();
        }
    }

}
