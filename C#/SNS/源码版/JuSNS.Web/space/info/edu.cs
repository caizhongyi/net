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
    public class edu : UserPage
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
            context.Put("cpagetitle", "教育信息");
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
            int eid = GetQueryInt("eid", 0);
            context.Put("eid", eid);
            EducationInfo edinfo = null;
            if (eid > 0)
            {
                edinfo = JuSNS.Home.User.User.Instance.GetEducationInfo(eid);
                context.Put("schoolname", edinfo.SchoolName);
                context.Put("leaveyear", edinfo.Leaveyear);
                if (edinfo.Leaveyear == "0")
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
                context.Put("schoolname", string.Empty);
                context.Put("leaveyear", string.Empty);
                context.Put("checked", string.Empty);
            }
            #region 教育状态啊
            string[] estr = { "小学", "初中", "高中", "大学", "硕士", "博士" };
            string eduSTR = string.Empty;
            for (int j = 0; j < estr.Length; j++)
            {
                if (eid > 0)
                {
                    if (edinfo.Levels == j)
                    {
                        eduSTR += "<option value=\"" + j + "\" selected>" + estr[j] + "</option>";
                    }
                    else
                    {
                        eduSTR += "<option value=\"" + j + "\">" + estr[j] + "</option>";
                    }
                }
                else
                {
                    eduSTR += "<option value=\"" + j + "\">" + estr[j] + "</option>";
                }
            }
            context.Put("eduinfo", eduSTR);
            #endregion
            Edulist(ref context);
        }

        public void Edulist(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);

            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_edu_aspx", PageIndex, 20, out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> edulist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable edu = new Hashtable();
                edu.Add("schoolname", dr["schoolname"]);
                if (dr["leaveyear"].ToString() == "0")
                {
                    edu.Add("leaveyear", "在读");
                }
                else
                {
                    edu.Add("leaveyear", dr["leaveyear"]);
                }
                edu.Add("levels", Public.GetLevel(dr["levels"]));
                edu.Add("id", dr["id"]);
                edulist.Add(edu);
            }
            dt.Dispose();
            context.Put("edulist", edulist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, 20));
        }


        public override void Page_PostBack(ref VelocityContext context)
        {
            string schoolname = GetString("txtschoolname");
            string leaveyear = GetString("txtleaveyear");
            int levels = GetFormInt("sltlevels", 0);
            string nolevel = GetString("chknolevel");
            if (!string.IsNullOrEmpty(nolevel))
            {
                leaveyear="0";
            }
            int eid = GetFormInt("eid", 0);
            EducationInfo edinfo = new EducationInfo();
            edinfo.ID = eid;
            edinfo.Leaveyear = leaveyear;
            edinfo.Levels = Convert.ToByte(levels);
            edinfo.PostTime = DateTime.Now;
            edinfo.SchoolName = schoolname;
            edinfo.UserID = this.UserID;
            int flag = 0;
            if (eid > 0) flag = 1;
            int m = JuSNS.Home.User.User.Instance.InsertEducation(edinfo, flag);
            ShowInfo(ref context);
            if (m > 0)
            {
                context.Put("redirecturl", "edu" + ExName);
            }
            else
            {
                context.Put("errors", "添加教育信息失败！");
            }
        }
    }
}
