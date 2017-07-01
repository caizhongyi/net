using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;


/// <summary>
/// Utils ��ժҪ˵��
/// </summary>
/// 
namespace DAL
{
    public class Utils
    {
        public static string USERNAME = "UserName";
        public static string POWER = "power";
        public Utils()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public static SqlConnection CreateConnection()
        {
            string connStr = ConfigurationSettings.AppSettings["connStr"];//����Դ

            SqlConnection conn = new SqlConnection(connStr);

            return conn;
        }

        public static int AddBySql(string sql)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}
