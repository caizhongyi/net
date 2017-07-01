using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class invite : UserPage
    {
        public int recount = UiConfig.CityNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            context.Put("cpagetitle", "邀请朋友");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("vcode", Input.MD5(mdl.VerifyCode, true));
            context.Put("myname", mdl.TrueName);
            context.Put("invitecontent", Public.GetXMLValue("inviteReg"));
            base.Page_Load(ref context);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            ShowInfo(ref context);
            string email = GetString("email");
            string error = string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                error += "邮件不能为空！\n";
            }
            string saymsg = GetString("saymsg");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            string desc = EmailConfig.invite;
            desc = desc.Replace("{UserName}", mdl.TrueName);
            desc = desc.Replace("{SiteName}", UiConfig.SiteName);
            desc = desc.Replace("{Content}", Public.GetXMLValue("inviteReg"));
            desc = desc.Replace("{InviteContent}", saymsg);
            desc = desc.Replace("{Url}", UiConfig.RootUrl + "/invite" + ExName + "?uid=" + Input.MD5((this.UserID.ToString() + UiConfig.CookieVerifyCode), true) + "|" + this.UserID + "&r=" + Input.MD5(mdl.VerifyCode, true) + "&code={VCODE}&email={EMAIL}");
            desc = desc.Replace("{RootURL}", UiConfig.RootUrl);
            desc = desc.Replace("{Domain}", UiConfig.Domain);
            Dictionary<string, int> dct = JuSNS.Home.User.User.Instance.InviteFriends(this.UserID, mdl.TrueName,email,desc);
            string rStr = string.Empty;
            string rest = string.Empty;
            foreach (KeyValuePair<string, int> kv in dct)
            {
                switch (kv.Value)
                {
                    case 0:
                        rStr = "<span class=\"green pstr\">成功发送邀请</span>";
                        break;
                    case 1:
                        rStr = "<span class=\"reshow pstr\">已在本站注册</span> ";
                        break;
                    case 2:
                        rStr = "<span class=\"reshow pstr\">已被邀请</span> ";
                        break;
                    case 3:
                        rStr = "<span class=\"reshow pstr\">发送邀请失败</span>";
                        break;
                    case 4:
                        rStr = "<span class=\"reshow pstr\">发送邀请失败：邮件发送失败</span>";
                        break;
                }
                rest += "<div>"+kv.Key + " (" + rStr + ")</div>\r\n";
            }
            if (!string.IsNullOrEmpty(rest))
            {
                context.Put("result", rest);
            }
            else
            {
                context.Put("errors", "发送错误！代码：InviteNullError");
            }
        }
    }
}