using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class filesadd : UserPage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.UserID;
            base.Page_Load(ref context);
            int gid = GetInt("gid", 0);
            bool isMember = JuSNS.Home.App.Group.Instance.IsJoinGroup(gid, uid);
            GroupInfo mdl = JuSNS.Home.App.Group.Instance.GetGroupInfo(gid);
            context.Put("groupname", groupname);
            context.Put("gid", gid);
            context.Put("cpagetitle", mdl.GroupName + groupname);
            context.Put("portrait", Public.GetGroupPortrait(mdl.Portrait));
            context.Put("GroupName", mdl.GroupName);
            context.Put("Bulletin", mdl.Bulletin);
            context.Put("topiccount", JuSNS.Home.App.Group.Instance.GetGroupTopicCount(gid, 1));
            context.Put("albumcount", JuSNS.Home.App.Group.Instance.GetGroupAlbumCount(gid));
            context.Put("filescount", JuSNS.Home.App.Group.Instance.GetGroupFilesCount(gid));
            context.Put("membercount", JuSNS.Home.App.Group.Instance.GetGroupMemberCount(gid));
            context.Put("ativecount", JuSNS.Home.App.Group.Instance.GetGroupAtiveCount(gid));
            int filesize = Convert.ToInt32(Public.GetXMLGroupValue("FileSpaceSize"));
            int unfilesize = Convert.ToInt32(JuSNS.Home.App.Group.Instance.GetFilesSize(gid) / 1024);
            if (unfilesize >= filesize)
            {
                context.Put("disabled", "disabled");
                context.Put("text", "空间已使用完");
            }
            else
            {
                context.Put("disabled", "");
                context.Put("text", "确定上传附件");
            }
            context.Put("filesize", filesize);
            context.Put("unfilesize", unfilesize);
            double mods = (double)unfilesize / filesize;
            context.Put("perentsize", Convert.ToInt32(mods * 100));

            if (!isMember)
            {
                context.Put("errors", "你不是群成员，不能上传。");
                context.Put("disabled", "disabled");
                context.Put("text", "您不是群会员");
            }

        }

        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            FilesInfo mdl = new FilesInfo();
            mdl.DownNumber = 0;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                string ext = hpf.FileName.Substring(hpf.FileName.LastIndexOf("."));
                if (Public.GetXMLGroupValue("GroupFilesExt").IndexOf(ext) == -1)
                {
                    context.Put("errors", "您上传的类型不被允许！");
                }
                else
                {
                    string filesName = Public.GetFile(hpf, Public.GetXMLGroupValue("GroupFilesExt"), Public.GetXMLGroupValue("GroupFilesPath"));
                    if (!string.IsNullOrEmpty(filesName))
                    {
                        mdl.FileName = filesName;
                        mdl.FileSize = Convert.ToInt32(hpf.ContentLength / 1024);
                        mdl.GroupID = GetInt("gid", 0);
                        mdl.PostIP = Public.GetClientIP();
                        mdl.PostTime = DateTime.Now;
                        mdl.Title = GetString("title");
                        mdl.UserID = this.UserID;
                        int n = JuSNS.Home.App.Group.Instance.InsertFiles(mdl);
                        if (n > 0)
                        {
                            context.Put("redirecturl", "files" + ExName + "?gid=" + GetInt("gid", 0));
                        }
                        else
                        {
                            context.Put("errors", "上传附件失败");
                        }
                    }
                    else
                    {
                        context.Put("errors", "最大允许上传" + Public.GetXMLValue("picsize") + "M");
                    }
                }
            }
            else
            {
                context.Put("errors", "请选择一个文件");
            }
            ShowInfo(ref context);
        }
    }
}
