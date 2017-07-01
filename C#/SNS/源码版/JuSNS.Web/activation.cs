using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web
{
    public class activation : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            showinfo(ref context);
        }

        protected void showinfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "激活电子邮件");
            string email = GetString("email");
            context.Put("email", email);
        }


        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            string email = GetString("email");
            if (string.IsNullOrEmpty(email) || email.IndexOf("@") == 1 || email.Length < 6 || email.IndexOf(".") == -1)
            {
                context.Put("errors", "电子邮件不正确");
            }
            else
            {
                int userid = JuSNS.Home.User.User.Instance.GetUserIDForEmail(email);
                if (userid > 0)
                {
                    UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(userid);
                    string vcode = Input.MD5(mdl.VerifyCode, true);
                    //发送电子邮件
                    try
                    {
                        #region 发送邮件
                        string desc = EmailConfig.emailactive;
                        JuSNS.Common.EMail em = JuSNS.Common.EMail.CreateInstance();
                        string body = desc;
                        body = body.Replace("{UserName}", mdl.TrueName);
                        body = body.Replace("{SiteName}", UiConfig.SiteName);
                        body = body.Replace("{Url}", UiConfig.URL + "/verify" + ExName + "?type=1&email=" + email + "&code=" + vcode);
                        body = body.Replace("{Domain}", UiConfig.Domain);
                        em.Body = body;
                        em.Subject = "激活电子邮件【" + UiConfig.SiteName + "】";
                        em.To = email.Trim();
                        em.From = UiConfig.SiteName + "<" + EmailConfig.from + ">";
                        em.Send();
                        #endregion
                        context.Put("rights", "请查收邮件：" + email + "");
                    }
                    catch (Exception e)
                    {
                        context.Put("errors", "发送邮件失败【" + e.Message + "】");
                    }
                }
                else
                {
                    context.Put("errors", "此电子邮件不存在");
                }
            }
            showinfo(ref context);
        }
    }
}