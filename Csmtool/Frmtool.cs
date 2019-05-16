using DAL;
using HLFtp;
using Spire.Xls;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmtool
{
    public partial class Frmtool : Form
    {
        public Frmtool()
        {
            InitializeComponent();
        }

        private HLFtp.HFTP ftp;

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

        void CleScanstat(int arid, string boxsn, string archno)
        {
            Common.ClearScanWrok(arid);
            string file = Path.Combine(T_ConFigure.gArchScanPath, boxsn + "-" + archno, T_ConFigure.ScanTempFile);
            if (ftp.FtpCheckFile(file)) {
                try {
                    ftp.FtpDelFile(file);
                } catch (Exception e) {
                    MessageBox.Show("清图像失败:" + e.ToString());
                }
            }
            MessageBox.Show("操作完成!");
        }

        void Cleinfo(int arch, int id)
        {
            if (id == 1) {
                Common.ClearInfoWrok(arch, 1);
                Common.ClearInfoWrok(arch, 2);
                Common.ClearInfoWrok(arch, 3);
                return;
            }
            if (rabcleInfobl.Checked)
                Common.ClearInfoWrok(arch, 1);
            else if (rabcleConten.Checked)
                Common.ClearInfoWrok(arch, 2);
            else if (rabcleCheck.Checked)
                Common.ClearInfoWrok(arch, 3);
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
            else Cleinfo(arid, 0);

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
            return true;
        }
        private void rabtjBoxsn_CheckedChanged(object sender, EventArgs e)
        {
            if (rabtjBoxsn.Checked)
                txtTjBoxsn2.Enabled = true;
            else txtTjBoxsn2.Enabled = false;
            txtTjBoxsn1.Focus();
        }

        void Quersql()
        {
            DataTable dt;
            if (rabBoxsn.Checked)
                dt = Common.GetArchQuerstat(combtjSql.Text.Trim(), txtTjBoxsn1.Text.Trim(), txtTjBoxsn2.Text.Trim(), "");
            else
                dt = Common.GetArchQuerstat(combtjSql.Text.Trim(), "", "", txtTjBoxsn1.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            dgvTjdata.DataSource = null;
            dgvTjdata.DataSource = dt;

        }

        private void buttjStart_Click(object sender, EventArgs e)
        {
            if (!istjtxt())
                return;
            Quersql();
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

        private void Frmtool_Shown(object sender, EventArgs e)
        {
            ftp = new HFTP();
            GetModul();
            Getkeys();
        }


        #region keys

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

        void Getkeys()
        {
            Toolskeys.LskeyModule.Clear();
            Toolskeys.LskeyOper.Clear();
            combKeyzdyoperlx.Items.Clear();
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

        void GetkeysOper(int x)
        {
            if (Toolskeys.isbool == true)
                return;
            Toolskeys.isbool = true;
            try {
                string str = Toolskeys.LskeyModule[x];
                str = "Module='" + str + "'";
                DataTable dt = Toolskeys.dtkeys.Select(str).CopyToDataTable();
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                Toolskeys.LskeyOper.Clear();
                combKeyzdyoperlx.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string opernu = dt.Rows[i][1].ToString();
                    Toolskeys.LskeyOper.Add(opernu);
                    combKeyzdyoperlx.Items.Add(opernu);
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

            if (txtKeyNewOperlx.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入操作类型!");
                txtKeyNewOperlx.Focus();
                return false;
            }
            if (txtKeyNewOpernum.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入操作值!");
                txtKeyNewOpernum.Focus();
                return false;
            }
            else {
                bool x = Common.IsKeyscount(combKeyNewModulList.Text.Trim(), txtKeyNewOperlx.Text.Trim(),
                    txtKeyNewOpernum.Text.Trim());
                if (x) {
                    MessageBox.Show("此操作类型或操作值已存在请更换!");
                    txtKeyNewOperlx.Focus();
                    return false;
                }

            }
            return true;
        }


        private void butKeyNew_Click(object sender, EventArgs e)
        {
            if (!Iskeystxt())
                return;
            Common.KeysInster(combKeyNewModulList.Text.Trim(), txtKeyNewOperlx.Text.Trim(), txtKeyNewOpernum.Text.Trim());
            Getkeys();
        }

        private void butKeysDel_Click(object sender, EventArgs e)
        {
            if (!Iskeystxt())
                return;
        }

        private void txtKeyNewOpernum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar))) {
                e.Handled = true;
            }
            Common.Keysdel(combKeyNewModulList.Text.Trim(), txtKeyNewOperlx.Text.Trim(), txtKeyNewOpernum.Text.Trim());
            Getkeys();
        }

        private void combKeyZdymodlx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = combKeyZdymodlx.SelectedIndex;
            GetkeysOper(x);

        }
        #endregion
    }
}
