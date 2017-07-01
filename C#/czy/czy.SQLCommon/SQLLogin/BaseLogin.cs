using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Security.Cryptography;
using czy.MyDAL;
using czy.MyClass.Web;


namespace czy.SQLAccess.Login
{
    /// <summary>
    /// 事件参数
    /// </summary>
    public class LoginEventArgs : EventArgs
    {
        private UserInfo _userInfo;
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }
        public LoginEventArgs()
        { }
        public LoginEventArgs(UserInfo u)
        {
            this._userInfo = u;
        }
    }

    /// <summary>
    ///用户登陆类[非存储过程登陆] [默认为SQL数据链接,链接字符AppConfig key中的值]
    ///用户名 Session["UserInfo"]注:(UserInfo信息类只当注销后才重新读取;)
    ///cookie CurrentUserID
    ///cookie CurrentUserName
    ///cookie CurrentUserPass
    /// </summary>
    public abstract class BaseLogin :ILogin
    {
        #region 事件
        public delegate void EventHander(object o, LoginEventArgs e);
        /// <summary>
        /// 登陆前
        /// </summary>
        public event EventHander BeforeLogin;
        /// <summary>
        /// 登陆后
        /// </summary>
        public event EventHander AfterLogin;
        #endregion

        #region 枚举
        public enum LoginState
        {
            /// <summary>
            /// 登陆成功
            /// </summary>
            Sucess,
            /// <summary>
            /// 不存在用户
            /// </summary>
            NotExistUser,
            /// <summary>
            /// 不存在密码
            /// </summary>
            PasswordError,
            /// <summary>
            /// 登陆失败
            /// </summary>
            Fail,
            /// <summary>
            /// 无状态
            /// </summary>
            None
        }
        #endregion

        #region  私有成员
        //private string _cookieUserUserID;
        //private string _cookieUserName;
        //private string _cookieUserPassword;
        //private string _tableName;
        //private string _tableUserColumn;
        //private string _tablePwdColumn;
        //private string _tableParam;
        //private string _conn;
        protected UserInfo _userInfo;
        protected string _userName;
        protected string _userId;
        protected IDataBaseAdvance dba;
        protected IDataBase db;
        protected LoginState loginState = LoginState.None;
        #endregion

        #region 属性
        /// <summary>
        /// 登陆状态
        /// </summary>
        public LoginState LoginResoult
        {
            get { return loginState; }
        }
        /// <summary>
        /// 是否登陆
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return GetUserInfo() == null ? false : true;
            }
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                this._userInfo=GetUserInfo() == null ? null : (UserInfo)GetUserInfo();
                return this._userInfo;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                this._userName = GetUserInfo() == null ? null : ((UserInfo)GetUserInfo()).UserName;
                return this._userName;
            }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get
            {  
                this._userId = GetUserInfo() == null ? null : ((UserInfo)GetUserInfo()).UserId;
                return this._userId;
            }
        }
        #endregion

        #region  machineKey生成的算法
        /// <summary>
        /// machineKey生成的算法：
        /// </summary>
        /// <param name="len">20为(validationKey);24为(decryptionKey)</param>
        /// <returns></returns>
        public static string CreateKey(int len)
        {

            byte[] bytes = new byte[len];

            new RNGCryptoServiceProvider().GetBytes(bytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {

                sb.Append(string.Format("{0:X2}", bytes[i]));

            }

            return sb.ToString();

        }
        #endregion

        #region Cookes
        /// <summary>
        /// Cookie获取当前用户的用户ID
        /// </summary>
        public static string CookieUserUserID
        {
            get { return CookieDes.getCookie("CurrentUserID"); }
        }

        /// <summary>
        ///  Cookie获取当前用户的名
        /// </summary>
        public static string CookieUserName
        {
            get { return HttpUtility.UrlDecode(CookieDes.getCookie("CurrentUserName")); }
        }
        /// <summary>
        /// Cookie获取当前用户的密码
        /// </summary>
        public static string CookieUserPassword
        {
            get { return CookieDes.getCookie("CurrentUserPass"); }
        }
        /// <summary>
        /// 保存在Cookie中
        /// </summary>
        /// <param name="userInfo">UserInfo类</param>
        /// <param name="day">保存时间</param>
        public static void SetCookie(UserInfo userInfo, int day)
        {
            CookieDes.setCookie("CurrentUserID", userInfo.UserId, day);
            CookieDes.setCookie("CurrentUserName", userInfo.UserName, day);
            CookieDes.setCookie("CurrentUserPass", userInfo.Pwd, day);
        }
        #endregion

        #region 初始化
        /// <summary>
        /// LoginHelper类
        /// </summary>
        public BaseLogin()
        {

        }
        
     
        #endregion

        #region  判断是否登陆
        /// <summary>
        /// 用户是否登陆(非登陆用户跳转至主页)
        /// </summary>
        /// <param name="loginUrl">首页URL</param>
        /// <param name="currentUrl">当前页URL</param>
        public void IsLoginAndRedirect(string loginUrl, string currentUrl)
        {
            if (this.GetUserInfo() == null)
            {
                HttpContext.Current . Response.Redirect(loginUrl+"?url="+currentUrl);
            }
        }
        #endregion

        #region 记录用户Session
        /// <summary>
        /// 记录用户Session
        /// </summary>
        /// <param name="user">UserInfo类</param>
        private  void SaveUser( UserInfo user)
        {
            HttpContext.Current.Session["UserInfo"] = user;
        }
        #endregion

        #region 清除Session["UserName"]和Session["UserID"]
        /// <summary>
        /// 清除Session["UserName"]和Session["UserID"]
        /// </summary>
        public  void LoginOut()
        {
            HttpContext.Current.Session["UserInfo"] = null;

        }
        #endregion

        #region  获取用户信息Session["UserInfo"]
        /// <summary>
        /// 获取用户信息Session["UserInfo"]
        /// </summary>
        /// <returns>返回UserInfo类</returns>
        private   UserInfo GetUserInfo()
        {
            if (HttpContext.Current .Session["UserInfo"] != null)
                return (UserInfo)HttpContext.Current.Session["UserInfo"];
            else
                return  null;
        }
      
        #endregion

        #region  用户登陆验证
        /// <summary>
        ///  用户登陆验证
        /// </summary>
        /// <param name="userInfo">UserInfo类</param>
        /// <param name="tableName">表名</param>
        /// <param name="tableUserColumn">用户列名</param>
        /// <param name="tablePwdColumn">密码列名</param>
        /// <param name="tableParam">参数</param>
        /// <returns>返回true为存在用户,false为不存在用户</returns>
        public bool CheckUser(UserInfo userInfo, string tableName, string tableUserColumn, string tablePwdColumn, string tableParam)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = string.Empty;
                string qurey = string.Format(" select {0} from {1} where {2}=N'{3}'", tablePwdColumn, tableName, tableUserColumn, userInfo.UserName.Trim());
                sql = tableParam == string.Empty ? qurey : qurey + " and " + tableParam;
                ds = dba == null ? db.GetDataSet(sql) : dba.GetDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string p = ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim();
                    if (p.Trim().ToLower() == userInfo.Pwd.Trim().ToLower())
                    {
                        loginState = LoginState.Sucess;
                        return true;
                    }
                    else
                    {
                        loginState = LoginState.PasswordError;
                        return false;
                    }
                }
                else
                {
                    loginState = LoginState.NotExistUser;
                    return false;
                }
            }
            catch(System.Exception ex)
            {

                loginState = LoginState.Fail;
                throw ex;
                
            }
            
        }

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
        public void LoginAndDirect(string redirectUrl, UserInfo userInfo, string tableName, string tableUserColumn, string tablePwdColumn, string tableParam)
        {
            if (CheckUser(userInfo,tableName, tableUserColumn, tablePwdColumn, tableParam))
            {
                SaveUser(userInfo);
                if (HttpContext.Current.Request.QueryString["url"] != null)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.QueryString["url"].ToString());
                }
                else
                {
                    HttpContext.Current.Response.Redirect(redirectUrl);
                }
            }
        }
   
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
        public  bool Login( UserInfo userInfo, string tableName, string tableUserColumn, string tablePwdColumn, string tableParam)
        {
            if (CheckUser(userInfo,tableName, tableUserColumn, tablePwdColumn, tableParam))
            {
                if (BeforeLogin != null) BeforeLogin(this, new LoginEventArgs());
                SaveUser(userInfo);
                if (AfterLogin != null) AfterLogin(this, new LoginEventArgs(userInfo));
                return true;
            }
            else
            {
                return false;
            }
        }
      
        #endregion

        #region 验证是否登陆
        /// <summary>
        /// 验证是否登陆并跳转
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="defaultPage">跳转页面</param>
        public   void IsLoginAndRedirect(string defaultPage)
        {
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                HttpContext.Current.Response.Redirect(defaultPage);
            }

        }
        #endregion

     
        //附参考的matchineKey配置：

        //<?xml version="1.0"?>

        //<configuration>

        //   <system.web>

        //     <machineKey validationKey="3FF1E929BC0534950B0920A7B59FA698BD02DFE8" decryptionKey="280450BB36319B474C996B506A95AEDF9B51211B1D2B7A77" decryption="3DES" validation="SHA1"/>

        //      </system.web>
      
        //</configuration>
    }


    /// <summary>
    /// 用户信息类
    /// </summary>
    public class UserInfo
    {
        private string _userId;
        /// <summary>
        /// ID
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        private string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _pwd;
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        private string _roleName;
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }

        private string _address;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _QQ;
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get { return _QQ; }
            set { _QQ = value; }
        }

        private string _tel;
        /// <summary>
        /// 手机
        /// </summary>
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        private string _email;
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _userType;
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
        private string _userAccount;
        
        /// <summary>
        /// 用户帐户
        /// </summary>
        public string UserAccount
        {
            get { return _userAccount; }
            set { _userAccount = value; }
        }

        private string _roleId;

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        private string _roleValue;

        /// <summary>
        /// 角色值
        /// </summary>
        public string RoleValue
        {
            get { return _roleValue; }
            set { _roleValue = value; }
        }
    }
}
