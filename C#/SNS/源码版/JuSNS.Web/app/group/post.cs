using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class post : UserPage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            //获得您最新发布的帖子ID
            string action = GetString("action");
            if (action == "maxid")
            {
                int gid = GetInt("gid", 0);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Write(JuSNS.Home.App.Group.Instance.GetMaxTopicForUser(gid, this.GetUserID()));
                HttpContext.Current.Response.End();
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.GetUserID();
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
                context.Put("errors", "你不是群成员，不能发表主题。");
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string content = GetString("txtcontent");
            string title = GetString("txttile");
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
                    mdl.Title = title;
                    mdl.TopicID = tid;
                    mdl.UserID = uid;
                    int m = JuSNS.Home.App.Group.Instance.InsertTopic(mdl);
                    if (m > 0)
                    {
                        if (islock == 1)
                        {
                            n = 2;
                        }
                        else
                        {
                            //插入动态
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, gid, (int)EnumDynType.CreatTopic, string.Empty, DateTime.Now, m, string.Empty));
                            //更新积分
                            JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(10), 0, 0, "发表了话题");
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
