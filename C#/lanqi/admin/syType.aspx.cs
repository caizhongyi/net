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

public partial class admin_syType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       

        if (!IsPostBack)
        {

            fun.BindPage("select * from news_class where sjid=5  ", AspNetPager2, rpNewClass2, 8);

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
      
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string name = TextBox2.Text;
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "类别名称不能为空");
        }
        else
        {
            name = fun.GetSafeStr(name);
            if (fun.CheckName("news_class", "class1", name))
            {
                fun.AJAXalert(this, "该类别已经存在");
            }
            else
            {

                string sql = string.Format("insert into news_class (class1,sjid) values ('{0}',{1})", name,5);
                fun.DoSql(this, sql, Request.Url.ToString());


            }
        }
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage("select * from news_class where sjid=5  ", AspNetPager2, rpNewClass2, 8);
    }
}
