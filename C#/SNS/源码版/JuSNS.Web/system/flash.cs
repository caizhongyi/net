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
    public class flash : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "首页幻灯片管理 - 管理中心");
            this.ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 50;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            dt = JuSNS.Home.UtilPage.GetPage("manager_flash_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("spic", dr["spic"]);
                info.Add("bpic", dr["bpic"]);
                info.Add("path", Public.GetXMLPageValue("flashpath"));
                info.Add("url", dr["url"]);
                info.Add("orderid", dr["orderid"]);
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
            string path = "~/library/js/flashView.js";
            string flashpath = root + Public.GetXMLPageValue("flashpath");
            string blist = string.Empty;
            string slist = string.Empty;
            string url = string.Empty;
            int recount = 9;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            //if (File.Exists(HttpContext.Current.Server.MapPath(path)))
            //    File.Delete(HttpContext.Current.Server.MapPath(path));
            //File.CreateText(HttpContext.Current.Server.MapPath(path));
            DataTable dt = JuSNS.Home.UtilPage.GetPage("manager_flash_make_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count < 2)
                {
                    context.Put("errors", "至少要2张图片才能生成幻灯片");
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((i + 1) == dt.Rows.Count)
                        {
                            blist += flashpath + "/" + dt.Rows[i]["bpic"];
                            slist += flashpath + "/" + dt.Rows[i]["spic"];
                            url += dt.Rows[i]["url"];
                        }
                        else
                        {
                            blist += flashpath + "/" + dt.Rows[i]["bpic"] + ",";
                            slist += flashpath + "/" + dt.Rows[i]["spic"] + ",";
                            url += dt.Rows[i]["url"] + ",";
                        }
                    }
                    string listSTR = string.Empty;
                    listSTR += "function loadFlashView()\r\n";
                    listSTR += "{\r\n";
                    listSTR += "SwfView.Add(\"" + root + "/library/flash/view.swf\", \"fpv\", \"swfDIV\", \"640\", \"260\", \"8.0.0.0\", \"#000\", \r\n";
                    listSTR += "{\r\n";
                    listSTR += "bigPhotoList: \"" + blist + "\",\r\n";
                    listSTR += "smallPhotoList: \"" + slist + "\",\r\n";
                    listSTR += "sourcePhotoList: \"" + url + "\", itemOverTime: 100, viewTime: 5000\r\n";
                    listSTR += "}\r\n";
                    listSTR += ",\r\n";
                    listSTR += "{\r\n";
                    listSTR += "scale: \"noscale\", allowScriptAccess: \"always\", wmode: \"transparent\"\r\n";
                    listSTR += "});\r\n";
                    listSTR += "SwfView.Init();\r\n";
                    listSTR += "}";
                    //StreamWriter sw = new StreamWriter(new FileStream(HttpContext.Current.Server.MapPath(path), FileMode.Create), Encoding.UTF8);
                    using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(path), false))
                    {
                        sw.WriteLine(listSTR);
                        sw.Close();
                        sw.Dispose();
                        context.Put("rights", "生成首页Flash幻灯片成功！");
                    }
                }
            }
            else
            {
                context.Put("errors", "没有需要生成的内容");
            }
            dt.Clear(); dt.Dispose();
            ShowInfo(ref context);
        }
    }

}
