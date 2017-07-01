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
    public class photoadd : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAtiveValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ative");
            }
            else
            {
                int aid = GetInt("aid", 0);
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int aid = GetInt("aid", 0);
            context.Put("aid", aid);
            AtiveInfo mdl = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(aid);
            context.Put("cpagetitle", mdl.AtiveName + "__上传图片");
        }


        public override void Page_PostBack(ref VelocityContext context)
        {
            UploadPicture picup = new UploadPicture();
            int rint = picup.Start();
            int albumid = -1;
            int aid = GetInt("aid", 0);
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
                    for (int i = 0; i < picup.SuccessNum; i++)
                    {
                        //照片信息入库
                        PhotoInfo phi = new PhotoInfo();
                        phi.AlbumID = albumid;
                        phi.Comments = 0;
                        phi.Description = string.Empty;
                        phi.FilePath = picup.FileName[i];
                        phi.FileSize = picup.ContentLength[i];
                        phi.Height = picup.Height[i];
                        phi.AtiveID = aid;
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
                        JuSNS.Home.App.Photo.Instance.Add(phi);
                    }
                }
            }
            if (!string.IsNullOrEmpty(error))
            {
                context.Put("errors", error);
            }
            else
            {
                    context.Put("redirecturl", "album" + ExName + "?aid=" + aid);
            }
            ShowInfo(ref context);
        }
    }
}
