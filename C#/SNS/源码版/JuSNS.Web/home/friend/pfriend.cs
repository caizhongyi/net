using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.friend
{
    public class pfriend : UserPage
    {
        public int recount = UiConfig.PNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "可能认识的人");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            showUserList(ref context, mdl.LastLoginIP);
        }

        protected void showUserList(ref VelocityContext context, string lastIP)
        {
            DataTable dt = JuSNS.Home.User.User.Instance.GetUserPossibleList(recount, this.UserID, lastIP);
            List<Hashtable> pfrdlist = new List<Hashtable>();
            UserInfo mdl = null;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable pfrd = new Hashtable();
                mdl = JuSNS.Home.User.User.Instance.GetUserInfo(dr["userid"]);
                pfrd.Add("userid", dr["userid"]);
                pfrd.Add("userhead", this.GetHeadImage(dr["UserID"], 1));
                pfrd.Add("truename", mdl.TrueName);
                pfrd.Add("spaceurl", this.GetSpaceURL(dr["UserID"]));
                pfrd.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.UserID, dr["UserID"]));
                pfrd.Add("twitter", Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["UserID"])));
                pfrd.Add("twittermore", Input.LostHTML(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["UserID"])));
                pfrdlist.Add(pfrd);
            }
            dt.Dispose();
            context.Put("pfrdlist", pfrdlist);
        }
    }
}