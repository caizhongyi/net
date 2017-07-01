using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.twitter
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context) 
        {
            base.Page_Load(ref context);
            ShowList(ref context);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            string twittercontent = JuSNS.Common.Input.FilterHTML(GetString("twittercontent"));
            string twittermedia = GetString("twittermedia");
            if (!string.IsNullOrEmpty(twittercontent) && twittercontent.Length > 2)
            {
                TwitterInfo mdl = new TwitterInfo();
                mdl.Comments = 0;
                mdl.Content = twittercontent;
                byte ischeck = Convert.ToByte(Public.GetXMLBaseValue("Twitter"));
                if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
                {
                    ischeck = 0;
                }
                if (ischeck == 1)
                {
                    mdl.IsLock = true;
                }
                else
                {
                    mdl.IsLock = false;
                }
                UserInfo uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(this.GetUserID());
                mdl.IsRec = 0;
                mdl.MType = string.Empty;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.UserID = uid;
                if (HttpContext.Current.Request.Files.Count>0)
                {
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    mdl.Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Public.GetXMLBaseValue("Twitterpath"));
                }
                else
                {
                    mdl.Pic = string.Empty;
                }
                mdl.Media = twittermedia;
                int n = JuSNS.Home.App.TWrite.Instance.InserTwitter(mdl);
                if (n > 0)
                {
                    //插入动态
                    if (Public.GetXMLBaseValue("Twitter") == "0")
                    {
                        DynInfo dyninfo = new DynInfo();
                        dyninfo.Content = twittercontent;
                        dyninfo.CUserID = 0;
                        dyninfo.DynType = (int)EnumDynType.CreatTwitter;
                        dyninfo.Infoarr = n;
                        dyninfo.PostTime = DateTime.Now;
                        dyninfo.UserID = uid;
                        JuSNS.Home.User.User.Instance.InsertDyn(dyninfo);
                        context.Put("redirecturl", "default" + ExName + "?r=add");
                    }
                    else
                    {
                        context.Put("rights","已经发布微博，但是需要管理审核。");
                    }
                }
                else
                {
                    context.Put("errors", "发布微博客失败");
                }
            }
            else
            {
                context.Put("errors", "内容必须大于2个字符！");
            }
            ShowInfo(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string q = GetString("q");
            int uid = this.GetUserID();
            SqlConditionInfo[] st = null;
            string FriendSTR = string.Empty;
            context.Put("q", q);
            if (q == "friend")
            {
                FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            }
            if (Input.IsInteger(q))
            {
                uid = Convert.ToInt32(q);
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                context.Put("cpagetitle", mdl.TrueName + "的微博");
            }
            else
            {
                switch (q)
                {
                    case "my":
                        context.Put("cpagetitle", "我的微博");
                        break;
                    case "friend":
                        context.Put("cpagetitle", "朋友的微博");
                        break;
                    default:
                        context.Put("cpagetitle", "所有微博");
                        break;
                }
            }
            DataTable dt = JuSNS.Home.UtilPage.GetTwitterPage(q, FriendSTR, uid, PageIndex, recount, out ReCount, out PgCount, st);

            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> twitlist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.GetUserID()).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable twit = new Hashtable();
                twit.Add("id", dr["id"]);
                twit.Add("userid", dr["userid"]);
                twit.Add("spaceurl", root+"/app/twitter?q="+dr["userid"]);
                twit.Add("twitterurl", Public.URLWrite(dr["id"], "twitter"));
                twit.Add("headpic", this.GetHeadImage(dr["userid"]));
                twit.Add("truename", dr["truename"]);
                twit.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["posttime"])));
                string contentSTR = Input.ReplaceSmaile(Input.FilterHTML(dr["content"].ToString()));
                string Pic = dr["pic"].ToString();
                string Player = string.Empty;
                if (!string.IsNullOrEmpty(Pic) && Pic.IndexOf(".") > -1)
                {
                    string ExPic = root + Public.GetXMLBaseValue("Twitterpath") + "/" + Pic;
                    contentSTR += "<br /><a href=\"" + ExPic + "\" target=\"_blank\"><img src=\"" + ExPic + "\" /></a>";
                }
                if (!string.IsNullOrEmpty(dr["media"].ToString()))
                {
                    contentSTR += "<br />" + Public.ShowPlayer(dr["media"].ToString());
                }
                twit.Add("content", contentSTR);
                twit.Add("comments", JuSNS.Home.App.TWrite.Instance.GetTwitterComments(dr["id"]));
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == this.GetUserID() || isadmin >0) isOp = true;
                if (isOp)
                {
                    twit.Add("showop", "<a href=\"javascript:;\" onclick=\"deletetwitter(" + dr["id"] + "," + this.GetUserID() + ")\" class=\"showok1\"></a>");
                }
                else
                {
                    twit.Add("showop", string.Empty);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        twit.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + this.GetUserID() + ",0,'twitter')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        twit.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + this.GetUserID() + ",1,'twitter')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    twit.Add("showrec", string.Empty);
                }
                twitlist.Add(twit);
            }
            dt.Dispose();
            context.Put("twitlist", twitlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
