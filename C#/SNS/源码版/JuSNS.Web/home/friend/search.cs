using System;
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
    public class search : BasePage
    {
        public int recount =40;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
            int GuestSearch = Convert.ToInt32(Public.GetXMLValue("GuestSearch"));
            if (GuestSearch == 0)
            {
                if (GetUserID() == 0)
                {
                    PageError("必须要会员才能搜索！", root + "/");
                }
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "搜索好友");
            ShowList(ref context);
            this.GetCity(ref context);
            this.SEYears(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            string r = GetString("r");
            string keys = GetString("keys");
            context.Put("keys", keys);
            int IsHead = GetInt("IsHead", 0);
            if (IsHead == 0)
            {
                context.Put("isheadcheck", string.Empty);
            }
            else
            {
                context.Put("isheadcheck", "checked");
            }
            int city = 0;
            if (!string.IsNullOrEmpty(GetString("SlctCity")))
            {
                city = Convert.ToInt32(GetString("SlctCity"));
            }
            else
            {
                if (GetString("SlctCity")!="0")
                {
                    city = Convert.ToInt32(GetInt("SlctProvince", 0));
                }
            }
            context.Put("selvalue", city);
            int sex = -1;
            if (GetInt("sex", -1) != -1)
            {
                sex = GetInt("sex", 0);
            }
            switch (sex)
            {
                case 0:
                    context.Put("check0", "checked");
                    context.Put("check1", string.Empty);
                    context.Put("check_1",string.Empty);
                    break;
                case 1:
                    context.Put("check0", string.Empty);
                    context.Put("check1", "checked");
                    context.Put("check_1", string.Empty);
                    break;
                case -1:
                    context.Put("check0", string.Empty);
                    context.Put("check1", string.Empty);
                    context.Put("check_1", "checked");
                    break;
            }
            int uID = GetUserID();
            int syear=GetInt("selBegin",0);
            int eyear=GetInt("selEnd",0);
            dt = JuSNS.Home.UtilPage.GetUserSearchPage(r, keys, IsHead, city, syear, eyear, sex, uID, PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> sealist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable sea = new Hashtable();
                sea.Add("userid", dr["userid"]);
                sea.Add("friendid", dr["userid"]);
                string sexs = "美女";
                if (dr["sex"].ToString() == "0")
                {
                    sexs = "帅哥";
                }
                sea.Add("sexs", sexs);
                sea.Add("spaceurl", this.GetSpaceURL(dr["UserID"]));
                sea.Add("userhead", this.GetHeadImage(dr["UserID"], 1));
                sea.Add("truename", Input.GetSubString(dr["truename"].ToString(), 6));
                sea.Add("isfriend", JuSNS.Home.User.User.Instance.IsFriends(this.GetUserID(), dr["UserID"]));
                sealist.Add(sea);
            }
            dt.Dispose();
            context.Put("sealist", sealist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        protected void GetCity(ref VelocityContext context)
        {
            int pro = Convert.ToInt32(GetInt("SlctProvince", 0));
            string province = string.Empty;
            string cityarray = string.Empty;
            IList<DictAreaInfo> list = JuSNS.Home.Other.Area.Instance.GetArea();
            foreach (DictAreaInfo info in list)
            {
                if (info.ParentID.Equals(0))
                {
                    if (info.ID == pro)
                    {
                        province += "<option value=\"" + info.ID + "\" selected>" + info.Name + "</option>";
                    }
                    else
                    {
                        province += "<option value=\"" + info.ID + "\">" + info.Name + "</option>";
                    }
                }
                else
                {
                    if (!cityarray.Equals(string.Empty))
                        cityarray += ",";
                    cityarray += "new Array('" + info.ParentID + "','" + info.ID + "','" + info.Name + "')";
                }
            }
            context.Put("province", province);
            context.Put("cityarray", cityarray);
        }

        protected void SEYears(ref VelocityContext context)
        {
            int syear = GetInt("selBegin", 0);
            int eyear = GetInt("selEnd", 0);
            string sSTR = string.Empty;
            for (int i = 12; i < 81; i++)
            {
                if (syear == i)
                {
                    sSTR += "<option value=\"" + i + "\" selected>" + i + "</option>";
                }
                else
                {
                    sSTR += "<option value=\"" + i + "\">" + i + "</option>";
                }
            }
            context.Put("beginyears", sSTR);

            string eSTR = string.Empty;
            for (int j = 12; j < 81; j++)
            {
                if (eyear == j)
                {
                    eSTR += "<option value=\"" + j + "\" selected>" + j + "</option>";
                }
                else
                {
                    eSTR += "<option value=\"" + j + "\">" + j + "</option>";
                }
            }
            context.Put("endyears", eSTR);
        }
    }
}