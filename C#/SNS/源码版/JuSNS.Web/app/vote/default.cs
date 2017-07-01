using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.vote
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            ShowInfo(ref context);
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            context.Put("q", q);
            string pagetitle = string.Empty;
            if (Input.IsInteger(q))
            {
                pagetitle = JuSNS.Home.User.User.Instance.GetUserInfo(q).TrueName + "的投票";
            }
            else
            {
                switch (q)
                {
                    case "my":
                        pagetitle = "我的投票";
                        break;
                    case "friend":
                        pagetitle = "朋友的投票";
                        break;
                    case "join":
                        pagetitle = "我参与的投票";
                        break;
                    case "hot":
                        pagetitle = "热门投票";
                        break;
                    default:
                        pagetitle = "最新投票";
                        break;
                }
            }
            ShowList(ref context, q, pagetitle);
        }

        protected void ShowList(ref VelocityContext context, string q, string pagetitle)
        {
            int recount = Convert.ToInt32(Public.GetXMLVoteValue("PageNumber"));
            int uid = this.GetUserID();
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
                context.Put("cpagetitle", "搜索关于” " + kwd + "“的" + pagetitle);
                context.Put("kwd", kwd);
            }
            else
            {
                context.Put("cpagetitle", pagetitle);
            }
            if (q == "friend") FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            DataTable dt = JuSNS.Home.UtilPage.GetVotePage(q, FriendSTR, uid, PageIndex, recount, out ReCount, out PgCount, st);
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
                info.Add("voteurl", Public.URLWrite(dr["id"], "vote"));
                info.Add("jcnt", dr["jcnt"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                string optionSTR = GetOption(dr["id"],dr["mode"]);
                info.Add("option", optionSTR);
                info.Add("title", dr["title"]);
                info.Add("titlefilt", Input.FilterHTML(dr["title"].ToString()));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'vote')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                if (isadmin > 0)
                {
                    if (Convert.ToBoolean(dr["isrec"]))
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'vote')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'vote')\" class=\"showrec\"></a>");
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

        protected string GetOption(object vid, object Mode)
        {
            StringBuilder s = new StringBuilder();
            List<VoteOptionInfo> list = JuSNS.Home.App.Vote.Instance.OptionList(Convert.ToInt32(vid));
            for (int i = 0; i < list.Count && i < 3; i++)
            {
                string o = string.Empty;
                if (Convert.ToInt32(Mode) == 1)//单选
                {
                    o = "<img src=\"" + root + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/radio.gif\" style=\"border:0px;\" align=\"absmiddle\" /> &nbsp;";
                    s.Append(o + Input.HtmlDecode(list[i].OptionName) + "<br />");
                }
                else//复选
                {
                    o = "<img src=\"" + root + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/checkbox.gif\" style=\"border:0px;\" align=\"absmiddle\" /> &nbsp;";
                    s.Append(o + Input.HtmlDecode(list[i].OptionName) + "<br />");
                }
            }
            if (list.Count > 3)
            {
                s.Append(" &nbsp; …");
            }
            return s.ToString();
        }
    }
}