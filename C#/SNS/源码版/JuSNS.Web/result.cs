using System;
using System.Web;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web
{
    public class result : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = GetInt("uid", 0);
            base.Page_Load(ref context);
            context.Put("cpagetitle", "恭喜！注册成功");
            UserInfo mdl = Home.User.User.Instance.GetUserInfo(uid);
            context.Put("username", mdl.UserName);
            context.Put("email", mdl.Email);
            context.Put("truename", mdl.TrueName);
            context.Put("mobile", mdl.Mobile);
            int state = mdl.State;
            switch (state)
            {
                case 0:
                    context.Put("state", "等待电子邮件验证，如果长时间没有收到邮件，<a href=\"activation" + ExName + "?email=" + mdl.Email + "\" title=\"点击验证电子邮件\">请点击此处</a>");
                    break;
                case 1:
                    context.Put("state", "帐户被锁定状态，请与管理员联系。");
                    break;
                case 2:
                    context.Put("state", "<a href=\"activation" + ExName + "?email=" + mdl.Email + "\" title=\"点击验证电子邮件\">帐户正常，但是还未验证电子邮件</a>");
                    break;
                case 3:
                    context.Put("state", "帐户正常，未捆绑手机");
                    break;
                case 5:
                    context.Put("state", "正常状态，已验证电子邮件");
                    break;
            }
        }
    }
}