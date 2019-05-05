using DAL;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CsmBorrStat
{
    public partial class FrmBorrLog : Form
    {
        public FrmBorrLog()
        {
            InitializeComponent();
        }
        List<string> lscol = new List<string>();
        List<string> lscollog = new List<string>();
        private string table;
        void InIcol()
        {
            combCol.DataSource = null;
            DataTable dt = null;
            lvQuer.Columns.Clear();
            lvQuer.Items.Clear();
            lvQuer.Columns.Add("序号");
            int id = combTable.SelectedIndex;
            if (id == 0)
                table = "V_borrTable";
            else table = "V_BorrLog";
            if (lscol.Count <= 0 || lscollog.Count <= 0)
                dt = T_Sysset.GetTableName(table);
            else {
                List<string> coltmp = null;
                if (combTable.SelectedIndex == 0)
                    coltmp = lscol.ToList();
                else coltmp = lscollog.ToList();
                for (int i = 0; i < coltmp.Count; i++) {
                    combCol.Items.Add(coltmp[i]);
                    lvQuer.Columns.Add(coltmp[i]);
                }
                return;
            }
            if (dt == null || dt.Rows.Count <= 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++) {
                string str = dt.Rows[i][0].ToString();
                lvQuer.Columns.Add(str);
                if (id == 0)
                    lscol.Add(str);
                else lscollog.Add(str);
            }
            combCol.DataSource = dt;
            combCol.DisplayMember = "Name";
        }

        private void combTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            InIcol();
        }

        private void FrmBorrLog_Shown(object sender, EventArgs e)
        {
            combTable.SelectedIndex = 0;
        }

        private void ButBorrQuer_Click(object sender, EventArgs e)
        {
            GetInfo();
        }

        void GetInfo()
        {
            lvQuer.Items.Clear();
            List<string> coltmp = null;
            if (combTable.SelectedIndex == 0)
                coltmp = lscol.ToList();
            else coltmp = lscollog.ToList();
            DataTable dt = Common.GetborrLog(table, combCol.Text.Trim(), txtgjz.Text.Trim(),
                datetime1.Value.ToString(), datetime2.Value.ToString(), chktime.Checked);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            int i = 1;
            foreach (DataRow dr in dt.Rows) {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                for (int t = 0; t < coltmp.Count; t++) {
                    string col = coltmp[t];
                    string str = dr[col].ToString();
                    lvi.SubItems.AddRange(new string[] { str });
                }
                lvQuer.Items.Add(lvi);
                i++;
            }
        }

        private void SaveFile(string file)
        {
            Workbook work = new Workbook();
            Worksheet wsheek = null;
            if (File.Exists(file))
                work.LoadFromFile(file);
            try {
                wsheek = work.Worksheets[0];
                int rows = wsheek.LastRow + 1;
                for (int i = 0; i < lvQuer.Items.Count; i++) {
                    for (int c = 0; c < lvQuer.Columns.Count; c++) {
                        wsheek.Range[rows + i, c + 1].Text = lv.Items[i].SubItems[c].Text;
                    }
                }
                work.SaveToFile(file, FileFormat.Version2007);
                work.Dispose();
            } catch { }
            MessageBox.Show("导出完成!");
        }

        private void ButBorrDc_Click(object sender, EventArgs e)
        {
            string file = "";
            if (saveFiledig.ShowDialog() == DialogResult.OK)
                file = saveFiledig.FileName;
            else file = "";
            if (file.Trim().Length <= 0)
                return;
            SaveFile(file);
        }
    }
}
