using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class startnotice : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "初始化通知");
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            UserInfo info = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            if (info.IsAdmin > 1)
            {
                int n = JuSNS.Home.App.Web.Instance.Start("notice");
                if (n > 0)
                {
                    context.Put("rights", "初始化成功！");
                }
                else
                {
                    context.Put("errors", "初始化失败！");
                }
            }
            else
            {
                context.Put("errors", "管理员权限不足，只有超级管理员才可进行此操作！");
            }
            ShowInfo(ref context);
        }
    }
}
