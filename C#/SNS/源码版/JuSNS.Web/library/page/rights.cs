using System;
using System.Web;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.library.page
{
    public class rights : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "操作成功");
            string msg = GetString("msg");
            string urls = GetString("urls");
            context.Put("errormessage", msg);
            string reurl = GetString("return");
            context.Put("isredirect", reurl);
            if (!string.IsNullOrEmpty(reurl) && reurl == "true") { context.Put("msgdesc", "页面跳转中……如果页面没有跳转，请点这里。"); } else { context.Put("msgdesc", "返回"); }
            if (!string.IsNullOrEmpty(urls)) { context.Put("returnurls", urls); } else { context.Put("returnurls", root + "/"); }
        }
    }
}
