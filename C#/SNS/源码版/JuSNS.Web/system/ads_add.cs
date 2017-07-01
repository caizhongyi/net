using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class ads_add : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int aid = GetInt("aid", 0);
            if (aid > 0)
            {
                context.Put("cpagetitle", "修改广告");
                AdsInfo info = JuSNS.Home.App.Web.Instance.GetAdsInfo(aid);
                context.Put("Title", info.Title);
                context.Put("positionType", info.PositionType);
                context.Put("Pic", info.Pic);
                if (info.IsLock)
                {
                    context.Put("IsLock", info.IsLock);
                }
                context.Put("URL", info.URL);
                context.Put("Content", info.Content);
                context.Put("EndTime", info.EndTime);
            }
            else
            {
                context.Put("cpagetitle", "添加广告");
                context.Put("Pic", string.Empty);
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string Title = GetString("Title");
            string positionType = GetString("positionType");
            string URL = GetString("URL");
            if (string.IsNullOrEmpty(Title))
            {
                context.Put("errors", "广告标题、广告链接地址、广告位必须填写。");
            }
            else
            {
                AdsInfo info = new AdsInfo();
                info.Click = 0;
                info.Content = GetString("Content");
                info.EndTime = Convert.ToDateTime(GetString("EndTime"));
                info.Id = GetInt("aid", 0);
                if (GetInt("IsLock", 0) == 1)
                {
                    info.IsLock = true;
                }
                else
                {
                    info.IsLock = false;
                }
                info.PositionType = positionType;
                info.Title = Title;
                info.URL = URL;
                string pic = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Public.GetXMLPageValue("adspic"));
                }
                if (string.IsNullOrEmpty(pic))
                {
                    info.Pic = GetString("opic");
                }
                else
                {
                    info.Pic = pic;
                }
                int n = JuSNS.Home.App.Web.Instance.InsertAds(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "ads" + ExName);
                }
                else
                {
                    context.Put("errors", "发生错误");
                }
            }
        }
    }
}
