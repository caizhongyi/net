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
    public class files : UserPage
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
            int filesize = Convert.ToInt32(Public.GetXMLGroupValue("FileSpaceSize"));
            int unfilesize = Convert.ToInt32(JuSNS.Home.App.Group.Instance.GetFilesSize(gid) / 1024);
            context.Put("filesize", filesize);
            context.Put("unfilesize", unfilesize);
            double mods = (double)unfilesize / filesize;
            context.Put("perentsize", Convert.ToInt32(mods * 100));
            
            if (!isMember)
            {
                context.Put("errors", "你不是群成员，不能查看。");
            }
            else
            {
                ShowList(ref context, gid);
            }
        }

        protected void ShowList(ref VelocityContext context, int gid)
        {
            int uid =this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@GroupID", gid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_groupfiles_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            bool isGroupAdmin = JuSNS.Home.App.Group.Instance.isGroupAdmin(gid, uid);
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                string filesname = dr["FileName"].ToString();
                string filesext = filesname.Substring(filesname.LastIndexOf('.') + 1);
                string fileType = "<img src=\"" + root + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/fileico/" + filesext + ".gif\" borer=\"0\" align=\"absmiddle\" onerror=\"this.src='" + root + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/fileico/ot.gif'\" /> ";
                info.Add("filetype", fileType);
                info.Add("headpic", this.GetHeadImage(dr["userid"], 0));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("userid", dr["userid"]);
                info.Add("title", dr["title"].ToString());
                info.Add("size", dr["FileSize"]);
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                string opSTR1 = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0 || isGroupAdmin)
                {
                    isOp = true;
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'groupfiles')\" title=\"删除\" class=\"showok1\"></a>";
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
