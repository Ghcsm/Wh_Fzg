using System;
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
        private void Ini()
        {

        }
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

        void InfoLoad()
        {

        }


        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            UcContents.ArchId = arid;
            UcContents.ArchMaxPage = gArchSelect1.ArchRegPages;
            ucContents1.LoadContents();
            if (ContenInfPar.Infobl) {
                string type = gArchSelect1.Archtype;
                ucInfo.LoadInfo(arid, 1, type);
            }
        }

        private void butSaveInfo_Click(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            ucInfo.SaveInfo(arid, 1);
        }
    }
}
