using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.UI.Page;
using JuSNS.Model;
using NVelocity;

namespace JuSNS.Web
{
    public class ads : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            string listSTR = string.Empty;
            int num = GetInt("num", 1);
            string ptype = GetString("ptype");
            if (!string.IsNullOrEmpty(ptype))
            {
                List<AdsInfo> infolist = JuSNS.Home.App.Web.Instance.GetAdsList(ptype,num);
                HttpContext.Current.Response.ClearContent();
                foreach (AdsInfo info in infolist)
                {
                    if (!string.IsNullOrEmpty(info.Pic))
                    {
                        listSTR += "<li><a href='" + info.URL + "' target='_blank' title='"+info.Title+"'><img src='" + Public.GetXMLPageValue("adspic") + "/" + info.Pic + "' /></a></li>";
                    }
                    else
                    {
                        listSTR += "<li><a href='" + info.URL + "' target='_blank'>" + info.Title + "</a></li>";
                    }
                }
            }
            HttpContext.Current.Response.Write("document.write(\"" + listSTR + "\")");
            HttpContext.Current.Response.End();
        }
    }
}