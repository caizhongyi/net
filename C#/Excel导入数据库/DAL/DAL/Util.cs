using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class Util
    {
        //�������ݿ�
        public static  SqlConnection GetConn()
        {
            string ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnStr"].ToString();
            SqlConnection conn = new SqlConnection(ConnStr);
            return conn;
        }
        public static DataTable GetDataTable(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
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



        //ִ�в���
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
        //��ȡ�������β���
        public static int GetIntExecuteScalar(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                int i = Convert.ToInt32(comm.ExecuteScalar().ToString());
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
        //��ȡ���������Ͳ���
        public static double GetdoubleExecuteScalar(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                double i = Convert.ToDouble(comm.ExecuteScalar());
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
        //��ȡ�����ַ�������
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
        //ִ�ж�ȡ����
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
        //����һ�����ݼ�����
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, conn);
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

        #region GetAddressDataSet ͨ���洢���̽����ݲ�ѯ����
        /// <summary>
        /// ͨ���洢���̽����ݲ�ѯ����
        /// </summary>
        /// <param name="Name">�洢��������</param>
        /// <returns></returns>
        public DataSet GetAddressDataSet(string Name)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(Name,conn);
                comm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
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
        #endregion

        #region GetAddressDataSet ͨ���洢���̽����ݲ�ѯ����
        /// <summary>
        /// ͨ���洢���̽����ݲ�ѯ����
        /// </summary>
        /// <param name="Name">�洢������</param>
        /// <param name="ID">Ҫ���Ĳ���</param>
        /// <param name="PID">�洢�����в���������</param>
        /// <returns></returns>
        public DataSet GetAddressDataSet(string Name, int ID, string PID)
        {
            SqlConnection conn = GetConn();
            conn.Open();
            try
            {
                SqlCommand comm = new SqlCommand(Name, conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(PID,SqlDbType.Int);
                comm.Parameters[PID].Value = ID;
                SqlDataAdapter da = new SqlDataAdapter();
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
        #endregion

       
    }
}
