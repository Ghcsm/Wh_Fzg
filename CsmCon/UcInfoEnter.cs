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
        public void GetInfoCol()
        {
            Common.GetInfoEnterSet();
            if (ClsInfoEnter.InfoTable.Count <= 0)
                return;
            int id = 0;
            for (int i = 0; i < ClsInfoEnter.InfoTable.Count; i++) {
                id = 0;
                string name = ClsInfoEnter.InfoTableName[i];
                if (!CreateTab(name))
                    return;
                string table = ClsInfoEnter.InfoTable[i];
                DataTable dt = Common.GetTableCol(table);
                int colnum = Convert.ToInt32(ClsInfoEnter.InfoNum[i]);
                int width = Convert.ToInt32(ClsInfoEnter.InfoLbWidth[i]);
                int txtwidth = Convert.ToInt32(ClsInfoEnter.InfotxtWidth[i]);
                string[] oldname = ClsInfoEnter.InfoCol[i].Split(';');
                foreach (DataRow dr in dt.Rows) {
                    string namecol = dr["name"].ToString();
                    string value = dr["value"].ToString();
                    if (oldname.Contains(namecol)) {
                        id += 1;
                        CreateTxt(tabControl.TabPages[i], name, namecol, value, colnum, id, width,txtwidth);
                    }
                }
            }
        }

        private bool CreateTab(string str)
        {
            try {
                TabPage tab = new TabPage();
                tab.Name = str;
                tab.Text = str;
                tab.BackColor = Color.Transparent;
                Panel p = new Panel();
                p.Name = str;
                p.BackColor = Color.Transparent;
                p.Dock = DockStyle.Fill;
                p.AutoSize = true;
                tab.Controls.Add(p);
                tabControl.Controls.Add(tab);
                return true;
            } catch {
                return false;
            }

        }

        private void CreateTxt(TabPage tp, string tname, string name, string val, int colnum, int id, int width,int txtwith)
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
            if (name.IndexOf("1") >= 0|| name.IndexOf("8") >= 0) {
                lb.ForeColor = Color.Red;
            }
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
            if (val.Trim().Length <= 0) {
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
                cb.KeyPress += Cb_KeyPress; ;
                pl.Controls.Add(cb);
            }
            if (txtcol == colnum) {
                txtcol = 0;
                txtrows += 1;
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

        public void SaveInfo(int archid, int enter)
        {
            try {
                if (archid <= 0)
                    return;
                Ts = 0;
                string name = tabControl.SelectedTab.Name;
                Control pl = tabControl.SelectedTab.Controls.Find(name, true)[0];
                Dictionary<int, string> dic1 = new Dictionary<int, string>();
                Dictionary<int, string> dicxx = new Dictionary<int, string>();
                foreach (Control t in pl.Controls) {
                    if (t.Tag != null && t.Tag.ToString() != "") {
                        string str = t.Text.Trim();
                        if (str != "") {
                            Ts += 1;
                        }
                        dic1.Add(Convert.ToInt32(t.Tag), str);
                    }
                }
                dicxx = dic1.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                int id = Common.SaveInfo(tabControl.SelectedIndex, archid, dicxx, enter, Ts);
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

        public void LoadInfo(int archid, int enter, string atype)
        {
            try {
                if (archid <= 0 || atype.Trim().Length <= 0)
                    return;
                Txtcle();
                int t = ClsInfoEnter.InfoTableName.IndexOf(atype);
                DataTable dt = Common.GetInfoTable(t, archid,enter);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                string name = tabControl.TabPages[t].Name;
                Control pl = tabControl.TabPages[t].Controls.Find(name, true)[0];
                DataRow dr = dt.Rows[0];
                string[] coltmp = ClsInfoEnter.InfoCol[t].Split(';');
                int id = 0;
                for (int i = 0; i < dt.Columns.Count; i++) {
                    string col = dt.Columns[i].ToString();
                    if (coltmp.Contains(col)) {
                        id += 1;
                        string str = dr[i].ToString();
                        SetInfoTxt(pl, id, str);
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("加载信息失败:" + e.ToString());
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
