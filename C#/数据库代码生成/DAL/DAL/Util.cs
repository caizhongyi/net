using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

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
