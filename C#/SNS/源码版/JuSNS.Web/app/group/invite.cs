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
    public class invite : UserPage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
                ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.UserID;
            base.Page_Load(ref context);
            int gid = GetInt("gid", 0);
            bool isMember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
            GroupInfo mdl = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
            context.Put("groupname", groupname);
            context.Put("gid", gid);
            context.Put("cpagetitle", mdl.GroupName + groupname);
            context.Put("portrait", Public.GetGroupPortrait(mdl.Portrait));
            context.Put("GroupName", mdl.GroupName);
            context.Put("Bulletin", mdl.Bulletin);
            context.Put("topiccount", JuSNS.Home.App.Group.Instance.GetGroupTopicCount(gid, 1));
            context.Put("albumcount", JuSNS.Home.App.Group.Instance.GetGroupAlbumCount(gid));
            context.Put("filescount", JuSNS.Home.App.Group.Instance.GetGroupFilesCount(gid));
            context.Put("membercount", JuSNS.Home.App.Group.Instance.GetGroupMemberCount(gid));
            context.Put("ativecount", JuSNS.Home.App.Group.Instance.GetGroupAtiveCount(gid));
            if (!isMember)
            {
                context.Put("errors", "你不是群成员，不能邀请。");
            }
            bool isgroupadmin = JuSNS.Home.App.Group.Instance.isGroupAdmin(gid, uid);
            if (!isgroupadmin)
            {
                context.Put("errors", "你不是群管理员，不能邀请。");
            }
            frdlists(ref context);
            frdclasslist(ref context);
        }

        protected void frdclasslist(ref VelocityContext context)
        {
            DataTable dt = JuSNS.Home.User.User.Instance.GetFriendClass(this.UserID);
            List<Hashtable> frdclist = new List<Hashtable>();
            int id = GetInt("q", 0);
            FriendInfo frdcinfo = JuSNS.Home.User.User.Instance.GetFriendInfo(id);
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frdc = new Hashtable();
                frdc.Add("id", dr["id"]);

                if (id == Convert.ToInt32(dr["id"]))
                {
                    frdc.Add("css", " class=\"current\"");
                }
                else
                {
                    frdc.Add("css", string.Empty);
                }
                frdc.Add("cname", dr["cname"]);
                frdclist.Add(frdc);
            }
            dt.Dispose();
            context.Put("frdclist", frdclist);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string friendid = GetString("friendid");
            int gid = GetInt("gid", 0);
            if (string.IsNullOrEmpty(friendid))
            {
                context.Put("errors", "请至少选择一个好友。");
            }
            else
            {
                string[] frdidARR = friendid.Split(',');
                int m = 0;
                for (int i = 0; i < frdidARR.Length; i++)
                {
                    int rsult = JuSNS.Home.App.Group.Instance.InviteFriend(this.UserID, Convert.ToInt32(frdidARR[i]), gid);
                    if (rsult > 0)
                    {
                        m++;
                    }
                }
                context.Put("rights", "邀请成功！一共邀请了"+m+"个");
            }
            ShowInfo(ref context);
        }

        protected void frdlists(ref VelocityContext context)
        {
            int recount = 140;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            int gid = GetInt("gid", 0);
            DataTable dt = null;
            SqlConditionInfo[] st = null;
            int classid = GetInt("q", 0);
            string JoinMember = JuSNS.Home.App.Group.Instance.GetMemberList(gid);
            dt = JuSNS.Home.UtilPage.GetFriendPage(Input.FixCommaStr(JoinMember), this.UserID, classid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> frdlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frd = new Hashtable();
                frd.Add("truename", dr["truename"]);
                frd.Add("id", dr["id"]);
                frd.Add("friendid", dr["friendid"]);
                frdlist.Add(frd);
            }
            dt.Dispose();
            context.Put("frdlist", frdlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
