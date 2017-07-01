using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }
    
   
    protected void Button1_Click(object sender, EventArgs e)
    {
     
        //BBL.IUserInfo UserInfoOperation = BBL.BBLFactory.GetUserInfo();
        string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(LoginPwd.Text.Trim (),"MD5");
       // Response.Write(pwd);
        //DataSet ds = UserInfoOperation.ChackLogin(LoginName.Text.Trim(), pwd);
        DataSet ds=
        if (ds.Tables[0].Rows.Count > 0)
        {

            FormsAuthentication.RedirectFromLoginPage(LoginName.Text.Trim(), true);
        }
        else
        {
            Response.Write("<script>alert('用户名或密码错误');</script>");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        LoginName.Text = "";
        LoginPwd.Text = "";
    }
}
