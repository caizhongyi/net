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

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        fun.bind(Repeater1, "select * from news_class2");
  
        
        if (!IsPostBack)
        {


          

            fun.BindPage("select * from ygfc", AspNetPager1, Repeater2, 16);

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        fun.BindPage("select * from ygfc", AspNetPager1, Repeater2, 16);
    }
}
