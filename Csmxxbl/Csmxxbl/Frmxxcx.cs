using System;
using System.Windows.Forms;
using DAL;

namespace Csmxxbl
{
    public partial class Frmxxcx : Form
    {
        public Frmxxcx()
        {
            InitializeComponent();
        }
        public static string aid;
        public static string anlx;
        private void but_sql_Click(object sender, EventArgs e)
        {
            this.lab_zj.Visible = false;
            if (this.com_ziduan.Text.Trim().Length<=0 && this.txt_gjz.Text.Trim().Length<=0)
            {
                MessageBox.Show("请输入相关信息!");
                this.com_ziduan.Focus();
                return;
            }
            int id = this.com_Leixing.SelectedIndex;
            if (id==0)
            {
                anlx = "土地信息";
            }
            else
            {
                anlx = "房屋信息";
            }
            
            string ziduan = this.com_ziduan.Text.Trim();
            string txtgjz = this.txt_gjz.Text.Trim();
            dateviewG.DataSource = null;
            dateviewG.DataSource = Common.QueryXxblinfo(ziduan, txtgjz, id);
            this.com_ziduan.Focus();
        }


      
        private void Frmxxcx_Shown(object sender, EventArgs e)
        {
            this.com_Leixing.SelectedIndex = 0;
            this.com_ziduan.Focus();
        }

        private void dateviewG_DoubleClick(object sender, EventArgs e)
        {
            if (dateviewG.RowCount<1 || dateviewG.SelectedRows.Count<1 )
            {               
                MessageBox.Show("请选择相关数据!");
                return;
            }
            int id = dateviewG.CurrentRow.Index;
            aid = dateviewG.Rows[id].Cells["主键"].Value.ToString();           
            this.Close();
           
        }

       

        private void txt_gjz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.but_sql.Focus();
            }
        }
             
        private void com_ziduan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                this.txt_gjz.Focus();
            }
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.com_ziduan.Text = "";
            this.com_ziduan.Items.Clear();           
            if (this.com_Leixing.SelectedIndex==0)
            {
                this.com_ziduan.Items.Add("不动产证号");
                this.com_ziduan.Items.Add("原土地证号");
                this.com_ziduan.Items.Add("权利人1");
                this.com_ziduan.Items.Add("权利人2");
                this.com_ziduan.Items.Add("权利人3");
                this.com_ziduan.Items.Add("权利人4");
            }   
            else if (this.com_Leixing.SelectedIndex==1)
            {
                this.com_ziduan.Items.Add("不动产证号");
                this.com_ziduan.Items.Add("原土地证号");
                this.com_ziduan.Items.Add("权利人1");
                this.com_ziduan.Items.Add("不动产ID");
                this.com_ziduan.Items.Add("权利ID");
                this.com_ziduan.Items.Add("权利人ID");
                this.com_ziduan.Items.Add("房屋坐落");
                
            }
        }

        private void com_Leixing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.com_ziduan.Focus();
        }

        private void dateviewG_Click(object sender, EventArgs e)
        {

        }       
    }
}
