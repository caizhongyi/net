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
public partial class liuyan : System.Web.UI.Page
{
    public string username;
    public string xueli;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.SessionTest();
        userCenter u = Session["userinfo"] as userCenter;
        username = u.Username;
        xueli = u.Xueli;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string lyContent =fun.GetSafeStr( txtContent.Value);
        string lyTitle =fun.GetSafeStr( txtTitle.Value);
        if (!fun.CheckStr(lyTitle) || !fun.CheckStr(lyContent))
        {
           
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('留言标题和内容不能为空')", true);
        }
        else
        {
            string sql =string.Format( "insert into question (name,question_content) values ('{0}','{1}')",lyTitle,lyContent);
            fun.DoSql(this,sql,Request.Url.ToString());
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["userinfo"] = null;
        fun.AJAXalert("alert('已经成功退出');location='index.aspx'");
    }
}
