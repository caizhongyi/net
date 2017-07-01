using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Company.BBL
{
    public class RolesInfo
    {
        public static DataSet Select()
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.RolesInfo(), "r_isdel='False'");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }
        public static int Insert(Company.Models.RolesInfo m_rolesInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_rolesInfo, "r_id");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Company.Models.RolesInfo m_rolesInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_rolesInfo, "r_id", "r_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }

        public static int Delete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("rolesInfo", new string[]{"r_isdel"}, new string[]{"'false'"}, "n_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Company.Models.RolesInfo(), "r_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
    }
}
