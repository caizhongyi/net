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
    public class repwd : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            showinfo(ref context);
        }

        protected void showinfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "找回密码");
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
                string vcode = Input.MD5(System.Guid.NewGuid().ToString(), true);
                EmailNoticeInfo info = new EmailNoticeInfo();
                info.Email = email;
                info.Id = 0;
                info.Ntype = 0;
                info.Posttime = DateTime.Now;
                info.Userid = 0;
                info.Vcode = vcode;
                //发送电子邮件
                try
                {
                    #region 发送邮件
                    string desc = EmailConfig.retrieve;
                    JuSNS.Common.EMail em = JuSNS.Common.EMail.CreateInstance();
                    string body = desc;
                    body = body.Replace("{UserName}", email);
                    body = body.Replace("{SiteName}", UiConfig.SiteName);
                    body = body.Replace("{Url}", UiConfig.URL + "/verify" + ExName + "?type=0&email=" + email + "&code=" + vcode);
                    body = body.Replace("{Domain}", UiConfig.Domain);
                    em.Body = body;
                    em.Subject = "通过邮件找回密码【" + UiConfig.SiteName + "】";
                    em.To = email.Trim();
                    em.From = UiConfig.SiteName + "<" + EmailConfig.from + ">";
                    em.Send();
                    #endregion
                    JuSNS.Home.App.Web.Instance.InsertEmailNotice(info);
                    context.Put("rights", "请查收邮件：" + email + "");
                }
                catch(Exception e)
                {
                    context.Put("errors", "发送邮件失败【" + e.Message + "】");
                }
            }
            showinfo(ref context);
        }
    }
}
