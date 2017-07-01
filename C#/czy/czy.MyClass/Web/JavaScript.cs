using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace czy.MyClass.Web
{
    /// <summary>
    /// Js 的摘要说明
    /// </summary>
    public class JavaScript
    {
        public JavaScript()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 将Script写入到页面中
        /// </summary>
        /// <param name="script">script内容</param>
        public static void ResponseScript(string script)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\" type=\"text/javascript\" defer>\n" + script + "\n</script>\n");
        }

        /// <summary>
        /// 输出警告框
        /// </summary>
        /// <param name="message"></param>
        public static void Alert(string message)
        {
            ResponseScript("    alert('" + message + "');");
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        public static void GoBack()
        {
            ResponseScript("    window.history.back();");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 提示信息并刷新本页面
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndReload(string message)
        {
            StringBuilder script = new StringBuilder();
            script.AppendFormat("    alert('{0}');\n", message);
            script.AppendLine("    window.location.reload();");
            ResponseScript(script.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 重定向到一个新页面
        /// </summary>
        /// <param name="url"></param>
        public static void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// 父页面跳转到新页面
        /// </summary>
        /// <param name="url"></param>
        public static void ParentPageRedirect(string url)
        {
            ResponseScript("    parent.location.href='" + url + "';");
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 输出警告框并且返回上一页
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndGoBack(string message)
        {
            StringBuilder script = new StringBuilder();
            script.AppendFormat("    alert('{0}');\n", message);
            script.AppendLine("    window.history.back();");
            ResponseScript(script.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 输出警告框并且转向新页面
        /// </summary>
        /// <param name="message">警告框内容</param>
        /// <param name="url">转向的页面</param>
        public static void AlertAndRedirect(string message, string url)
        {
            StringBuilder script = new StringBuilder();
            script.AppendFormat("    alert('{0}');\n", message);
            script.AppendFormat("    window.location.href='{0}'\n", url);
            ResponseScript(script.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 用户弹出页面的关闭该页面后刷新父页面
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndCloseThisOpenWindow(string message)
        {
            StringBuilder script = new StringBuilder();
            script.AppendFormat("    alert('{0}');\n", message);
            script.AppendLine("    window.opener.reload();");
            script.AppendLine("    window.close();");
            ResponseScript(script.ToString());
        }


        #region 打开新窗体
        /// <summary>
        /// 打开新窗体
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static void OpenNewFrom(Page page, string url)
        {
            string Script = "<script>window.open('" + url + "');</script>";
            Type CSType = page.GetType();
            ClientScriptManager objClientScript = page.ClientScript;
            objClientScript.RegisterStartupScript(CSType, "", Script);
        }
        #endregion

      

        #region Show 弹出确定、取消对话框
        /// <summary>
        /// 弹出确定、取消对话框
        /// </summary>
        /// <param name="registerControl">要点击的按钮</param>
        /// <param name="message">弹出的消息</param>
        /// <param name="wrongLogical">点击取消执行的方法名</param>
        public static void AlertConfirm(WebControl webControl, string message, EventHandler wrongLogical)
        {
            RegisterConfirm(webControl, message, "onclick", wrongLogical);
        }

        /// <summary>
        /// 注册JavaScript事件
        /// </summary>
        /// <param name="registerControl">WebControl</param>
        /// <param name="message">显示信息</param>
        /// <param name="clientEventName">事件名称</param>
        /// <param name="wrongLogical"></param>
        public static void RegisterConfirm(WebControl registerControl, string message, string clientEventName, EventHandler wrongLogical)
        {
            System.Web.UI.Page page = registerControl.Page;

            string rightLogicalCode = page.ClientScript.GetPostBackEventReference(registerControl, string.Empty);
            string wrongLogicalCode = string.Empty;

            if (wrongLogical != null)
            {
                Button btnCancel = new Button();
                btnCancel.Attributes["style"] = "display:none;";
                btnCancel.Click += new EventHandler(wrongLogical);
                page.Form.Controls.Add(btnCancel);

                wrongLogicalCode = "var _confrim_cancel_control_ = window.document.getElementById('" + btnCancel.ClientID + "'); if(_confrim_cancel_control_){_confrim_cancel_control_.click();}";
            }

            string jscodeA = "javascript:if(confirm('" + message + "')){function _confirm_(){" + rightLogicalCode + ";";
            string jscodeB = "} _confirm_();}else{ " + (wrongLogical != null ? wrongLogicalCode : "return false;") + " ; return false;}";

            if (registerControl.Attributes[clientEventName] != null && registerControl.Attributes[clientEventName].Contains("_confirm_()"))
                registerControl.Attributes[clientEventName] = string.Empty;

            registerControl.Attributes[clientEventName] = jscodeA + registerControl.Attributes[clientEventName] + jscodeB;
        }
        #endregion

 
     
    }
}