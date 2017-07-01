using System;
using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.album
{
    public class photoedit : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "修改相册图片");
            int aid = GetInt("aid", 0);
            context.Put("aid", aid);
            AlbumInfo mdl = JuSNS.Home.App.Album.Instance.GetInfo(aid);
            if (mdl.UserID != this.UserID)
            {
                context.Put("errors", "错误的参数：UserParamError");
            }
            else
            {
                context.Put("albumname", mdl.Title);
                ShowAlbumlist(ref context, aid);
            }
        }

        protected void ShowAlbumlist(ref VelocityContext context, int AlbumID)
        {
            int Num = 0;
            List<PhotoInfo> plist = JuSNS.Home.App.Album.Instance.InfoList(AlbumID, 0, 0);
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (PhotoInfo info in plist)
            {
                Num++;
                Hashtable pinfo = new Hashtable();
                string pic = string.Empty;
                if (info.PhotoType == 1)
                {
                    pic = this.GetSmallPic(info.FilePath, 0);
                }
                else
                {
                    pic = Public.GetSmallHeadPic(info.FilePath, 2);
                }
                pinfo.Add("pic", pic);
                if (info.IsCover) pinfo.Add("coverChecked", "checked"); else pinfo.Add("coverChecked", string.Empty);
                pinfo.Add("desc", info.Description);
                pinfo.Add("albumid", info.AlbumID);
                pinfo.Add("id", info.Id);
                infolist.Add(pinfo);
            }
            context.Put("infolist", infolist);
            context.Put("Num", Num);
            if (Num == 0)
            {
                context.Put("errors", "没有照片的相册不能编辑。");
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int gid = GetInt("gid", 0);
            string groupname = GetString("groupname");
            string coverid = GetString("coverid");
            string desc = GetString("desc");
            string pid = GetString("pid");
            int albumid = GetInt("aid", 0);
            string[] descARR = desc.Split(',');
            string[] ids = pid.Split(',');
            PhotoInfo mdl = new PhotoInfo();
            int Number = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] == coverid)
                {
                    mdl.IsCover = true;
                }
                else
                {
                    mdl.IsCover = false;
                }
                mdl.AlbumID = albumid;
                mdl.Description = descARR[i];
                mdl.Id = Convert.ToInt32(ids[i]);
                mdl.UserID = this.UserID;
                JuSNS.Home.App.Photo.Instance.Edit(mdl);
                Number++;
            }
            JuSNS.Home.App.Photo.Instance.UpdatePhotoCount(albumid, Number, this.UserID);
            if (gid > 0)
            {
                context.Put("redirecturl", "../album/AlbumView" + ExName + "?aid=" + albumid + "&gid=" + gid + "&groupname=" + groupname + "");
            }
            else
            {
                context.Put("redirecturl", "../album/AlbumView" + ExName + "?aid=" + albumid);
            }
            ShowInfo(ref context);
        }
    }
}
