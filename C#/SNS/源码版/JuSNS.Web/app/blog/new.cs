using System;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.blog
{
    public class @new : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int bid = GetQueryInt("bid", 0);
            context.Put("bid", bid);
            int mid = 0;
            int sid = 0;
            int pid = 0;
            if (bid > 0)
            {
                BlogInfo mdl = JuSNS.Home.App.Blog.Instance.GetBlogInfo(this.UserID, bid);
                context.Put("cpagetitle", "修改日志 - " + mdl.Title + "");
                context.Put("title", mdl.Title);
                context.Put("content", mdl.Content);
                if (mdl.IsDraft == 1) { context.Put("draft", "checked"); }
                else { context.Put("draft", string.Empty); }
                mid = mdl.MyClassID;
                sid = mdl.SysClassID;
                pid = mdl.Privacy;
            }
            else
            {
                context.Put("cpagetitle", "写新日志");
            }
            string sysclasslist = this.GetClassList(0, sid);
            string myclasslist = this.GetClassList(this.UserID, mid);
            context.Put("myclasslist", myclasslist);
            context.Put("sysclasslist", sysclasslist);
            context.Put("privacy", Public.GetPrivacy(pid));
        }

        protected string GetClassList(int UserID,int sid)
        {
            string listSTR = string.Empty;
            List<BlogClassInfo> Infolist = JuSNS.Home.App.Blog.Instance.GetBlogClass(UserID, 0);
            foreach (BlogClassInfo info in Infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=" + info.Id + " selected>" + info.CName + "</option>";
                }
                else
                {
                    listSTR += "<option value=" + info.Id + ">" + info.CName + "</option>";
                }
            }
            return listSTR;
        }


        public override void Page_PostBack(ref VelocityContext context)
        {
            ShowInfo(ref context);
            string title = GetString("txttitle");
            string content = GetString("txtcontent");
            int myclassid = GetInt("myclassid", 0);
            int bid = GetInt("bid", 0);
            int systemclassid = GetInt("systemclassid", 0);
            int privacy = GetInt("privacy", 0);
            int draft = GetInt("isDraft", 0);
            string error = string.Empty;
            if (string.IsNullOrEmpty(title))
            {
                error += "填写标题&nbsp;";
            }
            if (string.IsNullOrEmpty(content))
            {
                error += "填写内容&nbsp;";
            }
            if (systemclassid==0)
            {
                error += "选择系统分类&nbsp;";
            }
            if (!string.IsNullOrEmpty(error))
            {
                context.Put("errors", error);
            }
            else
            {
                int BlogisLock = Convert.ToInt32(Public.GetXMLBaseValue("Blog"));
                BlogInfo mdl = new BlogInfo();
                mdl.Attnumber = 0;
                mdl.Click = 0;
                mdl.Comments = 0;
                mdl.Content = content;
                mdl.ID = bid;
                mdl.IsDraft = Convert.ToByte(draft);
                if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
                {
                    BlogisLock = 0;
                }
                if (BlogisLock == 0)
                {
                    mdl.IsLock = false;
                }
                else
                {
                    mdl.IsLock = true;
                }
                mdl.IsRec = 0;
                mdl.LastModTime = DateTime.Now;
                mdl.MyClassID = myclassid;
                mdl.PicPath = string.Empty;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.Privacy = privacy;
                mdl.Reads = 0;
                mdl.ShareNumber = 0;
                mdl.SysClassID = systemclassid;
                mdl.Title = title;
                mdl.UserID = this.UserID;
                int n = JuSNS.Home.App.Blog.Instance.UpdateBlog(mdl);
                if (n > 0)
                {
                    if (BlogisLock == 0 && privacy != 2)
                    {
                        //插入动态
                        DynInfo dyninfo = new DynInfo();
                        dyninfo.Content = content;
                        dyninfo.CUserID = 0;
                        dyninfo.DynType = (int)EnumDynType.CreatBlog;
                        dyninfo.Infoarr = n;
                        dyninfo.PostTime = DateTime.Now;
                        dyninfo.UserID = this.UserID;
                        JuSNS.Home.User.User.Instance.InsertDyn(dyninfo);
                        //获得积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(3), 0, 0, "发表日志");
                        //PageRight("操作成功", Public.URLWrite(n, "blog"), true);
                        context.Put("redirecturl", Public.URLWrite(n, "blog"));
                    }
                    else
                    {
                        //PageRight("已经发布日志，但是需要审核才能显示", System.Web.HttpContext.Current.Request.Url.ToString(), true);
                        context.Put("rights", "已经发布日志，但是需要管理审核。");
                    }
                }
                else
                {
                    context.Put("errors", "操作失败:BlogOpRecord");
                }
            }
        }
    }
}
