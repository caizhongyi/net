using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.home.magic
{
    public class magic : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "我的道具");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            dt = JuSNS.Home.UtilPage.GetPage("user_magic_my_aspx", PageIndex, recount, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> magiclist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable magic = new Hashtable();
                //id, UserID, MID, PostTime, IsUse, SendUserID, IsUserTime, Number
                MagicInfo mdl = JuSNS.Home.User.User.Instance.GetMagicInfo(dr["mid"]);
                magic.Add("id", dr["id"]);
                magic.Add("magicpic", root + "/uploads/magic/" + mdl.Pic);
                magic.Add("mname", mdl.MName);
                magic.Add("mdesc", mdl.Mdesc);
                magic.Add("number", dr["number"]);
                magiclist.Add(magic);
            }
            dt.Dispose();
            context.Put("magiclist", magiclist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}