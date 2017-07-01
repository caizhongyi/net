using System;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.news
{
    public class @new : UserPage
    {
        public string contentname = Public.GetXMLValue("news", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("contentname", contentname);
            int nid = GetInt("nid", 0);
            context.Put("nid", nid);
            int sid = 0;
            if (nid > 0)
            {
                NewsInfo mdl = JuSNS.Home.App.News.Instance.GetNewsInfo(nid);
                context.Put("title", mdl.Title);
                context.Put("color", mdl.Color);
                context.Put("bold", mdl.Bold);
                context.Put("italic", mdl.Italic);
                context.Put("content", mdl.Content);
                sid = mdl.ClassID;
                context.Put("filePic", mdl.Pic);
                context.Put("fileFile", mdl.Files);
                context.Put("point", mdl.Point);
                context.Put("gpoints", mdl.GPoint);
                if (mdl.IsSys == 1)
                {
                    context.Put("issys", "checked");
                }
                context.Put("keyword", mdl.Keywords);
                context.Put("source", mdl.Source);
                context.Put("cpagetitle", "修改["+mdl.Title+"]  -" + contentname + "");
                if (Convert.ToByte(Public.GetXMLBaseValue("contentcheck")) == 1)
                {
                    context.Put("check", "修改后需要管理员重新审核才能发布！");
                }
            }
            else
            {
                context.Put("cpagetitle", "添加" + contentname + "");
            }
            string classlist = this.GetClassList(0, sid, " ");
            context.Put("classlist", classlist);
            UserInfo usrinfo = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            if (usrinfo.IsAdmin > 0)
            {
                context.Put("isadmin", true);
            }
        }

        protected string GetClassList(int parentid,int sid,string TmpSTR)
        {
            string listSTR = string.Empty;
            List<NewsChannelInfo> Infolist = JuSNS.Home.App.News.Instance.GetNewsChannel(parentid, 0);
            foreach (NewsChannelInfo info in Infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=" + info.Id + " selected>" + TmpSTR + info.ChannelName + "</option>";
                }
                else
                {
                    listSTR += "<option value=" + info.Id + ">" + TmpSTR + info.ChannelName + "</option>";
                }
                listSTR += GetClassList(info.Id, sid, " -- ");
            }
            return listSTR;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int nid = GetInt("nid", 0);
            int ClassID = GetInt("classid", 0);
            string content = GetString("txtcontent");
            string keywords = GetString("txtkeyword");
            string source = GetString("txtsource");
            string title = GetString("txttitle");
            string color = GetString("color");
            byte bold = Convert.ToByte(GetInt("bold", 0));
            byte italic = Convert.ToByte(GetInt("italic", 0));
            string error = string.Empty;
            if (string.IsNullOrEmpty(title))
            {
                error += "填写标题&nbsp;";
            }
            if (string.IsNullOrEmpty(content))
            {
                error += "填写内容&nbsp;";
            }
            if (ClassID == 0)
            {
                error += "选择系统分类&nbsp;";
            }
            if (!string.IsNullOrEmpty(error))
            {
                context.Put("errors", error);
            }
            else
            {
                NewsInfo mdl = new NewsInfo();
                mdl.AttNumber = 0;
                mdl.ClassID = ClassID;
                mdl.Click = 0;
                mdl.Id = nid;
                mdl.Comments = 0;
                mdl.Content = content;
                string Pic = string.Empty;
                string Files = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    string Path = Public.GetXMLBaseValue("ContentPath");
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Path);
                    HttpPostedFile hpf1 = HttpContext.Current.Request.Files[1];
                    string FileName = Input.MD5(Guid.NewGuid().ToString(), false);
                    Files = Public.GetFile(hpf1, Public.GetXMLValue("filetype"), Path, FileName);
                }
                if (nid > 0)
                {
                    if (string.IsNullOrEmpty(Pic))
                    {
                        Pic = GetString("HiddenfilePic");
                    }
                    if (string.IsNullOrEmpty(Files))
                    {
                        Files = GetString("HiddenfileFile");
                    }
                }
                int Point = GetInt("txtPoint", 0);
                int GPoint = GetInt("txtGPoint", 0);
                mdl.Files = Files;
                mdl.GPoint = GPoint;
                byte contentcheck = Convert.ToByte(Public.GetXMLBaseValue("contentcheck"));
                if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin >0)
                {
                    contentcheck = 0;
                }
                mdl.IsLock = contentcheck;
                mdl.IsRec = 0;
                mdl.IsSys = Convert.ToByte(GetInt("issys", 0));
                mdl.Keywords = keywords;
                mdl.Pic = Pic;
                mdl.Point = Point;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.ShareNumber = 0;
                mdl.Source = source;
                mdl.SpecialList = string.Empty;
                mdl.Title = title;
                mdl.Color = color;
                mdl.Bold = bold;
                mdl.Italic = italic;
                mdl.UserID = this.UserID;
                int n = JuSNS.Home.App.News.Instance.InsertUpdateNews(mdl);
                if (nid > 0)
                {
                    if (contentcheck == 1)
                    {
                        context.Put("rights", "需要审核后才能显示在前台。");
                    }
                    else
                    {
                        context.Put("redirecturl", Public.URLWrite(n, "news"));
                    }
                }
                else
                {
                    if (contentcheck == 1)
                    {
                        context.Put("rights", "需要审核后才能显示在前台。");
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.CreatNews, string.Empty, DateTime.Now, n, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(21), 0, 0, "发布新闻");
                        context.Put("redirecturl", Public.URLWrite(n, "news"));
                    }
                }
            }
            ShowInfo(ref context);
        }
    }
}