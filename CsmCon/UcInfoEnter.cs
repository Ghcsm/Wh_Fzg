using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CsmCon
{
    public partial class UcInfoEnter : UserControl
    {
        public UcInfoEnter()
        {
            InitializeComponent();
        }

        private int txtcol = 0;
        private int txtrows = 1;
        private int Ts = 0;
        private string TsTag = "-1";
        public int Archid;
        private int Enterinfo;
        private string Atype = "";
        private string infostr = "-1";
        private List<string> lsblstr = new List<string>();
        public void GetInfoCol()
        {
            Common.GetInfoEnterSet();
            if (ClsInfoEnter.InfoTable.Count <= 0)
                return;
            int id = 0;
            string wycol = "";
            lsblstr.Clear();
            for (int i = 0; i < ClsInfoEnter.InfoTable.Count; i++) {
                id = 0;
                txtcol = 0;
                txtrows = 1;
                string name = ClsInfoEnter.InfoTableName[i];
                if (ClsInfoEnter.InfoWycol.Count > 0)
                    wycol = ClsInfoEnter.InfoWycol[i];
                if (!CreateTab(name))
                    return;
                string table = ClsInfoEnter.InfoTable[i];
                DataTable dt = Common.GetTableCol(table);
                int Important = 0;
                int colnum = Convert.ToInt32(ClsInfoEnter.InfoNum[i]);
                int width = Convert.ToInt32(ClsInfoEnter.InfoLbWidth[i]);
                int txtwidth = Convert.ToInt32(ClsInfoEnter.InfotxtWidth[i]);
                string[] oldname = ClsInfoEnter.InfoCol[i].Split(';');
                for (int j = 0; j < oldname.Length; j++) {
                    string strcol = oldname[j];
                    Important = 0;
                    foreach (DataRow dr in dt.Rows) {
                        string namecol = dr["name"].ToString();
                        string value = dr["value"].ToString();
                        if (strcol == namecol) {
                            id += 1;
                            if (strcol == wycol)
                                TsTag = id.ToString();
                            if (value.Contains("*")) {
                                lsblstr.Add(id.ToString());
                                value = value.Replace("*", "");
                                Important = 1;
                            }
                            CreateTxt(tabControl.TabPages[i], name, namecol, value, colnum, id, width, txtwidth, Important);
                            break;
                        }
                    }
                }
            }
        }

        private bool CreateTab(string str)
        {
            try {
                TabPage tab = new TabPage();
                tab.AutoSize = true;
                tab.Name = str;
                tab.Text = str;
                tab.BackColor = Color.Transparent;
                tab.TabStop = false;
                Panel p = new Panel();
                p.Name = str;
                p.BackColor = SystemColors.GradientInactiveCaption;
                p.Dock = DockStyle.Fill;
                p.AutoSize = true;
                tab.Controls.Add(p);
                tabControl.Controls.Add(tab);

                return true;
            } catch {
                return false;
            }

        }

        private void CreateTxt(TabPage tp, string tname, string name, string val, int colnum, int id, int width, int txtwith, int Important)
        {
            Control pl = tp.Controls.Find(tname, true)[0];
            int xx = 0;
            int yy = 0;
            if (txtcol == 0)
                xx = 5;
            else
                xx = pl.Width / colnum * txtcol;
            txtcol += 1;
            Label lb = new Label();
            lb.Name = name;
            if (Important > 0)
                lb.ForeColor = Color.Red;
            lb.Text = name + ": ";
            lb.Width = width;
            lb.SendToBack();
            lb.BackColor = Color.Transparent;
            yy = xx + lb.Width;
            if (id <= colnum)
                lb.Location = new Point(xx, 7);
            else
                lb.Location = new Point(xx, txtrows * 30 - 20);
            pl.Controls.Add(lb);
            if (val.IndexOf(';') < 0) {
                TextBox txt = new TextBox();
                txt.Name = name;
                // txt.Width = (pl.Width - lb.Width * colnum) / colnum - 10;
                txt.Width = txtwith;
                txt.TabIndex = id;
                txt.Tag = id;
                txt.BringToFront();
                if (id <= colnum)
                    txt.Location = new Point(yy, 5);
                else
                    txt.Location = new Point(yy, txtrows * 30 - 20);
                txt.KeyPress += Txt_KeyPress; ;
                pl.Controls.Add(txt);
            }
            else {
                ComboBox cb = new ComboBox();
                cb.Name = name;
                // cb.Width = (pl.Width - lb.Width * colnum) / colnum - 10;
                cb.Width = txtwith;
                cb.TabIndex = id;
                cb.Tag = id;
                cb.BringToFront();
                string[] a = val.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    if (b.Trim().Length > 0) {
                        cb.Items.Add(b);
                    }
                }
                if (id <= colnum)
                    cb.Location = new Point(yy, 5);
                else
                    cb.Location = new Point(yy, txtrows * 30 - 20);
                cb.KeyPress += Cb_KeyPress;
                if (TsTag == id.ToString())
                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                pl.Controls.Add(cb);
            }
            if (txtcol == colnum) {
                txtcol = 0;
                txtrows += 1;
            }
        }

        private void Cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = (ComboBox)sender;
            if (comb.Tag.ToString() == TsTag && TsTag != "-1") {
                infostr = comb.Text.Trim();
                int p;
                LoadInfo(Archid, Enterinfo, Atype,out p);
                infostr = "0";
            }
        }

        public void Txtcle()
        {
            string name = tabControl.SelectedTab.Name;
            Control pl = tabControl.SelectedTab.Controls.Find(name, true)[0];
            foreach (Control c in pl.Controls) {
                if (c is TextBox || c is ComboBox)
                    c.Text = "";
            }
        }

        public void GetFocus()
        {
            if (tabControl.TabPages.Count <= 0)
                return;
            string name = tabControl.SelectedTab.Name;
            Control pl = tabControl.SelectedTab.Controls.Find(name, true)[0];
            foreach (Control t in pl.Controls) {
                if (t.Tag != null && t.Tag.ToString() == "1") {
                    t.Focus();
                    return;
                }
            }
        }

        bool Istxtnull(int id, Control tab)
        {
            if (lsblstr.Count <= 0)
                return false;
            foreach (Control p in tab.Controls) {
                if (p.Tag != null) {
                    if (lsblstr.IndexOf(p.Tag.ToString()) >= 0) {
                        if (p.Text.Trim().Length <= 0) {
                            MessageBox.Show(p.Name + "信息不能为空");
                            p.Focus();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void SaveInfo(int archid, int enter)
        {
            try {
                if (archid <= 0)
                    return;
                Ts = 0;
                if (tabControl.TabPages.Count <= 0)
                    return;
                string name = tabControl.SelectedTab.Name;
                Control pl = tabControl.SelectedTab.Controls.Find(name, true)[0];
                if (Istxtnull(tabControl.SelectedIndex, pl))
                    return;
                string wycolstr = "";
                Dictionary<int, string> dic1 = new Dictionary<int, string>();
                Dictionary<int, string> dicxx = new Dictionary<int, string>();
                foreach (Control t in pl.Controls) {
                    if (t.Tag != null && t.Tag.ToString().Trim().Length > 0) {
                        string str = t.Text.Trim();
                        if (str.Length > 0) {
                            Ts += 1;
                        }
                        if (t.Tag.ToString() != "-1" && t.Tag.ToString() == TsTag)
                            wycolstr = str;
                        dic1.Add(Convert.ToInt32(t.Tag), str);
                    }
                }
                dicxx = dic1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                int id = Common.SaveInfo(tabControl.SelectedIndex, archid, dicxx, enter, Ts, wycolstr);
                if (id > 0) {
                    Txtcle();
                    MessageBox.Show("保存成功!");
                    return;
                }
                MessageBox.Show("保存失败!");
            } catch (Exception e) {
                MessageBox.Show("保存失败:" + e.ToString());
            }

        }

        public void DelInfo(int archid, int enter)
        {
            try {
                if (tabControl.TabPages.Count <= 0)
                    return;
                string table = ClsInfoEnter.InfoTable[tabControl.SelectedIndex];
                string name = tabControl.SelectedTab.Name;
                Control pl = tabControl.SelectedTab.Controls.Find(name, true)[0];
                if (Istxtnull(tabControl.SelectedIndex, pl))
                    return;
                string wycolstr = "";
                foreach (Control t in pl.Controls) {
                    if (t.Tag != null && t.Tag.ToString().Trim().Length > 0) {
                        string str = t.Text.Trim();
                        if (t.Tag.ToString() != "-1" && t.Tag.ToString() == TsTag)
                            wycolstr = str;

                    }
                }
                if (wycolstr.Trim().Length <= 0)
                {
                    MessageBox.Show("手续信息不能为空!");
                    return;
                }
                Common.DelInfoEnter(archid, enter.ToString(), wycolstr, table);
                MessageBox.Show("删除成功!");
                Txtcle();
            } catch (Exception e) {
                MessageBox.Show("删除失败!" + e.ToString());
            }

        }

        private void SetInfoTxt(Control p, string col, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Name == col) {
                        ct.Text = str;
                    }
                }
            }
        }


        private void SetInfoTxt(Control p, int id, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == id.ToString()) {
                        ct.Text = str;
                    }
                }
            }
        }

        public void LoadInfo(int archid, int enter, string atype,out int xs)
        {
            try
            {
                xs = 0;
                Txtcle();
                if (archid <= 0 || atype.Trim().Length <= 0)
                    return;
                Archid = archid;
                Enterinfo = enter;
                Atype = atype;
                int t = ClsInfoEnter.InfoTable.IndexOf(atype);
                if (t == -1) {
                    MessageBox.Show("前台设置表名称范围中未包含:" + atype);
                    return;
                }
                if (infostr.Trim() == "-1")
                    infostr = "0";
                DataTable dt = Common.GetInfoTable(t, archid, enter, infostr);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                xs = dt.Rows.Count;
                string name = tabControl.TabPages[t].Name;
                Control pl = tabControl.TabPages[t].Controls.Find(name, true)[0];
                DataRow dr = dt.Rows[0];
                string[] coltmp = ClsInfoEnter.InfoCol[t].Split(';');
                int id = 0;
                for (int i = 0; i < dt.Columns.Count; i++) {
                    string col = dt.Columns[i].ToString();
                    if (coltmp.Contains(col)) {
                        id += 1;
                        string str = dr[i].ToString().Trim();
                        SetInfoTxt(pl, id, str);
                    }
                }
                tabControl.SelectedIndex = t;
            } catch (Exception e)
            {
                xs = 0;
                MessageBox.Show("加载信息失败:" + e.ToString());
            }
        }

        public void LoadInfo(List<string> lsinfo, List<string> lscol)
        {
            Txtcle();
            if (lsinfo.Count <= 0 || lscol.Count <= 0)
                return;
            int t = tabControl.SelectedIndex;
            string name = tabControl.TabPages[t].Name;
            Control pl = tabControl.TabPages[t].Controls.Find(name, true)[0];
            for (int i = 0; i < lscol.Count; i++) {
                string col = lscol[i];
                string str = lsinfo[i];
                SetInfoTxt(pl, col, str);
            }
        }



        private void Cb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
    }
}
