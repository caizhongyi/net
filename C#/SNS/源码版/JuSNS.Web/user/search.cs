using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.user
{
    public class search : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
            int uid = GetInt("uid", 0);
            int userid = this.GetUserID();
            UserInfo userinfo = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
            string filepath = "~/space/info/my/user" + uid + ".config";
            string userspace = Public.GetXMLValue("space", filepath);
            if (string.IsNullOrEmpty(userspace)) userspace = "default";
            context.Put("cpagetitle", userinfo.TrueName + "的空间");
            context.Put("userspace", userspace);
            context.Put("username", userinfo.TrueName);
            context.Put("spaceurl", this.GetSpaceURL(uid));
            context.Put("userid", uid);
            context.Put("headpic", this.GetHeadImage(uid, 2));
            string profile = string.Empty;
            if (userinfo.IsAdmin > 0)
            {
                profile += "<img src=\"" + root + "/template/" + UiConfig.SkinStyle + "/images/s-icon/admin.gif\" title=\"管理员\" /> ";
            }
            if (userinfo.IsVip)
            {
                profile += "<a href=\"" + root + "/home/vip" + ExName + "\" title=\"VIP会员\"><img src=\"" + root + "/template/" + UiConfig.SkinStyle + "/images/isvip.gif\" /></a> ";
            }
            int memberlevel = Public.GetMemberlevels(userinfo.MemberLevels);
            profile += "<a href=\"" + root + "/home/level" + ExName + "\" title=\"等级：" + memberlevel + "\"><img src=\"" + root + "/template/" + UiConfig.SkinStyle + "/images/level_" + memberlevel + ".gif\" /></a>";
            if (!string.IsNullOrEmpty(profile))
            {
                context.Put("profile", profile);
            }
            context.Put("visitnumber", userinfo.Click);
            int tid = 0;
            string twitter = Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(uid, out tid));
            context.Put("twitter", twitter);
            context.Put("twitterurl", Public.URLWrite(tid, "twitter"));
            if (userid > 0)
            {
                bool isfriend = JuSNS.Home.User.User.Instance.IsFriends(userid, uid);
                if (!isfriend)
                {
                    context.Put("friend", true);
                }
            }
            string q = GetString("q");
            if (q == "1")
            {
                context.Put("popcontentfriend", true);
            }
            else if (q == "2")
            {
                context.Put("popcontent", true);
            }
            this.Visitlist(ref context, uid);
            this.Frdlists(ref context, uid);
        }

        protected void Visitlist(ref VelocityContext context, int uid)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_visited_aspx", PageIndex, 30, out ReCount, out PgCount, new SqlConditionInfo("@UserID", uid, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> visitlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable visit = new Hashtable();
                visit.Add("truename", dr["truename"]);
                visit.Add("spaceurl", this.GetSpaceURL(dr["VisitorID"]));
                visit.Add("userhead", this.GetHeadImage(dr["VisitorID"], 1));
                visitlist.Add(visit);
            }
            dt.Dispose();
            context.Put("visitlist", visitlist);
        }

        protected void Frdlists(ref VelocityContext context, int uid)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_friend_aspx", PageIndex, 30, out ReCount, out PgCount, new SqlConditionInfo("@UserID", uid, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> frdlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frd = new Hashtable();
                frd.Add("truename", dr["truename"]);
                frd.Add("id", dr["id"]);
                frd.Add("spaceurl", this.GetSpaceURL(dr["friendid"]));
                frd.Add("userhead", this.GetHeadImage(dr["friendid"], 1));
                frdlist.Add(frd);
            }
            dt.Dispose();
            context.Put("frdlist", frdlist);
        }
    }
}
