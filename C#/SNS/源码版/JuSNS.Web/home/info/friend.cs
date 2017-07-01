using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.info
{
    public class friend : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "好友请求");
            this.Friendlist(ref context);
        }

        protected void Friendlist(ref VelocityContext context)
        {
            List<FriendInfo> ilist = JuSNS.Home.User.User.Instance.GetFriendRequest(this.UserID);
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (FriendInfo ifo in ilist)
            {
                Hashtable info = new Hashtable();
                info.Add("userid", ifo.FriendID);
                info.Add("id", ifo.ID);
                info.Add("truename", ifo.TrueName);
                info.Add("content", ifo.Descript);
                info.Add("time", Public.getTimeEXTSpan(ifo.PostTime));
                info.Add("spaceurl", this.GetSpaceURL(ifo.FriendID));
                info.Add("headpic", this.GetHeadImage(ifo.FriendID));
                infolist.Add(info);
            }
            context.Put("infolist", infolist);
        }
    }
}
