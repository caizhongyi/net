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

public partial class admin_modelManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      //  fun.bind(rpAdmin, "select * from modelyp  order by join_date desc", 6, Label1, Label2, start, prev, next, max);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (Request["ch"] != null && Id_str != "")
        {
            sql = "delete from  modelyp where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString(),"gerenpic","shenghuopic","yishupic");
        }
        else
        {
            fun.AJAXalert(this, "alert('请至少选择一项！');location='modelManager.aspx'");
        }
    }
}
