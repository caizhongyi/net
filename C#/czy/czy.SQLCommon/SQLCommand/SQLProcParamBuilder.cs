using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace czy.SQLAccess.CommandHelper
{
    /// <summary>
    /// SQL存储过程参数集合
    /// </summary>
    public sealed partial class SQLProcParamBuilder
    {
        /// <summary>
        /// 获取SQL存储过程参数集合
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <returns>SQL存储过程参数集合</returns>
        public static SqlParameter[] GetSQLParams(object DbTable)
        {
            Type t = DbTable.GetType();
            System.Reflection.PropertyInfo[] p = t.GetProperties();
            SqlParameter[] sqlParams = new SqlParameter[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                sqlParams[i] = new SqlParameter("@"+p[i].Name, p[i].GetValue(DbTable, null));
            }
            return sqlParams;
        }
        /// <summary>
        /// 获取SQL存储过程参数集合
        /// </summary>
        /// <param name="columns">数据库列名</param>
        /// <param name="values">数据库值</param>
        /// <returns>SQL存储过程参数集合</returns>
        public static SqlParameter[] GetSQLParams(string[] columns, object[] values)
        {
            SqlParameter[] sqlParams = new SqlParameter[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                sqlParams[i] = new SqlParameter("@" + columns[i], values[i]);
            }
            return sqlParams;
        }
    }
}
