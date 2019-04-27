using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace WareHouse
{
    public partial class StoreViewG : Form
    {
        public StoreViewG()
        {
            InitializeComponent();
        }
        int HouseGui;

        int[] Guibox = new int[10];
        private void GethouseGui()
        {
            GuiboxSz(0);
            LoadGui();
            LoadPagebut();
        }

        private void GuiboxSz(int id)
        {
            for (int i = 0; i < 10; i++)
            {
                if (id == 0)
                {
                    Guibox[i] = i + 1;
                }
                else if (id == 1)
                {
                    Guibox[i] -= 10;
                }
                else
                {
                    Guibox[i] += 10;
                }
            }
        }

        private void StoreViewG_Load(object sender, EventArgs e)
        {
            HouseGui = T_Sysset.GetHouseGuiMax(V_HouseSetCs.Houseid);
            GethouseGui();
        }

        private void LoadGui()
        {
            try
            {
                this.pan_gui.Controls.Clear();
                int GroupW = this.pan_gui.Width;
                int GroupH = this.pan_gui.Height;
                int Guibut = HouseGui + 10;
                DevComponents.DotNetBar.ButtonX[] but_gui = new DevComponents.DotNetBar.ButtonX[Guibut];

                for (int i = 0; i < 10; i++)
                {
                    int id = Guibox[i];
                    but_gui[i + 1] = new DevComponents.DotNetBar.ButtonX
                    {
                        Name = "but" + id.ToString(),
                        Text = id.ToString() + "号柜",
                        Font = new Font("宋体", 12, FontStyle.Bold),
                        Size = new Size(GroupW / 10, GroupH)
                    };
                    but_gui[i + 1].Location = new Point((i + 1 - 1) * (but_gui[i + 1].Width + 1), 0);
                    but_gui[i + 1].BackgroundImage = imageList_gui.Images[0];
                    but_gui[i + 1].BackgroundImageLayout = ImageLayout.Stretch;
                    but_gui[i + 1].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
                    this.pan_gui.Controls.Add(but_gui[i + 1]);
                    but_gui[i + 1].Click += new System.EventHandler(but_gui_Click);
                }
                this.pan_gui.Refresh();
                this.pan_gui.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadPagebut()
        {
            try
            {
                    int x = this.Width - 110;
                    int y = this.Height - 140;
                    DevComponents.DotNetBar.ButtonX[] but_Page = new DevComponents.DotNetBar.ButtonX[3];
                    for (int i = 1; i <= 2; i++)
                    {
                    but_Page[i] = new DevComponents.DotNetBar.ButtonX
                    {
                        Name = "but_page" + i.ToString(),
                        Size = new Size(80, 75)
                    };
                    if (i == 1)
                        {
                            but_Page[i].Location = new Point(5, y);
                            but_Page[i].BackgroundImage = imageList_page.Images[1];
                        }
                        else
                        {
                            but_Page[i].Location = new Point(x, y);
                            but_Page[i].BackgroundImage = imageList_page.Images[0];
                        }

                        but_Page[i].BackgroundImageLayout = ImageLayout.Stretch;
                        but_Page[i].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
                        but_Page[i].Click += new System.EventHandler(but_Page_Click);
                       this.Gr_gui.Controls.Add(but_Page[i]);
                    }
            }
            catch
            { }
        }

        private void but_Page_Click(object sender, EventArgs e)
        {
            try
            {
                DevComponents.DotNetBar.ButtonX bt = (DevComponents.DotNetBar.ButtonX)sender;
                if (bt.Name == "but_page1")
                {
                    if (Guibox[0] <= 1)
                    {
                        return;
                    }
                    GuiboxSz(1);
                    LoadGui();
                }
                else
                {
                    if (Guibox[9] < HouseGui)
                    {
                        GuiboxSz(2);
                        LoadGui();
                    }
                }
            }
            catch
            {
                MessageBox.Show("加载库房失败，请重新加载！");
                return;
            }
        }

        private void but_gui_Click(object sender, EventArgs e)
        {
            try
            {
                DevComponents.DotNetBar.ButtonX bt = (DevComponents.DotNetBar.ButtonX)sender;
                int g = Convert.ToInt32(bt.Name.Substring(3));
                V_HouseSetCs.HouseGui = g;
                T_Sysset.GetHouseGuiCs();
                StoreView st = new StoreView
                {
                    ShowInTaskbar = true
                };
                st.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
