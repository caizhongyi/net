using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using czy.MyDAL;

namespace czy.SQLAccess.Login
{
    /// <summary>
    /// SQL登陆类
    /// </summary>
    public  class SqlLogin : BaseLogin
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="conn">链接字符窜</param>
        /// <param name="type">链接字符类型</param>
        public SqlLogin(string conn, DataBase.ConnStringType type)
        {
             dba=new czy.MyDAL.SQLDataBase(conn,type);
        }

    }
}
