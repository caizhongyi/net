using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.space.info
{
    public class pwd : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            HttpContext.Current.Response.Expires = 0;
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "修改密码");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("username", "用户名：" + mdl.UserName);
            context.Put("email", "电子邮件：" + mdl.Email);
            context.Put("mobilesendcontent", "编写【" + Public.GetXMLValue("sendmobileNumber") + "】发送到 " + Public.GetXMLValue("sendMobileBind") + "，根据提示完成绑定。");
            if (!string.IsNullOrEmpty(mdl.Mobile))
            {
                context.Put("mobile", "手机：" + mdl.Mobile);
                string bindMobile = string.Empty;
                if (mdl.BindMoblie)
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',0);\" title=\"取消绑定\">已绑定</a>";
                }
                else
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',1);\" title=\"绑定手机\">未绑定</a>";
                }
                context.Put("bindmobile", "(" + bindMobile + ")");
            }
            else
            {
                context.Put("mobile", "手机：<a href=\"javascript:void(0);\" onclick=\"jQuery('#hideMobile').toggle();\" title=\"填写手机\">未填写</a>");
                context.Put("bindmobile", string.Empty);
            }

        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string password = GetString("txtpassword");
            string newpassword = GetString("txtnewpassword");
            string newpassword1 = GetString("txtnewpassword1");
            int isRights = 1;
            EnumLoginState state = JuSNS.Home.User.User.Instance.CheckUser(this.UserID, Input.MD5(password, false));
            if (state == EnumLoginState.Succeed)
            {
                isRights = 0;
            }
            if (string.IsNullOrEmpty(newpassword))
            {
                isRights = 2;
            }
            if (newpassword != newpassword1)
            {
                isRights = 3;
            }
            ShowInfo(ref context);
            if (isRights == 0)
            {
                bool isr = JuSNS.Home.User.User.Instance.ChangePassword(this.UserID, Input.MD5(newpassword, false));
                if (isr)
                {
                    SetCookie(this.UserID, this.UserName, Input.MD5(newpassword, false), false); 
                    context.Put("rights", "修改成功！");
                }
                else
                {
                    context.Put("errors", "修改失败！");
                }
            }
            else
            {
                if (isRights == 2)
                {
                    context.Put("errors", "请填写密码。");
                }
                else if (isRights == 3)
                {
                    context.Put("errors", "两次密码不一致。");
                }
                else
                {
                    context.Put("errors", "旧密码错误。");
                }
            }
        }

    }
}
