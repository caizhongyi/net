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
    public class comment : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string shoptitle = Public.GetXMLShopValue("shopname");
            context.Put("shop", shoptitle);
            int gid = GetInt("gid", 0);
            ShopGoodsInfo mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
            context.Put("cpagetitle", mdl.GoodsName + " _ 评分");
            context.Put("gid", gid);
            context.Put("sumsore", JuSNS.Home.App.Shop.Instance.GetGoodsSore(gid).ToString("0.0"));
            context.Put("iscomment", JuSNS.Home.App.Shop.Instance.IsShopUserComment(gid, this.GetUserID()));
            ShowList(ref context, gid);
            
        }

        protected void ShowList(ref VelocityContext context, int gid)
        {
            int uid = this.GetUserID();
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            DataTable dt = null;
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@GID", gid, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_shopcomments_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("userid", dr["userid"]);
                info.Add("truename", dr["truename"]);
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("sore", dr["Sore"]);
                info.Add("content", Input.ReplaceSmaile(dr["content"].ToString()));
                info.Add("time", Public.getTimeEXTSpan(Convert.ToDateTime(dr["posttime"])));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int gid = GetInt("gid", 0);
            int uid = this.GetUserID();
            int Sore = GetInt("radscore", 0);
            string content = GetString("commentcontent");
            ShopUserCommentInfo sucom = new ShopUserCommentInfo(0, gid, Convert.ToByte(Sore), uid, content, DateTime.Now, Public.GetClientIP(), 0);
            int n = JuSNS.Home.App.Shop.Instance.InsertUserComment(sucom);
            if (n > 0)
            {
                context.Put("rights", "评论成功");
            }
            else if (n == -1)
            {
                context.Put("errors", "您已经评论过了。");
            }
            else
            {
                context.Put("errors", "评论失败。");
            }
            ShowInfo(ref context);
        }
    }
}
