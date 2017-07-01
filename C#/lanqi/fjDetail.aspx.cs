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

public partial class fjDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.bind(Repeater1, "select * from news_class2");
        int id = fun.getQueryInt("id");
        DataTable dt = fun.GetDataTable("select * from culture where id=" + id);
        if (dt.Rows.Count > 0)
        {

            fun.DoSqlAJAX("update culture set hot=hot+1 where id="+id);

            fun.bind(Repeater2,dt);
        }
            
        

     
    }
}
