using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.SQLAccess.Login
{
    /// <summary>
    /// 登陆类接口
    /// </summary>
    public interface ILogin
    {
        /// <summary>
        /// 登陆状态
        /// </summary>
        BaseLogin.LoginState LoginResoult { get; }
        /// <summary>
        /// 是否登陆
        /// </summary>
        bool IsLogin { get; }
        /// <summary>
        /// 用户信息
        /// </summary>
        UserInfo UserInfo { get; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        string UserId { get; }
        /// <summary>
        /// 用户是否登陆(非登陆用户跳转至主页)
        /// </summary>
        /// <param name="loginUrl">首页URL</param>
        /// <param name="currentUrl">当前页URL</param>
        void IsLoginAndRedirect(string loginUrl, string currentUrl);
        /// <summary>
        /// 清除Session["UserName"]和Session["UserID"]
        /// </summary>
        void LoginOut();
        /// <summary>
        /// 用户登陆并跳转
        /// </summary>
        /// <param name="p">当前页面</param>
        /// <param name="redirectUrl">跳转页面</param>
        /// <param name="userInfo">用户信息类</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="tableUserColumn">数据库用户列名</param>
        /// <param name="tablePwdColumn">数据库密码列名</param>
        /// <param name="tableParam">SQL条件 </param>
        /// <returns>成功返回True,错误返回False</returns>
        void LoginAndDirect(string redirectUrl, UserInfo userInfo, string tableName, string tableUserColumn, string tablePwdColumn, string tableParam);
        /// <summary>
        ///  用户登陆验证
        /// </summary>
        /// <param name="userInfo">UserInfo类</param>
        /// <param name="tableName">表名</param>
        /// <param name="tableUserColumn">用户列名</param>
        /// <param name="tablePwdColumn">密码列名</param>
        /// <param name="tableParam">参数</param>
        /// <returns>返回true为存在用户,false为不存在用户</returns>
        bool CheckUser(UserInfo userInfo, string tableName, string tableUserColumn, string tablePwdColumn, string tableParam);
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="p">当前页面</param>
        /// <param name="userInfo">用户信息类</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="tableUserColumn">数据库用户列名</param>
        /// <param name="tablePwdColumn">数据库密码列名</param>
        /// <param name="tableParam">SQL条件</param>
        /// <returns>成功返回True,错误返回False</returns>
        bool Login(UserInfo userInfo, string tableName, string tableUserColumn, string tablePwdColumn, string tableParam);
    }
}
