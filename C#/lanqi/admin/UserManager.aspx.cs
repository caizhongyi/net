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

public partial class admin_UserManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("usercenter");
        fun.bind(rpAdmin , "select * from usercenter  order by join_date desc", 6, Label1, Label2, start, prev, next, max);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null&&Id_str!="")
        {
            sql = "delete from  usercenter where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this,"alert('请至少选择一项！');location='UserManager.aspx'");
        }
    
    }
   
}
