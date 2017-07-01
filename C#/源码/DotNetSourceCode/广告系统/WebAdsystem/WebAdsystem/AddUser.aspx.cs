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

public partial class AddUser1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    BBL.IUserInfo UserInfoOperation = BBL.BBLFactory.GetUserInfo();
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            string pwd=FormsAuthentication.HashPasswordForStoringInConfigFile(UserPwd.Text.Trim (),"MD5");
            int i = UserInfoOperation.InsertUserInfo(UserName.Text.Trim () ,LoginName.Text.Trim () ,pwd ,Convert .ToInt32( Right .Text.Trim ()),ReMark .Text.Trim () );
            if (i > 0)
            {
              Response.Write("<script javascript:language>alert('增加成功')</script>");
              Response.Redirect("WbUserManger.aspx");
                
            }
            else
            {
                Response.Write("<script javascript:language>alert('增加失败')</script>");
            }

            // Panel1.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Write("<script javascript:language>alert('" + ex.Message + "')</script>");
        }
    }
}
