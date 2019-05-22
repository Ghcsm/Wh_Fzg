using DAL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WareHouse
{
    public partial class StoreView : Form
    {
        public StoreView()
        {
            InitializeComponent();
        }

        private StoreImg strImg;
        HLFtp.HFTP ftp = new HLFtp.HFTP();



        private void LoadMjj()
        {
            if (ClsStore.Liesn < 1 || ClsStore.Cengsn < 1) {
                MessageBox.Show("此柜不存在或库房设置错误参数无法加载!");
                return;
            }
            this.PanleHouseBox.Controls.Clear();
            this.PanleHouseJuan.Controls.Clear();
            int num = Common.IsGuiCount(ClsStore.Absn);
            int GroupW = PanleHouseMj.Width;
            int GroupH = PanleHouseMj.Height;
            for (int i = 1; i <= ClsStore.Cengsn; i++) {
                for (int j = 1; j <= ClsStore.Liesn; j++) {
                    Application.DoEvents();
                    ClsStore.but_mjj[i, j] = new DevComponents.DotNetBar.ButtonX
                    {
                        Name = "but" + i.ToString() + j.ToString(),
                        Size = new Size(GroupW / ClsStore.Liesn, GroupH / ClsStore.Cengsn - 2)
                    };
                    ClsStore.but_mjj[i, j].Location = new Point((j - 1) * (ClsStore.but_mjj[i, j].Width + 2), (i - 1) * (ClsStore.but_mjj[i, j].Height + 2));
                    if (num <= 0)
                        ClsStore.but_mjj[i, j].BackgroundImage = imglist_mjj.Images[0];
                    else {
                        int num1 = Common.IsGuiColRowsCount(ClsStore.Absn, i, j);
                        if (num1 == ClsStore.Boxsn)
                            ClsStore.but_mjj[i, j].BackgroundImage = imglist_mjj.Images[1];
                        else if (num1 == 0)
                            ClsStore.but_mjj[i, j].BackgroundImage = imglist_mjj.Images[0];
                        else {
                            ClsStore.but_mjj[i, j].BackgroundImage = imgListmjj2.Images[0];
                            ClsStore.but_mjj[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                    ClsStore.but_mjj[i, j].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
                    ClsStore.but_mjj[i, j].Click += new System.EventHandler(but_mjj_Click);
                    this.PanleHouseMj.Controls.Add(ClsStore.but_mjj[i, j]);
                }
            }

            ClsStore.ObjHouseColRow = null;
            ClsStore.Objboxsn = null;
            ClsStore.ObjJuan = null;
            ClsStore.SelectLiesn = 0;
            ClsStore.SelectCengsn = 0;
            ClsStore.SelectBoxsn = 0;
            ClsStore.SelectJuansn = 0;
            ClsStore.butmjjx = 0;
            ClsStore.butmjjy = 0;
            ClsStore.butboxx = 0;
            ClsStore.butjunax = 0;
            ClsStore.Archid = 0;
            ClsStore.ArchPos = "";
            labHouseGui.Text = string.Format("当前选择：第{0}柜", ClsStore.Guisn);
            labHouseCeng.Text = string.Format("当前选择：第{0}层", ClsStore.SelectCengsn);
            labHouseLie.Text = string.Format("当前选择：第{0}列", ClsStore.SelectLiesn);
            labHouseBox.Text = string.Format("当前选择：第{0}盒", ClsStore.SelectBoxsn);
            labHouseJuan.Text = string.Format("当前选择：第{0}卷", ClsStore.SelectJuansn);
            toolsBoxsn.Text = "";
            ToolsS_code.Text = string.Format("  案卷号：{0} ", GetJuanCode());
        }




        private void but_mjj_Click(object sender, EventArgs e)
        {
            ClsStore.ObjHouseColRow = sender;
            DevComponents.DotNetBar.ButtonX butbox = (DevComponents.DotNetBar.ButtonX)sender;
            ClsStore.SelectCengsn = Convert.ToInt32(butbox.Name.Substring(3, 1));
            ClsStore.SelectLiesn = Convert.ToInt32(butbox.Name.Substring(4));
            butbox.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
            if (ClsStore.SelectCengsn == ClsStore.butmjjx && ClsStore.SelectLiesn == ClsStore.butmjjy)
                butbox.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
            else {
                butbox.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
                if (ClsStore.butmjjx > 0 && ClsStore.butmjjy > 0)
                    ClsStore.but_mjj[ClsStore.butmjjx, ClsStore.butmjjy].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            }
            ClsStore.butmjjx = ClsStore.SelectCengsn;
            ClsStore.butmjjy = ClsStore.SelectLiesn;
            ClsStore.butboxx = 0;
            ClsStore.butjunax = 0;
            ClsStore.SelectBoxsn = 0;
            ClsStore.SelectJuansn = 0;
            ClsStore.Archid = 0;
            ClsStore.ArchPos = "";
            labHouseCeng.Text = string.Format("当前选择：第{0}层", ClsStore.SelectCengsn);
            labHouseLie.Text = string.Format("当前选择：第{0}列", ClsStore.SelectLiesn);
            labHouseBox.Text = string.Format("当前选择：第{0}盒", ClsStore.SelectBoxsn);
            labHouseJuan.Text = string.Format("当前选择：第{0}卷", ClsStore.SelectJuansn);
            toolsBoxsn.Text = "";
            ToolsS_code.Text = string.Format("  案卷号：{0} ", GetJuanCode());
            if (ClsStore.Boxsn < 1) {
                MessageBox.Show("盒号获取错误无法加载");
                return;
            }
            this.PanleHouseBox.Controls.Clear();
            int GroupW = this.PanleHouseBox.Width;
            int GroupH = this.PanleHouseBox.Height;
            for (int i = 1; i <= ClsStore.Boxsn; i++) {
                Application.DoEvents();
                int box = Getboxsn(ClsStore.Boxsn, ClsStore.SelectCengsn, ClsStore.SelectLiesn, i);
                int num = Common.IsGuiArchNoCount(ClsStore.Absn, ClsStore.SelectCengsn, ClsStore.SelectLiesn, box);
                ClsStore.but_boxsn[i] = new DevComponents.DotNetBar.ButtonX
                {
                    Name = "but" + i.ToString(),
                    Size = new Size((GroupW - 15) / 15, GroupH - 2)
                };
                ClsStore.but_boxsn[i].Location = new Point((i - 1) * (ClsStore.but_boxsn[i].Width + 1), 0);
                if (num == ClsStore.Juansn)
                    ClsStore.but_boxsn[i].BackgroundImage = imglist_box.Images[0];
                else if (num == 0)
                    ClsStore.but_boxsn[i].BackgroundImage = imglist_box.Images[2];
                else if (num < ClsStore.Juansn)
                    ClsStore.but_boxsn[i].BackgroundImage = imglist_box.Images[1];
                else
                    ClsStore.but_boxsn[i].BackgroundImage = imglist_box.Images[2];

                ClsStore.but_boxsn[i].BackgroundImageLayout = ImageLayout.Stretch;
                ClsStore.but_boxsn[i].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
                ClsStore.but_boxsn[i].Click += new System.EventHandler(butboxsn_Click);
                this.PanleHouseBox.Controls.Add(ClsStore.but_boxsn[i]);
            }
        }

        private void butboxsn_Click(object sender, EventArgs e)
        {
            ClsStore.Objboxsn = sender;
            DevComponents.DotNetBar.ButtonX butbox = (DevComponents.DotNetBar.ButtonX)sender;
            ClsStore.SelectBoxsn = Convert.ToInt32(butbox.Name.Substring(3));

            if (ClsStore.SelectBoxsn == ClsStore.butboxx)
                butbox.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;

            else {
                butbox.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
                if (ClsStore.butboxx > 0)
                    ClsStore.but_boxsn[ClsStore.butboxx].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            }
            ClsStore.butboxx = ClsStore.SelectBoxsn;
            ClsStore.SelectJuansn = 0;
            ClsStore.butjunax = 0;
            ClsStore.Archid = 0;
            ClsStore.ArchPos = "";
            toolsBoxsn.Text = Getboxsn().ToString();
            labHouseBox.Text = string.Format("当前选择：第{0}盒 ", ClsStore.SelectBoxsn);
            labHouseJuan.Text = string.Format("当前选择：第{0}卷", ClsStore.SelectJuansn);
            ToolsS_code.Text = string.Format("  案卷号：{0} ", GetJuanCode());
            if (ClsStore.Juansn < 1) {
                MessageBox.Show("案卷数量获取错误无法加载！");
                return;
            }
            this.PanleHouseJuan.Controls.Clear();
            int GroupW = this.PanleHouseJuan.Width;
            int GroupH = this.PanleHouseJuan.Height;
            for (int i = 1; i <= ClsStore.Juansn; i++) {
                Application.DoEvents();
                int box = Getboxsn(ClsStore.Boxsn, ClsStore.SelectCengsn, ClsStore.SelectLiesn, ClsStore.SelectBoxsn);
                int num = Common.IsGuiArchNoSate(ClsStore.Absn, ClsStore.SelectCengsn, ClsStore.SelectLiesn, box, i);
                ClsStore.but_juan[i] = new DevComponents.DotNetBar.ButtonX
                {
                    Name = "but" + i.ToString(),
                    Size = new Size((GroupW - 15) / 15, GroupH - 5)
                };
                ClsStore.but_juan[i].Location = new Point((i - 1) * (ClsStore.but_juan[i].Width + 1), 2);
                if (num == 0)
                    ClsStore.but_juan[i].BackgroundImage = imglist_box.Images[6];
                else if (num == 1)
                    ClsStore.but_juan[i].BackgroundImage = imglist_box.Images[5];
                else if (num == 2)
                    ClsStore.but_juan[i].BackgroundImage = imglist_box.Images[3];
                else if (num == 3)
                    ClsStore.but_juan[i].BackgroundImage = imglist_box.Images[4];
                ClsStore.but_juan[i].BackgroundImageLayout = ImageLayout.Stretch;
                ClsStore.but_juan[i].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
                ClsStore.but_juan[i].Click += new System.EventHandler(but_juan_Click);
                this.PanleHouseJuan.Controls.Add(ClsStore.but_juan[i]);
            }
        }

        private void but_juan_Click(object sender, EventArgs e)
        {
            ClsStore.ObjJuan = sender;
            DevComponents.DotNetBar.ButtonX butjuan = (DevComponents.DotNetBar.ButtonX)sender;
            ClsStore.SelectJuansn = Convert.ToInt32(butjuan.Name.Substring(3));
            if (ClsStore.SelectJuansn == ClsStore.butjunax)
                butjuan.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
            else {
                butjuan.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
                if (ClsStore.butjunax > 0)
                    ClsStore.but_juan[ClsStore.butjunax].ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            }
            ClsStore.butjunax = ClsStore.SelectJuansn;
            labHouseJuan.Text = string.Format("当前选择：第{0}卷", ClsStore.SelectJuansn);
            this.ToolsS_code.Text = string.Format("  案卷号：{0} ", GetJuanCode());
            ClsStore.ArchPos = ToolsS_code.Text.Substring(6);
            ClsStore.Archid = Common.GetCode(ClsStore.ArchPos);

        }


        private void LoadFile()
        {
            butOpenFile.Enabled = false;
            try {
                if (ClsStore.Archid == 0) {
                    MessageBox.Show("此案卷号不存在!");
                    return;
                }
                int ArchState = Common.GetArchCheckState(ClsStore.Archid);
                if (ArchState != 1) {
                    MessageBox.Show("此卷档案未质检无法进行查阅！");
                    return;
                }
                string FileName = Common.GetFileNameByArchID(ClsStore.Archid);
                toolFileName.Text = FileName;
                toolFileId.Text = ClsStore.Archid.ToString() + " ";
                string localPath = Path.Combine(Common.LocalTempPath, FileName.Substring(0, 8));
                string localCheckFile = Path.Combine(Common.LocalTempPath, FileName.Substring(0, 8), FileName);
                try {
                    if (!Directory.Exists(localPath)) {
                        Directory.CreateDirectory(localPath);
                    }

                    if (File.Exists(localCheckFile)) {
                        File.Delete(localCheckFile);
                    }
                } catch {
                }

                if (ftp.CheckRemoteFile(Common.ArchSavePah, FileName.Substring(0, 8), FileName)) {
                    if (ftp.DownLoadFile(Common.ArchSavePah, FileName.Substring(0, 8), localCheckFile, FileName)) {
                        StoreImg.ArchId = ClsStore.Archid;
                        StoreImg.FileName = localCheckFile;
                        StoreImg.ImgPrint = ClsStore.Imgsys;
                        strImg = new StoreImg();
                        if (strImg == null || strImg.IsDisposed) {
                            strImg = new StoreImg();
                        }
                        strImg.Activate();
                        strImg.ShowDialog();
                        return;
                    }

                    MessageBox.Show("文件下载失败!");
                    return;
                }

                MessageBox.Show("远程文件不存在!");
                return;
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            } finally {
                butOpenFile.Enabled = true;
            }
        }

        private void StoreView_Shown(object sender, EventArgs e)
        {
            ftp.PercentChane += new HLFtp.HFTP.PChangedHandle(Downjd);
            Getsys();
        }


        private int Getboxsn()
        {
            int box = 0;
            int tmpbox = 0;

            for (int i = 1; i <= ClsStore.SelectLiesn - 1; i++) {
                for (int j = 1; j <= ClsStore.Cengsn; j++) {
                    tmpbox += ClsStore.Boxsn;
                }
            }
            tmpbox += ClsStore.Boxsn * (ClsStore.SelectCengsn - 1);

            if (ClsStore.Guisn == 1) {
                box = ClsStore.Guisn * ClsStore.Absn * ClsStore.Liesn * ClsStore.Cengsn * ClsStore.Boxsn + tmpbox + ClsStore.SelectBoxsn;
            }
            else {
                box = (ClsStore.GMaxbox - ClsStore.Cengsn * ClsStore.Liesn * ClsStore.Boxsn * 2) + (ClsStore.Absn * ClsStore.Liesn * ClsStore.Cengsn * ClsStore.Boxsn + tmpbox + ClsStore.SelectBoxsn);
            }
            return box;
        }

        private string GetJuanCode()
        {
            string juan;
            string ab = null;
            if (rabHouseA.Checked)
                ab = "A";
            else
                ab = "B";
            juan = (000 + ClsStore.Guisn).ToString().PadLeft(2, '0') + ab + "-"
                   + (00 + ClsStore.SelectCengsn).ToString().PadLeft(2, '0') + "-"
                   + (00 + ClsStore.SelectLiesn).ToString().PadLeft(2, '0') + "-"
                   + (00 + ClsStore.SelectBoxsn).ToString().PadLeft(2, '0') + "-"
                   + (00 + ClsStore.SelectJuansn).ToString().PadLeft(2, '0');
            return juan;
        }

        private void Downjd(object sender, HLFtp.PChangeEventArgs e)
        {
            this.Tools_jd.Visible = true;
            this.Tools_jd.Minimum = 1;
            this.Tools_jd.Maximum = (int)e.CountSize;
            Application.DoEvents();
            this.Tools_jd.Value = (int)e.TmpSize;
            if (e.CountSize == e.TmpSize) {
                this.Tools_jd.Visible = false;
            }
        }

        private void StoreView_Load(object sender, EventArgs e)
        {
            ClsStore.Houseid = V_HouseSetCs.Houseid;
            ClsStore.Guisn = V_HouseSetCs.HouseGui;
            ClsStore.Absn = 0;
            ClsStore.Liesn = V_HouseSetCs.HouseCol;
            ClsStore.Cengsn = V_HouseSetCs.HouseRow;
            ClsStore.Boxsn = V_HouseSetCs.Housebox;
            ClsStore.Juansn = V_HouseSetCs.Housejuan;
            ClsStore.GMaxbox = V_HouseSetCs.HouseboxMax;
        }

        private void rabHouseA_Click(object sender, EventArgs e)
        {
            ClsStore.Absn = 0;
            this.PanleHouseMj.Controls.Clear();
            LoadMjj();
        }

        private void rabHouseB_Click(object sender, EventArgs e)
        {
            ClsStore.Absn = 1;
            this.PanleHouseMj.Controls.Clear();
            LoadMjj();
        }

        private void butOpenFile_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private int Getboxsn(int boxxh)
        {
            int box = 0;
            int tmpbox = 0;

            for (int i = 1; i <= ClsStore.SelectLiesn - 1; i++) {
                for (int j = 1; j <= ClsStore.Cengsn; j++) {
                    tmpbox += ClsStore.Boxsn;
                }
            }
            tmpbox += ClsStore.Boxsn * (ClsStore.SelectCengsn - 1);

            if (ClsStore.Guisn == 1) {
                box = ClsStore.Guisn * ClsStore.Absn * ClsStore.Liesn * ClsStore.Cengsn * ClsStore.Boxsn + tmpbox + boxxh;
            }
            else {
                box = (ClsStore.GMaxbox - ClsStore.Cengsn * ClsStore.Liesn * ClsStore.Boxsn * 2) + (ClsStore.Absn * ClsStore.Liesn * ClsStore.Cengsn * ClsStore.Boxsn + tmpbox + boxxh);
            }
            return box;
        }

        private int Getboxsn(int maxbox, int ceng, int lie, int boxxh)
        {
            int box = 0;
            int tmpbox = 0;

            for (int i = 1; i <= lie - 1; i++) {
                for (int j = 1; j <= ClsStore.Cengsn; j++) {
                    tmpbox += maxbox;
                }
            }
            tmpbox += maxbox * (ceng - 1);

            if (ClsStore.Guisn == 1) {
                box = ClsStore.Guisn * ClsStore.Absn * ClsStore.Liesn * ClsStore.Cengsn * maxbox + tmpbox + boxxh;
            }
            else {
                box = (ClsStore.GMaxbox - ClsStore.Cengsn * ClsStore.Liesn * maxbox * 2) + (ClsStore.Absn * ClsStore.Liesn * ClsStore.Cengsn * maxbox + tmpbox + boxxh);
            }
            return box;
        }
        private void ArchGrounid(int id)
        {
            if (id == 1) {
                Common.ArchGrounding(ClsStore.Houseid, ClsStore.Guisn, ClsStore.Absn, ClsStore.SelectCengsn, ClsStore.SelectLiesn, ClsStore.SelectBoxsn,
                    txtGroundNum.Text.Trim(), toolsBoxsn.Text.Trim());
            }
            else if (id == 2) {

                for (int bs = 1; bs <= ClsStore.Boxsn; bs++) {
                    int tbox = Getboxsn(bs);
                    Common.ArchGrounding(ClsStore.Houseid, ClsStore.Guisn, ClsStore.Absn, ClsStore.SelectCengsn, ClsStore.SelectLiesn, bs,
                        txtGroundNum.Text.Trim(), tbox.ToString());
                    Application.DoEvents();
                    labGround.Text = string.Format("已上架{0}盒", bs);
                }
            }
            else {
                for (int lie = 1; lie <= ClsStore.Liesn; lie++) {
                    for (int row = 1; row <= ClsStore.Cengsn; row++) {
                        for (int box = 1; box <= ClsStore.Boxsn; box++) {
                            int tbox = Getboxsn(ClsStore.Boxsn, row, lie, box);
                            Common.ArchGrounding(ClsStore.Houseid, ClsStore.Guisn, ClsStore.Absn, row, lie, box,
                                txtGroundNum.Text.Trim(), tbox.ToString());
                            Application.DoEvents();
                            labGround.Text = string.Format("已上架{0}盒", tbox);
                        }
                    }
                }
            }
        }

        private void EndCon(int id)
        {
            if (id == 0) {
                rabHouseA.Enabled = false;
                rabHouseB.Enabled = false;
                radioGroundCeng.Enabled = false;
                radioGroundAB.Enabled = false;
                radioGroundBoxsn.Enabled = false;
                butDown.Enabled = false;
                butUp.Enabled = false;
                butOpenFile.Enabled = false;
                labGround.Visible = true;
            }
            else {
                rabHouseA.Enabled = true;
                rabHouseB.Enabled = true;
                radioGroundCeng.Enabled = true;
                radioGroundAB.Enabled = true;
                radioGroundBoxsn.Enabled = true;
                butDown.Enabled = true;
                butUp.Enabled = true;
                butOpenFile.Enabled = true;
                labGround.Visible = false;
            }
        }

        private void Grding()
        {
            EndCon(0);
            try {
                if (txtGroundNum.Text.Trim().Length <= 0) {
                    MessageBox.Show("请输入上架卷数");
                    txtGroundNum.Focus();
                    return;
                }
                int ArchNum = Convert.ToInt32(txtGroundNum.Text.Trim());
                if (ClsStore.Juansn < ArchNum) {
                    MessageBox.Show("卷数超出限制！");
                    txtGroundNum.Focus();
                    return;
                }

                if (radioGroundBoxsn.Checked) {
                    if (toolsBoxsn.Text.Length <= 0) {
                        MessageBox.Show("请选择目标盒号!");
                        return;
                    }

                    ArchGrounid(1);
                    butboxsn_Click(ClsStore.Objboxsn, null);
                }
                else if (radioGroundCeng.Checked) {
                    ArchGrounid(2);
                    but_mjj_Click(ClsStore.ObjHouseColRow, null);
                }
                else {
                    ArchGrounid(3);
                    but_juan_Click(ClsStore.ObjJuan, null);
                }
            } catch (Exception e) {
                MessageBox.Show("上架失败:" + e.ToString());
            } finally {
                EndCon(1);
            }

        }

        private void butUp_Click(object sender, EventArgs e)
        {

            if (!ClsStore.Gdring) {
                MessageBox.Show("警告，您没有此项操作权限!");
                return;
            }
            Grding();
        }


        private void butDown_Click(object sender, EventArgs e)
        {
            if (!ClsStore.Gdring) {
                MessageBox.Show("警告，您没有此项操作权限!");
                return;
            }
            if (ClsStore.SelectJuansn <= 0) {
                MessageBox.Show("请选择要下架的案卷！");
                return;
            }
            if (MessageBox.Show("您确认要删除此卷吗?", "下架", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;

            if (toolFileName.Text.Length > 0) {
                MessageBox.Show("此卷档案已有数据无法下架!");
                return;
            }
            if (ClsStore.Archid <= 0) {
                MessageBox.Show("获取案卷ID失败，无法下架!");
                return;
            }
            Common.ArchDel(ClsStore.Archid);
        }

        private void Getsys()
        {
            ClsStore.Gdring = false;
            ClsStore.Imgsys = false;
            ClsStore.Imgys = false;

            string str = DESEncrypt.DesEncrypt("库房上架");
            if (T_User.UserOtherSys.IndexOf(str) >= 0)
                ClsStore.Gdring = true;
            str = DESEncrypt.DesEncrypt("图像打印");
            if (T_User.UserOtherSys.IndexOf(str) >= 0)
                ClsStore.Imgsys = true;
            str = DESEncrypt.DesEncrypt("验收图像");
            if (T_User.UserOtherSys.IndexOf(str) >= 0)
                ClsStore.Imgys = true;
        }

    }
}
