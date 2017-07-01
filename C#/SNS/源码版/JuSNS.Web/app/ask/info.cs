using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ask
{
    public class info:BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAskValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ask");
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int aid = GetInt("aid", 0);
            JuSNS.Home.App.Ask.Instance.UpdateAskState(aid, 0, this.UserID);
            AskInfo mdl = JuSNS.Home.App.Ask.Instance.GetAskInfo(aid);
            if (mdl != null)
            {
                context.Put("userid", mdl.UserID);
                if (mdl.UserID > 0)
                {
                    context.Put("spaceurl", this.GetSpaceURL(mdl.UserID));
                    context.Put("headpic", this.GetHeadImage(mdl.UserID));
                    context.Put("userid", mdl.UserID);
                    context.Put("truename", JuSNS.Home.User.User.Instance.GetUserInfo(mdl.UserID).TrueName);
                }


                if (!string.IsNullOrEmpty(mdl.Pic))
                {
                    string pSTR = string.Empty;
                    string TmpSize = string.Empty; 
                    string ExPic = root + Public.GetXMLAskValue("askpicpath") +"/"+ mdl.Pic;
                    if (Public.GetSize(ExPic, 1) > 100) { TmpSize = " style=\"height:100px;\""; }
                    if (Public.GetSize(ExPic, 0) > 300) { TmpSize = " style=\"width:300px;\""; }
                    if (!string.IsNullOrEmpty(TmpSize))
                    {
                        pSTR += "<a href=\"" + ExPic + "\" target=\"_blank\"><img src=\"" + ExPic + "\" " + TmpSize + " /></a>";
                    }
                    else
                    {
                        pSTR += "<img src=\"" + ExPic + "\" />";
                    }
                    context.Put("pic", pSTR);
                }
                context.Put("title", Input.FilterHTML(mdl.Title));
                context.Put("click", mdl.Click);
                context.Put("content", mdl.Content);
                context.Put("isclose", mdl.IsClose);
                context.Put("aid", aid);
                int jf = mdl.JiFen;
                if (mdl.IsJinji==1)
                {
                    jf = jf + Convert.ToInt32(Public.GetXMLAskValue("isJinji"));
                }
                context.Put("jifen", jf);
                context.Put("time", Public.getTimeLEXYearSpan(mdl.PostTime));
                context.Put("cpagetitle", mdl.Title + " - 问答");
                ShowAskList(ref context);
                ShowCommentList(ref context, aid);
            }
            else
            {
                context.Put("errors", "错误的参数");
            }
        }

        protected void ShowAskList(ref VelocityContext context)
        {
            int Num = 0;
            int InfoNumber = Convert.ToInt32(Public.GetXMLAskValue("InfoNumber"));
            List<AskInfo> Infolist = JuSNS.Home.App.Ask.Instance.GetAskList(InfoNumber, string.Empty, 0);
            List<Hashtable> hotlist = new List<Hashtable>();
            foreach (AskInfo info in Infolist)
            {
                Num++;
                Hashtable hot = new Hashtable();
                hot.Add("title", Input.GetSubString(info.Title, 36));
                hot.Add("titles", info.Title);
                hot.Add("askurl", Public.URLWrite(info.Id, "ask"));
                hotlist.Add(hot);
            }
            context.Put("hotlist", hotlist);
            context.Put("hotcount", Num);
        }

        protected void ShowCommentList(ref VelocityContext context, int aid)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int uid = this.GetUserID();
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@Aid", aid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_askdanan_aspx", PageIndex, recount, out ReCount, out PgCount, st);
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
                comm.Add("content", ReplaySTR + Input.ReplaceSmaile(dr["content"].ToString()));
                int isbest = Convert.ToInt32(dr["isbest"]);
                if (isbest==1)
                {
                    comm.Add("isbest", "<em>最佳答案</em>");
                    comm.Add("isbestclass", "isbest");
                }
                else
                {
                    comm.Add("isbest", string.Empty);
                }
                bool isOp = false;
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == GetUserID() || isadmin >0)
                {
                    isOp = true;
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'ask')\" class=\"showok1\"></a>";
                comm.Add("showop", opSTR);
                commlist.Add(comm);
            }
            dt.Dispose();
            context.Put("commlist", commlist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            if (GetInt("txtislock", 0) != 0)
            {
                context.Put("errors", "关闭的的问题不允许回复");
            }
            else
            {
                if (this.GetUserID() == 0)
                {
                    context.Put("errors", "您还未登录");
                }
                else
                {
                    string content = GetString("txttcontent");
                    AskInfo mdl = new AskInfo();
                    mdl.ClassID = 0;
                    mdl.Click = 0;
                    mdl.Content = content;
                    mdl.Id = 0;
                    mdl.IsBest = 0;
                    mdl.IsClose = 0;
                    mdl.IsJinji = 0;
                    byte islock = Convert.ToByte(Public.GetXMLAskValue("ischck"));
                    mdl.IsLock = islock;
                    mdl.JiFen = 0;
                    mdl.ParentID = GetInt("aid", 0);
                    mdl.Pic = string.Empty;
                    mdl.PostTime = DateTime.Now;
                    mdl.Tag = string.Empty;
                    mdl.Title = string.Empty;
                    mdl.UserID = this.GetUserID();
                    int aid1 = 0;
                    int n = JuSNS.Home.App.Ask.Instance.InsertAsk(mdl, out aid1);
                    if (n > 0)
                    {
                        //这里增加积分
                        if (islock == 1)
                        {
                            //PageRight("回答问题成功，但是需要审核才能显示", System.Web.HttpContext.Current.Request.Url.ToString(), true);
                            context.Put("rights", "回答成功，但是你的回答需要审核！");
                        }
                        else
                        {
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.AskComment, string.Empty, DateTime.Now, GetInt("aid", 0), string.Empty));
                            //PageRight("回答问题成功", System.Web.HttpContext.Current.Request.Url.ToString(), true);
                            context.Put("rights", "回答成功！");
                        }
                    }
                    else
                    {
                        context.Put("errors", "回复失败");
                    }
                    ShowInfo(ref context);
                }
            }
        }
    }
}
