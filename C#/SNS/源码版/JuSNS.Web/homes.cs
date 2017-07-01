using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web
{
    public class homes : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "随便看看 - 社区动态");
            string q = GetString("q");
            int uid = GetInt("uid", 0);
            context.Put("guid", uid);
            if (uid > 0)
            {
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                context.Put("unames", mdl.TrueName);
            }
            ShowList(ref context, q, uid);
        }


        protected void ShowList(ref VelocityContext context,string dyntype,int userid)
        {
            int uid = this.GetUserID();
            string killuser = string.Empty;
            if (uid > 0)
            {
                string filepath = "~/space/info/my/user" + uid + ".config";
                killuser = Public.GetXMLValue("killuser", filepath);
            }
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = JuSNS.Home.UtilPage.GetDynAllPage(userid,dyntype, killuser, PageIndex, recount, out ReCount, out PgCount);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            string gDateSTR = string.Empty;
            bool todaytf = false;
            bool yesdaytf = false;
            bool longtf = false;
            bool longlongtf = false;
            bool isadmin = JuSNS.Home.User.User.Instance.IsAdmin(uid);
            foreach (DataRow dr in dt.Rows)
            {
                DynInfo dyninfo = new DynInfo();
                dyninfo.Content = Convert.ToString(dr["content"]);
                dyninfo.CUserID = Convert.ToInt32(dr["CUserID"]);
                dyninfo.DynType = Convert.ToInt32(dr["DynType"]);
                dyninfo.ID = Convert.ToInt32(dr["ID"]);
                dyninfo.Infoarr = Convert.ToInt32(dr["Infoarr"]);
                dyninfo.PostTime = DateTime.Parse(dr["PostTime"].ToString());
                dyninfo.TrueName = Convert.ToString(dr["TrueName"]);
                dyninfo.UserID = Convert.ToInt32(dr["UserID"]);
                Hashtable info = new Hashtable();
                string TmpUserLogStr = JuSNS.MVC.ParseUserLog.Instance.Parse(dyninfo, uid);
                if (!string.IsNullOrEmpty(TmpUserLogStr))
                {
                    DateTime nday = DateTime.Now;
                    string ShowDateSTR = string.Empty;
                    if (gDateSTR != Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd"))
                    {
                        if (Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd") == nday.ToString("MM-dd")) { if (!todaytf) { ShowDateSTR = "<li class=\"dyncli\">今天</li>"; todaytf = true; } }
                        else if (Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd") == (nday.AddDays(-1)).ToString("MM-dd")) { if (!yesdaytf) { ShowDateSTR = "<li class=\"dyncli\">昨天</li>"; yesdaytf = true; } }
                        else if (Convert.ToDateTime(dr["PostTime"]).ToString("MM-dd") == (nday.AddDays(-2)).ToString("MM-dd")) { if (!longtf) { ShowDateSTR = "<li class=\"dyncli\">前天</li>"; longtf = true; } }
                        else { if (!longlongtf) { ShowDateSTR = "<li class=\"dyncli\">以前的动态</li>"; longlongtf = true; } }
                    }
                    else
                    {
                        ShowDateSTR = string.Empty;
                    }
                    string deleteSTR = string.Empty;
                    if (isadmin)
                    {
                        deleteSTR = "<a href=\"javascript:;\" title=\"删除此动态\" onclick=\"deleteAll(" + dr["ID"] + "," + uid + ",'dyn')\" class=\"showok1\"></a>";
                    }
                    TmpUserLogStr = TmpUserLogStr.Replace("{ColseDyn}", "<a href=\"javascript:;\" title=\"屏蔽此用户的动态\" onclick=\"colosedyn(" + Convert.ToInt32(dr["ID"]) + "," + Convert.ToInt32(dr["UserID"]) + "," + uid + ")\" class=\"dync\"></a>" + deleteSTR);
                    info.Add("dynall", ShowDateSTR + "<li id=\"param_" + Convert.ToInt32(dr["ID"]) + "\">" + TmpUserLogStr + "</li>");
                    infolist.Add(info);
                }
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

    }
}
