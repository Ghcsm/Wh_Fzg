using System;
using HLjscom;
using System.IO;
using System.Windows.Forms;

namespace CsmCon
{
    public partial class ImgBrow : UserControl
    {
        public ImgBrow()
        {
            InitializeComponent();
            Himg.Spage += new Hljsimage.ScanPage(ShowPage);
            Init(id);
        }

        private UcContents ucContents1;
        private UcConten ucContents0;
        Hljsimage Himg = new Hljsimage();
        public delegate void TransmitPar(int page, int counpage);
        public event TransmitPar Spage;

        public static int ArchId { get; set; }
        public static bool ContentsEnabled { get; set; }
        public static bool ModuleVisible { get; set; }
        public static string FileName { get; set; }
        public static bool Print { get; set; }
        public static int id { get; set; }


        private int MaxPages;
        private int PagesCrren;


        public void Init(int id)
        {
            Himg._Instimagtwain(ImgView, this.Handle, 0);
            if (id == 0) {
                UcConten.Archid = ArchId;
                ucContents0 = new UcConten();
                ucContents0.Dock = DockStyle.Fill;
                ucContents0.OneClickGotoPage += UcContents0_OneClickGotoPage;
                gr0.Controls.Add(ucContents0);
            }
            else {
                UcContents.ArchId = ArchId;
                UcContents.ContentsEnabled = ContentsEnabled;
                UcContents.ModuleVisible = ModuleVisible;
                ucContents1 = new UcContents();
                ucContents1.Dock = DockStyle.Fill;
                ucContents1.OneClickGotoPage += UcContents1_OneClickGotoPage;
                gr0.Controls.Add(ucContents1);
            }
            LoadFile();
        }

        private void UcContents1_OneClickGotoPage(object sender, EventArgs e)
        {
            int p = UcConten.ArchPages;
            if (p >= 1 && p < MaxPages)
                Himg._Gotopage(p);
        }

        private void UcContents0_OneClickGotoPage(object sender, EventArgs e)
        {
            int p = UcConten.ArchPages;
            if (p >= 1 && p < MaxPages)
                Himg._Gotopage(p);
        }

        public void LoadFile(int arid, string file)
        {
            if (arid <= 0)
                return;
            ArchId = arid;
            FileName = file;
            LoadFile();
        }

        public void LoadConten(int arid)
        {
            if (id == 0)
                ucContents0.LoadConten(ArchId);
            else
                ucContents1.LoadContents(ArchId, 0);
        }

        private void LoadFile()
        {
            try {
                if (FileName.Length <= 0)
                    return;
                else {
                    Himg.Filename = FileName;
                    Himg.LoadPage(1);
                }
            } catch { ImgView.Image = null; }
        }

        public void Setpage(int page, int counpage)
        {
            if (Spage != null) {
                Spage(page, counpage);
            }
        }


        private void ShowPage(int page, int counpage)
        {
            PagesCrren = page;
            MaxPages = counpage;
            Setpage(page, counpage);
        }

        public void NextPage()
        {
            if (ImgView.Image != null) {
                if (PagesCrren < MaxPages)
                    Himg._Pagenext(1);
            }
        }

        public void PrivePage()
        {
            if (ImgView.Image != null) {
                if (PagesCrren > 1)
                    Himg._Pagenext(0);
            }
        }

        public void BigPage()
        {
            if (ImgView.Image != null)
                Himg._Sizeimge(1);
        }

        public void SmallPage()
        {
            if (ImgView.Image != null)
                Himg._Sizeimge(0);
        }

        public void RotePage()
        {
            if (ImgView.Image != null)
                Himg._Roteimage(1);
        }

        private void ImgView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                NextPage();
        }

        private void ImgView_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            switch (keyCode) {
                case Keys.Enter:
                    NextPage();
                    break;
                case Keys.PageDown:
                    NextPage();
                    break;
                case Keys.Up:
                    PrivePage();
                    break;
            }
        }

        public void PrintImg(int img1, int img2)
        {
            if (!Print) {
                MessageBox.Show("警告，您没有此项操作权限！");
                return;
            }
            if (img1 <= 0 || img2 <= 0) {
                MessageBox.Show("页码不能小于等于0");
                return;
            }
            if (img1 > img2) {
                MessageBox.Show("起始页不能大于终止页!");
                return;
            }

            if (img2 > MaxPages) {
                MessageBox.Show("终止页不能大于总页码!");
                return;
            }
            if (ImgView.Image != null)
                Himg._PrintImg(img1, img2);
        }

        public bool ImgNull()
        {
            if (ImgView.Image == null)
                return false;
            else return true;
        }
        public void Close()
        {
            if (ImgView.Image != null) {
                if (id == 0)
                    ucContents0.CloseConten();
                else
                    ucContents1.CloseConten();
                ImgView.Image = null;
                ArchId = 0;
                try {
                    if (File.Exists(FileName)) {
                        File.Delete(FileName);
                        Directory.Delete(Path.GetDirectoryName(FileName));
                    }
                } catch { }
            }

        }
    }
}
