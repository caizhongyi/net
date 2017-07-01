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
    public class visit : UserPage
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
            context.Put("myclasslist", GetClassList(this.UserID));
            context.Put("cpagetitle", "我看过的日志");
        }

        protected string GetClassList(int UserID)
        {
            int classid = GetInt("classid", 0);
            string listSTR = "<li><a href=\"../blog?classid=0\">全部</a></li>";
            if (classid == 0)
            {
                listSTR = "<li><a href=\"../blog?classid=0\">全部</a></li>";
            }
            List<BlogClassInfo> Infolist = JuSNS.Home.App.Blog.Instance.GetBlogClass(UserID, 0);
            foreach (BlogClassInfo info in Infolist)
            {
                listSTR += "<li><a href=\"../blog?classid=" + info.Id + "\">" + info.CName + "</a></li>";
            }
            return listSTR;
        }

        public void ShowList(ref VelocityContext context)
        {
            int recount = 15;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_blog_visit_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            int contentNumber = Convert.ToInt32(Public.GetXMLBaseValue("BlogContentNumber"));
            List<Hashtable> bloglist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable blog = new Hashtable();
                blog.Add("id", dr["id"]);
                blog.Add("userid", dr["userid"]);
                blog.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                blog.Add("headpic", this.GetHeadImage(dr["userid"]));
                blog.Add("title", dr["title"]);
                blog.Add("titleno", Input.FilterHTML(dr["title"]));
                blog.Add("url", Public.URLWrite(dr["id"], "blog"));
                blog.Add("truename", JuSNS.Home.User.User.Instance.GetUserInfo(dr["userid"]).TrueName);
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
                    content += "<img align=\"right\" src=\"" + root + "/uploads/app/blog/" + dr["PicPath"] + "\" style=\"width:70px;height:100px;\" />";
                }
                content += Input.GetSubString(Input.FilterHTML(dr["content"].ToString()), contentNumber);
                blog.Add("content", content);
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == this.UserID || isadmin >0)
                {
                    isOp = true;
                    blog.Add("edit", string.Empty);
                }
                if (isOp)
                {
                    blog.Add("showop", "<a href=\"javascript:;\" onclick=\"deletetblog(" + dr["id"] + "," + this.UserID + ")\" class=\"showok1\"></a>");
                }
                else
                {
                    blog.Add("showop", string.Empty);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        blog.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + this.UserID + ",0,'blog')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        blog.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + this.UserID + ",1,'blog')\" class=\"showrec\"></a>");
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
