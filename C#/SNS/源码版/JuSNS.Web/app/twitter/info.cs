using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.twitter
{
    public class info: BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            ShowInfo(ref context, uid);
        }

        protected void ShowInfo(ref VelocityContext context, int uid)
        {
            base.Page_Load(ref context);
            int tid = GetInt("tid", 0);
            if (tid == 0)
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                TwitterInfo mdl = JuSNS.Home.App.TWrite.Instance.GetTwitterInfo(tid);
                UserInfo useinfo = JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID);
                context.Put("cpagetitle", useinfo.TrueName+"的博客");
                context.Put("truename", useinfo.TrueName);
                context.Put("userid", mdl.UserID);
                string contentSTR = Input.ReplaceSmaile(mdl.Content);
                string Pic = mdl.Pic;
                string Player = string.Empty;
                if (!string.IsNullOrEmpty(Pic) && Pic.IndexOf(".") > -1)
                {
                    string ExPic = root + Public.GetXMLBaseValue("Twitterpath") + "/" + Pic;
                    contentSTR += "<br /><a href=\"" + ExPic + "\" target=\"_blank\"><img src=\"" + ExPic + "\" /></a>";
                }
                if (!string.IsNullOrEmpty(mdl.Media))
                {
                    contentSTR += "<br />" + Public.ShowPlayer(mdl.Media.ToString());
                }

                context.Put("content", contentSTR);
                context.Put("tid", tid);
                context.Put("headpic", this.GetHeadImage(mdl.UserID, 2));
                context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                context.Put("islock", mdl.IsLock);
                ShowCommentList(ref context, tid);
            }

        }

        protected void ShowCommentList(ref VelocityContext context, int tid)
        {
            int uid = this.GetUserID();
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@TID", tid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_twitter_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
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
                if (Convert.ToInt32(dr["CommentID"]) > 0)
                {
                    TwitterCommentInfo bml = JuSNS.Home.App.TWrite.Instance.GetTwitterCommentInfo(dr["CommentID"]);
                    ReplaySTR = "<div class=\"ctxreplay\"><b>" + bml.TrueName + "</b>：" + Input.ReplaceSmaile(bml.Content) + "</div>";
                }
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"]));
                bool isOp = false;
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == GetUserID() || isadmin >0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != GetUserID())
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," +tid + "," + GetUserID() + ",'" + dr["truename"] + "','twitter');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'twittercomment')\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int tid = GetInt("tid", 0);
            if (GetInt("txtislock", 0) != 0)
            {
                context.Put("errors", "锁定的微博不允许评论");
            }
            else
            {
                string commentcontent = GetString("commentcontent");
                TwitterCommentInfo mdl = new TwitterCommentInfo();
                mdl.Tid = tid;
                mdl.CommentID = 0;
                mdl.Content = commentcontent;
                byte TwitterCommentCheck = Convert.ToByte(Public.GetXMLBaseValue("TwitterCommentCheck"));
                mdl.IsLock = TwitterCommentCheck == 1 ? true : false;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.UserID = this.GetUserID();
                int n = JuSNS.Home.App.TWrite.Instance.InserTwitterComment(mdl);
                if (n > 0)
                {
                    if (TwitterCommentCheck == 1)
                    {
                        context.Put("rights", "发布微博评论成功。但是您的评论需要审核才能显示！");
                    }
                    else
                    {
                        //增加通知和动态
                        if (GetInt("userid", 0) != this.GetUserID())
                        {
                            JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "评论了你的微博", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyTwitter, tid));
                            //插入动态
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.TwitterComment, string.Empty, DateTime.Now, tid,string.Empty));
                            //更新积分
                            JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(6), 0, 0, "回复微博");
                        }
                        context.Put("rights", "发布微博评论成功");
                    }
                }
                else
                {
                    context.Put("errors", "发布微博评论成失败");
                }
            }
            ShowInfo(ref context, tid);
        }
    }
}
