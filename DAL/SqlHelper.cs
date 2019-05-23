using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class SQLHelper
    {
        public static SqlConnection Sqlconn = null;
        public static SqlCommand cmd = null;
        public static string connstr = DESEncrypt.DesDecryptkey(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);


        public SQLHelper()
        { }

        #region 建立数据库连接对象
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns>返回一个数据库的连接SqlConnection对象</returns>
        public static SqlConnection init()
        {
            try {
                Sqlconn = new SqlConnection(connstr);
                if (Sqlconn.State != ConnectionState.Open) {
                    Sqlconn.Open();
                }
            } catch (Exception e) {
                throw new Exception(e.Message.ToString());
            }
            return Sqlconn;
        }
        #endregion

        public static void sjClose()
        {
            if (Sqlconn.State == ConnectionState.Open) {
                Sqlconn.Close();
            }
        }

        #region 设置SqlCommand对象
        /// <summary>
        /// 设置SqlCommand对象       
        /// </summary>
        /// <param name="cmd">SqlCommand对象 </param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        private static void SetCommand(SqlCommand cmd, string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connstr)) {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                if (cmdParms != null) {
                    cmd.Parameters.AddRange(cmdParms);
                }
            }
        }
        #endregion

        #region 执行相应的sql语句，返回相应的DataSet对象
        /// <summary>
        /// 执行相应的sql语句，返回相应的DataSet对象
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <returns>返回相应的DataSet对象</returns>
        public static DataSet GetDataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(sqlstr, conn);
                    ada.Fill(ds);
                }
            } catch (Exception e) {
                throw new Exception(e.Message.ToString());
            }
            return ds;
        }
        #endregion

        #region 执行相应的sql语句，返回相应的DataSet对象
        /// <summary>
        /// 执行相应的sql语句，返回相应的DataSet对象
        /// </summary>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回相应的DataSet对象</returns>
        public static DataSet GetDataSet(string sqlstr, string tableName)
        {
            DataSet ds = new DataSet();
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(sqlstr, conn);
                    ada.Fill(ds, tableName);
                }
            } catch (Exception e) {
                throw new Exception(e.Message.ToString());
            }
            return ds;
        }
        #endregion

        #region 执行不带参数sql语句，返回一个DataTable对象
        /// <summary>
        /// 执行不带参数sql语句，返回一个DataTable对象
        /// </summary>
        /// <param name="cmdText">相应的sql语句</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable GetDataTable(string cmdText)
        {

            SqlDataReader reader;
            DataTable dt = new DataTable();
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand(cmdText, conn);
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(reader);
                    reader.Close();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行带参数的sql语句或存储过程，返回一个DataTable对象
        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回一个DataTable对象
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回一个DataTable对象</returns>
        public static DataTable GetDataTable(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlDataReader reader;
            DataTable dt = new DataTable();
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    if (cmdParms != null) {
                        cmd.Parameters.AddRange(cmdParms);
                    }
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(reader);
                    reader.Close();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行不带参数sql语句，返回所影响的行数
        /// <summary>
        /// 执行不带参数sql语句，返回所影响的行数
        /// </summary>
        /// <param name="cmdText">增，删，改sql语句</param>
        /// <returns>返回所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            int count;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand(cmdText, conn);
                    cmd.CommandTimeout = 120;
                    count = cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行带参数sql语句或存储过程，返回所影响的行数
        /// <summary>
        /// 执行带参数sql语句或存储过程，返回所影响的行数
        /// </summary>
        /// <param name="cmdText">带参数的sql语句和存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回所影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            int count;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.CommandTimeout = 120;
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    if (cmdParms != null) {
                        cmd.Parameters.AddRange(cmdParms);
                    }
                    count = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行不带参数sql语句，返回一个从数据源读取数据的SqlDataReader对象
        /// <summary>
        /// 执行不带参数sql语句，返回一个从数据源读取数据的SqlDataReader对象
        /// </summary>
        /// <param name="cmdText">相应的sql语句</param>
        /// <returns>返回一个从数据源读取数据的SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string cmdText)
        {
            SqlDataReader reader;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand(cmdText, conn);
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行带参数的sql语句或存储过程，返回一个从数据源读取数据的SqlDataReader对象
        /// <summary>
        /// 执行带参数的sql语句或存储过程，返回一个从数据源读取数据的SqlDataReader对象
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">参数集合</param>
        /// <returns>返回一个从数据源读取数据的SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlDataReader reader;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    if (cmdParms != null) {
                        cmd.Parameters.AddRange(cmdParms);
                    }
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行不带参数sql语句,返回结果集首行首列的值object
        /// <summary>
        /// 执行不带参数sql语句,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdText">相应的sql语句</param>
        /// <returns>返回结果集首行首列的值object</returns>
        public static object ExecuteScalar(string cmdText)
        {
            object obj;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand(cmdText, conn);
                    obj = cmd.ExecuteScalar();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion

        #region 执行带参数sql语句或存储过程,返回结果集首行首列的值object
        /// <summary>
        /// 执行带参数sql语句或存储过程,返回结果集首行首列的值object
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdParms">返回结果集首行首列的值object</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            object obj;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    if (cmdParms != null) {
                        cmd.Parameters.AddRange(cmdParms);
                    }
                    obj = cmd.ExecuteScalar();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion

        #region 执行查询语句，返回一个表
        /// <summary>
        /// 执行查询语句，返回一个表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ps">参数数组</param>
        /// <returns>返回一张表</returns>
        public static DataTable ExcuteTable(string sql, params SqlParameter[] ps)
        {
            DataTable dt = null;
            try {
                using (SqlConnection conn = new SqlConnection(connstr)) {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddRange(ps);
                    dt = new DataTable();
                    da.Fill(dt);
                }
            } catch {
                return null;
            }
            return dt;
        }
        #endregion

        #region 执行增删改的方法
        /// <summary>
        /// 执行增删改的方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ps">参数数组</param>
        /// <returns>返回一条记录</returns>
        public static int ExcuteNoQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connstr)) {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(ps);
                return command.ExecuteNonQuery();
            }
        }
        #endregion

        #region 执行存储过程的方法
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="ps">参数数组</param>
        /// <returns></returns>
        public static int ExcuteProc(string procName, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connstr)) {
                conn.Open();
                SqlCommand command = new SqlCommand(procName, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(ps);
                return command.ExecuteNonQuery();
            }
        }
        #endregion

        #region 查询结果集，返回的是首行首列
        /// <summary>
        /// 查询结果集，返回的是首行首列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ps">参数数组</param>
        /// <returns></returns>
        public static object ExecScalar(string sql, params SqlParameter[] ps) //调用的时候才判断是什么类型
        {
            using (SqlConnection conn = new SqlConnection(connstr)) {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(ps);
                return command.ExecuteScalar();
            }
        }
        #endregion

        /// <summary>  
        /// 验证数据是否存在  
        /// </summary>  
        /// <param name="sql"></param>  
        /// <param name="param"></param>  
        /// <returns></returns>  
        public bool ValiExist(string sql, SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(connstr)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (param != null)
                    cmd.Parameters.AddRange(param);
                using (SqlDataReader dr = cmd.ExecuteReader()) {
                    if (dr.Read()) {
                        conn.Close();
                        return true;
                    }
                    else {
                        conn.Close();
                        return false;
                    }
                }
            }
        }



    }
}