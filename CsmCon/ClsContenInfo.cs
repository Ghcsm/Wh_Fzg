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


    public static class ClsContenInfo
    {
        public static string ContenTable { get; set; } = "";
        public static string ContenWith { get; set; } = "";
        public static string ContenTxtwith { get; set; } = "";
        public static string ContenLie { get; set; } = "";
        public static string ContenTitle { get; set; } = "";
        public static string ContenPages { get; set; } = "";
        public static int PagesWz { get; set; } = 0;
        public static int TitleWz { get; set; } = 0;
        public static List<string> ContenCoList = new List<string>();
        public static string ContenCol { get; set; } = "";
        public static List<string> PageCount = new List<string>();
        public static List<string> PageCount2 = new List<string>();

        public static List<string> LsModule = new List<string>();
        public static List<string> LsModuleIndex = new List<string>();

        public static int txtcol { get; set; } = 0;
        public static int txtrows { get; set; } = 1;

        public static string Pagestmp { get; set; } = "";

        public static string Archtype { get; set; } = "";

        public static string Modulename { get; set; } = "";
    }



    public static class ClsConten
    {
        public static void GetContenInfo()
        {
            //if (ClsContenInfo.ContenTable ==null && ClsContenInfo.ContenTable.Length<=0)
            //    return;
            DataTable dt = T_Sysset.GetConten(ClsContenInfo.Modulename);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            ClsContenInfo.ContenCoList.Clear();
            ClsContenInfo.ContenTable = dr["ContenTable"].ToString();
            ClsContenInfo.ContenLie = dr["ContenLie"].ToString();
            ClsContenInfo.ContenWith = dr["ContenWith"].ToString();
            ClsContenInfo.ContenTxtwith = dr["ContentxtWith"].ToString();
            ClsContenInfo.ContenTitle = dr["ContenTitle"].ToString();
            ClsContenInfo.ContenPages = dr["ContenPages"].ToString();
            ClsContenInfo.ContenCol = dr["ContenCol"].ToString();
            if (ClsContenInfo.ContenCol.Length > 0) {
                string[] col = ClsContenInfo.ContenCol.Split(';');
                for (int i = 0; i < col.Length; i++) {
                    ClsContenInfo.ContenCoList.Add(col[i]);
                }
            }
            ClsContenInfo.PagesWz = ClsContenInfo.ContenCoList.IndexOf(ClsContenInfo.ContenPages);
            ClsContenInfo.TitleWz= ClsContenInfo.ContenCoList.IndexOf(ClsContenInfo.ContenTitle);
        }

        public static void LoadModule(ListViewEx lsv)
        {
            Task.Run(() =>
            {
                DataTable dt = Common.GetContentsModule();
                if (dt != null && dt.Rows.Count > 0) {
                    lsv.Items.Clear();
                    int i = 1;
                    foreach (DataRow dr in dt.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        string code = dr["Code"].ToString();
                        string title = dr["Title"].ToString();
                        ClsContenInfo.LsModuleIndex.Add(code);
                        ClsContenInfo.LsModule.Add(title);
                        lvi.Text = code;
                        lvi.SubItems.AddRange(new string[] { title, dr["ID"].ToString() });
                        lsv.BeginInvoke(new Action(() => { lsv.Items.Add(lvi); }));
                        i++;
                    }
                }
            });
        }

        public static void LoadContents(int archid, ListViewEx lsv, bool ch)
        {
            if (archid <= 0)
                return;
            Task.Run(() =>
            {
                lsv.Invoke(new Action(() =>
                {
                    lsv.Items.Clear();
                }));
                ClsContenInfo.PageCount.Clear();
                DataTable dt = Common.LoadContents(ClsContenInfo.ContenTable, ClsContenInfo.ContenCol, ClsContenInfo.ContenPages, Convert.ToInt32(ch), archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                int i = 1;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    string id = dr["id"].ToString();
                    lvi.SubItems.AddRange(new string[] { id });
                    for (int t = 0; t < ClsContenInfo.ContenCoList.Count; t++) {
                        string str = dr[ClsContenInfo.ContenCoList[t]].ToString();
                        if (t==ClsContenInfo.PagesWz)
                            ClsContenInfo.PageCount.Add(str);
                        lvi.SubItems.AddRange(new string[] { str });
                    }
                    lsv.Invoke(new Action(() =>
                {
                    lsv.Items.Add(lvi);
                }));
                    i++;
                }
                lsv.BeginInvoke(new Action(() =>
                {
                    if (lsv.Items.Count > 0)
                        lsv.Items[0].Selected = true;
                }));
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
                ClsContenInfo.PageCount2.Clear();
                DataTable dt = Common.LoadContents(ClsContenInfo.ContenTable, ClsContenInfo.ContenCol, ClsContenInfo.ContenPages, 2, archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                int i = 1;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    string id = dr["id"].ToString();
                    lvi.SubItems.AddRange(new string[] { id });
                    for (int t = 0; t < ClsContenInfo.ContenCoList.Count; t++) {
                        string str = dr[ClsContenInfo.ContenCoList[t]].ToString();
                        if (t == ClsContenInfo.PagesWz)
                            ClsContenInfo.PageCount2.Add(str);
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
            if (ClsContenInfo.ContenTable==null|| ClsContenInfo.ContenTable.Trim().Length <= 0)
                return;
            DataTable dt = Common.GetTableCol(ClsContenInfo.ContenTable);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            int id = 0;
            int colnum = Convert.ToInt32(ClsContenInfo.ContenLie);
            int width = Convert.ToInt32(ClsContenInfo.ContenWith);
            int txtwidth = Convert.ToInt32(ClsContenInfo.ContenTxtwith);
            string[] oldname = ClsContenInfo.ContenCol.Split(';');
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
            if (ClsContenInfo.txtcol == 0)
                xx = 5;
            else
                xx = pl.Width / colnum * ClsContenInfo.txtcol;
            ClsContenInfo.txtcol += 1;
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
                lb.Location = new Point(xx, ClsContenInfo.txtrows * 30 - 20);
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
                    txt.Location = new Point(yy, ClsContenInfo.txtrows * 30 - 20);
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
                    cb.Location = new Point(yy, ClsContenInfo.txtrows * 30 - 20);
                cb.KeyPress += Cb_KeyPress; ;
                pl.Controls.Add(cb);
            }
            if (ClsContenInfo.txtcol == colnum) {
                ClsContenInfo.txtcol = 0;
                ClsContenInfo.txtrows += 1;
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
            int id = ClsContenInfo.TitleWz;
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
            ClsContenInfo.Pagestmp = "";
            int id = 0;
            int title = ClsContenInfo.TitleWz + 1;
            int page = ClsContenInfo.PagesWz + 1;
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
                            ClsContenInfo.Pagestmp = ct.Text.Trim();
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
