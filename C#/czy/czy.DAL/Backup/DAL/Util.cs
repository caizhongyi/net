using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace DAL
{
    public class Util
    {
        //连接数据库
        public static  SqlConnection GetConn()
        {
            string ConnStr =System.Configuration.ConfigurationManager.AppSettings["ConnStr"].ToString();
            SqlConnection conn = new SqlConnection(ConnStr);
            return conn;
        }


        #region  非存储过程
        //执行操作
        public static int GetExecuteNonQuery(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                int i = Convert.ToInt32(comm.ExecuteNonQuery());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>
        /// 公有方法，执行一组Sql语句。
        /// </summary>
        /// <param name="SqlStrings">Sql语句组</param>
        /// <returns>0为失败,1为成功</returns>
        public static int GetExecuteNonQuery(ArrayList SqlStrings)
        {
            int state = -1;
            SqlConnection conn = GetConn();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = conn.BeginTransaction();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            try
            {
                foreach (String str in SqlStrings)
                {
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                state = 1;
            }
            catch
            {
                trans.Rollback();
                state = 0;
            }
            finally
            {
                conn.Close();
            }
            return state;
        }
        //读取单个整形操作
        public static  int GetIntExecuteScalar(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                int i = Convert.ToInt32(comm.ExecuteScalar());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //读取单个字符串操作
        public static string GetStrExecuteScalar(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                string i = Convert.ToString(comm.ExecuteScalar());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //执行读取操作
        public static SqlDataReader GetExecuteReader(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //返回一个数据集操作
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //返回一个数据表
        public static DataTable GetDataTable(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable  dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }


        /// <summary>
        /// 公有方法，在一个数据表中插入一条记录。
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Cols">哈西表，键值为字段名，值为字段值</param>
        /// <returns>是否成功</returns>
        public int Insert(String TableName, Hashtable Cols)
        {
            int Count = 0;

            if (Cols.Count <= 0)
            {
                return 1;
            }

            String Fields = " (";
            String Values = " Values(";
            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                {
                    Fields += ",";
                    Values += ",";
                }
                Fields += item.Key.ToString();
                Values += item.Value.ToString();
                Count++;
            }
            Fields += ")";
            Values += ")";

            String SqlString = "Insert into " + TableName + Fields + Values;

            return Convert.ToInt32(GetExecuteNonQuery(SqlString));
        }

        /// <summary>
        /// 公有方法，更新一个数据表。
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Cols">哈西表，键值为字段名，值为字段值</param>
        /// <param name="Where">Where子句</param>
        /// <returns>是否成功</returns>
        public int Update(String TableName, Hashtable Cols, String Where)
        {
            int Count = 0;
            if (Cols.Count <= 0)
            {
                return 1;
            }
            String Fields = " ";
            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                {
                    Fields += ",";
                }
                Fields += item.Key.ToString();
                Fields += "=";
                Fields += item.Value.ToString();
                Count++;
            }
            Fields += " ";

            String SqlString = "Update " + TableName + " Set " + Fields + Where;

            return Convert.ToInt32(GetExecuteNonQuery(SqlString));
        }
        #endregion



        #region  存储过程
        //执行操作
        public static int GetExecuteNonQuery(string sql,SqlParameter[] sqlParam)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                int i = Convert.ToInt32(comm.ExecuteNonQuery());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //读取单个整形操作
        public static int GetIntExecuteScalar(string sql, SqlParameter[] sqlParam)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                int i = Convert.ToInt32(comm.ExecuteScalar());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //读取单个字符串操作
        public static string GetStrExecuteScalar(string sql, SqlParameter[] sqlParam)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                string i = Convert.ToString(comm.ExecuteScalar());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //执行读取操作
        public static SqlDataReader GetExecuteReader(string sql, SqlParameter[] sqlParam)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                SqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Dispose();
            }
        }
        //返回一个数据集操作
        public static DataSet GetDataSet(string sql, SqlParameter[] sqlParam)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    sqlCommand.Parameters.Add(param);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        //返回一个数据表
        public static DataTable GetDataTable(string sql, SqlParameter[] sqlParam)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
               
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    sqlCommand.Parameters.Add(param);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion
    }
}
