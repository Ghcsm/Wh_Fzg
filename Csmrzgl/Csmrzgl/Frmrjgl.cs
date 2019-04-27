using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using System.Data.SqlClient;
using System.Threading;


namespace Csmrzgl
{
    public partial class Frmrjgl : Form
    {

        public Frmrjgl()
        {
            InitializeComponent();
        }

        private void Gettable()
        {
            combTable.Items.Clear();
            DataTable dt = Common.GetLogTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string str = dt.Rows[i][0].ToString();
                combTable.Items.Add(str);
            }
        }
        private void GetTablcol(int id)
        {
            if (id < 0)
                return;
            string table = combTable.Items[combTable.SelectedIndex].ToString();
            DataTable dt = Common.GetTableCol(table);
            combColname.Items.Clear();
            Thread.Sleep(300);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string col = dt.Rows[i][0].ToString();
                if (col.Trim().Length>0)
                 combColname.Items.Add(col);
            }
        }

        private void GettableInfo()
        {
            string BeginTime = dateTime_rzgl_qi.Text;
            string EndTime = dateTime_rzgl_zhi.Text;
            DateTime dt;
            if (!DateTime.TryParse(BeginTime, out dt)) {
                throw new ApplicationException("不是正确的日期格式类型，请检查！");

            }
            if (!DateTime.TryParse(BeginTime, out dt)) {
                throw new ApplicationException("不是正确的日期格式类型，请检查！");
            }

            DataTable data =
                Common.GetLogTableInfo(combTable.Text.Trim(), combColname.Text.Trim(), txt_gjz.Text.Trim(),BeginTime,EndTime);
            if (data == null || data.Rows.Count <= 0)
                return;
            dateview_rz.DataSource = data;
        }


        private void but_rzgl_so_Click(object sender, EventArgs e)
        {
            try {
                if (combTable.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择查询日志表!");
                    combTable.Focus();
                    return;
                }
                if (combColname.Text.Trim().Length <= 0) {
                    MessageBox.Show("请选择查询字段!");
                    combColname.Focus();
                    return;
                }
                GettableInfo();
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        private void combTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTablcol(combTable.SelectedIndex);
        }

        private void Frmrjgl_Shown(object sender, EventArgs e)
        {
            Gettable();
        }
    }
}
