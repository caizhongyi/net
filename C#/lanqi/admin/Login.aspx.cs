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

public partial class admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
       
        }
            

        
    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {

        string name = Request.Form["username"];
        string pwd=Request.Form["pwd"];

        if (!fun.CheckStr(name) || !fun.CheckStr(pwd))
        {
            Response.Write("<script>alert('用户名或密码不能为空');location='Login.aspx'</script>");
        }
        else
        {

            if (Request.Form["yzm"].ToLower() != Session["vcode"].ToString().ToLower())
            {
                Response.Write("<script>location='Login.aspx';alert('验证码错误');</script>");
            }
            else
            {
                name = fun.GetSafeStr(name);
                pwd = fun.GetSafeStr(pwd);

                fun.log("admin",name,pwd,"index.aspx",this);
            }
        }
        
        
    }
}
