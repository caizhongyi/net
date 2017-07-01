using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class member : BasePage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            base.Page_Load(ref context);
            int gid = GetInt("gid", 0);
            //得到是否是群管理员
            bool isAdmin = JuSNS.Home.App.Group.Instance.isGroupAdmin(gid, uid);
            if (isAdmin)
            {
                context.Put("isadmin", 1);
            }
            bool isSuperAdmin = JuSNS.Home.App.Group.Instance.isGroupSuperAdmin(gid, uid);
            if (isSuperAdmin)
            {
                context.Put("issuperadmin", 1);
            }
            GroupInfo mdl = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
            int ispublcs = mdl.Publics;
            if (ispublcs == 1)
            {
                context.Put("publics", 1);
            }
            context.Put("cpagetitle", mdl.GroupName+" - 群成员");
            context.Put("groupname", groupname);
            context.Put("gid", gid);
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
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            int q = GetInt("q", 0);
            context.Put("q", q);
            DataTable dt=null;
            if (q == 0)
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_group_member_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@GroupID", gid, TypeCode.Int32));
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_group_membercheck_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@GroupID", gid, TypeCode.Int32));
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("truename", dr["truename"]);
                info.Add("id", dr["id"]);
                info.Add("userid", dr["userid"]);
                info.Add("grade", dr["grade"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("userhead", this.GetHeadImage(dr["userid"], 1));
                info.Add("twitter", Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["userid"])));
                info.Add("twittermore", Input.LostHTML(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["userid"])));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
