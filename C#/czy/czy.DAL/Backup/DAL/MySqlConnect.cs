using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace DAL
{
    public class MySqlConnect
    {
        //连接数据库
        public static MySqlConnection GetConn()
        {
            string ConnStr = System.Configuration.ConfigurationManager.AppSettings["MySqlConn"].ToString();
            MySqlConnection conn = new MySqlConnection(ConnStr);
            return conn;
        }
        //执行操作
        public static int GetExecuteNonQuery(string MySql)
        {
            MySqlConnection conn = GetConn();
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
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
            MySqlConnection conn = GetConn();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlTransaction trans = conn.BeginTransaction();
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
        public static int GetIntExecuteScalar(string MySql)
        {
            MySqlConnection conn = GetConn();
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
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
        public static string GetStrExecuteScalar(string MySql)
        {
            MySqlConnection conn = GetConn();
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
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
        public static MySqlDataReader GetExecuteReader(string MySql)
        {
            MySqlConnection conn = GetConn();
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(MySql, conn);
                MySqlDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
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
        public static DataSet GetDataSet(string MySql)
        {
            MySqlConnection conn = GetConn();
            conn.Open();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(MySql, conn);
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
        public static DataTable GetDataTable(string MySql)
        {
            MySqlConnection conn = GetConn();
            conn.Open();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(MySql, conn);
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
    }
}
