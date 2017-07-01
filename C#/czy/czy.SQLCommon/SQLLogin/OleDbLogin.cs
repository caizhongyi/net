using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using czy.MyDAL;

namespace czy.SQLAccess.Login
{
    /// <summary>
    /// OleDb登陆类
    /// </summary>
    public class OleDbLogin : BaseLogin
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="conn">链接字符窜</param>
        /// <param name="type">链接字符类型</param>
        public OleDbLogin(string conn,DataBase.ConnStringType type)
        {
             db=new OleDbDataBase(conn,type);
           
        }
    }
}
