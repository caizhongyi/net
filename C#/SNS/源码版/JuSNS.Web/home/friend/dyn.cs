using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class dyn : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            string action = GetString("action");
            string filepath = "~/space/info/my/user" + this.UserID + ".config";
            if (action == "delete")
            {
                string resultSTR = string.Empty;
                string userid = GetString("uid");
                string fdynlist = Public.GetXMLValue("killuser", filepath);
                string[] list = fdynlist.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    if (userid != list[i])
                    {
                        resultSTR += list[i] + ",";
                    }
                }
                Public.setXmlInnerText(filepath, "/configuration/killuser", Input.FixCommaStr(resultSTR));
            }
            else if (action == "Set")
            {
                string dynnumber = GetString("dynnumber");
                Public.setXmlInnerText(filepath, "/configuration/dynnumber", dynnumber);
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "好友动态设置");
            int uid=this.UserID;
            dynlist(ref context,uid);
        }

        public void dynlist(ref VelocityContext context, int uid)
        {
            string listSTR = string.Empty;
            string filepath = "~/space/info/my/user" + this.UserID + ".config";
            string fdynlist = Public.GetXMLValue("killuser", filepath);
            string dynnumber = Public.GetXMLValue("dynnumber", filepath);
            context.Put("dynnumber", dynnumber);
            if (fdynlist == "0" || string.IsNullOrEmpty(fdynlist))
            {
                context.Put("friendlist", "<li>无记录</li>");
            }
            else
            {
                string[] list = fdynlist.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != "0")
                    {
                        UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(list[i]);
                        listSTR += "<li id=\"param_" + list[i] + "\">";
                        listSTR += "<div class=\"images center\"><a href=\"" + this.GetSpaceURL(list[i]) + "\"><img src=\"" + this.GetHeadImage(list[i], 1) + "\" /><br />" + mdl.TrueName + "</a><br /><a href=\"javascript:;\" onclick=\"deletedynlist(" + list[i] + ")\" class=\"showok1\"></a></div>";
                        listSTR += "</li>";
                    }
                }
                context.Put("friendlist", listSTR);
            }
        }
    }
}
