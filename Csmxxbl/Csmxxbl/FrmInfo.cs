using CsmCon;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Csmxxbl
{
    public partial class FrmInfo : Form
    {
        public FrmInfo()
        {
            InitializeComponent();
        }

        private UcInfoEnter ucInfo;
        private int entertag = 1;
        private bool chkbool = false;
        List<string> Lsinfo = new List<string>();
        int ts;

        private void FrmInfo_Shown(object sender, EventArgs e)
        {
            ucInfo = new UcInfoEnter();
            ucInfo.Dock = DockStyle.Fill;
            gr2.Controls.Add(ucInfo);
            ucInfo.GetInfoCol();
            try {
                chkbool = Convert.ToBoolean(ClsWriteini.ReadIni());
                chkInfo.Checked = chkbool;
            } catch {
                chkbool = false;
            }
            GetinfoSql();
        }

        private void GetinfoSql()
        {
            Lsinfo.Clear();
            DataTable dt = T_Sysset.GetInfoenterSql();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            string str = dt.Rows[0][2].ToString();
            if (str.Trim().Length <= 0)
                return;
            string[] str1 = str.Split(';');
            for (int i = 0; i < str1.Length; i++) {
                str = str1[i];
                if (str.Trim().Length <= 0)
                    continue;
                Lsinfo.Add(str);
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            int xyid = Convert.ToInt32(gArchSelect1.Archxystat);
            if (xyid >= 1) {
                MessageBox.Show("数据已校验完成无法进行修改!");
                return;
            }
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            int stat = Common.GetArchWorkState(arid);
            if (stat >= (int)T_ConFigure.ArchStat.质检完) {
                if (MessageBox.Show("此卷已质检完成,若强制修改补录信息系统将记录您的操作。", "强行修改信息", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK) {
                    return;
                }
                Common.Writelog(arid, "强制修改质检的录入信息!");
            }
            ucInfo.SaveInfo(arid, entertag);
            ts = Common.Getsx(arid, entertag);
            labsx.Text = string.Format("已录{0}手", ts);
            ucInfo.GetFocus();
        }

        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            string type = gArchSelect1.Archtype;
            if (arid <= 0)
                return;
            ucInfo.Archid = arid;
            ucInfo.LoadInfo(arid, entertag, type,out ts);
            labsx.Text = string.Format("已录{0}手", ts);
        }



        private void chkInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInfo.Checked) {
                chkInfo.Text = "二录信息";
                entertag = 2;
                ClsWriteini.WriteInt(chkbool.ToString());
                chkbool = false;
                return;
            }
            chkInfo.Text = "一录信息";
            entertag = 1;
            ClsWriteini.WriteInt(chkbool.ToString());
            chkbool = true;
        }

        private void chkInfo_Click(object sender, EventArgs e)
        {
            Frmpwd pwd = new Frmpwd();
            pwd.ShowDialog();
            if (Frmpwd.tf == false) {
                chkInfo.Checked = chkbool;
                return;
            }
        }

        private void gArchSelect1_LineFocus(object sender, EventArgs e)
        {
            if (ts <=0) {
                if (Lsinfo.Count <= 0) {
                    MessageBox.Show("请先配置后台数据!");
                    return;
                }
                FrminfoSql frmsql = new FrminfoSql();
                frmsql.lsinfo = Lsinfo;
                frmsql.ShowDialog();
                List<string> info = new List<string>();
                List<string> col = new List<string>();
                info = frmsql.lscolinfo;
                col = frmsql.lscol;
                ucInfo.LoadInfo(info, col);
            }
            ucInfo.GetFocus();
        }

        private void gArchSelect1_LineGetInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            string type = gArchSelect1.Archtype;
            if (arid <= 0)
                return;
            if (Lsinfo.Count <= 0) {
                MessageBox.Show("请先配置后台数据!");
                return;
            }
            FrminfoSql frmsql = new FrminfoSql();
            frmsql.lsinfo = Lsinfo;
            frmsql.ShowDialog();
            List<string> info = new List<string>();
            List<string> col = new List<string>();
            info = frmsql.lscolinfo;
            col = frmsql.lscol;
            ucInfo.LoadInfo(info, col);
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            int xyid = Convert.ToInt32(gArchSelect1.Archxystat);
            if (xyid >= 1) {
                MessageBox.Show("数据已校验完成无法进行修改!");
                return;
            }
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            int stat = Common.GetArchWorkState(arid);
            if (stat >= (int)T_ConFigure.ArchStat.质检完) {
                if (MessageBox.Show("此卷已质检完成,若强制修改补录信息系统将记录您的操作。", "强行修改信息", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK) {
                    return;
                }
                Common.Writelog(arid, "强制修改质检的录入信息!");
            }
            ucInfo.DelInfo(arid, entertag);
            ts = Common.Getsx(arid, entertag);
            labsx.Text = string.Format("已录{0}手", ts);
        }

        private void butBL_Click(object sender, EventArgs e)
        {
            if (gArchSelect1.Archid <= 0)
                return;
            FrmPage page = new FrmPage();
            page.Archid = gArchSelect1.Archid;
            page.ShowDialog();

        }

        private void butZd_Click(object sender, EventArgs e)
        {
            FrmZd zd=new FrmZd();
            zd.ShowDialog();
        }
    }
}
