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
    public class ative : BasePage
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
            if (uid == 0 && userid == 0)
            {
                context.Put("redirecturl", root + "/library/page/error" + ExName + "?return=false&urls=" + root + "/&error=" + JuSNS.Common.Input.URLEncode("错误的参数"));
            }
            else
            {
                if (uid == 0) uid = userid;
                UserInfo userinfo = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                if (userinfo != null)
                {
                    JuSNS.Home.User.User.Instance.UpdateUserState(uid, userid);
                    UserBaseInfo basic = JuSNS.Home.User.User.Instance.GetUserBaseInfo(uid);
                    UserSettingInfo stinfo = JuSNS.Home.User.User.Instance.GetUserSettingInfo(uid);
                    bool isPrivacy = JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivSpace, uid, userid);
                    if (stinfo == null) isPrivacy = true;
                    if (!JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivSpace, uid, userid))
                    {
                        context.Put("redirecturl", root + "/user/search" + ExName + "?uid=" + uid + "&q=" + stinfo.PrivSpace);
                    }
                    else
                    {
                        string filepath = "~/space/info/my/user" + uid + ".config";
                        string userspace = Public.GetXMLValue("space", filepath);
                        if (string.IsNullOrEmpty(userspace)) userspace = "default";
                        context.Put("cpagetitle", "活动 - " + userinfo.TrueName + "的空间");
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
                        if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivFavourite, uid, userid))
                        {
                            context.Put("sex", userinfo.Sex == 0 ? "男" : "女");
                            context.Put("constellation", JuSNS.Home.Other.Constellation.Instance.GetInfo(basic.Constellation).Constellation);
                            context.Put("birthday", Public.GetBirthday(basic.Birthday, basic.BirthidayDisplay));
                            context.Put("add", JuSNS.Home.Other.Area.Instance.GetAreaInfo(userinfo.ProvinceID).Name + JuSNS.Home.Other.Area.Instance.GetAreaInfo(userinfo.City).Name);
                            context.Put("home", JuSNS.Home.Other.Area.Instance.GetAreaInfo(basic.HomeCity).Name);
                            context.Put("inter", userinfo.Integral);
                            context.Put("interyb", userinfo.Inteyb);
                            context.Put("money", userinfo.Money.ToString("0.00"));
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
                        if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivMiniBlog, uid, userid)) this.TwitterList(ref context, uid);
                        this.GiftList(ref context, uid);
                        if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivEducate, uid, userid))
                        {
                            this.Edulist(ref context, uid);
                            this.Worklist(ref context, uid);
                        }
                    }
                    ShowList(ref context, uid);
                }
            }
        }

        protected void TwitterList(ref VelocityContext context, int uid)
        {
            int recount = 10;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", Convert.ToInt32(uid), TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_twitter_UserID_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> twitlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable twit = new Hashtable();
                twit.Add("twitterurl", Public.URLWrite(dr["id"], "twitter"));
                twit.Add("content", Input.ReplaceSmaile(Input.FilterHTML(dr["content"])));
                twit.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["posttime"])));
                twitlist.Add(twit);
            }
            dt.Dispose();
            context.Put("twitlist", twitlist);
            context.Put("twitrecordcount", ReCount);
        }

        public void Edulist(ref VelocityContext context, int uid)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_edu_aspx", PageIndex, 6, out ReCount, out PgCount, new SqlConditionInfo("@UserID", uid, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> edulist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable edu = new Hashtable();
                edu.Add("schoolname", dr["schoolname"]);
                if (dr["leaveyear"].ToString() == "0")
                {
                    edu.Add("leaveyear", "在读");
                }
                else
                {
                    edu.Add("leaveyear", dr["leaveyear"]);
                }
                edu.Add("levels", Public.GetLevel(dr["levels"]));
                edu.Add("levelsflag", dr["levels"]);
                edulist.Add(edu);
            }
            dt.Dispose();
            context.Put("edulist", edulist);
            context.Put("edurecordcount", ReCount);
        }

        public void Worklist(ref VelocityContext context, int uid)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;

            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_career_aspx", PageIndex, 6, out ReCount, out PgCount, new SqlConditionInfo("@UserID", uid, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> careerlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable career = new Hashtable();
                career.Add("company", dr["company"]);
                if (dr["leavetime"].ToString() == "0")
                {
                    career.Add("leavetime", "在职");
                }
                else
                {
                    career.Add("leavetime", dr["leavetime"]);
                }
                careerlist.Add(career);
            }
            dt.Dispose();
            context.Put("careerlist", careerlist);
            context.Put("careerrecordcount", ReCount);
        }

        protected void GiftList(ref VelocityContext context, int uid)
        {
            int recount = 8;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            SqlConditionInfo[] st = null; DataTable dt = null;
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_giftmy_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> giftlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable gift = new Hashtable();
                gift.Add("id", dr["id"]);
                gift.Add("giftname", dr["giftname"]);
                gift.Add("truename", dr["truename"]);
                gift.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                string giftPath = Public.GetXMLGiftValue("picPath");
                gift.Add("giftpic", giftPath + "/" + dr["pic"]);
                gift.Add("time", Public.getTimeEXTSpan(Convert.ToDateTime(dr["PostTime"])));
                giftlist.Add(gift);
            }
            dt.Dispose();
            context.Put("giftlist", giftlist);
            context.Put("giftrecordcount", ReCount);
        }

        protected void ShowList(ref VelocityContext context, int uid)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("persionnumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            DataTable dt = JuSNS.Home.UtilPage.GetAtivePage(uid.ToString(), string.Empty, 0, this.GetUserID(), PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("ativeurl", Public.URLWrite(dr["id"], "ative"));
                info.Add("userid", dr["userid"]);
                string states = string.Empty;
                DateTime ntime = DateTime.Now;
                DateTime etime = Convert.ToDateTime(dr["EndTime"]);
                if ((ntime - etime).Days > 0)
                {
                    states = "<span class=\"green\">[已结束]</span>";
                }
                info.Add("state", states);
                info.Add("ativename", dr["ativename"]);
                info.Add("members", dr["members"]);
                info.Add("starttime", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["starttime"])));
                info.Add("endtime", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["endtime"])));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == this.GetUserID() || isadmin > 0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + this.GetUserID() + ",'ative')\" class=\"showok1\" title=\"删除\"></a>";
                if (Convert.ToInt32(dr["userid"]) == uid)
                {
                    info.Add("showedite", true);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToBoolean(dr["isrec"]))
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + this.GetUserID() + ",0,'ative')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + this.GetUserID() + ",1,'ative')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    info.Add("showrec", string.Empty);
                }
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
