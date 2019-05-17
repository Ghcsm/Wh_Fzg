using CsmCon;
using DAL;
using System;
using System.IO;
using System.Windows.Forms;
using Convert = System.Convert;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WareHouse
{
    public partial class StoreImg : Form
    {
        public StoreImg()
        {
            InitializeComponent();
            Init();
        }

        private ImgBrow imgBrow1;
        public static int ArchId { get; set; }
        public static int Boxsn { get; set; }
        public static int Archno { get; set; }

        public static bool ContentsEnabled { get; set; }
        public static bool ModuleVisible { get; set; }
        public static string FileName { get; set; }

        public static bool ImgPrint { get; set; }



        private void Init()
        {
            ImgBrow.ArchId = ArchId;
            ImgBrow.FileName = FileName;
            ImgBrow.ContentsEnabled = false;
            ImgBrow.ModuleVisible = false;
            ImgBrow.Print = ImgPrint;
            ImgBrow.id = 0;
            imgBrow1 = new ImgBrow();
            imgBrow1.Dock = DockStyle.Fill;
            gr1.Controls.Add(imgBrow1);
            gr1.Refresh();
        }


        private void StoreImg_Load(object sender, EventArgs e)
        {
            imgBrow1.LoadFile(ArchId, FileName);
            imgBrow1.LoadConten(ArchId);
        }

        private void toolsPrivePages_Click(object sender, EventArgs e)
        {
            imgBrow1.PrivePage();
        }

        private void toolsNextPages_Click(object sender, EventArgs e)
        {
            imgBrow1.NextPage();
        }

        private void toolsBigPages_Click(object sender, EventArgs e)
        {
            imgBrow1.BigPage();
        }

        private void toolsSmallPages_Click(object sender, EventArgs e)
        {
            imgBrow1.SmallPage();
        }

        private void toolsRoate_Click(object sender, EventArgs e)
        {
            imgBrow1.RotePage();
        }

        private void toolsColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Imgclose()
        {
            imgBrow1.Close();
            try {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Directory.Exists(Path.GetDirectoryName(FileName));
                Directory.Delete(Path.GetDirectoryName(FileName));
            } catch  {}
        }

        private void StoreImg_FormClosing(object sender, FormClosingEventArgs e)
        {
            Imgclose();
        }

        private void txtPage1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }

        private void txtPages2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }

        private bool isTxt()
        {
            if (!ImgPrint) {
                MessageBox.Show("警告，您没有此项操作的权限!");
                return false;
            }
            if (txtPage1.Text.Trim().Length <= 0 || txtPages2.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入起始页码及终止页码!");
                return false;
            }

            if (txtPage1.Text.Trim() == "0" || txtPages2.Text.Trim() == "0") {
                MessageBox.Show("请输入正确的页码!");
                return false;
            }

            return true;
        }

        private void toolsPrintPages_Click(object sender, EventArgs e)
        {
            if (!isTxt())
                return;
            int p1 = Convert.ToInt32(txtPage1.Text.Trim());
            int p2 = Convert.ToInt32(txtPages2.Text.Trim());
            imgBrow1.PrintImg(p1, p2);
        }

        private void toolsImgOK_Click(object sender, EventArgs e)
        {
            if (imgBrow1.ImgNull() == true) {
                if (!ClsStore.Imgys) {
                    MessageBox.Show("您没有权限验收图像!");
                    return;
                }
                Common.QuerSetCheckLog(ArchId, Boxsn, Archno, 1, "视图");
                MessageBox.Show("设置完成");
            }
        }

        private void toolsImgNo_Click(object sender, EventArgs e)
        {
            if (imgBrow1.ImgNull() == true) {
                if (!ClsStore.Imgys) {
                    MessageBox.Show("您没有权限验收图像!");
                    return;
                }
                Common.QuerSetCheckLog(ArchId, Boxsn, Archno, 2, "视图");
                MessageBox.Show("设置完成");
            }
        }
    }
}
