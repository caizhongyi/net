using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.space.info
{
    public class work : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            HttpContext.Current.Response.Expires = 0;
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "工作信息");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("username", "用户名：" + mdl.UserName);
            context.Put("email", "电子邮件：" + mdl.Email);
            context.Put("mobilesendcontent", "编写【" + Public.GetXMLValue("sendmobileNumber") + "】发送到 " + Public.GetXMLValue("sendMobileBind") + "，根据提示完成绑定。");
            if (!string.IsNullOrEmpty(mdl.Mobile))
            {
                context.Put("mobile", "手机：" + mdl.Mobile);
                string bindMobile = string.Empty;
                if (mdl.BindMoblie)
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',0);\" title=\"取消绑定\">已绑定</a>";
                }
                else
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',1);\" title=\"绑定手机\">未绑定</a>";
                }
                context.Put("bindmobile", "(" + bindMobile + ")");
            }
            else
            {
                context.Put("mobile", "手机：<a href=\"javascript:void(0);\" onclick=\"jQuery('#hideMobile').toggle();\" title=\"填写手机\">未填写</a>");
                context.Put("bindmobile", string.Empty);
            }
            int cid = GetQueryInt("cid", 0);
            context.Put("cid", cid);
            CareerInfo edinfo = null;
            if (cid > 0)
            {
                edinfo = JuSNS.Home.User.User.Instance.GetCareerInfo(cid);
                context.Put("company", edinfo.Company);
                context.Put("leavetime", edinfo.LeaveTime);
                context.Put("jointime", edinfo.JoinTime);
                if (edinfo.LeaveTime == "0")
                {
                    context.Put("checked", "checked");
                }
                else
                {
                    context.Put("checked", string.Empty);
                }
            }
            else
            {
                context.Put("leavetime", string.Empty);
                context.Put("company", string.Empty);
                context.Put("jointime", string.Empty);
                context.Put("checked", string.Empty);
            }
            Worklist(ref context);
        }

        public void Worklist(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);

            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_career_aspx", PageIndex, 20, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> careerlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable career = new Hashtable();
                career.Add("company", dr["company"]);
                if (dr["leavetime"].ToString() == "0")
                {
                    career.Add("leavetime", "在职");
                }
                else
                {
                    career.Add("leavetime", dr["leavetime"]);
                }
                career.Add("jointime", dr["jointime"]);
                career.Add("id", dr["id"]);
                careerlist.Add(career);
            }
            dt.Dispose();
            context.Put("careerlist", careerlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, 20));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string company = GetString("txtcompany");
            string leavetime = GetString("txtleavetime");
            string jointime = GetString("txtjointime");
            string nolevel = GetString("chknolevel");
            if (!string.IsNullOrEmpty(nolevel))
            {
                leavetime = "0";
            }
            int cid = GetFormInt("cid", 0);
            CareerInfo edinfo = new CareerInfo();
            edinfo.ID = cid;
            edinfo.LeaveTime = leavetime;
            edinfo.JoinTime = jointime;
            edinfo.PostTime = DateTime.Now;
            edinfo.Company = company;
            edinfo.UserID = this.UserID;
            int flag = 0;
            if (cid > 0) flag = 1;
            int m = JuSNS.Home.User.User.Instance.InsertCareer(edinfo, flag);
            ShowInfo(ref context);
            if ( m> 0)
            {
                context.Put("redirecturl", "work" + ExName);
            }
            else
            {
                context.Put("errors", "添加工作信息失败！");
            }
        }
    }
}
