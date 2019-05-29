using DAL;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsmCon
{


    public class ClsContenInfo
    {
        public string ContenTable { get; set; } = "";
        public string ContenWith { get; set; } = "";
        public string ContenTxtwith { get; set; } = "";
        public string ContenLie { get; set; } = "";
        public string ContenTitle { get; set; } = "";
        public string ContenPages { get; set; } = "";
        public int PagesWz { get; set; } = 0;
        public int TitleWz { get; set; } = 0;
        public List<string> ContenCoList = new List<string>();
        public string ContenCol { get; set; } = "";
        public List<string> PageCount = new List<string>();
        public List<string> PageCount2 = new List<string>();

        public List<string> LsModule = new List<string>();
        public List<string> LsModuleIndex = new List<string>();

        public bool loadcon { get; set; }
        public int txtcol { get; set; } = 0;
        public int txtrows { get; set; } = 1;
        public string Pagestmp { get; set; } = "";
        public string Archtype { get; set; } = "";
        public string Modulename { get; set; } = "";
    }



    public static class ClsConten
    {
        public static void GetContenInfo()
        {
            //if (ClsContenInfo.ContenTable ==null && ClsContenInfo.ContenTable.Length<=0)
            //    return;
            DataTable dt = T_Sysset.GetConten(UcContents.clsinfo.Modulename);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            UcContents.clsinfo.ContenCoList.Clear();
            UcContents.clsinfo.ContenTable = dr["ContenTable"].ToString();
            UcContents.clsinfo.ContenLie = dr["ContenLie"].ToString();
            UcContents.clsinfo.ContenWith = dr["ContenWith"].ToString();
            UcContents.clsinfo.ContenTxtwith = dr["ContentxtWith"].ToString();
            UcContents.clsinfo.ContenTitle = dr["ContenTitle"].ToString();
            UcContents.clsinfo.ContenPages = dr["ContenPages"].ToString();
            UcContents.clsinfo.ContenCol = dr["ContenCol"].ToString();
            if (UcContents.clsinfo.ContenCol.Length > 0) {
                string[] col = UcContents.clsinfo.ContenCol.Split(';');
                for (int i = 0; i < col.Length; i++) {
                    UcContents.clsinfo.ContenCoList.Add(col[i]);
                }
            }
            UcContents.clsinfo.PagesWz = UcContents.clsinfo.ContenCoList.IndexOf(UcContents.clsinfo.ContenPages);
            UcContents.clsinfo.TitleWz = UcContents.clsinfo.ContenCoList.IndexOf(UcContents.clsinfo.ContenTitle);
        }

        public static void LoadModule(ListViewEx lsv)
        {
            lsv.Items.Clear();
            DataTable dt = Common.GetcontenModule();
            if (dt != null && dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    string type = dr["CoType"].ToString();
                    string code = dr["Code"].ToString();
                    string title = dr["Title"].ToString();
                    UcContents.clsinfo.LsModuleIndex.Add(code);
                    UcContents.clsinfo.LsModule.Add(title);
                    lvi.Text = title;
                    lvi.SubItems.AddRange(new string[] { code, type });
                    lsv.BeginInvoke(new Action(() => { lsv.Items.Add(lvi); }));
                }
            }

        }

        public static void LoadContents(int archid, ListViewEx lsv, bool ch)
        {
            if (archid <= 0)
                return;
            if (UcContents.clsinfo.loadcon)
                return;
            Task.Run(() =>
            {
                UcContents.clsinfo.loadcon = true;
                try {


                    lsv.Invoke(new Action(() => { lsv.Items.Clear(); }));
                    UcContents.clsinfo.PageCount.Clear();
                    DataTable dt = Common.LoadContents(UcContents.clsinfo.ContenTable, UcContents.clsinfo.ContenCol,
                        UcContents.clsinfo.ContenPages, Convert.ToInt32(ch), archid);
                    if (dt == null || dt.Rows.Count <= 0)
                        return;
                    int i = 1;
                    foreach (DataRow dr in dt.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        string id = dr["id"].ToString();
                        lvi.SubItems.AddRange(new string[] { id });
                        for (int t = 0; t < UcContents.clsinfo.ContenCoList.Count; t++) {
                            string str = dr[UcContents.clsinfo.ContenCoList[t]].ToString();
                            if (t == UcContents.clsinfo.PagesWz)
                                UcContents.clsinfo.PageCount.Add(str);
                            lvi.SubItems.AddRange(new string[] { str });
                        }

                        lsv.Invoke(new Action(() => { lsv.Items.Add(lvi); }));
                        i++;
                    }

                    lsv.Invoke(new Action(() =>
                    {
                        if (lsv.Items.Count > 0)
                            lsv.Items[0].Selected = true;
                    }));

                } catch {
                } finally {
                    UcContents.clsinfo.loadcon = false;
                }
            });
        }

        public static void LoadContents(int archid, ListView lsv)
        {
            if (archid <= 0)
                return;
            Task.Run(() =>
            {
                lsv.Invoke(new Action(() =>
                {
                    lsv.Items.Clear();
                }));
                UcContents.clsinfo.PageCount2.Clear();
                DataTable dt = Common.LoadContents(UcContents.clsinfo.ContenTable, UcContents.clsinfo.ContenCol, UcContents.clsinfo.ContenPages, 2, archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                int i = 1;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    string id = dr["id"].ToString();
                    lvi.SubItems.AddRange(new string[] { id });
                    for (int t = 0; t < UcContents.clsinfo.ContenCoList.Count; t++) {
                        string str = dr[UcContents.clsinfo.ContenCoList[t]].ToString();
                        if (t == UcContents.clsinfo.PagesWz)
                            UcContents.clsinfo.PageCount2.Add(str);
                        lvi.SubItems.AddRange(new string[] { str });
                    }
                    lsv.Invoke(new Action(() =>
                    {
                        lsv.Items.Add(lvi);
                    }));
                    i++;
                }
            });
        }


        public static void GetControl(Panel pl)
        {
            GetContenInfo();
            if (UcContents.clsinfo.ContenTable == null || UcContents.clsinfo.ContenTable.Trim().Length <= 0)
                return;
            DataTable dt = Common.GetTableCol(UcContents.clsinfo.ContenTable);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            int id = 0;
            int colnum = Convert.ToInt32(UcContents.clsinfo.ContenLie);
            int width = Convert.ToInt32(UcContents.clsinfo.ContenWith);
            int txtwidth = Convert.ToInt32(UcContents.clsinfo.ContenTxtwith);
            string[] oldname = UcContents.clsinfo.ContenCol.Split(';');
            for (int i = 0; i < oldname.Length; i++) {
                string str = oldname[i];
                foreach (DataRow dr in dt.Rows) {
                    string namecol = dr["name"].ToString();
                    string value = dr["value"].ToString();
                    if (str == namecol) {
                        id += 1;
                        CreateTxt(pl, namecol, value, colnum, id, width, txtwidth);
                    }
                }
            }
        }

        private static void CreateTxt(Panel pl, string name, string val, int colnum, int id, int width, int txtwidth)
        {
            int xx = 0;
            int yy = 0;
            if (UcContents.clsinfo.txtcol == 0)
                xx = 5;
            else
                xx = pl.Width / colnum * UcContents.clsinfo.txtcol;
            UcContents.clsinfo.txtcol += 1;
            Label lb = new Label();
            lb.Name = name;
            if (name.IndexOf("1") >= 0 || name.IndexOf("8") >= 0) {
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
                lb.Location = new Point(xx, UcContents.clsinfo.txtrows * 30 - 20);
            pl.Controls.Add(lb);
            if (val.Trim().Length <= 0) {
                TextBox txt = new TextBox();
                txt.Name = name;
                // txt.Width = (pl.Width - width * colnum) / colnum - 10;
                txt.Width = txtwidth;
                txt.TabIndex = id;
                txt.Tag = id;
                txt.BringToFront();
                if (id <= colnum)
                    txt.Location = new Point(yy, 5);
                else
                    txt.Location = new Point(yy, UcContents.clsinfo.txtrows * 30 - 20);
                txt.KeyPress += Txt_KeyPress; ;
                pl.Controls.Add(txt);
            }
            else {
                ComboBox cb = new ComboBox();
                cb.Name = name;
                // cb.Width = (pl.Width - lb.Width * colnum) / colnum - 10;
                cb.Width = txtwidth;
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
                    cb.Location = new Point(yy, UcContents.clsinfo.txtrows * 30 - 20);
                cb.KeyPress += Cb_KeyPress; ;
                pl.Controls.Add(cb);
            }
            if (UcContents.clsinfo.txtcol == colnum) {
                UcContents.clsinfo.txtcol = 0;
                UcContents.clsinfo.txtrows += 1;
            }
        }

        public static void SetInfoTxt(Control p, int id, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == id.ToString()) {
                        ct.Text = str;
                    }
                }
            }
        }

        public static void SetInfoTxt(Control p, string str)
        {
            int id = UcContents.clsinfo.TitleWz;
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == id.ToString()) {
                        ct.Text = str;
                    }
                }
            }
        }

        public static bool Gettxtzd(Control p)
        {
            bool tf = false;
            UcContents.clsinfo.Pagestmp = "";
            int id = 0;
            int title = UcContents.clsinfo.TitleWz + 1;
            int page = UcContents.clsinfo.PagesWz + 1;
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == title.ToString()) {
                        if (ct.Text.Trim().Length > 0) {
                            id += 1;
                        }
                    }
                    else if (ct.Tag.ToString() == page.ToString()) {
                        if (ct.Text.Trim().Length > 0) {
                            id += 1;
                            UcContents.clsinfo.Pagestmp = ct.Text.Trim();
                        }
                    }
                }
            }

            if (id > 1)
                return true;
            else
                return tf;
        }


        private static void Cb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private static void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
    }
}
