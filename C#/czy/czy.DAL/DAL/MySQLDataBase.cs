using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace czy.MyDAL
{
    public class MySqlDataBase :DataBase, IDataBaseAdvance
    {
        /// <summary>
        /// DataRead读取器事件
        /// </summary>
        public event DataReadEvent DataRead;
        protected MySqlConnection conn;
        public MySqlDataBase()
        { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="contectString">>链接字符窜</param>
        /// <param name="type">链接字符类型</param>
        public MySqlDataBase(string contectString,ConnStringType type)
        {
     
            if (type == ConnStringType.String)
            {
                _connString = contectString;
            }
            else
            {
                _connString = System.Configuration.ConfigurationSettings.AppSettings[contectString].ToString();
               
            }
            _connType = type;
          
        }
    


        #region  非存储过程
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="MySql">MySql语句</param>
        /// <returns>受影响的行数</returns>
        public  int ExecuteNonQuery(string MySql)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            int i;
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
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
        /// <param name="MySql">MySql语句</param>
        /// <returns>对像值</returns>
        public  object ExecuteScalar(string MySql)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            object i;
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
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
        /// 获取MySqlDataReader读取器
        /// </summary>
        /// <param name="MySql">该读取会一值保持链接</param>
        public  void ExecuteReader(string MySql)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
                MySqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
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
        /// <param name="MySql">MySql语句</param>
        /// <returns>数据集</returns>
        public  DataSet GetDataSet(string MySql)
        {
            conn = new MySqlConnection(_connString);
            DataSet ds = new DataSet();
            conn.Open();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(MySql, conn);
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
        /// <param name="MySql">MySql语句</param>
        /// <returns>数据表</returns>
        public  DataTable GetDataTable(string MySql)
        {
            conn = new MySqlConnection(_connString);
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(MySql, conn);
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
        /// <param name="MySql">MySql语句</param>
        /// <param name="MySqlParam">MySqlParameter参数数组</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string MySql, object[] MySqlParam)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            int i = 0;
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (MySqlParameter param in MySqlParam)
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
        /// <param name="MySql">MySql语句</param>
        /// <param name="MySqlParam">MySqlParameter参数数组</param>
        /// <returns>对像值</returns>
        public object ExecuteScalar(string MySql, object[] MySqlParam)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            object o;
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (MySqlParameter param in MySqlParam)
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
        /// 获取MySqlDataReader读取器
        /// </summary>
        /// <param name="MySql">该读取会一值保持链接</param>
        /// <param name="MySqlParam">MySqlParameter参数数组</param>
        /// <returns>MySqlDataReader读取器</returns>
        public void ExecuteReader(string MySql, object[] MySqlParam)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (MySqlParameter param in MySqlParam)
                    comm.Parameters.Add(param);
                MySqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
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
        /// <param name="MySql">MySql语句</param>
        /// <param name="MySqlParam">MySqlParameter参数数组</param>
        /// <returns>数据集</returns>
        public  DataSet GetDataSet(string MySql, object[] MySqlParam)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand MySqlCommand = new MySqlCommand(MySql, conn);
                MySqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (MySqlParameter param in MySqlParam)
                    MySqlCommand.Parameters.Add(param);
                MySqlDataAdapter da = new MySqlDataAdapter(MySqlCommand);
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
        /// <param name="MySql">MySql语句</param>
        /// <param name="MySqlParam">MySqlParameter参数数组</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string MySql, object[] MySqlParam)
        {
            conn = new MySqlConnection(_connString);
            conn.Open();
            DataTable dt = new DataTable();
            try
            {
               
                MySqlCommand MySqlCommand = new MySqlCommand(MySql, conn);
                MySqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (MySqlParameter param in MySqlParam)
                    MySqlCommand.Parameters.Add(param);
                MySqlDataAdapter da = new MySqlDataAdapter(MySqlCommand);
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
