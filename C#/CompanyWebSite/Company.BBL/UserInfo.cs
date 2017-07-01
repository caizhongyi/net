using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Company.BBL
{
    public class UserInfo
    {
        public static DataSet Select()
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.UserInfo(), "u_isdel='false'");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }
        public static int Insert(Company.Models.UserInfo m_userInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_userInfo, "u_id");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Company.Models.UserInfo m_userInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_userInfo, "u_id", "u_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }

        public static int Delete(int id)
        {

            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("UserInfo", new string[] { "u_isdel" }, new string[] { "'false'" }, "u_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Company.Models.UserInfo(), "u_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
    }
}
