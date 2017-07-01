using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.user.shop
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int isOpen = Convert.ToInt16(Public.GetXMLShopValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=shop");
            }
            else
            {
                ShowInfo(ref context);
            }
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int sid = GetInt("sid", 0);
            context.Put("sid", sid);
            ShopInfo info = JuSNS.Home.App.Shop.Instance.GetShopForID(sid);
            if (info == null)
            {
                PageError("找不到店铺", root + "/");
                //context.Put("errors", "错误的参数");
            }
            else
            {
                if (info.IsLock == 0)
                {
                    context.Put("cpagetitle", info.ShopName + " - 店铺管理");
                    context.Put("shopname", info.ShopName);
                    context.Put("shopurl", Public.URLWrite(sid, "shop"));
                    context.Put("shopbg", Public.GetXMLShopValue("shopPicPath") + "/" + info.Pic);
                    context.Put("jointime", info.PostTime.ToString("yyyy年MM月dd日"));
                    context.Put("address", JuSNS.Home.Other.Area.Instance.GetAreaInfo(info.AreaID).Name + info.ShopAddress);
                    context.Put("tel", info.Tel);
                    context.Put("fax", info.Fax);
                    context.Put("linkman", info.LinkMan);
                    context.Put("userid", info.UserID);
                    context.Put("content", Input.GetSubString(info.Content, 80));
                    string picpath = Public.GetXMLShopValue("shopPicPath");
                    context.Put("shoppicpath", picpath);
                    ShowList(ref context, sid);
                    ShowMulteList(ref context, sid);
                    ShowProList(ref context, sid);
                    ShowCommentList(ref context, sid);
                }
                else
                {
                    PageError("此店铺未开放", root + "/");
                    //context.Put("errors", "此店铺未开放");
                }
            }
        }

        protected void ShowCommentList(ref VelocityContext context, int gid)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLShopValue("CommentNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@GID", gid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shop_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> commlist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable comm = new Hashtable();
                comm.Add("id", dr["id"]);
                comm.Add("userid", dr["userid"]);
                comm.Add("truename", dr["truename"]);
                comm.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                comm.Add("headpic", this.GetHeadImage(dr["userid"]));
                comm.Add("time", Public.getTimeSpan(Convert.ToDateTime(dr["posttime"])));
                string ReplaySTR = string.Empty;
                if (Convert.ToInt32(dr["commid"]) > 0)
                {
                    GoodsCommentInfo bml = JuSNS.Home.App.Shop.Instance.GetGoodsCommentInfo(Convert.ToInt32(dr["commid"]));
                    ReplaySTR = "<div class=\"ctxreplay\"><b>" + bml.TrueName + "</b>：" + Input.ReplaceSmaile(bml.Content) + "</div>";
                }
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"].ToString()));
                bool isOp = false;
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin > 0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != uid)
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["pid"] + "," + uid + ",'" + dr["truename"] + "','shop');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deletetgoodscomment(" + dr["id"] + "," + uid + ")\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcounts", ReCount);
            context.Put("pagelists", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }


        protected void ShowList(ref VelocityContext context,int sid)
        {
            int uid = this.GetUserID();
            int recount = 6;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", sid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopnews_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("title", Input.GetSubString(dr["title"].ToString(),36));
                info.Add("shopnewsurl", root + "/user/shop/info" + ExName + "?nid=" + dr["id"]);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
        }

        protected void ShowMulteList(ref VelocityContext context, int sid)
        {
            string picpath = Public.GetXMLShopValue("shopPicPath");
            int recount = 2;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", sid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopmulte_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> multelist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable multe = new Hashtable();
                multe.Add("id", dr["id"]);
                multe.Add("title", Input.GetSubString(dr["title"].ToString(), 30));
                multe.Add("multeurl", Public.URLWrite(dr["id"], "multe"));
                string states = string.Empty;
                DateTime ntime = DateTime.Now;
                DateTime etime = Convert.ToDateTime(dr["EndTime"]);
                if ((ntime - etime).Days > 0)
                {
                    multe.Add("timeout", true);
                }
                multe.Add("starttime", Convert.ToDateTime(dr["starttime"]).ToString("MM月dd日"));
                multe.Add("endtime", Convert.ToDateTime(dr["endtime"]).ToString("MM月dd日"));
                multe.Add("joinmember", dr["joinmember"]);
                multe.Add("photo", picpath + "/" + dr["pic"]);
                multelist.Add(multe);
            }
            dt.Dispose();
            context.Put("multelist", multelist);
        }
        protected void ShowProList(ref VelocityContext context, int sid)
        {
            string picpath = Public.GetXMLShopValue("shopPicPath");
            int recount = 10;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", sid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopgoods_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> prolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable pro = new Hashtable();
                pro.Add("id", dr["id"]);
                pro.Add("goodsname", Input.GetSubString(dr["goodsname"].ToString(), 30));
                pro.Add("goodsurl", Public.URLWrite(dr["id"], "goods"));
                pro.Add("photo", picpath + "/" + dr["pic"]);
                pro.Add("mprice", dr["mprice"]);
                prolist.Add(pro);
            }
            dt.Dispose();
            context.Put("prolist", prolist);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string commentcontent = GetString("commentcontent");
            int sid = GetInt("sid", 0);
            GoodsCommentInfo mdl = new GoodsCommentInfo();
            mdl.PID = sid;
            mdl.Commid = 0;
            mdl.Content = commentcontent;
            int GoodsCommentCheck = Convert.ToInt32(Public.GetXMLShopValue("GoodsCommentCheck"));
            mdl.Islock = GoodsCommentCheck == 0 ? false : true;
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            mdl.UserID = this.GetUserID();
            mdl.CType = 1;
            int n = JuSNS.Home.App.Shop.Instance.InsertShopComment(mdl);
            if (n > 0)
            {
                if (GoodsCommentCheck == 1)
                {
                    context.Put("rights", "发布评论成功。需要审核才能显示！");
                }
                else
                {
                    //增加通知和动态
                    if (GetInt("userid", 0) != this.GetUserID() && GetInt("userid", 0) > 0)
                    {
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "评论了你的店铺", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyGoods, sid));
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.GoodsComment, string.Empty, DateTime.Now, sid, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(32), 0, 0, "评论了你的店铺");
                    }
                    context.Put("rights", "发布评论成功");
                }
            }
            else
            {
                context.Put("errors", "发布评论成失败");
            }
            ShowInfo(ref context);
        }
    }
}
