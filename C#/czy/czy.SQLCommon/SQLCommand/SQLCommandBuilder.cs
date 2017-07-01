using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace czy.SQLAccess.CommandHelper
{
    /// <summary>
    /// 生成SQL语句
    /// </summary>
    public sealed partial class SQLCommandBuilder
    {
        /// <summary>
        /// 对像类名
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <returns></returns>
        private static string GetName(object DbTable)
        {
            Type t = DbTable.GetType();
            return t.FullName.Substring(t.FullName.LastIndexOf('.')+1);
        }

        #region sql插入字符窜
        /// <summary>
        /// sql插入字符窜
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <param name="id">自增ID,如果为null则无自增ID</param>
        /// <returns>返回sql插入字符窜</returns>
        public static string GetInsertSQL(object DbTable,string id)
        {
            Type t = DbTable.GetType();
            int i = 0;
            string clostr = string.Empty;
            foreach (System.Reflection.PropertyInfo p in t.GetProperties())
            {
                if (p.Name.Trim().ToLower() != id.Trim().ToLower())
                {
                    if (i == 0)
                    {
                        clostr +=CheckColumnName( p.Name);
                    }
                    else
                    {
                        clostr += "," + CheckColumnName(p.Name);
                    }
                    i++;
                }
            }
            i = 0;
            string paramstr = string.Empty;
            foreach (System.Reflection.PropertyInfo p in t.GetProperties())
            {
                if (p.Name.Trim().ToLower() != id.Trim().ToLower())
                {
                   
                    if (i == 0)
                    {
                        if (p.GetValue(DbTable, null) != null)
                        { 
                            string temp=p.GetValue(DbTable, null).ToString().ToSQLSafe();
                            temp=temp == "0001/1/1 0:00:00" ? DateTime.Now.ToString():temp;
                            paramstr += string .Format ("'{0}'",temp);
                        }
                        else
                        {
                            paramstr += @"''";
                        }
                        
                    }
                    else
                    {
                        if (p.GetValue(DbTable, null) != null)
                        {
                            string temp=p.GetValue(DbTable, null).ToString().ToSQLSafe();
                            temp=temp == "0001/1/1 0:00:00" ? DateTime.Now.ToString():temp;
                            paramstr += string .Format (",'{0}'",temp);
                        }
                        else
                        {
                            paramstr +="," + @"''";
                        }

                    }
                    i++;
                }
            }
            string cmd = string.Format("insert into {0}({1}) values({2})", GetName(DbTable), clostr, paramstr);
            return cmd;
        }
        #endregion

        #region sql修改字符窜
        /// <summary>
        /// sql修改字符窜
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <param name="id">自增ID,如果为null则无自增ID</param>
        /// <param name="filter">条件</param>
        /// <returns>返回sql修改字符窜</returns>
        public static string GetUpdateSQL(object DbTable,string id, string filter)
        {
            Type t = DbTable.GetType();
            System.Reflection.PropertyInfo[] p = t.GetProperties();
            string upstr = string.Empty;
            StringBuilder sb = new StringBuilder();
            int itemCount = 0;
            for (int i = 0; i < p.Length ; i++)
            {
                if (p[i].Name.Trim().ToLower() != id.ToString().Trim().ToLower())
                {
                    if (itemCount == 0)
                    {
                        if(p[i].GetValue(DbTable, null)!=null)
                        {
                            string value = CheckType(p[i]) == ColumnType.String ?
                                "'" + p[i].GetValue(DbTable, null).ToString().ToSQLSafe() + "'": p[i].GetValue(DbTable, null).ToString().ToSQLSafe();
                            string s = string.Format("{0}={1}", CheckColumnName(p[i].Name), value);
                            upstr += s;
                        }
                    }
                    else
                    {
                        if (p[i].GetValue(DbTable, null) != null)
                        {
                            string value = CheckType(p[i]) == ColumnType.String ?
                                "'" + p[i].GetValue(DbTable, null).ToString().ToSQLSafe() + "'": p[i].GetValue(DbTable, null).ToString().ToSQLSafe();
                            string s = string.Format(",{0}={1}", CheckColumnName(p[i].Name), value);
                            upstr += s;
                        }

                    }
                    itemCount++;
                }
            }

            sb.AppendFormat("update {0} set {1} ", GetName(DbTable), upstr);
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        #endregion

        #region sql删除字符窜
        /// <summary>
        /// 删除字符窜
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <param name="filter">条件</param>
        /// <returns>返回删除字符窜</returns>
        public static string GetDelSQL(object DbTable, string filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from {0}", GetName(DbTable));
            if(filter.Length>0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        #endregion

        #region 获取sql查询字符窜
        /// <summary>
        /// 获取sql查询字符窜
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <param name="filter">条件</param>
        /// <returns>返回sql查询字符窜</returns>
        public static string GetSelectSQL(object DbTable, string filter)
        {
            Type t = DbTable.GetType();
            System.Reflection.PropertyInfo[] p = t.GetProperties();
            string upstr = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(" * ");
            sb.Append(" from " + GetName(DbTable) + " ");
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取sql查询字符窜
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <param name="columns">列名</param>
        /// <param name="filter">条件</param>
        /// <returns>返回sql查询字符窜</returns>
        public static string GetSelectSQL(object DbTable, string[] columns, string filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(string.Join(",", columns));
            sb.Append(" from " + GetName(DbTable) + " ");
            sb.Append(filter);
            return sb.ToString();
        }

        /// <summary>
        /// 获取sql查询字符窜
        /// </summary>
        /// <param name="DbTable">对像模型</param>
        /// <param name="columnParams">列参数</param>
        /// <param name="filter">条件</param>
        /// <returns>返回sql查询字符窜</returns>
        public static string GetSelectSQL(object DbTable,string columnParams, string filter)
        {
            Type t = DbTable.GetType();
            System.Reflection.PropertyInfo[] p = t.GetProperties();
            string upstr = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(columnParams);
            sb.Append(" from " + GetName(DbTable) + " ");
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        #endregion

        #region sql插入字符窜
        /// <summary>
        /// sql插入字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colsArgs">列名</param>
        /// <param name="paramArgs">值,[N'']..</param>
        /// <returns>返回sql插入字符窜</returns>
        public static string GetInsertSQL(string tableName, string[] colsArgs, string[] paramArgs)
        {

            int i = 0;
            string clostr = string.Empty;
            foreach (string col in colsArgs)
            {
                if (i == 0)
                {
                    clostr += CheckColumnName(col);
                }
                else
                {
                    clostr += "," +CheckColumnName(col);
                }
                i++;
            }
            i = 0;
            string paramstr = string.Empty;
            foreach (string param in paramArgs)
            {

                if (i == 0)
                {
                    paramstr += param;
                }
                else
                {
                    paramstr += "," + param + "";
                }
                i++;
            }
            string cmd = string.Format("insert into {0}({1}) values({2})", tableName, clostr, paramstr);
            return cmd;
        }



        #endregion

        #region sql修改字符窜
        /// <summary>
        /// sql修改字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colsArgs">列名</param>
        /// <param name="paramArgs">参数</param>
        /// <param name="condtion">条件 </param>
        /// <returns>返回sql修改字符窜</returns>
        public static string GetUpdateSQL(string tableName, string[] colsArgs, string[] paramArgs, string filter)
        {
            StringBuilder sb = new StringBuilder();
            string upstr = string.Empty;
            for (int i = 0, j = 0; i < colsArgs.Length && j < paramArgs.Length; i++, j++)
            {
                if (i == 0)
                {
                    string p = string.Format("{0}='{1}'",CheckColumnName(colsArgs[i]), paramArgs[j]);
                    upstr += p;
                }
                else
                {

                    string p = string.Format(",{0}='{1}'", CheckColumnName(colsArgs[i]), paramArgs[j]);
                    upstr += p;

                }
            }

            sb.AppendFormat("update {0} set {1} ", tableName, upstr);
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        #endregion

        #region sql删除字符窜
        /// <summary>
        /// 删除字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="conditon">条件</param>
        /// <returns>返回删除字符窜</returns>
        public static string GetDelSQL(string tableName, string filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from {0} ", tableName);
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        #endregion

        #region 获取sql查询字符窜
        /// <summary>
        /// 获取sql查询字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="closArgs">列名</param>
        /// <param name="condtion">条件</param>
        /// <returns>返回sql查询字符窜</returns>
        public static string GetSelectSQL(string tableName, string[] closArgs, string filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(string.Join(",", closArgs));
            sb.Append(" from " + tableName + " ");
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取sql查询字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filter">条件</param>
        /// <returns>返回sql查询字符窜</returns>
        public static string GetSelectSQL(string tableName,  string filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(" * ");
            sb.Append(" from " + tableName + " ");
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取sql查询字符窜
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnParams">列参数</param>
        /// <param name="filter">条件</param>
        /// <returns>返回sql查询字符窜</returns>
        public static string GetSelectSQL(string tableName,string columnParams, string filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(columnParams);
            sb.Append(" from " + tableName + " ");
            if (filter.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(filter);
            }
            return sb.ToString();
        }
        #endregion

        private enum ColumnType
        {
            String,
            Numberic
        }

        private static ColumnType CheckType(PropertyInfo p)
        {
            return System.Type.GetTypeCode(p.GetType()) == System.TypeCode.Decimal 
                || System.Type.GetTypeCode(p.GetType()) == System.TypeCode.Double
                || System.Type.GetTypeCode(p.GetType()) == System.TypeCode.Int32
            ? ColumnType.Numberic : ColumnType.String;
        }

        private static object CheckColumnName(object columnName)
        {
            string[] names = new string[] { "date", "percent", "number", "type", "name", "count" };
            foreach (string name in names)
            {
                if (name == columnName.ToString().ToLower())
                {
                    columnName = string.Format("[{0}]", columnName.ToString());
                }
            }
            return columnName;
        }
    }
}

