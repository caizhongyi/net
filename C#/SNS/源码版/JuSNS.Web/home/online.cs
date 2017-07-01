using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home
{
    public class online : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            if (q == "friend")
            {
                context.Put("cpagetitle", "好友在线列表");
            }
            else
            {
                context.Put("cpagetitle", "所有在线用户");
            }
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string q = GetString("q");
            int uid = this.GetUserID();
            string FriendSTR = string.Empty;
            context.Put("q", q);
            if (q == "friend")
            {
                FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            }
            DataTable dt = JuSNS.Home.UtilPage.GetOnlinePage(q, FriendSTR, uid, PageIndex, recount, out ReCount, out PgCount, null);

            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("userid", dr["userid"]);
                info.Add("spaceurl", GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"]));
                info.Add("truename", dr["truename"]);
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["lasttime"])));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
