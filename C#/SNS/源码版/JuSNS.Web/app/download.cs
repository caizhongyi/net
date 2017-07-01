using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app
{
    public class download : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            string doaction = GetString("do");
            int uid = this.GetUserID();
            switch (doaction)
            {
                case "group":
                    string errorstr = string.Empty;
                    int gid = GetInt("gid", 0);
                    int fid = GetInt("id", 0);
                    if (gid == 0)
                    {
                        errorstr += "错误的参数";
                        //context.Put("errors", "错误的参数");
                        PageError(errorstr, root + "/");
                    }
                    if (uid == 0)
                    {
                        errorstr += "需要登录后才能查看<br />";
                        //context.Put("errors", "需要登录后才能查看");
                        PageError(errorstr, root + "/login"+ExName+"?urls=" + System.Web.HttpContext.Current.Request.Url);
                    }
                    bool isMember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
                    if (!isMember)
                    {
                        errorstr += "您不是群会员，不能下载！<br />";
                        PageError(errorstr, root + "/app/group/default" + ExName + "?urls=" + System.Web.HttpContext.Current.Request.Url);
                    }
                    FilesInfo mdl = JuSNS.Home.App.Group.Instance.GetFileInfo(fid);
                    if (mdl.GroupID != gid)
                    {
                        errorstr += "错误的参数！<br />";
                    }
                    if (string.IsNullOrEmpty(errorstr))
                    {
                        context.Put("redirecturl", root + Public.GetXMLGroupValue("GroupFilesPath") + "/" + mdl.FileName);
                    }
                    else
                    {
                        context.Put("errors", errorstr);
                    }
                    break;
            }
        }
    }
}
