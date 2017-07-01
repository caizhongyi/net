using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.ative
{
    public class info : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int isOpen = Convert.ToInt16(Public.GetXMLAtiveValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ative");
            }
            else
            {
                int ismember = Convert.ToInt16(Public.GetXMLAtiveValue("ismember"));
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
            int aid = GetInt("aid", 0);
            context.Put("aid", aid);
            //更新点击率
            JuSNS.Home.App.Ative.Instance.UpdateClick(aid);
            #region 基本参数
            AtiveInfo mdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            context.Put("cpagetitle", mdl.AtiveName);
            context.Put("ativeurl", Public.GetXMLAtiveValue("picpath") + "/" + mdl.Photo);
            context.Put("ativename", Input.FilterHTML(mdl.AtiveName));
            context.Put("truename", mdl.TrueName);
            context.Put("classid", mdl.ClassID);
            context.Put("classname", mdl.ClassName);
            context.Put("address", mdl.AddRess);
            context.Put("content", mdl.Content);
            context.Put("starttime", mdl.StartTime.ToString("yyyy年MM月dd日"));
            context.Put("endtime", mdl.EndTime.ToString("yyyy年MM月dd日"));
            context.Put("baotime", mdl.BaoMingTime.ToString("yyyy年MM月dd日"));
            context.Put("persionnumber", mdl.PersionNumber);
            if (mdl.IsChecks == 1)
            {
                context.Put("check", "需要");
            }
            else
            {
                context.Put("check", "不需要");
            }
            context.Put("userurl", Public.URLWrite(mdl.UserID, "user"));
            context.Put("click", mdl.Clicks);
            context.Put("baomembers", JuSNS.Home.App.Ative.Instance.GetMembers(aid, 2));
            context.Put("attmembers", JuSNS.Home.App.Ative.Instance.GetMembers(aid, 1));
            int state = JuSNS.Home.App.Ative.Instance.GetAtiveATT(aid, uid);
            //-1没有记录，0参与了未审核，1已经关注过了，2已经参与了
            string button = string.Empty;
            DateTime etime = mdl.BaoMingTime;
            DateTime ntime = DateTime.Now;
            int Time = (ntime - etime).Days;
            if (mdl.UserID == uid)
            {
                int n = JuSNS.Home.App.Ative.Instance.GetCheckMembers(aid);
                if (n > 0 && Time <= 0)
                {
                    button = "<a href=\"javascript:;\" onclick=\"CheckMember(" + aid + "," + uid + ")\">有<span class=\"reshow strong\">" + n + "</span>个会员需要审核</a> ";
                }
            }
            else
            {
                if (Time > 0)
                {
                    button = "<span class=\"reshow\">报名日期已过</span>";
                }
                else
                {
                    switch (state)
                    {
                        case -1:
                            button = "<input id=\"Button2\" type=\"button\" value=\"我要报名\" onclick=\"baoming(" + aid + "," + uid + ",2)\" class=\"btn_blue4\" /> ";
                            button += "<input id=\"Button3\" class=\"btn_blue4\" onclick=\"baoming(" + aid + "," + uid + ",1)\"  type=\"button\" value=\"我要关注\" /> ";
                            break;
                        case 0:
                            button = "<span class=\"reshow\">参与了活动，管理员还未审核</span>";
                            break;
                        case 1:
                            button = "<input id=\"Button2\" type=\"button\" value=\"我要报名\" onclick=\"baoming(" + aid + "," + uid + ",2)\" class=\"btn_blue4\" /> ";
                            button += " &nbsp; 已关注过了";
                            break;
                        case 2:
                            if (Time <= 0)
                            {
                                button = "<a href=\"javascript:;\" onclick=\"outative(" + aid + "," + uid + ")\">退出活动</a>";
                            }
                            break;
                    }
                }
            }
            context.Put("button", button);
            #endregion 
            ShowList(ref context, aid);
            ShowListJoin(ref context, aid);
            ShowListPhto(ref context, aid);
        }


        protected void ShowList(ref VelocityContext context, int aid)
        {
            List<AtiveMemberInfo> BInfolist = JuSNS.Home.App.Ative.Instance.GetCheckMemberList(Convert.ToInt32(Public.GetXMLAtiveValue("attmember")), aid, 1);
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (AtiveMemberInfo info in BInfolist)
            {
                Hashtable cks = new Hashtable();
                cks.Add("id", info.Id);
                cks.Add("aid", info.Aid);
                cks.Add("userid", info.UserID);
                cks.Add("truename", info.TrueName);
                cks.Add("spaceurl", this.GetSpaceURL(info.UserID));
                cks.Add("headpic", this.GetHeadImage(info.UserID));
                cks.Add("time", Public.getTimeEXTSpan(info.PostTime));
                infolist.Add(cks);
            }
            context.Put("infolist", infolist);
        }

        protected void ShowListJoin(ref VelocityContext context, int aid)
        {
            List<AtiveMemberInfo> BInfolist = JuSNS.Home.App.Ative.Instance.GetCheckMemberList(Convert.ToInt32(Public.GetXMLAtiveValue("joinmember")), aid, 2);
            List<Hashtable> joininfolist = new List<Hashtable>();
            foreach (AtiveMemberInfo joininfo in BInfolist)
            {
                Hashtable cks = new Hashtable();
                cks.Add("id", joininfo.Id);
                cks.Add("aid", joininfo.Aid);
                cks.Add("userid", joininfo.UserID);
                cks.Add("truename", joininfo.TrueName);
                cks.Add("spaceurl", this.GetSpaceURL(joininfo.UserID));
                cks.Add("headpic", this.GetHeadImage(joininfo.UserID));
                cks.Add("time", Public.getTimeEXTSpan(joininfo.PostTime));
                joininfolist.Add(cks);
            }
            context.Put("joininfolist", joininfolist);
        }
        protected void ShowListPhto(ref VelocityContext context, int aid)
        {
            List<PhotoInfo> BInfolist = JuSNS.Home.App.Ative.Instance.GetAtiveAlbumList(Convert.ToInt32(Public.GetXMLAtiveValue("photonumber")), aid);
            List<Hashtable> photoinfolist = new List<Hashtable>();
            foreach (PhotoInfo photoinfo in BInfolist)
            {
                Hashtable cks = new Hashtable();
                cks.Add("id", photoinfo.Id);
                cks.Add("aid", photoinfo.AtiveID);
                cks.Add("userid", photoinfo.UserID);
                cks.Add("truename", photoinfo.TrueName);
                cks.Add("photourl", this.GetSmallPic(photoinfo.FilePath, 1));
                cks.Add("time", Public.getTimeEXTSpan(photoinfo.PostTime));
                photoinfolist.Add(cks);
            }
            context.Put("photoinfolist", photoinfolist);
        }
    }
}
