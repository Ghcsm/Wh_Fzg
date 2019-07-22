using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace Csmxxbl
{
    public partial class FrminfoSql : Form
    {
        public FrminfoSql()
        {
            InitializeComponent();
        }

        public List<string> lsinfo = new List<string>();
        public List<string> lscolinfo = new List<string>();
        public List<string> lscol = new List<string>();

        private void butSql_Click(object sender, EventArgs e)
        {

            if (combInfocol.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询字段!");
                combInfocol.Focus();
                return;
            }
            if (combInfoTj.Text.Trim().Length <= 0) {
                MessageBox.Show("请选择查询条件！");
                combInfoTj.Focus();
                return;
            }
            if (txtgjz.Text.Trim().Length <= 0) {
                MessageBox.Show("请输入查询关键字!");
                txtgjz.Focus();
                return;
            }
            DataTable dt = Common.GetLsData(combInfocol.Text.Trim(), combInfoTj.Text.Trim(), txtgjz.Text.Trim());
            if (dt == null || dt.Rows.Count <= 0)
                return;
            dataGrid.DataSource = dt;
        }

        private void FrminfoSql_Shown(object sender, EventArgs e)
        {
            combInfoTj.SelectedIndex = 0;
            if (lsinfo.Count <= 0)
                return;
            combInfocol.Items.Clear();
            for (int i = 0; i < lsinfo.Count; i++) {
                string str = lsinfo[i];
                if (str.Trim().Length <= 0)
                    continue;
                combInfocol.Items.Add(str);
            }
            combInfocol.SelectedIndex = 0;
            txtgjz.Focus();
        }

        private void dataGrid_DoubleClick(object sender, EventArgs e)
        {
            lscolinfo.Clear();
            lscol.Clear();
            if (dataGrid.RowCount <= 0 || dataGrid.SelectedRows.Count <= 0)
                return;
            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                string str = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells[i].Value.ToString();
                lscolinfo.Add(str);
                str = dataGrid.Columns[i].Name;
                lscol.Add(str);
            }
            if (lscolinfo.Count>0)
                this.Dispose();
        }

        private void combInfocol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
                SendKeys.Send("{Tab}");
        }

        private void combInfoTj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtgjz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }
    }
}
