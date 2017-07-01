using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.center
{
    /// <summary>
    ///应用程序
    /// </summary>
    public class @default : UserPage
    {
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        /// <summary>
        /// 公共显示
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "应用程序");
            ShowList(ref context);
            context.Put("apppath", Public.GetXMLPageValue("apppath"));
            string q = GetString("q");
            int sid = 0;
            if (Input.IsInteger(q))
            {
                sid = Convert.ToInt32(q);
            }
            context.Put("classlist", GetClassList(sid));
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="context"></param>
        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            string q = GetString("q");
            int recount = 15;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            context.Put("q", q);
            DataTable dt = null;
            if (string.IsNullOrEmpty(q))
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_app_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            else
            {
                if (Input.IsInteger(q))
                {
                    st = new SqlConditionInfo[1];
                    st[0] = new SqlConditionInfo("@ClassID", Convert.ToInt32(q), TypeCode.Int32);
                    dt = JuSNS.Home.UtilPage.GetPage("user_app_classid_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                }
                else
                {
                    switch (q)
                    {
                        case "my":
                            st = new SqlConditionInfo[1];
                            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                            dt = JuSNS.Home.UtilPage.GetPage("user_app_my_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                            break;
                        case "mys":
                            st = new SqlConditionInfo[1];
                            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                            dt = JuSNS.Home.UtilPage.GetPage("user_app_mys_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                            break;
                        case "hot":
                            dt = JuSNS.Home.UtilPage.GetPage("user_hot_aspx", PageIndex, recount, out ReCount, out PgCount, null);
                            break;
                    }
                }
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("appname", dr["appname"]);
                info.Add("headpic", this.GetHeadImage(dr["userid"], 0));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("icon", dr["icon"]);
                info.Add("userid", dr["userid"]);
                info.Add("clicks", dr["click"]);
                info.Add("issetup", JuSNS.App.App.Instance.IsSetupApp(Convert.ToInt32(dr["id"]),this.UserID));
                info.Add("setupnumber", dr["setupNumber"]);
                info.Add("content", Input.GetSubString(Input.FilterHTML(dr["content"].ToString()),150));
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["CreatTime"])));
                string state = string.Empty;
                /*<option value="2" >准备开发...</option>
                       <option value="3" >开发中...</option>
                       <option value="1" >开发完成...</option>

               0normal,1deving,2tocheck,3getpass,4stop*/
                switch (Convert.ToByte(dr["islock"]))
                {
                    case 1:
                        info.Add("showstate", "已提交应用，等待审核");
                        break;
                    case 2:
                        info.Add("showstate", "准备开发阶段");
                        break;
                    case 3:
                        info.Add("showstate", "开发中");
                        break;
                    case 4:
                        info.Add("showstate", "被停止");
                        break;
                }
                string opSTR = string.Empty;
                if (dr["islock"].ToString() != "0")
                {
                    opSTR = "<a href=\"new" + ExName + "?appid=" + dr["id"] + "\" class=\"edit1\" title=\"编辑\"></a>";
                }
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        /// <summary>
        /// 得到分类
        /// </summary>
        /// <returns></returns>
        protected string GetClassList(int sid)
        {
            string listSTR = string.Empty;
            foreach (KeyValuePair<int, string> kv in JuSNS.Config.UserApiConfig.AppType)
            {
                if (sid == kv.Key)
                {
                    listSTR += "<li class=\"current\"><a href=\"../center?q="+kv.Key+"\">" + kv.Value + "</a></li>";
                }
                else
                {
                    listSTR += "<li><a href=\"../center?q=" + kv.Key + "\">" + kv.Value + "</a></li>";
                }
            }
            return listSTR;
        }
    }
}
