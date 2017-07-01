using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.sql
{
    public class BackUpDataBase
    {

        public static int SqlBackUp(string dataBaseName, string path)
        {
            string sql = string.Format("Backup database " + dataBaseName + " to disk = '{0}'", path); //MyTest数据库名称
            return Util.GetExecuteNonQuery(sql);
        }
    }
}
