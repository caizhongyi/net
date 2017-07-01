using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class @new : UserPage
    {
        public string  groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int sid = 0;
            int pid = 0;
            int psid = 0;
            int IsLight = Convert.ToInt32(Public.GetXMLGroupValue("IsLight"));
            if (IsLight == 1) context.Put("islight", IsLight);
            context.Put("groupname", groupname);
             int gid = GetInt("gid", 0);
            if (gid > 0)
            {
                GroupInfo info = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
                pid = info.Privacy;
                psid = info.Publics;
                context.Put("gname", info.GroupName);
                context.Put("bulletin", info.Bulletin);
                context.Put("portrait", info.Portrait);
                context.Put("areaid", JuSNS.Home.Other.Area.Instance.GetAreaInfo(info.CityID).ParentID);
                context.Put("cpagetitle", "修改" + groupname);
                context.Put("sitem", info.CityID);
            }
            else
            {
                context.Put("sitem", 0);
                context.Put("areaid", 0);
                context.Put("cpagetitle", "创建" + groupname);
            }
            context.Put("classlist", ShowClassList(0, string.Empty, sid));
            context.Put("privacy", Public.GetPrivacy(pid));
            context.Put("publics", Public.GetPublics(psid));
        }

        protected string ShowClassList(int parentid, string tmpstr,int sid)
        {
            string listSTR = string.Empty;
            List<GroupClassInfo> infolist = JuSNS.Home.App.Group.Instance.GetClassList(parentid);
            foreach (GroupClassInfo info in infolist)
            {
                if (sid == info.ID)
                {
                    listSTR += "<option value=\"" + info.ID + "\" selected>" + tmpstr + info.ClassName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.ID + "\">" + tmpstr + info.ClassName + "</option>";
                }
                listSTR += ShowClassList(info.ID, " -- ", sid);
            }
            return listSTR;
        }


        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            string Bulletin = GetString("bulletin");
            string groupname = GetString("groupname");
            if (string.IsNullOrEmpty(groupname))
            {
                context.Put("errors", "填写" + groupname + "名称");
            }
            else
            {
                int cityid = 0;
                int getcityid = GetInt("cityid", 0);
                int getprovince = GetInt("provinceid", 0);
                if (getcityid > 0) cityid = getcityid; else cityid = getprovince;
                GroupInfo mdl = new GroupInfo();
                mdl.Bulletin = Bulletin;
                mdl.CityID = cityid;
                mdl.ClassID = GetInt("classid", 0);
                mdl.Click = 0;
                mdl.GroupName = groupname;
                mdl.Islight = false;
                int CreatGroupCheck = Convert.ToInt32(Public.GetXMLGroupValue("CreatGroupCheck"));
                if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
                {
                    CreatGroupCheck = 0;
                }
                mdl.IsLock = CreatGroupCheck == 1 ? true : false;
                mdl.IsRec = 0;
                mdl.Members = 1;
                string Pic = string.Empty;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    string Path = Public.GetXMLGroupValue("GroupPicPath");
                    HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                    Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Path);
                }
                if (string.IsNullOrEmpty(Pic))
                {
                    Pic = GetString("hidportrait");
                }
                mdl.Portrait = Pic;
                mdl.PostIP = Public.GetClientIP();
                mdl.PostTime = DateTime.Now;
                mdl.Privacy = GetInt("privacy", 0);
                mdl.Publics = GetInt("publics", 0);
                mdl.Id = GetInt("gid", 0);
                mdl.SkinDir = GetString("skinDir");
                mdl.UserID = this.UserID;
                int gID = 0;
                int n = JuSNS.Home.App.Group.Instance.InsertandUpdate(mdl, out gID);
                if (n > 0)
                {
                    if (CreatGroupCheck == 0)
                    {
                        if (GetInt("privacy", 0) != 2)
                        {
                            //插入动态
                            JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatGroup, string.Empty, DateTime.Now, gID, string.Empty));
                        }
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(9), 0, 0, "创建了社群");
                        context.Put("redirecturl", "group" + ExName + "?gid=" + gID);
                    }
                    else
                    {
                        context.Put("rights", "创建" + groupname + "成功，但是需要审核。");
                    }
                }
                else
                {
                    context.Put("errors", "创建" + groupname + "失败");
                }
            }
            ShowInfo(ref context);
        }
    }
}