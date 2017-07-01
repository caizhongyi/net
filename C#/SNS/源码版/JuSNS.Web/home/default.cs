using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            string action = GetString("action");
            if (action == "SendDyn")
            {
                string divid = GetString("divid");
                switch (divid)
                {
                    case "dyn-all":
                        GetDyn(ref context, string.Empty, 1);
                        break;
                    case "dyn-system":
                        string systemSTR = this.GetSystem(10,0);
                        HttpContext.Current.Response.Write(systemSTR);
                        HttpContext.Current.Response.End();
                        break;
                    default:
                        GetDyn(ref context, divid, 0);
                        break;
                }
            }
            else
            {
                ShowInfo(ref context);
                bool isShow = false;
                HttpCookie SNSMessageCookie = HttpContext.Current.Request.Cookies["SNSShowSysMessage" + this.UserID];
                if (SNSMessageCookie != null && !string.IsNullOrEmpty(SNSMessageCookie["ShowSysMessage" + this.UserID]))
                {
                    if (SNSMessageCookie["ShowSysMessage" + this.UserID] == "1") isShow = true;
                }
                if (!isShow) context.Put("isshow", true);
            }
        }

        protected string GetSystem(int number,int flag)
        {
            string listSTR = string.Empty;
            List<NewsInfo> infolist = JuSNS.Home.App.News.Instance.GetNewsList(number, 6, 0);
            foreach (NewsInfo info in infolist)
            {
                if (flag == 0)
                {
                    listSTR += "<li class=\"systemitem\">";
                    listSTR += "<div class=\"systemtitle\" style=\"background:url(" + root + "/template/" + UiConfig.SkinStyle + "/images/sys.gif) no-repeat 0 3px;\"><a href=\"" + Public.URLWrite(info.Id, "news") + "\" target=\"_blank\">" + info.Title + "</a> <label class=\"time\">" + Public.getTimeEXPINSpan(info.PostTime) + "</label></div>";
                    listSTR += "<div class=\"systemcontent\">" + Input.GetSubString(Input.FilterHTML(info.Content), 160) + "</div>";
                    listSTR += "</li>";
                }
                else
                {
                    listSTR += "<li><a href=\"" + Public.URLWrite(info.Id, "news") + "\" target=\"_blank\">" + info.Title + "</a></li>\r\n";
                }
            }
            return listSTR;
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", this.TrueName + "@我的家园");
            string twitter = Input.ReplaceSmaile(JuSNS.Home.App.TWrite.Instance.GetTwritterNew(this.UserID));
            context.Put("twitter", twitter);
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            UserBaseInfo basi = JuSNS.Home.User.User.Instance.GetUserBaseInfo(this.UserID);
            DateTime ntime = DateTime.Now;
            DateTime btime = basi.Birthday;
            if ((ntime - btime).Days < 21600)
            {
                context.Put("birthday", basi.Birthday.ToString("yyyy年MM月dd日"));
            }
            if (mdl.IsVip)
            {
                context.Put("isvip", true);
            }
            byte state = mdl.State;
            if ((EnumUserState)state == EnumUserState.NormalNotEmail || (EnumUserState)state == EnumUserState.Register)
            {
                context.Put("notemail", true);
            }
            context.Put("click", mdl.Click);
            context.Put("email", mdl.Email);
            context.Put("code", mdl.VerifyCode);
            context.Put("integral", mdl.Integral);
            context.Put("memberleves", Public.GetMemberlevels(mdl.MemberLevels));
            context.Put("mlnumber", mdl.MemberLevels);
            context.Put("inteyb", mdl.Inteyb);
            context.Put("sex", mdl.Sex == 0 ? "男" : "女");
            context.Put("nowcity", JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.ProvinceID).Name + JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.City).Name);
            visitlist(ref context);
            showCityList(ref context,mdl);
            frdlists(ref context);
            RegisetNew(ref context);
            context.Put("friendinfo", JuSNS.Home.User.User.Instance.GetNote(this.UserID, 0, 0));
            context.Put("groupinfo", JuSNS.Home.User.User.Instance.GetNote(this.UserID, 0, 1));
            context.Put("noteinfo", JuSNS.Home.User.User.Instance.GetNote(this.UserID, 0, 2));
            context.Put("boxinfo", JuSNS.Home.User.User.Instance.GetNote(this.UserID, 0, 3));
            context.Put("celinfo", JuSNS.Home.User.User.Instance.GetNote(this.UserID, 0, 4));
            GetDyn(ref context, string.Empty, 0);
            context.Put("announce", GetSystem(10, 1));
        }

        protected void visitlist(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_visited_aspx", PageIndex, Convert.ToInt32(Public.GetXMLBaseValue("visitenumber")), out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> visitlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable visit = new Hashtable();
                visit.Add("truename", dr["truename"]);
                visit.Add("id", dr["id"]);
                visit.Add("friendid", dr["VisitorID"]);
                visit.Add("spaceurl", this.GetSpaceURL(dr["VisitorID"]));
                visit.Add("userhead", this.GetHeadImage(dr["VisitorID"], 1));
                visitlist.Add(visit);
            }
            dt.Dispose();
            context.Put("visitlist", visitlist);
        }

        protected void showCityList(ref VelocityContext context,UserInfo mdl)
        {
            int City = mdl.City;
            int Sex = mdl.Sex;
            DataTable dt = JuSNS.Home.User.User.Instance.GetUserFriendList(Convert.ToInt32(Public.GetXMLBaseValue("citynumber")), this.UserID, City, Sex);
            List<Hashtable> citylist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable city = new Hashtable();
                city.Add("userid", dr["userid"]);
                city.Add("userhead", this.GetHeadImage(dr["UserID"], 1));
                city.Add("truename", dr["truename"]);
                city.Add("spaceurl", this.GetSpaceURL(dr["UserID"]));
                citylist.Add(city);
            }
            dt.Dispose();
            context.Put("citylist", citylist);
        }

        protected void frdlists(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            int classid = GetInt("q", 0);
            dt = JuSNS.Home.UtilPage.GetPage("user_friend_aspx", PageIndex, Convert.ToInt32(Public.GetXMLBaseValue("friendnumber")), out ReCount, out PgCount, new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32));
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> frdlist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable frd = new Hashtable();
                frd.Add("truename", dr["truename"]);
                frd.Add("id", dr["id"]);
                frd.Add("friendid", dr["friendid"]);
                frd.Add("spaceurl", this.GetSpaceURL(dr["friendid"]));
                frd.Add("userhead", this.GetHeadImage(dr["friendid"], 1));
                 frdlist.Add(frd);
            }
            dt.Dispose();
            context.Put("frdlist", frdlist);
        }

        protected void RegisetNew(ref VelocityContext context)
        {
            List<UserInfo> Infolist = JuSNS.Home.User.User.Instance.RegisterUserNew(Convert.ToInt32(Public.GetXMLBaseValue("regnumber")),this.UserID);
            List<Hashtable> reglist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable reg = new Hashtable();
                reg.Add("userid", info.UserID);
                reg.Add("truename", info.TrueName);
                reg.Add("spaceurl", this.GetSpaceURL(info.UserID));
                reg.Add("userhead", this.GetHeadImage(info.UserID));
                reglist.Add(reg);
            }
            context.Put("reglist", reglist);
        }
        /// <summary>
        /// 得到动态
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dyntype"></param>
        /// <param name="isall"></param>
        protected void GetDyn(ref VelocityContext context, string dyntype, int isall)
        {
            string listSTR = string.Empty;
            string filepath = "~/space/info/my/user" + this.UserID + ".config";
            int dynnumber = 20;
            string tmpNumber = Public.GetXMLValue("dynnumber", filepath);
            if (Input.IsInteger(tmpNumber) && tmpNumber != "0")
            {
                dynnumber = Convert.ToInt32(Public.GetXMLValue("dynnumber", filepath));
            }
            string killuser = Public.GetXMLValue("killuser", filepath);
            string FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(this.UserID);
            //得到我关注的人
            string AttSTR = Input.FixCommaStr(JuSNS.Home.User.User.Instance.GetAtt(this.UserID));
            if (!string.IsNullOrEmpty(AttSTR)) { FriendSTR = FriendSTR + "," + AttSTR; }
            List<DynInfo> infolist = JuSNS.Home.User.User.Instance.GetDynList(dynnumber, this.UserID, FriendSTR, killuser, dyntype);
            string gDateSTR = string.Empty;
            bool todaytf = false;
            bool yesdaytf = false;
            bool longtf = false;
            bool longlongtf = false;
            string TmpSTR = string.Empty;
            if (string.IsNullOrEmpty(dyntype))
            {
                int showannounce = Convert.ToInt32(Public.GetXMLBaseValue("showannounce"));
                if(showannounce==1) TmpSTR = GetSystem(1,0);
            }
            bool isadmin = JuSNS.Home.User.User.Instance.IsAdmin(this.UserID);
            StringBuilder sb = new StringBuilder();
            foreach (DynInfo info in infolist)
            {
                string TmpUserLogStr = JuSNS.MVC.ParseUserLog.Instance.Parse(info, this.UserID);
                if (!string.IsNullOrEmpty(TmpUserLogStr))
                {
                    DateTime nday = DateTime.Now;
                    string ShowDateSTR = string.Empty;
                    if (gDateSTR != info.PostTime.ToString("MM-dd"))
                    {
                        if (info.PostTime.ToString("MM-dd") == nday.ToString("MM-dd")) { if (!todaytf) { ShowDateSTR = "<li class=\"dyncli\">今天</li>"; todaytf = true; } }
                        else if (info.PostTime.ToString("MM-dd") == (nday.AddDays(-1)).ToString("MM-dd")) { if (!yesdaytf) { ShowDateSTR = "<li class=\"dyncli\">昨天</li>"; yesdaytf = true; } }
                        else if (info.PostTime.ToString("MM-dd") == (nday.AddDays(-2)).ToString("MM-dd")) { if (!longtf) { ShowDateSTR = "<li class=\"dyncli\">前天</li>"; longtf = true; } }
                        else { if (!longlongtf) { ShowDateSTR = "<li class=\"dyncli\">以前的动态</li>"; longlongtf = true; } }
                    }
                    else
                    {
                        ShowDateSTR = string.Empty;
                    }
                    string deleteSTR = string.Empty;
                    if (isadmin)
                    {
                        deleteSTR = "<a href=\"javascript:;\" title=\"删除此动态\" onclick=\"deleteAll(" + info.ID + "," + this.UserID + ",'dyn')\" class=\"showok1\"></a>";
                    }
                    TmpUserLogStr = TmpUserLogStr.Replace("{ColseDyn}", "<a href=\"javascript:;\" title=\"屏蔽此用户的动态\" onclick=\"colosedyn(" + info.ID + "," + info.UserID + "," + this.UserID + ")\" class=\"dync\"></a>" + deleteSTR);
                    listSTR += ShowDateSTR + "<li id=\"param_" + info.ID + "\">" + TmpUserLogStr + "</li>";
                }
            }
            if (isall == 1 || !string.IsNullOrEmpty(dyntype))
            {
                HttpContext.Current.Response.Write(listSTR);
                HttpContext.Current.Response.End();
            }
            else
            {
                context.Put("dynall", TmpSTR + listSTR);
            }
        }

        protected string FromDynType(EnumDynType dyntype)
        {
            string listSTR = string.Empty;

            return listSTR;
        }

       
    }
}
