using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web
{
    /// <summary>
    /// 举报
    /// </summary>
    public class report : UserPage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "举报不良信息");
            string urls = GetString("urls");
            context.Put("urls", urls);
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            ReportInfo info = new ReportInfo();
            info.Content = GetString("content");
            info.Id = 0;
            info.PostIP = Public.GetClientIP();
            info.PostTime = DateTime.Now;
            info.Urls = GetString("urls");
            info.UserID = this.UserID;
            int n = JuSNS.Home.App.Web.Instance.InsertReport(info);
            if (n > 0)
            {
                context.Put("rights", "已经提交了信息给管理员！感谢您");
            }
            else
            {
                context.Put("errors", "操作失败");
            }
            ShowInfo(ref context);
        }
    }
}
