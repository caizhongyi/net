using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyClass.CommandHelper;



namespace mston
{
    public partial class Login : System.Web.UI.Page
    {

        MyClass.User.Login.ILogin ilogin = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    
        private int GetUserId(Company.Models.UserInfo user)
        {
            string sql =string .Format ("select u_id from userInfo where u_name=N'{0}'",user.u_name);
            return Convert .ToInt32( T_BasePage.DB.ExecuteScalar(sql));
        }
        private bool CheckValidate(string validate)
        {
            if (Session["ValidateCode"]!=null && Session["ValidateCode"].ToString () == validate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UserLogin(Company.Models.UserInfo user)
        {
            // if (MyClass.User.UserLogin.CheckLogin(user.UserName, user.UserPwd, this))
            //  {
            MyClass.User.Login.UserInfo userInfo = new MyClass.User.Login.UserInfo();
            if (Session["ValidateCode"] != null)
            {
                if (CheckValidate(TxtCode.Text.Trim()))
                {

                    userInfo.UserName = user.u_name;
                    userInfo.Pwd = user.u_pwd;
                    MyClass.User.Login.ILogin login = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
                    bool isLogin = login.Login(userInfo, "UserInfo", "u_name", "u_pwd", "");

                    switch (login.LoginResoult)
                    {
                        case MyClass.User.Login.BaseLogin.LoginState.NotExistUser: MyClass.JavaScriptHelper.ShowMsg(this,"用户不存在!");break;
                        case MyClass.User.Login.BaseLogin.LoginState.PasswordError: MyClass.JavaScriptHelper.ShowMsg(this, "密码错误!");break;
                        case MyClass.User.Login.BaseLogin.LoginState.Sucess: UpdateLoginTime(user); Response.Redirect("index.aspx"); break;
                        default: MyClass.JavaScriptHelper.ShowMsg(this, "登陆失败,您的帐户可能被禁用了!"); break;
                    }

                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('验证码错误!')</script>");
                }

                //  }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        

        private bool UpdateLoginTime(Company.Models.UserInfo user)
        {
            string sql = string.Format("update userInfo set u_loginDate='{0}' where u_name='{1}'",
              DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss"),
                user.u_name);
            MyDAL.IDataBase db = new MyDAL.SQLDataBase(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            return db.ExecuteNonQuery(sql) > 0 ? true : false;
         
        }
        protected void btn_login_Click(object sender, EventArgs e)
        {
            Company.Models.UserInfo user = new Company.Models.UserInfo();
            user.u_name = TxtUsername.Text.Trim();
            user.u_pwd = MyClass.Encrypt.MD5Encrypt.Md5Code(TxtPassword.Text.Trim());
            UserLogin(user);
        }
}
}
