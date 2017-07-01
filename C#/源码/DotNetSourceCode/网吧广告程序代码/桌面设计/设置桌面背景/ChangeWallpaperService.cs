using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SetWallpaper;

namespace SetWallpaper
{
    class ChangeWallpaperService:IChangeWallpaperService
    {
        #region 实现更新墙纸图片
        /// <summary>
        /// 变换图片
        /// </summary>
        /// <param name="filename">图片文件夹名字</param>
        public void UpdateWallpaper()
        {
            //存储图片路径
            string PicturePath = null;

            //读取当前程序目录名
            string CurrentDir = Directory.GetCurrentDirectory();

            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            string[] PictureFiles = Directory.GetFiles(CurrentDir+"\\WallpaperPicture\\".ToString());

            //以WallpaperPicture中的文件数为上限,以零为底限开始随机数,并作为数组下标
            Random rd = new Random();
            PicturePath=PictureFiles[rd.Next(0,PictureFiles.Length-1)];

            //实例化ActiveDesktop
            ActiveDesktop RefreshDesktop = new ActiveDesktop();

            //实现IActiveDesktop接口
            IActiveDesktop iad = RefreshDesktop as IActiveDesktop;

            //设置墙纸
            iad.SetWallpaper(PicturePath,0);

            //刷新桌面
            iad.ApplyChanges(AD_APPLY.ALL);

        }

        #endregion
    }
}
