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

public partial class admin_Book : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("shopping");
        fun.bind(rpBook, "select * from shopping  order by id desc", 6, Label1, Label2, start, prev, next, max);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null)
        {
            sql = "delete from  shopping where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！'location='"+Request.Url.ToString()+"')");
        }


        fun.bind(rpBook, "select * from shopping  order by id desc", 6, Label1, Label2, start, prev, next, max);
    }
}
