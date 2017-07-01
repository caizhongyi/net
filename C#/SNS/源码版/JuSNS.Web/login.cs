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
    public class login : BasePage
    {
        public string isRegMobile = Public.GetXMLValue("isRegMobile");
        public string loginCode = Public.GetXMLValue("loginCode");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {

            string r = GetQueryString("r");
            string urls = GetQueryString("urls");
            #region 判断状态
            if (!string.IsNullOrEmpty(r))
            {
                switch (r)
                {
                    case "out":
                        clearCookies();
                        context.Put("redirecturl", root + "/default"+ExName);
                        break;
                    case "ip":
                        clearCookies();
                        context.Put("error", "IP发生了改变，请重新登录");
                        break;
                    case "Err_NameOrPwdError":
                        clearCookies();
                        context.Put("error", "错误的用户名或者密码");
                        break;
                    case "Err_DurativeLogError":
                        clearCookies();
                        context.Put("error", "连续错误登陆，您已经被锁定");
                        break;
                    case "Err_TimeOut":
                        clearCookies();
                        context.Put("error", "你访问的页面需要登录后才能查看");
                        break;
                }
            }
            else
            {
                if (this.GetUserID() > 0)
                {
                    context.Put("redirecturl", root + "/home/default" + ExName);
                }
            }
            #endregion
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "用户登录");
            context.Put("urls", urls);
            if (loginCode == "1")
            {
                context.Put("isvcode", string.Empty);
            }
            if (isRegMobile == "1")
            {
                context.Put("mobile", "/手机");
            }
            else
            {
                context.Put("mobile", string.Empty);
            }
        }

        /// <summary>
        /// 清除Cookies
        /// </summary>
        public void clearCookies()
        {
            HttpCookie SNSToKenCookie = HttpContext.Current.Request.Cookies["SNSUserPassPort"];
            if (SNSToKenCookie != null)
            {
                SNSToKenCookie.Values.Clear();
                SNSToKenCookie.Domain = JuSNS.Config.UiConfig.CookieDomain;
                SNSToKenCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(SNSToKenCookie);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            string urls = GetString("urls");
            base.Page_Loadno(ref context);
            #region 合法性判断
            string username = GetFormString("username");
            string password = GetFormString("password");
            string autologin = GetFormString("autologin");
            string errSTR = string.Empty;

            if (string.IsNullOrEmpty(username)) { errSTR += "用户名<br /> "; }
            if (string.IsNullOrEmpty(password)) { errSTR += "密码<br /> "; }
            if (loginCode == "1")
            {
                string vcode = GetFormString("vcode");
                if (HttpContext.Current.Session["JuSNSCheckCode"] == null)
                {
                    errSTR += "验证码不正确<br /> ";
                }
                else
                {
                    if (vcode != HttpContext.Current.Session["JuSNSCheckCode"].ToString())
                    {
                        errSTR += "验证码不正确<br /> ";
                    }
                }
            }
            #endregion
            if (!string.IsNullOrEmpty(errSTR))
            {
                context.Put("errors", " 以下必须填写<br />" + errSTR);
            }
            else
            {

                #region 开始注册！
                EnumLoginState rev;
                string uName = string.Empty;
                string trueName = string.Empty;
                int LoginNum = 0;
                int uid = 0;
                if (JuSNS.Common.Input.isEmail(username))
                {
                    //电子邮件登录
                    rev = JuSNS.Home.User.User.Instance.Login(username, JuSNS.Common.Input.MD5(password, false), out uid, out uName, out trueName, out LoginNum, 0);
                }
                else if (JuSNS.Common.Input.isMobile(username))
                {
                    //手机登录
                    rev = JuSNS.Home.User.User.Instance.Login(username, JuSNS.Common.Input.MD5(password, false), out uid, out uName, out trueName, out LoginNum, 2);
                }
                else
                {
                    //用户名登录
                    rev = JuSNS.Home.User.User.Instance.Login(username, JuSNS.Common.Input.MD5(password, false), out uid, out uName, out trueName, out LoginNum, 1);
                }
                switch (rev)
                {
                    case EnumLoginState.Succeed:
                        bool auto = false;
                        if (autologin == "1")
                        {
                            auto = true;
                        }
                        SetCookie(uid, uName, JuSNS.Common.Input.MD5(password, false), auto);
                        if (!string.IsNullOrEmpty(urls) && urls.IndexOf("inc/") == -1)
                        {
                            if (LoginNum == 0)
                            {
                                context.Put("redirecturl", string.Format("~/space/info/profile{0}?f=1", ExName));
                            }
                            else
                            {
                                //context.Put("redirecturl", urls.Replace("-----", "&"));
                                HttpContext.Current.Response.Redirect(urls.Replace("-----", "&"));
                            }
                        }
                        else
                        {
                            if (LoginNum == 0)
                            {
                                context.Put("redirecturl", string.Format("~/space/info/profile{0}?f=1", ExName));
                            }
                            else
                            {
                                context.Put("redirecturl", root + "/home/default" + ExName);
                            }
                        }
                        break;
                    case EnumLoginState.Err_UnActivation:
                        //errSTR += "帐号(" + username + ")未通过<a href=\"" + root + "/activation" + ExName + "?email=" + JuSNS.Home.User.User.Instance.GetUserInfo(uid).Email + "\">电子邮件激活</a>";
                        PageError("帐号(" + username + ")未通过<a href='" + root + "/activation" + ExName + "?email=" + JuSNS.Home.User.User.Instance.GetUserInfo(uid).Email + "'>电子邮件激活</a>", root + "/activation" + ExName + "?email=" + JuSNS.Home.User.User.Instance.GetUserInfo(uid).Email);
                        break;
                    case EnumLoginState.Err_NameOrPwdError:
                        errSTR += "帐号(" + username + ")错误，或密码错误";
                        break;
                    case EnumLoginState.Err_Locked:
                        errSTR += "帐号(" + username + ")已经被锁定";
                        break;
                }
                #endregion
                if (!string.IsNullOrEmpty(errSTR))
                {
                    context.Put("errors", errSTR + "");
                }
            }
            ShowInfo(ref context);
        }
    }
}
