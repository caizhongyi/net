using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.gift
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "赠送礼物");
            context.Put("maxsendfriend", Public.GetXMLGiftValue("maxsendfriend"));
            int maxgift = Convert.ToInt32(Public.GetXMLGiftValue("maxgift"));
            context.Put("maxgift", maxgift);
            int classid = GetInt("classid", 0);
            int uid = GetInt("uid", 0);
            if (uid > 0)
            {
                UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                context.Put("susername", mdl.TrueName);
            }
            context.Put("suid", uid);
            ShowList(ref context, classid);
            classlist(ref context, classid);
        }

        protected void classlist(ref VelocityContext context, int classid)
        {
            List<GiftClassInfo> infolist = JuSNS.Home.User.User.Instance.GetGiftClassList();
            List<Hashtable> giftclist = new List<Hashtable>();
            foreach (GiftClassInfo info in infolist)
            {
                Hashtable giftc = new Hashtable();
                giftc.Add("id", info.Id);
                if (classid == Convert.ToInt32(info.Id))
                {
                    giftc.Add("css", " class=\"current\"");
                }
                else
                {
                    giftc.Add("css", string.Empty);
                }
                giftc.Add("cname", info.ClassName);
                giftclist.Add(giftc);
            }
            context.Put("giftclist", giftclist);
        }

        protected void ShowList(ref VelocityContext context, int classid)
        {
            int uid = this.GetUserID();
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            if (classid > 0)
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@ClassID", classid, TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_giftclass_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_giftall_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("giftname", dr["giftname"]);
                info.Add("content", Input.FilterHTML(dr["content"].ToString()));
                string giftPath = Public.GetXMLGiftValue("picPath");
                info.Add("giftpic", giftPath + "/" + dr["pic"]);
                info.Add("giftgpoint", Convert.ToInt32(dr["gpoint"]));
                info.Add("giftpoint", Convert.ToInt32(dr["point"]));
                info.Add("sendnumber", Convert.ToInt32(dr["SendNumber"]));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'gift')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                if (Convert.ToBoolean(dr["IsAd"]))
                {
                    info.Add("isvips", true);
                }
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string frienduid = GetString("hidefrienduid");
            int giftid = GetInt("giftid", 0);
            int uid = this.UserID;
            int m = 0;
            int sm = 0;
            if (giftid > 0)
            {
                if (string.IsNullOrEmpty(frienduid))
                {
                    context.Put("errors", "请至少选择一个好友");
                }
                else
                {
                    string[] fuidARR = frienduid.Split(',');
                    sm = fuidARR.Length;
                    for (int i = 0; i < fuidARR.Length; i++)
                    {
                        GiftUserInfo mdl = new GiftUserInfo();
                        mdl.Content = GetString("txtcontent");
                        mdl.GiftID = giftid;
                        mdl.PostTime = DateTime.Now;
                        mdl.ReviceID = Convert.ToInt32(fuidARR[i]);
                        mdl.UserID = uid;
                        int n = JuSNS.Home.User.User.Instance.InsertGiftUser(mdl);
                        if (n == 1)
                        {
                            m++;
                            JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), Convert.ToInt32(fuidARR[i]), "给你赠送了一个礼物", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.CreatGift, giftid));
                            //插入动态
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), Convert.ToInt32(fuidARR[i]), (int)EnumDynType.CreatGift, string.Empty, DateTime.Now, giftid, string.Empty));
                            //更新积分
                            JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(23), 0, 0, "赠送礼物(系统)");
                        }
                    }
                    if (sm == m)
                    {
                        context.Put("rights", "为" + m + "个好友赠送了礼物");
                    }
                    else
                    {
                        context.Put("rights", "为" + m + "个好友赠送了礼物，赠送失败"+(sm-m)+"个。");
                    }
                }
            }
            else
            {
                context.Put("errors", "请至少选择一个礼物");
            }
            ShowInfo(ref context);
        }
    }
}
