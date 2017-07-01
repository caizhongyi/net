using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.UI.Page;
using NVelocity;
using System.IO;

namespace JuSNS.Web.space.info
{
    public class head1 : UserPage
    {
        public string f = string.Empty;
        static string filename = string.Empty;
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "调整/裁剪头像");
            filename = this.GetHeadPath(this.UserID);
            if (string.IsNullOrEmpty(filename))
            {
                context.Put("redirecturl", "head" + ExName + "?r=nohead&f=" + f + "");
            }
            else
            {
                f = GetQueryString("f");
                if (!string.IsNullOrEmpty(f) && f == "1") { context.Put("startlogin", true); }
                context.Put("flag", f);
                string myhead = this.GetHeadImage(this.UserID, 3);
                if (myhead.IndexOf("default") > -1)
                {
                    context.Put("redirecturl", "head" + ExName + "?r=nohead&f=" + f + "");
                }
                context.Put("orghead", this.GetHeadImage(this.UserID, 3));
            }
        }

        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            ShowInfo(ref context);
            string tmpimg = this.GetFormString("img_pos");
            string tmpcut = this.GetFormString("cut_pos");
            if (string.IsNullOrEmpty(tmpimg) || string.IsNullOrEmpty(tmpcut))
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                string[] a_img_pos = tmpimg.Split(',');
                string[] a_cut_pos = tmpcut.Split(',');

                int imageWidth = Int32.Parse(a_img_pos[0]);
                int imageHeight = Int32.Parse(a_img_pos[1]);

                int cutTop = Int32.Parse(a_cut_pos[1]);
                int cutLeft = Int32.Parse(a_cut_pos[0]);

                int dropWidth = 120;
                int dropHeight = 120;
                string filepath = Public.GetSmallHeadPic(filename, 3);
                string smallPath = Public.GetSmallHeadPic(filename, 2);

                JuSNS.MVC.ZoomHead.SaveCutPic(HttpContext.Current.Server.MapPath(filepath), HttpContext.Current.Server.MapPath(smallPath), 0, 0, dropWidth, dropHeight, cutLeft, cutTop, imageWidth, imageHeight);
                //生成最小图片缩图
                MakeAllThumbnail(smallPath);
                if (!string.IsNullOrEmpty(GetFormString("hideflag")) && GetFormString("hideflag") == "1")
                {
                    context.Put("redirecturl", root + "/home/friend/city" + ExName + "?r=city&f=1");
                }
                else
                {
                    context.Put("redirecturl", "head" + ExName + "?r=succ&match=" + Rand.Number(6));
                }
            }
        }

        /// <summary>
        /// 生成所有缩略图
        /// </summary>
        /// <param name="path">原始图片路径</param>
        /// <param name="filename">文件名</param>
        public static void MakeAllThumbnail(string path)
        {
            Dictionary<string, PicConfigInfo> pci;
            pci = PicConfig.Portrait;
            foreach (string k in pci.Keys)
            {
                if (k == "0" || k == "1")
                {
                    Thumbnail thb = new Thumbnail(HttpContext.Current.Server.MapPath(path));
                    string dir = root + "/" + pci[k].Directory;
                    dir = HttpContext.Current.Server.MapPath(dir);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string cPath = dir + @"\" + filename;
                    if (File.Exists(cPath))
                    {
                        File.Delete(cPath);
                    }
                    thb.Width = pci[k].X;
                    thb.Height = pci[k].Y;
                    thb.NewPath = cPath;
                    thb.Make();
                }
            }
        }
    }
}
