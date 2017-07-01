using System;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.center
{
    /// <summary>
    /// 详细信息
    /// </summary>
    public class info : UserPage
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
                if (info.IsLock == 0)
                {
                    context.Put("isnormal", true);
                }
                JuSNS.App.App.Instance.UpdateAppClick(appid);
                bool issetup = JuSNS.App.App.Instance.IsSetupApp(appid, this.UserID);
                context.Put("issetup", issetup);
                bool isdev = JuSNS.App.App.Instance.IsDeveloper(this.UserID);
                context.Put("isdev", isdev);
                context.Put("spaceurl", this.GetSpaceURL(info.UserID));
                context.Put("headpic", this.GetHeadImage(info.UserID));
                context.Put("truename", JuSNS.Home.User.User.Instance.GetUserInfo(info.UserID).TrueName);
                context.Put("cpagetitle", info.Appname);
                context.Put("apppath",Public.GetXMLPageValue("apppath"));
                context.Put("pic", info.Pic);
                context.Put("icon", info.Icon);
                context.Put("content", info.Content);
                context.Put("appid", appid);
                context.Put("SetupContent", info.SetupContent);
                context.Put("clicks", info.Click);
                context.Put("setupnumber", info.SetupNumber);
            }
        }
    }
}
