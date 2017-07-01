using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class @class : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "好友分组");
            string fids = GetQueryString("fids");
            string cname = GetString("cname");
            int fid = GetInt("fid", 0);
            int id = GetInt("id", 0);
            context.Put("fid", fid);
            if (!string.IsNullOrEmpty(fids))
            {
                context.Put("fids", fids);
                context.Put("cname", cname);
                context.Put("button", "修改");
            }
            else
            {
                context.Put("fids", 0);
                context.Put("cname", string.Empty);
                context.Put("button", "增加");
            }
            DataTable dt = JuSNS.Home.User.User.Instance.GetFriendClass(this.UserID);
            List<Hashtable> frdlist = new List<Hashtable>();
            bool ischeck = false;
            context.Put("ids", id);
            FriendInfo frdinfo = JuSNS.Home.User.User.Instance.GetFriendInfo(id);
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frd = new Hashtable();
                if (Convert.ToInt32(dr["userid"]) == 0)
                {
                    frd.Add("cname", "<span class=\"red\" title=\"默认分组\">" + dr["CName"] + "</span>");
                    frd.Add("delete", string.Empty);
                }
                else
                {
                    frd.Add("cname", "<a href=\"class" + ExName + "?id=" + id + "&fids=" + dr["id"] + "&fid=" + fid + "&cname=" + Input.URLEncode(dr["CName"].ToString()) + "\" title=\"点击修改\">" + dr["CName"] + "</a>");
                    frd.Add("delete", "<a href=\"javascript:void(0)\" onclick=\"DeleteFriendClass(" + dr["id"] + "," + this.UserID + ")\" class=\"showok1\"></a>");
                }
                frd.Add("id", dr["id"]);
                if (frdinfo.ClassID == Convert.ToInt32(dr["id"]))
                {
                    frd.Add("checked", "checked");
                    ischeck = true;
                }
                else
                {
                    frd.Add("checked", string.Empty);
                }
                frdlist.Add(frd);
            }
            if (ischeck)
            {
                context.Put("checked0", string.Empty);
            }
            else
            {
                context.Put("checked0", "checked");
            }
            dt.Dispose();
            context.Put("frdlist", frdlist);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int flassid = GetFormInt("friendclassid", 0);
            int fid = GetInt("fid", 0);
            context.Put("fid", fid);
            int m = JuSNS.Home.User.User.Instance.UpdateFriendClass(fid, flassid, this.UserID);
            if (m > 0)
            {
                context.Put("rights", "设置好友分组成功！");
            }
            else
            {
                context.Put("errors", "设置好友分组失败");
            }
            ShowInfo(ref context);
        }
    }
}
