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
using Model;
public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string name = dlUsername.Value;
        string pwd = dlPassword.Value;

        if (!fun.CheckStr(name) || !fun.CheckStr(pwd))
        {
            Response.Write("<script>alert('用户名或密码不能为空');location='Login.aspx'</script>");
        }
        else
        {

            if (yzm.Value.ToLower() != Session["vcode"].ToString().ToLower())
            {
                Response.Write("<script>location='Login.aspx';alert('验证码错误');</script>");
            }
            else
            {
                name = fun.GetSafeStr(name);
                pwd = fun.GetSafeStr(pwd);

                fun.userlog("usercenter", name, pwd, "index.aspx", this);
               
            }
        }
        
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        string username = txtName.Value;
        string pwd = txtPwd.Value;
        string repwd = txtRePwd.Value;
        if (!fun.CheckStr(username)||!fun.CheckStr(pwd))
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('用户名或密码不能为空')", true);
        }
        else if (pwd != repwd)
        {
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('2次密码输入不一至')", true);
        }
        else if (fun.CheckName("usercenter","username",username))
        {
           
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('用户名已经存在')", true);
        }
        else
        {
            userCenter u = new userCenter();
            u.Username = username;
            u.Userpassword =fun.MD5( fun.GetSafeStr( pwd));
            fun.DoSqlAJAX(fun.insert(u));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('注册成功')", true);
        }

    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
