using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Drawing;

namespace SetWallpaper
{
    class ChangeAdPictureService:IChangeAdPictureService
    {

        #region   ��Timerʵ�ֶԹ��ͼƬ���л�

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
                //ʵ���߳̿���
                Thread.Sleep(1500);

                //�洢ͼƬ·��
                string PicturePath = null;

                //��ȡ��ǰ����Ŀ¼��
                string CurrentDir = Directory.GetCurrentDirectory();

                //�Դ�Ϊ��Ŀ¼,��ȡfilename������ļ�,�洢��PictureFiles
                string[] PictureFiles = Directory.GetFiles(CurrentDir + "\\AdPicture\\".ToString());

                //ʹ�������±꣬ȡ���ļ���
                Random rd = new Random();
                PicturePath = PictureFiles[rd.Next(0,PictureFiles.Length-1)];

            }
        }

        public Image ChanageAdSize(string StrFileName,int width,int heigh)
        {
            System.Drawing.Bitmap MyBitmap = new Bitmap(StrFileName);
            Bitmap NewBitmap = new Bitmap(MyBitmap, width, heigh);
            return NewBitmap;
        }
    }
}
