using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DAL
{
    public partial class frmModel : Form
    {
        public frmModel()
        {
            InitializeComponent();

            LoadContentModel();
        }

        private int mID;   //模板ID
        private void LoadContentModel()
        {
            try
            {
                string strSql = "select * from [T_CONTENTMODEL] order by code ";
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql);
                this.listView1.Items.Clear();
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dr["Code"].ToString();
                    lvi.SubItems.AddRange(new string[] { dr["Title"].ToString(), dr["ID"].ToString() });
                    this.listView1.Items.Add(lvi);
                    i++;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            finally
            {

            }

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
                {
                    string Code = listView1.SelectedItems[0].Text;
                    txType.Text = Code.Substring(0, 2);
                    txtCode.Text = Code.Substring(2);
                    txtTitle.Text = listView1.SelectedItems[0].SubItems[1].Text;
                    mID = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text);                   
                   
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddContentModel();
        }

        private void AddContentModel()
        {
            try
            {
                if (txType.Text.Length != 2)
                {
                    MessageBox.Show("类型输入错误!");
                    txType.Focus();
                    return;
                }
                if (txtCode.Text == "")
                {
                    MessageBox.Show("代码输入错误!");
                    txtCode.Focus();
                    return;
                }
                if (txtTitle.Text == "")
                {
                    MessageBox.Show("请输入标题!");
                    txtTitle.Focus();
                    return;
                }

                string strSql = "select code from T_CONTENTMODEL where Title=@Title";
                SqlParameter p1 = new SqlParameter("@Title", txtTitle.Text.Trim());
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                if (dt.Rows.Count >0)
                {
                    string a=dt.Rows[0][0].ToString();
                    MessageBox.Show(string.Format("此目录已存在,代号 {0}", a));
                    return;
                }

                strSql = "PInsertContentModel";
                string Code = txType.Text + txtCode.Text.PadLeft(2,'0');
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@Code", Code);
                p[1] = new SqlParameter("@Title",txtTitle.Text.Trim());
                p[2] = new SqlParameter("@UserID", 1);               

                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                LoadContentModel();
                if (this.listView1.Items.Count>0)
                this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }          
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteContentModel();
        }

        private void DeleteContentModel()
        {
           
            if (mID == 0)
            {
                MessageBox.Show("请先选择目录!");
                return;
            }
            if (listView1.SelectedItems == null)
            {
                return;
            }

            if (MessageBox.Show("您确认要删除吗?", "确认删除", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                string strSql = "PDeleteContentModel";
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@mID", mID);
                p[1] = new SqlParameter("@UserID", 1);

                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                LoadContentModel();
            }

        }
        private void txType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
       
        private void txType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCode.Focus();
            }
        }
      
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTitle.Focus();
            }
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }
    }
}
