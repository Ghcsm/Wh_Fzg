using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsmImg;
using HLjscom;

namespace Csmdapx
{
    public partial class FrmCombe : Form
    {
        public FrmCombe()
        {
            InitializeComponent();
        }
        Hljsimage Himg = new Hljsimage();
        public int ImgPage;
        public string file;
        private List<Bitmap> bmp = new List<Bitmap>();
        private void FrmCombe_Shown(object sender, EventArgs e)
        {
            bmp.Clear();
            lbImgPage.Items.Clear();
            txtPage.Focus();
        }

        private void butImgLoad_Click(object sender, EventArgs e)
        {
            if (txtPage.Text.Trim().Length <= 0)
                return;
            int p;
            bool bl = int.TryParse(txtPage.Text.Trim(), out p);
            if (!bl) {
                MessageBox.Show("页码不正确无法加载!");
                txtPage.Focus();
                return;
            }
            if (p >ImgPage) {
                MessageBox.Show("页码超出范围无法加载!");
                txtPage.Focus();
                return;
            }
            Bitmap b = Himg.Getbmp(file,p);
            if (b == null)
            {
                MessageBox.Show("加载图像文件失败!");
                txtPage.Focus();
                return;
            }
            bmp.Add(b);
            lbImgPage.Items.Add(p.ToString());
        }

        private void butImgcombe_Click(object sender, EventArgs e)
        {
            if (lbImgPage.Items.Count <= 0)
                return;
            if (bmp.Count <= 0)
                return;
            Istxt(false);
            pict1.Image = null;
            Action Act = Imgcombe;
            Act.BeginInvoke(null, null);

        }

        void Istxt(bool bl)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (!bl) {
                    butImgLoad.Enabled = false;
                    butImgcombe.Enabled = false;
                    butImgsave.Enabled = false;
                    pic2.Visible = true;
                    return;
                }
                butImgLoad.Enabled = true;
                butImgcombe.Enabled = true;
                butImgsave.Enabled = true;
                pic2.Visible = false;
                return;
            }));
        }

        void Imgcombe()
        {
            Task t = Task.Run(() =>
            {
                try {
                    Bitmap bip = ClsImg.ImgPj(bmp);
                    if (bip != null) {
                        this.Invoke(new Action(() => { pict1.Image = bip; }));
                    }
                    else
                        MessageBox.Show("拼接失败");
                } catch (Exception e) {
                    MessageBox.Show("拼接失败:" + e.ToString());
                }
            });
            Task.WaitAll(t);
            Istxt(true);
        }

        private void butImgsave_Click(object sender, EventArgs e)
        {
            if (lbImgPage.Items.Count <= 0)
                return;
            if (pict1.Image == null)
                return;
            for (int i = 0; i < lbImgPage.Items.Count; i++) {
                string str = lbImgPage.Items[i].ToString();
                Himg._DelePageTag(Convert.ToInt32(str));
            }
            string p = lbImgPage.Items[lbImgPage.Items.Count - 1].ToString();
            Image bmp = pict1.Image;
            Himg._Insterpage(bmp,Convert.ToInt32(p));
            this.Close();
        }
    }
}
