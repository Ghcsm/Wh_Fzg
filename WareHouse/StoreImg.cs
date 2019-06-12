using CsmCon;
using DAL;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
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

        private void Init()
        {
            ImgBrow.ArchId = ArchId;
            ImgBrow.FileName = FileName;
            ImgBrow.ContentsEnabled = false;
            ImgBrow.ModuleVisible = false;
            ImgBrow.Print = ClsStore.Imgsys;
            ImgBrow.id = 0;
            imgBrow1 = new ImgBrow();
            imgBrow1.Spage += new ImgBrow.TransmitPar(ShowPage);
            imgBrow1.Dock = DockStyle.Fill;
            gr1.Controls.Add(imgBrow1);
            gr1.Refresh();
        }

        private void ShowPage(int page, int counpage)
        {
            toolslab_PagesCount.Text = string.Format("共{0}页", counpage);
            toolslab_PagesCrre.Text = string.Format("第{0}页", page);
        }

        private void StoreImg_Load(object sender, EventArgs e)
        {
            imgBrow1.LoadFile(ArchId, FileName);
            imgBrow1.LoadConten(ArchId);
            GetUser();
            GetArchInfo();
        }
        private void GetUser()
        {
            Task.Run(() =>
            {
                string Scanner = string.Empty;
                string Indexer = string.Empty;
                string Checker = string.Empty;
                DataTable dt = Common.GetOperator(ArchId);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                DataRow dr = dt.Rows[0];
                Scanner = dr["扫描"].ToString();
                Indexer = dr["排序"].ToString();
                Checker = dr["质检"].ToString();
                this.BeginInvoke(new Action(() =>
                {
                    toolslab_scanuser.Text = string.Format("扫描：{0}", Scanner);
                    toolslab_Indexuser.Text = string.Format("排序：{0}", Indexer);
                    toolslab_Checkuser.Text = string.Format("质检：{0}", Checker);

                }));
            });
        }

        private void GetArchInfo()
        {
            if (ClsStore.Imgyszt == "1")
                toolslabCheck.Text = "验收：完成";
            else if (ClsStore.Imgyszt == "2")
                toolslabCheck.Text = "验收：否决";
            else
                toolslabCheck.Text = "验收：未验收";
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
            } catch { }
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
            if (!ClsStore.Imgsys) {
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
