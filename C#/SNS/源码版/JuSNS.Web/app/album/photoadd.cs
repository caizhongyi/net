using System;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.album
{
    public class photoadd : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "上传图片");
            int aid = GetInt("aid", 0);
            AlbumInfo mdl = JuSNS.Home.App.Album.Instance.GetInfo(aid);
            context.Put("albumname", mdl.Title);
            if (mdl.UserID != this.UserID)
            {
                context.Put("errors", "错误的参数：UserParamError");
            }
            else
            {
                int gid = GetInt("gid", 0);
                string groupname = GetString("groupname"); 
                if (gid > 0)
                {
                    if (string.IsNullOrEmpty(groupname))
                    {
                        context.Put("errors", "参数传递错误");
                        gid = 0;
                    }
                    else
                    {
                        context.Put("groupname", groupname);
                        context.Put("gid", gid);
                    }
                }
                ShowAlbumlist(ref context, aid, gid);
            }
        }

        protected void ShowAlbumlist(ref VelocityContext context,int aid,int gid)
        {
            List<AlbumInfo> InfoList = JuSNS.Home.App.Album.Instance.AlbumList(this.UserID, gid);
            string listSTR = string.Empty;
            foreach (AlbumInfo info in InfoList)
            {
                if (aid == info.AlbumID)
                {
                    listSTR += "<option value=\"" + info.AlbumID + "\" selected>" + info.Title + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.AlbumID + "\">" + info.Title + "</option>";
                }
            }
            context.Put("albumlist", listSTR);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            UploadPicture picup = new UploadPicture();
            int rint  = picup.Start();
            int albumid = GetInt("albumid", 0);
            int gid = GetInt("gid", 0);
            string groupname = GetString("groupname");
            string error = string.Empty;
            if (rint != 0)
            {
                switch (rint)
                {
                    case -1:
                        error += "上传失败";
                        break;
                    case 1:
                        error += "图片太小";
                        context.Put("errors", "图片太小");
                        break;
                    case 2:
                        error += "图片太大。最多允许上传" + Convert.ToInt32(JuSNS.Common.Public.GetXMLValue("picsize")) * 1000 + "KB的图片。";
                        break;
                    case 3:
                        error += "图片格式不正确。格式只允许：" + JuSNS.Common.Public.GetXMLValue("pictype");
                        break;
                }
            }
            else
            {
                if (picup.SuccessNum < 1)
                {
                    error += "上传图片不成功！";
                }
                else
                {
                    int m = 0;
                    string pic = string.Empty;
                    for (int i = 0; i < picup.SuccessNum; i++)
                    {
                        //照片信息入库
                        PhotoInfo phi = new PhotoInfo();
                        phi.AlbumID = albumid;
                        phi.AtiveID = 0;
                        phi.Comments = 0;
                        phi.Description = string.Empty;
                        string filepath=picup.FileName[i];
                        phi.FilePath = filepath;
                        phi.FileSize = picup.ContentLength[i];
                        phi.Height = picup.Height[i];
                        phi.Width = picup.Width[i];
                        phi.IsCover = false;
                        bool isLock = false;
                        int UploadCheck = Convert.ToInt32(Public.GetXMLAlbumValue("UploadCheck"));
                        if (UploadCheck == 1) isLock = true;
                        phi.IsLock = isLock;
                        phi.PhotoType = 1;
                        phi.PostIP = Common.Public.GetClientIP();
                        phi.PostTime = DateTime.Now;
                        phi.State = 0;
                        phi.UserID = this.UserID;
                        phi.Views = 0;
                        int pid = JuSNS.Home.App.Photo.Instance.Add(phi);
                        if (pid > 0)
                        {
                            pic += filepath + ",";
                            m++;
                        }
                    }
                    pic = Input.FixCommaStr(pic);
                    //更新积分
                    JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(8) * m, 0, 0, "上传了照片，共" + m + "张");
                    //插入动态
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, albumid, (int)EnumDynType.CreatPhoto, pic, DateTime.Now, 0, string.Empty));
                }
            }
            if (!string.IsNullOrEmpty(error))
            {
                context.Put("errors", error);
            }
            else
            {
                if (gid > 0)
                {
                    context.Put("redirecturl", "PhotoEdit" + ExName + "?aid=" + albumid + "&gid=" + gid + "&groupname=" + groupname);
                }
                else
                {
                    context.Put("redirecturl", "PhotoEdit" + ExName + "?aid=" + albumid);
                }
            }
            ShowInfo(ref context);
        }
    }
}
