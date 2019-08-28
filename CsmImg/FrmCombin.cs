using HLjscom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leadtools;

namespace CsmImg
{
    public partial class FrmCombin : Form
    {
        public FrmCombin()
        {
            InitializeComponent();
        }
        Hljsimage Himg = new Hljsimage();
        public string file = "";
        public int MaxPage = 0;
        public RasterImage NewImg;
        private void FrmCombin_Shown(object sender, EventArgs e)
        {
            ImageView.Image = null;
            Himg._CreateWidth(ImageView);
            txtPage.Focus();
        }

        void Loadimg(int p)
        {
            if (file.Trim().Length <= 0) {
                MessageBox.Show("图像文件名称不正确!");
                return;
            }
            Himg.LoadFloater(file, p, ImageView);
        }

        private void butLoad_Click(object sender, EventArgs e)
        {
            if (txtPage.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入加载图像的页码!");
                txtPage.Focus();
                return;
            }
            int p;
            bool bl = int.TryParse(txtPage.Text.Trim(), out p);
            if (!bl) {
                MessageBox.Show("请输入正确的图像页码!");
                txtPage.Focus();
                return;
            }
            if (p <= 0 || p > MaxPage) {
                MessageBox.Show("页码不存在!");
                txtPage.Focus();
                return;
            }
            Loadimg(p);
            txtPage.Text = "";
            txtPage.Focus();
        }

        private void butSider_Click(object sender, EventArgs e)
        {
            Himg._SidecropFloter(ImageView, 1);
        }

        private void butRectl_Click(object sender, EventArgs e)
        {
            Himg._RoteImageFolter(ImageView);
        }

        private void butZoomM_Click(object sender, EventArgs e)
        {
            Himg._SizeImgeFloter(ImageView, 1);
        }

        private void butZooms_Click(object sender, EventArgs e)
        {
            Himg._SizeImgeFloter(ImageView, 0);
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            ImageView.CombineFloater(false);
            ImageView.Floater = null;
            NewImg = ImageView.Image.Clone();
            this.Close();
        }

        private void ImageView_KeyDown(object sender, KeyEventArgs e)
        {
            if (ImageView.Floater == null)
                return;
            if (e.KeyData == Keys.Left)
                Himg._FloterMove(ImageView, 0);
            else if (e.KeyData == Keys.Right)
                Himg._FloterMove(ImageView, 1);
            else if (e.KeyData == Keys.Up)
                Himg._FloterMove(ImageView, 2);
            else if (e.KeyData ==Keys.Down)
                Himg._FloterMove(ImageView, 3);
        }
    }
}
