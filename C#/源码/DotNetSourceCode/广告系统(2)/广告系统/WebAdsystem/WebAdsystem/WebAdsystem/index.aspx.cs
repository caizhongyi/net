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
using BBL.Inface;
using BBL;
using DAL.Model;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }

    IUserInfo userinfo = BBLFactory.GetUserInfo();
    protected void Button1_Click(object sender, EventArgs e)
    {
        UserInfo ui = new UserInfo();
        ui.User_id=LoginName.Text;
        ui.User_pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(LoginPwd.Text.Trim (),"MD5");
        if (userinfo.UserLogin(ui))
        {

            FormsAuthentication.RedirectFromLoginPage(LoginName.Text.Trim(), true);
            Session["userName"] = ui.User_id;
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
