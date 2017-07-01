using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SetWallpaper;

namespace WbSystem
{
    public partial class PictureInfoForm : Form
    {
        public static int i = 1;

        //��һҳ��ʾЧ��
        public static int a = 1;
        public static int b = 1;
        public static int c = 1;
        public static int d = 1;
        public static int ei = 1;
        //��һҳ��ʾЧ��
        public static int au = 0;
        //ת��ͼƬ
        IChangeAdPictureService icaps = Factory.GetNewAdPicture();
        public PictureInfoForm()
        {
            InitializeComponent();
        }

        private void PictureInfoForm_Load(object sender, EventArgs e)
        {
            //�ж���һҳ����Ϣ
            au = GetA();


            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - BackForm.Formwidth-this.Width, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            
            this.BackColor = Color.FromArgb(255,200,0);
            this.TransparencyKey = BackColor;
            //����Ԥ��ͼƬ
            KeepPicture();
            try
            {
                //�洢ͼƬ·��
                string PicturePath = null;
                //�Դ�Ϊ��Ŀ¼,��ȡfilename������ļ�,�洢��PictureFiles
                string[] PictureFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + AdForm.ImageUrl);

                //ʹ�������±꣬ȡ���ļ���
                PicturePath = PictureFiles[AdForm.ImageCount];
                PbImage.Image = icaps.ChanageAdSize(PicturePath, 150, 50);
            }
            catch
            {
                return;
            }  
        }
         ///<summary>
         ///ʵ���ļ���ȡ����
         ///</summary>
         ///<param name="CurrentDir"></param>
         ///<param name="FileName"></param>
         ///<returns></returns>
        Image GetPictureFile(string CurrentDir,string FileName)
        {
                //�洢ͼƬ·��
                string PicturePath = null;
                //�Դ�Ϊ��Ŀ¼,��ȡfilename������ļ�,�洢��PictureFiles
                string[] PictureFiles = null;  
                PictureFiles = Directory.GetFiles(CurrentDir + FileName);
                PicturePath = PictureFiles[i];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap,74, 51);
                return NewBitmap;
        }
        void KeepPicture()
        {
            pbA.Image = GetPictureFile(Directory.GetCurrentDirectory(), @"\AdPictureA\");
            pbB.Image = GetPictureFile(Directory.GetCurrentDirectory(), @"\AdPictureB\");
            pbC.Image = GetPictureFile(Directory.GetCurrentDirectory(), @"\AdPictureC\");
            pbD.Image = GetPictureFile(Directory.GetCurrentDirectory(), @"\AdPictureD\");
            pbE.Image = GetPictureFile(Directory.GetCurrentDirectory(), @"\AdPictureE\");
        }
        private void btnClosed_Click(object sender, EventArgs e)
        {
            WbSystem.AdForm.IsForm = true;
            this.Close();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string[] PictureFilesA = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureA\");
            string[] PictureFilesB = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureB\");
            string[] PictureFilesC = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureC\");
            string[] PictureFilesD = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureD\");
            string[] PictureFilesE = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureE\");
            //a
            if (a < PictureFilesA.Length)
            { 
                string PicturePathA = PictureFilesA[a];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePathA);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbA.Image = (Image)NewBitmap;
                au = a;
                a++;

            }
            else
            {
                a = 1;
            }
            //b
            if (b < PictureFilesB.Length)
            {
                string PicturePathB = PictureFilesB[b];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePathB);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbB.Image = (Image)NewBitmap;
                b++;
            }
            else
            {
                b = 1;
            }
            //c
            if (c < PictureFilesC.Length)
            {
                string PicturePathC = PictureFilesC[c];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePathC);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbC.Image = (Image)NewBitmap;
                c++;
            }
            else
            {
                c = 1;
            }
            //d
            if (d < PictureFilesD.Length)
            {
                string PicturePathD = PictureFilesD[d];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePathD);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbD.Image = (Image)NewBitmap;
                d++;
            }
            else
            {
                d = 1;
            }
            //e
            if (ei < PictureFilesE.Length)
            { 
                string PicturePathE = PictureFilesE[ei];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePathE);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbE.Image = (Image)NewBitmap;
                ei++;
            }
            else
            {
                ei = 1;
            }
        }
        //�ж���һҳ��ͼƬ��Ϣ
        private int GetA()
        {
            string[] PictureFilesA = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureA\");
            return PictureFilesA.Length-1;
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            string[] PictureFilesA = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureA\");
            string[] PictureFilesB = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureB\");
            string[] PictureFilesC = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureC\");
            string[] PictureFilesD = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureD\");
            string[] PictureFilesE = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\AdPictureE\");
            //a
            if (au >0)
            {
                string PicturePath = PictureFilesA[au];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbA.Image = (Image)NewBitmap;
                au--;
            }
            else
            {
                au = GetA();
                string PicturePath = PictureFilesA[au];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 74, 51);
                pbA.Image = (Image)NewBitmap;
            }
        }
    }
}