using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.gbook
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            string pagetitle = "我的留言本";
            base.Page_Load(ref context);
            int useridid = GetInt("uid", 0);
            int sendid = GetInt("sendid", 0);
            int uid = this.GetUserID();
            if (useridid > 0)
            {
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(useridid);
                pagetitle = mdl.TrueName + "的留言本";
            }
            else
            {
                if (useridid == -1)
                {
                    pagetitle = "给管理员留言";
                    useridid = -1;
                }
                else
                {
                    useridid = uid;
                }
            }
            if (sendid > 0)
            {
                context.Put("sendid", sendid);
            }
            else
            {
                context.Put("sendid", useridid);
            }
            context.Put("cpagetitle", pagetitle);
            ShowList(ref context,uid, useridid, sendid);
        }

        protected void ShowList(ref VelocityContext context,int uid,int userid,int sendid)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            DataTable dt = null;
            if (userid == -1)
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_gbook_M_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                if (sendid == 0)
                {
                    st = new SqlConditionInfo[1];
                    st[0] = new SqlConditionInfo("@UserID", userid, TypeCode.Int32);
                    dt = JuSNS.Home.UtilPage.GetPage("user_gbook_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                }
                else
                {
                    st = new SqlConditionInfo[2];
                    st[0] = new SqlConditionInfo("@UserID", userid, TypeCode.Int32);
                    st[1] = new SqlConditionInfo("@SendID", sendid, TypeCode.Int32);
                    dt = JuSNS.Home.UtilPage.GetPage("user_gbookd_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                    context.Put("cpagetitle", "您和" + JuSNS.Home.User.User.Instance.GetUserInfo(sendid).TrueName + "的对话录");
                }
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("sendid", dr["sendid"]);
                if (sendid != Convert.ToInt32(dr["sendid"]))
                {
                    info.Add("dialog", "<a href=\"../gbook/default"+ExName+"?uid=" + userid + "&sendid=" + dr["sendid"] + "\">对话录</a>");
                }
                info.Add("content", Input.ReplaceSmaile(dr["content"]));
                if (Convert.ToInt32(dr["sendid"]) == uid)
                {
                    info.Add("truename", "您");
                }
                else
                {
                    info.Add("truename", dr["truename"]);
                }
                info.Add("truename1", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["sendid"]));
                info.Add("headpic", this.GetHeadImage(dr["sendid"], 2));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || Convert.ToInt32(dr["sendid"]) == uid) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'gbook')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string errors = string.Empty;
            GBookInfo mdl = new GBookInfo();
            mdl.Content = GetString("content");
            mdl.Id = 0;
            byte ischeck=Convert.ToByte(Public.GetXMLBaseValue("gbookcheck"));
            if (JuSNS.Home.User.User.Instance.IsAdmin(this.UserID))
                ischeck = 0;
            mdl.IsLock = ischeck;
            mdl.ParentID = 0;
            mdl.PostTime = DateTime.Now;
            mdl.SendID = this.UserID;
            mdl.UserID = GetInt("sendid", 0);
            int n = JuSNS.Home.User.User.Instance.InsertGbook(mdl);
            if (n > 0)
            {
                if (ischeck == 1)
                {
                    context.Put("rights","留言成功，但是需要管理员审核！");
                }
                else
                {
                    context.Put("rights","留言成功！");
                }
            }
            else
            {
                context.Put("errors", "留言失败");
            }
            ShowInfo(ref context); 
        }
    }
}
