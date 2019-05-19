using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HLjscom;

namespace Csmdapx
{
    public partial class Frmwt : Form
    {
        public Frmwt()
        {
            InitializeComponent();
        }
        Hljsimage HlImage = new Hljsimage();
        public  static int wtid = 0;
        private void but_zuo_Click(object sender, EventArgs e)
        {
            toolsmem_left_Click(sender, null);
        }

        private void but_you_Click(object sender, EventArgs e)
        {
            toolsmem_right_Click(sender, null);
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            toolsmem_save_Click(sender, null);
        }

        private void Frmwt_Shown(object sender, EventArgs e)
        {
            wtid = 0;
            imgview.Zoom(Leadtools.Controls.ControlSizeMode.FitAlways, 1, imgview.DefaultZoomOrigin);
        }

        private void toolsmem_left_Click(object sender, EventArgs e)
        {
            HlImage._RoteimgWt(imgview, 0);
        }

        private void toolsmem_right_Click(object sender, EventArgs e)
        {
            HlImage._RoteimgWt(imgview, 1);
        }

        private void toolsmem_save_Click(object sender, EventArgs e)
        {
            wtid = 1;
            this.Close();
        }
    }
}
