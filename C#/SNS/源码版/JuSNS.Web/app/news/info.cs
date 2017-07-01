using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Drawing;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.news
{
    public class info : BasePage
    {
        public int uid = 0;
        public string contentname = Public.GetXMLValue("news", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            uid = GetUserID();
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("contentname", contentname);
            int nid = GetInt("nid", 0);
            JuSNS.Home.App.News.Instance.UpdateNewsState(nid);
            NewsInfo mdl = JuSNS.Home.App.News.Instance.GetNewsInfo(nid);
            bool isRead = false;
            if (mdl.UserID != GetUserID())
            {
                if (mdl.IsLock == 1)
                {
                    isRead = true;
                    context.Put("errors", "错误的参数");
                }
            }
            else
            {
                context.Put("showop", "| <a href=\"new" + ExName + "?nid=" + nid + "\">编辑</a>");
            }
            if (!isRead)
            {
                context.Put("cpagetitle", mdl.Title + "_" + contentname + "查看");
                if (mdl.UserID > 0)
                {
                    context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                    context.Put("headpic", this.GetHeadImage(mdl.UserID));
                    context.Put("userid", mdl.UserID);
                    context.Put("truename", JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID).TrueName);
                }
                context.Put("id", nid);
                context.Put("title", Input.FilterHTML(mdl.Title));
                context.Put("click", mdl.Click);
                context.Put("att", mdl.AttNumber);
                context.Put("content", mdl.Content);
                context.Put("source", mdl.Source);
                context.Put("comments", mdl.Comments);
                context.Put("time", mdl.PostTime.ToString("yyyy-MM-dd HH:mm:ss"));
                string Pic = mdl.Pic;
                if (!string.IsNullOrEmpty(Pic))
                {
                    string strFilePath = Public.GetXMLBaseValue("ContentPath") + "/" + Pic;
                    strFilePath = strFilePath.Replace("//", "/");
                    try
                    {
                        Image pic = Image.FromFile(HttpContext.Current.Server.MapPath(strFilePath));//strFilePath是该图片的绝对路径
                        int intWidth = pic.Width;//长度像素值
                        int intHeight = pic.Height;//高度像素值  
                        string strPic = string.Empty;
                        if (pic.Width > 400)
                        {
                            strPic = "<a href=\"" + strFilePath + "\" target=\"_blank\"><img src=\"" + strFilePath + "\" style=\"width:400px;\" /></a>";
                        }
                        else
                        {
                            strPic = "<img src=\"" + strFilePath + "\" />";
                        }
                        context.Put("pic", strPic);
                    }
                    catch { context.Put("pic", "<img src=\"" + root + "/template/images/deleted.gif\" />"); }
                }
                string Files = mdl.Files;
                if (!string.IsNullOrEmpty(Files))
                {
                    string strFiles = string.Empty;
                    if (mdl.Point > 0 || mdl.GPoint > 0)
                    {
                        if (JuSNS.Home.User.User.Instance.isFilesDownload(this.GetUserID(), nid, 0))
                        {
                            strFiles = "<a href=\"readfiles" + ExName + "?nid=" + nid + "\">下载此" + contentname + "附件</a>";
                        }
                        else
                        {
                            strFiles = "<a href=\"readfiles" + ExName + "?nid=" + nid + "\" onclick=\"javascript:if (!confirm('下载附件需要" + mdl.Point + "个积分，" + mdl.GPoint + "个" + Public.GetXMLValue("gName") + "。')) return false;\">下载此" + contentname + "附件【需要" + mdl.Point + "个积分，" + mdl.GPoint + "个" + Public.GetXMLValue("gName") + "】</a>";
                        }
                    }
                    else
                    {
                        strFiles = "<a href=\"readfiles" + ExName + "?nid=" + nid + "\">下载此" + contentname + "附件</a>";
                    }
                    context.Put("files", strFiles);
                }
                ShowCommentList(ref context, nid);
                ShowHotList(ref context, nid);
            }
            context.Put("islock", mdl.IsLock);
        }

        protected void ShowCommentList(ref VelocityContext context, int nid)
        {
            int recount = Convert.ToInt32(Public.GetXMLBaseValue("ContentCommentNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@NewsID", nid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_news_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
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
                if (Convert.ToInt32(dr["parentid"]) > 0)
                {
                    NewsCommentInfo bml = JuSNS.Home.App.News.Instance.GetNewsCommentInfo(Convert.ToInt32(dr["parentid"]));
                    ReplaySTR = "<div class=\"ctxreplay\"><b>" + bml.TrueName + "</b>：" + Input.ReplaceSmaile(bml.Content) + "</div>";
                }
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"].ToString()));
                bool isOp = false;
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == GetUserID() || isadmin >0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != GetUserID())
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["newsid"] + "," + GetUserID() + ",'" + dr["truename"] + "','news');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deletetnewscomment(" + dr["id"] + "," + GetUserID() + ")\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        protected void ShowHotList(ref VelocityContext context, int bid)
        {
            int Num = 0;
            int ContentHotNumber = Convert.ToInt32(Public.GetXMLBaseValue("ContentHotNumber"));
            List<NewsInfo> Infolist = JuSNS.Home.App.News.Instance.GetNewsList(ContentHotNumber, 0, 0);
            List<Hashtable> hotlist = new List<Hashtable>();
            foreach (NewsInfo info in Infolist)
            {
                Num++;
                Hashtable hot = new Hashtable();
                hot.Add("title", Input.GetSubString(info.Title, 36));
                hot.Add("titles", Input.FilterHTML(info.Title));
                hot.Add("newsurl", Public.URLWrite(info.Id, "news"));
                hotlist.Add(hot);
            }
            context.Put("hotlist", hotlist);
            context.Put("hotcount", Num);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            if (GetInt("txtislock", 0) != 0)
            {
                context.Put("errors", "锁定的" + contentname + "不允许评论");
            }
            else
            {
                string commentcontent = GetString("commentcontent");
                int nid = GetInt("nid", 0);
                NewsCommentInfo mdl = new NewsCommentInfo();
                mdl.NewsID = nid;
                mdl.ParentID = 0;
                mdl.Content = commentcontent;
                byte NewsCommentCheck = Convert.ToByte(Public.GetXMLBaseValue("NewsCommentCheck"));
                mdl.IsLock = NewsCommentCheck;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.UserID = this.GetUserID();
                int n = JuSNS.Home.App.News.Instance.InsertNewsComment(mdl);
                if (n > 0)
                {
                    if (NewsCommentCheck == 1)
                    {
                        context.Put("rights", "发布日志评论成功。但是您的评论需要审核才能显示！");
                    }
                    else
                    {
                        //增加通知和动态
                        if (GetInt("userid", 0) != this.GetUserID()&&GetInt("userid",0)>0)
                        {
                            JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "评论了您的新闻", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyNews, nid));
                            //插入动态
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.NewsComment, string.Empty, DateTime.Now, nid, string.Empty));
                            //更新积分
                            JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(22), 0, 0, "评论新闻");
                        }
                        context.Put("rights", "发布日志评论成功");
                    }
                }
                else
                {
                    context.Put("errors", "发布日志评论成失败");
                }
            }
            ShowInfo(ref context);
        }

    }

}