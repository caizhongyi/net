using System;
using System.Text;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.vote
{
    public class view : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid=this.GetUserID();
            base.Page_Load(ref context);
            int vid = GetInt("vid", 0);
            if (vid == 0)
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                VoteInfo mdl = JuSNS.Home.App.Vote.Instance.GetVoteInfo(vid);
                string pagetitle = mdl.Title;
                context.Put("cpagetitle", pagetitle);
                byte isfriendViews = mdl.IsFriend;
                context.Put("time", Public.getTimeLEXYearSpan(mdl.PostTime));
                context.Put("userid", mdl.UserID);
                context.Put("truename", JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID).TrueName);
                context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                context.Put("headpic", this.GetHeadImage(mdl.UserID, 2));
                context.Put("jcnt", mdl.JCnt);
                context.Put("vid", vid);
                context.Put("content", mdl.Content);
                DateTime ntime = DateTime.Now;
                DateTime etime = mdl.EndTime;
                if ((etime - ntime).Days < 365)
                {
                    context.Put("endtime", mdl.EndTime.ToString("yyyy-MM-dd"));
                }
                if ((ntime - etime).Days > 0)
                {
                    context.Put("timeout", true);
                }
                context.Put("option", GetOption(vid, mdl.Mode, mdl.VCnt));
                if (JuSNS.Home.App.Vote.Instance.IsVote(uid, vid))
                {
                    context.Put("isvote", true);
                }
                context.Put("myselect", GetAnswer(vid, uid));
                string time=string.Empty;
                context.Put("mycomment", GetComm(vid, uid,out time));
                context.Put("ctime", time);
                //判断当前用户是否是发投票用户的好友
                bool isFriend = JuSNS.Home.User.User.Instance.IsFriends(uid, mdl.UserID);
                if (isFriend||uid==mdl.UserID)
                {
                    isFriend = true;
                }
                context.Put("showcomment", isFriend);
                ShowList(ref context, vid);
                if (isfriendViews == 1)
                {
                    if (!isFriend)
                    {
                        context.Put("friendview", false);
                    }
                }
            }
        }

        protected void ShowList(ref VelocityContext context, int vid)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLVoteValue("PageNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            st = new SqlConditionInfo[2];
            st[0] = new SqlConditionInfo("@VoteID", vid, TypeCode.Int32);
            st[1] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_joinvote_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("userid", dr["userid"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("anwser", GetAnswer(vid, Convert.ToInt32(dr["userid"])));
                string time = string.Empty;
                info.Add("comment", GetComm(vid, Convert.ToInt32(dr["userid"]),out time));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("time", time);
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'voteto')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        /// <summary>
        /// 取得我的答案
        /// </summary>
        /// <param name="ID">投票编号</param>
        /// <param name="UID">会员编号</param>
        /// <returns></returns>
        protected string GetAnswer(int vid, int uid)
        {
            StringBuilder s = new StringBuilder();
            bool tf = false;
            List<VoteOptionInfo> list = JuSNS.Home.App.Vote.Instance.OptionList(vid, uid, out tf);
            if (tf)
            {
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == (list.Count - 1))
                        {
                            s.Append("<span>" + list[i].OptionName + "</span>");
                        }
                        else
                        {
                            s.Append("<span>" + list[i].OptionName + "</span>、");
                        }
                    }
                    return s.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 取得评论内容
        /// </summary>
        /// <param name="vid">投票编号</param>
        /// <param name="uid">会员编号</param>
        /// <returns></returns>
        protected string GetComm(int vid, int uid,out string time)
        {

            VoteToInfo mdl = JuSNS.Home.App.Vote.Instance.GetVoteToInfo(vid, uid);
            if (mdl != null)
            {
                time = Public.getTimeEXYearSpan(mdl.PostTime);
                return Input.HtmlEncode(mdl.Content);
            }
            else
            {
                time = string.Empty;
                return string.Empty;
            }
        }

        /// <summary>
        /// 取得投票选项
        /// </summary>
        /// <param name="ID">投票编号</param>
        /// <returns>返回结果</returns>
        protected string GetOption(int vid, int mode,int vcnt)
        {
            StringBuilder s = new StringBuilder();
            List<VoteOptionInfo> list = JuSNS.Home.App.Vote.Instance.OptionList(vid);
            for (int i = 0; i < list.Count; i++)
            {
                float j = 0;
                if (vcnt != 0)
                {
                    j = ((float)list[i].Cnt / (float)vcnt) * 100;
                }
                string o = string.Empty;
                if (mode == 1)//单选
                {
                    o = "<input name=\"Option_" + vid + "\" type=\"radio\" value=\"" + list[i].ID + "\" /> ";
                }
                else//复选
                {
                    o = "<input name=\"Option_" + vid + "\" type=\"checkbox\" value=\"" + list[i].ID + "\" /> ";
                }

                s.Append("<div class=\"pitem\"><div class=\"voteinfo\">"+Input.HtmlDecode(list[i].OptionName) + "：</div>\r\n");
                s.Append("<div class=\"voteplan\">\r\n");
                s.Append("<div style=\"border:1px solid #ababab;\"><div class=\"plan\" style=\"border:1px solid #f4f9fd;background-color:#" + JuSNS.Common.Rand.Str_Color(true) + ";width:" + j + "%;\"></div></div>\r\n");
                s.Append("</div>\r\n");
                s.Append("<div class=\"votenumber\"> " + o + "&nbsp; " + Convert.ToInt32(j) + "% (" + list[i].Cnt + "人)</div></div>\r\n");
            }
            return s.ToString();
        }
    }
}
