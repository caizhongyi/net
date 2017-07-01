using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class list : BasePage
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
            int gid = GetInt("gid", 0);
            GroupInfo mdl = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
            context.Put("groupname", groupname);
            context.Put("gid", gid);
            context.Put("cpagetitle", mdl.GroupName + groupname);
            context.Put("portrait", Public.GetGroupPortrait(mdl.Portrait));
            context.Put("GroupName", mdl.GroupName);
            context.Put("Bulletin", mdl.Bulletin);
            bool isMember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
            if (isMember) context.Put("ismember", true);
            context.Put("topiccount", JuSNS.Home.App.Group.Instance.GetGroupTopicCount(gid, 1));
            context.Put("albumcount", JuSNS.Home.App.Group.Instance.GetGroupAlbumCount(gid));
            context.Put("filescount", JuSNS.Home.App.Group.Instance.GetGroupFilesCount(gid));
            context.Put("membercount", JuSNS.Home.App.Group.Instance.GetGroupMemberCount(gid));
            context.Put("ativecount", JuSNS.Home.App.Group.Instance.GetGroupAtiveCount(gid));
            ShowList(ref context, gid);
        }

        protected void ShowList(ref VelocityContext context,int gid)
        {
            int uid = this.GetUserID();
            string q = GetString("q");
            int recount = Convert.ToInt32(Public.GetXMLGroupValue("TopicPageNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = null;
            context.Put("q", q);
            if (!string.IsNullOrEmpty(kwd))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                context.Put("cpagetitle", "搜索 " + kwd + " 的" + groupname);
                context.Put("kwd", kwd);
            }
            DataTable dt = JuSNS.Home.UtilPage.GetGroupTopicPage(q, gid, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            bool isGroupAdmin=JuSNS.Home.App.Group.Instance.isGroupAdmin(gid,uid);
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("headpic", this.GetHeadImage(dr["userid"], 0));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("topicurl", Public.URLWrite(dr["id"], "topic"));
                info.Add("userid", dr["userid"]);
                string tSTR = string.Empty;
                if (Convert.ToBoolean(dr["istop"]))
                {
                    tSTR += "<span class=\"istop\">顶</span>";
                }
                if (Convert.ToBoolean(dr["isbest"]))
                {
                    tSTR += "<span class=\"isbest\">精</span>";
                }
                info.Add("title", tSTR + Input.GetSubString(dr["title"].ToString(), 40));
                info.Add("titleall", dr["title"]);
                info.Add("clicks", dr["clicks"]);
                info.Add("replynumber", dr["Replynumber"]);
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["PostTime"])));
                if (Input.IsDate(dr["lastposttime"].ToString()))
                {
                    info.Add("replytime", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["lastposttime"])));
                }
                else
                {
                    info.Add("replytime", "&nbsp;");
                }
                string opSTR = string.Empty;
                string opSTR1 = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin > 0 || isGroupAdmin)
                {
                    isOp = true;
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'grouptopic')\" title=\"删除\" class=\"showok1\"></a>";
                info.Add("showop", opSTR);
                if (isGroupAdmin)
                {
                    if (!Convert.ToBoolean(dr["istop"]))
                    {
                        opSTR1 += "<a href=\"javascript:;\" onclick=\"setgrouptop(" + dr["id"] + "," + uid + ",1)\" title=\"设置固顶\" class=\"showtop\"></a>";
                    }
                    else
                    {
                        opSTR1 += "<a href=\"javascript:;\" onclick=\"setgrouptop(" + dr["id"] + "," + uid + ",0)\" title=\"取消固顶\" class=\"showtop1\"></a>";
                    }
                    if (Convert.ToInt32(dr["isbest"]) == 0)
                    {
                        opSTR1 += "<a href=\"javascript:;\" onclick=\"setgroupbest(" + dr["id"] + "," + uid + ",1)\" title=\"设置精华\" class=\"showbest\"></a>";
                    }
                    else
                    {
                        opSTR1 += "<a href=\"javascript:;\" onclick=\"setgroupbest(" + dr["id"] + "," + uid + ",0)\" title=\"取消精华\" class=\"showbest1\"></a>";
                    }
                }
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'topic')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'topic')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    info.Add("showrec", string.Empty);
                }
                info.Add("showop1", opSTR1);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
