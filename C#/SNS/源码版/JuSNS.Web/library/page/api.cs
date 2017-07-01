using System;
using System.Web;
using System.Reflection;
using System.IO;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.library.page
{
    /// <summary>
    /// 通用整合
    /// </summary>
    public class api: BasePage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            //整合类型，0注册，1登录，3修改密码
            string apitype = GetString("apitype");
            //用户名
            string username = GetString("username");
            //密码
            string password = GetString("password");
            //电子邮件
            string email = GetString("email");
            //用户Key
            string apikey = GetString("apikey");
            //返回的URL，为空不返回
            string backurl = GetString("backurl");
        }

        public override void Page_PostBack(ref VelocityContext context)
        {

        }
    }
}
