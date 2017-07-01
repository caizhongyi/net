using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.help
{
    public class @new : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int uid = this.UserID;
            if (!JuSNS.Home.User.User.Instance.IsAdmin(uid))
            {
                context.Put("errors", "您没有权限");
            }
            else
            {
                int hid = GetInt("hid", 0);
                if (hid > 0)
                {
                    HelpInfo info = JuSNS.Home.App.Web.Instance.GetHelp(hid);
                    context.Put("helpid", info.HelpID);
                    context.Put("title", info.Title);
                    context.Put("content", info.Content);
                    context.Put("cpagetitle", "修改帮助");
                }
                else
                {
                    context.Put("cpagetitle", "添加帮助");
                }
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            HelpInfo info = new HelpInfo();
            info.Content = GetString("txtcontent");
            info.HelpID = GetString("helpid");
            info.ID = GetInt("hid", 0);
            info.Title = GetString("title");
            int n = JuSNS.Home.App.Web.Instance.InsertHelp(info);
            if (n > 0)
            {
                context.Put("rights", "保存成功！");
            }
            else
            {
                context.Put("errors", "保存失败！");
            }
            ShowInfo(ref context);
        }
    }
}
