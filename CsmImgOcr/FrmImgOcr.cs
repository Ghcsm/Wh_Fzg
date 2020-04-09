using HLjscom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsmImgOcr
{
    public partial class FrmImgOcr : Form
    {
        public FrmImgOcr()
        {
            InitializeComponent();
        }
        Hljsimage Himg = new Hljsimage();
        private void FrmImgOcr_Load(object sender, EventArgs e)
        {
            try {
                Himg._Instimagtwain(this.ImgView, this.Handle, 1);
                Himg._Rectang(true);
            } catch (Exception ex) {
                MessageBox.Show("初始化失败请重新加载" + ex.ToString());
                Himg.Dispose();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Himg._Twainscan(0);
        }
    }
}
