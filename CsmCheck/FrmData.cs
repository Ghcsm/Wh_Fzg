using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CsmCheck
{
    public partial class FrmData : Form
    {
        public FrmData()
        {
            InitializeComponent();
        }

        private void GetInfo()
        {
            ClsTable.Ac = true;
            try {
                if (ClsTable.LsTable.Count <= 0)
                {
                    MessageBox.Show("未在后台设置表信息!");
                    return;
                }
                DataGridView dg1 = null;
                DataGridView dg2 = null;
                ClsTable.lsTabletmp.Clear();
                int x = ClsTable.LsTable.IndexOf(ClsTable.ArchType);
                if (x < 0) {
                    MessageBox.Show("未在后台设置此表信息!");
                    return;
                }
                string tb = ClsTable.LsTable[x];
                string col = ClsTable.lsCol[x];
                string msgk = ClsTable.lsMsgk[x];
                if (msgk == "信息框1") {
                    dg1 = dgvInfo1_one;
                    dg2 = dgvInfo1_two;
                }
                else if (msgk == "信息框2") {
                    dg1 = dgvInfo2_one;
                    dg2 = dgvInfo2_two;
                }
                Thread.Sleep(300);
                ClsSetInfo.SetArchInfo(ClsTable.Archid, tb, col, dg1, dg2);
                Thread.Sleep(100);
                ClsSetInfo.ArchStat(lbCheck);
                Thread.Sleep(100);
                ClsSetInfo.GetuserInfo(statools, toolsuser1, toolsusertime1, toolsuser2, toolsusertime2);

            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            } finally {
                ClsTable.Ac = false;
            }
        }

        private void FrmData_Shown(object sender, EventArgs e)
        {
            ClsSetInfo.GetTableinfo();
        }

        #region Frminfo

        private void butFg_Click(object sender, EventArgs e)
        {
            if (!ClsTable.Archzt) {
                MessageBox.Show("数据未校验无需要返工!");
                return;
            }
            ClsSetInfo.SetXyinfo(2);
        }
        private void dgvInfo1_one_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.dgvInfo1_two.Rows.Count > 0) {
                dgvInfo1_two.FirstDisplayedScrollingRowIndex = dgvInfo1_one.FirstDisplayedScrollingRowIndex;
                dgvInfo1_two.HorizontalScrollingOffset = dgvInfo1_one.HorizontalScrollingOffset;
                dgvInfo1_two.Update();
            }
        }

        private void dgvInfo1_two_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.dgvInfo1_one.Rows.Count > 0) {
                dgvInfo1_one.FirstDisplayedScrollingRowIndex = dgvInfo1_two.FirstDisplayedScrollingRowIndex;
                dgvInfo1_one.HorizontalScrollingOffset = dgvInfo1_two.HorizontalScrollingOffset;
                dgvInfo1_one.Update();
            }
        }

        private void dgvInfo2_one_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.dgvInfo2_two.Rows.Count > 0) {
                dgvInfo2_two.FirstDisplayedScrollingRowIndex = dgvInfo2_one.FirstDisplayedScrollingRowIndex;
                dgvInfo2_two.HorizontalScrollingOffset = dgvInfo2_one.HorizontalScrollingOffset;
                dgvInfo2_two.Update();
            }
        }

        private void dgvInfo2_two_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.dgvInfo2_one.Rows.Count > 0) {
                dgvInfo2_one.FirstDisplayedScrollingRowIndex = dgvInfo2_two.FirstDisplayedScrollingRowIndex;
                dgvInfo2_one.HorizontalScrollingOffset = dgvInfo2_two.HorizontalScrollingOffset;
                dgvInfo2_one.Update();
            }
        }

        private void pict1_Click(object sender, EventArgs e)
        {
            if (dgvInfo1_one.Rows.Count <= 0 || dgvInfo1_two.Rows.Count <= 0)
                return;
            if (dgvInfo1_one.CurrentRow.Index < 0) {
                MessageBox.Show("请先选择正确的数据行!");
                return;
            }
            string str = ClsTable.ArchType;
            ClsSetInfo.UpdateInfo(str, 1, dgvInfo1_one, dgvInfo1_two);
        }

        private void pict2_Click(object sender, EventArgs e)
        {
            if (dgvInfo1_one.Rows.Count <= 0 || dgvInfo1_two.Rows.Count <= 0)
                return;
            if (dgvInfo1_two.CurrentRow.Index < 0) {
                MessageBox.Show("请先选择正确的数据行!");
                return;
            }
            string str = ClsTable.ArchType;
            ClsSetInfo.UpdateInfo(str, 2, dgvInfo1_one, dgvInfo1_two);
        }

        private void pict3_Click(object sender, EventArgs e)
        {
            if (dgvInfo2_one.Rows.Count <= 0 || dgvInfo2_two.Rows.Count <= 0)
                return;
            if (dgvInfo2_one.CurrentRow.Index < 0) {
                MessageBox.Show("请先选择正确的数据行!");
                return;
            }
            string str = ClsTable.ArchType;
            ClsSetInfo.UpdateInfo(str, 1, dgvInfo2_one, dgvInfo2_two);
        }

        private void pict4_Click(object sender, EventArgs e)
        {
            if (dgvInfo2_one.Rows.Count <= 0 || dgvInfo2_two.Rows.Count <= 0)
                return;
            if (dgvInfo2_two.CurrentRow.Index <= 0) {
                MessageBox.Show("请先选择正确的数据行!");
                return;
            }
            string str = ClsTable.ArchType;
            ClsSetInfo.UpdateInfo(str, 2, dgvInfo2_one, dgvInfo2_two);
        }


        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            ClsTable.Archid = gArchSelect1.Archid;
            ClsTable.ArchType = gArchSelect1.Archtype;
            if (ClsTable.Archid <= 0 || ClsTable.ArchType.Trim().Length<=0)
                return;
            dgvInfo1_one.DataSource = null;
            dgvInfo1_two.DataSource = null;
            dgvInfo2_one.DataSource = null;
            dgvInfo2_two.DataSource = null;
            if (ClsTable.Ac == false) {
                Action Act = GetInfo;
                Act.BeginInvoke(null, null);
            }
        }

        private void InfoCheck()
        {
            bool archok1 = false;
            bool archok2 = false;
            if (dgvInfo1_one.Rows.Count > 0 || dgvInfo1_two.Rows.Count > 0) {
                if (dgvInfo1_one.Rows.Count <= 0 || dgvInfo1_two.Rows.Count <= 0) {

                    string str = "表：" + ClsTable.ArchType + " 缺少一录或二录信息，请先补录";
                    MessageBox.Show(str);
                    return;
                }
                if (dgvInfo1_one.Rows.Count > 0 && dgvInfo1_two.Rows.Count > 0) {
                    archok1 = ClsSetInfo.ArchidCheck(dgvInfo1_one, dgvInfo1_two);
                }
            }
            if (dgvInfo2_one.Rows.Count > 0 || dgvInfo2_two.Rows.Count > 0) {
                if (dgvInfo2_one.Rows.Count <= 0 || dgvInfo2_two.Rows.Count <= 0) {
                    string str = "表：" + ClsTable.ArchType + " 缺少一录或二录信息，请先补录";
                    MessageBox.Show(str);
                    return;
                }
                else if (dgvInfo2_one.Rows.Count > 0 && dgvInfo2_two.Rows.Count > 0) {
                    archok2 = ClsSetInfo.ArchidCheck(dgvInfo1_one, dgvInfo1_two);
                }
            }
            if (archok1 == true || archok2 == true) {
                ClsSetInfo.SetXyinfo(1);
            }
        }
        private void butCheck_Click(object sender, EventArgs e)
        {
            if (ClsTable.Archzt) {
                if (MessageBox.Show("本卷已校验完成,需要重新校验吗?", "警告", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                    InfoCheck();
                }
                return;
            }
            InfoCheck();
        }


        #endregion


    }
}
