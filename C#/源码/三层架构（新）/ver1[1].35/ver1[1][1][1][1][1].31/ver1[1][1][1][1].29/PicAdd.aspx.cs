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

public partial class PicAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        

        if (this.IsPostBack)
        {
            string path = Server.MapPath("~/Upload/Pic/");
            string name = Request.Form["Name"];//a,b,c===>[]
            string description = Request.Form["Description"];//a1,b1,c1

            string[] names = name.Split(',');
            string[] descriptions = description.Split(',');

            string albumsID = Request.QueryString["id"];

            int count = 0;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile file = Request.Files[i];
                if (file.FileName != "")
                {
                    string fileName = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("-", "") + i;
                    fileName += System.IO.Path.GetExtension(file.FileName).ToLower();
                    file.SaveAs(path + fileName);

                    string connStr = System.Configuration.ConfigurationManager.AppSettings["connStr"];
                    SqlConnection conn = new SqlConnection(connStr);
                    conn.Open();
                    string sql = "insert into Picture(AlbumsID,Name,Description,Flag,CreateTime,Hits,Path)values('" + albumsID + "','" + names[i] + "','" + descriptions[i] + "',0,'" + DateTime.Now.ToString() + "',0,'" + fileName + "')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (result == 1)
                    {
                        count++;
                    }
                }

            }
            if (count == 0)
            {
                this.Msg.Text = "很遗憾，没有上传成功";
            }
            else
            {
                this.Msg.Text = "恭喜，上传成功" + count + "张图片到相册";
            }
        }
        
        
    }
}
