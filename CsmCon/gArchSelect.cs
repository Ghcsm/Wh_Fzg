using DAL;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsmCon
{
    public partial class gArchSelect : UserControl
    {
        public gArchSelect()
        {
            InitializeComponent();
        }

        public delegate void ArchSelectHandle(object sender, EventArgs e);

        public delegate void ArchSelectHandleFocus(object sender, EventArgs e);

        public delegate void ArchSelectHandleGetInfo(object sender, EventArgs e);
        public delegate void ArchSelectHandleLoadFile(object sender, EventArgs e);

        public event ArchSelectHandle LineClickLoadInfo;
        public event ArchSelectHandleFocus LineFocus;
        public event ArchSelectHandleGetInfo LineGetInfo;
        public event ArchSelectHandleLoadFile LineLoadFile;

        public bool GotoPages { get; set; }
        public bool LoadFileBoole { get; set; }
        public bool PagesEnd { get; set; }
        public int Archid { get; set; }
        public int Boxsn { get; set; }
        public string Archtype { get; set; }
        public string ArchImgFile { get; set; }
        public int ArchRegPages { get; set; }

        public string ArchPos;

        private void Witeini()
        {
            ClsIni.Archbox = txtBoxsn.Text.Trim();
            ClsIni.ArchNo = comboxClass.Text.Trim();
            ClsIni.Rabchk = radioBoxsn.Checked.ToString();
            new ClsWriteini().WriteInt();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Query();
            Witeini();
            if (PagesEnd)
                txtPages.Enabled = false;
        }

        private bool istxt()
        {
            if (radioBoxsn.Checked) {
                try {
                    int boxNo = int.Parse(this.txtBoxsn.Text.Trim());
                } catch (Exception) {
                    MessageBox.Show("盒号输入错误");
                    this.txtBoxsn.Focus();
                    return false;
                }
            }
            else {
                gArchSelectInfo.garchid = 0;
                gArchSelectInfo.garchcol = "";
                if (gArchSelectInfo.gArchtable.Count <= 0) {
                    MessageBox.Show("请先后台设置查询字段!");
                    return false;
                }
                if (comboxClass.Text.Trim().Length <= 0 || txtBoxsn.Text.Trim().Length <= 0) {
                    MessageBox.Show("档案类型选择错误或盒号为空!");
                    this.comboxClass.SelectAll();
                    return false;
                }
                gArchSelectInfo.garchid = gArchSelectInfo.gArchTabch.IndexOf(comboxClass.Text.Trim());
                gArchSelectInfo.garchcol = gArchSelectInfo.gArchtableCol[gArchSelectInfo.garchid];
                string[] coltmp = gArchSelectInfo.garchcol.Split(';');
                string[] txttmp = txtBoxsn.Text.Trim().Split('-');
                if (coltmp.Length != txttmp.Length) {
                    MessageBox.Show("后台设置字段与查询信息长度不一致!");
                    return false;
                }
            }
            return true;
        }

        private void Query()
        {
            if (!istxt())
                return;
            DataTable dt;
            LvData.Items.Clear();
            if (radioBoxsn.Checked)
                dt = Common.QueryBoxsn(txtBoxsn.Text.Trim());
            else {

                string tb = gArchSelectInfo.gArchtable[gArchSelectInfo.garchid];
                string tbcol = gArchSelectInfo.garchcol;
                string strzd = txtBoxsn.Text.Trim();
                int arid = Common.QueryTableInfo(tb, tbcol, strzd);
                if (arid <= 0) {
                    MessageBox.Show("获取ID失败，此信息或不存在!");
                    return;
                }
                dt = Common.QueryBoxsn(arid);
            }

            if (dt != null && dt.Rows.Count > 0) {
                int i = 1;
                int stat = 0;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    string boxsn = dr["boxsn"].ToString();
                    string archno = dr["ArchNo"].ToString();
                    string arid = dr["ID"].ToString();
                    string pages = dr["PAGES"].ToString();
                    string type = dr["ArchType"].ToString();
                    string ImgFile = (dr["IMGFILE"] == null ? "" : dr["IMGFILE"].ToString());
                    stat = Convert.ToInt32((dr["ArchState"].ToString() == null ? "0" : dr["ArchState"].ToString()));
                    if (stat >= 3 && stat < 5)
                        lvi.ImageIndex = 0;
                    else if (stat >= 5 && stat < 7)
                        lvi.ImageIndex = 1;
                    else if (stat == 7)
                        lvi.ImageIndex = 2;
                    lvi.SubItems.AddRange(new string[] { boxsn, archno, ImgFile, pages, arid, type });
                    this.LvData.Items.Add(lvi);
                    i++;
                }
            }
        }

        private void radioBoxsn_Click(object sender, EventArgs e)
        {
            comboxClass.Enabled = false;
        }

        private void radioClass_Click(object sender, EventArgs e)
        {
            comboxClass.Enabled = true;
        }

        private void comboxClass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtBoxsn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void Getini()
        {
            try {
                if (File.Exists(new ClsWriteini().strFilePath)) {
                    ClsIni.strFile = Path.GetFileNameWithoutExtension(new ClsWriteini().strFilePath);
                    ClsIni.Archbox = (new ClsWriteini().ContentValue(ClsIni.strFile, "Archsn"));
                    ClsIni.ArchNo = (new ClsWriteini().ContentValue(ClsIni.strFile, "ArchQX"));
                    ClsIni.Rabchk = (new ClsWriteini().ContentValue(ClsIni.strFile, "Rabchk"));
                    this.BeginInvoke(new Action(() =>
                    {
                        txtBoxsn.Text = ClsIni.Archbox;
                        if (ClsIni.Rabchk == "true")
                            radioBoxsn.Checked = true;
                        else
                        {
                            comboxClass.Enabled = true;
                            radioClass.Checked = true;
                            int x = gArchSelectInfo.gArchTabch.IndexOf(ClsIni.ArchNo);
                            comboxClass.SelectedIndex = x+1;
                        }
                    }));

                }
            } catch { }
        }

        private void GetboxarchInfo()
        {
            gArchSelectInfo.gArchTabch.Clear();
            gArchSelectInfo.gArchtable.Clear();
            gArchSelectInfo.gArchtableCol.Clear();
            DataTable dt = T_Sysset.GetBoxsncolSet();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            comboxClass.BeginInvoke(new Action(() =>
            {
                comboxClass.Items.Clear();
                comboxClass.Items.Add("");
            }));
            for (int i = 0; i < dt.Rows.Count; i++) {
                string tb = dt.Rows[i][1].ToString();
                if (tb.Trim().Length <= 0)
                    continue;
                string tbch = dt.Rows[i][2].ToString();
                string tbcol = dt.Rows[i][3].ToString();
                gArchSelectInfo.gArchtable.Add(tb);
                gArchSelectInfo.gArchTabch.Add(tbch);
                gArchSelectInfo.gArchtableCol.Add(tbcol);
                comboxClass.Invoke(new Action(() => { comboxClass.Items.Add(tbch); }));

            }
            Getini();
        }

        private void gArchSelect_Load(object sender, EventArgs e)
        {
            Task.Run(new Action(() =>
            {
                GetboxarchInfo();
            }));
           
            if (!GotoPages)
                butLoad.Visible = false;
            else {
                butLoad.Visible = true;
                butPageUpdate.Visible = false;
                txtPages.Enabled = false;
            }
        }

        private void LvData_Click(object sender, EventArgs e)
        {
            if (LvData.SelectedItems.Count > 0 && LvData.SelectedIndices.Count > 0) {
                Archid = Convert.ToInt32(LvData.SelectedItems[0].SubItems[5].Text);
                Archtype = LvData.SelectedItems[0].SubItems[6].Text;
                string boxs = LvData.SelectedItems[0].SubItems[1].Text;
                string juan = LvData.SelectedItems[0].SubItems[2].Text;
                Boxsn = Convert.ToInt32(boxs);
                ArchPos = boxs + "-" + juan;
                string pags = Common.Getpages(Archid);
                if (pags.Length > 0) {
                    ArchRegPages = Convert.ToInt32(pags);
                    txtPages.Text = pags;
                }
                ArchImgFile = LvData.SelectedItems[0].SubItems[3].Text;
                if (LineClickLoadInfo != null)
                    LineClickLoadInfo(sender, new EventArgs());
            }
        }

        private void LvData_DoubleClick(object sender, EventArgs e)
        {
            if (LvData.SelectedItems.Count > 0 && LvData.SelectedIndices.Count > 0) {
                if (LineGetInfo != null)
                    LineGetInfo(sender, new EventArgs());
            }
        }

        private void LvData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!GotoPages)
                txtPages.Focus();
            else {
                if (!LoadFileBoole) {
                    if (LineFocus != null)
                        LineFocus(sender, new EventArgs());
                    return;
                }
                butLoad.Focus();
            }
        }

        private void txtPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                e.Handled = true;
            else if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void butLoad_Click(object sender, EventArgs e)
        {
            if (Archid <= 0)
                return;
            if (ArchRegPages <= 0) {
                MessageBox.Show("页码不正确");
                return;
            }
            if (LineLoadFile != null)
                LineLoadFile(sender, new EventArgs());
        }

        private void butOk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                butOk_Click(null, null);
                LvData.Focus();
            }
        }

        private void butPageUpdate_Click(object sender, EventArgs e)
        {

            try {
                int pages = Convert.ToInt32(txtPages.Text.Trim());
                if (pages <= 0)
                    return;
                Common.UpdatePages(txtPages.Text.Trim(), Archid);
                ArchRegPages = pages;
                if (LineFocus != null)
                    LineFocus(sender, new EventArgs());
            } catch {
                MessageBox.Show("更新页码失败!");
            }
        }
    }
}
