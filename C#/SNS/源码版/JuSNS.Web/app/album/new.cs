using System;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.album
{
    /// <summary>
    /// 上传图片
    /// </summary>
    public class @new: UserPage
    {
        public int uid = 0;
        public string contentname = Public.GetXMLBaseValue("contentName");
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            uid = GetUserID();
            ShowInfo(ref context);
        }
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "上传图片");
            int gid = GetInt("gid", 0);
            string groupname = GetString("groupname");
            ShowAlbumlist(ref context, gid);
            context.Put("privacy", Public.GetPrivacy(0));
            if (gid > 0)
            {
                if (string.IsNullOrEmpty(groupname))
                {
                    context.Put("errors", "参数传递错误");
                }
            }
            context.Put("groupname", groupname);
            context.Put("gid", gid);
        }
        /// <summary>
        /// 显示相册列表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="gid"></param>
        protected void ShowAlbumlist(ref VelocityContext context,int gid)
        {
            List<AlbumInfo> InfoList = JuSNS.Home.App.Album.Instance.AlbumList(this.UserID,gid);
            string listSTR = string.Empty;
            foreach (AlbumInfo info in InfoList)
            {
                listSTR += "<option value=\"" + info.AlbumID + "\">" + info.Title + "</option>";
            }
            context.Put("albumlist", listSTR);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref VelocityContext context)
        {
            string albumname = GetString("albumname");
            string albumdesc = GetString("albumdesc");
            int gid = GetInt("gid", 0);
            string groupname = GetString("groupname");
            int Privacy = GetInt("Privacy", 0);
            if (string.IsNullOrEmpty(albumname))
            {
                context.Put("errors", "创建相册失败");
            }
            else
            {
                AlbumInfo mdl = new AlbumInfo();
                mdl.AlbumID = 0;
                mdl.CreateTime = DateTime.Now;
                mdl.Description = albumdesc;
                mdl.GroupID = gid;
                mdl.ImagesCount = 0;
                mdl.LastUploadTime = DateTime.Now;
                mdl.Privacy = Privacy;
                mdl.Title = albumname;
                mdl.UserID = this.UserID;
                int albumid = JuSNS.Home.App.Album.Instance.Add(mdl);
                if (Privacy != 2)
                {
                    //插入动态
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.CreatAlbum, string.Empty, DateTime.Now, albumid, string.Empty));
                }
                //更新积分
                JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(7), 0, 0, "创建了相册");
                if (gid > 0)
                {
                    context.Put("redirecturl", "PhotoAdd" + ExName + "?aid=" + albumid + "&gid=" + gid + "&groupname=" + groupname + "");
                }
                else
                {
                    context.Put("redirecturl", "PhotoAdd" + ExName + "?aid=" + albumid);
                }
            }
            ShowInfo(ref context);
        }
    }
}
