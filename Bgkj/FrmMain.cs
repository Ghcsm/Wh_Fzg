using DAL;
using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Csmsjid;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;

namespace Bgkj
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        Csjid cid = new Csjid();
        private bool usersys;
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = T_ConFigure.SfName;
            this.labUser.Text = string.Format("    当前用户： {0}    ", T_User.LoginName);
            this.labCo.Text = " " + DESEncrypt.DesDecrypt(T_ConFigure.SfCoName);
            T_ConFigure.Bgsoft = false;
            GetHouseName();
            InitModule();
        }
        #region
        void InitModule()
        {
            DataTable dt = T_Sysset.IsGetModule(1);
            if (dt.Rows.Count > 0) {
                Task.Run(() =>
                {
                    foreach (DataRow dr in dt.Rows) {
                        int idGroup = Convert.ToInt32(dr["ModuleInt"].ToString());
                        string nameChModule = dr["ModuleChName"].ToString();
                        string nameModule = DESEncrypt.DesDecrypt(dr["ModuleName"].ToString());
                        string nameModuleFile = DESEncrypt.DesDecrypt(dr["ModuleFileName"].ToString());
                        int imgIdx = Convert.ToInt32(dr["ModuleImgIdx"].ToString());
                        if (idGroup == 1) {
                            ButtonItem item = new ButtonItem();
                            item.Name = nameModule;
                            item.Tag = nameModuleFile;
                            item.ImagePosition = eImagePosition.Top;
                            item.ImageIndex = imgIdx;
                            item.Click += Item_Click;
                            sideBarDataModule.BeginInvoke(new Action(() =>
                                {
                                    item.Text = nameChModule;
                                    sideBarDataModule.SubItems.Add(item);
                                }));

                        }
                        else if (idGroup == 2) {
                            ButtonItem item = new ButtonItem();
                            item.Name = nameModule;
                            item.Tag = nameModuleFile;
                            item.ImagePosition = eImagePosition.Top;
                            item.ImageIndex = imgIdx;
                            item.Click += Item_Click;
                            sideBarImgModule.BeginInvoke(new Action(() =>
                            {
                                item.Text = nameChModule;
                                sideBarImgModule.SubItems.Add(item);
                            }));
                        }
                        else if (idGroup == 3) {
                            ButtonItem item = new ButtonItem();
                            item.Name = nameModule;
                            item.Tag = nameModuleFile;
                            item.ImagePosition = eImagePosition.Top;
                            item.ImageIndex = imgIdx;
                            item.Click += Item_Click;
                            sideBarSysModule.BeginInvoke(new Action(() =>
                            {
                                item.Text = nameChModule;
                                sideBarSysModule.SubItems.Add(item);
                            }));
                        }
                        else if (idGroup == 4) {
                            ButtonItem item = new ButtonItem();
                            item.Name = nameModule;
                            item.Tag = nameModuleFile;
                            item.ImagePosition = eImagePosition.Top;
                            item.ImageIndex = imgIdx;
                            item.Click += Item_Click;
                            sideBarOtherModule.BeginInvoke(new Action(() =>
                            {
                                item.Text = nameChModule;
                                sideBarOtherModule.SubItems.Add(item);
                            }));
                        }
                    }
                    ButtonItem item1 = new ButtonItem();
                    item1.Name = "FrmModuleSet";
                    item1.ImagePosition = eImagePosition.Top;
                    item1.ImageIndex = 21;
                    item1.Click += Item_Click;
                    sideBarSysModule.BeginInvoke(new Action(() =>
                    {
                        item1.Text = "模块授权设置";
                        sideBarSysModule.SubItems.Add(item1);
                    }));
                });
                sideBarDataModule.Refresh();
                sideBarImgModule.Refresh();
                sideBarSysModule.Refresh();
                sideBarOtherModule.Refresh();
            }
            AcAbout();
        }
        #endregion

        private void GetHouseName()
        {
            List<V_HouseSet> HouseEc = new List<V_HouseSet>();
            DataTable dt = T_Sysset.GetHouseName();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                V_HouseSet House = new V_HouseSet();
                House.HouseID = Convert.ToInt32(dr["id"].ToString());
                House.HouseName = dr["HOUSENAME"].ToString();
                HouseEc.Add(House);
            }
            comHouse.Items.Clear();
            comHouse.Items.AddRange(HouseEc.ToArray());
            comHouse.SelectedItem = HouseEc;
            comHouse.SelectedIndex = 0;

        }

        private void AcAbout()
        {
            Task.Run(() =>
            {
                T_ConFigure.Moid = cid.Getid();
                if (T_ConFigure.Moid.Length < 28 || T_ConFigure.Moid == null || T_ConFigure.Moid.Length > 36) {
                    showerr(0);
                    Application.Exit();
                }
                T_Sysset.Getsoid(T_ConFigure.Moid);
                if (T_ConFigure.Mosn != null && T_ConFigure.Motm != null) {
                    if (T_ConFigure.Mosn.Length < 36 && T_ConFigure.Mosn.Length > 28 &&
                        T_ConFigure.Motm.Length < 65 && T_ConFigure.Motm.Length > 45) {
                        if (!cid.GetId(T_ConFigure.Mosn, T_ConFigure.Motm)) {
                            return;
                        }
                    }
                }
                T_ConFigure.Bgsoft = true;
                this.BeginInvoke(new Action(() => { t.Enabled = true; }));
            });
        }

        private void showerr(int id)
        {
            if (id == 0)
                MessageBox.Show(DESEncrypt.DesDecrypt("5pHKaw9paMJeZLjA4CfXwrThQRVwbLz6pgFEY1l4Fc0Gyl8gygnV7w=="));
            else if (id == 1)
                MessageBox.Show(DESEncrypt.DesDecrypt("meoLKAkxrxANmOfAwGS12V4KHBEJUa/1VIa+qHccp9s="));
            else if (id == 2)
                MessageBox.Show(DESEncrypt.DesDecrypt("meoLKAkxrxAX1zZKKGeWCYni/LGcTICQE5fGlDbVoHQP7pLeyq4z0Q=="));
            else if (id == 3) {
                MessageBox.Show(DESEncrypt.DesDecrypt("meoLKAkxrxAX1zZKKGeWCYni/LGcTICQE5fGlDbVoHQP7pLeyq4z0Q=="));
                this.labDate.Text = " 请先激活软件....";
                this.labDate.BackColor = Color.Red;
            }
            else {
                this.labDate.Text = " 请先激活软件....";
                this.labDate.BackColor = Color.Red;
            }
        }

        //设置窗体变化时 子窗体也变化 此问题未解决
        private void CreateForms(string name, string strnamespace, string FilePath)
        {
            try {
                bool isBar = false;
                for (int i = 0; i < bar3.Items.Count; i++) {
                    if (!bar3.Visible)
                        bar3.Visible = true;
                    string str = bar3.Items[i].Text;
                    if (str == name) {
                        bar3.Items[i].Visible = true;
                        bar3.SelectedDockTab = i;
                        bar3.Refresh();
                        isBar = true;
                        break;
                    }
                }
                if (!isBar) {
                    Assembly outerAsm = Assembly.LoadFrom(FilePath);
                    Type outerForm = outerAsm.GetType(strnamespace, false);
                    Form fm = Activator.CreateInstance(outerForm) as Form;
                    fm.TopLevel = false;
                    fm.Dock = DockStyle.Fill;

                    DockContainerItem item = null;
                    PanelDockContainer panel = new PanelDockContainer();
                    item = new DockContainerItem();

                    item.CanClose = eDockContainerClose.Yes;
                    item.Control = panel;
                    item.Text = name;
                    panel.Dock = DockStyle.Fill;
                    panel.Controls.Add(fm);
                    bar3.Items.Add(item);
                    fm.Show();
                    bar3.SelectedDockContainerItem = item;
                }
            } catch (Exception e) {
                MessageBox.Show("创建窗体失败!" + e.ToString());
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            usersys = true;
            ButtonItem but = (ButtonItem)sender;
            string Fname = but.Name;
            string name = but.Text;
        Labasys:
            if (!usersys) {
                showerr(1);
                return;
            }
            if (T_User.UserSys.IndexOf(DESEncrypt.DesEncrypt(name)) < 0 && T_User.UserId != 1)
                usersys = false;
            else usersys = true;
            if (usersys && T_ConFigure.Bgsoft && Fname.Contains("FrmModuleSet")) {
                showerr(3);
                FrmSet Fset = new FrmSet();
                ClsSetInfopar.imList = imgListManger;
                Fset.ShowDialog();
                return;
            }
            else if (usersys && T_ConFigure.Bgsoft && Fname.Contains("Csmgy.Frmgy")) {
                showerr(3);
            }
            if (usersys && Fname.Contains("FrmModuleSet")) {
                FrmSet Fset = new FrmSet();
                ClsSetInfopar.imList = imgListManger;
                Fset.ShowDialog();
                return;
            }
            if (!usersys)
                goto Labasys;
            string path = "";
            if (usersys && !T_ConFigure.Bgsoft)
                path = but.Tag.ToString();
            else if (!usersys)
                path = "@" + but.Tag.ToString() + "!";
            CreateForms(name, Fname, path);
        }

        private void bar3_Closing(object sender, BarClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.labDate.Text = string.Format("  当前时间： {0}  ", DateTime.Now.ToString());
        }

        private void t_Tick(object sender, EventArgs e)
        {
            showerr(4);
        }

        private void comHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            V_HouseSet v_house = comHouse.SelectedItem as V_HouseSet;
            V_HouseSetCs.Houseid = v_house.HouseID;
            V_HouseSetCs.HouseName = v_house.HouseName;
            T_ConFigure.gArchScanPath = T_ConFigure.FtpArchScan + v_house.HouseID;

        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            Action ac = HLFtp.HFTP.GetFtpInfo;
            ac();
            T_ConFigure.gArchScanPath = T_ConFigure.FtpArchScan + V_HouseSetCs.Houseid;
            Task.Run(new Action(() =>
            {
                T_ConFigure.IPAddress = SetLoginip();
                Common.SetloginIp(1);
                if (T_ConFigure.FtpIP == T_ConFigure.IPAddress)
                    T_ConFigure.FtpTmpPath = T_ConFigure.FtpFwqPath;
            }));
            
        }

        private string SetLoginip()
        {
            try {
                IPAddress localip = Dns.GetHostAddresses(Dns.GetHostName())
                    .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .First();
                return localip.ToString();
            } catch {
                return "";
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                Common.SetloginIp(0);
            } catch { }
        }
    }
}
