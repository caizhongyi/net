using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.blog
{
    public class info :BasePage
    {
        public int uid = 0;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            uid = GetUserID();
            int bid = GetInt("bid", 0);
            JuSNS.Home.App.Blog.Instance.UpdateBlogState(bid, uid);
            BlogInfo mdl = JuSNS.Home.App.Blog.Instance.GetBlogInfo(0, bid);
            bool isRead = false;
            if (mdl.UserID != uid)
            {
                if (mdl.IsLock == true)
                {
                    isRead = true;
                    context.Put("errors", "错误的参数");
                }
            }
            if (!isRead)
            {
                bool isPrivacy = JuSNS.Home.User.User.Instance.CheckPrivacy(mdl.Privacy, mdl.UserID, uid);
                if (!isPrivacy)
                {
                    PageError("隐私设置，您无法查看！", "javascript:history.back();");
                    //context.Put("redirecturl", root + "/user/search" + ExName + "?uid=" + uid + "&q=" + stinfo.PrivSpace);
                }
                else
                {
                    context.Put("cpagetitle", mdl.Title + " - 博客日志");
                    context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                    context.Put("headpic", this.GetHeadImage(mdl.UserID));
                    context.Put("userid", mdl.UserID);
                    context.Put("id", bid);
                    context.Put("title", mdl.Title);
                    context.Put("click", mdl.Click);
                    context.Put("att", mdl.Attnumber);
                    context.Put("content", mdl.Content);
                    context.Put("comments", mdl.Comments);
                    context.Put("time", mdl.PostTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    context.Put("truename", JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID).TrueName);
                    ShowCommentList(ref context, bid);
                    ShowTrackList(ref context, bid);
                    ShowHotList(ref context, bid);
                }
            }
        }

        protected void ShowCommentList(ref VelocityContext context,int bid)
        {
            int recount = Convert.ToInt32(Public.GetXMLBaseValue("BlogCommentNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@blogID", bid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_blog_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> commlist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable comm = new Hashtable();
                comm.Add("id", dr["id"]);
                comm.Add("userid", dr["userid"]);
                comm.Add("truename", dr["truename"]);
                comm.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                comm.Add("headpic", this.GetHeadImage(dr["userid"]));
                comm.Add("time", Public.getTimeSpan(Convert.ToDateTime(dr["posttime"])));
                string ReplaySTR = string.Empty;
                if (Convert.ToInt32(dr["commid"]) > 0)
                {
                    BlogCommentInfo bml = JuSNS.Home.App.Blog.Instance.GetBlogCommentInfo(Convert.ToInt32(dr["commid"]));
                    ReplaySTR = "<div class=\"ctxreplay\"><b>" + bml.TrueName + "</b>：" + Input.ReplaceSmaile(bml.Content) + "</div>";
                }
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"].ToString()));
                bool isOp = false; 
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != uid)
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["blogid"] + "," + uid + ",'" + dr["truename"] + "','blog');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deletetblogcomment(" + dr["id"] + "," + uid + ")\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        protected void ShowTrackList(ref VelocityContext context, int bid)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLBaseValue("Blogtrack"));
            List<BlogFootInfo> Infolist = JuSNS.Home.App.Blog.Instance.GetBlogFoot(recount, bid);
            List<Hashtable> tracklist = new List<Hashtable>();
            foreach (BlogFootInfo info in Infolist)
            {
                Num++;
                Hashtable track = new Hashtable();
                track.Add("userid", info.UserID);
                track.Add("truename", info.TrueName);
                track.Add("spaceurl", this.GetSpaceURL(info.UserID));
                track.Add("headpic", this.GetHeadImage(info.UserID));
                track.Add("time", Public.getTimeEXTSpan(info.CreatTime));
                tracklist.Add(track);
            }
            context.Put("tracklist", tracklist);
            context.Put("trackcount", Num);
        }

        protected void ShowHotList(ref VelocityContext context, int bid)
        {
            int Num = 0;
            int BlogHotNumber = Convert.ToInt32(Public.GetXMLBaseValue("BlogHotNumber"));
            List<BlogInfo> Infolist = JuSNS.Home.App.Blog.Instance.GetBlogList(BlogHotNumber, 0, 0);
            List<Hashtable> hotlist = new List<Hashtable>();
            foreach (BlogInfo info in Infolist)
            {
                Num++;
                Hashtable hot = new Hashtable();
                hot.Add("title", info.Title);
                hot.Add("blogurl", Public.URLWrite(info.ID, "blog"));
                hotlist.Add(hot);
            }
            context.Put("hotlist", hotlist);
            context.Put("hotcount", Num);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string commentcontent = GetString("commentcontent");
            int bid = GetInt("bid", 0);
            BlogCommentInfo mdl = new BlogCommentInfo();
            mdl.BlogID = bid;
            mdl.CommID = 0;
            mdl.Content = commentcontent;
            int BlogCommentCheck = Convert.ToInt32(Public.GetXMLBaseValue("BlogCommentCheck"));
            mdl.IsLock = BlogCommentCheck == 0 ? false : true;
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            mdl.UserID = this.GetUserID();
            int n = JuSNS.Home.App.Blog.Instance.InsertBlogComment(mdl);
            if (n > 0)
            {
                if (BlogCommentCheck == 1)
                {
                    //PageRight("发布日志评论成功，但是需要审核才能显示", System.Web.HttpContext.Current.Request.Url.ToString(), true);
                    context.Put("rights", "发布日志评论成功。需要审核才能显示！");
                }
                else
                {
                    //增加通知和动态
                    if (GetInt("userid", 0) != this.GetUserID())
                    {
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "评论了你的日志", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyBlog, bid));
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.BlogComment, string.Empty, DateTime.Now, bid, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(4), 0, 0, "评论日志");
                    }
                    //PageRight("发布日志评论成功", System.Web.HttpContext.Current.Request.Url.ToString(), true);
                    context.Put("rights", "发布日志评论成功");
                }
            }
            else
            {
                context.Put("errors", "发布日志评论失败");
            }
            ShowInfo(ref context);
        }
    }
}