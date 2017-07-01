using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.sql
{
    public class Login
    {
        /// <summary>
        /// 用户登陆sql
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="userCols">用户列名</param>
        /// <param name="pwdCols">密码列名</param>
        /// <param name="userId">用户ID</param>
        /// <param name="pwd">密码</param>
        /// <returns>true为成功,false为失败</returns>
        public static bool Login(string tableName,string userCols,string pwdCols,string userId, string pwd)
        {
            string sql = string.Format(" select {2} from {0} where {1}=N'{3}'", tableName, userCols, pwdCols, userId.Trim ());
            DataSet ds = Util.GetDataSet(sql);
            string p = ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim();
            if (p == pwd.Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
