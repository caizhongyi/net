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

public partial class syDetail : System.Web.UI.Page
{
    int id=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.bind(Repeater1, "select * from news_class2");
        id = fun.getQueryInt("id");
     


        fun.BindPage("select * from news where sjid=" + id, AspNetPager1, Repeater2, 10);


    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from news where sjid=" + id, AspNetPager1, Repeater2, 10);
    }
}
