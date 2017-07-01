using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.ative
{
    public class book : BasePage
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
            context.Put("aid", aid);
            AtiveInfo amdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            context.Put("auserid", amdl.UserID);
            ShowCommentList(ref context,aid);
            context.Put("cpagetitle", "活动【" + amdl.AtiveName + "】的留言");
        }

        protected void ShowCommentList(ref VelocityContext context, int aid)
        {
            AtiveInfo ainfo = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLAtiveValue("commentNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@Aid", aid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_ativebook_aspx", PageIndex, recount, out ReCount, out PgCount, st);
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
                    AtiveCommentInfo bml = JuSNS.Home.App.Ative.Instance.GetAtiveCommentInfo(Convert.ToInt32(dr["CommentID"]));
                    ReplaySTR = "<div class=\"ctxreplay\"><b>" + bml.TrueName + "</b>：" + Input.ReplaceSmaile(bml.Content) + "</div>";
                }
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"].ToString()));
                bool isOp = false;
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0 || ainfo.UserID==uid)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != uid)
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["ativeid"] + "," + uid + ",'" + dr["truename"] + "','ative');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'ativecomment')\" class=\"showok1\"></a>";
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
            int aid = GetInt("aid", 0);
            AtiveCommentInfo mdl = new AtiveCommentInfo();
            mdl.AtiveID = aid;
            mdl.CommentID = 0;
            mdl.Content = commentcontent;
            int CommentCheck = Convert.ToInt32(Public.GetXMLAtiveValue("CommentCheck"));
            mdl.IsLock = CommentCheck == 0 ? false : true;
            mdl.PostIP = Public.GetClientIP();
            mdl.Posttime = DateTime.Now;
            mdl.Userid = this.GetUserID();
            int n = JuSNS.Home.App.Ative.Instance.InsertAtiveComment(mdl);
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
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "活动留了言", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyActive, aid));
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.ActiveComment, string.Empty, DateTime.Now, aid, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(19), 0, 0, "活动留言");
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
