using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace MyClass.MyFileOpeation
{
    /// <summary>
    /// 随机读取文件
    /// </summary>
    public class RadomReadFile
    {  
        #region 实现文件读取方法
        /// <summary>
        /// 实现文件读取方法
        /// </summary>
        /// <param name="CurrentDir"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static Image GetPictureFile(string CurrentDir, string FileName)
        {

            //存储图片路径
            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            string[] PictureFiles = Directory.GetFiles(CurrentDir + FileName);
            Random rd = new Random();
            //使用数组下标，取得文件名
            PicturePath = PictureFiles[rd.Next(0, PictureFiles.Length - 1)];
            return System.Drawing.Image.FromFile(PicturePath);

        }
        #endregion

        #region   用Timer实现对广告图片的切换
        public static  string FileUrl=string .Empty ;
        public static void GetPictureTime()
        {
            Thread td = new Thread(new ThreadStart(UpdateAdPicture));
            td.Start();
        }

        private static void UpdateAdPicture()
        {
            while (true)
            {
                //实现线程控制
                Thread.Sleep(1500);

                //存储图片路径
                string PicturePath = null;

                //读取当前程序目录名
                string CurrentDir = Application.StartupPath;

                //以此为根目录,读取filename下面的文件,存储到PictureFiles
                string[] PictureFiles = Directory.GetFiles(CurrentDir + FileUrl.ToString());

                //使用数组下标，取得文件名
                Random rd = new Random();
                PicturePath = PictureFiles[rd.Next(0, PictureFiles.Length - 1)];

            }
        }

        #endregion

    }
}
