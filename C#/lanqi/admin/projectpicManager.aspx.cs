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

public partial class admin_projectpicManager : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            fun.BindPage("select project_pic.*,question.name,productguige.guige as pname from project_pic,question,productguige where project_pic.productid=productguige.id and project_pic.siteid=question.id   order by project_pic.id desc ", AspNetPager1, rpNews, 6);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select project_pic.*,question.name,user_product.name as pname from project_pic,question,user_product   order by project_pic.id desc ", AspNetPager1, rpNews, 6);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (fun.CheckStr(Id_str))
        {
            sql = "delete from  project_pic where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString(),"pic");
        }
        else
        {
            fun.AJAXalert(this, "请至少选择一项！");
        }
    }
}
