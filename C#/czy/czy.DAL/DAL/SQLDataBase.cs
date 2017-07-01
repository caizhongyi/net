using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace czy.MyDAL
{
    public class SQLDataBase : DataBase, IDataBaseAdvance
    {
        /// <summary>
        /// DataRead读取器事件
        /// </summary>
        public event DataReadEvent DataRead;
        protected SqlConnection conn;
        //SqlDataReader reader;

        //public SqlDataReader Reader
        //{
        //    get { return reader; }
        //    set { reader = value; }
        //}
        public SQLDataBase()
        { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="contectString">>链接字符窜</param>
        /// <param name="type">链接字符类型</param>
        public SQLDataBase(string contectString,ConnStringType type)
        {
            if (type == ConnStringType.String)
            {
                _connString = contectString;
            }
            else if (type == ConnStringType.ConfigKey)
            {
                _connString = System.Configuration.ConfigurationSettings.AppSettings[contectString].ToString();
            }
            _connType = type;
      
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="contectString">>链接字符窜</param>
        /// <param name="type">链接字符类型</param>
        public SQLDataBase(string server, string dataBase, string user, string pwd)
        {
            _connType = ConnStringType.String;
            _connString = string.Format("server={0};database={1};uid={2};pwd={3}",server,dataBase,user,pwd);
           

        }

        #region  非存储过程
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public  int ExecuteNonQuery(string sql)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            int i;
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                i = Convert.ToInt32(comm.ExecuteNonQuery());
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
            return i;
        }
        /// <summary>
        /// 读取单行单列值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>对像值</returns>
        public  object ExecuteScalar(string sql)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            object i;
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                i = comm.ExecuteScalar();
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
            return i;
        }
        /// <summary>
        /// 获取SqlDataReader读取器
        /// </summary>
        /// <param name="sql">该读取会一值保持链接</param>
        public  void ExecuteReader(string sql)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    DataRead(dr);
                }
                dr.Close();
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
        /// 返回一个数据集操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>数据集</returns>
        public  DataSet GetDataSet(string sql)
        {
            conn = new SqlConnection(_connString);
            DataSet ds = new DataSet();
            conn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
              
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
            return ds;
        }
        /// <summary>
        //  返回一个数据表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>数据表</returns>
        public  DataTable GetDataTable(string sql)
        {
            conn = new SqlConnection(_connString);
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
               
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
            return dt;
        }

        #endregion

        #region  存储过程
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>受影响的行数</returns>
        public  int ExecuteNonQuery(string sql,object[] sqlParam)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            int i = 0;
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                i = Convert.ToInt32(comm.ExecuteNonQuery());
               
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
            return i;
        }

        /// <summary>
        /// 读取单行单列值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>对像值</returns>
        public object ExecuteScalar(string sql, object[] sqlParam)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            object o;
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                o = Convert.ToString(comm.ExecuteScalar());
            
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
            return o;
        }
        /// <summary>
        /// 获取SqlDataReader读取器
        /// </summary>
        /// <param name="sql">该读取会一值保持链接</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        public void ExecuteReader(string sql, object[] sqlParam)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    comm.Parameters.Add(param);
                SqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    DataRead(dr);
                }
                dr.Close();
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
        /// 返回一个数据集操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string sql, object[] sqlParam)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    sqlCommand.Parameters.Add(param);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(ds);
                
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
            return ds;
        }
        /// <summary>
        //  返回一个数据表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string sql, object[] sqlParam)
        {
            conn = new SqlConnection(_connString);
            conn.Open();
            DataTable dt = new DataTable();
            try
            {
               
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in sqlParam)
                    sqlCommand.Parameters.Add(param);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dt);
              
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
            return dt;
        }
        #endregion
    }
}
