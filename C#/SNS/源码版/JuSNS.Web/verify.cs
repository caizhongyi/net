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
    /// <summary>
    /// 验证
    /// </summary>
    public class verify : BasePage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            showinfo(ref context);
        }
        /// <summary>
        /// 基础信息
        /// </summary>
        /// <param name="context"></param>
        protected void showinfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int type = GetInt("type", -1);
            string code = GetString("code");
            string email = GetString("email");
            if (type == -1)
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                context.Put("type", type);
                switch (type)
                {
                    case 0:
                        EmailNoticeInfo info = JuSNS.Home.App.Web.Instance.GetEmailNoticeInfo(code);
                        if (info != null)
                        {
                            if (info.Email != email)
                            {
                                context.Put("err", "错误的参数");
                            }
                        }
                        else
                        {
                            context.Put("err", "错误的参数");
                        }
                        context.Put("cpagetitle", "找回密码");
                        break;
                    case 1:
                        context.Put("cpagetitle", "激活电子邮件");
                        context.Put("email", email);
                        int userid = JuSNS.Home.User.User.Instance.GetUserIDForEmail(email);
                        if (userid == 0)
                        {
                            context.Put("err", "错误的参数");
                        }
                        else
                        {
                            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(userid);
                            if (mdl.UserID > 0)
                            {
                                if (Input.MD5(mdl.VerifyCode, true) != code)
                                {
                                    context.Put("err", "错误的参数");
                                }
                                else
                                {
                                    int n = JuSNS.Home.User.User.Instance.ActivationEmail(userid);
                                    if (n == 0)
                                    {
                                        context.Put("err", "激活失败，你是否已经激活过了？");
                                    }
                                }
                            }
                            else
                            {
                                context.Put("err", "错误的参数");
                            }
                        }
                        break;
                    case 2:
                        context.Put("cpagetitle", "找回密码");
                        break;
                }
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string newpassword = GetString("newpassword");
            string confimnewpassword = GetString("confimnewpassword");
            string email = GetString("email");
            string code = GetString("code");
            if (newpassword != confimnewpassword)
            {
                context.Put("errors", "2次密码不一致");
            }
            else
            {
                int userid = JuSNS.Home.User.User.Instance.GetUserIDForEmail(email);
                bool isr = JuSNS.Home.User.User.Instance.ChangePassword(userid, Input.MD5(newpassword, false));
                if (isr)
                {
                    EmailNoticeInfo info = new EmailNoticeInfo();
                    info.Email = email;
                    info.Id = 1;
                    info.Ntype = 0;
                    info.Posttime = DateTime.Now;
                    info.Userid = 0;
                    info.Vcode = code;
                    JuSNS.Home.App.Web.Instance.InsertEmailNotice(info);
                    context.Put("redirecturl", root + "/login" + ExName);
                }
                else
                {
                    context.Put("errors", "修改失败");
                }
            }
        }

    }
}