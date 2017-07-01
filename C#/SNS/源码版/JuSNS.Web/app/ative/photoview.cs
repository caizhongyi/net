using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ative
{
    public class photoview : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAtiveValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ative");
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
            int aid = GetInt("aid", 0);
            int pid = GetInt("pid", 0);
            context.Put("aid", aid);
            context.Put("pid", pid);
            AtiveInfo amdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            PhotoInfo mdl = JuSNS.Home.App.Ative.Instance.GetInfo(pid);
            context.Put("cpagetitle", "活动【" + amdl.AtiveName+ "】中的图片");
            context.Put("userid", mdl.UserID);
            context.Put("truename", mdl.TrueName);
            context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
            context.Put("headpic", this.GetHeadImage(mdl.UserID, 1));
            context.Put("orgPhotoPath", Public.GetOrgPic(mdl.FilePath));
            context.Put("totlepic", JuSNS.Home.App.Ative.Instance.AtiveImgCount(aid));
            context.Put("numberpic", JuSNS.Home.App.Ative.Instance.TheNumber(aid, pid));
            context.Put("prepicid", JuSNS.Home.App.Ative.Instance.PrePhotoID(aid, pid));
            context.Put("nextpicid", JuSNS.Home.App.Ative.Instance.NextPhotoID(aid, pid));
            context.Put("pic", this.GetSmallPic(mdl.FilePath, 4));
            context.Put("desc", mdl.Description);
            context.Put("comments", mdl.Comments);
            context.Put("views", mdl.Views);
            string exif = "图片原始大小:" + mdl.Width + "×" + mdl.Height;
            string opSTR = "<li><a href=\"javascript:;\" onclick=\"showexif('" + exif + "')\">查看照片EXIF信息</a></li>";
            if (JuSNS.Home.User.User.Instance.IsAdmin(uid) || uid == mdl.UserID)
            {
                opSTR += "<li><a href=\"javascript:;\" onclick=\"deletes(" + mdl.Id + "," + uid + ",'photo')\">删除照片</a></li>";
            }
            context.Put("showop", opSTR);
            ShowCommentList(ref context, pid);
            //更新点击率
            JuSNS.Home.App.Photo.Instance.UpdateNumber(pid,0);
        }

        protected void ShowCommentList(ref VelocityContext context, int pid)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLAlbumValue("PhotoCommentNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@PhotoID", pid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_photo_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> commlist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable comm = new Hashtable();
                comm.Add("id", dr["id"]);
                comm.Add("userid", dr["userid"]);
                comm.Add("truename", dr["truename"]);
                comm.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                comm.Add("headpic", this.GetHeadImage(dr["userid"]));
                comm.Add("time", Public.getTimeSpan(Convert.ToDateTime(dr["posttime"])));
                string ReplaySTR = string.Empty;
                if (Convert.ToInt32(dr["CommentID"]) > 0)
                {
                    PhotoCommentInfo bml = JuSNS.Home.App.Photo.Instance.GetPhotoCommentInfo(Convert.ToInt32(dr["CommentID"]));
                    ReplaySTR = "<div class=\"ctxreplay\"><b>" + bml.TrueName + "</b>：" + Input.ReplaceSmaile(bml.Content) + "</div>";
                }
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"].ToString()));
                bool isOp = false;
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != uid)
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["photoid"] + "," + uid + ",'" + dr["truename"] + "','photo');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'photocomment')\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string commentcontent = GetString("commentcontent");
            int pid = GetInt("pid", 0);
            PhotoCommentInfo mdl = new PhotoCommentInfo();
            mdl.PhotoID = pid;
            mdl.CommentID = 0;
            mdl.Content = commentcontent;
            int CommentCheck = Convert.ToInt32(Public.GetXMLAlbumValue("CommentCheck"));
            mdl.IsLock = CommentCheck == 0 ? false : true;
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            mdl.UserID = this.GetUserID();
            int n = JuSNS.Home.App.Photo.Instance.InsertPhotoComment(mdl);
            if (n > 0)
            {
                if (CommentCheck == 1)
                {
                    context.Put("rights", "发布评论成功。但是您的评论需要审核才能显示！");
                }
                else
                {
                    context.Put("rights", "发布评论成功");
                }
            }
            else
            {
                context.Put("errors", "发布评论成失败");
            }
            ShowInfo(ref context);
        }

    }
}
