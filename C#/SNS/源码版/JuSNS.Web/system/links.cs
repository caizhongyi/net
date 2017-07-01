using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;
using System.Web;
using System.IO;
using System.Text;
namespace JuSNS.Web.system
{
    public class links : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "友情链接 - 管理中心");
            this.ShowList(ref context);
            int lid = GetInt("lid", 0);
            if (lid > 0)
            {
                LinksInfo info = JuSNS.Home.App.Web.Instance.GetLinksInfo(lid);
                context.Put("linkname", info.LinkName);
                context.Put("url", info.URL);
                context.Put("linktype", info.LinkType);
                context.Put("pic", info.Pic);
            }
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            dt = JuSNS.Home.UtilPage.GetPage("manager_links_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("linkname", dr["linkname"]);
                info.Add("url", dr["url"]);
                info.Add("type", dr["LinkType"].ToString() == "0" ? "文字" : "图片");
                info.Add("islock", Convert.ToBoolean(dr["islock"]));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string linkname = GetString("linkname");
            string url = GetString("url");
            if (string.IsNullOrEmpty(linkname) || string.IsNullOrEmpty(url))
            {
                context.Put("errors", "名称和地址必须填写，地址必须带http://");
            }
            else
            {
                LinksInfo info = new LinksInfo();
                info.Id = GetInt("lid", 0);
                info.Islock = false;
                info.LinkName = linkname;
                info.LinkType = Convert.ToByte(GetInt("linktype", 0));
                info.URL = url;
                string pics = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    pics = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Public.GetXMLPageValue("linkspath"));
                }
                if (string.IsNullOrEmpty(pics))
                {
                    info.Pic = GetString("hidepic");
                }
                else
                {
                    info.Pic = pics;
                }
                int n = JuSNS.Home.App.Web.Instance.InsertLinks(info);
                if (n > 0)
                {
                    context.Put("rights", "保存成功");
                }
                else
                {
                    context.Put("errors", "发生错误");
                }
                ShowInfo(ref context);
            }
        }
    }
}
