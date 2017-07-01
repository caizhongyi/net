using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class group: ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "社群管理 - 管理中心");
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
            if (GetString("light") == "1")
            {
                dt = JuSNS.Home.UtilPage.GetPage("manager_group_light_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            else
            {
                if (!string.IsNullOrEmpty(kwd))
                {
                    context.Put("kwd", kwd);
                    st = new SqlConditionInfo[1];
                    st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                    st[0].Blur = 3;
                    dt = JuSNS.Home.UtilPage.GetPage("manager_group_key_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                }
                else
                {
                    dt = JuSNS.Home.UtilPage.GetPage("manager_group_aspx", PageIndex, recount, out ReCount, out PgCount, null);
                }
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("groupurl", Public.URLWrite(dr["id"], "group"));
                info.Add("truename", dr["truename"]);
                info.Add("logo", Public.GetGroupPortrait(dr["Portrait"]));
                info.Add("posttime", Public.getTimeEXTSpan(Convert.ToDateTime(dr["posttime"])));
                string groupname = dr["groupname"].ToString();
                info.Add("groupname", groupname);
                if (Convert.ToBoolean(dr["isrec"]))
                {
                    info.Add("showrec", "<a href=\"javascript:;\" title=\"已推荐\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'group')\" class=\"showrec1\"></a>");
                }
                else
                {
                    info.Add("showrec", "<a href=\"javascript:;\" title=\"未推荐\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'group')\" class=\"showrec\"></a>");
                }
                info.Add("id", dr["id"]);
                if (Convert.ToBoolean(dr["Islight"]))
                {
                    info.Add("state", Public.GetEnumStateOp(dr["IsLock"], Convert.ToInt32(dr["id"]), uid, "group") + " <input type=\"button\" class=\"btn_blue6\" title=\"取消名人机构\" onclick=\"Checklight(" + dr["id"] + "," + uid + ",0)\" value=\"取消名人机构\" />");
                }
                else
                {
                    info.Add("state", Public.GetEnumStateOp(dr["IsLock"], Convert.ToInt32(dr["id"]), uid, "group") + " <input type=\"button\" class=\"btn_blue6\" title=\"设为名人机构\" onclick=\"Checklight(" + dr["id"] + "," + uid + ",1)\" value=\"设为名人机构\" />");
                }
                info.Add("islock", Public.GetEnumState(dr["IsLock"]));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
