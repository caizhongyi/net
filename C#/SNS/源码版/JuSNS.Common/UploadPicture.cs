using System;
using System.Collections.Generic;
using System.Web;
using JuSNS.Config;
using System.Drawing;

namespace JuSNS.Common
{
    public class UploadPicture : UploadPic
    {
        public UploadPicture(HttpFileCollection hfc)
            : base(hfc)
        {
            server = PicConfig.PhotoServer;
        }
        public UploadPicture()
            : base()
        {
            server = PicConfig.PhotoServer;
        }

        //0成功，1图片太小，2图片太大，3图片格式不正确，</returns>
        public override int Start()
        {
            int rint = 0;
            string dir = string.Empty;
            dir = PicConfig.PhotoRoot;
            string path = RootDir + "/" + dir;
            path = HttpContext.Current.Server.MapPath(path);
            Dictionary<string, PicConfigInfo> pci = PicConfig.Photo;
            int j = 0;
            for (int i = 0; i < hfc.Count; i++)
            {
                string fileMainName = Public.GetNewFileName();
                string fileExtName = GetExt(hfc[i].FileName);
                string filename = fileMainName + "." + fileExtName;
                if (i > 0 && hfc[i].ContentLength == 0)
                {
                    return 0;
                }
                if (hfc[i].ContentLength > size)
                {
                    return 2;
                }
                if (!AllowExt(fileExtName))
                {
                    return 3;
                }
                bool issuc = false;
                #region 上传for本地
                Upload up = new Upload();
                up.PostedFile = hfc[i];
                up.Extension = fileExtName;
                up.FileLength = up.PostedFile.ContentLength;
                up.FileName = filename;
                up.SavePath = path;
                string err = up.UploadStart();
                if (err.IndexOf('$') >-1)
                {
                    Thumbnail.MakeAllThumbnail(path, filename);
                    _SuccessNum++;
                    issuc = true;
                }
                #endregion

                if (issuc)
                {
                    Array.Resize<int>(ref _Width, j + 1);
                    Array.Resize<int>(ref _Height, j + 1);
                    Array.Resize<string>(ref _FileName, j + 1);
                    Array.Resize<int>(ref _ContentLength, j + 1);
                    System.Drawing.Image image = Bitmap.FromStream(hfc[i].InputStream);
                    _Width[j] = image.Width;
                    _Height[j] = image.Height;
                    _FileName[j] = filename;
                    _ContentLength[j] = hfc[i].ContentLength;
                    j++;
                    rint = 0;
                }
            }
            return rint;
        }
    }
}
