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
using System.Data.SqlClient;

public partial class AlbumnList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断用户是否已经登录，如果没有则自动跳转到首页，该页面不允许非登录用户操作
        if (Session["UserName"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        string id = Request.QueryString["id"];
        string action = Request.QueryString["action"];

        if (action == "del")
        {
            string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = "Delete  From Albums Where ID="+id;
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            conn.Close();

        }
    }


   
}
