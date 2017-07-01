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

public partial class AlbumnEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断用户是否已经登录，如果没有则自动跳转到首页，该页面不允许非登录用户操作
        if (Session["UserName"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!this.IsPostBack)
        {

            string albumnId = Request.QueryString["id"];

            string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "select * from Albums where id=" + albumnId;
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                this.Name.Text = dr["Name"].ToString();
                this.Description.Text = dr["Description"].ToString();
                this.Power.SelectedValue = dr["Power"].ToString();
                this.Image1.ImageUrl = dr["Cover"].ToString();
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string albumnId = Request.QueryString["id"];
        string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        string name = this.Name.Text;
        string description = this.Description.Text;
        string power = this.Power.SelectedValue;
        string cover = this.Image1.ImageUrl;
        bool fileOk = false;

        string path = Server.MapPath("~/Upload/Cover/");
        string fileName = Utils.PictureUpload(this.Cover,path);

        fileOk = fileName != "";

        if (fileOk)
        {
            cover = "Upload/Cover/" + fileName;
        }

        string sql = "update Albums set Name='" + name + "' ,Description='" + description + "' ,Power='" + power + "',Cover='" + cover + "' Where ID=" + albumnId;
        SqlCommand cmd = new SqlCommand(sql, conn);
        int result = cmd.ExecuteNonQuery();
        if (result > 0)
        {
            this.Label1.Text = "相册修改成功";
            if (fileOk)
            {
                this.Image1.ImageUrl = cover;
            }
            this.Label1.Visible = true;
        }
        else
        {
            this.Label1.Text = "相册修改失败";
            this.Label1.Visible = true;
        }
        conn.Close();

    }
}
