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

public partial class Reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.AgreePanel.Visible = false;
        this.RegPanel.Visible = true;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        string sql = "insert into UserInfo(UserName,Password)values('"+this.Username.Text+"','"+this.Password.Text+"')";
        SqlCommand cmd = new SqlCommand(sql,conn);
        int result =cmd.ExecuteNonQuery();
        if (result > 0)
        {
            Response.Write("注册成功");
        }
        else
        {
            Response.Write("注册失败");
        }
        conn.Close();

    }
}
