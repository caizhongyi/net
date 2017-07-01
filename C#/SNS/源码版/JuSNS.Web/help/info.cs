using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.help
{
    public class info : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int hid = GetInt("hid", 0);
            string q = GetString("q");
            if (hid == 0&&string.IsNullOrEmpty(q))
                HttpContext.Current.Response.Redirect("../help");
            HelpInfo mdl = null;
            if (!string.IsNullOrEmpty(q))
            {
                mdl = JuSNS.Home.App.Web.Instance.GetHelpQ(q);
            }
            else
            {
                mdl = JuSNS.Home.App.Web.Instance.GetHelp(hid);
            }
            if (mdl == null)
            {
                context.Put("cpagetitle", " 找不到记录");
                context.Put("title", "没有找到记录");
                context.Put("content", "您要找的内容在我们数据库中无记录");
            }
            else
            {
                context.Put("cpagetitle", mdl.Title + " - 帮助中心");
                context.Put("title", mdl.Title);
                context.Put("helpid", mdl.HelpID);
                context.Put("content", mdl.Content);
            }
        }

    }
}
