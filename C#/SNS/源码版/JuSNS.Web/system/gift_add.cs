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
    public class gift_add : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int gid = GetInt("gid", 0);
            int sid = 0;
            if (gid > 0)
            {
                context.Put("cpagetitle", "修改礼物");
                GiftInfo info = JuSNS.Home.User.User.Instance.GetGiftInfo(gid);
                context.Put("Content", info.Content);
                context.Put("GiftName", info.GiftName);
                context.Put("GPoint", info.GPoint);
                if (info.IsAd)
                {
                    context.Put("IsAd", info.IsAd);
                }
                context.Put("IsLock", info.IsLock);
                context.Put("Pic", info.Pic);
                context.Put("Point", info.Point);
                sid = info.ClassID;
            }
            else
            {
                context.Put("cpagetitle", "添加礼物");
                context.Put("Pic",string.Empty);
            }
            context.Put("classlist", GetClassList(0, string.Empty, sid));
        }

        protected string GetClassList(int parentid, string TmpSTR, int sid)
        {
            string listSTR = string.Empty;
            List<GiftClassInfo> infolist = JuSNS.Home.User.User.Instance.GetGiftClassList();
            foreach (GiftClassInfo info in infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=\"" + info.Id + "\" selected>" + TmpSTR + info.ClassName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.Id + "\">" + TmpSTR + info.ClassName + "</option>";
                }
            }
            return listSTR;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string GiftName = GetString("GiftName");
            if (string.IsNullOrEmpty(GiftName))
            {
                context.Put("errors", "礼物名称必须填写");
            }
            else
            {
                GiftInfo info = new GiftInfo();
                info.ClassID = GetInt("classid", 0);
                info.Content = GetString("Content");
                info.GiftName = GetString("GiftName");
                info.GPoint = GetInt("GPoint", 0);
                info.Id = GetInt("gid", 0);
                if (GetInt("IsAd", 0) == 1)
                {
                    info.IsAd = true;
                }
                else
                {
                    info.IsAd = false;
                }
                info.IsLock = Convert.ToByte(GetInt("IsLock", 0));
                info.Point = GetInt("Point", 0);
                info.PostTime = DateTime.Now;
                info.SendNumber = 0;
                string pic = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Public.GetXMLGiftValue("picPath"));
                }
                if (string.IsNullOrEmpty(pic))
                {
                    info.Pic = GetString("opic");
                }
                else
                {
                    info.Pic = pic;
                }
                int n = JuSNS.Home.User.User.Instance.InsertGift(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "gift" + ExName);
                }
                else
                {
                    context.Put("errors", "发生错误");
                }
            }
        }
    }
}
