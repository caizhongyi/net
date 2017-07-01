using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;
using System.IO;
using System.Xml;
using System.Net;

namespace JuSNS.Web.system
{
    public class @default : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
                ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "会员管理 - 管理中心");
            this.ShowList(ref context);
            LinkjusnsVersionPage(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int q=GetInt("q",0);
            context.Put("q", GetString("q"));
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            string kwd = GetString("kwd");
                if (q != 0)
                {
                    if (!string.IsNullOrEmpty(kwd))
                    {
                        context.Put("kwd", kwd);
                        st = new SqlConditionInfo[2];
                        st[0] = new SqlConditionInfo("@Q", q, TypeCode.Int32);
                        st[1] = new SqlConditionInfo("@KWD", kwd, TypeCode.String);
                        st[1].Blur = 3;
                        dt = JuSNS.Home.UtilPage.GetPage("manager_userlist_q_key_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                    }
                    else
                    {
                        st = new SqlConditionInfo[1];
                        st[0] = new SqlConditionInfo("@Q", q, TypeCode.Int32);
                        dt = JuSNS.Home.UtilPage.GetPage("manager_userlist_q_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(kwd))
                    {
                        context.Put("kwd", kwd);
                        st = new SqlConditionInfo[1];
                        st[0] = new SqlConditionInfo("@KWD", kwd, TypeCode.String);
                        st[0].Blur = 3;
                        dt = JuSNS.Home.UtilPage.GetPage("manager_userlist_key_aspx", PageIndex, recount, out ReCount, out PgCount, st);
                    }
                    else
                    {
                        dt = JuSNS.Home.UtilPage.GetPage("manager_userlist_aspx", PageIndex, recount, out ReCount, out PgCount, null);
                    }
                }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("truename", dr["truename"]);
                info.Add("regtime", Public.getTimeEXTSpan(Convert.ToDateTime(dr["RegTime"])));
                if (Input.IsDate(dr["LastLoginTime"].ToString()))
                {
                    info.Add("lastlogintime", Public.getTimeEXTSpan(Convert.ToDateTime(dr["LastLoginTime"])));
                }
                else
                {
                    info.Add("lastlogintime", string.Empty);
                }
                info.Add("sex", Convert.ToByte(dr["sex"]) == 0 ? "男" : "女");
                info.Add("email", dr["email"]);
                info.Add("state", dr["state"]);
                info.Add("userid", dr["userid"]);
                info.Add("integral", dr["integral"]);
                info.Add("inteyb", dr["inteyb"]);
                info.Add("logintimes", dr["LoginTimes"]);
                bool admin = JuSNS.Home.User.User.Instance.IsAdmin(dr["userid"]);
                if (admin)
                {
                    info.Add("admin", 1);
                }
                else
                {
                    info.Add("admin", 0);
                }
                info.Add("money", Convert.ToDecimal(dr["Money"]).ToString("0.00"));
                info.Add("isvip", Convert.ToBoolean(dr["isvip"]));
                info.Add("isrec", Convert.ToByte(dr["isrec"]));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public void LinkjusnsVersionPage(ref VelocityContext context)
        {
            #region 链接官方升级页面
            string returnSTR = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://user.jusns.com/version/ver.html");

            try
            {
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = false;
                req.Timeout = 1500;

                HttpWebResponse Http_Res = (HttpWebResponse)req.GetResponse();

                if (Http_Res.StatusCode.ToString() != "OK")
                {
                    returnSTR = "";
                }
                else
                {
                    returnSTR = "<iframe style=\"width:98%;height:20px;\" frameborder=\"no\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\" src=\"http://user.jusns.com/version/ver.html\"></iframe>";
                }
            }
            catch
            {
                returnSTR = "<span style=\"padding-top:8px;height:20px;color:Red;\">版本信息：访问JuSNS Inc.官方站失败！无法获取最新补丁及版本信息，<a href=\"http://www.jusns.com\" target=\"_blank\">点击这里获取最新信息.</a></span>";
            }
            context.Put("jusnsversion", returnSTR);
            #endregion
        }
    }
}
