using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ative
{
    public class album : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int isOpen = Convert.ToInt16(Public.GetXMLAtiveValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ative");
            }
            else
            {
                int ismember = Convert.ToInt16(Public.GetXMLAtiveValue("ismember"));
                if (ismember != 1)
                {
                    if (uid == 0)
                    {
                        context.Put("redirecturl", root + "/library/page/error" + ExName + "?error=Err_TimeOut&urls=" + root + "/login" + ExName + "?urls=" + HttpContext.Current.Request.Url);
                    }
                }
                int aid = GetInt("aid", 0);
                ShowInfo(ref context, uid);
            }
        }

        protected void ShowInfo(ref VelocityContext context, int uid)
        {
            base.Page_Load(ref context);
            int aid = GetInt("aid", 0);
            context.Put("aid", aid);
            AtiveInfo mdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            context.Put("cpagetitle", mdl.AtiveName + "相册");
            ShowList(ref context, aid);
        }

        protected void ShowList(ref VelocityContext context, int aid)
        {
            List<PhotoInfo> BInfolist = JuSNS.Home.App.Ative.Instance.GetAtiveAlbumList(Convert.ToInt32(Public.GetXMLAtiveValue("photolistnumber")), aid);
            List<Hashtable> photoinfolist = new List<Hashtable>();
            foreach (PhotoInfo photoinfo in BInfolist)
            {
                Hashtable cks = new Hashtable();
                cks.Add("id", photoinfo.Id);
                cks.Add("aid", photoinfo.AtiveID);
                cks.Add("userid", photoinfo.UserID);
                cks.Add("truename", photoinfo.TrueName);
                cks.Add("photourl", this.GetSmallPic(photoinfo.FilePath, 1));
                cks.Add("time", Public.getTimeEXTSpan(photoinfo.PostTime));
                photoinfolist.Add(cks);
            }
            context.Put("photoinfolist", photoinfolist);
        }
    }
}