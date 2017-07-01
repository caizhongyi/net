using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        czy.MyClass.Web.Controls.DataPager dp = new czy.MyClass.Web.Controls.DataPager(50);
        //dp.CurrentPage =Convert .ToInt32( Request.QueryString["page"]);
        dp.ShowNumberLabel = true;
        Response.Write(dp.GetHTMLPager("Default2.aspx", ""));
        
        //czy.MyDAL.SQL.SQLBBLBuilder.CreateBBL("czy_database","server=127.0.0.1,1344;database=czy_database;uid=sa;pwd=123456;","BBL","Models",czy.MyDAL.DataBase.ConnStringType.String);
        // czy.MyDAL.SQL.SQLModelsBuilder.Create("czy_database","server=127.0.0.1,1344;database=czy_database;uid=sa;pwd=123456;",  "Models", czy.MyDAL.DataBase.ConnStringType.String,czy.MyDAL.SQL.SQLModelsBuilder.ApplicationType.Web);
    }
}
