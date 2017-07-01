using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.info
{
    public class group : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "群组请求");
            this.grouplist(ref context);
        }

        protected void grouplist(ref VelocityContext context)
        {
            List<GroupInviteInfo> ilist = JuSNS.Home.User.User.Instance.GetGroupRequest(this.UserID);
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (GroupInviteInfo ifo in ilist)
            {
                Hashtable info = new Hashtable();
                info.Add("userid", ifo.UserID);
                info.Add("id", ifo.ID);
                info.Add("reviceid", ifo.ReviceID);
                info.Add("truename", ifo.TrueName);
                info.Add("groupname", ifo.GroupName);
                info.Add("groupid", ifo.GroupID);
                info.Add("time", Public.getTimeEXTSpan(ifo.PostTime));
                info.Add("spaceurl", this.GetSpaceURL(ifo.UserID));
                info.Add("headpic", this.GetHeadImage(ifo.UserID));
                infolist.Add(info);
            }
            context.Put("infolist", infolist);
        }
    }
}
