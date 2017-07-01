using System;
using System.Collections.Generic;
using System.Text;

namespace czy.MyDAL.SQL
{
    public sealed partial class BackUpDataBase 
    {
        static IDataBaseAdvance db;
        /// <summary>
        /// 备份数据库脚本
        /// </summary>
        /// <param name="dataBaseName">数据库名称</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static int SqlBackUp(string dataBaseName, string path,string connstr,czy.MyDAL.DataBase.ConnStringType type)
        {
            db = new SQLDataBase(connstr, type);
            string sql = string.Format("Backup database " + dataBaseName + " to disk = '{0}'", path); //MyTest数据库名称
            return db.ExecuteNonQuery(sql); 
        }
    }
}
