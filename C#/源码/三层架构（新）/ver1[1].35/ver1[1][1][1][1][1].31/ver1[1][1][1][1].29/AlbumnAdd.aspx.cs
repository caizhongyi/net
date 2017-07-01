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

public partial class AlbumnAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            this.Label1.Visible = false;
        }

        //判断用户是否已经登录，如果没有则自动跳转到首页，该页面不允许非登录用户操作
        if (Session["UserName"] == null)
        {
            Session["RedirectUrl"] = Request.Path;
            Response.Redirect("Default.aspx");
        }

    }

    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool fileOk = false;
        string name = this.Name.Text;//
        string description = this.Description.Text;
        string power = this.Power.SelectedValue;
        string cover = "";

        
        string path = Server.MapPath("~/Upload/Cover/");
        string fileName = Utils.PictureUpload(this.Cover,path);

        fileOk = fileName != "" ;

        if(fileOk){

            cover = "Upload/Cover/" + fileName;
            string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = "insert into Albums(UserName,Name,Status,Cover,Description,CreateTime,Hits,Power)values('" + Session["UserName"] + "','" + name + "','0','"+cover+"','"+description+"','"+DateTime.Now.ToString()+"',0,"+power+")";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                this.Label1.Text = "相册创建成功";
                this.Label1.Visible = true;
            }
            else
            {
                this.Label1.Text = "相册创建失败";
                this.Label1.Visible = true;
            }
            conn.Close();


            //Response.Write("上传成功");
            //Response.Write("<img src='Upload/Cover/" + this.FileUpload1.FileName + "'/>");
        }
        else
        {
            this.Label1.Text="封面上传失败，创建相册不成功";
        }
    }

   
}
