﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.system
{
    public class comment : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "资讯评论");
            this.ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            string kwd = GetString("kwd");
            if (!string.IsNullOrEmpty(kwd))
            {
                context.Put("kwd", kwd);
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                dt = JuSNS.Home.UtilPage.GetPage("manager_comment_news_key_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("manager_comment_news_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("newsurl", Public.URLWrite(dr["NewsID"], "news"));
                info.Add("truename", dr["truename"]);
                info.Add("posttime", Public.getTimeEXTSpan(Convert.ToDateTime(dr["posttime"])));
                string title = dr["content"].ToString();
                info.Add("title", Input.ReplaceSmaile(Input.GetSubString(Input.FilterHTML(title), 50)));
                info.Add("content", Input.ReplaceSmaile(title));
                info.Add("state", Public.GetEnumStateOp(dr["IsLock"], Convert.ToInt32(dr["id"]), uid, "newscomment"));
                info.Add("id", dr["id"]);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
