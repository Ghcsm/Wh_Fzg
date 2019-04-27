using System;
using DAL;
using System.Data;
using System.Windows.Forms;

namespace CsmCheck
{
    public static class ClsSetInfo
    {

        public static void GetTableinfo()
        {
            DataTable dt = T_Sysset.GetInfoCheck();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ClsTable.LsTable.Clear();
            ClsTable.lsCol.Clear();
            ClsTable.lsMsgk.Clear();
            ClsTable.lsTabletmp.Clear();
            foreach (DataRow dr in dt.Rows) {
                string tb = dr["InfoCheckTable"].ToString();
                string col = dr["InfocheckCol"].ToString();
                string msgk = dr["InfoCheckMsg"].ToString();
                ClsTable.LsTable.Add(tb);
                ClsTable.lsCol.Add(col);
                ClsTable.lsMsgk.Add(msgk);
            }
        }

        public static void SetArchInfo(int archid, string table, string col, DataGridView dg1, DataGridView dg2)
        {
            DataTable dt = Common.GetInfoTable(archid, table, col);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataTable dt1 = dt.Select("EnterTag=1").CopyToDataTable();
            if (dt1 == null || dt1.Rows.Count <= 0)
                return;
            dg1.BeginInvoke(new Action(() =>
            {
                dg1.DataSource = null;
                dg1.DataSource = dt1;
                dg1.Columns["EnterTag"].Visible = false;
                dg1.ClearSelection();
                Stopsort(dg1);
            }));
            DataTable dt2 = dt.Select("EnterTag=2").CopyToDataTable();
            if (dt2 == null || dt2.Rows.Count <= 0)
                return;
            dg2.BeginInvoke(new Action(() =>
            {
                dg2.DataSource = null;
                dg2.DataSource = dt2;
                dg2.Columns["EnterTag"].Visible = false;
                dg2.ClearSelection();
                Stopsort(dg2);
            }));
        }

        private static void Stopsort(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++) {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public static void ArchStat(Label ls)
        {
            ClsTable.Archzt = false;
            int zt = Common.GetXystat(ClsTable.Archid);
            ls.BeginInvoke(new Action(() =>
            {
                if (zt > 0) {
                    ls.Text = "已校验";
                    ClsTable.Archzt = true;
                }
                else ls.Text = "未校验";

            }));
        }

        public static bool ArchidCheck(DataGridView dgv1, DataGridView dgv2)
        {

            if (dgv1.Rows.Count <= 0 || dgv2.Rows.Count <= 0) {
                return false;
            }
            for (int i = 0; i < dgv1.Rows.Count; i++) {
                for (int t = 0; t < dgv1.Columns.Count; t++) {

                    if (t == dgv1.Columns.Count) {
                        t -= 1;
                    }
                    else if (t > dgv1.Columns.Count) {
                        t = 0;
                    }
                    string col = dgv1.Columns[t].Name.ToString();
                    if (col.Trim().Length <= 0) {
                        dgv1.Columns.RemoveAt(t);
                        dgv2.Columns.RemoveAt(t);
                        t = 0;
                    }
                    if (col.ToLower() == "entertag") {
                        dgv1.Columns.RemoveAt(t);
                        dgv2.Columns.RemoveAt(t);
                        t = 0;
                    }
                    if (dgvbool(t, dgv1, dgv2) == false) {
                        dgv1.Columns.RemoveAt(t);
                        dgv2.Columns.RemoveAt(t);
                        t = 0;
                    }
                }
            }
            if (dgv1.Rows.Count <= 0 && dgv2.Rows.Count <= 0)
                return true;
            else return false;
        }

        private static bool dgvbool(int id, DataGridView dgv1, DataGridView dgv2)
        {
            bool tf = false;
            for (int i = 0; i < dgv1.Rows.Count; i++) {
                string str1 = dgv1.Rows[i].Cells[id].Value.ToString();
                string str2 = dgv2.Rows[i].Cells[id].Value.ToString();
                if (str1 == str2) {
                    tf = false;
                }
                else {
                    return true;
                }
            }
            return tf;

        }

        public static void SetXyinfo()
        {
            Common.SetArchXy(ClsTable.Archid);
        }

        public static void UpdateInfo(string table, int sx, DataGridView dgv1, DataGridView dgv2)
        {
            if (dgv1.Rows.Count <= 0 || dgv2.Rows.Count <= 0)
                return;
            int rows = 0;
            if (sx == 1)
                rows = dgv1.CurrentRow.Index;
            else
                rows = dgv2.CurrentRow.Index;
            string str1 = dgv1.Rows[rows].Cells[0].Value.ToString();
            string str2 = dgv2.Rows[rows].Cells[0].Value.ToString();
            string col1 = dgv1.Columns[0].Name.ToString();
            string str = "";
            if (sx == 1) {
                dgv2.Rows[rows].Cells[0].Value = str1;
                str = str1;
                sx = 2;
            }
            else if (sx == 2) {
                dgv1.Rows[rows].Cells[0].Value = str2;
                str = str2;
                sx = 1;
            }
            Common.UpdateXyinfo(table, col1, str, sx.ToString(), ClsTable.Archid);
            //ArchidCheck(dgv1, dgv2);
        }

        public static void GetuserInfo(StatusStrip st, ToolStripStatusLabel l1, ToolStripStatusLabel lt, ToolStripStatusLabel l2, ToolStripStatusLabel lt2)
        {
            DataTable dt = Common.GetEnteruserInfo(ClsTable.Archid);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            try {
                DataTable dt1 = dt.Select("OneTwoTag=1").CopyToDataTable();
                if (dt1 != null || dt1.Rows.Count > 0) {
                    string user1 = "";
                    string time1 = "";
                    st.BeginInvoke(new Action(() =>
                    {
                        user1 = dt1.Rows[0][0].ToString();
                        time1 = dt1.Rows[0][1].ToString();
                        l1.Text = string.Format("一录：{0}", user1);
                        lt.Text = string.Format("一录时间：{0}", time1);
                    }));
                }
                DataTable dt2 = dt.Select("OneTwoTag=2").CopyToDataTable();
                if (dt2 != null || dt2.Rows.Count > 0) {
                    string user2 = "";
                    string time2 = "";
                    st.BeginInvoke(new Action(() =>
                    {
                        user2 = dt2.Rows[0][0].ToString();
                        time2 = dt2.Rows[0][1].ToString();
                        l2.Text = string.Format("二录：{0}", user2);
                        lt2.Text = string.Format("二录时间：{0}", time2);
                    }));
                }
            } catch { }

        }
    }
}
