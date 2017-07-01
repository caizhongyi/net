using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.shop
{
    public class upload : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string Pic = string.Empty;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                string Path = Public.GetXMLShopValue("shopPicPath");
                HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Path);
                if (string.IsNullOrEmpty(Pic))
                {
                    context.Put("errors", "上传失败，请重新选择图片上传");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type=\"text/javascript\">");
                    sb.Append("parent.InserHTMLToEdits(\"" + Path + "/" + Pic + "\");");
                    sb.Append("window.location.href=\"upload"+ExName+"\";");
                    sb.Append("</script>");
                    HttpContext.Current.Response.Write(sb.ToString());
                }
            }
            else
            {
                context.Put("errors", "请选择一张图片");
            }
            ShowInfo(ref context);
        }
    }
}
