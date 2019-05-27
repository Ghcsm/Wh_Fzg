using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Csmsjcf
{
    public static class ClsInIPar
    {
        #region sqlParameter

        public static void Getgzinfo()
        {
            ClsDataSplitPar.ClsdirDirsn = 0;
            ClsDataSplitPar.ClsFilesn = 0;
            ClsDataSplitPar.ClsdirCol = "";
            ClsDataSplitPar.ClsdirMl = "";
            ClsDataSplitPar.ClsdirMlpage = "";
            ClsDataSplitPar.ClsFileNmaecd = 0;
            ClsDataSplitPar.ClsFileNameQian = "";
            ClsDataSplitPar.ClsFileNameHou = "";
            ClsDataSplitPar.ClsFileNamecol = "";
            ClsDataSplitPar.ClsFileDlname = "";
            ClsDataSplitPar.ClsdirPageZero = 0;
            ClsDataSplitPar.ClsFilezero = false;
            ClsDataSplitPar.Clsdircolleg.Clear();
            DataTable dt = T_Sysset.GetDataSplit();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            ClsDataSplitPar.ClsdirTable = dt.Rows[0][1].ToString();
            ClsDataSplitPar.ClsdirDirsn = Convert.ToInt32(dt.Rows[0][2].ToString());
            ClsDataSplitPar.ClsdirCol = dt.Rows[0][3].ToString().Replace('\\', ',');
            string str = dt.Rows[0][3].ToString();
            if (str.IndexOf('\\') >= 0) {
                string[] s = str.Split('\\');
                str = "";
                for (int i = 0; i < s.Length; i++) {
                    if (str.Trim().Length <= 0)
                        str += s[0];
                    else
                        str += "," + s[0];
                    ClsDataSplitPar.Clsdircolleg.Add(s[1]);
                }
            }
            ClsDataSplitPar.ClsdirMl = dt.Rows[0][4].ToString();
            ClsDataSplitPar.ClsFileTable = dt.Rows[0][5].ToString();
            ClsDataSplitPar.ClsFilesn = Convert.ToInt32(dt.Rows[0][6].ToString());
            string file = dt.Rows[0][7].ToString();
            if (ClsDataSplitPar.ClsFilesn == 3)
                ClsDataSplitPar.ClsFileNamecol = file;
            else {
                if (file.IndexOf(';') >= 0) {
                    string[] f = file.Split(';');
                    ClsDataSplitPar.ClsFileNmaecd = Convert.ToInt32(f[0]);
                    ClsDataSplitPar.ClsFileNameQian = f[1];
                    ClsDataSplitPar.ClsFileNameHou = f[2];
                }
                else
                    ClsDataSplitPar.ClsFileNamecol = file;
            }
            ClsDataSplitPar.ClsFilezero = Convert.ToBoolean(dt.Rows[0][8].ToString());
            ClsDataSplitPar.ClsFileDlname = dt.Rows[0][9].ToString();
            ClsDataSplitPar.ClsdirPageZero = Convert.ToInt32(dt.Rows[0][10].ToString());
            ClsDataSplitPar.ClsdirMlpage = dt.Rows[0][11].ToString();
            GetExportTable();
        }

        private static void GetExportTable()
        {
            ClsDataSplitPar.ClsExportTable.Clear();
            ClsDataSplitPar.ClsExportxlsid.Clear();
            ClsDataSplitPar.ClsExportCol.Clear();
            ClsDataSplitPar.ClsExportColLeg.Clear();
            DataTable dt = T_Sysset.GetDataSplitExporTable();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                string table = dr["ImportTable"].ToString();
                string col = dr["ImportCol"].ToString();
                string xlsid = dr["BindId"].ToString();
                ClsDataSplitPar.ClsExportTable.Add(table);
                ClsDataSplitPar.ClsExportxlsid.Add(xlsid);
                string str = "";
                string leg = "";
                if (col.IndexOf(';') >= 0) {
                    string[] strleg = col.Split(';');
                    for (int i = 0; i < strleg.Length; i++) {
                        string[] leg1 = strleg[i].Split(':');
                        if (str.Trim().Length <= 0) {
                            str += leg1[0];
                            leg += leg1[1];
                        }
                        else {
                            str += "," + leg1[0];
                            leg += "," + leg1[1];
                        }
                    }
                    ClsDataSplitPar.ClsExportCol.Add(str);
                    ClsDataSplitPar.ClsExportColLeg.Add(leg);
                }

            }
        }
        #endregion
    }
}
