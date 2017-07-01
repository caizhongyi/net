using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;
using System.Web;
namespace JuSNS.Web.app.group
{
    public class topic : BasePage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            base.Page_Load(ref context);
            int tid = GetInt("tid", 0);
            GroupTopicInfo tinfo = JuSNS.Home.App.Group.Instance.GetTopicInfo(tid);
            int gid = GetInt("gid", 0);
            if (gid == 0)
            {
                gid = tinfo.Groupid;
            }
            GroupInfo mdl = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
            context.Put("groupname", groupname);
            context.Put("gid", gid);
            context.Put("tid", tid);
            JuSNS.Home.App.Group.Instance.UpdateTopicClicks(tid);
            context.Put("portrait", Public.GetGroupPortrait(mdl.Portrait));
            context.Put("GroupName", mdl.GroupName);
            context.Put("Bulletin", mdl.Bulletin);
             context.Put("topiccount", JuSNS.Home.App.Group.Instance.GetGroupTopicCount(gid, 1));
            context.Put("albumcount", JuSNS.Home.App.Group.Instance.GetGroupAlbumCount(gid));
            context.Put("filescount", JuSNS.Home.App.Group.Instance.GetGroupFilesCount(gid));
            context.Put("membercount", JuSNS.Home.App.Group.Instance.GetGroupMemberCount(gid));
            context.Put("ativecount", JuSNS.Home.App.Group.Instance.GetGroupAtiveCount(gid));
            context.Put("cpagetitle", tinfo.Title + "_" + mdl.GroupName);
            context.Put("cpagetitle1", tinfo.Title);
            context.Put("spaceurl", this.GetSpaceURL(tinfo.UserID));
            context.Put("headpic", this.GetHeadImage(tinfo.UserID));
            context.Put("truename", tinfo.TrueName);
            context.Put("userid", tinfo.UserID);
            context.Put("title", tinfo.Title);
            context.Put("content", tinfo.Content);
            context.Put("time", Public.getTimeSpan(tinfo.Posttime));
            string postip = tinfo.PostIP;
            context.Put("ip", postip.Replace(postip.Substring(postip.LastIndexOf(".")), ".*"));
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            bool isOp = false;
            if (tinfo.UserID == uid || isadmin >0) isOp = true;
            if (isOp)
            {
                context.Put("showop", "<a href=\"javascript:;\" onclick=\"deletetopics(" + tid + "," + uid + "," + gid + ")\" class=\"showok1\"></a>");
            }
            else
            {
                context.Put("showop", string.Empty);
            }
            showlist(ref context, tid, uid);
        }

        protected void showlist(ref VelocityContext context, int tid, int uid)
        {
            int recount = Convert.ToInt32(Public.GetXMLGroupValue("topicspage")); ;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@TopicID", tid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_topics_page_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("userid", dr["userid"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"]));
                info.Add("content", dr["content"]);
                info.Add("foolnumber", dr["foolnumber"]);
                info.Add("time", Public.getTimeSpan(Convert.ToDateTime(dr["posttime"])));
                string postip = dr["PostIP"].ToString();
                info.Add("ip", postip.Replace(postip.Substring(postip.LastIndexOf(".")), ".*"));
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp)
                {
                    info.Add("showop", "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'grouptopic')\" class=\"showok1\"></a> <a href=\"javascript:;\" onclick=\"showedits(" + dr["id"] + ")\" class=\"edit1\" title=\"编辑\"></a>");
                }
                else
                {
                    info.Add("showop", string.Empty);
                }
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string content = GetString("txtcontent");
            int tid = GetInt("tid", 0);
            int gid = GetInt("gid", 0);
            int uid = this.GetUserID();
            bool isMember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
            int n = 0;
            if (!isMember)
            {
                n = -2;
            }
            else
            {
                if (uid > 0)
                {
                    GroupTopicInfo mdl = new GroupTopicInfo();
                    mdl.Clicks = 0;
                    mdl.Content = content;
                    mdl.Groupid = gid;
                    mdl.IsBest = 0;
                    byte islock = Convert.ToByte(Public.GetXMLGroupValue("ReplyCheck"));
                    //管理员或者创建发布的帖子不需要审核
                    int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
                    bool isGroupAdmin = JuSNS.Home.App.Group.Instance.isGroupAdmin(gid, uid);
                    if (isadmin >0 || isGroupAdmin)
                    {
                        islock = 0;
                    } 
                    mdl.IsLock = islock;
                    mdl.IsTop = false;
                    mdl.LastpostTime = DateTime.Now;
                    mdl.PostIP = Public.GetClientIP();
                    mdl.Posttime = DateTime.Now;
                    mdl.Replynumber = 0;
                    mdl.Title = string.Empty;
                    mdl.TopicID = tid;
                    mdl.UserID = uid;
                    if (JuSNS.Home.App.Group.Instance.InsertTopic(mdl) > 0)
                    {
                        if (islock == 1)
                        {
                            n = 2;
                        }
                        else
                        {
                            //增加通知和动态
                            int userid = GetInt("userid", 0);
                            //插入动态
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, uid, userid, (int)EnumDynType.TopicComment, string.Empty, DateTime.Now, tid, string.Empty)); 
                            if (userid != uid)
                            {
                                JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, uid, userid, "回复了你的话题", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyTopic, tid));
                                //更新积分
                                JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(11), 0, 0, "回复话题");
                            }
                            n = 1;
                        }
                    }
                    else
                    {
                        n = 0;
                    }
                }
                else
                {
                    n = -1;
                }
            }
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(n);
            HttpContext.Current.Response.End();
        }
    }
}