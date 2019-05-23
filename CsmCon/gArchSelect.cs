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
        #region const
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
        public string Archstat { get; set; }
        public string Archxystat { get; set; }

        public string ArchPos;

        #endregion

        #region click

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

        private void LvData_DoubleClick(object sender, EventArgs e)
        {
            if (LvData.SelectedItems.Count > 0 && LvData.SelectedIndices.Count > 0) {
                if (LineGetInfo != null)
                    LineGetInfo(sender, new EventArgs());
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
            if (e.KeyChar == 13)
                butOk_Click(null, null);

        }

        private void butPageUpdate_Click(object sender, EventArgs e)
        {

            if (Archstat.Trim().Length > 0) {
                int p = Convert.ToInt32(Archstat);
                if (p >= (int)T_ConFigure.ArchStat.排序完) {
                    MessageBox.Show("已经排序完成无法进行更改页码!");
                    LvData.Focus();
                    return;
                }
            }
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

        #endregion

        #region  void

        public bool GetFocus()
        {
            bool bl = false;
            foreach (Control ct in gr1.Controls) {
                if (ct is Panel) {
                    foreach (Control p in ct.Controls) {
                        if (p.Focused)
                            bl = true;
                    }
                }
                else {
                    if (ct.Focused)
                        bl = true;
                }
            }
            foreach (Control ct in gr2.Controls) {
                if (ct is Panel) {
                    foreach (Control p in ct.Controls) {
                        if (p.Focused)
                            bl = true;
                    }
                }
                else {
                    if (ct.Focused)
                        bl = true;
                }
            }
            return bl;
        }

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
            LvData.Focus();
            if (LvData.Items.Count > 0) {
                LvData.Items[0].Selected = true;
                LvData_Click(null, null);
            }
        }

        private bool istxt()
        {
            ClsContenInfo.Archtype = "ArchType";
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
                if (comboxClass.Text.Trim().Length <= 0 || txtBoxsn.Text.Trim().Length <= 0) {
                    MessageBox.Show("档案类型选择错误或盒号为空!");
                    this.comboxClass.SelectAll();
                    return false;
                }
                if (comboxClass.SelectedIndex == 1)
                    ClsContenInfo.Archtype = "ArchConten";
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
            else
                dt = Common.QueryBoxsnid(txtBoxsn.Text.Trim());
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
                    string type = dr[ClsContenInfo.Archtype].ToString();
                    string ImgFile = (dr["IMGFILE"].ToString().Trim().Length <= 0 ? "" : dr["IMGFILE"].ToString());
                    if (ImgFile.Trim().Length > 0) {
                        try {
                            ImgFile = DESEncrypt.DesDecrypt(ImgFile);
                        } catch {
                            ImgFile = "解密失败!";
                        }
                    }

                    stat = Convert.ToInt32(dr["ArchState"].ToString().Trim().Length <= 0 ? "0" : dr["ArchState"].ToString());
                    string xystat = dr["CheckXyState"].ToString().Trim().Length <= 0 ? "0" : dr["CheckXyState"].ToString();
                    if (stat >= 3 && stat < 5)
                        lvi.ImageIndex = 0;
                    else if (stat >= 5 && stat < 7)
                        lvi.ImageIndex = 1;
                    else if (stat == 7)
                        lvi.ImageIndex = 2;
                    lvi.SubItems.AddRange(new string[] { boxsn, archno, ImgFile, pages, arid, type, stat.ToString(), xystat.ToString() });
                    this.LvData.Items.Add(lvi);
                    i++;
                }
            }
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
                        if (ClsIni.Rabchk.ToLower() == "true")
                            radioBoxsn.Checked = true;
                        else {
                            comboxClass.Enabled = true;
                            radioClass.Checked = true;
                            comboxClass.Text = ClsIni.ArchNo;
                        }
                    }));

                }
            } catch { }
        }


        private void gArchSelect_Load(object sender, EventArgs e)
        {
            Getini();
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
                Archstat = LvData.SelectedItems[0].SubItems[7].Text;
                Archxystat = LvData.SelectedItems[0].SubItems[8].Text;
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

        #endregion

    }
}
