using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CsmCon;
using DAL;

namespace Csmmldj
{
    public partial class Frmmldj : Form
    {

        public Frmmldj()
        {
            InitializeComponent();

        }
        UcContents ucContents1;
        UcInfoEnter ucInfo;

        private void Frmmldj_Load(object sender, EventArgs e)
        {
            splitCont.Panel2Collapsed = true;
            ContenInfPar.Infobl = Common.GetConteninfobl();
            UcContents.Modulename = this.Text;
            UcContents.ArchId = 0;
            UcContents.ContentsEnabled = true;
            UcContents.ModuleVisible = false;
            ucContents1 = new UcContents();
            {
                ucContents1.Dock = DockStyle.Fill;
            }
            gr2.Controls.Add(ucContents1);

            if (ContenInfPar.Infobl) {
                splitCont.Panel2Collapsed = false;
                Infoshow();
            }
            else
                splitCont.Panel2Collapsed = true;
        }

        void Infoshow()
        {
            ucInfo = new UcInfoEnter();
            ucInfo.Dock = DockStyle.Fill;
            grinfo.Controls.Add(ucInfo);
            ucInfo.GetInfoCol();
        }


        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            UcContents.ArchId = arid;
            UcContents.ArchMaxPage = gArchSelect1.ArchRegPages;
            UcContents.ArchStat = Convert.ToInt32(gArchSelect1.Archstat);
            ucContents1.LoadContents(arid, UcContents.ArchMaxPage);
            if (ContenInfPar.Infobl) {
                string type = gArchSelect1.Archtype;
                int p;
                ucInfo.LoadInfo(arid, 1, type, out p);
            }
            Getuser(arid);
        }

        private void Getuser(int arid)
        {
            DataTable dt = Common.GetOperator(arid);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            string enter = dr["录入"].ToString();
            string entertime = dr["录入时间"].ToString();
            tools_user.Text = "用户: " + enter;
            tools_time.Text = "录入时间: " + entertime;
        }

        private void butSaveInfo_Click(object sender, EventArgs e)
        {
            int xyid = Convert.ToInt32(gArchSelect1.Archxystat);
            if (xyid >= 1) {
                MessageBox.Show("数据已校验完成无法进行修改!");
                return;
            }
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            ucInfo.SaveInfo(arid, 1);
            ucContents1.Focus();
        }

        private void gArchSelect1_LineFocus(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            if (ContenInfPar.Infobl) {
                ucInfo.GetFocus();
                return;
            }
            ucContents1.Focus();
        }

        private void gArchSelect1_LineGetInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            FrmLsinfo lsinfo = new FrmLsinfo();
           
            lsinfo.ShowDialog();
            List<string> lsname = new List<string>();
            List<string> lsdata = new List<string>();
            lsname = FrmLsinfo.lsname;
            lsdata = FrmLsinfo.lsdata;
            if (lsname.Count <= 0 || lsdata.Count <= 0)
                return;
            ucInfo.Setlsinfo(lsname,lsdata);

        }
    }
}
