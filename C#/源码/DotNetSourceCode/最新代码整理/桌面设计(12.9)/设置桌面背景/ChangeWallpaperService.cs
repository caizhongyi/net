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
        #region ʵ�ָ���ǽֽͼƬ
        /// <summary>
        /// �任ͼƬ
        /// </summary>
        /// <param name="filename">ͼƬ�ļ�������</param>
        public void UpdateWallpaper()
        {
            ArrayList al = new ArrayList();
            //string[] PictureFiles = null;
            //�洢ͼƬ·��
            string PicturePath = null;

            //��ȡ��ǰ����Ŀ¼��
            string CurrentDir = Directory.GetCurrentDirectory();
            //��ȡXML������Ϣ
            DataSet ds = new DataSet();
            ds.ReadXml("AdvInfo.xml");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string type = ds.Tables[0].Rows[i][6].ToString();
                //�Դ�Ϊ��Ŀ¼,��ȡfilename������ļ�,�洢��PictureFiles
                if (type == "2")
                {
                        al.Add(ds.Tables[0].Rows[i][2].ToString());
                }
            }
            ////��WallpaperPicture�е��ļ���Ϊ����,����Ϊ���޿�ʼ�����,����Ϊ�����±�
            Random rd = new Random();
            PicturePath =UpdatePictureInfo(CurrentDir+@"\Adpictures\"+al[rd.Next(0, al.Count)].ToString().Substring(2));
            ////ʵ����ActiveDesktop
            //ActiveDesktop RefreshDesktop = new ActiveDesktop();

            ////ʵ��IActiveDesktop�ӿ�
            //IActiveDesktop iad = RefreshDesktop as IActiveDesktop;
            //����ǽֽ
            //iad.SetWallpaper(PicturePath, 0);
            //ˢ������
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
