using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ative
{
    public class check : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "审核好友");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int aid = GetInt("aid", 0);
            AtiveInfo mdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            if (mdl.UserID != this.UserID)
            {
                context.Put("errors", "不是你的活动，你不可审核！");
            }
            else
            {
                List<AtiveMemberInfo> BInfolist = JuSNS.Home.App.Ative.Instance.GetCheckMemberList(50, aid, 0);
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
}
