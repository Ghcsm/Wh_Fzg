using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DAL
{
    public partial class CntView : UserControl
    {
        public CntView()
        {
            InitializeComponent();
            Init();
        }
        public delegate void CntSelectHandle(object sender, EventArgs e, int id);
        public event CntSelectHandle OneCotentClicked;
        public delegate void CntSelectHandleG(object sender, EventArgs e, int id);
        public event CntSelectHandleG GoFous;

        public int m_ArchID { get; set; }   //卷的ID
        public int m_MaxPage { get; set; }  //卷页数
        private int mID { get; set; }       //目录表ID
        public int ButTF { get; set; } //是否转跳焦点

        public int cx = 0;

        private int Crrentpage;

        public string strCode
        {
            get
            {
                return txType.Text.Trim().PadLeft(2, '0') + txtCode.Text.Trim().PadLeft(2, '0'); ;
            }
        }

        public void Init()
        {
            //cboType.Items.Clear();
            //cboType.Items.Add("原件");
            //cboType.Items.Add("复印件");
            cboType.SelectedIndex = 0;
        }

        public void OnChangMl(int page)
        {
            try {
                Crrentpage = page;
                txtPage.Text = page.ToString();
                ListViewItem li = this.listview_Ml.Items.Cast<ListViewItem>().First(x => x.SubItems[2].Text == Convert.ToString(page));

                if (li != null) {
                    this.listview_Ml.SelectedItems.Clear();
                    li.Selected = true;
                }
            }
            catch { }

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try {
                if (e.KeyCode == Keys.Enter) {
                    string strSql = "select Title from T_ContentModel where code=@Code";
                    SqlParameter p1 = new SqlParameter("@Code", strCode);
                    txtTitle.Text = Convert.ToString(DAL.SQLHelper.ExecScalar(strSql, p1));
                    cboType.Focus();
                }
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }

        }

        private void txType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8) {
                e.Handled = true;
            }
        }

        private void txType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                this.btnAdd1.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddContent();
            if (ButTF > 0) {
                if (GoFous != null) {
                    GoFous(sender, new EventArgs(), 0);
                }
                return;
            }
            this.cboType.SelectedIndex = 0;
            txtCode.Focus();
        }

        private void AddContent()
        {
            try {
                string err = Valid(false);
                if (err != "") {
                    MessageBox.Show(err);
                    return;
                }
                string strSql = "PinsertArchContent";
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@RName", txtTitle.Text.Trim());
                p[1] = new SqlParameter("@RType", cboType.Text.Trim());
                p[2] = new SqlParameter("@FromPage", Convert.ToInt32(txtPage.Text.Trim()));
                p[3] = new SqlParameter("@Code", strCode);
                p[4] = new SqlParameter("@ArchID", m_ArchID);
                p[5] = new SqlParameter("@UserID", Common.OperID);
                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                LoadContent();
                if (this.listview_Ml.Items.Count > 0)
                    this.listview_Ml.Items[this.listview_Ml.Items.Count - 1].EnsureVisible();
                txtCode.Text = "";
                txtTitle.Text = "";
                txtPage.Text = "";

            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        public void LoadContent()
        {
            try {
                if (m_ArchID == 0)
                    return;

                string strSql = "select * from T_ArchContent where archid=@archid order by frompage";
                SqlParameter p1 = new SqlParameter("@archid", m_ArchID);
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                this.listview_Ml.Items.Clear();
                int i = 1;
                if (cx == 1) {
                    foreach (DataRow dr in dt.Rows) {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        lvi.SubItems.AddRange(new string[] { dr["RName"].ToString(), (Convert.ToInt32(dr["FROMPAGE"].ToString()) - 1).ToString(), "", dr["Code"].ToString(), dr["ID"].ToString() });
                        this.listview_Ml.Items.Add(lvi);
                        i++;
                    }
                    return;
                }
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.AddRange(new string[] { dr["RName"].ToString(), dr["FROMPAGE"].ToString(), dr["RType"].ToString(), dr["Code"].ToString(), dr["ID"].ToString() });
                    this.listview_Ml.Items.Add(lvi);
                    i++;
                }
                // SendKeys.Send("{Tab}");
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }


        private string Valid(bool IsUpdate)
        {
            string err = "";
            if (txtTitle.Text.Trim() == "") {
                err = "请输入标题!";
                txtTitle.Focus();
                return err;
            }
            if (txtPage.Text.Trim() == "") {
                err = "请输入起始页号!";
                txtPage.Focus();
                return err;
            }
            if (m_ArchID <= 0) {
                err = "请选择相关案卷档案!";
                return err;
            }
            if (this.cboType.Text.Trim().Length <= 0) {
                err = "件数不能为空！";
                this.cboType.Focus();
                return err;
            }
            if (Convert.ToInt32(txtPage.Text.Trim()) > m_MaxPage) {
                err = "起始页号不能大于卷的总页数!";
                return err;
            }
            string iPage = txtPage.Text.Trim();
            string tmpPage = string.Empty;
            int tmpID = 0;
            if (!IsUpdate) {
                foreach (ListViewItem item in listview_Ml.Items) {
                    tmpPage = item.SubItems[2].Text.ToString();
                    if (iPage == tmpPage) {
                        txtPage.Focus();
                        err = "起始页号已存在!";
                        return err;
                    }
                }
            }
            else {
                if (mID == 0) {
                    err = "请先选择目录!";
                    return err;
                }
                foreach (ListViewItem item in listview_Ml.Items) {
                    tmpPage = item.SubItems[2].Text.ToString();
                    tmpID = Convert.ToInt32(item.SubItems[5].Text);
                    if (iPage == tmpPage && tmpID != mID) {
                        txtPage.Focus();
                        err = "起始页号已存在!";
                        return err;
                    }
                }
            }

            return err;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            try {
                if (listview_Ml.SelectedItems != null && listview_Ml.SelectedItems.Count > 0) {
                    txtTitle.Text = listview_Ml.SelectedItems[0].SubItems[1].Text;
                    txtPage.Text = listview_Ml.SelectedItems[0].SubItems[2].Text;
                    txtCode.Text = listview_Ml.SelectedItems[0].SubItems[4].Text.Substring(2);
                    mID = Convert.ToInt32(listview_Ml.SelectedItems[0].SubItems[5].Text);
                    cboType.Text = listview_Ml.SelectedItems[0].SubItems[3].Text;
                    //if (listview_Ml.SelectedItems[0].SubItems[3].Text == "原件")
                    //{
                    //    cboType.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    cboType.SelectedIndex = 1;
                    //}
                    if (OneCotentClicked != null) {
                        OneCotentClicked(sender, new EventArgs(), 1);
                    }
                }
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditContent();
            this.cboType.SelectedIndex = 0;
            this.txtCode.Focus();
        }

        private void EditContent()
        {
            try {
                string err = Valid(true);
                if (err != "") {
                    MessageBox.Show(err);
                    return;
                }

                string strSql = "PupdatetArchContent";
                SqlParameter[] p = new SqlParameter[7];
                p[0] = new SqlParameter("@RName", txtTitle.Text.Trim());
                p[1] = new SqlParameter("@RType", cboType.Text);
                p[2] = new SqlParameter("@FromPage", Convert.ToInt32(txtPage.Text));
                p[3] = new SqlParameter("@Code", strCode);
                p[4] = new SqlParameter("@ArchID", m_ArchID);
                p[5] = new SqlParameter("@UserID", Common.OperID);
                p[6] = new SqlParameter("@mID", mID);

                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                LoadContent();
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteContent();
            this.cboType.SelectedIndex = 0;
            this.txtCode.Focus();
        }

        private void DeleteContent()
        {
            try {
                if (m_ArchID == 0) {
                    MessageBox.Show("请先选择档案!");
                    return;
                }
                if (mID == 0) {
                    MessageBox.Show("请先选择目录!");
                    return;
                }
                if (listview_Ml.SelectedItems == null) {
                    return;
                }
                string strSql = "PDeleteArchContent";
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@UserID", 1);
                p[1] = new SqlParameter("@mID", mID);

                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                LoadContent();
                txtCode.Text = "";
                txtPage.Text = "";
                txtTitle.Text = "";
                cboType.Text = "";
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }

        }


        private void txtCode_Enter(object sender, EventArgs e)
        {
            txtCode.SelectAll();
        }

        private void txType_Enter(object sender, EventArgs e)
        {
            txType.SelectAll();

        }

        private void LoadMoBan()
        {
            try {
                string strSql = "select * from [T_CONTENTMODEL] order by code ";
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql);
                this.listview_moban.Items.Clear();
                int i = 1;
                foreach (DataRow dr in dt.Rows) {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dr["Code"].ToString();
                    lvi.SubItems.AddRange(new string[] { dr["Title"].ToString(), dr["ID"].ToString() });
                    this.listview_moban.Items.Add(lvi);
                    i++;
                }
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }

        }

        private void check_moban_CheckedChanged(object sender, EventArgs e)
        {
            if (this.check_moban.Checked == true) {
                this.listview_Ml.Width = this.Width - 199;
                this.listview_moban.Visible = true;
                LoadMoBan();
            }
            else {
                this.listview_Ml.Width = 445;
                this.listview_moban.Visible = false;
            }
        }

        private void listview_moban_DoubleClick(object sender, EventArgs e)
        {

            if (this.listview_moban.SelectedItems != null && this.listview_moban.SelectedItems.Count > 0) {
                string tcode = this.listview_moban.SelectedItems[0].SubItems[0].Text;
                txType.Text = tcode.Substring(0, 2);
                txtCode.Text = tcode.Substring(2);
                txtTitle.Text = this.listview_moban.SelectedItems[0].SubItems[1].Text;
                txtPage.Text = Crrentpage.ToString();
                AddContent();
                this.listview_moban.SelectedItems.Clear();
                if (OneCotentClicked != null) {
                    OneCotentClicked(sender, new EventArgs(), 0);
                }

            }
        }

        private void listview_Ml_Click(object sender, EventArgs e)
        {
            if (this.listview_Ml.SelectedItems != null && this.listview_Ml.SelectedItems.Count > 0) {
                mID = Convert.ToInt32(this.listview_Ml.SelectedItems[0].SubItems[5].Text);
            }
            listview_Ml_DoubleClick(null, null);
        }

        private void but_moban_Click(object sender, EventArgs e)
        {
            frmModel model = new frmModel();
            model.ShowDialog();
        }

        private void listview_Ml_DoubleClick(object sender, EventArgs e)
        {
            if (this.listview_Ml.SelectedItems != null && this.listview_Ml.SelectedItems.Count > 0) {
                string tcode = "";
                //int P = 0;
                //if (cx == 1)
                //{
                //    tcode = this.listview_Ml.SelectedItems[0].SubItems[4].Text;
                //    txtTitle.Text = this.listview_Ml.SelectedItems[0].SubItems[1].Text;
                //    P=(Convert.ToInt32(this.listview_Ml.SelectedItems[0].SubItems[2].Text) + 1);
                //     txtPage.Text = P.ToString() ;
                //    cboType.Text = this.listview_Ml.SelectedItems[0].SubItems[3].Text;
                //    txType.Text = tcode.Substring(0, 2);
                //    txtCode.Text = tcode.Substring(2);
                //    if (OneCotentClicked != null)
                //    {
                //        OneCotentClicked(sender, new EventArgs(), P);
                //    }
                //    return;
                //}
                tcode = this.listview_Ml.SelectedItems[0].SubItems[4].Text;
                txtTitle.Text = this.listview_Ml.SelectedItems[0].SubItems[1].Text;
                txtPage.Text = this.listview_Ml.SelectedItems[0].SubItems[2].Text;
                cboType.Text = this.listview_Ml.SelectedItems[0].SubItems[3].Text;
                txType.Text = tcode.Substring(0, 2);
                txtCode.Text = tcode.Substring(2);
                if (OneCotentClicked != null) {
                    OneCotentClicked(sender, new EventArgs(), 1);
                }
            }
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                if (ButTF != 0) {
                    this.btnAdd1.Focus();
                    return;
                }
                this.txtPage.Focus();
            }
        }

        private void cboType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.txtTitle.Focus();
        }

        private void btnModel_Click(object sender, EventArgs e)
        {

        }
    }
}
