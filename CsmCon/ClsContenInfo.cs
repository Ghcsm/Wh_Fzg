using DAL;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        public List<string> lsModulelx = new List<string>();

        public bool loadcon { get; set; }
        public int txtcol { get; set; } = 0;
        public int txtrows { get; set; } = 1;
        public string Pagestmp { get; set; } = "";
        public string Archtype { get; set; } = "";

        private Panel ptxt;
        AutoCompleteStringCollection source = new AutoCompleteStringCollection();

        public void GetContenInfo()
        {
            //if (ClsContenInfo.ContenTable ==null && ClsContenInfo.ContenTable.Length<=0)
            //    return;
            DataTable dt = T_Sysset.GetConten(UcContents.Modulename);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            ContenCoList.Clear();
            ContenTable = dr["ContenTable"].ToString();
            ContenLie = dr["ContenLie"].ToString();
            ContenWith = dr["ContenWith"].ToString();
            ContenTxtwith = dr["ContentxtWith"].ToString();
            ContenTitle = dr["ContenTitle"].ToString();
            ContenPages = dr["ContenPages"].ToString();
            ContenCol = dr["ContenCol"].ToString();
            if (ContenCol.Length > 0) {
                string[] col = ContenCol.Split(';');
                for (int i = 0; i < col.Length; i++) {
                    ContenCoList.Add(col[i]);
                }
            }
            PagesWz = ContenCoList.IndexOf(ContenPages);
            TitleWz = ContenCoList.IndexOf(ContenTitle);
            LoadModulels();
        }

        public void LoadModule(ListViewEx lsv)
        {
            lsv.Items.Clear();
            DataTable dt = Common.GetcontenModule();
            if (dt != null && dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    string type = dr["CoType"].ToString();
                    string code = dr["Code"].ToString();
                    string title = dr["Title"].ToString();
                    string titlelx = dr["TitleLx"].ToString();
                    lvi.Text = code;
                    lvi.SubItems.AddRange(new string[] { title, titlelx, type });
                    lsv.BeginInvoke(new Action(() => { lsv.Items.Add(lvi); }));
                }
            }

        }

        public void LoadModulels()
        {
            LsModule.Clear();
            lsModulelx.Clear();
            LsModuleIndex.Clear();
            DataTable dt = Common.GetcontenModule();
            if (dt != null && dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    string type = dr["CoType"].ToString();
                    string code = dr["Code"].ToString();
                    string title = dr["Title"].ToString();
                    string titlelx = dr["TitleLx"].ToString();
                    LsModuleIndex.Add(code);
                    LsModule.Add(title);
                    lsModulelx.Add(titlelx);
                  //  byte[] bytes = Encoding.Default.GetBytes(title);
                  //  string s = Encoding.GetEncoding("gb2312").GetString(bytes);
                    source.Add(title);
                }
            }

        }

        public void LoadContents(int archid, ListViewEx lsv, bool ch)
        {
            if (archid <= 0)
                return;
            if (loadcon)
                return;
            Task.Run(() =>
            {
                loadcon = true;
                try {

                    lsv.Invoke(new Action(() => { lsv.Items.Clear(); }));
                    PageCount.Clear();
                    DataTable dt = Common.LoadContents(ContenTable, ContenCol,
                       ContenPages, Convert.ToInt32(ch), archid);
                    if (dt == null || dt.Rows.Count <= 0)
                        return;
                    int i = 1;
                    foreach (DataRow dr in dt.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        string id = dr["id"].ToString();
                        lvi.SubItems.AddRange(new string[] { id });
                        for (int t = 0; t < ContenCoList.Count; t++) {
                            string str = dr[ContenCoList[t]].ToString();
                            if (t == PagesWz)
                                PageCount.Add(str);
                            lvi.SubItems.AddRange(new string[] { str });
                        }

                        lsv.Invoke(new Action(() => { lsv.Items.Add(lvi); }));
                        i++;
                    }
                    //lsv.Invoke(new Action(() =>
                    //{
                    //    if (lsv.Items.Count > 0)
                    //        lsv.Items[lsv.Items.Count-1].Selected = true;
                    //}));

                } catch {
                } finally {
                    loadcon = false;
                }
            });
        }

        public void LoadContents(int archid, ListView lsv)
        {
            if (archid <= 0)
                return;
            Task.Run(() =>
            {
                lsv.Invoke(new Action(() =>
                {
                    lsv.Items.Clear();
                }));
                PageCount2.Clear();
                DataTable dt = Common.LoadContents(ContenTable, ContenCol, ContenPages, 2, archid);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                int i = 1;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    string id = dr["id"].ToString();
                    lvi.SubItems.AddRange(new string[] { id });
                    for (int t = 0; t < ContenCoList.Count; t++) {
                        string str = dr[ContenCoList[t]].ToString();
                        if (t == PagesWz)
                            PageCount2.Add(str);
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


        public void GetControl(Panel pl)
        {
            ptxt = pl;
            GetContenInfo();
            if (ContenTable == null || ContenTable.Trim().Length <= 0)
                return;
            DataTable dt = Common.GetTableCol(ContenTable);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            int id = 0;
            int colnum = Convert.ToInt32(ContenLie);
            int width = Convert.ToInt32(ContenWith);
            int txtwidth = Convert.ToInt32(ContenTxtwith);
            string[] oldname = ContenCol.Split(';');
            for (int i = 0; i < oldname.Length; i++) {
                string str = oldname[i];
                foreach (DataRow dr in dt.Rows) {
                    string namecol = dr["name"].ToString();
                    string value = dr["value"].ToString();
                    if (str == namecol) {
                        id += 1;
                        CreateTxt(pl, namecol, value, colnum, id, width, txtwidth);
                        break;
                    }
                }
            }
        }

        private void CreateTxt(Panel pl, string name, string val, int colnum, int id, int width, int txtwidth)
        {
            int xx = 0;
            int yy = 0;
            if (txtcol == 0)
                xx = 5;
            else
                xx = pl.Width / colnum * txtcol;
            txtcol += 1;
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
                lb.Location = new Point(xx, txtrows * 30 - 20);
            pl.Controls.Add(lb);
            if (val.Trim().Length <= 0) {
                TextBox txt = new TextBox();
                txt.Name = name;
                // txt.Width = (pl.Width - width * colnum) / colnum - 10;
                txt.Width = txtwidth;
                txt.TabIndex = id;
                txt.Tag = id;
                if (txt.Tag.ToString() == (TitleWz+1).ToString())
                {
                    //东丽区自动匹配
                    txt.AutoCompleteCustomSource = source;
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txt.Leave += Txt_Leave;
                }
                txt.BringToFront();
                if (id <= colnum)
                    txt.Location = new Point(yy, 5);
                else
                    txt.Location = new Point(yy, txtrows * 30 - 20);
                txt.KeyPress += Txt_KeyPress; 
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
                    cb.Location = new Point(yy, txtrows * 30 - 20);
                cb.KeyPress += Cb_KeyPress; ;
                pl.Controls.Add(cb);
            }
            if (txtcol == colnum) {
                txtcol = 0;
                txtrows += 1;
            }
        }

       

        public void SetInfoTxt(Control p, int id, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox)
                {
                    if (ct.Tag.ToString() == id.ToString())
                    {
                        ct.Text = str;
                    }
                }
            }
        }
        public void SetInfoTxtcls(Control p, int id, string str)
        {
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox)
                {
                    if (ct.Tag.ToString() == id.ToString())
                    {
                        ct.Text = str;
                    }
                    else
                        ct.Text = "";
                }
            }
        }

        public void SetInfoTxt(Control p, string str)
        {
            int id = TitleWz+1;
            foreach (Control ct in p.Controls) {
                if (ct is TextBox || ct is ComboBox) {
                    if (ct.Tag.ToString() == id.ToString()) {
                        ct.Text = str;
                    }
                }
            }
        }

        public bool Gettxtzd(Control p)
        {
            bool tf = false;
            Pagestmp = "";
            int id = 0;
            int title = TitleWz + 1;
            int page = PagesWz + 1;
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
                            Pagestmp = ct.Text.Trim();
                        }
                    }
                }
            }
            if (id > 1)
                return true;
            else
                return tf;
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
        private void Txt_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            //东丽区自动配置，输入标题时 目录种类显示
            if (txt.Tag.ToString() == (PagesWz).ToString()) {
                int id = LsModule.IndexOf(txt.Text.Trim());
                string str = "";
                if (id >= 0) {
                    str = lsModulelx[id].ToString();
                    UcContents.Setxtxtls(ptxt, PagesWz, str);
                }
            }
        }

    }
}
