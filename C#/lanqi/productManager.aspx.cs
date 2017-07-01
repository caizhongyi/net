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
public partial class productManager : System.Web.UI.Page
{
    int id = 0;
    public string username;
    public string xueli;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.SessionTest();
        userCenter u = Session["userinfo"] as userCenter;
        username = u.Username;
        xueli = u.Xueli;
        id = u.Id;

        fun.BindPage("select user_product.*,user_producttype3.*,user_product.id as pid from user_product,user_producttype3 where typeid=user_producttype3.id and userid=" + id + " order by user_product.id desc", AspNetPager1, Repeater1, 8);
        

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        fun.BindPage("select user_product.*,user_producttype3.*,user_product.id as pid from user_product,user_producttype3 where typeid=user_producttype3.id and userid=" + id + " order by user_product.id desc", AspNetPager1, Repeater1, 8);
     }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["userinfo"] = null;
        fun.AJAXalert("alert('已经成功退出');location='index.aspx'");
    }
}
