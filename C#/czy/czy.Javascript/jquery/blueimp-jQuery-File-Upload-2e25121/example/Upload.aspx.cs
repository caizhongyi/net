using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using czy.Upload;
using System.IO;
using System.Threading;

public partial class Upload : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["file"] == null)
        {
            List<czy.Upload.Uploader.UploadFile> uploadFiles = new czy.Upload.Uploader(1, "/example/files").Upload();
            int i = 0;
            string html = "";
            html += "[";
            foreach (Uploader.UploadFile uploadFile in uploadFiles)
            {
                if (i != 0) { html += ","; }
                html += "{";
                html += "\"name\":" + "\"" + @uploadFile.Name + "\",";
                html += "\"size\":" + "" + @uploadFile.Size + ",";
                html += "\"url\":" + "\"" + @uploadFile.RelativePath.Replace("/", @"\/") + "\",";
                html += "\"thumbnail_url\":" + "\"" + @uploadFile.RelativePath.Replace("/", @"\/") + "\",";
                html += "\"delete_url\":" + "\"" + @"\/example\/Upload.aspx?file=" + @uploadFile.UploadName + "\",";
                html += "\"delete_type\":" + "\"DELETE\"";
                html += "}";
                i++;
            }
            html += "]";
            Response.Write(html);
        }
        else
        {
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/example/files/" + Request.QueryString["file"].ToString());
        }
    }
}