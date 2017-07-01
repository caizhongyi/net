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
        /// DataRead��ȡ���¼�
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
        /// ��ʼ��
        /// </summary>
        /// <param name="contectString">>�����ַ���</param>
        /// <param name="type">�����ַ�����</param>
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
        /// ��ʼ��
        /// </summary>
        /// <param name="contectString">>�����ַ���</param>
        /// <param name="type">�����ַ�����</param>
        public SQLDataBase(string server, string dataBase, string user, string pwd)
        {
            _connType = ConnStringType.String;
            _connString = string.Format("server={0};database={1};uid={2};pwd={3}",server,dataBase,user,pwd);
           

        }

        #region  �Ǵ洢����
        /// <summary>
        /// ִ�в���
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>��Ӱ�������</returns>
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
        /// ��ȡ���е���ֵ
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>����ֵ</returns>
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
        /// ��ȡSqlDataReader��ȡ��
        /// </summary>
        /// <param name="sql">�ö�ȡ��һֵ��������</param>
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
        /// ����һ�����ݼ�����
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>���ݼ�</returns>
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
        //  ����һ�����ݱ�
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <returns>���ݱ�</returns>
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

        #region  �洢����
        /// <summary>
        /// ִ�в���
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="sqlParam">SqlParameter��������</param>
        /// <returns>��Ӱ�������</returns>
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
        /// ��ȡ���е���ֵ
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParam">SqlParameter��������</param>
        /// <returns>����ֵ</returns>
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
        /// ��ȡSqlDataReader��ȡ��
        /// </summary>
        /// <param name="sql">�ö�ȡ��һֵ��������</param>
        /// <param name="sqlParam">SqlParameter��������</param>
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
        /// ����һ�����ݼ�����
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="sqlParam">SqlParameter��������</param>
        /// <returns>���ݼ�</returns>
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
        //  ����һ�����ݱ�
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="sqlParam">SqlParameter��������</param>
        /// <returns>���ݱ�</returns>
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
