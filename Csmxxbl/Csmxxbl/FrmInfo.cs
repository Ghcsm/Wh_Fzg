using CsmCon;
using System;
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
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            int xyid =Convert.ToInt32(gArchSelect1.Archxystat);
            if (xyid >=1 )
            {
                MessageBox.Show("数据已校验完成无法进行修改!");
                return;
            }
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            ucInfo.SaveInfo(arid, entertag);
        }

        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            string type = gArchSelect1.Archtype;
            if (arid <= 0)
                return;
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
    }
}
