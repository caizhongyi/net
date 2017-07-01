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

public partial class newList : System.Web.UI.Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.bind(Repeater1, "select top 9 * from news_class2");
        int id = fun.getQueryInt("id");
        int sjid = fun.getQueryInt("sjid");
        if (id == 23)
        {
            xw.Visible = false;
            zj.Visible = true;
        }
        else if (id == 20)
        {
            xw.Visible = false;
            zj.Visible = false;
            yd.Visible = true;
        }
        else if (id == 5)
        {
            xw.Visible = false;
            zj.Visible = false;
            yd.Visible = false;
            DivNews.Visible = true;
            fun.bind(Repeater3, "select top 9 * from news_class where sjid="+ 5 +" order by id desc");
        }
        else
        {
           
            if (sjid != 0)
            {
                sql = "select * from news where sjid=" + sjid + " order by paixu asc,id desc";
                
            }
            else
            {
                sql = "select * from news where class1_id=" + id + " order by paixu asc,id desc";
               
            }
            
            fun.BindPage(sql, AspNetPager1, Repeater2, 33);
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage(sql, AspNetPager1, Repeater2, 33);
    }
}
