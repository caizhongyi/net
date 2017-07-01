using System;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.center
{
    public class appinfo : UserPage
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
            base.Page_Load(ref context);
            int appid = GetInt("appid", 0);
            AppInfo info = JuSNS.App.App.Instance.GetAppInfo(appid);
            if (info == null)
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                bool issetup = JuSNS.App.App.Instance.IsSetupApp(appid, this.UserID);
                if (!issetup)
                {
                    context.Put("errors", "您还没安装此应用程序");
                }
                else
                {
                    AppDeveloperInfo mdl = JuSNS.App.App.Instance.GetDevInfo(info.UserID);
                    if (mdl == null)
                    {
                        context.Put("errors", "应用程序开发者发生错误！");
                    }
                    else
                    {
                        context.Put("cpagetitle", info.Appname);
                        context.Put("url", info.Url + "?appid=" + appid + "&appkey=" + info.Appkey + "&devid=" + info.UserID + "&userid=" + this.UserID + "&userkey=" + mdl.Userkey + "");
                        int height = 0;
                        if (info.Height > 0)
                        {
                            height = info.Height;
                        }
                        else
                        {
                            height = 800;
                        }
                        context.Put("height", height);
                        int width = 0;
                        if (info.Width == 0 || info.Width > 810)
                        {
                            width = 810;
                        }
                        else
                        {
                            width = info.Width;
                        }
                        context.Put("width", width);
                    }
                }
                     
            }
        }
    }
}
