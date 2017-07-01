<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.Web;

public class FileUpload : IHttpHandler {

    private HttpContext ctx;
    public void ProcessRequest(HttpContext context)
    {
        ctx = context;
        string uploadPath = context.Server.MapPath("~/Upload");
        FileUploadProcess fileUpload = new FileUploadProcess();
        fileUpload.FileUploadCompleted += new FileUploadCompletedEvent(fileUpload_FileUploadCompleted);
        fileUpload.ProcessRequest(context, uploadPath);
    }

    void fileUpload_FileUploadCompleted(object sender, FileUploadCompletedEventArgs args)
    {
        string id = ctx.Request.QueryString["id"];
        //FileInfo fi = new FileInfo(args.FilePath);
        //string targetFile = Path.Combine(fi.Directory.FullName, args.FileName);
        //if (File.Exists(targetFile))
        //    File.Delete(targetFile);
        //fi.MoveTo(targetFile);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}