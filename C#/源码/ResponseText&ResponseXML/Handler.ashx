<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        int numA = int.Parse(context.Request.QueryString["A"]);
        int numB = int.Parse(context.Request.QueryString["B"]);
        int Result = numA + numB;
        context.Response.Write(Result.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}