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
    public class city : UserPage
    {
        public int recount = UiConfig.CityNumber;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "同城好友");
            //得到城市
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            int City = mdl.City;
            int Sex = mdl.Sex;
            if (City == 0)
            {
                this.PageError("请填写目前所居住地后进行此操作", root + "/space/info/profile" + ExName + "");
            }
            else
            {
                DictAreaInfo dinfo = JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.City);
                context.Put("cityname", dinfo.Name);
                if (GetString("q") == "ram")
                {
                    City = 0;
                    context.Put("cpagetitle", "有缘人");
                }
                else
                {
                    if (!string.IsNullOrEmpty(GetString("sex")))
                    {
                        int GetSex = GetInt("sex", 0);
                        Sex = GetSex;
                    }
                    JuSNS.MVC.GetCSS.CSS(ref context, GetString("sex"));
                }
                showCityList(ref context, City, Sex);
            }
        }

        protected void showCityList(ref VelocityContext context, int City, int sex)
        {
            DataTable dt = JuSNS.Home.User.User.Instance.GetUserFriendList(recount, this.UserID, City, sex);
            List<Hashtable> citylist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable city = new Hashtable();
                city.Add("userid", dr["userid"]);
                city.Add("userhead", this.GetHeadImage(dr["UserID"], 1));
                city.Add("truename", dr["truename"]);
                city.Add("spaceurl", this.GetSpaceURL(dr["UserID"]));
                city.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.UserID, dr["UserID"]));
                city.Add("twitter", Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["UserID"])));
                city.Add("twittermore", Input.LostHTML(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(dr["UserID"])));
                citylist.Add(city);
            }
            dt.Dispose();
            context.Put("citylist", citylist);
        }
    }
}