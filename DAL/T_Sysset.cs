using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class T_Sysset
    {

        #region userLogion

        public static void GetSfname()
        {
            T_ConFigure.SfName = DESEncrypt.DesDecrypt(ConfigurationManager.ConnectionStrings["Sfname"].ConnectionString);
        }
        public static DataTable GetUser()
        {
            DataTable dt = null;
            string strSql = "select id,UserName,UserSys,OtherSys,Usermenu from M_User  ";
            dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static DataTable GetuserSys()
        {
            string strSql = "select UserName'用户',UserPhone'电话',UserSys'模块权限',OtherSys'其他权限',DoTime'入职时间',Bz'备注',id from M_User order by id";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static bool IsCheckUser(string user, string pwd)
        {
            string strSql = "select count(*) from M_User where  UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter p1 = new SqlParameter("@UserName", user);
            string userpwd = DESEncrypt.DesEncrypt(pwd);
            SqlParameter p2 = new SqlParameter("@UserPwd", userpwd);
            bool i = Convert.ToBoolean(SQLHelper.ExecScalar(strSql, p1, p2));
            return i;
        }
        public static void UpUserPwd(string pwd)
        {
            string strSql = "update M_user set UserPwd=@UserPwd where id=@id ";
            SqlParameter p1 = new SqlParameter("@id", T_User.UserId);
            string userpwd = DESEncrypt.DesEncrypt(pwd);
            SqlParameter p2 = new SqlParameter("@UserPwd", userpwd);
            SQLHelper.ExecScalar(strSql, p1, p2);

        }
        public static bool IsCheckUser(string user)
        {
            string strSql = "select count(*) from M_User where  UserName=@UserName";
            SqlParameter p1 = new SqlParameter("@UserName", user);
            bool i = Convert.ToBoolean(SQLHelper.ExecScalar(strSql, p1));
            return i;
        }
        public static void AddUserSys(string user, string pwd, string phone, string time, string bz, string usersys, string othesys, string menu)
        {
            string strSql = "PAddUser";
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@username", user);
            p[2] = new SqlParameter("@pwd", DESEncrypt.DesEncrypt(pwd));
            p[3] = new SqlParameter("@phone", phone);
            p[4] = new SqlParameter("@time", time);
            p[5] = new SqlParameter("@bz", bz);
            p[6] = new SqlParameter("@usersys", DESEncrypt.DesEncrypt(usersys));
            p[7] = new SqlParameter("@othersys", DESEncrypt.DesEncrypt(othesys));
            p[8] = new SqlParameter("@usermenu", DESEncrypt.DesEncrypt(menu));
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static void UpUserSys(string user, string phone, string time, string bz, string usersys, string othesys, string id, string menu)
        {
            string strSql = "PUpUser";
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@username", user);
            p[2] = new SqlParameter("@phone", phone);
            p[3] = new SqlParameter("@time", time);
            p[4] = new SqlParameter("@bz", bz);
            p[5] = new SqlParameter("@usersys", DESEncrypt.DesEncrypt(usersys));
            p[6] = new SqlParameter("@othersys", DESEncrypt.DesEncrypt(othesys));
            p[7] = new SqlParameter("@id", id);
            p[8] = new SqlParameter("@usermenu", DESEncrypt.DesEncrypt(menu));
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }
        public static void Deluser(string user)
        {
            string strSql = "PDelUser";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@username", user);
            p[2] = new SqlParameter("@ip", T_ConFigure.IPAddress);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }


        #endregion

        #region AddModule
        public static DataTable IsGetModule(int id)
        {
            DataTable dt = null;
            string strSql = "";
            if (id == 0)
                strSql = "select * from V_Getmodule order by id ";
            else
                strSql = "select * from V_Getmodule where ModuleSys is null order by id ";
            dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static DataTable IsGetMenuSet()
        {
            string strStr = "select * from M_MouduleMain";
            DataTable dt = SQLHelper.ExcuteTable(strStr);
            return dt;
        }
        public static DataTable GetMenuSet()
        {
            string strStr = "select ModuleName from M_MouduleMain order by Moduleid";
            DataTable dt = SQLHelper.ExcuteTable(strStr);
            return dt;
        }
        public static void SaveMenuset(string id, string name, string xh)
        {
            string strSql = "PupdateMenuset";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@id", id);
            p[1] = new SqlParameter("@name", name);
            p[2] = new SqlParameter("@xh", xh);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            Common.Writelog(0, "修改菜单:" + name);
        }

        public static void DelMenuSet(int id, string name)
        {
            string strSql = "delete from M_MouduleMain where id=@id";
            SqlParameter p1 = new SqlParameter("@id", id);
            SQLHelper.ExecScalar(strSql, p1);
            Common.Writelog(0, "删除菜单:" + name);
        }

        public static DataTable GetModule()
        {
            DataTable dt = null;
            string strSql = "select ModuleChName,ModuleInt from M_Moudule order by id ";
            dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static DataTable GetMenuSetid()
        {
            DataTable dt = null;
            string strSql = "select ModuleName,Moduleid from M_MouduleMain ";
            dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }


        public static DataTable GetOtherModule()
        {
            DataTable dt = null;
            string strSql = "select OtherModule,id from M_OtherModuleSys order by id ";
            dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static void SaveModule()
        {
            string strSql = "select id from M_Moudule where ModuleChName=@ModuleChName";
            SqlParameter p1 = new SqlParameter("@ModuleChName", T_addModule.T_moduleChName);
            int count = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
            strSql = "PInserModule";
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@userid", T_User.UserId);
            p[1] = new SqlParameter("@ModuleName", DESEncrypt.DesEncrypt(T_addModule.T_moduleName));
            p[2] = new SqlParameter("@ModuleChName", T_addModule.T_moduleChName);
            p[3] = new SqlParameter("@ModuleInt", T_addModule.T_moduleInt);
            p[4] = new SqlParameter("@ModuleImgIdx", T_addModule.T_moduleImgIdx);
            p[5] = new SqlParameter("@ModuleFileName", DESEncrypt.DesEncrypt(T_addModule.T_moduleFileName));
            p[6] = new SqlParameter("@id", count);
            if (count > 0)
                p[7] = new SqlParameter("@stat", 2);
            else
                p[7] = new SqlParameter("@stat", 1);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            Common.Writelog(0, "增加模块:" + T_addModule.T_moduleChName);
        }

        public static void SeleModule()
        {
            DataTable dt = null;
            string strSql = "select * from M_Moudule where ModuleChName=@ModuleChName";
            SqlParameter p1 = new SqlParameter("@ModuleChName", T_addModule.T_moduleChName);
            dt = SQLHelper.ExcuteTable(strSql, p1);
            if (dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) {
                    T_addModule.T_moduleName = DESEncrypt.DesDecrypt(dr["ModuleName"].ToString());
                    T_addModule.T_moduleChName = dr["ModuleChName"].ToString();
                    T_addModule.T_moduleFileName = DESEncrypt.DesDecrypt(dr["ModuleFileName"].ToString());
                    T_addModule.T_moduleInt = dr["ModuleInt"].ToString();
                    T_addModule.T_moduleImgIdx = Convert.ToInt32(dr["ModuleImgIdx"].ToString());
                }
                return;
            }
            T_addModule.T_moduleName = "";
            T_addModule.T_moduleChName = "";
            T_addModule.T_moduleFileName = "";
            T_addModule.T_moduleInt = "";
            T_addModule.T_moduleImgIdx = 0;
        }

        public static void SeleModuleSofte()
        {
            DataTable dt = null;
            string strSql = "select * from M_Soid where Msoid=@Msoid";
            SqlParameter p1 = new SqlParameter("@Msoid", T_addModule.T_id);
            dt = SQLHelper.ExcuteTable(strSql, p1);
            if (dt.Rows.Count > 0) {
                DataRow dr = dt.Rows[0];
                T_addModule.T_sn = dr["Msosn"].ToString();
                T_addModule.T_time = dr["Msotm"].ToString();
                return;
            }
            T_addModule.T_sn = "";
            T_addModule.T_time = "";
        }

        public static void SaveModuleSofte(bool t)
        {
            if (t == false) {
                string strSql = "select id from M_Soid where Msoid=@Msoid ";
                SqlParameter p1 = new SqlParameter("@Msoid", T_addModule.T_id);
                int count = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                strSql = "Psoftid";
                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@Msoid", T_addModule.T_id);
                p[1] = new SqlParameter("@Msosn", T_addModule.T_sn);
                p[2] = new SqlParameter("@Msotm", T_addModule.T_time);
                if (count > 0)
                    p[3] = new SqlParameter("@id", 2);
                else
                    p[3] = new SqlParameter("@id", 1);
                p[4] = new SqlParameter("@userid", T_User.UserId);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            }
            else {
                string strSql = "update M_Soid set Msotm=@time ";
                SqlParameter p1 = new SqlParameter("@time", T_addModule.T_time);
                SQLHelper.ExecScalar(strSql, p1);
            }
            Common.Writelog(0, "修改软件授权时间" + T_addModule.T_time);
        }

        public static void UpdateModuelSys(string name, bool sys)
        {
            string strSql = "";
            if (sys)
                strSql = "update M_Moudule set ModuleSys=1  where ModuleChName=@name";
            else
                strSql = "update M_Moudule set ModuleSys=null  where ModuleChName=@name";
            SqlParameter p1 = new SqlParameter("@name", name);
            SQLHelper.ExecScalar(strSql, p1);
            Common.Writelog(0, "对模块" + name + "进行" + sys.ToString());

        }
        public static void DelModuelSys(string name)
        {
            string strSql = "delete from  M_Moudule  where ModuleChName=@name";
            SqlParameter p1 = new SqlParameter("@name", name);
            SQLHelper.ExecScalar(strSql, p1);
            Common.Writelog(0, "永久禁用模块" + name);
        }



        #endregion

        #region Ftpset

        public static void GetFtpset()
        {
            string strSql = "select * from M_FtpInfo ";
            DataTable dt = DAL.SQLHelper.ExcuteTable(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];
            T_ConFigure.FtpIP = dr["IP"].ToString();
            T_ConFigure.FtpPort = Convert.ToInt32(dr["Port"].ToString());
            T_ConFigure.FtpUser = dr["UserName"].ToString();
            T_ConFigure.FtpPwd = DESEncrypt.DesDecrypt(dr["PWD"].ToString());
            T_ConFigure.FtpArchScan = dr["ArchScan"].ToString();
            T_ConFigure.FtpArchIndex = dr["ArchIndex"].ToString();
            T_ConFigure.FtpArchSave = dr["ArchSave"].ToString();
            T_ConFigure.FtpArchUpdate = dr["UpdatePath"].ToString();
            T_ConFigure.FtpTmp = dr["FtpTmp"].ToString();
            T_ConFigure.FtpTmpPath = dr["FtpTmpPath"].ToString();
            T_ConFigure.FtpBakimgFwq = Convert.ToInt32(dr["FtpBakimgFwq"].ToString());
            T_ConFigure.FtpBakimgBd = Convert.ToInt32(dr["FtpBakimgBd"].ToString());
            T_ConFigure.FtpStyle = Convert.ToInt32(dr["FtpStyle"].ToString());
            T_ConFigure.FtpFwqPath = dr["FtpFwqPath"].ToString();
        }

        public static int SetFtpInfo()
        {
            string strSql = "PSaveFtpInfo";
            SqlParameter[] p = new SqlParameter[15];
            p[0] = new SqlParameter("@ip", T_ConFigure.FtpIP);
            p[1] = new SqlParameter("@Port", T_ConFigure.FtpPort);
            p[2] = new SqlParameter("@UserName", T_ConFigure.FtpUser);
            p[3] = new SqlParameter("@PWD", DESEncrypt.DesEncrypt(T_ConFigure.FtpPwd));
            p[4] = new SqlParameter("@ArchScan", T_ConFigure.FtpArchScan);
            p[5] = new SqlParameter("@ArchIndex", T_ConFigure.FtpArchIndex);
            p[6] = new SqlParameter("@ArchSave", T_ConFigure.FtpArchSave);
            p[7] = new SqlParameter("@Update", T_ConFigure.FtpArchUpdate);
            p[8] = new SqlParameter("@FtpTmp", T_ConFigure.FtpTmp);
            p[9] = new SqlParameter("@FtpTmpPath", T_ConFigure.FtpTmpPath);
            p[10] = new SqlParameter("@FtpBakimgFwq", T_ConFigure.FtpBakimgFwq);
            p[11] = new SqlParameter("@FtpBakimgBd", T_ConFigure.FtpBakimgBd);
            p[12] = new SqlParameter("@FtpStyle", T_ConFigure.FtpStyle);
            p[13] = new SqlParameter("@userid", T_User.UserId);
            p[14] = new SqlParameter("@fwqpath", T_ConFigure.FtpFwqPath);
            int i = Convert.ToInt32(SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p).ToString());
            return i;
        }

        #endregion

        #region soid

        public static void Getsoid(string str)
        {
            try {
                string strSql = "select * from M_soid where Msoid=@Msoid";
                SqlParameter p1 = new SqlParameter("@Msoid", str);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
                if (dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    T_ConFigure.Mosn = dr["Msosn"].ToString();
                    T_ConFigure.Motm = dr["Msotm"].ToString();
                    return;
                }
                T_ConFigure.Mosn = "";
                T_ConFigure.Motm = "";
            } catch {
                T_ConFigure.Mosn = "";
                T_ConFigure.Motm = "";
            }

        }

        #endregion

        #region HouseSet

        public static DataTable GetHouseInfo()
        {

            string strSql = "select * from M_HouseName order by id";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }
        public static DataTable GetHouseName()
        {

            string strSql = "select id,HouseName from M_HouseName order by id";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static bool SelectHouseName()
        {
            string strSql = "select count(*) from M_HouseName where HouseName=@HouseName ";
            SqlParameter p1 = new SqlParameter("@HouseName", V_HouseName.HouseSetName);
            int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1).ToString());
            if (id > 0)
                return true;
            else
                return false;
        }
        public static int SelectHouseNameid()
        {
            string strSql = "select id from M_HouseName where HouseName=@HouseName ";
            SqlParameter p1 = new SqlParameter("@HouseName", V_HouseName.HouseSetName);
            int id = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1).ToString());
            return id;
        }

        public static void AddHouseInfo()
        {
            string strSql = "PInsertHouse";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@HouseName", V_HouseName.HouseSetName);
            p[1] = new SqlParameter("@HouseType", V_HouseName.HouseSetType);
            p[2] = new SqlParameter("@BoxNumber", V_HouseName.MaxBoxNum);
            int i = SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }
        public static void UpdateHouseInfo(string id)
        {
            string strSql = "PUpdateHouse";
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@HouseName", V_HouseName.HouseSetName);
            p[1] = new SqlParameter("@HouseType", V_HouseName.HouseSetType);
            p[2] = new SqlParameter("@BoxNumber", V_HouseName.MaxBoxNum);
            p[3] = new SqlParameter("@HouseID", id);
            p[4] = new SqlParameter("@userid", T_User.UserId);
            int i = SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static bool isHouseData()
        {
            string strSqlcz = "select top 1 HouseID from M_HouseSet where HouseID=@HouseID  ";
            SqlParameter p1 = new SqlParameter("@HouseID", V_HouseName.HouseSetid);
            DataTable dt = SQLHelper.ExcuteTable(strSqlcz, p1);
            if (dt.Rows.Count > 0) {
                return true;
            }
            return false;
        }

        public static void DeleteHouse()
        {
            string strSqlDel = "delete from M_HouseName where HouseID = @HouseID";
            SqlParameter p = new SqlParameter("@HouseID", V_HouseName.HouseSetid);
            SQLHelper.ExecScalar(strSqlDel, p);
        }


        public static DataTable GetHouseGui(int id)
        {
            DataTable dt = null;
            try {
                string strSql = "select * from M_HouseSet where  Houseid=@HouseID order by HouseGui";
                SqlParameter p1 = new SqlParameter("@HouseID", id);
                dt = SQLHelper.ExcuteTable(strSql, p1);
                return dt;
            } catch {
                return null;
            }
        }
        public static int GetHouseGuiMax(int id)
        {
            try {
                string strSql = "select max(HouseGui) from M_HouseSet where  Houseid=@HouseID ";
                SqlParameter p1 = new SqlParameter("@HouseID", id);
                int box = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1));
                return box;

            } catch {
                return 0;
            }
        }

        public static void HouseSetAdd(int id)
        {
            string strSql = "";
            if (id == 0)
                strSql = "PInsertCabInfo";
            else
                strSql = "PInsertCabInfoLot";
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@CabNo", V_HouseSetCs.HouseGui);
            p[1] = new SqlParameter("@ColNo", V_HouseSetCs.HouseCol);
            p[2] = new SqlParameter("@RowNo", V_HouseSetCs.HouseRow);
            p[3] = new SqlParameter("@BoxCount", V_HouseSetCs.Housebox);
            p[4] = new SqlParameter("@HouseID", V_HouseSetCs.Id);
            p[5] = new SqlParameter("@JuanSn", V_HouseSetCs.Housejuan);
            p[6] = new SqlParameter("@useid", T_User.UserId);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static void HouseSetChanger()
        {
            string strSql = "PUpdateCabInfo";
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@CabNo", V_HouseSetCs.HouseGui);
            p[1] = new SqlParameter("@ColNo", V_HouseSetCs.HouseCol);
            p[2] = new SqlParameter("@RowNo", V_HouseSetCs.HouseRow);
            p[3] = new SqlParameter("@BoxCount", V_HouseSetCs.Housebox);
            p[4] = new SqlParameter("@CabinetID", V_HouseSetCs.Id);
            p[5] = new SqlParameter("@HouseID", V_HouseSetCs.Houseid);
            p[6] = new SqlParameter("@juansn", V_HouseSetCs.Housejuan);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static int Getboxsnimage(int boxsnq, int boxsnz)
        {
            try {
                string strSql = "select COUNT( IMGFILE)  from T_IMAGEFILE where BOXSN>@boxsnq and  BOXSN<@boxsnz";
                SqlParameter p1 = new SqlParameter("@boxsnq", boxsnq);
                SqlParameter p2 = new SqlParameter("@boxsnz", boxsnz);
                int i = Convert.ToInt32(SQLHelper.ExecScalar(strSql, p1, p2));
                return i;
            } catch {
                return 0;
            }
        }

        public static void GetHouseGuiCs()
        {
            try {
                string strSql = "select * from M_HouseSet where  Houseid=@HouseID and HouseGui=@HouseGui";
                SqlParameter p1 = new SqlParameter("@HouseID", V_HouseSetCs.Houseid);
                SqlParameter p2 = new SqlParameter("@HouseGui", V_HouseSetCs.HouseGui);
                DataTable dt = SQLHelper.ExcuteTable(strSql, p1, p2);
                if (dt.Rows.Count > 0) {
                    DataRow dr = dt.Rows[0];
                    V_HouseSetCs.HouseCol = Convert.ToInt32(dr["HouseCol"].ToString());
                    V_HouseSetCs.HouseRow = Convert.ToInt32(dr["HouseRow"].ToString());
                    V_HouseSetCs.Housebox = Convert.ToInt32(dr["HoseBoxCount"].ToString());
                    V_HouseSetCs.Housejuan = Convert.ToInt32(dr["HouseJuan"].ToString());
                    V_HouseSetCs.HouseboxMax = Convert.ToInt32(dr["MAXBOXSN"].ToString());
                    return;
                }
                V_HouseSetCs.HouseCol = 0;
                V_HouseSetCs.HouseRow = 0;
                V_HouseSetCs.Housebox = 0;
                V_HouseSetCs.Housejuan = 0;
                V_HouseSetCs.HouseboxMax = 0;
            } catch {
                V_HouseSetCs.HouseCol = 0;
                V_HouseSetCs.HouseRow = 0;
                V_HouseSetCs.Housebox = 0;
                V_HouseSetCs.Housejuan = 0;
                V_HouseSetCs.HouseboxMax = 0;
            }

        }

        #endregion

        #region generalSet

        public static string isTable(string table)
        {
            try {
                string strSql = "SELECT table_name FROM information_schema.TABLES WHERE table_name =@table";
                SqlParameter p1 = new SqlParameter("@table", table);
                string tab = SQLHelper.ExecScalar(strSql, p1).ToString();
                return tab;
            } catch {
                return "";
            }
        }

        public static DataTable GetTableCol(string table)
        {
            string strSql = "PGetTableInfo";
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@table", table);
            DataTable dt = SQLHelper.GetDataTable(strSql, CommandType.StoredProcedure, p);
            return dt;
        }

        public static DataTable GetTableName(string table)
        {
            string strSql = "SELECT NAME FROM SYSCOLUMNS WHERE ID = OBJECT_ID(@table)";
            SqlParameter p1 = new SqlParameter("@table", table);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p1);
            return dt;
        }

        public static void UpdateGensetPrint(string table, string xy, string col, string fontall, string fontspec)
        {
            string strSql = "update M_genset set printTable=@table, PrintxyCol=@xy,PrintColInfo=@col ,PrintFontColAll=@fontall,PrintFontSpec=@fontspec";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@xy", xy);
            SqlParameter p2 = new SqlParameter("@col", col);
            SqlParameter p3 = new SqlParameter("@fontall", fontall);
            SqlParameter p4 = new SqlParameter("@fontspec", fontspec);
            SQLHelper.ExecScalar(strSql, p0, p1, p2, p3, p4);
        }

        public static DataTable GetGensetPrint()
        {
            string strSql = "select * from M_genset ";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void UpdateGensetPrintConten(string table, string info)
        {
            string strSql = "update M_genset set PrintContenTable=@table, PrintContenInfo=@info";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", info);
            SQLHelper.ExecScalar(strSql, p0, p1);
        }

        public static void UpdateGensetImport(string table, string info)
        {
            string strSql = "update M_GenSetImport set ImportInfoZd=@info where ImportTable=@table";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", info);
            SQLHelper.ExecScalar(strSql, p0, p1);

        }

        public static void SaveGensetImport(string table, string info)
        {
            string strSql = "insert into  M_GenSetImport  (ImportTable,ImportInfoZd) values(@table,@info)";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", info);
            SQLHelper.ExecScalar(strSql, p0, p1);
        }

        public static DataTable GetImportDt()
        {
            string strSql = "select * from M_GenSetImport ";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }


        public static DataTable GetImportTable(string str)
        {
            string strSql = "select ImportInfoZd from M_GenSetImport where ImportTable=@table ";
            SqlParameter p0 = new SqlParameter("@table", str);
            DataTable dt = SQLHelper.ExcuteTable(strSql, p0);
            return dt;
        }

        public static void DelImportTable(string t)
        {
            string strSql = "delete from M_GenSetImport where ImportTable=@table";
            SqlParameter p1 = new SqlParameter("@table", t);
            SQLHelper.ExecScalar(strSql, p1);
        }

        public static DataTable GetInfoTable()
        {
            string strSql = "select * from M_GenSetInfo ";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void SaveGensetInfo(string table, string info, string name, string num, string width, string txtwidth)
        {
            string strSql = "insert into  M_GenSetInfo  (InfoTable,InfoAddzd,InfoName,InfoNum,InfoLabWidth,InfoTxtWidth) values(@table,@info,@name,@num,@width,@txtwidth)";
            SqlParameter[] par =
            {
                new SqlParameter("@table", table),
                new SqlParameter("@info", info),
                new SqlParameter("@name", name),
                new SqlParameter("@num", num),
                new SqlParameter("@width", width),
                new SqlParameter("@txtwidth", txtwidth)
            };
            SQLHelper.ExecScalar(strSql, par);
        }
        public static void UpdateGensetInfo(string table, string info, string name, string num, string width, string txtwith)
        {
            string strSql = "update M_GenSetInfo set InfoAddzd=@info, InfoName=@name, InfoNum=@num,InfoLabWidth=@width,InfoTxtWidth=@txtwith  where InfoTable=@table";
            SqlParameter[] par =
            {
                new SqlParameter("@table", table),
                new SqlParameter("@info", info),
                new SqlParameter("@name", name),
                new SqlParameter("@num", num),
                new SqlParameter("@width", width),
                new SqlParameter("@txtwith", txtwith)
            };
            SQLHelper.ExecScalar(strSql, par);
        }

        public static void DelInfoTable(string t)
        {
            string strSql = "delete from M_GenSetInfo where InfoTable=@table";
            SqlParameter p1 = new SqlParameter("@table", t);
            SQLHelper.ExecScalar(strSql, p1);
        }


        public static void UpdateQuerInfo(string table, string str, int enter)
        {
            string strSql = "update M_genset set QuerTable=@table, QuerInfoZd=@info,QuerEnter=@enter";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", str);
            SqlParameter p2 = new SqlParameter("enter", enter);
            SQLHelper.ExecScalar(strSql, p0, p1, p2);
        }



        public static void UPdateDataSplitInfo(string dirtable, int dirsn, string dircol, string dirml, string filetable, int filesn, string filename, bool filebool, string filecol)
        {
            string strSql = "update M_GenSetDataSplit set DataTable=@dirtable, Dirsn=@dirsn,DirCol=@dircol," +
                            "DirMl=@dirml,FileTable=@filetable,Filesn=@filesn,FileName=@filename,FileBool=@filebool,FileNamecol=@filecol";
            SqlParameter[] par =
            {
                new SqlParameter("@dirtable", dirtable),
                new SqlParameter("@dirsn", dirsn),
                new SqlParameter("@dircol", dircol),
                new SqlParameter("@dirml", dirml),
                new SqlParameter("@filetable", filetable),
                new SqlParameter("@filesn", filesn),
                new SqlParameter("@filename", filename),
                new SqlParameter("@filebool", filebool.ToString()),
                new SqlParameter("@filecol", filecol)
            };
            SQLHelper.ExecScalar(strSql, par);
        }

        public static DataTable GetDataSplit()
        {
            string strSql = "select * from M_GenSetDataSplit";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void SaveDataSplitExport(string table, string info, string xlsid)
        {
            string strSql = "insert into  M_GenSetDataSplitTable  (ImportTable,ImportCol,BindId) values(@table,@info,@xlsid)";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", info);
            SqlParameter p2 = new SqlParameter("@xlsid", xlsid);
            SQLHelper.ExecScalar(strSql, p0, p1, p2);
        }
        public static void UpdateDataSplitExport(string table, string info, string xlsid)
        {
            string strSql = "update M_GenSetDataSplitTable set ImportCol=@info, BindId=@xlsid  where InfoTable=@table";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", info);
            SqlParameter p2 = new SqlParameter("@name", xlsid);
            SQLHelper.ExecScalar(strSql, p0, p1, p2);
        }


        public static void DelDataSplitExportTable(string t)
        {
            string strSql = "delete from M_GenSetDataSplitTable where ImportTable=@table";
            SqlParameter p1 = new SqlParameter("@table", t);
            SQLHelper.ExecScalar(strSql, p1);
        }

        public static DataTable GetDataSplitExporTable()
        {
            string strSql = "select * from M_GenSetDataSplitTable";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }


        public static void SaveGensetInfoCheck(string table, string info, string msg)
        {
            string strSql = "insert into  M_GenSetInfoCheck  (InfoCheckTable,InfocheckCol,InfoCheckMsg) values(@table,@info,@msg)";
            SqlParameter[] par =
            {
                new SqlParameter("@table", table),
                new SqlParameter("@info", info),
                new SqlParameter("@msg", msg)
            };
            SQLHelper.ExecScalar(strSql, par);
        }
        public static void UpdateGensetInfoCheck(string table, string info, string msg)
        {
            string strSql = "update M_GenSetInfoCheck set InfocheckCol=@col, InfoCheckMsg=@msg where InfoCheckTable=@table";
            SqlParameter[] par =
            {
                new SqlParameter("@table", table),
                new SqlParameter("@col", info),
                new SqlParameter("@msg", msg)
            };
            SQLHelper.ExecScalar(strSql, par);
        }

        public static DataTable GetInfoCheck()
        {
            string strSql = "select * from M_GenSetInfoCheck";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void DelInfoCheckTable(string t)
        {
            string strSql = "delete from M_GenSetInfoCheck where InfoCheckTable=@table";
            SqlParameter p1 = new SqlParameter("@table", t);
            SQLHelper.ExecScalar(strSql, p1);
        }

        public static void UpdateConten(string table, string str, string lie, string with, string txtwith, string title, string pages)
        {
            string strSql = "update M_GenSetConten set ContenTable=@table, ContenCol=@info ,ContenLie=@lie,ContenWith=@with,ContentxtWith=@txtwith,ContenTitle=@title,ContenPages=@pages";
            SqlParameter[] par =
            {
                new SqlParameter("@table", table),
                new SqlParameter("@info", str),
                new SqlParameter("@lie", lie),
                new SqlParameter("@with", with),
                new SqlParameter("@txtwith", txtwith),
                new SqlParameter("@title", title),
                new SqlParameter("@pages", pages)
            };
            SQLHelper.ExecScalar(strSql, par);
        }
        public static DataTable GetConten()
        {
            string strSql = "select * from M_GenSetConten";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void DelTableCol(string table, string col)
        {
            string strSql = "ALTER TABLE " + table + " DROP COLUMN " + col;
            SQLHelper.ExecScalar(strSql);
        }

        public static void CreateTableColAlter(string table, string col, string lx, bool nullk)
        {
            string str = "";
            if (nullk == true)
                str = "null ";
            else str = "not null";
            string strSql = "ALTER TABLE " + table + " alter column  " + col + " " + lx + " " + str;
            SQLHelper.ExecScalar(strSql);
        }

        public static DataTable GetSysTable()
        {
            string strSql = "select * from M_sysTable ";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void SaveCreateTable(string table, List<string> col, List<string> lx, List<string> nullk, bool crsave)
        {
            string strSql = "";
            if (crsave == true) {
                strSql = "CREATE TABLE [dbo].[" + table + "]( [ID] [int] IDENTITY(1,1) NOT NULL,";
                for (int i = 0; i < col.Count; i++) {
                    if (i != col.Count - 1)
                        strSql += "[" + col[i] + "] " + lx[i] + " " + nullk[i] + " , ";
                    else
                        strSql += "[" + col[i] + "] " + lx[i] + " " + nullk[i];
                }
                strSql += ",[EnterTag] int, [Archid] int not null )";
            }
            else {
                strSql = "ALTER TABLE " + table + " add ";
                for (int i = 0; i < col.Count; i++) {
                    if (i != col.Count - 1)
                        strSql += col[i] + " " + lx[i] + " " + nullk[i] + ",";
                    else
                        strSql += col[i] + " " + lx[i] + " " + nullk[i];
                }

            }
            SQLHelper.ExecScalar(strSql);
        }


        public static void CreateTableExplain(string table, List<string> col, List<string> colsm)
        {
            string strSql = "PaddDescription";
            for (int i = 0; i < col.Count; i++) {
                string c = col[i];
                string sm = colsm[i];
                if (sm.Trim().Length <= 0)
                    continue;
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@sm", sm);
                p[1] = new SqlParameter("@table", table);
                p[1] = new SqlParameter("@col", c);
                SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
            }
        }
        public static void CreateTableExplain(string table, string col, string colsm)
        {
            string strSql = "PaddDescription";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@sm", colsm);
            p[1] = new SqlParameter("@table", table);
            p[1] = new SqlParameter("@col", col);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static void CreateTableUpdateExplain(string table, string col, string colsm)
        {
            string strSql = "PUpdateDescription";
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@sm", colsm);
            p[1] = new SqlParameter("@table", table);
            p[1] = new SqlParameter("@col", col);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);

        }

        public static void CreateTableDelExplain(string table, string col)
        {
            string strSql = "PDelDescription";
            SqlParameter[] p = new SqlParameter[2];
            p[1] = new SqlParameter("@table", table);
            p[1] = new SqlParameter("@col", col);
            SQLHelper.ExecuteNonQuery(strSql, CommandType.StoredProcedure, p);
        }

        public static DataTable GetborrTable()
        {
            string strSql = "select * from M_GenSetBorr ";
            DataTable dt = SQLHelper.ExcuteTable(strSql);
            return dt;
        }

        public static void UpdateBorrInfo(string table, string str, bool tag,string time)
        {
            string strSql = "update M_GenSetBorr set Tablename=@table, Tabcolname=@info, Timecol=@time";
            SqlParameter p0 = new SqlParameter("@table", table);
            SqlParameter p1 = new SqlParameter("@info", str);
            SqlParameter p2 = new SqlParameter("@time", time);
            SQLHelper.ExecScalar(strSql, p0, p1,p2);
            if (!tag) {
                strSql = "alter table " + table + " add BorrTag varchar(10)";
                SQLHelper.ExecScalar(strSql);
            }

        }

        #endregion

        #region backsjk

        public static void BackSql(string p)
        {
            string sjk = SQLHelper.connstr;
            if (sjk.Trim().Length > 0) {
                string[] a = sjk.Split(';');
                string b = a[1].Replace("Database=", "");
                string strSql = "backup database @sjk to disk=@path";
                SqlParameter p0 = new SqlParameter("@sjk", b.Trim());
                SqlParameter p1 = new SqlParameter("@path", p);
                SQLHelper.ExecScalar(strSql, p0, p1);
            }
        }


        #endregion
    }
}
