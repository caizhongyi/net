using System;
using System.Text;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.shop
{
    public class goods : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            if (uid > 0)
            {
                ShopInfo mdl = JuSNS.Home.App.Shop.Instance.GetShopForUserID(uid);
                if (mdl != null)
                {
                    context.Put("isshop", true);
                }
            }
            int isOpen = Convert.ToInt16(Public.GetXMLShopValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=shop");
            }
            else
            {
                int ismember = Convert.ToInt16(Public.GetXMLShopValue("ismember"));
                if (ismember != 1)
                {
                    if (uid == 0)
                    {
                        context.Put("redirecturl", root + "/library/page/error" + ExName + "?error=Err_TimeOut&urls=" + root + "/login" + ExName + "?urls=" + HttpContext.Current.Request.Url);
                    }
                }
                ShowInfo(ref context, uid);
            }
        }

        protected void ShowInfo(ref VelocityContext context, int uid)
        {
            base.Page_Load(ref context);
            string shoptitle = Public.GetXMLShopValue("shopname");
            int gid = GetInt("gid",0);
            GetGoodsInfo(ref context, gid, shoptitle, uid);
            context.Put("shop", shoptitle);
            context.Put("gid", gid);
            if (JuSNS.Home.App.Shop.Instance.GetGoodsSore(gid) > 0)
            {
                context.Put("sumsore", JuSNS.Home.App.Shop.Instance.GetGoodsSore(gid).ToString("0.0"));
            }
            else
            {
                context.Put("sumsore", "--");
            }
            ShowCommentList(ref context, gid);
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
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_goods_comment_aspx", PageIndex, recount, out ReCount, out PgCount, st);
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
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0)
                {
                    isOp = true;
                }
                if (Convert.ToInt32(dr["userid"]) != uid)
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"replaycomment(" + dr["id"] + "," + dr["pid"] + "," + uid + ",'" + dr["truename"] + "','goods');\">回复</a> ";
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deletetgoodscomment(" + dr["id"] + "," + uid + ")\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        protected void GetGoodsInfo(ref VelocityContext context, int gid, string shoptitle, int uid)
        {
            JuSNS.Home.App.Shop.Instance.UpdateGoodsState(gid, 0);
            string Path = Public.GetXMLShopValue("shopPicPath");
            ShopGoodsInfo mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
            UserInfo uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID);
            context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
            context.Put("truename", uinfo.TrueName);
            context.Put("userid", mdl.UserID);
            context.Put("cpagetitle", mdl.GoodsName);
            context.Put("goodsname", Input.FilterHTML(mdl.GoodsName));
            bool isold = mdl.IsOld;
            if (isold)
            {
                context.Put("isold", "二手");
            }
            else
            {
                context.Put("isold", "全新");
            }
            context.Put("classname", mdl.ClassName);
            context.Put("classid", mdl.ClassID);
            context.Put("goodsimges", Path + "/" + mdl.Pic);
            context.Put("areaid", JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.AreaID).Name);
            if (mdl.ShopID > 0)
            {
                context.Put("shopname", JuSNS.Home.App.Shop.Instance.GetShopForID(mdl.ShopID).ShopName);
                context.Put("shopid", mdl.ShopID);
                context.Put("shopurl", Public.URLWrite(mdl.ShopID, "shop"));
            }
            context.Put("goodsurl", Public.URLWrite(mdl.ShopID, "goods"));
            context.Put("content", mdl.Content);
            context.Put("price", mdl.Price.ToString("0.00"));
            if (mdl.MPrice > 0)
            {
                context.Put("mprice", mdl.MPrice.ToString("0.00"));
            }
            if (mdl.GPoint > 0)
            {
                context.Put("goodsgpoint", mdl.GPoint);
            }
            context.Put("buynumber", mdl.BuyNumber);
            context.Put("multebuy", mdl.MulteBuy);
            context.Put("goodsid", gid);
            context.Put("click", mdl.Click);
            context.Put("goodsuserid", mdl.UserID);
            context.Put("topnumber", mdl.TopNumber);
            context.Put("downnumber", mdl.DownNumber);
            string youxiao = string.Empty;
            DateTime stime = mdl.StartTime;
            DateTime etime = mdl.EndTime;
            DateTime ntime = DateTime.Now;
            if ((etime - stime).Days < 100)
            {
                if ((ntime - etime).Days > 0)
                {
                    youxiao = "已过期";
                }
                else
                {
                    youxiao = stime.ToString("yyyy年MM月dd日") + " 至 " + etime.ToString("yyyy年MM月dd日");
                }
            }
            else
            {
                youxiao = "000";
            }
            context.Put("youxiao", youxiao);
            context.Put("tel", mdl.Tel);
            context.Put("address", mdl.AddRess);
            string exSTR = string.Empty;
            byte ExpressStyle = mdl.ExpressStyle;
            switch (ExpressStyle)
            {
                case 0:
                    exSTR = "免费寄送";
                    break;
                case 1:
                    exSTR = "普通邮寄";
                    break;
                case 2:
                    exSTR = "快递公司";
                    break;
                case 3:
                    exSTR = "EMS";
                    break;
            }
            context.Put("expressstyle", exSTR);
            context.Put("money", mdl.ExpressContent);
            context.Put("minmembers", mdl.MulteMinNumber);
            context.Put("maxmembers", mdl.MulteMaxNumber);
            context.Put("number", mdl.Number);
            context.Put("OpenTrade", Public.GetXMLShopValue("OpenTrade"));
            ShowGoodsList(ref context, mdl.UserID);
            ShowGoodsListRec(ref context);
            ShowMulteList(ref context);
        }

        protected void ShowGoodsListRec(ref VelocityContext context)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLShopValue("IsRecGoods"));
            List<ShopGoodsInfo> Infolist = JuSNS.Home.App.Shop.Instance.GetGoodsList(recount,1);
            List<Hashtable> goodsreclist = new List<Hashtable>();
            foreach (ShopGoodsInfo info in Infolist)
            {
                Num++;
                Hashtable goodsrec = new Hashtable();
                goodsrec.Add("userid", info.UserID);
                goodsrec.Add("goodsname", info.GoodsName);
                goodsrec.Add("goodsurl", Common.Public.URLWrite(info.Id, "goods"));
                goodsreclist.Add(goodsrec);
            }
            context.Put("goodsreclist", goodsreclist);
            context.Put("goodsreccount", Num);
        }

        protected void ShowGoodsList(ref VelocityContext context,int userid)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLShopValue("IsRecGoods"));
            List<ShopGoodsInfo> Infolist = JuSNS.Home.App.Shop.Instance.GetUserGoodsList(recount, userid);
            List<Hashtable> goodslist = new List<Hashtable>();
            foreach (ShopGoodsInfo info in Infolist)
            {
                Num++;
                Hashtable goods = new Hashtable();
                goods.Add("userid", info.UserID);
                goods.Add("goodsname", info.GoodsName);
                goods.Add("goodsurl", Common.Public.URLWrite(info.Id, "goods"));
                goodslist.Add(goods);
            }
            context.Put("goodslist", goodslist);
            context.Put("goodscount", Num);
        }

        protected void ShowMulteList(ref VelocityContext context)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLShopValue("IsRecGoods"));
            List<ShopMulteBuyInfo> Infolist = JuSNS.Home.App.Shop.Instance.GetMulteList(recount, 1);
            List<Hashtable> multelist = new List<Hashtable>();
            foreach (ShopMulteBuyInfo info in Infolist)
            {
                Num++;
                Hashtable multe = new Hashtable();
                multe.Add("userid", info.UserID);
                multe.Add("title", info.Title);
                multe.Add("multeurl", Common.Public.URLWrite(info.Id, "multe"));
                multelist.Add(multe);
            }
            context.Put("multelist", multelist);
            context.Put("multecount", Num);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string commentcontent = GetString("commentcontent");
            int gid = GetInt("gid", 0);
            GoodsCommentInfo mdl = new GoodsCommentInfo();
            mdl.PID = gid;
            mdl.Commid = 0;
            mdl.Content = commentcontent;
            int GoodsCommentCheck = Convert.ToInt32(Public.GetXMLShopValue("GoodsCommentCheck"));
            mdl.Islock = GoodsCommentCheck == 0 ? false : true;
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            mdl.UserID = this.GetUserID();
            mdl.CType = 0;
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
                        JuSNS.Home.User.User.Instance.InsertNotice(new NoticeInfo(0, this.GetUserID(), GetInt("userid", 0), "评论了你的商品", false, DateTime.Now, Public.GetClientIP(), (byte)EnumNoticeType.ReplyGoods, gid));
                        //插入动态
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), GetInt("userid", 0), (int)EnumDynType.GoodsComment, string.Empty, DateTime.Now, gid, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(32), 0, 0, "评论商品");
                    }
                    context.Put("rights", "发布评论成功");
                }
            }
            else
            {
                context.Put("errors", "发布评论成失败");
            }
            ShowInfo(ref context, this.GetUserID());
        }
    }
}
