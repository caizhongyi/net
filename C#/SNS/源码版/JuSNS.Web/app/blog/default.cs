using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.blog
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
            context.Put("classlist", GetClassList(0));
            context.Put("myclasslist", GetClassList(this.GetUserID()));
            string orderby = GetString("orderby");
            if (!string.IsNullOrEmpty(orderby)) JuSNS.MVC.GetCSS.CSS(ref context, orderby);
        }

        protected string GetClassList(int UserID)
        {
            int classid = GetInt("classid", 0);
            string listSTR = "<li><a href=\"../blog?classid=0\">全部</a></li>";
            if (classid == 0)
            {
                listSTR = "<li class=\"current\"><a href=\"../blog?classid=0\">全部</a></li>";
            }
            List<BlogClassInfo> Infolist = JuSNS.Home.App.Blog.Instance.GetBlogClass(UserID, 0);
            foreach (BlogClassInfo info in Infolist)
            {
                if (classid == info.Id)
                {
                    listSTR += "<li class=\"current\"><a href=\"../blog?classid=" + info.Id + "\">" + info.CName + "</a></li>";
                }
                else
                {
                    listSTR += "<li><a href=\"../blog?classid=" + info.Id + "\">" + info.CName + "</a></li>";
                }
            }
            return listSTR;
        }

        public void ShowList(ref VelocityContext context)
        {
            int userid = this.GetUserID();
            string q = GetString("q");
            context.Put("q", q);
            string ptitle = "博客日志";
            if (!string.IsNullOrEmpty(q))
            {
                if (Input.IsInteger(q))
                {
                    ptitle = JuSNS.Home.User.User.Instance.GetUserInfo(q).TrueName + "的日志";
                }
                else
                {
                    switch (q)
                    {
                        case "my":
                            ptitle = "我的日志";
                            break;
                        case "friend":
                            ptitle = "朋友的日志";
                            break;
                    }
                }
            }
            context.Put("cpagetitle", ptitle);
            int recount = 15;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string keys = string.Empty;
            if (q == "friend") keys = JuSNS.Home.User.User.Instance.GetFriendList(userid);
            int classid = GetInt("classid", 0);
            if (classid > 0)
            {
                context.Put("cpagetitle", JuSNS.Home.App.Blog.Instance.GetClassName(classid)+" - 分类的日志");
            }
            int uid = GetInt("uid", 0);
            string oby = GetString("orderby");
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = null;
            if (!string.IsNullOrEmpty(kwd))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                context.Put("kwd", kwd);
                context.Put("cpagetitle", "关于 【"+kwd + "】 的日志");
            }
            DataTable dt = JuSNS.Home.UtilPage.GetBlogPage(q, keys, classid, oby, userid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            int contentNumber = Convert.ToInt32(Public.GetXMLBaseValue("BlogContentNumber"));
            List<Hashtable> bloglist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(userid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable blog = new Hashtable();
                blog.Add("id", dr["id"]);
                blog.Add("userid", dr["userid"]);
                blog.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                blog.Add("headpic", this.GetHeadImage(dr["userid"]));
                string TitleSTR = dr["title"].ToString();
                blog.Add("titleno", Input.FilterHTML(TitleSTR));
                int IsDraft = Convert.ToByte(dr["IsDraft"]);
                if (IsDraft == 1) TitleSTR += "<span class=\"reshow\">[草稿]</span>";
                blog.Add("title", TitleSTR);
                blog.Add("url", Public.URLWrite(dr["id"], "blog"));
                blog.Add("truename", dr["truename"]);
                blog.Add("click", dr["click"]);
                blog.Add("share", dr["ShareNumber"]);
                blog.Add("att", dr["attnumber"]);
                blog.Add("comment", dr["Comments"]);
                blog.Add("sysclassid", dr["sysclassid"]);
                blog.Add("sysclass", JuSNS.Home.App.Blog.Instance.GetClassName(Convert.ToInt32(dr["sysClassID"])));
                blog.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["postTime"])));
                string content = string.Empty;
                if (!string.IsNullOrEmpty(dr["PicPath"].ToString()))
                {
                    content += "<img align=\"right\" src=\"" + root + Public.GetXMLBaseValue("BlogPath") + "/" + dr["PicPath"] + "\" style=\"width:70px;height:90px;\" />";
                }
                content += Input.GetSubString(Input.FilterHTML(dr["content"].ToString()), contentNumber);
                blog.Add("content", content);
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == userid || isadmin > 0)
                {
                    isOp = true;
                    blog.Add("edit", string.Empty);
                }
                if (isOp)
                {
                    blog.Add("showop", "<a href=\"javascript:;\" onclick=\"deletetblog(" + dr["id"] + "," + userid + ")\" class=\"showok1\"></a>");
                }
                else
                {
                    blog.Add("showop", string.Empty);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        blog.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + userid + ",0,'blog')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        blog.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + userid + ",1,'blog')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    blog.Add("showrec", string.Empty);
                }
                bloglist.Add(blog);
            }
            dt.Dispose();
            context.Put("bloglist", bloglist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}