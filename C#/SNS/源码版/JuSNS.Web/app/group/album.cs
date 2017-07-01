using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.group
{
    public class album : UserPage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public string gName = string.Empty;
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
            context.Put("cpagetitle", mdl.GroupName + "群相册");
            context.Put("portrait", Public.GetGroupPortrait(mdl.Portrait));
            context.Put("GroupName", mdl.GroupName);
            gName = mdl.GroupName;
            context.Put("Bulletin", mdl.Bulletin);
            context.Put("topiccount", JuSNS.Home.App.Group.Instance.GetGroupTopicCount(gid, 1));
            context.Put("albumcount", JuSNS.Home.App.Group.Instance.GetGroupAlbumCount(gid));
            context.Put("filescount", JuSNS.Home.App.Group.Instance.GetGroupFilesCount(gid));
            context.Put("membercount", JuSNS.Home.App.Group.Instance.GetGroupMemberCount(gid));
            context.Put("ativecount", JuSNS.Home.App.Group.Instance.GetGroupAtiveCount(gid));
            ShowList(ref context, gid);
        }

        public void ShowList(ref VelocityContext context, int gid)
        {
            int recount = 10;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@GroupID", gid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_groupalbum_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("title", dr["title"]);
                info.Add("albumid", dr["albumid"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("userid", dr["userid"]);
                info.Add("desc", dr["Description"]);
                info.Add("piccount", dr["ImagesCount"]);
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["CreateTime"])));
                string editTime = dr["LastUploadTime"].ToString();
                if (Input.IsDate(editTime))
                {
                    info.Add("edittime", Public.getTimeEXPINSpan(Convert.ToDateTime(editTime)));
                }
                if (Convert.ToInt32(dr["userid"]) == this.UserID)
                {
                    info.Add("self", 1);
                }
                else
                {
                    info.Add("self", 0);
                }
                string images = this.GetSmallPic(JuSNS.Home.App.Album.Instance.CoverPath((int)dr["AlbumID"]), 1);
                info.Add("pic", images);
                info.Add("picofalbum", photosOfAlbum((int)dr["AlbumID"], (int)dr["UserID"]));
                if (Convert.ToInt32(dr["userid"]) == this.UserID || isadmin >0)
                {
                    isOp = true;
                }
                if (isOp)
                {
                    info.Add("showop", "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["albumid"] + "," + this.UserID + ",'album')\" class=\"showok1\"></a>");
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

        /// <summary>
        /// 相册中的照片
        /// </summary>
        /// <param name="AlbumID">相册ID</param>
        /// <returns></returns>
        protected string photosOfAlbum(int AlbumID, int UserID)
        {
            StringBuilder sb = new StringBuilder();
            List<PhotoInfo> infolist = JuSNS.Home.App.Album.Instance.InfoList(AlbumID, 0, 8);
            if (infolist.Count > 0)
            {
                sb.Append("<table class=\"photo-recent\">");
                sb.Append("<tr>");
                for (int i = 0; i < infolist.Count; i++)
                {
                    sb.Append("<td><a href=\""+root+"/app/album/photoview"+ExName+"?pid=" + infolist[i].Id + "\">" + JuSNS.MVC.MVCPublic.ZoomPic(this.GetSmallPic(infolist[i].FilePath, 0), 0, 55) + "</a></td>");
                }
                sb.Append("<td>&nbsp;<a href=\"" + root + "/app/album/albumview" + ExName + "?aid=" + AlbumID + "&gid=" + GetInt("gid", 0) + "&groupname=" + gName + "\">浏览更多&raquo;</a></td>");
                sb.Append("</tr></table>");
            }
            return sb.ToString();
        }
    }
}
