using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;
namespace JuSNS.Web.app.ative
{
    public class member : BasePage
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
                int flag = GetInt("flag", 0);
                int aid = GetInt("aid", 0);
                JuSNS.MVC.GetCSS.CSS(ref context, GetString("flag"));
                if (flag == 0) flag = 2; else flag = 1;
                ShowInfo(ref context, uid, flag);
                ShowList(ref context, aid, flag);
            }
        }

        protected void ShowInfo(ref VelocityContext context, int uid, int flag)
        {
            base.Page_Load(ref context);
            int aid = GetInt("aid", 0);
            context.Put("aid", aid);
            AtiveInfo mdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            if (flag == 1)
            {
                context.Put("attcss", " class=\"current\"");
                context.Put("cpagetitle", "关注的会员 - " + mdl.AtiveName);
            }
            else
            {
                context.Put("joincss", " class=\"current\"");
                context.Put("cpagetitle", "报名的会员 - " + mdl.AtiveName);
            }

        }

        protected void ShowList(ref VelocityContext context, int aid,int flag)
        {
            List<AtiveMemberInfo> BInfolist = JuSNS.Home.App.Ative.Instance.GetCheckMemberList(Convert.ToInt32(Public.GetXMLAtiveValue("memberlistnumber")), aid, flag);
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (AtiveMemberInfo info in BInfolist)
            {
                Hashtable cks = new Hashtable();
                cks.Add("id", info.Id);
                cks.Add("aid", info.Aid);
                cks.Add("userid", info.UserID);
                cks.Add("truename", info.TrueName);
                cks.Add("spaceurl", this.GetSpaceURL(info.UserID));
                cks.Add("headpic", this.GetHeadImage(info.UserID));
                cks.Add("time", Public.getTimeEXTSpan(info.PostTime));
                infolist.Add(cks);
            }
            context.Put("infolist", infolist);
        }
    }
}
