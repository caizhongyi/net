using System;
using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class group : BasePage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid=this.GetUserID();
            base.Page_Load(ref context);
            int IsLight = Convert.ToInt32(Public.GetXMLGroupValue("IsLight"));
            if (IsLight == 1) context.Put("islight", IsLight);
            int gid = GetInt("gid", 0);
            GroupInfo mdl = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
            if (mdl == null)
            {
                PageError("错误的参数", root + "/");
            }
            else
            {
                context.Put("groupname", groupname);
                context.Put("gid", gid);
                context.Put("cpagetitle", mdl.GroupName);
                context.Put("portrait", Public.GetGroupPortrait(mdl.Portrait));
                context.Put("GroupName", mdl.GroupName);
                context.Put("Bulletin", mdl.Bulletin);
                bool isMember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
                if (isMember) context.Put("ismember", true);
                bool isgroupadmin = JuSNS.Home.App.Group.Instance.isGroupAdmin(gid, uid);
                if (isgroupadmin)
                {
                    context.Put("isadmin", true);
                }
                context.Put("topiccount", JuSNS.Home.App.Group.Instance.GetGroupTopicCount(gid, 1));
                ShowTopList(ref context, gid);
                ShowMemberList(ref context, gid);
                ShowMemberList1(ref context, gid);
                context.Put("albumcount", JuSNS.Home.App.Group.Instance.GetGroupAlbumCount(gid));
                context.Put("filescount", JuSNS.Home.App.Group.Instance.GetGroupFilesCount(gid));
                context.Put("membercount", JuSNS.Home.App.Group.Instance.GetGroupMemberCount(gid));
                context.Put("ativecount", JuSNS.Home.App.Group.Instance.GetGroupAtiveCount(gid));
            }
        }

        protected void ShowTopList(ref VelocityContext context, int gid)
        {
            int recount = Convert.ToInt32(Public.GetXMLGroupValue("GroupIndexNumber"));
            List<GroupTopicInfo> Ilist = JuSNS.Home.App.Group.Instance.GetGroupTopicList(recount, gid);
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (GroupTopicInfo ifo in Ilist)
            {
                Hashtable info = new Hashtable();
                info.Add("id", ifo.Id);
                info.Add("userid", ifo.UserID);
                string tSTR = string.Empty;
                if (ifo.IsTop)
                {
                    tSTR += "<span class=\"istop\">顶</span>";
                }
                if (ifo.IsBest == 1)
                {
                    tSTR += "<span class=\"isbest\">精</span>";
                }
                info.Add("headpic", this.GetHeadImage(ifo.UserID, 0));
                info.Add("spaceurl", this.GetSpaceURL(ifo.UserID));
                info.Add("topicurl", Public.URLWrite(ifo.Id,"topic"));
                info.Add("truename", ifo.TrueName);
                info.Add("title", tSTR + Input.GetSubString(ifo.Title, 46));
                info.Add("clicks", ifo.Clicks);
                info.Add("replynumber", ifo.Replynumber);
                if (ifo.LastpostTime != null && Input.IsDate(ifo.LastpostTime.ToString()))
                {
                    info.Add("time", Public.getTimeEXPINSpan(ifo.LastpostTime));
                }
                else
                {
                    info.Add("time", Public.getTimeEXPINSpan(ifo.Posttime));
                }
                infolist.Add(info);
            }
            context.Put("infolist", infolist);
        }

        protected void ShowMemberList(ref VelocityContext context, int gid)
        {
            int recount = Convert.ToInt32(Public.GetXMLGroupValue("GroupMemberNumber"));
            List<GroupMemberInfo> Ilist = JuSNS.Home.App.Group.Instance.GetGroupMemberList(recount, gid, 0);
            List<Hashtable> memlist = new List<Hashtable>();
            foreach (GroupMemberInfo ifo in Ilist)
            {
                Hashtable mem = new Hashtable();
                mem.Add("id", ifo.ID);
                mem.Add("userid", ifo.UserID);
                mem.Add("headpic", this.GetHeadImage(ifo.UserID, 1));
                mem.Add("spaceurl", this.GetSpaceURL(ifo.UserID));
                mem.Add("truename", ifo.TrueName);
                memlist.Add(mem);
            }
            context.Put("memlist", memlist);
        }

        protected void ShowMemberList1(ref VelocityContext context, int gid)
        {
            int recount = Convert.ToInt32(Public.GetXMLGroupValue("GroupMemberNumber"));
            List<GroupMemberInfo> Ilist = JuSNS.Home.App.Group.Instance.GetGroupMemberList(recount, gid, 1);
            List<Hashtable> mem1list = new List<Hashtable>();
            foreach (GroupMemberInfo ifo in Ilist)
            {
                Hashtable mem1 = new Hashtable();
                mem1.Add("id", ifo.ID);
                mem1.Add("userid", ifo.UserID);
                mem1.Add("headpic", this.GetHeadImage(ifo.UserID, 1));
                mem1.Add("spaceurl", this.GetSpaceURL(ifo.UserID));
                mem1.Add("truename", ifo.TrueName);
                mem1list.Add(mem1);
            }
            context.Put("mem1list", mem1list);
        }
    }
}
