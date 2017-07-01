using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace SetWallpaper
{
    class ChangeAdPictureService:IChangeAdPictureService
    {

        #region   用Timer实现对广告图片的切换

        public void GetPictureTime()
        {
            Thread td = new Thread(new ThreadStart(UpdateAdPicture));
            td.Start();
        }

        #endregion

        private void UpdateAdPicture()
        {
            while (true)
            {
                //实现线程控制
                Thread.Sleep(1500);

                //存储图片路径
                string PicturePath = null;

                //读取当前程序目录名
                string CurrentDir = Directory.GetCurrentDirectory();

                //以此为根目录,读取filename下面的文件,存储到PictureFiles
                string[] PictureFiles = Directory.GetFiles(CurrentDir + "\\AdPicture\\".ToString());

                //使用数组下标，取得文件名
                Random rd = new Random();
                PicturePath = PictureFiles[rd.Next(0,PictureFiles.Length-1)];

            }
        }

    }
}
