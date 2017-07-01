using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;


/// <summary>
/// Utils 的摘要说明
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
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static SqlConnection CreateConnection()
        {
            string connStr = ConfigurationSettings.AppSettings["connStr"];//数据源

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
