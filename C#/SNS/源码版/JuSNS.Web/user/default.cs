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
    public class @default : BasePage
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
                    if (userinfo.State == (byte)EnumUserState.Lock)
                    {
                        context.Put("redirecturl", root + "/library/page/error" + ExName + "?return=false&urls=" + root + "/&error=" + JuSNS.Common.Input.URLEncode("此会员已被锁定"));
                    }
                    else
                    {
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
                            this.Visitlist(ref context, uid);
                            if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivFriends, uid, userid))
                            {
                                this.Frdlists(ref context, uid);
                            }
                            else
                            {
                                context.Put("friendvisiable", "1");
                            }
                            if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivMiniBlog, uid, userid)) this.TwitterList(ref context, uid);
                            this.GiftList(ref context, uid);
                            this.ShowList(ref context, uid, userid);
                            this.BlogList(ref context, uid);
                            this.AlbumList(ref context, uid);
                            if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivEducate, uid, userid))
                            {
                                this.Edulist(ref context, uid);
                                this.Worklist(ref context, uid);
                            }
                            if (JuSNS.Home.User.User.Instance.CheckPrivacy(stinfo.PrivLeaveWord, uid, userid))
                            {
                                this.GbookList(ref context, uid);
                            }
                            else
                            {
                                context.Put("gbookvisiable", "1");
                            }
                        }
                    }
                }
            }
        }

        protected void Visitlist(ref VelocityContext context,int uid)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            DataTable  dt = JuSNS.Home.UtilPage.GetPage("user_visited_aspx", PageIndex, 30, out ReCount, out PgCount, new SqlConditionInfo("@UserID", uid, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> visitlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable visit = new Hashtable();
                visit.Add("truename", Input.GetSubString(dr["truename"].ToString(), 7));
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
                frd.Add("truename", Input.GetSubString(dr["truename"].ToString(), 7));
                frd.Add("id", dr["id"]);
                frd.Add("spaceurl", this.GetSpaceURL(dr["friendid"]));
                frd.Add("userhead", this.GetHeadImage(dr["friendid"], 1));
                frdlist.Add(frd);
            }
            dt.Dispose();
            context.Put("frdlist", frdlist);
        }

        protected void TwitterList(ref VelocityContext context,int uid)
        {
            int recount = 10;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            SqlConditionInfo[] st =  new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetTwitterPage(uid.ToString(), string.Empty, uid, PageIndex, recount, out ReCount, out PgCount, st);
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

        protected void ShowList(ref VelocityContext context,int uid,int userid)
        {
            string listSTR = string.Empty;
            int recount = 15;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = JuSNS.Home.UtilPage.GetDynAllPage(uid, string.Empty, string.Empty, PageIndex, recount, out ReCount, out PgCount);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            string gDateSTR = string.Empty;
            bool todaytf = false;
            bool yesdaytf = false;
            bool longtf = false;
            bool longlongtf = false;
            bool isadmin = JuSNS.Home.User.User.Instance.IsAdmin(this.GetUserID());
            foreach (DataRow dr in dt.Rows)
            {
                DynInfo dyninfo = new DynInfo();
                dyninfo.Content = Convert.ToString(dr["content"]);
                dyninfo.CUserID = Convert.ToInt32(dr["CUserID"]);
                dyninfo.DynType = Convert.ToInt32(dr["DynType"]);
                dyninfo.ID = Convert.ToInt32(dr["ID"]);
                dyninfo.Infoarr = Convert.ToInt32(dr["Infoarr"]);
                dyninfo.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                dyninfo.TrueName = Convert.ToString(dr["TrueName"]);
                dyninfo.UserID = Convert.ToInt32(dr["UserID"]);
                Hashtable info = new Hashtable();
                string TmpUserLogStr = JuSNS.MVC.ParseUserLog.Instance.Parse(dyninfo, userid);
                if (!string.IsNullOrEmpty(TmpUserLogStr))
                {
                    DateTime nday = DateTime.Now;
                    string ShowDateSTR = string.Empty;
                    if (gDateSTR != Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd"))
                    {
                        if (Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd") == nday.ToString("MM-dd")) { if (!todaytf) { ShowDateSTR = "<li class=\"dyncli\">今天</li>"; todaytf = true; } }
                        else if (Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd") == (nday.AddDays(-1)).ToString("MM-dd")) { if (!yesdaytf) { ShowDateSTR = "<li class=\"dyncli\">昨天</li>"; yesdaytf = true; } }
                        else if (Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd") == (nday.AddDays(-2)).ToString("MM-dd")) { if (!longtf) { ShowDateSTR = "<li class=\"dyncli\">前天</li>"; longtf = true; } }
                        else { if (!longlongtf) { ShowDateSTR = "<li class=\"dyncli\">以前的动态</li>"; longlongtf = true; } }
                    }
                    else
                    {
                        ShowDateSTR = string.Empty;
                    }
                    string deleteSTR = string.Empty;
                    if (isadmin)
                    {
                        deleteSTR = "<a href=\"javascript:;\" title=\"删除此动态\" onclick=\"deleteAll(" + dr["ID"] + "," + this.GetUserID() + ",'dyn')\" class=\"showok1\"></a>";
                    }
                    TmpUserLogStr = TmpUserLogStr.Replace("{ColseDyn}", deleteSTR);
                    listSTR+=ShowDateSTR + "<li id=\"param_" + Convert.ToInt32(dr["ID"]) + "\">" + TmpUserLogStr + "</li>";
                }
            }
            dt.Dispose();
            context.Put("dynlist", listSTR);
        }

        protected void GiftList(ref VelocityContext context,int uid)
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

        public void BlogList(ref VelocityContext context, int uid)
        {
            int recount = 5;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
            DataTable dt = JuSNS.Home.UtilPage.GetBlogPage(uid.ToString(), string.Empty, 0, string.Empty, 0, PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> bloglist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable blog = new Hashtable();
                blog.Add("id", dr["id"]);
                blog.Add("userid", dr["userid"]);
                blog.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                blog.Add("headpic", this.GetHeadImage(dr["userid"]));
                blog.Add("title", dr["title"]);
                blog.Add("clicks", dr["click"]);
                blog.Add("comments", dr["Comments"]);
                
                blog.Add("blogurl", Public.URLWrite(dr["id"], "blog"));
                blog.Add("content", Input.GetSubString(Input.FilterHTML(dr["content"]), 160));
                blog.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["postTime"])));
                bloglist.Add(blog);
            }
            dt.Dispose();
            context.Put("bloglist", bloglist);
            context.Put("blogrecordcount", ReCount);
        }

        public void AlbumList(ref VelocityContext context, int uid)
        {
            int recount = 5;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = 1;
             SqlConditionInfo[] st = null;
            DataTable dt = JuSNS.Home.UtilPage.GetAlbumPage(uid.ToString(), string.Empty, 0, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> albumlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable album = new Hashtable();
                album.Add("albumid", dr["albumid"]);
                album.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["CreateTime"])));
                string images = this.GetSmallPic(JuSNS.Home.App.Album.Instance.CoverPath((int)dr["AlbumID"]), 1);
                album.Add("pic", images);
                albumlist.Add(album);
            }
            dt.Dispose();
            context.Put("albumlist", albumlist);
            context.Put("albumrecordcount", ReCount);
        }

        public void Edulist(ref VelocityContext context,int uid)
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

        public void Worklist(ref VelocityContext context,int uid)
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

        protected void GbookList(ref VelocityContext context, int uid)
        {
            int recount = 10;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            DataTable dt = null;
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_gbook_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> gbooklist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable gbook = new Hashtable();
                gbook.Add("id", dr["id"]);
                gbook.Add("content", Input.ReplaceSmaile(dr["content"]));
                gbook.Add("truename", dr["truename"]);
                gbook.Add("spaceurl", this.GetSpaceURL(dr["sendid"]));
                gbook.Add("headpic", this.GetHeadImage(dr["sendid"], 2));
                gbook.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                gbooklist.Add(gbook);
            }
            dt.Dispose();
            context.Put("gbooklist", gbooklist);
        }
    }
}