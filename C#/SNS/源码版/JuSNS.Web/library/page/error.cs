using System;
using System.Web;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.library.page
{
    public class error : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "发生错误");
            string Err = GetString("error");
            string urls = GetString("urls");
            string ErrorMsg = string.Empty;
            switch (Err)
            {
                case "Err_IPLimited":
                    ErrorMsg = "您的IP[" + JuSNS.Common.Public.GetClientIP() + "]被限制，不能登陆！";
                    break;
                case "Err_Locked":
                    ErrorMsg = "您已经被锁定！";
                    break;
                case "Err_AdminLocked":
                    ErrorMsg = "您已经被锁定！";
                    break;
                case "Err_UnActivation":
                    ErrorMsg = "您还没通过电子邮件激活，不能登陆本系统！";
                    break;
                case "Err_TimeOut":
                    ErrorMsg = "你访问的页面需要登录后才能查看";//登录过期
                    break;
                case "Err_AdminTimeOut":
                    ErrorMsg = "你访问的页面需要登录后才能查看";//登录过期
                    break;
                case "Err_NoAuthority":
                    ErrorMsg = "您没有此项的操作权限！";
                    break;
                case "Err_DbException":
                    ErrorMsg = "系统错误。出错原因可能是：<br />1.与数据库服务器的通信失败；<br />2.数据库连接字符串不正确；<br />3.数据库发生异常。";
                    break;
                case "Err_DurativeLogError":
                    ErrorMsg = "连续错误登陆，您已经被锁定！";
                    break;
                case "Err_NameOrPwdError":
                    ErrorMsg = "用户名不存在或者密码错误";
                    break;
                case "Err_GroupExpire":
                    ErrorMsg = "您的帐号已过期";
                    break;
                case "Err_NotAdmin":
                    ErrorMsg = "抱歉，您不是管理员。您的操作已经记录！<br />您的IP：[" + JuSNS.Common.Public.GetClientIP() + "]已被记录";
                    break;
                case "Err_IPChange":
                    ErrorMsg = "你的IP发生了变化";
                    return;
                default:
                    ErrorMsg = "" + Err;
                    break;
            }
            context.Put("errormessage", ErrorMsg);
            string reurl = GetString("return");
            if (!string.IsNullOrEmpty(reurl) && reurl == "true"){context.Put("msgdesc", "页面跳转中……如果页面没有跳转，请点这里。"); } else { context.Put("msgdesc", "返回");}
            if (!string.IsNullOrEmpty(urls)){context.Put("returnurls", urls);}else{context.Put("returnurls", root + "/"); }
        }
    }
}
