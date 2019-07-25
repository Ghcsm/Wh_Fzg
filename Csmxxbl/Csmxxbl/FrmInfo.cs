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
                MessageBox.Show("此卷已质检完成,不能再修改信息！");
                return;
            }
            ucInfo.SaveInfo(arid, entertag);
        }

        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            string type = gArchSelect1.Archtype;
            if (arid <= 0)
                return;
            ucInfo.Archid = arid;
            ucInfo.LoadInfo(arid, entertag, type);
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
            ucInfo.LoadInfo(info,col);
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
                MessageBox.Show("此卷已质检完成,不能再删除信息！");
                return;
            }
            ucInfo.DelInfo(arid, entertag);
        }
    }
}
