using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.library.page
{
    /// <summary>
    /// 编辑器上传
    /// </summary>
    public class upload : UserPage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            Page_PostBack(ref context);
        }

        public void showinfo(ref VelocityContext context, string msg)
        {
            context.Put("msg", msg);
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            //获取参数
            string action = GetString("type");
            string Path = string.Empty;
            string ftype=Public.GetXMLValue("pictype");
            switch (action)
            {
                case "pic":
                    Path = Public.GetXMLPageValue("curpicpath");
                    break;
                case "file":
                    Path = Public.GetXMLPageValue("curfilepath");
                    ftype = Public.GetXMLValue("filetype");
                    break;
                case "flash":
                    Path = Public.GetXMLPageValue("curflashpath");
                    ftype = ".swf,.flv";
                    break;
                case "media":
                    Path = Public.GetXMLPageValue("curmediapath");
                    ftype = Public.GetXMLValue("mediatype");
                    break;
            }
            string Pic = string.Empty;
            //获取数据
            HttpFileCollection filecollection = HttpContext.Current.Request.Files;
            HttpPostedFile hpf = filecollection.Get("filedata");
            string errors = string.Empty;
            if (hpf.ContentLength == 0)
            {
                errors = "无数据提交";
            }
            else
            {
                Pic = Public.GetFile(hpf, ftype, Path);
                if (string.IsNullOrEmpty(Pic))
                {
                    errors = "上传失败";
                }
            }
            //写入数据
            //string msg = "{'err':'" + jsonString(errors) + "','msg':'" + root + Path + "/" + Pic + "'}";
            //showinfo(ref context, msg);
            HttpContext.Current.Response.Write("{'err':'" + jsonString(errors) + "','msg':'" + root + Path + "/" + Pic + "'}");
            HttpContext.Current.Response.End();
        }

        string jsonString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("/", "\\/");
            str = str.Replace("'", "\\'");
            return str;
        }

    }
}
