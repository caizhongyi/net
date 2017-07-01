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
        public SqlConnection GetConn()
        {
            string ConnStr =System.Configuration.ConfigurationManager.AppSettings["ConnStr"].ToString();
            SqlConnection conn = new SqlConnection(ConnStr);
            return conn;
        }
        //ִ�в���
        public int GetExecuteNonQuery(string sql)
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
        public int GetIntExecuteScalar(string sql)
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
        //��ȡ�����ַ�������
        public string GetStrExecuteScalar(string sql)
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
        public SqlDataReader GetExecuteReader(string sql)
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
        public DataSet GetDataSet(string sql)
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
    }
}
