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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = fun.GetDataTable("select * from cultureType order by paixu asc");
        //for (int i = 1; i <= 14; i++)
        //{
        //    string str = "";
        //    HtmlGenericControl ul = this.FindControl("ul" + i) as HtmlGenericControl;
       
        //    foreach (DataRow dr in dt.Select(" sjid=" + i))
        //    {
        //        str += "<li><a href='fengji.aspx?id="+dr["id"]+"' >"+dr["type"]+"</a></li>";
          
        //    }
        //    ul.InnerHtml = str;
        //}
       fun.bind(carousel, "select top 20 * from user_product where typeid=34  order by paixu asc,id desc");
       
    }

  
}
