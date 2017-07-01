using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class email : UserPage
    {
        public int recount = UiConfig.CityNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            context.Put("cpagetitle", "邮件地址薄");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("vcode", Input.MD5(mdl.VerifyCode, true));
        }
    }
}