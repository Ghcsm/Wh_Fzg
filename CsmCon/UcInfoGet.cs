using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace CsmCon
{
    public partial class UcInfoGet : UserControl
    {
        public UcInfoGet()
        {
            InitializeComponent();
        }
        public delegate void ArchSelectHandleDoubleClk(object sender, EventArgs e);
        public event ArchSelectHandleDoubleClk DoubleClk;
        private string table = "";
        private string strzd = "";

        public static List<string> lsName = new List<string>();
        public static List<string> lsdata = new List<string>();

        private void combZd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void combtj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void txtgjz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SendKeys.Send("{Tab}");
        }

        private void butSql_Click(object sender, EventArgs e)
        {
            if (combZd.Text.Trim().Length <= 0 || combtj.Text.Trim().Length <= 0 || txtgjz.Text.Trim().Length <= 0) {
                MessageBox.Show("请完善各字段信息!");
                combZd.Focus();
                return;
            }

            SelectSql();
        }

        void SelectSql()
        {
            dgvInfo.DataSource = null;
            string zd = combZd.Text.Trim();
            string tj = combtj.Text.Trim();
            string gjz = txtgjz.Text.Trim();
            DataTable dt = Common.QueryData(zd, tj, gjz, table, strzd);

            dgvInfo.DataSource = dt;
        }

        private void dgvInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgvInfo.RowCount < 1 || dgvInfo.SelectedRows.Count < 1) {
                MessageBox.Show("请选择相关数据!");
                return;
            }
            lsName.Clear();
            lsdata.Clear();
            int id = dgvInfo.CurrentRow.Index;
            for (int i = 0; i < dgvInfo.ColumnCount; i++) {
                string str = dgvInfo.Rows[id].Cells[i].Value.ToString();
                string name = dgvInfo.Columns[i].Name;
                lsName.Add(name);
                lsdata.Add(str);
            }
            if (DoubleClk != null)
                DoubleClk(sender, new EventArgs());
        }

        void Init()
        {
            combZd.Items.Clear();
            DataTable dt = T_Sysset.GetInfoenterSql();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            table = dr["InfoTable"].ToString();
            strzd = dr["InfoCol"].ToString();
            if (strzd.Length > 0) {
                string[] a = strzd.Split(';');
                for (int i = 0; i < a.Length; i++) {
                    string b = a[i];
                    if (b.Trim().Length > 0) {
                        combZd.Items.Add(b);
                    }
                }
                if (combZd.Items.Count > 0)
                    combZd.SelectedIndex = 0;
            }
            combtj.SelectedIndex = 0;
        }

        private void UcInfoGet_Load(object sender, EventArgs e)
        {
            Init();
        }
    }
}
