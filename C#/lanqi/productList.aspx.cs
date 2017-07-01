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

public partial class productList : System.Web.UI.Page
{
    int id = 0;
    public  int sjid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
    //    fun.bind(Repeater1,"select top 9 * from user_producttype3");
fun.bind(Repeater1,"select top 9 * from news_class2");
        id = fun.getQueryInt("id");
        sjid = fun.getQueryInt("sjid");
        string sql = "";
        if (!IsPostBack)
        {
            fun.bind(DropDownList1,"select * from user_producttype2","type","id","È«²¿");
            if (id == 0)
            {
              sql= "select * from user_product where sjid=" + sjid + " order by paixu asc ,id desc";
            }
            else
            {
                sql = "select * from user_product where sjid=" + sjid + " and typeid=" + id + " order by paixu asc ,id desc";
            }

            Session["sql"] = sql;

            fun.BindPage(Session["sql"].ToString(), AspNetPager1, Repeater2, 32);

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        fun.BindPage(Session["sql"].ToString(), AspNetPager1, Repeater2, 32);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int typeid2 =int.Parse( DropDownList1.SelectedValue);


     
        id = fun.getQueryInt("id");
        sjid = fun.getQueryInt("sjid");
        string sql = "";
      
            if (id == 0)
            {
                sql = "select * from user_product where sjid=" + sjid ;
            }
            else
            {
                sql = "select * from user_product where sjid=" + sjid + " and typeid=" + id ;
            }

           

        if (typeid2 != 0)
        {
          
            sql += " and typeid2=" + typeid2;
        }

        sql += " order by paixu asc ,id desc";

        Session["sql"] = sql;
        AspNetPager1.CurrentPageIndex = 1;
        fun.BindPage(Session["sql"].ToString(), AspNetPager1, Repeater2, 32);
    }
}
