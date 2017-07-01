using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace czy.BBL
{
    public class TypeInfo
    {
        public static DataSet Select()
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.TypeInfo(), "t_isdel='false' order by t_order asc");
            return Util.IDatabase.GetDataSet(sql);

        }

        public static DataSet Select(int tParentId)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.TypeInfo(), "t_parentId=" + tParentId + " and t_isdel='false' order by t_order asc");
            return Util.IDatabase.GetDataSet(sql);

        }
        public static int Insert(Models.TypeInfo m_typeInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_typeInfo, "t_id");
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Models.TypeInfo m_typeInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_typeInfo, "t_id", "t_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }

        public static int Delete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("TypeInfo", new string[]{"t_isdel"}, new string[]{"'false'"}, "t_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Models.TypeInfo(), "t_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
    }
}
