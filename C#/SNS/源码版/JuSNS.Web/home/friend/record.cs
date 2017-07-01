using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class record : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "邀请记录");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string q = GetString("q");
            SqlConditionInfo[] st = new SqlConditionInfo[2];
            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
            st[1] = new SqlConditionInfo("@Succ", Convert.ToInt32(q), TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_InviteRecord_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> reclist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable rec = new Hashtable();
                rec.Add("id", dr["id"]);
                rec.Add("email", dr["email"]);
                rec.Add("posttime", Convert.ToDateTime(dr["posttime"]).ToString("yyyy-MM-dd hh:mm:ss"));
                rec.Add("succ", dr["Reply"].ToString().Replace("1", "<span class=\"reshow\">确认</span>").Replace("0", "待确认"));
                if (dr["Reply"].ToString() == "1")
                {
                    rec.Add("replaytime", Convert.ToDateTime(dr["ReplyTime"]).ToString("yyyy-MM-dd hh:mm:ss"));
                    rec.Add("username", "<a href=\"" + this.GetSpaceURL(dr["RegUserID"]) + "\" target=\"_blank\">" + JuSNS.Home.User.User.Instance.GetUserInfo(dr["RegUserID"]).TrueName + "</a>");
                }
                else
                {
                    rec.Add("replaytime", string.Empty);
                    rec.Add("username", string.Empty);
                }
                reclist.Add(rec);
            }
            dt.Dispose();
            context.Put("reclist", reclist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}