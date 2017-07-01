using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.home.friend
{
    public class visit : UserPage
    {
        public int recount = UiConfig.VNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "来访者");
            visitlist(ref context);
        }


        protected void visitlist(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            string q = GetString("q");
            if (q== "my")
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_visited_my_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_visited_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            }
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> visitlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable visit = new Hashtable();
                visit.Add("truename", dr["truename"]);
                string GetNew = string.Empty;
                if (q == "my")
                {
                    GetNew = JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["UserID"]);
                    visit.Add("id", dr["id"]);
                    visit.Add("uid", dr["UserID"]);
                    visit.Add("friendid", dr["userid"]);
                    visit.Add("spaceurl", this.GetSpaceURL(dr["UserID"]));
                    visit.Add("userhead", this.GetHeadImage(dr["UserID"], 1));
                    visit.Add("twitter", Input.ReplaceSmaile(GetNew));
                    visit.Add("twittermore", Input.LostHTML(GetNew));
                    visit.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.UserID, dr["UserID"]));
                }
                else
                {
                    GetNew = JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["VisitorID"]);
                    visit.Add("id", dr["id"]);
                    visit.Add("uid", dr["VisitorID"]);
                    visit.Add("friendid", dr["VisitorID"]);
                    visit.Add("spaceurl", this.GetSpaceURL(dr["VisitorID"]));
                    visit.Add("userhead", this.GetHeadImage(dr["VisitorID"], 1));
                    visit.Add("twitter", Input.ReplaceSmaile(GetNew));
                    visit.Add("twittermore", Input.LostHTML(GetNew));
                    visit.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.UserID, dr["VisitorID"]));
                }
                visitlist.Add(visit);
            }
            dt.Dispose();
            context.Put("visitlist", visitlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}