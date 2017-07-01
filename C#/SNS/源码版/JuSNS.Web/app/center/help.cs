using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Model;
using JuSNS.Common;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.center
{
    /// <summary>
    /// 帮助中心
    /// </summary>
    public class help:BasePage
    {
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        /// <summary>
        /// 公共显示
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            context.Put("redirecturl", root + "/help/info" + ExName + "?q=developor");
        }
    }
}
