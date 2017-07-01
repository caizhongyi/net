using System;
using System.Web;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class addw : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "添加好友");
            int fid = GetInt("fid", 0);
            context.Put("head", this.GetHeadImage(fid, 1));
            context.Put("fid", fid);
            context.Put("rand", JuSNS.Common.Rand.Number(5));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int fid = GetInt("fid", 0);
            FriendInfo mdl = new FriendInfo();
            mdl.ClassID = 0;
            mdl.Descript = GetString("desc");
            mdl.FDegree = 0;
            mdl.FriendID = fid;
            mdl.PostTime = DateTime.Now;
            mdl.State = 1;
            mdl.UserID = this.UserID;
            int n = 0;
            if (this.UserID != fid)
            {
                n = JuSNS.Home.User.User.Instance.InsertFriend(mdl, 0);
                if (n > 0)
                {
                    context.Put("rights", "添加好友成功，等待对方验证！");
                }
                else
                {
                    if (n == -1)
                    {
                        context.Put("errors", "你已经添加过此好友了！");
                    }
                    else
                    {
                        context.Put("errors", "添加好友失败");
                    }
                }
            }
            else
            {
                context.Put("errors", "不能添加自己为好友");
            }
            ShowInfo(ref context);
        }
    }
}