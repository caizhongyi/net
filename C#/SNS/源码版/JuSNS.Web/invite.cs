using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web
{
    public class invite : BasePage
    {
        public string loginCode = Public.GetXMLValue("loginCode");
        public string isRegMobile = Public.GetXMLValue("isRegMobile");
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = GetUserID();
            if (uid > 0)
            {
                context.Put("redirecturl", root + "/home");
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "邀请注册");
            string RegSwitch = Public.GetXMLValue("OpenReg");
            string errSTR = string.Empty;
            if (RegSwitch != "1")
            {
                if (RegSwitch == "0")
                {
                    errSTR += "注册已经关闭，请与管理员联系！";
                }
                else if (RegSwitch == "2")
                {
                    errSTR += "系统未开启注册，需要注册用户邀请才能注册！";
                }
            }
            CheckVerfiy(ref context);
        }

        protected void CheckVerfiy(ref NVelocity.VelocityContext context)
        {
            string uid = GetString("uid");
            string r = GetString("r");
            string error = string.Empty;
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(r) || uid.IndexOf("|") == -1)
            {
                error += "错误的参数";
            }
            string[] uidARR = uid.Split('|');
            string userid = uidARR[1];
            if (Input.MD5(userid + UiConfig.CookieVerifyCode, true) != uidARR[0])
            {
                error += "无效的参数：UserParamError";
            }
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(userid);
            if (Input.MD5(mdl.VerifyCode, true) != r)
            {
                error += "无效的参数：UserCodeError";
            }
            string code = GetString("code");
            string email = GetString("email");
            int m = 0;
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(email))
            {
                //验证邮件是否合法
                //0正确，1无效的参数，2已经验证过了
                int n = JuSNS.Home.User.User.Instance.GetFriendInvite(Convert.ToInt32(userid), email, code);
                switch (n)
                {
                    case 1:
                        error += "无效的参数：InviteCodeError";
                        break;
                    case 2:
                        error += "已经激活了邀请：UserPassError";
                        break;
                }
                m = 1;
            }
            if (!string.IsNullOrEmpty(error))
            {
                context.Put("errors", error);
            }
            else
            {
                if (m == 0)
                {
                    context.Put("rurls", root + "/register" + ExName + "?uid=" + Input.MD5(mdl.VerifyCode, true) + "_" + userid + "");
                }
                else
                {
                    context.Put("rurls", root + "/register" + ExName + "?uid=" + Input.MD5(mdl.VerifyCode, true) + "_" + userid + "&email=" + email + "&code=" + code + "");
                }
                context.Put("username", "<a href=\"" + GetSpaceURL(userid) + "\" target=\"_blank\">" + mdl.TrueName + "</a>");
                context.Put("uname", mdl.TrueName);
                context.Put("userid", userid);
                context.Put("spaceurl", GetSpaceURL(userid));
                context.Put("userhead", GetHeadImage(userid, 2));
                context.Put("city", JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.City).Name);
            }
        }
    }
}