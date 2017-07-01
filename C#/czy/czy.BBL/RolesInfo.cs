using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace czy.BBL
{
    public class RolesInfo
    {
        public static DataSet Select()
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.RolesInfo(), "");
            return Util.IDatabase.GetDataSet(sql);

        }
        public static DataSet Select(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetSelectSQL(new Models.RolesInfo(),string .Format ( "r_id={0}",id));
            return Util.IDatabase.GetDataSet(sql);

        }
        public static int Insert(Models.RolesInfo m_rolesInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetInsertSQL(m_rolesInfo, "r_id");
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int Update(int id, Models.RolesInfo m_rolesInfo)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL(m_rolesInfo, "r_id", "r_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }

        public static int Delete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetUpdateSQL("rolesInfo", new string[]{"r_isdel"}, new string[]{"'false'"}, "n_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
        public static int RealDelete(int id)
        {
            string sql = czy.MyClass.CommandHelper.SQLCommandBuilder.GetDelSQL(new Models.RolesInfo(), "r_id=" + id);
            return Util.IDatabase.ExecuteNonQuery(sql);
        }
    }
}
