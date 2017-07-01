using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SetWallpaper;
using System.Runtime.InteropServices;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace SetWallpaper
{
    class ChangeWallpaperService:IChangeWallpaperService
    {
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
            );
        #region 实现更新墙纸图片
        /// <summary>
        /// 变换图片
        /// </summary>
        /// <param name="filename">图片文件夹名字</param>
        public void UpdateWallpaper()
        {
            ArrayList al = new ArrayList();
            //string[] PictureFiles = null;
            //存储图片路径
            string PicturePath = null;

            //读取当前程序目录名
            string CurrentDir = Directory.GetCurrentDirectory();
            //读取XML背景信息
            DataSet ds = new DataSet();
            ds.ReadXml("AdvInfo.xml");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string type = ds.Tables[0].Rows[i][6].ToString();
                //以此为根目录,读取filename下面的文件,存储到PictureFiles
                if (type == "2")
                {
                        al.Add(ds.Tables[0].Rows[i][2].ToString());
                }
            }
            ////以WallpaperPicture中的文件数为上限,以零为底限开始随机数,并作为数组下标
            Random rd = new Random();
            PicturePath =UpdatePictureInfo(CurrentDir+@"\Adpictures\"+al[rd.Next(0, al.Count)].ToString().Substring(2));
            ////实例化ActiveDesktop
            //ActiveDesktop RefreshDesktop = new ActiveDesktop();

            ////实现IActiveDesktop接口
            //IActiveDesktop iad = RefreshDesktop as IActiveDesktop;
            //设置墙纸
            //iad.SetWallpaper(PicturePath, 0);
            //刷新桌面
            //iad.ApplyChanges(AD_APPLY.ALL);
            if (File.Exists(PicturePath))
                SystemParametersInfo(20, 0, PicturePath, 0x2); // 0x1 | 0x2 
            File.Delete(PicturePath.Substring(0, PicturePath.LastIndexOf('.')) + ".bmp");
        }
        #endregion
        #region
        public string UpdatePictureInfo(string PicturePath)
        {
            Image JpgImage = Image.FromFile(PicturePath);
            string newFileName = PicturePath.Substring(0, PicturePath.LastIndexOf('.')) + ".bmp";

            JpgImage.Save(newFileName, System.Drawing.Imaging.ImageFormat.Bmp);
            return newFileName;
        }
        #endregion
    }
}
