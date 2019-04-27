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
using System.IO;

namespace Csmdapx
{
    public partial class Fstit : Form
    {
        public Fstit()
        {
            InitializeComponent();
        }
        Hljsimage Hlimage = new Hljsimage();
        Point PointPic1;
        Point PointPic2;
        int ImgW = 0;       
        int LoadPage = 0;
        public static int MaxPage= 0;
        public static string SourFile = "";
        public static string NewFile = "";
        public static int Page1;
        public static int Page2;
        public static int id = 0;
        PictureBox Pictbox = new PictureBox();    

        private void but_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsNumber(txt_page.Text) == false)
                {
                    MessageBox.Show("输入页码错误!");
                    return;
                }            
                NewFile = Path.Combine(@"c:\temp\", LoadPage.ToString() + ".jpg");
                if (File.Exists(NewFile)==true)
                {
                    File.Delete(NewFile);
                }
                Hlimage._ImgPj(LoadPage, SourFile, NewFile);
                if (pic1.Image == null)
                {                   
                    pic1.Image = Image.FromFile(NewFile);
                    pic1.Visible = true;
                    Page1 = LoadPage;
                    return;
                }
                else if (pic2.Image == null)
                {                   
                    pic2.Image = Image.FromFile(NewFile);
                    pic2.Visible = true;
                    Page2 = LoadPage;
                    return;
                }
                MessageBox.Show("无法添加新的图像!");
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }           
        }

        private bool IsNumber(string page)
        {
            try
            {
                if (int.Parse(page) > 0)
                {
                    if (int.Parse(page) <= MaxPage)
                    {
                        LoadPage = int.Parse(page);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private Image JoinImage(Image Img1, Image Img2)
        {
            int imgHeight = 0, imgWidth = 0;
            imgWidth = Img1.Width + Img2.Width;
            int i1 = pic1.Top;
            int i2 = pic2.Top;          
            imgHeight = Math.Max(Img1.Height, Img2.Height);
            Bitmap joinedBitmap = new Bitmap(imgWidth, imgHeight);
            Graphics graph = Graphics.FromImage(joinedBitmap);
            graph.DrawImage(Img1, ImgW, 0, Img1.Width, Img1.Height);
            graph.DrawImage(Img2, Img1.Width, 0, Img2.Width, Img2.Height);
            return joinedBitmap;
        }


        private void pic1_MouseDown(object sender, MouseEventArgs e)
        {
            PointPic1 = e.Location;
        }

        private void pic1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newPos = pic1.Location;
                newPos.Offset(e.Location.X - PointPic1.X, e.Location.Y - PointPic1.Y);
                //保证拖动后控件还在当前窗体的可见范围内
                if (new Rectangle(0, 0, this.Width - but_add.Width, this.Height - but_add.Height * 5 / 2).Contains(newPos))
                {
                    pic1.Location = newPos;                  

                }
            }
        }

        private void pic1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newPos = pic1.Location;
                newPos.Offset(e.Location.X - PointPic1.X, e.Location.Y - PointPic1.Y);               
                if (new Rectangle(new Point(0, 0), this.Size).Contains(newPos))
                {
                    pic1.Location = newPos;
                    ImgW = (pic1.Image.Width / pic1.Width) * (pic1.Width - (pic2.Location.X - pic1.Location.X));  
                }
            }
        }

        private void pic2_MouseDown(object sender, MouseEventArgs e)
        {
            PointPic2 = e.Location;
        }

        private void pic2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newPos = pic2.Location;
                //适当调整button1的位置
                newPos.Offset(e.Location.X - PointPic2.X, e.Location.Y - PointPic2.Y);
                //保证拖动后控件还在当前窗体的可见范围内
                if (new Rectangle(0, 0, this.Width - but_add.Width, this.Height - but_add.Height * 5 / 2).Contains(newPos))
                {
                    pic2.Location = newPos;                  

                }
            }
        }

        private void pic2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newPos = pic2.Location;
                //适当调整button1的位置
                newPos.Offset(e.Location.X - PointPic2.X, e.Location.Y - PointPic2.Y);
                //保证拖动后控件还在当前窗体的可见范围内
                if (new Rectangle(new Point(0, 0), this.Size).Contains(newPos))
                {
                    pic2.Location = newPos;
                    ImgW = (pic1.Image.Width / pic1.Width) * (pic1.Width - (pic2.Location.X - pic1.Location.X));                   
                }
            }
        }
        private void but_Duiq_Click(object sender, EventArgs e)
        {
            pic2.Top = pic1.Top;
            pic2.Left = pic1.Left + pic1.Width;
            ImgW = (pic1.Image.Width / pic1.Width) * (pic1.Width - (pic2.Location.X - pic1.Location.X)); 
        }

        private void but_Save_Click(object sender, EventArgs e)
        {
            try
            {
              
                Pictbox.Image = (JoinImage(pic1.Image, pic2.Image));
                NewFile = Path.Combine(@"c:\Temp\", DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                if (File.Exists(NewFile) == true)
                {
                    File.Delete(NewFile);
                }
                Pictbox.Image.Save(NewFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("拼接成功！");
                pic1.Image = null;
                pic1.Visible = false;
                pic2.Image = null;
                pic2.Visible = false;
                id = 1;               
                this.Close();
            }
            catch
            {
                id = 0;
                MessageBox.Show("拼接失败");
            }
        }
        private void Fstit_Shown(object sender, EventArgs e)
        {
            id = 0;
        }

        private void but_sized_Click(object sender, EventArgs e)
        {
            pic1.Width += 20;
            pic1.Height += 20;
            pic2.Width += 20;
            pic2.Height += 20;
        }

        private void but_sizex_Click(object sender, EventArgs e)
        {
            pic1.Width -= 20;
            pic1.Height -= 20;
            pic2.Width -= 20;
            pic2.Height -= 20;
        }
    }
}
