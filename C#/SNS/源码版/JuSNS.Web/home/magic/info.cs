using System;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.magic
{
    public class info : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "详细资料");
            int mid = GetInt("mid", 0);
            MagicInfo mdl = JuSNS.Home.User.User.Instance.GetMagicInfo(mid);
            UserInfo uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("magicpic", root + "/uploads/magic/" + mdl.Pic);
            context.Put("mname", mdl.MName);
            context.Put("mdesc", mdl.Mdesc);
            string mSTR = string.Empty;
            if (UiConfig.Magic != 10)
            {
                int epoint = Convert.ToInt32(mdl.Point * UiConfig.Magic / 10);
                int egpoint = Convert.ToInt32(mdl.Gpoint * UiConfig.Magic / 10);
                mSTR = "(" + UiConfig.Magic + "折优惠后<span class=\"reshow\"> " + epoint + " </span>积分和<span class=\"reshow\">" + egpoint + "</span> " + UiConfig.gName + ")";
            }
            context.Put("mprice", "<span class=\"reshow\">" + mdl.Point + "</span> 积分和<span class=\"reshow\"> " + mdl.Gpoint + " </span>" + UiConfig.gName + mSTR);
            context.Put("mypoints", "<span class=\"reshow\">" + uinfo.Integral + "</span> 积分和<span class=\"reshow\"> " + uinfo.Inteyb + " </span>" + UiConfig.gName);
            context.Put("number", mdl.Number);
        }
    }
}