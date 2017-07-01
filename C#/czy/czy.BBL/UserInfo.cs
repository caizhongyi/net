using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace czy.BBL
{
    public class UserInfo
    {
        public static DataSet Select()
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.UserInfo(), "u_isdel='false'");
            return Util.IDatabase.GetDataSet(sql);

        }
        public static int Insert(Models.UserInfo m_userInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_userInfo, "u_id");
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Models.UserInfo m_userInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_userInfo, "u_id", "u_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }

        public static int Delete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("UserInfo", new string[] { "u_isdel" }, new string[] { "'false'" }, "u_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Models.UserInfo(), "u_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static bool login(string user,string pwd)
        {
            return Util.ILogin.Login(new czy.SQLCommon.Login.UserInfo() {  UserName=user,Pwd=pwd}, "UserInfo", "u_name", "u_pwd", "");
        }
    }
}
