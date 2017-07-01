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
using czy.SQLCommon.Login;
using BBL;



namespace mston
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    
        private int GetUserId(Models.UserInfo user)
        {
            return new int();
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

        private void UserLogin(Models.UserInfo user)
        {
            if (Session["ValidateCode"] != null)
            {
                if (CheckValidate(TxtCode.Text.Trim()))
                {

                    bool resoult = BBL.UserInfo.Login(TxtUsername.Text, TxtPassword.Text);
                    //switch (resoult)
                    //{
                    //    case cz.User.Login.BaseLogin.LoginState.NotExistUser: MyClass.JavaScriptHelper.ShowMsg(this,"用户不存在!");break;
                    //    case MyClass.User.Login.BaseLogin.LoginState.PasswordError: MyClass.JavaScriptHelper.ShowMsg(this, "密码错误!");break;
                    //    case MyClass.User.Login.BaseLogin.LoginState.Sucess: UpdateLoginTime(user); Response.Redirect("index.aspx"); break;
                    //    default: MyClass.JavaScriptHelper.ShowMsg(this, "登陆失败,您的帐户可能被禁用了!"); break;
                    //}
                    if (!resoult)
                    {
                        czy.MyClass.Web.JavaScript.Alert("用户名或密码错误！");
                    }
                    else
                    { Response.Redirect("Index.aspx"); }

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
        

        private bool UpdateLoginTime(Models.UserInfo user)
        {
            //string sql = string.Format("update userInfo set u_loginDate='{0}' where u_name='{1}'",
            //  DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss"),
            //    user.u_name);
            //MyDAL.IDataBase db = new MyDAL.SQLDataBase(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
            //return db.ExecuteNonQuery(sql) > 0 ? true : false;
            return false;
         
        }
        protected void btn_login_Click(object sender, EventArgs e)
        {
            Models.UserInfo user = new Models.UserInfo();
            user.u_name = TxtUsername.Text.Trim();
            user.u_pwd = czy.MyClass.Encrypt.MD5Encrypt.Md5Code(TxtPassword.Text.Trim());
            UserLogin(user);
        }
}
}
