using DAL;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Csmsjcf
{
    public static class ClsOperate
    {

        public static DataTable SelectSql(string boxsn)
        {
            DataTable dt = null;
            if (ClsFrmInfoPar.ConverMode == 2)
                Common.DataSplitUpdate(ClsFrmInfoPar.Houseid, boxsn);
            return dt = Common.GetDataSplitBoxsn(ClsFrmInfoPar.Houseid, boxsn);

        }

        public static DataTable SelectSql(string boxsn, string archno)
        {
            DataTable dt = null;
            if (ClsFrmInfoPar.ConverMode == 2)
                Common.DataSplitUpdate(ClsFrmInfoPar.Houseid, boxsn, archno);
            return dt = Common.GetDataSplitBoxsn(ClsFrmInfoPar.Houseid, boxsn, archno);
        }

        //获取自定义table及字段 写入xls
        public static string GetAnjuanInfo(string archid, string xls)
        {
            if (ClsDataSplit.ClsExportTable.Count <= 0)
                return "错误：后台未设置导出表";
            DataTable dt = null;
            Workbook work = new Workbook();
            Worksheet wsheek = null;
            try {
                work.LoadFromFile(xls);
                for (int i = 0; i < ClsDataSplit.ClsExportTable.Count; i++) {
                    dt = Common.GetDataExporTableInfo(archid, ClsDataSplit.ClsExportTable[i], ClsDataSplit.ClsExportCol[i]);
                    if (dt == null || dt.Rows.Count <= 0)
                        continue;
                    wsheek = work.Worksheets[ClsDataSplit.ClsExportxlsid[i]];
                    int rows = wsheek.LastRow + 1;
                    for (int t = 0; t < dt.Rows.Count; t++) {
                        for (int c = 0; c < dt.Columns.Count; c++) {
                            wsheek.Range[rows + t, c + 1].Text = dt.Rows[t][c].ToString();
                        }
                    }
                    wsheek.SaveToFile(xls, "");
                }
                work.SaveToFile(xls, FileFormat.Version2007);
                work.Dispose();
                return "ok";
            } catch (Exception e) {
                work.Dispose();
                return "错误：" + e.ToString();
            }
        }


        public static string GetDirColName(string archid)
        {
            string str = "";
            if (ClsDataSplit.ClsdirTable.Trim().Length <= 0)
                return "错误：文件夹命名规则中的表名未定义!";
            DataTable dt = Common.GetDataExporTableDirName(archid, ClsDataSplit.ClsdirTable, ClsDataSplit.ClsdirCol);
            if (dt == null || dt.Rows.Count <= 0)
                return "错误：未找到文件夹字段信息!";
            for (int i = 0; i < dt.Columns.Count; i++) {
                if (i != dt.Columns.Count - 1)
                    str += dt.Columns[i].ToString() + "\\";
                else str += dt.Columns[i].ToString();
            }
            return str;
        }

        public static DataTable GetdirmlInfo(string archid)
        {
            DataTable dt = null;
            if (ClsDataSplit.ClsdirTable.Trim().Length <= 0 || ClsDataSplit.ClsdirMl.Trim().Length <= 0)
                return null;
            string str = ClsDataSplit.ClsdirMl.Replace('\\', ',');
            dt = Common.GetDataExporTableConentName(archid, ClsDataSplit.ClsdirTable, str);
            if (dt == null || dt.Rows.Count <= 0)
                return null;
            return dt;
        }


        public static string GetFileName(string archid)
        {
            string str = "";
            if (ClsDataSplit.ClsFileTable.Trim().Length <= 0)
                return "错误：后台未设置文件规则表";
            if (ClsFrmInfoPar.FileNamesn == 3) {
                DataTable dt =
                    Common.GetDataExporTableFileName(archid, ClsDataSplit.ClsFileTable, ClsDataSplit.ClsFileNamecol);
                if (dt == null || dt.Rows.Count <= 0)
                    return "错误：未找到文件名规则表中的字段信息";
                str = dt.Rows[0][1].ToString();
            }
            return str;
        }


        //获取文件名
        public static string GetFileName(string path, string fileformat)
        {
            try {
                string str = "";
                var files = Directory.GetFiles(path, fileformat);
                if (files.Length == 0)
                    str = "1";
                else
                    str = (files.Length + 1).ToString();

                if (ClsDataSplit.ClsFilezero) {
                    int cd = ClsDataSplit.ClsFileNmaecd - str.Length - ClsDataSplit.ClsFileNameQian.Length - ClsDataSplit.ClsFileNameHou.Length;
                    str = str.PadLeft(cd, '0');
                }
                str = ClsDataSplit.ClsFileNameQian + str + ClsDataSplit.ClsFileNameHou;

                return str;
            } catch (Exception e) {
                return "错误：" + e.ToString();
            }
        }

        //下载文件
        public static string DownFile(string imgpath, string file)
        {
            try {
                string str = "";
                if (file.Trim().Length <= 0)
                    return "错误：文件名长度不正确";
                if (ClsFrmInfoPar.Ftp == 2)
                    str = Path.Combine(imgpath, "ArchSave", file.Substring(0, 8), file);
                else {
                    HLFtp.HFTP ftp = new HLFtp.HFTP();
                    string path = Path.Combine(Common.LocalTempPath, file.Substring(0, 8));
                    str = Path.Combine(Common.LocalTempPath, file.Substring(0, 8), file);
                    if (!Directory.Exists(path)) {
                        Directory.CreateDirectory(path);
                    }
                    if (File.Exists(str) == true) {
                        File.Delete(str);
                    }
                    if (ftp.CheckRemoteFile("ArchSave", file.Substring(0, 8), file)) {
                        if (!ftp.DownLoadFile("ArchSave", file.Substring(0, 8), str, file)) {
                            return "错误：下载文件失败";
                        }
                    }
                    else {
                        return "错误：远程文件不存在!";
                    }
                }
                return str;
            } catch (Exception e) {
                return "错误：" + e.ToString();
            }
        }

        //添加任务
        public static bool AddTask()
        {
            ClsFrmInfoPar.TaskBoxCount.Clear();
            if (ClsFrmInfoPar.TaskBoxCounttmp.Count <= 0)
                return false;
            for (int i = 0; i < ClsFrmInfoPar.TaskBoxCounttmp.Count; i++) {
                string s = ClsFrmInfoPar.TaskBoxCounttmp[i];
                string[] b = s.Split('-');
                int b1 = Convert.ToInt32(b[0]);
                int b2 = Convert.ToInt32(b[1]);
                for (int t = b1; t <= b2; t++) {
                    ClsFrmInfoPar.TaskBoxCount.Add(t.ToString());
                }
            }
            if (ClsFrmInfoPar.TaskBoxCount.Count > 0)
                return true;
            else
                return false;
        }

        //获取xls模版
        public static string XlsPath()
        {
            string str = "";
            string xlsmode = ClsFrmInfoPar.XlsPath;
            if (!File.Exists(xlsmode)) {
                str = "错误：未找到Xls模版文件!";
                return str;
            }
            str = Path.Combine(ClsFrmInfoPar.MimgPath, DateTime.Now.ToString("yyyyMMdd") + ".xls");
            if (!File.Exists(str)) {
                File.Copy(xlsmode, str);
            }
            return str;
        }

        public static bool Iserror(List<string> Lsstr, string strpath)
        {
            string str = "";
            bool error = true;
            if (Lsstr.Count == 1) {
                str = Lsstr[0];
                if (str.Contains("错误")) {
                    ClsWritelog.Writelog(strpath, str);
                    error = false;
                }
            }
            else {
                for (int i = 0; i < Lsstr.Count; i++) {
                    str = Lsstr[i];
                    if (str.Contains("错误")) {
                        ClsWritelog.Writelog(strpath, str);
                        error = false;
                    }
                }
            }
            return error;
        }

    }
}
