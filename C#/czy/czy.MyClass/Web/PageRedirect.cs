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
    /// PageRedirect 的摘要说明
    /// </summary>
    public class PageRedirect
    {
        public PageRedirect()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 重定域名更换的重定向页面
        /// <summary>
        /// 重定向页面
        /// </summary>
        /// <param name="url">url</param>
        public static  void Redirect( string url)
        {
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", url);
        }
        #endregion
    }

}