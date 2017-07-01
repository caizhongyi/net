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
    /// AddCss 的摘要说明
    /// </summary>
    public class Common
    {
        public Common()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 页面头部加入CSS样式链接
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="cssPath">路径</param>
        public static void AddCssHeadLink(Page page, string cssPath)
        {
            HtmlLink link = new HtmlLink();
            link.Href = cssPath;
            link.Attributes["rel"] = "stylesheet";
            link.Attributes["type"] = "text/css";
            page.Header.Controls.Add(link);
        }
        /// <summary>
        /// 页面头部加入CSS样式链接
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="cssPath">路径</param>
        public static void AddJavaScriptHeadLink(Page page, string cssPath)
        {
            HtmlLink link = new HtmlLink();
            link.Href = cssPath;
            link.Attributes["type"] = "text/JavaScript";
            page.Header.Controls.Add(link);
        }
        /// <summary>
        /// 获取QQTalkHTML代码
        /// </summary>
        /// <param name="QQ">QQ号</param>
        /// <param name="imgUrl">图片路径</param>
        /// <returns></returns>
        public static string GetQQTalkHTML(string QQ,string imgUrl)
        {
           return string .Format ("<a target='blank' href='tencent://message/?uin={1}&Site=工具啦&Menu=yes'><img border='0' src='{0}' alt='在线咨询'/></a>",imgUrl,QQ);
        }
    }

}