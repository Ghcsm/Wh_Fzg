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
            Ini();
        }
        UcContents ucContents1;
        private void Ini()
        {
            UcContents.ArchId =0;
            UcContents.ContentsEnabled = true;
            UcContents.ModuleVisible = false;
            ucContents1 = new UcContents();
            {
                ucContents1.Dock = DockStyle.Fill;
            }
            gr2.Controls.Add(ucContents1);
        }
        private void Frmmldj_Load(object sender, EventArgs e)
        {
           
        }

        private void Frmmldj_Shown(object sender, EventArgs e)
        {

        }

        private void gArchSelect1_LineFocus(object sender, EventArgs e)
        {

            UcContents.ArchId = gArchSelect1.Archid;
            UcContents.ArchMaxPage = gArchSelect1.ArchRegPages;
            ucContents1.txtId.Focus();

        }

        private void gArchSelect1_LineClickLoadInfo(object sender, EventArgs e)
        {
            int arid = gArchSelect1.Archid;
            if (arid <= 0)
                return;
            UcContents.ArchId = arid;
            UcContents.ArchMaxPage = gArchSelect1.ArchRegPages;
            ucContents1.LoadContents();
        }
       
    }
}
