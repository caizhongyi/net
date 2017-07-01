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
        //�������ݿ�
        public static  SqlConnection GetConn()
        {
            string ConnStr =System.Configuration.ConfigurationManager.AppSettings["ConnStr"].ToString();
            SqlConnection conn = new SqlConnection(ConnStr);
            return conn;
        }


        #region  �Ǵ洢����
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
        /// <summary>
        /// ���з�����ִ��һ��Sql��䡣
        /// </summary>
        /// <param name="SqlStrings">Sql�����</param>
        /// <returns>0Ϊʧ��,1Ϊ�ɹ�</returns>
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
        //��ȡ�������β���
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
                conn.Close();
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
        //����һ�����ݱ�
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
        /// ���з�������һ�����ݱ��в���һ����¼��
        /// </summary>
        /// <param name="TableName">����</param>
        /// <param name="Cols">��������ֵΪ�ֶ�����ֵΪ�ֶ�ֵ</param>
        /// <returns>�Ƿ�ɹ�</returns>
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
        /// ���з���������һ�����ݱ�
        /// </summary>
        /// <param name="TableName">����</param>
        /// <param name="Cols">��������ֵΪ�ֶ�����ֵΪ�ֶ�ֵ</param>
        /// <param name="Where">Where�Ӿ�</param>
        /// <returns>�Ƿ�ɹ�</returns>
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



        #region  �洢����
        //ִ�в���
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
        //��ȡ�������β���
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
        //��ȡ�����ַ�������
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
        //ִ�ж�ȡ����
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
        //����һ�����ݼ�����
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
        //����һ�����ݱ�
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
