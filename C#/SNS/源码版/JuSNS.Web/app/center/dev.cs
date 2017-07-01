using System;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.center
{
    /// <summary>
    /// 开发者
    /// </summary>
    public class dev : UserPage
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
            context.Put("cpagetitle", "开发者中心");
            bool isdev = JuSNS.App.App.Instance.IsDeveloper(this.UserID);
            AppDeveloperInfo mdl = JuSNS.App.App.Instance.GetDevInfo(this.UserID);
            if (mdl!=null)
            {
                if (mdl.IsLock == (byte)EnumCusState.ForNormal || isdev)
                {
                    context.Put("isdev", true);
                    context.Put("userkey", mdl.Userkey);
                }
                else
                {
                    switch (mdl.IsLock)
                    {
                        case (byte)EnumCusState.ForLock:
                            context.Put("state", "审核中...");
                            break;
                        case (byte)EnumCusState.ForStop:
                            context.Put("state", "被停止...");
                            break;
                        case (byte)EnumCusState.ForUnPass:
                            context.Put("state", "审核未通过...");
                            break;
                    }
                }
            }
            UserInfo info = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("email", info.Email);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            string tel = GetString("tel");
            string mobile = GetString("moblie");
            string username = GetString("username");
            if ((string.IsNullOrEmpty(tel) && string.IsNullOrEmpty(mobile)) || string.IsNullOrEmpty(username))
            {
                context.Put("errors", "姓名、联系方式必须填写(电话、手机可任选一个)");
            }
            else
            {
                AppDeveloperInfo info = new AppDeveloperInfo();
                info.Email = GetString("email");
                info.Username = GetString("username");
                info.IsLock = (byte)EnumCusState.ForLock;
                info.JoinTime = DateTime.Now;
                info.Mobile = mobile;
                info.Tel = tel;
                info.Userid = this.UserID;
                info.Userkey = Input.MD5(System.Guid.NewGuid().ToString(), false);
                int n = JuSNS.App.App.Instance.InsertDev(info);
                if (n > 0)
                {
                    context.Put("rights", "申请成功，等待管理员审核");
                }
                else
                {
                    context.Put("errors", "发生错误");
                }

            }
            ShowInfo(ref context);
        }
    }
}
