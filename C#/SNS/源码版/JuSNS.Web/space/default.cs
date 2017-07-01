using System;
using System.Web;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.space
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "个人主页");
        }
    }
}
