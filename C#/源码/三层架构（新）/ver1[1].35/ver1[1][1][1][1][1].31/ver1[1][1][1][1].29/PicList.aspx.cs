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

public partial class PicList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        string picId = Request.QueryString["picId"];

        if (picId == null)
        {
            string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "select top 1 * from Picture where AlbumsID=" + id + " order by ID desc";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.Image1.ImageUrl = "~/Upload/pic/" + dr["path"].ToString();
            }
        }
        else
        {
            string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "select  * from Picture where id=" + picId;
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.Image1.ImageUrl = "~/Upload/pic/" + dr["path"].ToString();
            }
        }

    }
}
