using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home
{
    public class level:UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "会员等级");
            string listSTR = string.Empty;
            string memberleves = Config.UiConfig.memberleves;
            string[] memberARR = memberleves.Split(',');
            for (int i = 0; i < memberARR.Length; i++)
            {
                listSTR += "<p><img src=\"" + root + "/template/" + Config.UiConfig.SkinStyle + "/images/level_" + (i + 1) + ".gif\"> <span class=\"reshow\">" + memberARR[i] + "</span> 天</p>";
            }
            context.Put("result", listSTR);
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("level", "<img src=\"" + root + "/template/" + Config.UiConfig.SkinStyle + "/images/level_" + Public.GetMemberlevels(mdl.MemberLevels) + ".gif\"> (" + mdl.MemberLevels + "天)");
        }
    }
}
