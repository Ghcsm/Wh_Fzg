using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Csmsjcf
{
  public static  class ClsInIPar
    {
        #region sqlParameter

        public static void Getgzinfo()
        {
            ClsDataSplit.ClsdirDirsn = 0;
            ClsDataSplit.ClsFilesn = 0;
            ClsDataSplit.ClsdirCol = "";
            ClsDataSplit.ClsdirMl = "";
            DataTable dt = T_Sysset.GetDataSplit();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ClsDataSplit.ClsdirTable = dt.Rows[0][1].ToString();
            ClsDataSplit.ClsdirDirsn = Convert.ToInt32(dt.Rows[0][2].ToString());
            ClsDataSplit.ClsdirCol = dt.Rows[0][3].ToString().Replace('\\', ',');
            ClsDataSplit.ClsdirMl = dt.Rows[0][4].ToString().Replace('\\', ',');

            ClsDataSplit.ClsFileTable = dt.Rows[0][5].ToString();
            ClsDataSplit.ClsFilesn = Convert.ToInt32(dt.Rows[0][6].ToString());
            string file = dt.Rows[0][7].ToString();
            if (ClsDataSplit.ClsFilesn == 3)
                ClsDataSplit.ClsFileNamecol = file;
            else {
                if (file.IndexOf(';') >= 0) {
                    string[] f = file.Split(';');
                    ClsDataSplit.ClsFileNmaecd = Convert.ToInt32(f[0]);
                    ClsDataSplit.ClsFileNameQian = f[1];
                    ClsDataSplit.ClsFileNameHou = f[2];
                }
                else
                    ClsDataSplit.ClsFileNamecol = file;
            }
            ClsDataSplit.ClsFilezero = Convert.ToBoolean(dt.Rows[0][8].ToString());
            GetExportTable();
        }

        private static void GetExportTable()
        {
            ClsDataSplit.ClsExportTable.Clear();
            ClsDataSplit.ClsExportxlsid.Clear();
            ClsDataSplit.ClsExportCol.Clear();
            DataTable dt = T_Sysset.GetDataSplitExporTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                string table = dr["ImportTable"].ToString();
                string col = dr["ImportCol"].ToString();
                string xlsid = dr["BindId"].ToString();

                ClsDataSplit.ClsExportTable.Add(table);
                ClsDataSplit.ClsExportxlsid.Add(xlsid);
                ClsDataSplit.ClsExportCol.Add(col);
            }
        }


        #endregion
    }
}
