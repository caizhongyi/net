using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SetWallpaper;

namespace SetWallpaper
{
    class ChangeWallpaperService:IChangeWallpaperService
    {
        #region ʵ�ָ���ǽֽͼƬ
        /// <summary>
        /// �任ͼƬ
        /// </summary>
        /// <param name="filename">ͼƬ�ļ�������</param>
        public void UpdateWallpaper()
        {
            //�洢ͼƬ·��
            string PicturePath = null;

            //��ȡ��ǰ����Ŀ¼��
            string CurrentDir = Directory.GetCurrentDirectory();

            //�Դ�Ϊ��Ŀ¼,��ȡfilename������ļ�,�洢��PictureFiles
            string[] PictureFiles = Directory.GetFiles(CurrentDir+"\\WallpaperPicture\\".ToString());

            //��WallpaperPicture�е��ļ���Ϊ����,����Ϊ���޿�ʼ�����,����Ϊ�����±�
            Random rd = new Random();
            PicturePath=PictureFiles[rd.Next(0,PictureFiles.Length-1)];

            //ʵ����ActiveDesktop
            ActiveDesktop RefreshDesktop = new ActiveDesktop();

            //ʵ��IActiveDesktop�ӿ�
            IActiveDesktop iad = RefreshDesktop as IActiveDesktop;

            //����ǽֽ
            iad.SetWallpaper(PicturePath,0);

            //ˢ������
            iad.ApplyChanges(AD_APPLY.ALL);

        }

        #endregion
    }
}
