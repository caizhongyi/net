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

public partial class admin_questionmanager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("question");
        fun.bind(rpAdmin, "select question.*,usercenter.*,questionstate.*,questionstate.state as sname from question,usercenter,questionstate where userid=usercenter.id and question.state=questionstate.id   order by question.id desc", 6, Label1, Label2, start, prev, next, max);
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null && Id_str != "")
        {
            sql = "delete from  question where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！');location='"+Request.Url.ToString()+"'");
        }
    }
}
