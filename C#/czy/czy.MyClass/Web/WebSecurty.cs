using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;

namespace czy.MyClass.Web
{
    /// <summary>
    ///WebSecurty 的摘要说明
    /// </summary>
    public class WebSecurty
    {
        public WebSecurty()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 登入跳转页面并且保存Coookie(放在主页面的load中)
        /// <summary>
        /// 登入跳转页面并且保存Coookie
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="setCookie">是否创建Cookie</param>
        public void GetCookiesAndRedirect(string userName, bool setCookie)
        {
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(userName, setCookie);

        }

        #endregion

        #region 注消重新登陆时,跳转的URL
        /// <summary>
        /// 注消重新登陆时,跳转的URL
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>跳转的URL</returns>
        public string GetUrl(string userName, bool setCookie)
        {
            return System.Web.Security.FormsAuthentication.GetRedirectUrl(userName, setCookie);
        }
        #endregion

        #region 注消
        /// <summary>
        /// 注消
        /// </summary>
        /// <returns>loginURL</returns>
        public string LoginOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return FormsAuthentication.LoginUrl;
        }
        #endregion

        //#region 验证用户登录是否有效
        ///// <summary>
        ///// 验证用户登录是否有效
        ///// </summary>
        ///// <param name="strName">用户名</param>
        ///// <param name="strPassword">用户输入的密码</param>
        ///// <param name="strHashPassword">加密过的正确密码</param>
        ///// <returns>是否正确</returns>
        //public static bool ValidateLogin(string strName, string strPassword, string strHashPassword)
        //{
        //    //WebHelp.GetHashValue(ref strPassword);

        //    if (strPassword.Trim().Equals(strHashPassword.Trim()))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
        //#endregion

        #region CreateSecurityKey [创建安全证书]

        /// <summary>
        /// 如果票证将存储在持久性 Cookie（跨浏览器会话保存），则为 true；否则为 false。如果该票证存储在 URL 中，将忽略此值。
        /// </summary>
        private static bool IsPersistent = false;

        /// <summary>
        /// 用来存储在统一登录平台下的系统间导航时所传输的值。
        /// </summary>
        private static string strIdentifier = string.Empty;

        /// <summary>
        /// 加密的 Forms 身份验证票证。
        /// </summary>
        private static string strLoginKey = string.Empty;

        /// <summary>
        /// 创建安全证书（包含Forms凭据和自定义Cookie）。
        /// </summary>
        /// <param name="objPage">页面对象，一般为“this”</param>
        /// <param name="strName">用户名</param>
        public static void CreateSecurityKey(Page objPage, string strName, DataTable dtRoles)
        {
            string strReturnUrl = FormsAuthentication.DefaultUrl;
            try
            {
                if (AddFormsCookie(objPage, strName, dtRoles))
                {
                    strReturnUrl = (objPage.Request.QueryString["ReturnUrl"] != null) ? objPage.Request.QueryString["ReturnUrl"].Trim() : FormsAuthentication.DefaultUrl;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    objPage.Response.Write("<script> alert(@'失败：\n请检查您相关设置。\n1、网络状态是否可用？\n2、浏览器是否可以使用Cookie？')</script>");
                    return;
                }
            }
            catch (Exception e)
            {
                string str = e.Message;
                FormsAuthentication.SignOut();
                objPage.Response.Write("<script> alert( @'失败：\n请检查您相关设置。\n1、网络状态是否可用？\n2、浏览器是否可以使用Cookie？')</script>");
                return;
            }

            objPage.Response.Redirect(strReturnUrl, true);
        }

        /// <summary>
        /// 创建Forms凭据。
        /// </summary>
        /// <param name="strName">用户名</param>
        /// <returns>是否成功创建Forms凭据</returns>
        internal static bool AddFormsCookie(Page objPage, string strLoginName, DataTable dtRoles)
        {
            //DataTable dtRoleNames = new DataTable("UserRoles"); //存放返回的角色列表
            // TODO : 代码开发任务(CT20080917-02)，根据用户名获取角色列表。

            if (dtRoles.Rows.Count < 1)
            {
                return false;
            }
            else
            {
                string strRole = string.Empty;
                for (int i = 0; i < dtRoles.Rows.Count; i++)
                {
                    if (i != dtRoles.Rows.Count - 1)
                    {
                        strRole += dtRoles.Rows[i]["RoleName"] + ",";
                    }
                    else
                    {
                        strRole += dtRoles.Rows[i]["RoleName"];
                    }
                }

                FormsAuthenticationTicket Fat = new FormsAuthenticationTicket(1, strLoginName, DateTime.Now, DateTime.Now.AddMinutes(30), IsPersistent, strRole, "/");
                strLoginKey = FormsAuthentication.Encrypt(Fat);

                HttpCookie LoginCookie = new HttpCookie(FormsAuthentication.FormsCookieName, strLoginKey);
                objPage.Response.Cookies.Add(LoginCookie);

                return true;
            }
        }


        #endregion

    }

}