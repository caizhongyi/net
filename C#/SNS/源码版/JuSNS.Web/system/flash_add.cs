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
    public class flash_add : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int fid = GetInt("fid", 0);
            if (fid > 0)
            {
                context.Put("cpagetitle", "修改幻灯片");
                FlashInfo info = JuSNS.Home.User.User.Instance.GetFlashInfo(fid);
                context.Put("BPIC", info.BPic);
                context.Put("SPIC", info.SPic);
                context.Put("URL", info.URL);
                context.Put("OrderID", info.OrderID);
                context.Put("IsLock", info.IsLock);
            }
            else
            {
                context.Put("cpagetitle", "添加幻灯片");
            }

        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string HBPIC = GetString("HBPIC");
            string HSPIC = GetString("HSPIC");
            if (string.IsNullOrEmpty(GetString("URL")))
            {
                context.Put("errors", "添加URL链接地址，必须带http://");
            }
            else
            {
                FlashInfo info = new FlashInfo();
                info.Id = GetInt("fid", 0);
                info.IsLock = Convert.ToBoolean(GetInt("IsLock", 0));
                info.PostTime = DateTime.Now;
                info.OrderID = GetInt("OrderID", 0);
                info.URL = GetString("URL");
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    string bpic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Public.GetXMLPageValue("flashpath"));
                    if (string.IsNullOrEmpty(bpic))
                    {
                        info.BPic = HBPIC;
                    }
                    else
                    {
                        info.BPic = bpic;
                    }
                    HttpPostedFile hpf1 = HttpContext.Current.Request.Files[1];
                    string spic = Public.GetFile(hpf1, Public.GetXMLValue("pictype"), Public.GetXMLPageValue("flashpath"));
                    if (string.IsNullOrEmpty(spic))
                    {
                        info.SPic = HSPIC;
                    }
                    else
                    {
                        info.SPic = spic;
                    }
                }
                else
                {
                    info.BPic = HBPIC;
                    info.SPic = HSPIC;
                }
                int n = JuSNS.Home.User.User.Instance.InsertFlash(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "flash" + ExName);
                }
                else
                {
                    context.Put("errors", "发生错误");
                }
            }
            ShowInfo(ref context);
        }
    }
}