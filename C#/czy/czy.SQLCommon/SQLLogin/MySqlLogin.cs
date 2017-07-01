using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using czy.MyDAL;

namespace czy.SQLAccess.Login
{
    /// <summary>
    /// MySQL登陆类
    /// </summary>
    public  class MySqlLogin :BaseLogin
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="conn">链接字符窜</param>
        /// <param name="type">链接字符类型</param>
        public MySqlLogin(string conn, DataBase.ConnStringType type)
        {
             dba=new MySqlDataBase(conn,type);
        }
    }
}
