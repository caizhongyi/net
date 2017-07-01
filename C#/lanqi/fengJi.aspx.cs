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

public partial class fengJi : System.Web.UI.Page
{
    int id = 0;
    int sjid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        fun.WriteMeta(this);
        fun.bind(Repeater2, "select * from news_class2");
        id=fun.getQueryInt("id");
        sjid=fun.getQueryInt("sjid");
        if (id != 0)
        {
            string name = fun.getById(id.ToString(),"id","culturetype","type");
            if (name.Contains("风光")||name.Contains("地图"))
            {
                lunbo.Visible = true;
                xw.Visible = false;
           
                fun.BindPage("select * from culture where typeid=" + id+" order by paixu asc", AspNetPager2, Repeater3, 10);
            }
            else
            {

                fun.BindPage("select * from culture where typeid=" + id + " order by paixu asc", AspNetPager1, Repeater1, 33);
            }
        }
        else
        {
            fun.BindPage("select * from culture where sjid=" + sjid + " order by paixu asc", AspNetPager1, Repeater1, 33);
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        if (id != 0)
        {
            fun.BindPage("select * from culture where typeid=" + id + " order by paixu asc", AspNetPager1, Repeater1, 33);
        }
        else
        {
            fun.BindPage("select * from culture where sjid=" + sjid + " order by paixu asc", AspNetPager1, Repeater1, 33);
        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from culture where typeid=" + id + " order by paixu asc", AspNetPager2, Repeater3, 10);
    }
}
