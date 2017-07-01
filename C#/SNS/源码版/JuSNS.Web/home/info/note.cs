using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.info
{
    /// <summary>
    /// 通知信息
    /// </summary>
    public class note : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <param name="context"></param>
        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "通知信息");
            ShowList(ref context);
            //更新所有通知信息为已读
            JuSNS.Home.User.User.Instance.UpdateNote(this.UserID);
        }
        /// <summary>
        /// 显示通知列表
        /// </summary>
        /// <param name="context"></param>
        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_note_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("content", dr["content"]);
                if (Convert.ToInt32(dr["userid"]) == this.UserID)
                {
                    info.Add("truename", "系统通知");
                    info.Add("spaceurl", "javascript:;");
                }
                else
                {
                    info.Add("truename", dr["truename"]);
                    info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                }
                info.Add("noteurl", this.GetURL(ref context,dr["CorrID"], dr["MsgType"]));
                bool isread = Convert.ToBoolean(dr["isread"]);
                if (!isread)
                {
                    info.Add("isread", 1);
                }
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["UserID"]) == uid) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'note')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        /// <summary>
        /// 得到具体链接地址
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cid"></param>
        /// <param name="msgtype"></param>
        /// <returns></returns>
        protected string GetURL(ref VelocityContext context,object cid, object msgtype)
        {
            context.Put("isurl", 1);
            switch (Convert.ToByte(msgtype))
            {
                case (byte)EnumNoticeType.CreatGift:
                    return root + "/app/gift";
                case (byte)EnumNoticeType.CreatPoke:
                    return root + "/app/poke/my"+ExName;
                case (byte)EnumNoticeType.InviteJoinSite:
                    return Public.URLWrite(cid, "user");
                case (byte)EnumNoticeType.JoinActive:
                    return Public.URLWrite(cid, "ative");
                case (byte)EnumNoticeType.JoinGroup:
                    return Public.URLWrite(cid, "group");
                case (byte)EnumNoticeType.JoinMulte:
                    return Public.URLWrite(cid, "multe");
                case (byte)EnumNoticeType.JoinVote:
                    return Public.URLWrite(cid, "vote");
                case (byte)EnumNoticeType.PulishMulte:
                    return Public.URLWrite(cid, "multe");
                case (byte)EnumNoticeType.ReplyActive:
                    return Public.URLWrite(cid, "ative");
                case (byte)EnumNoticeType.ReplyAsk:
                    return Public.URLWrite(cid, "ask");
                case (byte)EnumNoticeType.ReplyBlog:
                    return Public.URLWrite(cid, "blog");
                case (byte)EnumNoticeType.ReplyGoods:
                    return Public.URLWrite(cid, "goods");
                case (byte)EnumNoticeType.ReplyMulte:
                    return Public.URLWrite(cid, "multe");
                case (byte)EnumNoticeType.ReplyNews:
                    return Public.URLWrite(cid, "news");
                case (byte)EnumNoticeType.ReplyPhoto:
                    return Public.URLWrite(cid, "photo");
                case (byte)EnumNoticeType.ReplyTopic:
                    return Public.URLWrite(cid, "topic");
                case (byte)EnumNoticeType.ReplyTwitter:
                    return Public.URLWrite(cid, "twitter");
                case (byte)EnumNoticeType.SendBook:
                    return root + "/app/gbook";
                case (byte)EnumNoticeType.SendBox:
                    return root + "/friend/mail";
                case (byte)EnumNoticeType.SetAskBest:
                    return Public.URLWrite(cid, "ask");
                case (byte)EnumNoticeType.VipTimeout:
                    return root + "/home/vip" + ExName;
                case (byte)EnumNoticeType.Friend:
                    context.Put("isurl", 0);
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}
