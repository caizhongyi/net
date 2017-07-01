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
    public class photoview : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int userid = this.GetUserID();
            base.Page_Load(ref context);
            int pid = GetInt("pid", 0);
            PhotoInfo mdl = JuSNS.Home.App.Photo.Instance.GetInfo(pid);
            AlbumInfo amdl = JuSNS.Home.App.Album.Instance.GetInfo(mdl.AlbumID);
            string error = string.Empty;
            int priacy = amdl.Privacy;
            if (priacy == 1)
            {
                if (!JuSNS.Home.User.User.Instance.IsFriends(this.GetUserID(), amdl.UserID))
                {
                    error += "只能好友可见！";
                }
            }
            else if (priacy == 2)
            {
                if (this.GetUserID() != amdl.UserID)
                {
                    error += "只能本人可见！";
                }
            }
            context.Put("cpagetitle", "浏览" + mdl.AlbumName + "中的图片");
            if (string.IsNullOrEmpty(error))
            {
                if (mdl.AlbumID > 0)
                {
                    context.Put("albumname", amdl.Title);
                }
                else
                {
                    context.Put("albumname", "头像相册");
                    context.Put("cpagetitle", "头像相册");
                }
                context.Put("aid", mdl.AlbumID);
                context.Put("pid", pid);
                context.Put("userid", mdl.UserID);
                context.Put("truename", mdl.TrueName);
                context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                context.Put("headpic", this.GetHeadImage(mdl.UserID, 1));
                if (mdl.PhotoType == 0)
                {
                    context.Put("orgPhotoPath", Public.GetOrgHeadPic(mdl.FilePath));
                }
                else
                {
                    context.Put("orgPhotoPath", Public.GetOrgPic(mdl.FilePath));
                }
                if (mdl.AlbumID > 0)
                {
                    context.Put("totlepic", amdl.ImagesCount);
                }
                context.Put("numberpic", JuSNS.Home.App.Photo.Instance.TheNumber(mdl.AlbumID, pid, mdl.UserID));
                context.Put("prepicid", JuSNS.Home.App.Photo.Instance.PrePhotoID(mdl.AlbumID, pid, mdl.UserID));
                context.Put("nextpicid", JuSNS.Home.App.Photo.Instance.NextPhotoID(mdl.AlbumID, pid, mdl.UserID));
                context.Put("pic", this.GetSmallPic(mdl.FilePath, 4));
                context.Put("desc", mdl.Description);
                context.Put("comments", mdl.Comments);
                context.Put("views", mdl.Views);
                string exif = "图片原始大小:" + mdl.Width + "×" + mdl.Height;
                string opSTR = "<li><a href=\"javascript:;\" onclick=\"showexif('" + exif + "')\">查看照片EXIF信息</a></li>";
                bool isadmin = JuSNS.Home.User.User.Instance.IsAdmin(userid);
                if (isadmin || userid == mdl.UserID)
                {
                    opSTR += "<li><a href=\"javascript:;\" onclick=\"deletes(" + mdl.Id + "," + userid + ",'photo')\">删除照片</a></li>";
                }
                if (isadmin)
                {
                    if (mdl.IsRec)
                    {
                        context.Put("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + pid + "," + userid + ",0,'photo')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        context.Put("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + pid + "," + userid + ",1,'photo')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    context.Put("showrec", string.Empty);
                }
                context.Put("showop", opSTR);
                ShowCommentList(ref context, pid);
                //更新点击率
                JuSNS.Home.App.Photo.Instance.UpdateNumber(pid, 0);
            }
            else
            {
                context.Put("errors", error);
            }
        }

        protected void ShowCommentList(ref VelocityContext context, int pid)
        {
            int recount = Convert.ToInt32(Public.GetXMLAlbumValue("PhotoCommentNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@PhotoID", pid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_photo_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> commlist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin;
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
                if (Convert.ToInt32(dr["userid"]) == this.GetUserID() || isadmin >0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != this.GetUserID())
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["photoid"] + "," + this.GetUserID() + ",'" + dr["truename"] + "','photo');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + this.GetUserID() + ",'photocomment')\" class=\"showok1\"></a>";
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
                    //增加通知和动态
                    if (GetInt("userid", 0) != this.GetUserID())
                    {
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "评论了您的照片", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyPhoto, pid));
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.PhotoComment, string.Empty, DateTime.Now, pid, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(20), 0, 0, "评论照片");
                    }
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
