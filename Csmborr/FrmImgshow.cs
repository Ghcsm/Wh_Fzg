using CsmCon;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csmborr
{
    public partial class FrmImgshow : Form
    {
        public FrmImgshow()
        {
            InitializeComponent();
            Init();
        }

        public static int Arhcid { get; set; }
        public static string Filename { get; set; }
        public static bool ImgPrint { get; set; }
        private ImgBrow imgBrow1;
       
        private void Init()
        {
            ImgBrow.ArchId = Arhcid;
            ImgBrow.FileName = Filename;
            ImgBrow.ContentsEnabled = false;
            ImgBrow.ModuleVisible = false;
            ImgBrow.Print = ImgPrint;
            ImgBrow.id = 0;
            imgBrow1 = new ImgBrow();
            imgBrow1.Dock = DockStyle.Fill;
            gr.Controls.Add(imgBrow1);
            gr.Refresh();
        }

        private void FrmImgshow_Load(object sender, EventArgs e)
        {
            imgBrow1.LoadFile(Arhcid, Filename);
        }
        private void Imgclose()
        {
            imgBrow1.Close();
            try {
                if (File.Exists(Filename))
                    File.Delete(Filename);
                Directory.Exists(Path.GetDirectoryName(Filename));
                Directory.Delete(Path.GetDirectoryName(Filename));
            } catch {}
        }

        private bool isTxt()
        {
            if (!ImgPrint) {
                MessageBox.Show("警告，您没有此项操作的权限!");
                return false;
            }
            if (tooltxtpage1.Text.Trim().Length <= 0 || tooltxtpage2.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入起始页码及终止页码!");
                return false;
            }

            if (tooltxtpage1.Text.Trim() == "0" || tooltxtpage2.Text.Trim() == "0") {
                MessageBox.Show("请输入正确的页码!");
                return false;
            }

            return true;
        }

        private void FrmImgshow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Imgclose();
        }

        private void toolbutqian_Click(object sender, EventArgs e)
        {
            imgBrow1.PrivePage();
        }

        private void toolbuthou_Click(object sender, EventArgs e)
        {
            imgBrow1.NextPage();
        }

        private void toolbutmax_Click(object sender, EventArgs e)
        {
            imgBrow1.BigPage();
        }

        private void toolbutsize_Click(object sender, EventArgs e)
        {
            imgBrow1.SmallPage();
        }

        private void toolbutrote_Click(object sender, EventArgs e)
        {
            imgBrow1.RotePage();
        }

        private void toolbutprint_Click(object sender, EventArgs e)
        {
            if (!isTxt())
                return;
            
            int p1 = Convert.ToInt32(tooltxtpage1.Text.Trim());
            int p2 = Convert.ToInt32(tooltxtpage2.Text.Trim());
            Common.WriteBorrLog(BorrMethod.Boxsn, BorrMethod.Archno, BorrMethod.Archid, "打印图像:"+p1.ToString()+"-"+p2.ToString());
            imgBrow1.PrintImg(p1, p2);
        }

        private void toolbutclose_Click(object sender, EventArgs e)
        {
            Imgclose();
        }

        private void tooltxtpage1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }

        private void tooltxtpage2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }
    }
}
