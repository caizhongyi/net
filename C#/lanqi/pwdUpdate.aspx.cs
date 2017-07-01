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
public partial class pwdUpdate : System.Web.UI.Page
{
    public string username;
    public string xueli;
    int uid;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.SessionTest();
        userCenter u = Session["userinfo"] as userCenter;
        username = u.Username;
        xueli = u.Xueli;
        uid = ((userCenter)(Session["userinfo"])).Id;

    }

 
    protected void Button1_Click(object sender, EventArgs e)
    {
        string pwd = oldpwd.Value;
        string npwd = newpwd.Value;
        string rnpwd = renewpwd.Value;
        userCenter u = new userCenter();
        u.Id = uid;
        fun.getModel(u);

        if (fun.MD5(pwd)==u.Userpassword)
        {
            if (npwd == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('新密码不能为空')", true);
            }
            else if (npwd != rnpwd)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('2次密码输入不一致')", true);
            }
            else
            {
               
                u.Userpassword = fun.MD5(npwd);
                fun.DoSql(this, fun.update(u),Request.Url.ToString());

            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('原密码输入错误')", true);

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["userinfo"] = null;
        fun.AJAXalert("alert('已经成功退出');location='index.aspx'");
    }
}
