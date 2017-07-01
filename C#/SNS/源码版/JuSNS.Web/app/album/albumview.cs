using System;
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
    /// 相册预览
    /// </summary>
    public class albumview : BasePage
    {
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
            int aid = GetInt("aid", 0);
            int uid = GetInt("uid", 0);
            context.Put("aid", aid);
            string error = string.Empty;
            if (aid ==0)
            {
                if (uid == 0)
                    uid = this.GetUserID();
                context.Put("cpagetitle", "头像相册");
                context.Put("albumname", "头像相册浏览");
                context.Put("userid", uid);
                context.Put("truename", JuSNS.Home.User.User.Instance.GetUserInfo(uid).TrueName);
                context.Put("spaceurl", this.GetSpaceURL(uid));
                context.Put("headpic", this.GetHeadImage(uid, 1));
            }
            else
            {
                AlbumInfo mdl = JuSNS.Home.App.Album.Instance.GetInfo(aid);
                context.Put("cpagetitle", "相册【" + mdl.Title + "】浏览");
                context.Put("albumname", mdl.Title);
                context.Put("userid", mdl.UserID);
                string truename = JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID).TrueName;
                context.Put("truename", truename);
                context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                context.Put("headpic", this.GetHeadImage(mdl.UserID, 1));
                int priacy = mdl.Privacy;
                if (priacy == 1)
                {
                    if (!JuSNS.Home.User.User.Instance.IsFriends(this.GetUserID(), mdl.UserID))
                    {
                        error += "只能" + truename + "的好友可见！";
                    }
                }
                else if (priacy == 2)
                {
                    if (this.GetUserID() != mdl.UserID)
                    {
                        error += "只能本人可见！";
                    }
                }
            }
            if (string.IsNullOrEmpty(error))
            {
                ShowList(ref context, aid, uid);
                int gid = GetInt("gid", 0);
                string groupname = GetString("groupname");
                if (gid > 0)
                {
                    if (string.IsNullOrEmpty(groupname))
                    {
                        context.Put("errors", "参数传递错误");
                    }
                }
                context.Put("groupname", groupname);
                context.Put("gid", gid);
            }
            else
            {
                context.Put("errors", error);
            }
        }
        /// <summary>
        /// 显示列表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="aid"></param>
        /// <param name="uid"></param>
        protected void ShowList(ref VelocityContext context,int aid,int uid)
        {
            int recount = Convert.ToInt32(Public.GetXMLAlbumValue("PhotoNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
             DataTable dt = null;
             if (aid == 0)
             {
                 if (uid == 0)
                 {
                     uid = this.GetUserID();
                 }
                 SqlConditionInfo[] st = new SqlConditionInfo[1];
                 st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
                 dt = JuSNS.Home.UtilPage.GetPage("user_albumhead_aspx", PageIndex, recount, out ReCount, out PgCount, st);
             }
             else
             {
                 SqlConditionInfo[] st = new SqlConditionInfo[1];
                 st[0] = new SqlConditionInfo("@AlbumID", aid, TypeCode.Int32);
                 dt = JuSNS.Home.UtilPage.GetPage("user_albumsingle_aspx", PageIndex, recount, out ReCount, out PgCount, st);
             }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.GetUserID()).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("pic", this.GetSmallPic(dr["FilePath"].ToString(), 1));
                info.Add("desc", Input.GetSubString(dr["Description"].ToString(), 14));
                info.Add("descmore", dr["Description"]);
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == this.GetUserID() || isadmin >0)
                {
                    isOp = true;
                    info.Add("edit", string.Empty);
                }
                if (isOp)
                {
                    info.Add("showop", "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + this.GetUserID() + ",'photo')\" class=\"showok1\"></a>");
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
    }
}
