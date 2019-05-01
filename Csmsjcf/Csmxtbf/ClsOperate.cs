﻿using DAL;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using HLjscom;

namespace Csmsjcf
{
    public static class ClsOperate
    {

        public static DataTable SelectSql(string boxsn)
        {
            DataTable dt = null;
            if (ClsFrmInfoPar.ConverMode == 2)
                Common.DataSplitUpdate(ClsFrmInfoPar.Houseid, boxsn);
            return dt = Common.GetDataSplitBoxsn(ClsFrmInfoPar.Houseid, boxsn, ClsFrmInfoPar.ConverMode);
        }

        public static DataTable SelectSql(string boxsn, string archno)
        {
            DataTable dt = null;
            if (ClsFrmInfoPar.ConverMode == 2)
                Common.DataSplitUpdate(ClsFrmInfoPar.Houseid, boxsn, archno);
            return dt = Common.GetDataSplitBoxsn(ClsFrmInfoPar.Houseid, boxsn, archno, ClsFrmInfoPar.ConverMode);
        }

        //获取自定义table及字段 写入xls
        public static string GetAnjuanInfo(string archid, string xls)
        {
            if (ClsDataSplitPar.ClsExportTable.Count <= 0)
                return "错误：后台未设置导出表";
            Workbook work = new Workbook();
            Worksheet wsheek = null;
            try {
                work.LoadFromFile(xls);
                for (int i = 0; i < ClsDataSplitPar.ClsExportTable.Count; i++) {
                    DataTable dt = Common.GetDataExporTableInfo(archid, ClsDataSplitPar.ClsExportTable[i], ClsDataSplitPar.ClsExportCol[i]);
                    if (dt == null || dt.Rows.Count <= 0)
                        continue;
                    wsheek = work.Worksheets[Convert.ToInt32(ClsDataSplitPar.ClsExportxlsid[i])];
                    int rows = wsheek.LastRow + 1;
                    if (rows == 0)
                        rows = 1;
                    for (int t = 0; t < dt.Rows.Count; t++) {
                        for (int c = 0; c < dt.Columns.Count; c++) {
                            wsheek.Range[rows + t, c + 1].Text = dt.Rows[t][c].ToString();
                        }
                    }
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
            if (ClsDataSplitPar.ClsdirTable.Trim().Length <= 0)
                return "错误：文件夹命名规则中的表名未定义!";
            DataTable dt = Common.GetDataExporTableDirName(archid, ClsDataSplitPar.ClsdirTable, ClsDataSplitPar.ClsdirCol);
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
            if (ClsDataSplitPar.ClsdirTable.Trim().Length <= 0 || ClsDataSplitPar.ClsdirMl.Trim().Length <= 0)
                return null;
            string str = ClsDataSplitPar.ClsdirMl.Replace('\\', ',');
            dt = Common.GetDataExporTableConentName(archid, ClsDataSplitPar.ClsdirTable, str);
            if (dt == null || dt.Rows.Count <= 0)
                return null;
            return dt;
        }


        public static string GetFileName(string archid)
        {
            string str = "";
            if (ClsDataSplitPar.ClsFileTable.Trim().Length <= 0)
                return "错误：后台未设置文件规则表";
            if (ClsFrmInfoPar.FileNamesn == 3) {
                DataTable dt =
                    Common.GetDataExporTableFileName(archid, ClsDataSplitPar.ClsFileTable, ClsDataSplitPar.ClsFileDlname);
                if (dt == null || dt.Rows.Count <= 0)
                    return "错误：未找到文件名规则表中的字段信息";
                str = dt.Rows[0][0].ToString();
            }
            return str;
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
                    lock (ClsFrmInfoPar.Filelock) {
                        ClsWritelog.Writelog(strpath, str);
                        error = false;
                    }
                }
            }
            else {
                for (int i = 0; i < Lsstr.Count; i++) {
                    str = Lsstr[i];
                    if (str.Contains("错误")) {
                        lock (ClsFrmInfoPar.Filelock) {
                            ClsWritelog.Writelog(strpath, str);
                            error = false;
                        }
                    }
                }
            }
            return error;
        }



        public static string OcrPdf(Hljsimage himg, List<string> fcount, string path, int xc, string boxsn, string archno)
        {
            string str = "ok";
            for (int pdf = 0; pdf < fcount.Count; pdf++) {
                string yfile = fcount[pdf];
                string fname = Path.GetFileNameWithoutExtension(yfile);
                string filename = Path.Combine(path, fname + ".pdf");
                str = himg._AutoPdfOcr2(yfile, filename, ClsFrmInfoPar.OcrPath, 1);
                if (str.IndexOf("错误") >= 0) {
                    str = himg._AutoPdfOcr2(yfile, filename, ClsFrmInfoPar.OcrPath, 2);
                    if (str.IndexOf("错误") >= 0) {
                        if (ClsFrmInfoPar.Ocrpdf) {
                            str = himg.Autopdf(yfile, filename);
                            if (str.IndexOf("错误") >= 0)
                                return str;
                        }
                        else return str;
                    }

                }

            }
            return str;
        }

    }
}
