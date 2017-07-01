using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Company.BBL
{
    public class TypeInfo
    {
        public static DataSet Select()
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.TypeInfo(), "t_isdel='false' order by t_order asc");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }

        public static DataSet Select(int tParentId)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Company.Models.TypeInfo(), "t_parentId=" + tParentId + " and t_isdel='false' order by t_order asc");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.GetDataSet(sql);

        }
        public static int Insert(Company.Models.TypeInfo m_typeInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_typeInfo, "t_id");
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Company.Models.TypeInfo m_typeInfo)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_typeInfo, "t_id", "t_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }

        public static int Delete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("TypeInfo", new string[]{"t_isdel"}, new string[]{"'false'"}, "t_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Company.Models.TypeInfo(), "t_id=" + id);
            MyDAL.IDataBase idb = new MyDAL.SQLDataBase(Util.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return idb.ExecuteNonQuery(sql);
        }
    }
}
