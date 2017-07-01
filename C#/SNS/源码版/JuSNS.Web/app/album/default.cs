using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.album
{
    /// <summary>
    /// 相册列表
    /// </summary>
    public class @default : BasePage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            string ptitle = string.Empty;
            if (Input.IsInteger(q))
            {
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(q);
                ptitle = mdl.TrueName + "的相册";
                context.Put("quid", q);
                context.Put("ptitle", ptitle);
            }
            else
            {
                switch (q)
                {
                    case "friend":
                        ptitle = "朋友的相册";
                        break;
                    case "my":
                        ptitle = "我的相册";
                        break;
                    default:
                        ptitle = "所有相册";
                        break;
                }
            }
            context.Put("cpagetitle", ptitle);
            ShowList(ref context, q);
            ShowFriendlist(ref context);
        }
        /// <summary>
        /// 显示朋友列表
        /// </summary>
        /// <param name="context"></param>
        public void ShowFriendlist(ref VelocityContext context)
        {
            string listSTR = string.Empty;
            List<FriendInfo> list = JuSNS.Home.User.User.Instance.GetFriendListTop(15, this.GetUserID());
            foreach (FriendInfo info in list)
            {
                if (GetInt("q", 0) == info.FriendID)
                {
                    listSTR += "<li class=\"current\"><a href=\"../album?q=" + info.FriendID + "\">" + info.TrueName + "</a></li>\r\n";
                }
                else
                {
                    listSTR += "<li><a href=\"../album?q=" + info.FriendID + "\">" + info.TrueName + "</a></li>\r\n";
                }
            }
            context.Put("friendlist", listSTR);
        }
        /// <summary>
        /// 显示相册列表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="q"></param>
        public void ShowList(ref VelocityContext context,string q)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLAlbumValue("PageNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = null;
            string FriendSTR = string.Empty;
            context.Put("q", q);
            if (!string.IsNullOrEmpty(kwd))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                context.Put("cpagetitle", "搜索 " + kwd + " 的相册");
            }
            if (q == "friend") FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            context.Put("kwd", kwd);
            DataTable dt = JuSNS.Home.UtilPage.GetAlbumPage(q, FriendSTR, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("title", dr["title"]);
                info.Add("albumid", dr["albumid"]);
                info.Add("albumurl", Public.URLWrite(dr["albumid"], "album"));
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("userid", dr["userid"]);
                info.Add("desc", dr["Description"]);
                info.Add("piccount", dr["ImagesCount"]);
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["CreateTime"])));
                string editTime=dr["LastUploadTime"].ToString();
                if (Input.IsDate(editTime))
                {
                    info.Add("edittime", Public.getTimeEXPINSpan(Convert.ToDateTime(editTime)));
                }
                if (Convert.ToInt32(dr["userid"]) == uid)
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
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin > 0)
                {
                    isOp = true;
                }
                if (isOp)
                {
                    info.Add("showop", "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["albumid"] + "," + uid + ",'album')\" class=\"showok1\"></a>");
                }
                else
                {
                    info.Add("showop", string.Empty);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["albumid"] + "," + uid + ",0,'album')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["albumid"] + "," + uid + ",1,'album')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    info.Add("showrec", string.Empty);
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
            List<PhotoInfo> infolist = JuSNS.Home.App.Album.Instance.InfoList(AlbumID, 0, 6);
            if (infolist.Count > 0)
            {
                sb.Append("<table class=\"photo-recent\">");
                sb.Append("<tr>");
                for (int i = 0; i < infolist.Count; i++)
                {
                    sb.Append("<td><a href=\"" + Public.URLWrite(infolist[i].Id, "photo") + "\">" + JuSNS.MVC.MVCPublic.ZoomPic(this.GetSmallPic(infolist[i].FilePath, 0), 0, 55) + "</a></td>");
                }
                sb.Append("<td>&nbsp;<a href=\"" + Public.URLWrite(AlbumID,"album") + "\">浏览更多&raquo;</a></td>");
                sb.Append("</tr></table>");
            }
            return sb.ToString();
        }
    }
}
