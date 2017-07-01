using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace czy.MyClass.Web
{
    /// <summary>
    /// FomsValidate___ 的摘要说明(登陆验证若成功則成生身份証票)
    /// </summary>
    public class FormsValidate
    {
        public FormsValidate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 登陆成功后
        /// </summary>
        /// <param name="returnUrl">指向请求页面</param>
        /// <param name="roles">角色名称</param>
        public static void FormsLogin(string returnUrl, string roles)
        {
            //若成功則成生身份証票
            FormsAuthenticationTicket Tick = new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddMinutes(1), false, roles, FormsAuthentication.FormsCookiePath);
            string strCookie = FormsAuthentication.Encrypt(Tick);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strCookie);
            HttpContext.Current.Response.Cookies.Add(cookie);            //加入到Cookie中
            if (returnUrl != null)
            {
                HttpContext.Current.Response.Redirect(returnUrl);            //指向需請求的Rul
            }
            else
            {
                HttpContext.Current.Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }
        //登錄後，需將用戶生成的Cookie恢復到Server端,在Global.axpx事件Application_PostAuthenticateRequest中寫:
        public static void CookiePostAuthenticateRequest(Page page)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authcookie = HttpContext.Current.Request.Cookies[cookieName];
            if (null == authcookie)
            {
                return;
            }
            FormsAuthenticationTicket ticek = FormsAuthentication.Decrypt(authcookie.Value);
            if (null == ticek)
            {

            }

            string[] roles = ticek.UserData.Split(',');
            FormsIdentity id = new FormsIdentity(ticek);
            System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, roles);
            HttpContext.Current.User = principal;
            if (!HttpContext.Current.User.IsInRole("Admin"))
            {
                page.Response.Expires = 0;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public void WebSignOut()
        {
            FormsAuthentication.SignOut();
            //Response.Redirect(FormsAuthentication.LoginUrl);
        }

    }
}