using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web
{
    public class url: BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            context.Put("urls", GetString("urls"));
            context.Put("cpagetitle", "浏览网页");
            base.Page_Load(ref context);
        }
    }
}
