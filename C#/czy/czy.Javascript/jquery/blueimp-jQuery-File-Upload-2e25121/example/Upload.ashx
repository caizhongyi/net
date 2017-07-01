<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;

public class Upload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.QueryString["file"] == null)
        {
            czy.Upload.Uploader uploader = new czy.Upload.Uploader(1, "/example/files");
            System.Collections.Generic.List<czy.Upload.Uploader.UploadFile> uploadFiles = uploader.Upload();
            int i = 0;
            string html = "";
            html += "[";
            foreach (czy.Upload.Uploader.UploadFile uploadFile in uploadFiles)
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
            context.Response.Write(html);
        }
        else
        {
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/example/files/" + context.Request.QueryString["file"].ToString());
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}