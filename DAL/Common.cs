﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAL
{
    public class Common
    {
        #region 基本配置
        public const string AdminPassword = "888888";
        public const string ArchScanPath = "ArchScan";
        public const string ArchIndexPath = "ArchIndex";
        public const string ArchSavePah = "ArchSave";
        public const string ArchUpdate = "Update";
        public const string TifExtension = ".Tif";
        public const string ScanTempFile = "ScanTemp.Tif";
        public static string LocalTempPath = @"c:\Temp";
        public static string CabinetOperationType = "1";   //下架操作
        public static int Backimg = 0;
        public static int Backimbd = 0;
        public static string ModuleNameReg = "目录登记";
        public static int 档案未质检 = 0;

        public static string FtpIP;
        public static string FtpUser;
        public static string FtpPwd;
        public static string gArchScanPath;
        public static string OperName;
        public static Int32 OperID;
        public static string ContentReg;
        public static string sfname;


        public enum 档案状态
        {
            无 = 0,

            扫描中 = 1,
            质检退回 = 2,
            扫描完 = 3,
            排序中 = 4,
            排序完 = 5,
            质检中 = 6,
            质检完 = 7,
            已关联 = 8
        }

        #endregion


        #region  Contents

        public static DataTable GetContentsModule()
        {
            try {
                string strSql = "select * from M_ContentsModule order by code ";
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql);
                return dt;
            } catch {
                return null;
            }
        }


        public static int ContentsInster(string table, List<string> lscol, Dictionary<int, string> dirxx, int archid)
        {
            string coltmp = "";
            string zdtmp = "";
            string strSql = "insert into " + table + " (";
            for (int i = 0; i < lscol.Count; i++) {
                if (i != lscol.Count - 1) {

                    coltmp += lscol[i] + ",";
                    zdtmp += "'" + dirxx[i + 1].ToString() + "',";

                }
                else {
                    coltmp += lscol[i] + ",Archid ) values (";
                    zdtmp += "'" + dirxx[i + 1].ToString() + "','" + archid + "')";
                }
            }
            strSql += coltmp + zdtmp;
            SQLHelper.ExecScalar(strSql);
            strSql = "PInsertArchContent";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@ARCHID", archid);
            p[1] = new SqlParameter("@UserID", T_User.UserId);
            p[2] = new SqlParameter("@mID", archid);
            p[3] = new SqlParameter("@info", "新增");
            int id = SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            return id;
        }

        public static int ContentsEdit(string table, List<string> lscol, Dictionary<int, string> dirxx, string mid, int archid)
        {
            string coltmp = "";
            string strSql = "update " + table + " set ";
            for (int i = 0; i < lscol.Count; i++) {
                if (i != lscol.Count - 1) {
                    coltmp += lscol[i] + "='" + dirxx[i + 1].ToString() + "',";
                }
                else {
                    coltmp += lscol[i] + "='" + dirxx[i + 1].ToString() + "' where id=" + mid;
                }
            }
            strSql += coltmp;
            SQLHelper.ExecScalar(strSql);
            strSql = "PupdatetArchContent";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@ARCHID", archid);
            p[1] = new SqlParameter("@UserID", T_User.UserId);
            p[2] = new SqlParameter("@mID", mid);
            p[3] = new SqlParameter("@info", "更新");
            int id = SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            return id;
        }


        public static DataTable LoadContents(string table, string col, string pages, int id, int arid)
        {
            string coltmp = col.Replace(';', ',');
            try {
                string strSql = "";
                if (id == 1)
                    strSql = "select id," + coltmp + " from " + table + " where archid=@archid ";
                else
                    strSql = "select id," + coltmp + " from " + table + " where archid=@archid  order by " + pages;
                SqlParameter p1 = new SqlParameter("@archid", arid);
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static void ContentsDel(string table, int mid, int arid)
        {
            string strSql = "delete from " + table + " where Archid=" + arid + " and id=" + mid;
            SQLHelper.ExecScalar(strSql);
            strSql = "PDeleteArchContent";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@mID", mid);
            p[2] = new SqlParameter("@archid", arid);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        #endregion

        #region  house

        public static void ArchGrounding(int houseid, int gui, int ab, int col, int row, int boxxh, string juannum, string boxsn)
        {
            string strSql = "PInsertCabinet";
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@HouseID", houseid);
            p[1] = new SqlParameter("@CabNo", gui);
            p[2] = new SqlParameter("@CabAB", ab);
            p[3] = new SqlParameter("@ColNo", col);
            p[4] = new SqlParameter("@RowNo", row);
            p[5] = new SqlParameter("@BoxNo", boxxh);
            p[6] = new SqlParameter("@BoxCount", juannum);
            p[7] = new SqlParameter("@UserID", T_User.UserId);
            p[8] = new SqlParameter("@BoxSN", boxsn);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static void ArchDel(int arid)
        {
            string strSql = "PdelCabInfo";
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@@ArchID", arid);
            p[1] = new SqlParameter("@@UserID", T_User.UserId);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static DataTable GetOthersys()
        {
            string strSql = "select OtherSys from M_User where id=@id ";
            SqlParameter p1 = new SqlParameter("@id", T_User.UserId);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static int IsGuiCount(int ab)
        {
            try {
                string strSql = "select count(*) from M_imagefile where CabNo=@gui and CabAb=@ab and Houseid=@houseid";
                SqlParameter p1 = new SqlParameter("@gui", V_HouseSetCs.HouseGui);
                SqlParameter p2 = new SqlParameter("@houseid", V_HouseSetCs.Houseid);
                SqlParameter p3 = new SqlParameter("@ab", ab);
                int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2, p3).ToString());
                return id;
            } catch {
                return 0;
            }
        }

        public static int IsGuiColRowsCount(int ab, int col, int row)
        {
            try {
                string strSql = "select max(boxsn) from M_imagefile where Houseid=@houseid and CabNo=@gui and CabAb=@ab and Colno=@col and RowNo=@row ";
                SqlParameter p1 = new SqlParameter("@gui", V_HouseSetCs.HouseGui);
                SqlParameter p2 = new SqlParameter("@houseid", V_HouseSetCs.Houseid);
                SqlParameter p3 = new SqlParameter("@ab", ab);
                SqlParameter p4 = new SqlParameter("@col", col);
                SqlParameter p5 = new SqlParameter("@row", row);
                int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2, p3, p4, p5).ToString());
                return id;
            } catch {
                return 0;
            }
        }

        public static int IsGuiArchNoCount(int ab, int col, int row, int boxsn)
        {
            try {
                string strSql = "select max(ArchNO) from M_imagefile where Houseid=@houseid and CabNo=@gui and CabAb=@ab and Colno=@col and RowNo=@row and Boxsn=@boxsn";
                SqlParameter p1 = new SqlParameter("@gui", V_HouseSetCs.HouseGui);
                SqlParameter p2 = new SqlParameter("@houseid", V_HouseSetCs.Houseid);
                SqlParameter p3 = new SqlParameter("@ab", ab);
                SqlParameter p4 = new SqlParameter("@col", col);
                SqlParameter p5 = new SqlParameter("@row", row);
                SqlParameter p6 = new SqlParameter("@boxsn", boxsn);
                int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2, p3, p4, p5, p6).ToString());
                return id;
            } catch {
                return 0;
            }
        }

        public static int IsGuiArchNoSate(int ab, int col, int row, int boxsn, int juan)
        {
            try {
                string strSql = "select imgfile, Checked from M_imagefile where Houseid=@houseid and CabNo=@gui and CabAb=@ab and Colno=@col and RowNo=@row and Boxsn=@boxsn and ArchNo=@juan";
                SqlParameter[] p = new SqlParameter[7];
                p[0] = new SqlParameter("@gui", V_HouseSetCs.HouseGui);
                p[1] = new SqlParameter("@houseid", V_HouseSetCs.Houseid);
                p[2] = new SqlParameter("@ab", ab);
                p[3] = new SqlParameter("@col", col);
                p[4] = new SqlParameter("@row", row);
                p[5] = new SqlParameter("@boxsn", boxsn);
                p[6] = new SqlParameter("@juan", juan);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p);
                if (dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    string file = dr["imgfile"].ToString();
                    string str = dr["Checked"].ToString();
                    if (file.Length <= 0)
                        return 1;
                    else if (str.Length <= 0)
                        return 2;
                    else
                        return 3;
                }
                return 0;
            } catch {
                return 0;
            }
        }

        #endregion

        #region Query

        public static DataTable QueryData(string Field, string operation, string FieldValue)
        {
            DataTable dt;
            try {
                string strSql = "PQueryData";
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@Field", Field);
                p[1] = new SqlParameter("@operation", operation);
                p[2] = new SqlParameter("@FieldValue", FieldValue);
                dt = DAL.SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
                return dt;
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
                return null;
            }
        }

        public static DataTable QuerboxsnInfo(int id)
        {
            string strSql = "select boxsn,archno, IMGFILE,ArchYanShou from M_IMAGEFILE where id=@arid";
            SqlParameter p1 = new SqlParameter("@arid", id);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static void QuerSetCheckLog(int arid, int boxsn, int archno, int stat, string sour)
        {
            string strSql = "PInserQuerCheckLog";
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@archid", arid);
            p[2] = new SqlParameter("@boxsn", boxsn);
            p[3] = new SqlParameter("@archno", archno);
            p[4] = new SqlParameter("@ip", T_ConFigure.IPAddress);
            p[5] = new SqlParameter("@stat", stat);
            p[6] = new SqlParameter("@setsour", sour);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }


        #endregion

        #region  gArchselect

        public static DataTable QueryBoxsn(string boxsn)
        {
            try {
                string strSql = "select * from V_ImgFile where boxsn=@boxsn and houseid=@houseid ";
                SqlParameter p1 = new SqlParameter("@boxsn", boxsn);
                SqlParameter p2 = new SqlParameter("@houseid", V_HouseSetCs.Houseid);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2);
                return dt;
            } catch (Exception e) {
                MessageBox.Show("获取数据失败" + e.ToString());
                return null;

            }
        }

        public static DataTable QueryBoxsn(string boxsn, string archtype)
        {
            try {
                string strSql = "select * from V_ImgFile where  houseid=@houseid and boxsn=@boxsn and ArchType=@archtype";
                SqlParameter p1 = new SqlParameter("@boxsn", boxsn);
                SqlParameter p2 = new SqlParameter("@houseid", V_HouseSetCs.Houseid);
                SqlParameter p3 = new SqlParameter("@archtype", archtype);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2, p3);
                return dt;
            } catch (Exception e) {
                MessageBox.Show("获取数据失败" + e.ToString());
                return null;

            }
        }

        public static void UpdatePages(string pages, int archid)
        {
            string strSql = "PUpPages";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@archid", archid);
            p[1] = new SqlParameter("@pages", pages);
            p[2] = new SqlParameter("@userid", T_User.UserId);
            p[3] = new SqlParameter("@ip", T_ConFigure.IPAddress);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static string Getpages(int arid)
        {
            string strSql = "SELECT PAGES FROM M_IMAGEFILE WHERE ID=@arid";
            SqlParameter p1 = new SqlParameter("@arid", arid);
            return SQLHelper.ExecScalar(strSql, p1).ToString();
        }



        #endregion

        #region ScanIndexCheckModule
        public static void SetArchWorkState(int ArchID, int ArchState)
        {
            try {
                string strSql = "PSetArchWorkState";
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@Archid", ArchID);
                p[1] = new SqlParameter("@ArchState", ArchState);
                p[2] = new SqlParameter("@userid", T_User.UserId);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        public static void SetScanFinish(int arid, int pages, int error, int zt)
        {
            string strSql = "PScanWorkReg";
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@Archid", arid);
            p[2] = new SqlParameter("@ArchState", zt);
            p[3] = new SqlParameter("@Pages", pages);
            p[4] = new SqlParameter("@ScanError", error);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static void SetIndexFinish(int arid, string file, int zt)
        {
            string strSql = "PUpdateIndexInfo";
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@FileName", file);
            p[2] = new SqlParameter("@Archid", arid);
            p[3] = new SqlParameter("@ArchState", zt);
            p[4] = new SqlParameter("@PageIndexInfo", "");
            p[5] = new SqlParameter("@DeleTag", "");
            p[6] = new SqlParameter("@stat", 1);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }
        public static void SetCheckFinish(int arid, string file)
        {
            string strSql = "PUpdateCheckInfo";
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@FileName", file);
            p[2] = new SqlParameter("@Archid", arid);
            p[3] = new SqlParameter("@ArchState", (int)T_ConFigure.ArchStat.质检完);
            p[4] = new SqlParameter("@PageIndexInfo", "");
            p[5] = new SqlParameter("@DeleTag", "");
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static void SetIndexCancel(int arid, string pageindex, string deletag)
        {
            string strSql = "PUpdateIndexInfo";
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@FileName", "");
            p[2] = new SqlParameter("@Archid", arid);
            p[3] = new SqlParameter("@ArchState", 3);
            p[4] = new SqlParameter("@PageIndexInfo", pageindex);
            p[5] = new SqlParameter("@DeleTag", deletag);
            p[6] = new SqlParameter("@stat", "0");
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static DataTable ReadPageIndexInfo(int ArchID)
        {
            try {
                string strSql = "select PageIndexInfo ,DeleTag from M_imagefile where  id=@ArchID";
                SqlParameter p1 = new SqlParameter("@ArchID", ArchID);
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static void WiteUpTask(int arid, string archpos, string filename, int archstat, int page, string fileapth)
        {
            string strSql = "PUpTask";
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@TypeModule", T_ConFigure.FtpStyle);
            p[1] = new SqlParameter("@Archid", arid);
            p[2] = new SqlParameter("@ArchPos", archpos);
            p[3] = new SqlParameter("@FileName", filename);
            p[4] = new SqlParameter("@ArchState", archstat);
            p[5] = new SqlParameter("@Userid", T_User.LoginName);
            p[6] = new SqlParameter("@IP", T_ConFigure.IPAddress);
            p[7] = new SqlParameter("@pages", page);
            p[8] = new SqlParameter("@filepath", fileapth);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static int Gettask(int arid)
        {
            string strSql = "select count(*) from M_UpTask where Archid=@arid";
            SqlParameter p1 = new SqlParameter("@arid", arid);
            int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1).ToString());
            return i;
        }

        public static void DelTask(int arid)
        {
            string strSql = "delete from M_UpTask where Archid=@id";
            SqlParameter p1 = new SqlParameter("@id", arid);
            SQLHelper.ExecScalar(strSql, p1);
        }

        public static DataTable GetTask()
        {
            try {
                string strSql = "";
                if (T_User.LoginName != "Admin")
                    strSql = "select TypeModule'模式',Archid'卷id',ArchPos'案卷号',FileName'文件名',ArchStat'流程',Userid'用户',IP,DateTime'时间',Filepath'路径' from M_UpTask where Userid=@userid";
                else
                    strSql = "select TypeModule'模式',Archid'卷id',ArchPos'案卷号',FileName'文件名',ArchStat'流程',Userid'用户',IP,DateTime'时间',Filepath'路径' from M_UpTask";
                DataTable dt = SQLHelper.ExcuteTable(strSql);
                return dt;
            } catch {
                return null;
            }

        }


        #endregion

        #region sundry

        public static void SetloginIp(int id)
        {
            string strSql = "PInserLogin";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@ip", T_ConFigure.IPAddress);
            p[2] = new SqlParameter("@stat", id);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static DataTable GetboxArchno(int b)
        {
            try {
                string strSql = "select id from M_imagefile where boxsn=@box and CHECKED=1";
                DataTable dt = SQLHelper.ExcuteTable(strSql);
                return dt;
            } catch {
                return null;
            }

        }


        #endregion

        #region Printparm

        public static void SavePrintParmXy(string str)
        {
            string strSql = "PupPrintParm";
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@xy", str);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static string GetPrintParmXy()
        {
            try {
                string strSql = "select PrintInfoXy from M_PrintParm";
                return SQLHelper.ExecScalar(strSql).ToString();
            } catch {
                return "";
            }
        }

        public static DataTable GetPrintInfoDataTable(string table, List<string> col, int arid)
        {
            try {
                string strSql = "select ";//" from  @table  where Archid=@arid";
                for (int i = 0; i < col.Count; i++) {
                    if (i != col.Count - 1)
                        strSql += col[i] + ",";
                    else
                        strSql += col[i];
                }
                strSql += " from " + table + "  where Archid=@arid and EnterTag=1";
                SqlParameter p1 = new SqlParameter("@arid", arid);
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }

        }


        #endregion

        #region GetPrintInfo

        public static DataTable GetPrintArchInfo(int archid, string table)
        {
            try {
                string strSql = "select * from @table  where Archid=@archid";
                SqlParameter p1 = new SqlParameter("@table", table);
                SqlParameter p2 = new SqlParameter("@archid", archid);
                DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1, p2);
                return dt;
            } catch {
                return null;
            }
        }

        public static DataTable GetPrintConten(int archid, string table, List<string> col, string xh)
        {
            DataTable dt = null;
            string str = "";
            for (int i = 0; i < col.Count; i++) {
                if (i < col.Count - 1)
                    str += col[i] + ",";
                else str += col[i];
            }

            try {
                string strSql = "";
                SqlParameter p1 = new SqlParameter("@archid", archid);
                if (xh.Trim().Length > 0) {
                    strSql = "select " + str + " from " + table + " where Archid=@archid order by " + xh;
                }
                else
                    strSql = "select " + str + " from " + table + " where Archid=@archid ";
                dt = SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return dt;
            }
        }

        #endregion

        #region 其他统计

        public static int GetArchPages(int ArchID)
        {
            try {
                string strSql = "select pages from M_IMAGEFILE where id=@id ";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }

        public static DataTable GetGroup(string date1, string date2)
        {
            string strSql = "PWorkGroup";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@Date1", date1);
            p[1] = new SqlParameter("@Date2", date2);
            p[2] = new SqlParameter("@OperId", T_User.UserId);
            DataTable data = SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
            return data;
        }
        public static DataTable GetGroupAdmin(string date1, string date2, string b1, string b2, string type)
        {
            string strSql = "PWorkGroupAdmin";
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Date1", date1);
            p[1] = new SqlParameter("@Date2", date2);
            p[2] = new SqlParameter("@box1", b1);
            p[3] = new SqlParameter("@box2", b2);
            p[4] = new SqlParameter("@type", type);
            DataTable data = SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
            return data;
        }

        public static void Writelog(int ArchID, string str)
        {

            string strSql = "Plog";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@Archid", ArchID);
            p[2] = new SqlParameter("@Operation", str);
            p[3] = new SqlParameter("@ipadd", T_ConFigure.IPAddress);
            int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }


        #endregion

        #region backup

        public static DataTable GetBoxsnSql(string b, string b1, bool bk)
        {
            DataTable dt = null;
            string strSql = "";
            try {
                if (bk)
                    strSql = "select rank() over(ORDER BY id)'序号',Boxsn '盒号',ArchNo '卷号',ImgFile '文件' from M_IMAGEFILE where boxsn>@b1 and  boxsn<=@b2 and CHECKED=1 and BACKUPED is null";
                else
                    strSql = "select rank() over(ORDER BY id)'序号',Boxsn '盒号',ArchNo '卷号',ImgFile '文件' from M_IMAGEFILE where boxsn>@b1 and  boxsn<=@b2 and CHECKED=1";
                SqlParameter p1 = new SqlParameter("@b1", b);
                SqlParameter p2 = new SqlParameter("@b2", b1);
                dt = SQLHelper.ExcuteTable(strSql, p1, p2);
                return dt;
            } catch {
                return dt;
            }
        }

        #endregion

        #region ImportTable

        public static DataTable GetImportTable()
        {
            string strSql = "select ImportTable from M_GenSetImport";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static DataTable GetImportTable(string str)
        {
            string strSql = "select ImportInfoZd from M_GenSetImport where ImportTable=@table";
            SqlParameter p1 = new SqlParameter("@table", str);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        private static string UpdateArchtype(string table)
        {
            string strSql = "select MIN(id) from M_IMAGEFILE where ArchType is null  ";
            string id = SQLHelper.ExecScalar(strSql).ToString();
            if (id.Trim().Length > 0) {
                strSql = "update M_IMAGEFILE set ArchType=@type where id=@id";
                SqlParameter p1 = new SqlParameter("@type", table);
                SqlParameter p2 = new SqlParameter("@id", id);
            }
            return id;
        }

        public static void ImportData(string table, string col, string data, bool ck, bool wyzbl, int wyz)
        {
            if (ck == true) {
                string coldata = "";
                string strSql = "select count(*) from " + table + " where ";
                string[] a = col.Split(',');
                string[] b = data.Split(',');
                if (wyzbl == false) {

                    for (int i = 0; i < a.Length; i++) {
                        string s = b[i];
                        if (s.Trim().Length > 0) {
                            if (i != a.Length - 1)
                                coldata += a[i] + "='" + b[i] + "' and ";
                            else
                                coldata += a[i] + "='" + b[i] + "'";
                        }
                    }
                }
                else {
                    coldata += a[wyz] + "='" + b[wyz] + "'";
                }
                strSql += coldata;
                int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql).ToString());
                if (id > 0) {
                    strSql = "delete from " + table + " where " + coldata;
                    SQLHelper.ExecScalar(strSql);
                }
            }
            string archid = UpdateArchtype(table);
            string strSql1 = "insert into " + table;
            string[] c = col.Split(',');
            string[] d = data.Split(',');
            string coltmp = "( ";
            string zdtmp = "(' ";
            for (int i = 0; i < c.Length; i++) {
                if (i != c.Length - 1) {

                    coltmp += c[i] + ",";
                    zdtmp += d[i] + "','";
                }
                else {
                    coltmp += c[i];
                    zdtmp += d[i];
                }
            }
            strSql1 += coltmp + ", Archid)" + " values " + zdtmp + archid + "')";
            SQLHelper.ExecScalar(strSql1);
        }



        #endregion

        #region InfoEnter

        public static void GetInfoEnterSet()
        {
            ClsInfoEnter.InfoTable.Clear();
            ClsInfoEnter.InfoCol.Clear();
            ClsInfoEnter.InfoNum.Clear();
            ClsInfoEnter.InfoTableName.Clear();
            ClsInfoEnter.InfoLbWidth.Clear();
            ClsInfoEnter.InfotxtWidth.Clear();
            string strSql = "select * from M_GenSetInfo order by id";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows) {
                ClsInfoEnter.InfoTable.Add(dr["InfoTable"].ToString());
                ClsInfoEnter.InfoCol.Add(dr["InfoAddzd"].ToString());
                ClsInfoEnter.InfoNum.Add(dr["InfoNum"].ToString());
                ClsInfoEnter.InfoTableName.Add(dr["InfoName"].ToString());
                ClsInfoEnter.InfoLbWidth.Add(dr["InfoLabWidth"].ToString());
                ClsInfoEnter.InfotxtWidth.Add(dr["InfoTxtWidth"].ToString());
            }
        }

        public static DataTable GetTableCol(string table)
        {
            string strSql =
                "SELECT B.name ,C.value FROM sys.tables A INNER JOIN sys.columns B ON B.object_id = A.object_id LEFT JOIN sys.extended_properties C ON C.major_id = B.object_id AND C.minor_id = B.column_id WHERE A.name = @table";
            SqlParameter p1 = new SqlParameter("@table", table);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static int SaveInfo(int t, int archid, Dictionary<int, string> dirxx, int enter, int ts)
        {
            try {
                string coltmp = "";
                string zdtmp = "";
                string table = ClsInfoEnter.InfoTable[t];
                string strSql = "select count(*) from " + table + " where Archid=@arid and EnterTag=@Etag";
                SqlParameter p1 = new SqlParameter("@table", table);
                SqlParameter p2 = new SqlParameter("@arid", archid);
                SqlParameter p3 = new SqlParameter("@Etag", enter);
                int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2, p3).ToString());
                if (id <= 0) {
                    strSql = "insert into " + table + " (" + ClsInfoEnter.InfoCol[t].Replace(';', ',') + ",Archid,Entertag ) values (";
                    for (int i = 1; i < dirxx.Count + 1; i++) {
                        if (i != dirxx.Count) {

                            zdtmp += "'" + dirxx[i].ToString() + "',";

                        }
                        else {
                            zdtmp += "'" + dirxx[i].ToString() + "'," + archid + "," + enter + ")";
                        }
                    }
                    strSql += zdtmp;
                }
                else {
                    strSql = "update " + table + " set ";
                    string[] str = ClsInfoEnter.InfoCol[t].Replace(';', ',').Split(',');
                    for (int i = 0; i < str.Length; i++) {
                        if (i != str.Length - 1) {
                            coltmp += str[i] + "='" + dirxx[i + 1].ToString() + "',";
                        }
                        else {
                            coltmp += str[i] + "='" + dirxx[i + 1].ToString() + "' where Archid=" + archid + " and EnterTag=" + enter;
                        }
                    }
                    strSql += coltmp;
                }
                SQLHelper.ExecScalar(strSql);
                WirteWork(archid, ts, enter, table);
                SetInfoEnterTable(archid, ClsInfoEnter.InfoTableName[t]);
                return 1;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        private static void SetInfoEnterTable(int archid, string type)
        {
            string strSql = "update M_IMAGEFILE set ArchType=@type where id=@arid";
            SqlParameter p1 = new SqlParameter("@arid", archid);
            SqlParameter p2 = new SqlParameter("@type", type);
            SQLHelper.ExecScalar(strSql, p1, p2);
        }

        private static void WirteWork(int archid, int ts, int enter, string table)
        {
            string strSql = "PWiteWork";
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@Archid", archid);
            p[2] = new SqlParameter("@enter", enter.ToString());
            p[3] = new SqlParameter("@ipadd", T_ConFigure.IPAddress);
            p[4] = new SqlParameter("@Ts", ts);
            p[5] = new SqlParameter("@tablename", table);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static DataTable GetInfoTable(int t, int archid, int enter)
        {
            string strSql = "select * from " + ClsInfoEnter.InfoTable[t] + " where Archid=@arid and EnterTag=@Etag";
            SqlParameter p1 = new SqlParameter("@arid", archid);
            SqlParameter p2 = new SqlParameter("@Etag", enter);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2);
            return dt;
        }

        public static void GetQuerInfo()
        {
            ClsQuerInfo.QuerTable = "";
            ClsQuerInfo.QuerTableList.Clear();
            string strSql = "select QuerTable,QuerInfoZd from M_Genset";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            ClsQuerInfo.QuerTable = dr["QuerTable"].ToString();
            string[] str = dr["QuerInfoZd"].ToString().Split(';');
            ClsQuerInfo.QuerTableList = new List<System.String>(str);
        }

        #endregion

        #region DataSplit

        public static void DataSplitUpdate(int houseid, string boxsn)
        {
            string strSql = "update M_IMAGEFILE set SPLITERROR=null where boxsn=@boxsn and HOUSEID=@houseid";
            SqlParameter p1 = new SqlParameter("@boxsn", boxsn);
            SqlParameter p2 = new SqlParameter("@houseid", houseid);
            SQLHelper.ExecScalar(strSql, p1, p2);
        }
        public static void DataSplitUpdate(string arid)
        {
            string strSql = "update M_IMAGEFILE set SPLITERROR=1 where id=@id";
            SqlParameter p1 = new SqlParameter("@id", arid);
            SQLHelper.ExecScalar(strSql, p1);
        }
        public static void DataSplitUpdate(int houseid, string boxsn, string archno)
        {
            string strSql = "update M_IMAGEFILE set SPLITERROR=null where boxsn=@boxsn and ARCHNO=@archno and HOUSEID=@houseid";
            SqlParameter p1 = new SqlParameter("@boxsn", boxsn);
            SqlParameter p2 = new SqlParameter("@archno", archno);
            SqlParameter p3 = new SqlParameter("@houseid", houseid);
            SQLHelper.ExecScalar(strSql, p1, p2, p3);
        }

        public static DataTable GetDataSplitBoxsn(int houseid, string boxsn, int id)
        {
            string strSql = "";
            if (id == 3)
                strSql = "select ID,BOXSN,ARCHNO,PAGES,IMGFILE From M_IMAGEFILE where  HOUSEID=@houseid and CHECKED=1  and BOXSN=@boxsn order by ARCHNO";
            else
                strSql = "select ID,BOXSN,ARCHNO,PAGES,IMGFILE From M_IMAGEFILE where  HOUSEID=@houseid and CHECKED=1 and SPLITERROR is null and BOXSN=@boxsn order by ARCHNO";
            SqlParameter p1 = new SqlParameter("@boxsn", boxsn);
            SqlParameter p2 = new SqlParameter("@houseid", houseid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2);
            return dt;
        }
        public static DataTable GetDataSplitBoxsn(int houseid, string boxsn, string archno, int id)
        {
            string strSql = "";
            if (id == 3)
                strSql = "select ID,BOXSN,ARCHNO,PAGES,IMGFILE From M_IMAGEFILE where  HOUSEID=@houseid and CHECKED=1  and BOXSN=@boxsn and ARCHNO=@archno order by ARCHNO";
            else
                strSql = "select ID,BOXSN,ARCHNO,PAGES,IMGFILE From M_IMAGEFILE where  HOUSEID=@houseid and CHECKED=1 and SPLITERROR is null and BOXSN=@boxsn and ARCHNO=@archno order by ARCHNO";

            SqlParameter p1 = new SqlParameter("@boxsn", boxsn);
            SqlParameter p2 = new SqlParameter("@houseid", houseid);
            SqlParameter p3 = new SqlParameter("@archno", archno);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2, p3);
            return dt;
        }

        public static DataTable GetDataExporTableInfo(string archid, string table, string col)
        {
            string strSql = "select " + col.Replace(';', ',');
            strSql += " from " + table + " where Archid=@archid";
            SqlParameter p1 = new SqlParameter("@archid", archid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static DataTable GetDataExporTableConentName(string archid, string table, string col)
        {
            string[] str = col.Split(',');
            string strSql = "select " + col;
            strSql += " from " + table + " where Archid=@archid" + " order by " + str[1];
            SqlParameter p1 = new SqlParameter("@archid", archid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static DataTable GetDataExporTableDirName(string archid, string table, string col)
        {
            string strSql = "select " + col;
            strSql += " from " + table + " where Archid=@archid";
            SqlParameter p1 = new SqlParameter("@archid", archid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static DataTable GetDataExporTableFileName(string archid, string table, string col)
        {
            string strSql = "select " + col;
            strSql += " from " + table + " where Archid=@archid";
            SqlParameter p1 = new SqlParameter("@archid", archid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        #endregion

        #region InfoCheck

        public static DataTable GetInfoTable(int archid, string table, string col)
        {
            string strSql = "select " + col.Replace(';', ',') + ",EnterTag from " + table + " where Archid=@archid";
            SqlParameter p1 = new SqlParameter("@archid", archid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static void SetArchXy(int ArchID)
        {
            try {
                string strSql = "PArchXy";
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@archid", ArchID);
                p[2] = new SqlParameter("@ipadd", T_ConFigure.IPAddress);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }

        public static void UpdateXyinfo(string table, string col, string str, string id, int arid)
        {
            string strSql = "update " + table + " set " + col + "=@str where EnterTag=@id and Archid=@arid";
            SqlParameter p1 = new SqlParameter("@str", str);
            SqlParameter p2 = new SqlParameter("@id", id);
            SqlParameter p3 = new SqlParameter("@arid", arid);
            SQLHelper.ExecScalar(strSql, p1, p2, p3);

            strSql = "PInfoChecklog";
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@UserID", T_User.UserId);
            p[1] = new SqlParameter("@archid", arid);
            p[2] = new SqlParameter("@tableid", id);
            p[3] = new SqlParameter("@tablename", table);
            p[4] = new SqlParameter("@tableCol", col);
            p[5] = new SqlParameter("@tableStr", str);
            p[6] = new SqlParameter("@ipadd", T_ConFigure.IPAddress);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static DataTable GetEnteruserInfo(int arid)
        {
            string strSql = "select * from V_enterUserinfo where Archid=@arid";
            SqlParameter p1 = new SqlParameter("@arid", arid);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }


        #endregion

        #region Log

        public static DataTable GetLogTable()
        {
            string strSql = "select logTable from M_Tablelog";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static DataTable GetLogTablecol(string table)
        {
            string strSql = "select logTable from M_Tablelog";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static DataTable GetLogTableInfo(string table, string col, string txt, string time1, string time2)
        {
            string strSql = "";
            if (txt.Trim().Length <= 0)
                strSql = "select * from " + table + " where  dotime>=@time1 and dotime<=@time2";
            else
                strSql = "select * from " + table + " where " + col + " like '%" + txt + "%' and  dotime>=@time1 and dotime<=@time2";
            SqlParameter p1 = new SqlParameter("@time1", time1);
            SqlParameter p2 = new SqlParameter("@time2", time2);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2);
            return dt;
        }


        #endregion

        #region QuerBorr

        public static DataTable QuerBorrData(string Field, string operation, string FieldValue, string time1, string time2, bool chkgjz, bool time, string tablecol)
        {
            DataTable dt;
            try {
                string strSql = "PQueryBorr";
                SqlParameter[] p = new SqlParameter[8];
                p[0] = new SqlParameter("@Field", Field);
                p[1] = new SqlParameter("@operation", operation);
                p[2] = new SqlParameter("@FieldValue", FieldValue);
                p[3] = new SqlParameter("@time1", time1);
                p[4] = new SqlParameter("@time2", time2);
                p[5] = new SqlParameter("@gjzbool", Convert.ToInt32(chkgjz));
                p[6] = new SqlParameter("@time", Convert.ToInt32(time));
                p[7] = new SqlParameter("@timecol", tablecol);
                dt = DAL.SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
                return dt;
            } catch (Exception ee) {
                MessageBox.Show("查询失败:" + ee.ToString());
                return null;
            }
        }

        #endregion

        #region borr

        public static void SaveborrInfo(string name, string sex, string sfz, string phone, string add, string work,
            string artype, string yt, string time, string page, string bz, int arid, string boxsn, string archno)
        {
            try {
                string strSql = "PInseBorr";
                SqlParameter[] p = new SqlParameter[17];
                p[0] = new SqlParameter("@userid", T_User.UserId);
                p[1] = new SqlParameter("@jname", name);
                p[2] = new SqlParameter("@sex", sex);
                p[3] = new SqlParameter("@phone", phone);
                p[4] = new SqlParameter("@time", time);
                p[5] = new SqlParameter("@bz", bz);
                p[6] = new SqlParameter("@card", sfz);
                p[7] = new SqlParameter("@address", add);
                p[8] = new SqlParameter("@worktype", work);
                p[9] = new SqlParameter("@archtype", artype);
                p[10] = new SqlParameter("@archUse", yt);
                p[11] = new SqlParameter("@Pages", page);
                p[12] = new SqlParameter("@arid", arid);
                p[13] = new SqlParameter("@cuser", T_User.LoginName);
                p[14] = new SqlParameter("@ipadd", T_ConFigure.IPAddress);
                p[15] = new SqlParameter("@boxsn", boxsn);
                p[16] = new SqlParameter("@archno", archno);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                WriteBorrLog(boxsn, archno, arid, work);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
        }

        public static void WriteBorrLog(string boxsn, string archno, int arid, string opter)
        {
            try {
                string strSql = "PInseBorrLog";
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@boxsn", boxsn);
                p[1] = new SqlParameter("@archno", archno);
                p[2] = new SqlParameter("@arid", arid);
                p[3] = new SqlParameter("@opter", opter);
                p[4] = new SqlParameter("@opteruser", T_User.LoginName);
                p[5] = new SqlParameter("@ipadd", T_ConFigure.IPAddress);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            } catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
        }

        public static DataTable GetborrLog(string table, string col, string gjz, string time1, string time2, bool time)
        {
            string strSql = "PQueryBorrLog";
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@col", col);
            p[1] = new SqlParameter("@table", table);
            p[2] = new SqlParameter("@gjz", gjz);
            p[3] = new SqlParameter("@time1", time1);
            p[4] = new SqlParameter("@time2", time2);
            p[5] = new SqlParameter("@time", Convert.ToInt32(time));
            DataTable dt = SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
            return dt;
        }


        #endregion

        #region MyRegion



        //public static int GetArchPages(int ArchID)
        //{
        //    try {
        //        string strSql = "select pages from   M_IMAGEFILE where id=@id";
        //        SqlParameter p1 = new SqlParameter("@id", ArchID);
        //        int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
        //        return i;
        //    } catch {
        //        return 0;
        //    }
        //}

        public static int GetCode(string Code)
        {
            try {
                string strSql = "select id from M_IMAGEFILE where ARCNUM=@code";
                SqlParameter p1 = new SqlParameter("@code", Code);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }

        public static int Gid(string id)
        {
            try {
                string strSql = "select id from T_Soid where SoId=@id";
                SqlParameter p1 = new SqlParameter("@id", id);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }


        public static string Gidstr(int id)
        {
            string str = "";
            DataTable dt;
            try {
                string strSql = "Psoid";
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@UID", id);
                dt = DAL.SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
                if (dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    str = dr["sid"].ToString();
                }
                return str;
            } catch {
                return str;
            }
        }


        public static int GetUserRw(int id)
        {
            try {
                string strSql = "select Userid from T_UpRw where Userid=@id";
                SqlParameter p1 = new SqlParameter("@id", id);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }


        public static string GetArchZD(string Zongsn, int zl)
        {
            try {
                string strSql = "select count(*) from   M_IMAGEFILE where Zongsn=@box and ZhuangDing is not null";
                SqlParameter p1 = new SqlParameter("@box", Zongsn);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                if (i > 0) {
                    string sqlu = "select ZhuangDing from M_IMAGEFILE where Zongsn=@box ";
                    SqlParameter p2 = new SqlParameter("@box", Zongsn);
                    string user = SQLHelper.ExecScalar(sqlu, p2).ToString();
                    string Sql = "select 登录名 from [V_USERTBL] where id=@zL  ";
                    SqlParameter p3 = new SqlParameter("@zL", user);
                    string name = SQLHelper.ExecScalar(Sql, p3).ToString();
                    return name;
                }
                return null;
            } catch {
                return null;
            }
        }

        public static string GetArchZL(string zongsn, int zl)
        {
            try {
                string strSql = "select count(*) from   M_IMAGEFILE where Zongsn=@box and ZENLI is not null";
                SqlParameter p1 = new SqlParameter("@box", zongsn);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                if (i > 0) {
                    string sqlu = "select ZENLI from M_IMAGEFILE where Zongsn=@box ";
                    SqlParameter p2 = new SqlParameter("@box", zongsn);
                    string user = SQLHelper.ExecScalar(sqlu, p2).ToString();
                    string Sql = "select 登录名 from [V_USERTBL] where id=@zL  ";
                    SqlParameter p3 = new SqlParameter("@zL", user);
                    string name = SQLHelper.ExecScalar(Sql, p3).ToString();
                    return name;
                }
                return null;
            } catch {
                return null;
            }
        }




        public static int UpdateZD(string zongsn, int zL)
        {
            try {
                string strSql = "update M_IMAGEFILE set ZhuangDing=@zL where zongsn=@box and CHECKED=1";
                SqlParameter p1 = new SqlParameter("@zL", zL);
                SqlParameter p2 = new SqlParameter("@box", zongsn);
                SQLHelper.ExecScalar(strSql, p1, p2);
                string strsql = "Select count(*) from M_IMAGEFILE where zongsn=@box and ZhuangDing=@zl ";
                SqlParameter p3 = new SqlParameter("@zL", zL);
                SqlParameter p4 = new SqlParameter("@box", zongsn);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strsql, p3, p4));
                return i;

            } catch {
                return 0;
            }
        }


        public static int UpdateZL(string Zongsn, int zL)
        {
            try {
                // string strSql = "update M_IMAGEFILE set ZENLI=@zL where boxsn=@box and pages>0 and imgfile is not null";
                string strSql = "update M_IMAGEFILE set ZENLI=@zL where Zongsn=@box and CHECKED=1";
                SqlParameter p1 = new SqlParameter("@zL", zL);
                SqlParameter p2 = new SqlParameter("@box", Zongsn);
                SQLHelper.ExecScalar(strSql, p1, p2);
                string strsql = "Select count(*) from M_IMAGEFILE where Zongsn=@box and ZENLI=@zl ";
                SqlParameter p3 = new SqlParameter("@zL", zL);
                SqlParameter p4 = new SqlParameter("@box", Zongsn);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strsql, p3, p4));
                return i;

            } catch {
                return 0;
            }
        }

        public static int JLqspage(int ArchID, string page)
        {
            try {
                string strSql = "update M_IMAGEFILE set Qspage=@page where ID=@id";
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", ArchID);
                p[1] = new SqlParameter("@page", page);
                int i = SQLHelper.ExecuteNonQuery(strSql, CommandType.Text, p);
                return i;
            } catch {
                return 0;
            }
        }


        public static string GetQspage(int ArchID)
        {
            try {
                string strSql = "select Qspage from M_IMAGEFILE  where ID=@id";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                return SQLHelper.ExecScalar(strSql, p1).ToString();

            } catch {
                return "0";
            }
        }


        public static int CleaState(int ArchID)
        {
            try {
                string strSql = "update M_IMAGEFILE set ArchState=0 where ID=@id";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }

        public static int GaiBRwState(int ArchID)
        {
            try {
                string strSql = "update T_UpRw set zt=1 where Archid=@id";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }


        private static int DelRwid(int Archid)
        {
            try {
                string strSql = "delete from T_UpRw where Archid=@Archid  ";
                SqlParameter p1 = new SqlParameter("@Archid", Archid);
                //  DataTable dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }



        public static int GetRw(int ArchID)
        {
            try {
                string strSql = "select archid from T_UpRw where Archid=@id";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }

        public static int GetJuan(int box)
        {
            try {
                string strSql = "select COUNT(*) from M_IMAGEFILE where BOXSN=@box";
                SqlParameter p1 = new SqlParameter("@box", box);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }

        public static string GetFileNameByArchID(int ArchID)
        {
            try {
                string strSql = "select imgfile from   M_IMAGEFILE where id=@id";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                string filename = (string)SQLHelper.ExecScalar(strSql, p1);
                return filename;
            } catch {
                return null;
            }
        }


        public static string GetCurrentTime()
        {
            try {
                string strSql = "SELECT replace(replace(replace(REPLACE( CONVERT(varchar(100), GETDATE(), 21),'-',''),' ',''),'.',''),':','')";
                string NowTime = (string)SQLHelper.ExecScalar(strSql);
                return NowTime;
            } catch {
                return "";
            }
        }

        public static int GetArchWorkState(int ArchID)
        {
            try {
                int ArchState = 0;
                string strSql = "select ArchState from M_ImageFile where  id=@ArchID";
                SqlParameter p1 = new SqlParameter("@ArchID", ArchID);
                ArchState = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1).ToString());
                return ArchState;
            } catch {
                return 0;
            }
        }



        public static DataTable GetOperator(int ArchID)
        {
            DataTable dt;
            try {
                string strSql = "select * from V_ArchOperator where id=@archid ";
                SqlParameter p1 = new SqlParameter("@archid", ArchID);
                dt = SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }
        public static DataTable GetOperator(int enter, int ArchID)
        {
            DataTable dt;
            string strSql = string.Empty;
            try {
                if (enter == 1) {
                    strSql = "select * from V_ArchOperator where id=@archid ";
                }
                else if (enter == 2) {
                    strSql = "select * from V_ArchOperator2 where id=@archid ";
                }
                SqlParameter p1 = new SqlParameter("@archid", ArchID);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }


        public static int GetHouseGui(int Houseid)
        {
            try {
                string strSql = "select MAX(cabinetno) from [T_STOREROOM] where houseid=@Houseid";
                SqlParameter p1 = new SqlParameter("Houseid", Houseid);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }
        }



        public static DataTable GetHouse(int Houseid)
        {
            DataTable dt;
            try {
                string strSql = "select * from [T_STOREROOM] where houseid=@houseid order by HOUSEID ";
                SqlParameter p1 = new SqlParameter("@houseid", Houseid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static DataTable GetHouseCanS(int Houseid, int Gui)
        {
            DataTable dt;
            try {
                string strSql = "select * from [T_STOREROOM] where houseid=@houseid and CABINETNO=@gui ";
                SqlParameter p1 = new SqlParameter("@houseid", Houseid);
                SqlParameter p2 = new SqlParameter("@gui", Gui);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1, p2);
                return dt;
            } catch {
                return null;
            }
        }


        public static DataTable GetTimeInfo(int ArchID)
        {
            DataTable dt;
            try {
                string strSql = "select * from V_GetTime where id=@archid ";
                SqlParameter p1 = new SqlParameter("@archid", ArchID);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }



        public static DataTable GetArchInfoByHouseAndBoxSN(int HouseID, int BoxSN)
        {
            DataTable dt;
            try {

                string strSql = "select * from V_imagefile where boxsn=@boxsn and houseid=@houseid order by  id";
                SqlParameter p1 = new SqlParameter("@boxsn", BoxSN);
                SqlParameter p2 = new SqlParameter("@houseid", HouseID);
                dt = SQLHelper.ExcuteTable(strSql, p1, p2);
                return dt;
            } catch {
                return null;
            }
        }


        public static DataTable GetArchInfoByHouseBarCode(int HouseID, int BoxSN, int Boxsnz)
        {
            DataTable dt;
            try {

                string strSql = "select RANK() OVER(ORDER BY BarCode ),BarCode 'code'  from V_imagefile where boxsn>=@boxsn and boxsn<=@Boxsnz  and houseid=@houseid ";
                SqlParameter p1 = new SqlParameter("@boxsn", BoxSN);
                SqlParameter p2 = new SqlParameter("@houseid", HouseID);
                SqlParameter p3 = new SqlParameter("@Boxsnz", Boxsnz);
                dt = SQLHelper.ExcuteTable(strSql, p1, p2, p3);
                return dt;
            } catch {
                return null;
            }
        }

        public static int GetXystat(int aid)
        {
            try {
                string strTF = "select CheckXyState from M_IMAGEFILE where id=@archid";
                SqlParameter t1 = new SqlParameter("@archid", aid);
                int tf = Convert.ToInt32(SQLHelper.ExecScalar(strTF, t1));
                return tf;
            } catch {
                return 0;
            }
        }

        public static void SetCheckXy(int ArchID, int ArchState, string pages)
        {
            try {
                string strSql = "PUpdateXyxx";
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@Archid", ArchID);
                p[2] = new SqlParameter("@zt", ArchState);
                p[3] = new SqlParameter("@pages", pages);
                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            } catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }

        }


        public static void SaveCoverPrintParameter(T_CoverPrintSet Cover)
        {
            try {
                string strSql = "UPDATE T_CoverPrintSet  SET  [Ywbh_X]=@Ywbh_X,[Ywbh_Y]=@Ywbh_Y ,[Dah_X]=@Dah_X ,[Dah_Y]=@Dah_Y ,[Dawz_X]=@Dawz_X ,[Dawz_Y]=@Dawz_Y,[Qlr_X]=@Qlr_X ,[Qlr_Y]=@Qlr_Y," +
                                   "[Ywr_X]=@Ywr_X,[Ywr_Y]=@Ywr_Y,[Bdczh_X]=@Bdczh_X,[Bdczh_Y]=@Bdczh_Y,[Bdcdy_X]=@Bdcdy_X,[Bdcdy_Y]=@Bdcdy_Y,[Dizi_X]=@Dizi_X,[Dizi_Y]=@Dizi_Y ,[JianJu]=@jianju," +
                                   "[LJr_x]=@ljr_X,[Ljr_y]=@ljr_Y,[Shr_x]=@Shr_x,[Shr_y]=@Shr_y,[LjTime_x]=@LjTime_x,[LjTime_y]=@LjTime_y";
                SqlParameter[] p = new SqlParameter[23];
                p[0] = new SqlParameter("@Ywbh_X", Cover.Ywbh_X);
                p[1] = new SqlParameter("@Ywbh_Y", Cover.Ywbh_Y);
                p[2] = new SqlParameter("@Dah_X", Cover.Dah_X);
                p[3] = new SqlParameter("@Dah_Y", Cover.Dah_Y);
                p[4] = new SqlParameter("@Dawz_X", Cover.Dawz_X);
                p[5] = new SqlParameter("@Dawz_Y", Cover.Dawz_Y);
                p[6] = new SqlParameter("@Qlr_X", Cover.Qlr_X);
                p[7] = new SqlParameter("@Qlr_Y", Cover.Qlr_Y);
                p[8] = new SqlParameter("@Ywr_X", Cover.Ywr_X);
                p[9] = new SqlParameter("@Ywr_Y", Cover.Ywr_Y);
                p[10] = new SqlParameter("@Bdczh_X", Cover.Bdczh_X);
                p[11] = new SqlParameter("@Bdczh_Y", Cover.Bdczh_Y);
                p[12] = new SqlParameter("@Bdcdy_X", Cover.Bdcdy_X);
                p[13] = new SqlParameter("@Bdcdy_Y", Cover.Bdcdy_Y);
                p[14] = new SqlParameter("@Dizi_X", Cover.Dizi_X);
                p[15] = new SqlParameter("@Dizi_Y", Cover.Dizi_Y);
                p[16] = new SqlParameter("@jianju", Cover.JianJu);
                p[17] = new SqlParameter("@LJr_x", Cover.Ljr_X);
                p[18] = new SqlParameter("@LJr_y", Cover.Ljr_Y);
                p[19] = new SqlParameter("@Shr_x", Cover.Shr_X);
                p[20] = new SqlParameter("@Shr_y", Cover.Shr_Y);
                p[21] = new SqlParameter("@LjTime_x", Cover.Ljtime_X);
                p[22] = new SqlParameter("@LjTime_y", Cover.Ljtime_Y);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.Text, p);
                MessageBox.Show("更新成功!");
            } catch {
                MessageBox.Show("参数更新失败!");
            }

        }

        public static T_CoverPrintSet ReadCoverPrintParameter()
        {
            T_CoverPrintSet Cover = new T_CoverPrintSet();
            DataTable dt;
            try {

                string strSql = "select * from T_CoverPrintSet";
                dt = SQLHelper.ExcuteTable(strSql);
                if (dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    Cover.Ywbh_X = Convert.ToInt32(dr["Ywbh_X"]);
                    Cover.Ywbh_Y = Convert.ToInt32(dr["Ywbh_Y"]);

                    Cover.Dah_X = Convert.ToInt32(dr["Dah_X"]);
                    Cover.Dah_Y = Convert.ToInt32(dr["Dah_Y"]);

                    Cover.Dawz_X = Convert.ToInt32(dr["Dawz_X"]);
                    Cover.Dawz_Y = Convert.ToInt32(dr["Dawz_Y"]);


                    Cover.Qlr_X = Convert.ToInt32(dr["Qlr_X"]);
                    Cover.Qlr_Y = Convert.ToInt32(dr["Qlr_Y"]);

                    Cover.Ywr_X = Convert.ToInt32(dr["Ywr_X"]);
                    Cover.Ywr_Y = Convert.ToInt32(dr["Ywr_Y"]);

                    Cover.Bdczh_X = Convert.ToInt32(dr["Bdczh_X"]);
                    Cover.Bdczh_Y = Convert.ToInt32(dr["Bdczh_Y"]);

                    Cover.Bdcdy_X = Convert.ToInt32(dr["Bdcdy_X"]);
                    Cover.Bdcdy_Y = Convert.ToInt32(dr["Bdcdy_Y"]);

                    Cover.Dizi_X = Convert.ToInt32(dr["Dizi_X"]);
                    Cover.Dizi_Y = Convert.ToInt32(dr["Dizi_Y"]);

                    Cover.Ljr_X = Convert.ToInt32(dr["Ljr_X"]);
                    Cover.Ljr_Y = Convert.ToInt32(dr["Ljr_Y"]);

                    Cover.Shr_X = Convert.ToInt32(dr["Shr_X"]);
                    Cover.Shr_Y = Convert.ToInt32(dr["Shr_Y"]);

                    Cover.Ljtime_X = Convert.ToInt32(dr["Ljtime_X"]);
                    Cover.Ljtime_Y = Convert.ToInt32(dr["Ljtime_Y"]);

                    Cover.JianJu = Convert.ToInt32(dr["jianju"]);
                }
                return Cover;
            } catch {
                return null;
            }

        }

        //public static T_FaYuan ReadFyInfo(int archid)
        //{
        //    T_FaYuan Fy = new T_FaYuan();
        //    DataTable dt;
        //    int tf = 0;
        //    try
        //    {
        //        // string strSql = "select * from T_DajFengMian where 档号 like @gjz1 and 案卷号 like @gjz2";
        //        string strSql = "select * from T_DajFengMian where Archid=@archid";
        //        SqlParameter p1 = new SqlParameter("@archid", archid);
        //        dt = SQLHelper.ExcuteTable(strSql, p1);
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.Rows[0];
        //            Fy.ID = Convert.ToInt32(dr["FID"]);
        //            Fy.Qzongsn = dr["全宗号"].ToString();
        //            Fy.AnJuansn = dr["案卷号"].ToString();
        //            Fy.MLsn = dr["目录号"].ToString();
        //            Fy.BaoGuanQi = dr["保管期限"].ToString();
        //            Fy.AnJuanTitle = dr["案卷题名"].ToString();
        //            string Qnian = dr["起始年代"].ToString();
        //            string Qyue = dr["起始月份"].ToString();
        //            string Qtime = dr["起始日期"].ToString();
        //            Fy.QiZhiDate = Qnian + Qyue + Qtime;
        //            string Znian = dr["终止年代"].ToString();
        //            string Zyue = dr["终止月份"].ToString();
        //            string Ztime = dr["终止日期"].ToString();
        //            Fy.ZhiDate = Znian + Zyue + Ztime;
        //        }
        //        try
        //        {
        //            strSql = "select ZENLI from M_IMAGEFILE where ID=@archid";
        //            SqlParameter t1 = new SqlParameter("@archid", archid);
        //            //int tf = Convert.ToInt32(SQLHelper.ExecScalar(strSql, t1));                    
        //            if (SQLHelper.ExecScalar(strSql) == DBNull.Value)
        //            {
        //                tf = 0;
        //            }
        //            else
        //            {
        //                tf = Convert.ToInt32(SQLHelper.ExecScalar(strSql, t1));
        //            }
        //        }
        //        catch
        //        {
        //            tf = 0;
        //        }
        //        Fy.ZLID = tf;
        //        return Fy;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}

        public static DataTable QueryXxblinfo(string gjz1, string gjz2, int id)
        {
            DataTable dt;
            string strSql = "";
            try {
                if (id == 0) {
                    strSql = "select * from T_Tdxx_tmp  where " + gjz1 + " like @gjz";
                }
                else if (id == 1) {
                    strSql = "select * from T_Fwxx_tmp  where " + gjz1 + " like @gjz";
                }

                SqlParameter p1 = new SqlParameter("@gjz", "%" + gjz2 + "%");
                dt = SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }

        }

        //public static T_TaXiangQuan ReadTaXiangQuanInfo(int ArchID)
        //{
        //    T_TaXiangQuan TaXiangQuan = new T_TaXiangQuan();
        //    DataTable dt;
        //    try
        //    {
        //        string strSql = "select * from T_TaXiangQuan where archid=@archid";
        //        SqlParameter p1 = new SqlParameter("@archid", ArchID);
        //        dt = SQLHelper.ExcuteTable(strSql, p1);
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.Rows[0];
        //            TaXiangQuan.OWNER = dr["OWNER"].ToString();
        //            TaXiangQuan.OWNNUM = dr["OWNNUM"].ToString();
        //            TaXiangQuan.TaXiangQuanRen = dr["TaXiangQuanRen"].ToString();
        //            TaXiangQuan.TaXiangNum = dr["TaXiangNum"].ToString();
        //            TaXiangQuan.ARCHNUM = dr["ARCHNUM"].ToString();
        //            TaXiangQuan.ArchID = ArchID;
        //        }
        //        return TaXiangQuan;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static void SaveFaYuanInfo(T_FaYuan Fy)
        //{
        //    try
        //    {
        //        string Qnian = "";
        //        string Qyue = "";
        //        string Qtime = "";
        //        string Znian = "";
        //        string Zyue = "";
        //        string Ztime = "";
        //        char[] zero = { '0' };
        //        if (Fy.QiZhiDate.Length == 4)
        //        {
        //            Qnian = Fy.QiZhiDate;
        //        }
        //        else if (Fy.QiZhiDate.Length == 6)
        //        {
        //            Qnian = Fy.QiZhiDate.Substring(0, 4);
        //            Qyue = Fy.QiZhiDate.Substring(4, 2);
        //        }
        //        else if (Fy.QiZhiDate.Length == 8)
        //        {
        //            Qnian = Fy.QiZhiDate.Substring(0, 4);
        //            Qyue = Fy.QiZhiDate.Substring(4, 2);
        //            Qtime = Fy.QiZhiDate.Substring(6, 2);
        //        }
        //        if (Fy.ZhiDate.Length == 4)
        //        {
        //            Znian = Fy.ZhiDate;
        //        }
        //        else if (Fy.ZhiDate.Length == 6)
        //        {
        //            Znian = Fy.ZhiDate.Substring(0, 4);
        //            Zyue = Fy.ZhiDate.Substring(4, 2);
        //        }
        //        else if (Fy.ZhiDate.Length == 8)
        //        {
        //            Znian = Fy.ZhiDate.Substring(0, 4);
        //            Zyue = Fy.ZhiDate.Substring(4, 2);
        //            Ztime = Fy.ZhiDate.Substring(6, 2);
        //        }
        //        string strSelet = "select * from T_DajFengMian where Archid=@id";
        //        SqlParameter t1 = new SqlParameter("@id", Fy.ArchID);
        //        int tf = Convert.ToInt32(SQLHelper.ExecScalar(strSelet, t1));
        //        // int Maxnull = Convert.ToInt32(SQLHelper.ExecScalar(strSelet));
        //        if (tf != 0)
        //        {
        //            //  string strSql = "update T_DajFengMian set 档号=@dangsn,案卷号=@anjuansn,案卷题名=@anjuantitle,起止日期=@qidate,保管期限=@baoguantime,Archid=@archid where fid=@fid";
        //            string strSql = "update T_DajFengMian set 全宗号=@Qzongsn,目录号=@mlsn,案卷号=@anjuansn,案卷题名=@anjuantitle,保管期限=@baoguantime,起始年代=@qiyers,起始月份=@qiyue,起始日期=@qtime,终止年代=@Zyers,终止月份=@zyue,终止日期=@ztime,zt=@zt where Archid=@arcid";
        //            SqlParameter[] p = new SqlParameter[13];
        //            p[0] = new SqlParameter("@Qzongsn", Fy.Qzongsn.PadLeft(5, '0'));
        //            p[1] = new SqlParameter("@mlsn", Fy.MLsn.PadLeft(3, '0'));
        //            p[2] = new SqlParameter("@anjuansn", Fy.AnJuansn.PadLeft(5, '0'));
        //            p[3] = new SqlParameter("@anjuantitle", Fy.AnJuanTitle);
        //            p[4] = new SqlParameter("@baoguantime", Fy.BaoGuanQi);
        //            p[5] = new SqlParameter("@qiyers", Qnian);
        //            p[6] = new SqlParameter("@qiyue", Qyue);
        //            p[7] = new SqlParameter("@qtime", Qtime);
        //            p[8] = new SqlParameter("@Zyers", Znian);
        //            p[9] = new SqlParameter("@zyue", Zyue);
        //            p[10] = new SqlParameter("@ztime", Ztime);
        //            p[11] = new SqlParameter("@arcid", Fy.ArchID);
        //            p[12] = new SqlParameter("@zt", 1);
        //            SQLHelper.ExecuteNonQuery(strSql, CommandType.Text, p);

        //        }
        //        else
        //        {
        //            //  string strSql = "update T_DajFengMian set 档号=@dangsn,案卷号=@anjuansn,案卷题名=@anjuantitle,起止日期=@qidate,保管期限=@baoguantime,Archid=@archid where fid=@fid";
        //            // string strSql = " insert into T_DajFengMian ([档号],[案卷号],[案卷题名],[起止日期],[保管期限],[Archid])  values (@dangsn,@anjuansn,@anjuantitle,@qidate,@baoguantime,@archid ) ";
        //            string strSql = "insert into T_DajFengMian([全宗号],[目录号],[案卷号],[案卷题名],[保管期限],[起始年代],[起始月份],[起始日期],[终止年代],[终止月份],[终止日期] ,[Archid])values (@qzongsn,@mlsn,@anjuan,@anjuantitle,@baoguanqi,@qnian,@qyue,@qtime,@znian,@zyue,@ztime,@Archid)";
        //            SqlParameter[] p = new SqlParameter[13];
        //            p[0] = new SqlParameter("@qzongsn", Fy.Qzongsn.PadLeft(5, '0'));
        //            p[1] = new SqlParameter("@mlsn", Fy.MLsn.PadLeft(3, '0'));
        //            p[2] = new SqlParameter("@anjuan", Fy.AnJuansn.PadLeft(5, '0'));
        //            p[3] = new SqlParameter("@anjuantitle", Fy.AnJuanTitle);
        //            p[4] = new SqlParameter("@baoguanqi", Fy.BaoGuanQi);
        //            p[5] = new SqlParameter("@qnian", Qnian);
        //            p[6] = new SqlParameter("@qyue", Qyue);
        //            p[7] = new SqlParameter("@qtime", Qtime);
        //            p[8] = new SqlParameter("@znian", Znian);
        //            p[9] = new SqlParameter("@zyue", Zyue);
        //            p[10] = new SqlParameter("@ztime", Ztime);
        //            p[11] = new SqlParameter("@archid", Fy.ArchID);
        //            p[12] = new SqlParameter("@zt", 1);
        //            SQLHelper.ExecuteNonQuery(strSql, CommandType.Text, p);
        //        }

        //        string strSqlid = "update M_IMAGEFILE set zenli=@zenli, ZongSn=@zongsn where ID=@id";
        //        string str = Fy.Qzongsn.TrimStart(zero) + "-" + Fy.MLsn.TrimStart(zero) + "-" + Fy.AnJuansn.TrimStart(zero);

        //        SqlParameter[] p1 = new SqlParameter[3];
        //        p1[0] = new SqlParameter("@zongsn", str);
        //        p1[1] = new SqlParameter("@id", Fy.ArchID);
        //        p1[2] = new SqlParameter("@zenli", Fy.ZLID);
        //        SQLHelper.ExecuteNonQuery(strSqlid, CommandType.Text, p1);

        //        string strSqlml = "update T_DajMuL set 全宗号=@Qzongsn,目录号=@mlsn,案卷号=@anjuansn where Archid=@archid";
        //        SqlParameter[] p2 = new SqlParameter[4];
        //        p2[0] = new SqlParameter("@Qzongsn", Fy.Qzongsn.PadLeft(5, '0'));
        //        p2[1] = new SqlParameter("@mlsn", Fy.MLsn.PadLeft(3, '0'));
        //        p2[2] = new SqlParameter("@anjuansn", Fy.AnJuansn.PadLeft(5, '0'));
        //        p2[3] = new SqlParameter("@archid", Fy.ArchID);
        //        SQLHelper.ExecuteNonQuery(strSqlml, CommandType.Text, p2);


        //        //补录条数
        //        string stStrbl = "PSaveChanQuanInfo";
        //        SqlParameter[] ts = new SqlParameter[4];
        //        ts[0] = new SqlParameter("@UserID", T_User.UserId);
        //        ts[1] = new SqlParameter("@ArchID", Fy.ArchID);
        //        ts[2] = new SqlParameter("@ArchNum", Fy.ARCHNUM);
        //        ts[3] = new SqlParameter("@TiaoS", Fy.tiaos);
        //        int i = DAL.SQLHelper.ExecuteNonQuery(stStrbl, CommandType.StoredProcedure, ts);
        //        MessageBox.Show("ok");
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show(ee.ToString());
        //    }

        //}

        //public static void SaveTaXiangInfo(T_TaXiangQuan TaXiang)
        //{
        //    //try
        //    //{
        //    //    string strSql = "PSaveTaXiangInfo";
        //    //    SqlParameter[] p = new SqlParameter[7];
        //    //    p[0] = new SqlParameter("@UserID", T_User.UserId);
        //    //    p[1] = new SqlParameter("@ArchID", TaXiang.ArchID);
        //    //    p[2] = new SqlParameter("@Owner", TaXiang.OWNER);
        //    //    p[3] = new SqlParameter("@OwnerNum", TaXiang.OWNNUM);
        //    //    p[4] = new SqlParameter("@TaXiangQuanRen", TaXiang.TaXiangQuanRen);
        //    //    p[5] = new SqlParameter("@TaXiangNum", TaXiang.TaXiangNum);
        //    //    p[6] = new SqlParameter("@ArchNum", TaXiang.ARCHNUM);
        //    //    int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        //    //}
        //    //catch (Exception ee)
        //    //{
        //    //    MessageBox.Show(ee.ToString());
        //    //}

        //}

        public static DataTable GetArchContent(int ArchID)
        {
            DataTable dt;
            try {
                string strSql = "select * from V_getML where archid=@arch1 and xh=1 order by xh,FROMPAGE";
                SqlParameter p1 = new SqlParameter("@arch1", ArchID);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static string GetArchywr(int ArchID)
        {
            string str = "";
            try {
                string strSql = "select 权利人名称 from V_getQlr where archid=@arid and 区分当前业务权利人与义务人=1";
                SqlParameter p1 = new SqlParameter("@arid", ArchID);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
                if (dt.Rows.Count > 0) {
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        if (i > 0) {
                            str += "、" + dt.Rows[i][0].ToString();
                        }
                        else {
                            str = dt.Rows[i][0].ToString();
                        }
                    }
                }
                // str = SQLHelper.ExecScalar(strSql, p1).ToString() ;
                return str;
            } catch {
                return str;
            }
        }

        public static int GetArchLpbbz(int archid)
        {
            int str = 0;
            try {
                string strSql = "select  备注1 from T_ChangTuFc where Archid = @arid";
                SqlParameter p1 = new SqlParameter("@arid", archid);
                string vobj = SQLHelper.ExecScalar(strSql, p1).ToString();
                if (string.IsNullOrEmpty(vobj))
                    str = 0;
                else
                    str = Convert.ToInt32(vobj.ToString());
                return str;
            } catch {
                return str;
            }
        }

        public static int GetArchywrCount(int ArchID)
        {
            int str = 0;
            try {
                string strSql = "select count(*) from V_getQlr where archid=@arid and 区分当前业务权利人与义务人=1";
                SqlParameter p1 = new SqlParameter("@arid", ArchID);
                str = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1).ToString());
                return str;
            } catch {
                return str;
            }
        }

        public static string GetArchqlr(int ArchID)
        {
            string str = "";
            try {
                string strSql = "select 权利人名称 from V_getQlr where archid=@arid and 区分当前业务权利人与义务人=0";
                SqlParameter p1 = new SqlParameter("@arid", ArchID);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
                if (dt.Rows.Count > 0) {
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        if (i > 0) {
                            str += "、" + dt.Rows[i][0].ToString();
                        }
                        else {
                            str = dt.Rows[i][0].ToString();
                        }
                    }
                }
                // str = SQLHelper.ExecScalar(strSql, p1).ToString();
                return str;
            } catch {
                return str;
            }
        }


        public static string GetArchqlrZj(int ArchID)
        {
            string str = "";
            try {
                string strSql = "select 证件号码 from V_getQlr where archid=@arid and 区分当前业务权利人与义务人=0";
                SqlParameter p1 = new SqlParameter("@arid", ArchID);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
                if (dt.Rows.Count > 0) {
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        if (i > 0) {
                            str += "、" + dt.Rows[i][0].ToString();
                        }
                        else {
                            str = dt.Rows[i][0].ToString();
                        }
                    }
                }
                // str = SQLHelper.ExecScalar(strSql, p1).ToString();
                return str;
            } catch {
                return str;
            }
        }

        public static DataTable GetArchFm(int arid)
        {
            DataTable dt;
            try {
                string strSql = "select * from v_getfm where archid=@arch1 and xh=1 order by xh ";
                SqlParameter p1 = new SqlParameter("@arch1", arid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static DataTable GetArchFd(int arid)
        {
            DataTable dt;
            try {
                string strSql = "select * from v_getfd where archid=@arch1";
                SqlParameter p1 = new SqlParameter("@arch1", arid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static void UpdateLjr(int arid, string str)
        {

            try {
                string strSql = " select count(*) from M_IMAGEFILE where boxsn=@archid and ZongSn is not null ";
                SqlParameter p1 = new SqlParameter("@archid", arid);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                if (i <= 0) {

                    strSql = "update M_IMAGEFILE set ZongSn=@ljr where boxsn=@boxsn";
                    p1 = new SqlParameter("@boxsn", arid);
                    SqlParameter p2 = new SqlParameter("@ljr", str);
                    SQLHelper.ExecScalar(strSql, p1, p2);
                }
            } catch {
                return;
            }
        }

        public static void ClearOneArch(int ArchID)
        {
            try {
                string strSql = "PClearOneArch";
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", ArchID);

                int i = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);

                //string strRw = "delete from T_UpRw where archid=@archid";
                //SqlParameter p1 = new SqlParameter("@archid", ArchID);
                //int r = (Int32)SQLHelper.ExecScalar(strRw, p1);

            } catch { }

        }

        public static int ClearScan(int ArchID)
        {
            try {
                string strSql = "update M_IMAGEFILE set IMGFILE=null ,INDEXSTAFF=null,ScanStaff=null,IndexTime=null,ScanTime=null,PageIndexInfo=null,DeleTag=null,dotime=null,archstate=0 where ID=@id";
                SqlParameter p1 = new SqlParameter("@id", ArchID);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }

        }

        public static int ClearScanWrok(int ArchID)
        {
            try {
                // string strSql = " update T_ScanWork set ARCHPAGES=null,SCANSTAFF=null,SMALLSCANPAGES=null,ScanTime=null,DoTime=null where ARCHID=@ArchID";
                string strSql = " delete from T_ScanWork where ARCHID=@ArchID";
                SqlParameter p1 = new SqlParameter("@ArchID", ArchID);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return i;
            } catch {
                return 0;
            }

        }





        #endregion

        #region 朝阳国土数据信息

        public static int Updatesjzj(string str, int arid)
        {
            try {
                if (str.IndexOf("抵押") < 0 && str.IndexOf("房屋") < 0 && str.IndexOf("林权") < 0 && str.IndexOf("土地") < 0 && str.IndexOf("查封") < 0 && str.IndexOf("预告") < 0) {
                    return 0;
                }
                string strSql = "update M_IMAGEFILE set AnJxxstaff=@id where ID=@arid";
                SqlParameter p1 = new SqlParameter("@arid", arid);
                SqlParameter p2 = new SqlParameter("@id", T_User.UserId);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2));
                return i;
            } catch {
                return 0;
            }
        }
        public static int AddQLr2(List<string> str, int archid, int ts)
        {
            try {
                string strSql = " select state from T_GQLR where Archid=@archid";
                SqlParameter p1 = new SqlParameter("@archid", archid);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                if (i <= 0) {
                    MessageBox.Show("请先保存权利人1后再增加权利人2-4！");
                    return 0;
                }
                strSql = "PAddQLr2";
                SqlParameter[] p = new SqlParameter[11];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@QLr", str[0].ToString());
                p[3] = new SqlParameter("@QLrZj", str[1].ToString());
                p[4] = new SqlParameter("@QLrnum", str[2].ToString());
                p[5] = new SqlParameter("@QLrLeix", str[3].ToString());
                p[6] = new SqlParameter("@QLrXingz", str[4].ToString());
                p[7] = new SqlParameter("@QLrbL", str[5].ToString());
                p[8] = new SqlParameter("@QLrChiZ", str[6].ToString());
                p[9] = new SqlParameter("@State", str[7].ToString());
                p[10] = new SqlParameter("@QlrTs", ts);
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                if (s > 0) {
                    return 1;
                }
                return 0;
            } catch {
                return 0;
            }
        }

        public static int AddQLrandDLr(List<string> str, int archid, int ts)
        {
            try {
                string strSql = "PAddQLrandDlr";
                SqlParameter[] p = new SqlParameter[19];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@QLr", str[0].ToString());
                p[3] = new SqlParameter("@QLrZj", str[1].ToString());
                p[4] = new SqlParameter("@QLrnum", str[2].ToString());
                p[5] = new SqlParameter("@QLrLeix", str[3].ToString());
                p[6] = new SqlParameter("@QLrXingz", str[4].ToString());
                p[7] = new SqlParameter("@QLrbL", str[5].ToString());
                p[8] = new SqlParameter("@QLrChiZ", str[6].ToString());
                p[9] = new SqlParameter("@DLr", str[7].ToString());
                p[10] = new SqlParameter("@DlrNum", str[8].ToString());
                p[11] = new SqlParameter("@DlrZj", str[9].ToString());
                p[12] = new SqlParameter("@DlrDianh", str[10].ToString());
                p[13] = new SqlParameter("@Fr", str[11].ToString());
                p[14] = new SqlParameter("@Frzj", str[12].ToString());
                p[15] = new SqlParameter("@FrNum", str[13].ToString());
                p[16] = new SqlParameter("@Frdianh", str[14].ToString());
                p[17] = new SqlParameter("@zj", str[15].ToString());
                p[18] = new SqlParameter("@QlrTs", ts);
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        public static DataTable GetQLrxx(int archid)
        {
            DataTable dt;
            try {
                string strSql = " select * from T_GQLR where Archid=@id";
                SqlParameter p1 = new SqlParameter("@id", archid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static DataTable GetQLrxx(string archid, string xx)
        {
            DataTable dt;
            try {
                string strSql = "";
                SqlParameter p1 = null;
                if (xx == "土地信息") {
                    strSql = " select * from T_Tdxx_tmp where 主键=@id";
                    p1 = new SqlParameter("@id", archid);
                }
                else if (xx == "房屋信息") {
                    strSql = " select * from T_Fwxx_tmp where 主键=@id";
                    p1 = new SqlParameter("@id", archid);
                }
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        //public static DataTable GetAjxx(int id,int archid)
        //{
        //    DataTable dt;
        //    string strSql = "";          
        //    try
        //    {
        //        switch(id)
        //        {                       
        //            case 1:
        //                strSql = " select * from T_GDyxx where Archid=@archid";
        //                break;
        //            case 2:                     
        //                strSql = " select * from T_GFwxx where Archid=@archid";                      
        //                break;
        //            case 3:
        //                strSql = " select * from T_GTdxx where Archid=@archid";
        //                break;
        //            case 4:
        //                strSql = " select * from T_GLqxx where Archid=@archid";
        //                break;
        //            case 5:
        //                strSql = " select * from T_Gcfxx where Archid=@archid";
        //                break;
        //            case 6:
        //                strSql = " select * from T_Gygxx where Archid=@archid";
        //                break;                 
        //        }
        //        SqlParameter p1 = new SqlParameter("@archid", archid);
        //        dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
        //        return dt;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public static DataTable Getajxx(int archid)
        {
            try {
                DataTable dt;
                string strSql = "select * from v_getsjjc where archid=@archid and xh=1";
                SqlParameter p1 = new SqlParameter("@archid", archid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }
        public static DataTable GetAjxx(int id, string archid)
        {
            DataTable dt;
            string strSql = "";
            try {
                switch (id) {
                    case 1:
                        // strSql = " select * from T_GDyxx where Archid=@archid";
                        break;
                    case 2:
                        strSql = " select * from T_Fwxx_tmp where 主键=@archid";
                        break;
                    case 3:
                        strSql = " select * from T_Tdxx_tmp where 主键=@archid";
                        break;
                    case 4:
                        // strSql = " select * from T_GLqxx where Archid=@archid";
                        break;
                    case 5:
                        // strSql = " select * from T_Gcfxx where Archid=@archid";
                        break;
                    case 6:
                        // strSql = " select * from T_Gygxx where Archid=@archid";
                        break;
                }
                SqlParameter p1 = new SqlParameter("@archid", archid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        //public static int DelAjxx(int id, int archid)
        //{           
        //    string strSql = "";
        //    try
        //    {
        //        switch (id)
        //        {
        //            case 0:
        //                strSql = " delete  from T_GDyxx where Archid=@archid";
        //                break;
        //            case 1:
        //                strSql = " delete from T_GFwxx where Archid=@archid";
        //                break;
        //            case 2:
        //                strSql = " delete from T_GTdxx where Archid=@archid";
        //                break;
        //            case 3:
        //                strSql = " delete from T_GLqxx where Archid=@archid";
        //                break;
        //            case 4:
        //                strSql = " delete from T_Gcfxx where Archid=@archid";
        //                break;
        //            case 5:
        //                strSql = " delete from T_Gygxx where Archid=@archid";
        //                break;
        //        }
        //        SqlParameter p1 = new SqlParameter("@archid", archid);
        //        DAL.SQLHelper.ExecScalar(strSql, p1);
        //        return DelQlr(archid);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        private static int DelQlr(int archid)
        {
            try {
                string strSql = "delete from T_Gqlr where archid=@arid";
                SqlParameter p1 = new SqlParameter("@arid", archid);
                DAL.SQLHelper.ExecScalar(strSql, p1);
                return DelQlrJl(archid);
            } catch {
                return 0;
            }
        }

        private static int DelQlrJl(int archid)
        {
            try {
                string strSql = "delete from T_CHANQUAN where archid=@arid";
                SqlParameter p1 = new SqlParameter("@arid", archid);
                DAL.SQLHelper.ExecScalar(strSql, p1);
                return 1;
            } catch {
                return 0;
            }
        }

        public static int AddDyxx(int archid, Dictionary<int, string> xx, int ts)
        {
            try {
                string strSql = "PAddDyxx";
                SqlParameter[] p = new SqlParameter[34];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@Ts", ts);
                p[3] = new SqlParameter("@档案号", xx[1].ToString().TrimStart('0'));
                p[4] = new SqlParameter("@盒号", xx[2].ToString().TrimStart('0'));
                p[5] = new SqlParameter("@抵押开始时间", xx[3].ToString());
                p[6] = new SqlParameter("@抵押结束日期", xx[4].ToString());
                p[7] = new SqlParameter("@抵押方式", xx[5].ToString());
                p[8] = new SqlParameter("@在建建筑物坐落", xx[6].ToString());
                p[9] = new SqlParameter("@在建建筑物抵押范围", xx[7].ToString());
                p[10] = new SqlParameter("@是否注销", xx[8].ToString());
                p[11] = new SqlParameter("@被担保主债权数额", xx[9].ToString());
                p[12] = new SqlParameter("@登记类型", xx[10].ToString());
                p[13] = new SqlParameter("@不动产类别", xx[11].ToString());
                p[14] = new SqlParameter("@附记", xx[12].ToString());
                p[15] = new SqlParameter("@抵押证号", xx[13].ToString());
                p[16] = new SqlParameter("@原抵押凭证号", xx[14].ToString());
                p[17] = new SqlParameter("@受理编号", xx[15].ToString());
                p[18] = new SqlParameter("@登记原因", xx[16].ToString());
                p[19] = new SqlParameter("@还款期限", xx[17].ToString());
                p[20] = new SqlParameter("@产权来源", xx[18].ToString());
                p[21] = new SqlParameter("@原不动产证书", xx[19].ToString());
                p[22] = new SqlParameter("@原房产证号", xx[20].ToString());
                p[23] = new SqlParameter("@原土地证号", xx[21].ToString());
                p[24] = new SqlParameter("@注销抵押业务号", xx[22].ToString());
                p[25] = new SqlParameter("@注销抵押原因", xx[23].ToString());
                p[26] = new SqlParameter("@注销登簿人", xx[24].ToString());
                p[27] = new SqlParameter("@注销登簿时间", xx[25].ToString());
                p[28] = new SqlParameter("@抵押面积", xx[26].ToString());
                p[29] = new SqlParameter("@备注", xx[27].ToString());
                p[30] = new SqlParameter("@抵押人名称", xx[28].ToString());
                p[31] = new SqlParameter("@登簿人", xx[29].ToString());
                p[32] = new SqlParameter("@抵押登记时间", xx[30].ToString());
                p[33] = new SqlParameter("@抵押顺序", xx[31].ToString());
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }

        //public static int AddFwxx(int archid, Dictionary<int, string> xx, int ts)
        //{
        //    try
        //    {
        //        string strSql = "PAddFwxx";
        //        SqlParameter[] p = new SqlParameter[45];
        //        p[0] = new SqlParameter("@UserID", T_User.UserId);
        //        p[1] = new SqlParameter("@ArchID", archid);
        //        p[2] = new SqlParameter("@Ts", ts);
        //        p[3] = new SqlParameter("@档案号", xx[1].ToString().TrimStart('0'));
        //        p[4] = new SqlParameter("@盒号", xx[2].ToString().TrimStart('0'));
        //        p[5] = new SqlParameter("@规划用途", xx[3].ToString());
        //        p[6] = new SqlParameter("@房屋性质", xx[4].ToString());
        //        p[7] = new SqlParameter("@房屋结构", xx[5].ToString());
        //        p[8] = new SqlParameter("@所在层", xx[6].ToString().TrimStart('0'));
        //        p[9] = new SqlParameter("@总层数", xx[7].ToString().TrimStart('0'));
        //        p[10] = new SqlParameter("@竣工时间", xx[8].ToString());
        //        p[11] = new SqlParameter("@产权来源", xx[9].ToString());
        //        p[12] = new SqlParameter("@房屋坐落", xx[10].ToString());
        //        p[13] = new SqlParameter("@房屋类型", xx[11].ToString());
        //        p[14] = new SqlParameter("@共有情况", xx[12].ToString());
        //        p[15] = new SqlParameter("@交易价格", xx[13].ToString());
        //        p[16] = new SqlParameter("@专有建筑面积", xx[14].ToString());
        //        p[17] = new SqlParameter("@建筑面积", xx[15].ToString());
        //        p[18] = new SqlParameter("@受理编号", xx[16].ToString());
        //        p[19] = new SqlParameter("@分摊建筑面积", xx[17].ToString());
        //        p[20] = new SqlParameter("@总套数", xx[18].ToString());
        //        p[21] = new SqlParameter("@权利类型", xx[19].ToString());
        //        p[22] = new SqlParameter("@权利性质", xx[20].ToString());
        //        p[23] = new SqlParameter("@是否注销", xx[21].ToString());
        //        p[24] = new SqlParameter("@登记类型", xx[22].ToString());
        //        p[25] = new SqlParameter("@面积单位", xx[23].ToString());
        //        p[26] = new SqlParameter("@登记簿状态", xx[24].ToString());
        //        p[27] = new SqlParameter("@登记原因", xx[25].ToString());
        //        p[28] = new SqlParameter("@不动产类型", xx[26].ToString());
        //        p[29] = new SqlParameter("@附记", xx[27].ToString());
        //        p[30] = new SqlParameter("@不动产证号", xx[28].ToString());
        //        p[31] = new SqlParameter("@原不动产证号", xx[29].ToString());
        //        p[32] = new SqlParameter("@原房产证号", xx[30].ToString());
        //        p[33] = new SqlParameter("@原土地证号", xx[31].ToString());
        //        p[34] = new SqlParameter("@原产权人", xx[32].ToString());
        //        p[35] = new SqlParameter("@独用土地面积", xx[33].ToString());
        //        p[36] = new SqlParameter("@分摊土地面积", xx[34].ToString());
        //        p[37] = new SqlParameter("@土地使用权面积", xx[35].ToString());
        //        p[38] = new SqlParameter("@土地权属性质", xx[36].ToString());
        //        p[39] = new SqlParameter("@土地用途", xx[37].ToString());
        //        p[40] = new SqlParameter("@土地使用开始日期", xx[38].ToString());
        //        p[41] = new SqlParameter("@土地使用结束日期", xx[39].ToString());
        //        p[42] = new SqlParameter("@土地使用权类型", xx[40].ToString());
        //        p[43] = new SqlParameter("@登记时间", xx[41].ToString());
        //        p[44] = new SqlParameter("@登簿人", xx[42].ToString());
        //        int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        //        return s;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        public static int AddTdxx(int archid, Dictionary<int, string> xx, int ts)
        {
            try {
                string strSql = "PAddTdxx";
                SqlParameter[] p = new SqlParameter[52];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@Ts", ts);
                p[3] = new SqlParameter("@地籍号", xx[1].ToString());
                p[4] = new SqlParameter("@土地坐落", xx[2].ToString());
                p[5] = new SqlParameter("@宗地面积", xx[3].ToString());
                p[6] = new SqlParameter("@用途", xx[4].ToString());
                p[7] = new SqlParameter("@权属性质", xx[5].ToString());
                p[8] = new SqlParameter("@使用权类型", xx[6].ToString());
                p[9] = new SqlParameter("@权利设定方式", xx[7].ToString());
                p[10] = new SqlParameter("@容积率", xx[8].ToString());
                p[11] = new SqlParameter("@建筑密度", xx[9].ToString());
                p[12] = new SqlParameter("@建筑限高", xx[10].ToString());
                p[13] = new SqlParameter("@东至", xx[11].ToString());
                p[14] = new SqlParameter("@南至", xx[12].ToString());
                p[15] = new SqlParameter("@西至", xx[13].ToString());
                p[16] = new SqlParameter("@北至", xx[14].ToString());
                p[17] = new SqlParameter("@共有情况", xx[15].ToString());
                p[18] = new SqlParameter("@取得价格", xx[16].ToString());
                p[19] = new SqlParameter("@土地价格", xx[17].ToString());
                p[20] = new SqlParameter("@单位", xx[18].ToString());
                p[21] = new SqlParameter("@老地籍号", xx[19].ToString());
                p[22] = new SqlParameter("@起始日期1", xx[20].ToString());
                p[23] = new SqlParameter("@终止日期1", xx[21].ToString());
                p[24] = new SqlParameter("@起始日期2", xx[22].ToString());
                p[25] = new SqlParameter("@终止日期2", xx[23].ToString());
                p[26] = new SqlParameter("@等级", xx[24].ToString());
                p[27] = new SqlParameter("@不动产类别", xx[25].ToString());
                p[28] = new SqlParameter("@宗地类别", xx[26].ToString());
                p[29] = new SqlParameter("@用途1", xx[27].ToString());
                p[30] = new SqlParameter("@用途2", xx[28].ToString());
                p[31] = new SqlParameter("@用途3", xx[29].ToString());
                p[32] = new SqlParameter("@备注", xx[30].ToString());
                p[33] = new SqlParameter("@档案号", xx[31].ToString().TrimStart('0'));
                p[34] = new SqlParameter("@是否注销", xx[32].ToString());
                p[35] = new SqlParameter("@登记类型", xx[33].ToString());
                p[36] = new SqlParameter("@登记原因", xx[34].ToString());
                p[37] = new SqlParameter("@受理编号", xx[35].ToString());
                p[38] = new SqlParameter("@发证日期", xx[36].ToString());
                p[39] = new SqlParameter("@不动产证号", xx[37].ToString());
                p[40] = new SqlParameter("@原不动产证号", xx[38].ToString());
                p[41] = new SqlParameter("@原土地证号", xx[39].ToString());
                p[42] = new SqlParameter("@使用权面积", xx[40].ToString());
                p[43] = new SqlParameter("@登记时间", xx[41].ToString());
                p[44] = new SqlParameter("@登簿人", xx[42].ToString());
                p[45] = new SqlParameter("@注销原因", xx[43].ToString());
                p[46] = new SqlParameter("@注销登簿人", xx[44].ToString());
                p[47] = new SqlParameter("@注销登簿时间", xx[45].ToString());
                p[48] = new SqlParameter("@权利类型", xx[46].ToString());
                p[49] = new SqlParameter("@电子监管号", xx[47].ToString());
                p[50] = new SqlParameter("@批准文号", xx[48].ToString());
                p[51] = new SqlParameter("@盒号", xx[49].ToString().TrimStart('0'));
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }

        public static int AddLqxx(int archid, Dictionary<int, string> xx, int ts)
        {
            try {
                string strSql = "PAddLqxx";
                SqlParameter[] p = new SqlParameter[45];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@Ts", ts);
                p[3] = new SqlParameter("@档案号", xx[1].ToString().TrimStart('0'));
                p[4] = new SqlParameter("@受理编号", xx[2].ToString());
                p[5] = new SqlParameter("@林地共有情况", xx[3].ToString());
                p[6] = new SqlParameter("@使用权承包面积", xx[4].ToString());
                p[7] = new SqlParameter("@林地使用开始日期", xx[5].ToString());
                p[8] = new SqlParameter("@林地使用结束日期", xx[6].ToString());
                p[9] = new SqlParameter("@林地所有权性质", xx[7].ToString());
                p[10] = new SqlParameter("@主要树种", xx[8].ToString());
                p[11] = new SqlParameter("@株数", xx[9].ToString());
                p[12] = new SqlParameter("@林种", xx[10].ToString());
                p[13] = new SqlParameter("@造林年度", xx[11].ToString());
                p[14] = new SqlParameter("@小地名", xx[12].ToString());
                p[15] = new SqlParameter("@林班", xx[13].ToString());
                p[16] = new SqlParameter("@小班", xx[14].ToString());
                p[17] = new SqlParameter("@起源", xx[15].ToString());
                p[18] = new SqlParameter("@林权坐落", xx[16].ToString());
                p[19] = new SqlParameter("@单位", xx[17].ToString());
                p[20] = new SqlParameter("@是否注销", xx[18].ToString());
                p[21] = new SqlParameter("@林木使用权人", xx[19].ToString());
                p[22] = new SqlParameter("@林木所有权人", xx[20].ToString());
                p[23] = new SqlParameter("@不动产类别", xx[21].ToString());
                p[24] = new SqlParameter("@发包方名称", xx[22].ToString());
                p[25] = new SqlParameter("@权利人名称", xx[23].ToString());
                p[26] = new SqlParameter("@林权证号", xx[24].ToString());
                p[27] = new SqlParameter("@原林权证号", xx[25].ToString());
                p[28] = new SqlParameter("@地籍号", xx[26].ToString());
                p[29] = new SqlParameter("@原权证号", xx[27].ToString());
                p[30] = new SqlParameter("@登记时间", xx[28].ToString());
                p[31] = new SqlParameter("@登记原因", xx[29].ToString());
                p[32] = new SqlParameter("@登簿人", xx[30].ToString());
                p[33] = new SqlParameter("@被执行人", xx[31].ToString());
                p[34] = new SqlParameter("@查封时限", xx[32].ToString());
                p[35] = new SqlParameter("@轮候查封期限", xx[33].ToString());
                p[36] = new SqlParameter("@档案业务号", xx[34].ToString());
                p[37] = new SqlParameter("@注销登簿时间", xx[35].ToString());
                p[38] = new SqlParameter("@注销原因", xx[36].ToString());
                p[39] = new SqlParameter("@注销登簿人", xx[37].ToString());
                p[40] = new SqlParameter("@盒号", xx[38].ToString().TrimStart('0'));
                p[41] = new SqlParameter("@东至", xx[39].ToString().TrimStart('0'));
                p[42] = new SqlParameter("@南至", xx[40].ToString().TrimStart('0'));
                p[43] = new SqlParameter("@西至", xx[41].ToString().TrimStart('0'));
                p[44] = new SqlParameter("@北至", xx[42].ToString().TrimStart('0'));
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }

        public static int AddCfxx(int archid, Dictionary<int, string> xx, int ts)
        {
            try {
                string strSql = "PAddCfxx";
                SqlParameter[] p = new SqlParameter[33];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@Ts", ts);
                p[3] = new SqlParameter("@档案号", xx[1].ToString().TrimStart('0'));
                p[4] = new SqlParameter("@登记原因", xx[2].ToString());
                p[5] = new SqlParameter("@查封文号", xx[3].ToString());
                p[6] = new SqlParameter("@查封类型", xx[4].ToString());
                p[7] = new SqlParameter("@查封机关", xx[5].ToString());
                p[8] = new SqlParameter("@查封范围", xx[6].ToString());
                p[9] = new SqlParameter("@查封文件", xx[7].ToString());
                p[10] = new SqlParameter("@查封期限起", xx[8].ToString());
                p[11] = new SqlParameter("@查封期限止", xx[9].ToString());
                p[12] = new SqlParameter("@查封登记时间", xx[10].ToString());
                p[13] = new SqlParameter("@权利性质", xx[11].ToString());
                p[14] = new SqlParameter("@登记类型", xx[12].ToString());
                p[15] = new SqlParameter("@登记小类", xx[13].ToString());
                p[16] = new SqlParameter("@不动产类别", xx[14].ToString());
                p[17] = new SqlParameter("@登记薄状态", xx[15].ToString());
                p[18] = new SqlParameter("@坐落", xx[16].ToString());
                p[19] = new SqlParameter("@不动产类型", xx[17].ToString());
                p[20] = new SqlParameter("@面积单位", xx[18].ToString());
                p[21] = new SqlParameter("@不动产单元号", xx[19].ToString());
                p[22] = new SqlParameter("@登记原因1", xx[20].ToString());
                p[23] = new SqlParameter("@解封文号", xx[21].ToString());
                p[24] = new SqlParameter("@解封机关", xx[22].ToString());
                p[25] = new SqlParameter("@解封登记时间", xx[23].ToString());
                p[26] = new SqlParameter("@查封登簿人", xx[24].ToString());
                p[27] = new SqlParameter("@解封登簿人", xx[25].ToString());
                p[28] = new SqlParameter("@备注", xx[26].ToString());
                p[29] = new SqlParameter("@盒号", xx[27].ToString().TrimStart('0'));
                p[30] = new SqlParameter("@原不动产证号", xx[28].ToString());
                p[31] = new SqlParameter("@原房产证号", xx[29].ToString());
                p[32] = new SqlParameter("@原土地证号", xx[30].ToString());
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }

        public static int AddYgxx(int archid, Dictionary<int, string> xx, int ts)
        {
            try {
                string strSql = "PAddYgxx";
                SqlParameter[] p = new SqlParameter[31];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@Ts", ts);
                p[3] = new SqlParameter("@档案号", xx[1].ToString().TrimStart('0'));
                p[4] = new SqlParameter("@取得价格", xx[2].ToString());
                p[5] = new SqlParameter("@是否注销", xx[3].ToString());
                p[6] = new SqlParameter("@不动产类型", xx[4].ToString());
                p[7] = new SqlParameter("@预告登记种类", xx[5].ToString());
                p[8] = new SqlParameter("@登记类型", xx[6].ToString());
                p[9] = new SqlParameter("@附记", xx[7].ToString());
                p[10] = new SqlParameter("@不动产证号", xx[8].ToString());
                p[11] = new SqlParameter("@受理编号", xx[9].ToString());
                p[12] = new SqlParameter("@原产籍号", xx[10].ToString());
                p[13] = new SqlParameter("@产权来源", xx[11].ToString());
                p[14] = new SqlParameter("@预告登记证明号", xx[12].ToString());
                p[15] = new SqlParameter("@登记原因", xx[13].ToString());
                p[16] = new SqlParameter("@原权证号", xx[14].ToString());
                p[17] = new SqlParameter("@抵押开始时间", xx[15].ToString());
                p[18] = new SqlParameter("@抵押结束时间", xx[16].ToString());
                p[19] = new SqlParameter("@抵押方式", xx[17].ToString());
                p[20] = new SqlParameter("@交易合同号", xx[18].ToString());
                p[21] = new SqlParameter("@交易金额", xx[19].ToString());
                p[22] = new SqlParameter("@备注", xx[20].ToString());
                p[23] = new SqlParameter("@登记时间", xx[21].ToString());
                p[24] = new SqlParameter("@登簿人", xx[22].ToString());
                p[25] = new SqlParameter("@档案业务号", xx[23].ToString());
                p[26] = new SqlParameter("@注销原因", xx[24].ToString());
                p[27] = new SqlParameter("@注销登簿人", xx[25].ToString());
                p[28] = new SqlParameter("@注销登簿时间", xx[26].ToString());
                p[29] = new SqlParameter("@担保范围", xx[27].ToString());
                p[30] = new SqlParameter("@盒号", xx[28].ToString().TrimStart('0'));
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }
        #endregion


        #region 昌图不动产
        public static int AddFwxx(int archid, Dictionary<int, string> xx, int ts, int enter)
        {
            try {
                string strSql = string.Empty;
                if (enter == 1) {
                    strSql = "PAddChanQuan";
                }
                else if (enter == 2) {
                    strSql = "PAddChanQuan2";
                }
                else {
                    return 0;
                }
                SqlParameter[] p = new SqlParameter[61];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@Ts", ts);
                p[3] = new SqlParameter("@所属区域", xx[1].ToString());
                p[4] = new SqlParameter("@不动产证号", xx[2].ToString());
                p[5] = new SqlParameter("@总页数", xx[3].ToString());
                p[6] = new SqlParameter("@共有情况", xx[4].ToString());
                p[7] = new SqlParameter("@房地坐落", xx[5].ToString());
                p[8] = new SqlParameter("@权利类型", xx[6].ToString());
                p[9] = new SqlParameter("@权利性质", xx[7].ToString());
                p[10] = new SqlParameter("@登记类型", xx[8].ToString());
                p[11] = new SqlParameter("@登记小类", xx[9].ToString());
                p[12] = new SqlParameter("@登记薄状态", xx[10].ToString());
                p[13] = new SqlParameter("@面积单位", xx[11].ToString());
                p[14] = new SqlParameter("@不动产类型", xx[12].ToString());
                p[15] = new SqlParameter("@不动产单元号", xx[13].ToString());
                p[16] = new SqlParameter("@土地使用权人", xx[14].ToString());
                p[17] = new SqlParameter("@登记日期", xx[15].ToString());
                p[18] = new SqlParameter("@登簿人", xx[16].ToString());
                p[19] = new SqlParameter("@规划用途", xx[17].ToString());
                p[20] = new SqlParameter("@房屋性质", xx[18].ToString());
                p[21] = new SqlParameter("@房屋结构", xx[19].ToString());
                p[22] = new SqlParameter("@总层数", xx[20].ToString());
                p[23] = new SqlParameter("@所在层数", xx[21].ToString());
                p[24] = new SqlParameter("@建筑面积", xx[22].ToString());
                p[25] = new SqlParameter("@分摊土地面积", xx[23].ToString());
                p[26] = new SqlParameter("@独用土地面积", xx[24].ToString());
                p[27] = new SqlParameter("@土地登簿人", xx[25].ToString());
                p[28] = new SqlParameter("@登记原因", xx[26].ToString());
                p[29] = new SqlParameter("@土地登簿时间", xx[27].ToString());
                p[30] = new SqlParameter("@土地权证号", xx[28].ToString());
                p[31] = new SqlParameter("@土地使用期限起", xx[29].ToString());
                p[32] = new SqlParameter("@土地使用期限止", xx[30].ToString());
                p[33] = new SqlParameter("@产权来源", xx[31].ToString());
                p[34] = new SqlParameter("@备注1", xx[32].ToString());
                p[35] = new SqlParameter("@开发企业名称", xx[33].ToString());
                p[36] = new SqlParameter("@开工日期", xx[34].ToString());
                p[37] = new SqlParameter("@竣工日期", xx[35].ToString());
                p[38] = new SqlParameter("@行政代码", xx[36].ToString());
                p[39] = new SqlParameter("@开发企业组织机构代码", xx[37].ToString());
                p[40] = new SqlParameter("@建设工程许可证", xx[38].ToString());
                p[41] = new SqlParameter("@建设工程用地规划许可证", xx[39].ToString());
                p[42] = new SqlParameter("@自然幢", xx[40].ToString());
                p[43] = new SqlParameter("@地上层数", xx[41].ToString());
                p[44] = new SqlParameter("@地下层数", xx[42].ToString());
                p[45] = new SqlParameter("@逻辑幢名称", xx[43].ToString());
                p[46] = new SqlParameter("@建筑物状态", xx[44].ToString());
                p[47] = new SqlParameter("@总套数", xx[45].ToString());
                p[48] = new SqlParameter("@预测建筑面积", xx[46].ToString());
                p[49] = new SqlParameter("@实测建筑面积", xx[47].ToString());
                p[50] = new SqlParameter("@预测建筑面积1", xx[48].ToString());
                p[51] = new SqlParameter("@实测建筑面积1", xx[49].ToString());
                p[52] = new SqlParameter("@自然幢顺序号", xx[50].ToString());
                p[53] = new SqlParameter("@逻辑幢号", xx[51].ToString());
                p[54] = new SqlParameter("@实际层", xx[52].ToString());
                p[55] = new SqlParameter("@名义层", xx[53].ToString());
                p[56] = new SqlParameter("@终止楼层", xx[54].ToString());
                p[57] = new SqlParameter("@门牌号", xx[55].ToString());
                p[58] = new SqlParameter("@户型", xx[56].ToString());
                p[59] = new SqlParameter("@户型结构", xx[57].ToString());
                p[60] = new SqlParameter("@土地用途", xx[58].ToString());
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }

        public static int Getqlrzt(int archid, int Enter)
        {
            string strSql = string.Empty;
            if (Enter == 1) {
                strSql = "select count(*) from T_GQLR where Archid=@id and xh=1";
            }
            else if (Enter == 2) {
                strSql = "select count(*) from T_GQLR2 where Archid=@id and xh=1";
            }
            else {
                return 0;
            }
            SqlParameter p1 = new SqlParameter("@id", archid);
            int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
            return i;
        }

        public static int CTupdatexy(int archid, int Enter, int fwql, string xh, string col, string str)
        {
            string strSql = string.Empty;
            SqlParameter p1 = null;
            SqlParameter p2 = null;
            SqlParameter p3 = null;
            int i = -1;
            try {
                if (Enter == 1) {
                    if (fwql == 1) {
                        strSql = "update T_ChangTuFc set " + col + "=@str where archid=@archid";
                        p1 = new SqlParameter("@archid", archid);
                        p2 = new SqlParameter("@str", str);
                        i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2));
                    }
                    else if (fwql == 2) {
                        strSql = "update T_GQLR set " + col + "=@str where archid=@archid and xh=@xh";
                        p1 = new SqlParameter("@archid", archid);
                        p2 = new SqlParameter("@xh", xh);
                        p3 = new SqlParameter("@str", str);
                        i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2, p3));
                        if (col == "权利人名称" && xh == "1") {
                            strSql = "update M_IMAGEFILE set DaLeix=@str where id=@archid";
                            p1 = new SqlParameter("@archid", archid);
                            p2 = new SqlParameter("@str", str);
                            i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2));
                        }
                    }
                }
                else if (Enter == 2) {
                    if (fwql == 1) {
                        strSql = "update T_ChangTuFc2 set " + col + "=@str where archid=@archid";
                        p1 = new SqlParameter("@archid", archid);
                        p2 = new SqlParameter("@str", str);
                        i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2));
                    }
                    else if (fwql == 2) {
                        strSql = "update T_GQLR2 set " + col + "=@str where archid=@archid and xh=@xh";
                        p1 = new SqlParameter("@archid", archid);
                        p2 = new SqlParameter("@xh", xh);
                        p3 = new SqlParameter("@str", str);
                        i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2, p3));
                        if (col == "权利人名称" && xh == "1") {
                            strSql = "update M_IMAGEFILE set DaLeix=@str where id=@archid";
                            p1 = new SqlParameter("@archid", archid);
                            p2 = new SqlParameter("@str", str);
                            i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2));
                        }
                    }
                }
                return i;
            } catch {
                return i;
            }
        }

        public static int Getquyu(string str)
        {
            string strSql = string.Empty;
            strSql = "select count(*) from T_Quyu where 行政区名称=@quyu";
            SqlParameter p1 = new SqlParameter("@quyu", str);
            int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
            return i;
        }

        public static int AddQlrxx(int archid, Dictionary<int, string> xx, int ts, int xh, int enter)
        {
            try {
                string strSql = string.Empty;
                if (enter == 1) {
                    strSql = "PAddQLr";
                }
                else if (enter == 2) {
                    strSql = "PAddQLr2";
                }
                else {
                    return 0;
                }
                SqlParameter[] p = new SqlParameter[15];
                p[0] = new SqlParameter("@UserID", T_User.UserId);
                p[1] = new SqlParameter("@ArchID", archid);
                p[2] = new SqlParameter("@QlrTs", ts);
                p[3] = new SqlParameter("@区分当前业务权利人与义务人", xx[1].ToString());
                p[4] = new SqlParameter("@权利人名称", xx[2].ToString());
                p[5] = new SqlParameter("@证件种类", xx[3].ToString());
                p[6] = new SqlParameter("@证件号码", xx[4].ToString());
                p[7] = new SqlParameter("@性别", xx[5].ToString());
                p[8] = new SqlParameter("@电话", xx[6].ToString());
                p[9] = new SqlParameter("@所属行业", xx[7].ToString());
                p[10] = new SqlParameter("@权利人性质", xx[8].ToString());
                p[11] = new SqlParameter("@权利比例", xx[9].ToString());
                p[12] = new SqlParameter("@共有权人单位关系", xx[10].ToString());
                p[13] = new SqlParameter("@单元ID", xx[11].ToString());
                p[14] = new SqlParameter("@xh", xh);
                int s = DAL.SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
                return s;
            } catch {
                return 0;
            }
        }


        public static DataTable GetAjxx(int id, int archid, int enter)
        {
            DataTable dt;
            string strSql = "";
            try {
                switch (id) {
                    case 1:
                        if (enter == 1) {
                            strSql = " select * from T_ChangTuFc where Archid=@archid";
                        }
                        else if (enter == 2) {
                            strSql = " select * from T_ChangTuFc2 where Archid=@archid";
                        }
                        else {
                            return null;
                        }
                        break;
                    case 2:
                        if (enter == 1) {
                            strSql = " select * from T_GLpbXM where Archid=@archid";
                        }
                        else if (enter == 2) {
                            strSql = " select * from T_GLpbXM2 where Archid=@archid";
                        }
                        else {
                            return null;
                        }
                        break;
                    case 3:
                        if (enter == 1) {
                            strSql = " select * from T_GLpbLjz where Archid=@archid";
                        }
                        else if (enter == 2) {
                            strSql = " select * from T_GLpbLjz2 where Archid=@archid";
                        }
                        else {
                            return null;
                        }
                        break;
                    case 4:
                        if (enter == 1) {
                            strSql = " select * from T_GLpbCh where Archid=@archid";
                        }
                        else if (enter == 2) {
                            strSql = " select * from T_GLpbCh2 where Archid=@archid";
                        }
                        else {
                            return null;
                        }
                        break;
                }
                SqlParameter p1 = new SqlParameter("@archid", archid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        public static int DelAjxx(int id, int archid)
        {
            string strSql = "";
            try {
                switch (id) {
                    case 0:
                        strSql = " delete  from T_GDyxx where Archid=@archid";
                        break;
                    case 1:
                        strSql = " delete from T_GFwxx where Archid=@archid";
                        break;
                    case 2:
                        strSql = " delete from T_GTdxx where Archid=@archid";
                        break;
                    case 3:
                        strSql = " delete from T_GLqxx where Archid=@archid";
                        break;
                    case 4:
                        strSql = " delete from T_Gcfxx where Archid=@archid";
                        break;
                    case 5:
                        strSql = " delete from T_Gygxx where Archid=@archid";
                        break;
                }
                SqlParameter p1 = new SqlParameter("@archid", archid);
                DAL.SQLHelper.ExecScalar(strSql, p1);
                return DelQlr(archid);
            } catch {
                return 0;
            }
        }

        public static DataTable GetQLrxx(int archid, int id, int enter)
        {
            DataTable dt;
            try {
                string strSql = string.Empty;
                if (enter == 1) {
                    strSql = " select * from T_GQLR where Archid=@id and xh=@st";
                }
                else if (enter == 2) {
                    strSql = " select * from T_GQLR2 where Archid=@id and xh=@st";
                }
                else {
                    return null;
                }
                SqlParameter p1 = new SqlParameter("@id", archid);
                SqlParameter p2 = new SqlParameter("@st", id);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1, p2);
                return dt;
            } catch {
                return null;
            }
        }

        public static DataTable GetXlsxx(int archid, int id)
        {
            DataTable dt;
            try {
                string strSql = string.Empty;
                if (id == 1) {
                    strSql = "select * from V_getDengjiBo where archid=@arid and xh=1";
                }
                else if (id == 2) {
                    strSql = "select * from V_getQlr where archid=@arid";
                }
                else if (id == 3) {
                    strSql = "select * from V_getLpbXm where archid=@arid";
                }
                else if (id == 4) {
                    strSql = "select * from V_getLpbZrz where archid=@arid";
                }
                else if (id == 5) {
                    strSql = "select * from V_getLpbC where archid=@arid";
                }
                SqlParameter p1 = new SqlParameter("@arid", archid);
                dt = DAL.SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }

        #endregion


    }
}