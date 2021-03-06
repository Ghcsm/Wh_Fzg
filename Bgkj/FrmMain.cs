﻿using DAL;
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
using Csmgy;

namespace Bgkj
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        Csjid cid = new Csjid();

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = T_ConFigure.SfName;
            this.labUser.Text = string.Format("    当前用户： {0}    ", T_User.LoginName);
            //DESEncrypt.DesDecrypt(T_ConFigure.SfCoName);
            string str = T_ConFigure.SfCoName;
            if (str.Trim().Length <= 0)
                Application.Exit();
            this.labCo.Text = " " + str;
            T_ConFigure.Bgsoft = false;
            GetHouseName();
            bool x = await IniMenu();
            if (x)
                InitModule();
        }
        #region

        Task<bool> IniMenu()
        {
            return Task.Run(() =>
            {
                DataTable dt = T_Sysset.GetMenuSet();
                if (dt == null || dt.Rows.Count <= 0)
                    return false;
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string strname = dt.Rows[i][0].ToString();
                    if (strname.Trim().Length <= 0)
                        continue;
                    if (T_User.UserMenu.IndexOf(DESEncrypt.DesEncrypt(strname)) < 0)
                        continue;
                    SideBarPanelItem item = new SideBarPanelItem();
                    item.Name = strname;
                    item.Text = strname;
                    item.FontBold = true;
                    sideBarManger.Invoke(new Action(() =>
                    {
                        sideBarManger.Panels.Add(item);
                        sideBarManger.Refresh();
                    }));
                }
                return true;
            });

        }

        void InitModule()
        {
            DataTable dt = T_Sysset.IsGetModule(1);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            Task.Run(() =>
            {
                foreach (DataRow dr in dt.Rows) {
                    int idGroup = Convert.ToInt32(dr["ModuleInt"].ToString());
                    string nameChModule = dr["ModuleChName"].ToString();
                    if (T_User.UserSys.IndexOf(DESEncrypt.DesEncrypt(nameChModule)) < 0)
                        continue;
                    string nameModule = DESEncrypt.DesDecrypt(dr["ModuleName"].ToString());
                    string nameModuleFile = DESEncrypt.DesDecrypt(dr["ModuleFileName"].ToString());
                    int imgIdx = Convert.ToInt32(dr["ModuleImgIdx"].ToString());
                    string menuModule = dr["ModuleMenuName"].ToString();
                    ButtonItem item = new ButtonItem
                    {
                        Name = nameModule,
                        Tag = nameModuleFile,
                        ImagePosition = eImagePosition.Top,
                        ImageIndex = imgIdx
                    };
                    item.Click += Item_Click;
                    sideBarManger.Invoke(new Action(() =>
                    {
                        item.Text = nameChModule;
                        for (int i = 0; i < sideBarManger.Panels.Count; i++) {
                            string str = sideBarManger.Panels[i].Text;
                            if (str.IndexOf(menuModule) >= 0)
                                sideBarManger.Panels[i].SubItems.Add(item);
                        }
                        sideBarManger.Refresh();
                    }));
                }
                ButtonItem item1 = new ButtonItem
                {
                    Name = "FrmModuleSet",
                    ImagePosition = eImagePosition.Top,
                    ImageIndex = 21
                };
                item1.Click += Item_Click;
                sideBarManger.BeginInvoke(new Action(() =>
                {
                    item1.Text = "模块授权设置";
                    for (int i = 0; i < sideBarManger.Panels.Count; i++) {
                        string str = sideBarManger.Panels[i].Text;
                        if (str.IndexOf("系统") >= 0)
                            sideBarManger.Panels[2].SubItems.Add(item1);
                    }
                    sideBarManger.Refresh();
                }));
            });
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
                try
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
                                ClsIsinfo.Istime();
                                return;
                            }
                        }
                    }
                    T_ConFigure.Bgsoft = true;
                    this.BeginInvoke(new Action(() => { t.Enabled = true; }));
                    ClsIsinfo.Istime();
                }
                catch 
                {
                    T_ConFigure.Bgsoft = true;
                    this.BeginInvoke(new Action(() => { t.Enabled = true; }));
                    ClsIsinfo.Istime();
                }
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
                MessageBox.Show(DESEncrypt.DesDecrypt("jCYeMNb2MK7CCH4XVR2HyqqfcTePyp0XOjr8U7kIHp/6tmtJp7lHozncfoZPiDwq"));
                this.labDate.Text = " 请先激活软件....";
                this.labDate.BackColor = Color.Red;
            }
            else {
                this.labDate.Text = " 请先激活软件....";
                this.labDate.BackColor = Color.Red;
            }
        }


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
                    fm.Text = name;
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

        private void Setitems()
        {
            for (int i = 0; i < bar3.Items.Count; i++) {
                bar3.Items[i].Visible = false;
                bar3.SelectedDockTab = i;
                bar3.Refresh();
            }
        }


        private void Item_Click(object sender, EventArgs e)
        {
            ButtonItem but = (ButtonItem)sender;
            string Fname = but.Name;
            string name = but.Text;
            if (T_ConFigure.Bgsoft)
                Setitems();
            if (T_User.UserId == 1 && Fname.Contains("FrmModuleSet")) {
                if (T_ConFigure.Bgsoft)
                    showerr(2);
                FrmSet Fset = new FrmSet();
                ClsSetInfopar.imList = imgListManger;
                Fset.ShowDialog();
                return;
            }
            else if (T_User.UserId == 1 && Fname.Contains("Csmgy.Frmgy")) {
                if (T_ConFigure.Bgsoft)
                    showerr(2);
                Frmgy gy = new Frmgy();
                ClsSetInfopar.imList = imgListManger;
                gy.ShowDialog();
                return;
            }
            else if (T_User.UserId != 1 && Fname.Contains("FrmModuleSet")) {
                MessageBox.Show("此模块只有管理员可以操作!");
                return;
            }
            string path = "";
            if (!T_ConFigure.Bgsoft)
                path = but.Tag.ToString();
            else {
                path = DESEncrypt.DesEncrypt(but.Tag.ToString());
                Fname = DESEncrypt.DesEncrypt(Fname);
            }
            if (T_ConFigure.Bgsoft)
                showerr(2);
            if (Fname.Contains("FrmModuleSet") || Fname.Contains("Csmgy.Frmgy")
              || Fname.Contains("Csmxtpz.Frmxtpz") || Fname.Contains("Update.FrmUpate")) {
                Assembly outerAsm = Assembly.LoadFrom(path);
                Type outerForm = outerAsm.GetType(Fname, false);
                Form fm = Activator.CreateInstance(outerForm) as Form;
                fm.TopLevel = true;
                fm.ShowDialog();
                return;
            }
            CreateForms(name, Fname, path);
        }

        private void bar3_Closing(object sender, BarClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Setitems();
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
                if (Common.Gettask() > 0) {
                    MessageBox.Show("任务正在进行中请稍候关闭程序!");
                    e.Cancel = true;
                    return;
                }
                Common.SetloginIp(0);
            } catch { }
        }
    }
}
