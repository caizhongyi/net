using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.library.page
{
    public class open:BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            string q = GetString("q");
            string app=GetString("app");
            string appname = string.Empty;
            if (q == "false")
            {
                switch (app)
                {
                    case "ask":
                        appname = "问答模块";
                        break;
                    case "ative":
                        appname = "活动模块";
                        break;
                }
            }
            context.Put("cpagetitle", appname);
            base.Page_Loadno(ref context);
        }
    }
}
