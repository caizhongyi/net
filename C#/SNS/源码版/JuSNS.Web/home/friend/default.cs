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
    public class @default : UserPage
    {
        public int recount = UiConfig.FNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "我的好友");
            frdlists(ref context);
            frdclasslist(ref context);
        }

        protected void frdclasslist(ref VelocityContext context)
        {
            DataTable dt = JuSNS.Home.User.User.Instance.GetFriendClass(this.UserID);
            List<Hashtable> frdclist = new List<Hashtable>();
            int id = GetInt("q", 0);
            FriendInfo frdcinfo = JuSNS.Home.User.User.Instance.GetFriendInfo(id);
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frdc = new Hashtable();
                frdc.Add("id", dr["id"]);

                if (id == Convert.ToInt32(dr["id"]))
                {
                    frdc.Add("css", " class=\"current\"");
                }
                else
                {
                    frdc.Add("css", string.Empty);
                }
                frdc.Add("cname", dr["cname"]);
                frdclist.Add(frdc);
            }
            dt.Dispose();
            context.Put("frdclist", frdclist);
        }

        protected void frdlists(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            int classid = GetInt("q", 0);
            if (classid > 0)
            {
                SqlConditionInfo[] st = new SqlConditionInfo[2];
                st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                st[1] = new SqlConditionInfo("@ClassID", classid, TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_friend_class_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_friend_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            }
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> frdlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frd = new Hashtable();
                frd.Add("truename", dr["truename"]);
                frd.Add("id", dr["id"]);
                frd.Add("friendid", dr["friendid"]);
                frd.Add("spaceurl", this.GetSpaceURL(dr["friendid"]));
                frd.Add("userhead", this.GetHeadImage(dr["friendid"], 1));
                frd.Add("twitter", Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["friendID"])));
                frd.Add("twittermore", Input.LostHTML(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["friendID"])));
                frdlist.Add(frd);
            }
            dt.Dispose();
            context.Put("frdlist", frdlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

    }
}
