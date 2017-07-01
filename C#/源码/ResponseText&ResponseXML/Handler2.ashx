<%@ WebHandler Language="C#" Class="Handler2" %>

using System;
using System.Web;

public class Handler2 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        context.Response.Write("<?xml version='1.0' encoding='utf-8' ?><root><student id='s1'><name>wah</name><sex>M</sex></student><student id='s2'><name>wah2</name><sex>M</sex></student></root>");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}