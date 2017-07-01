using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.home.friend
{
    public class att : UserPage
    {
        public int recount = UiConfig.ATTNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            string action = GetString("action");
            if (action == "DeleteATT")
            {
                int aid = GetInt("aid", 0);
                int n = JuSNS.Home.User.User.Instance.DeleteAtt(aid, this.UserID);
                System.Web.HttpContext.Current.Response.Write(n);
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            if (GetString("r") == "1")
            {
                context.Put("cpagetitle", "我关注的人");
            }
            else
            {
                context.Put("cpagetitle", "关注我的人");
            }
            JuSNS.MVC.GetCSS.CSS(ref context, GetString("r"));
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            string r=GetString("r");
            if (r == "1")
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_ATT_my_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_ATT_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            }
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> attlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable att = new Hashtable();
                att.Add("id", dr["id"]);
                if (r == "1")
                {
                    att.Add("userid", dr["atterid"]);
                    att.Add("userhead", this.GetHeadImage(dr["atterid"], 1));
                    att.Add("truename", dr["truename"]);
                    att.Add("spaceurl", this.GetSpaceURL(dr["atterid"]));
                    att.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.UserID, dr["atterid"]));
                }
                else
                {
                    att.Add("userid", dr["userid"]);
                    att.Add("userhead", this.GetHeadImage(dr["UserID"], 1));
                    att.Add("truename", dr["truename"]);
                    att.Add("spaceurl", this.GetSpaceURL(dr["UserID"]));
                    att.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.UserID, dr["UserID"]));
                }
                attlist.Add(att);
            }
            dt.Dispose();
            context.Put("attlist", attlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}