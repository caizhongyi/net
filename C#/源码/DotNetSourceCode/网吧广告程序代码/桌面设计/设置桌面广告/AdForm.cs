using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace WbSystem
{
    public partial class AdForm : Form
    {
        //控制图片转换
        public System.Timers.Timer tda;
        public System.Timers.Timer tdb;
        public System.Timers.Timer tdc;
        public System.Timers.Timer tdd;
        public System.Timers.Timer tde;

        //保存图片信息
        public static string ImageUrl=null;
        public static int ImageCount=0;

        private static int Aimage=0;
        private static int Bimage=0;
        private static int Cimage=0;
        private static int Dimage=0;
        private static int Eimage=0;


        public PictureInfoForm pif=null;
        public static bool IsForm = true;
        public AdForm()
        {
            InitializeComponent();
        }

        private void AdForm_Load(object sender, EventArgs e)
        {
            //设置Ad图片位置
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width,(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height- this.Height)/2);

            //设置背景为透明
            this.BackColor = Color.FromArgb(255, 200, 0);
            this.TransparencyKey = BackColor;

            //调用timer事件
            TimeMenthA();
            TimeMenthB();
            TimeMenthC();
            TimeMenthD();
            TimeMenthE();

            //显示Ad广告窗体
            NewsInfoForm nif = new NewsInfoForm();
            nif.Show();
        }
        private void TimeMenthA()
        {
            tda = new System.Timers.Timer(3000);
            tda.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureA);
            tda.AutoReset = true;
            tda.Start();

        }
        private void TimeMenthB()
        {
            tdb = new System.Timers.Timer(3000);
            tdb.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureB);
            tdb.AutoReset = true;
            tdb.Start();
        }
        private void TimeMenthC()
        {
            tdc = new System.Timers.Timer(3000);
            tdc.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureC);
            tdc.AutoReset = true;
            tdc.Start();
        }
        private void TimeMenthD()
        {
            tdd = new System.Timers.Timer(3000);
            tdd.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureD);
            tdd.AutoReset = true;
            tdd.Start();
        }
        private void TimeMenthE()
        {
            tde = new System.Timers.Timer(3000);
            tde.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureE);
            tde.AutoReset = true;
            tde.Start();
        }
        private void UpdateAdPictureA(object sender, System.Timers.ElapsedEventArgs e)
        {
            pictureAd1.Image = null;

            //获取根目录
            string CurrentDir = Directory.GetCurrentDirectory();
            string FileNameA = @"\AdPictureA\";

            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            string[] PictureFiles = null;
            PictureFiles = Directory.GetFiles(CurrentDir + FileNameA);

            try
            {
                PicturePath = PictureFiles[Bimage];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd1.Image = (Image)NewBitmap;
                Aimage = Aimage + 1;
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(CurrentDir + FileNameA + "Log4.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd1.Image = (Image)NewBitmap;
                Aimage = 0;
                return;
            }
        }
        private void UpdateAdPictureB(object sender, System.Timers.ElapsedEventArgs e)
        {
                pictureAd2.Image = null;

                //获取根目录
                string CurrentDir = Directory.GetCurrentDirectory();
                string FileNameB = @"\AdPictureB\";

                string PicturePath = null;
                //以此为根目录,读取filename下面的文件,存储到PictureFiles
                string[] PictureFiles = null;
                PictureFiles = Directory.GetFiles(CurrentDir + FileNameB);

                try
                {
                    PicturePath = PictureFiles[Bimage];
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd2.Image = (Image)NewBitmap;
                    Bimage = Bimage + 1;
                }
                catch
                {
                    System.Drawing.Bitmap MyBitmap = new Bitmap(CurrentDir + FileNameB + "Log4.jpg");
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd2.Image = (Image)NewBitmap;
                    Bimage = 0;
                    return;
                }
        }
        private void UpdateAdPictureC(object sender, System.Timers.ElapsedEventArgs e)
        {
            pictureAd3.Image = null;
            //获取根目录
            string CurrentDir = Directory.GetCurrentDirectory();
            string FileNameC = @"\AdPictureC\";

            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            string[] PictureFiles = null;
            PictureFiles = Directory.GetFiles(CurrentDir + FileNameC);
            try
            {
                PicturePath = PictureFiles[Cimage];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd3.Image = (Image)NewBitmap;
                Cimage = Cimage + 1;
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(CurrentDir + FileNameC + "Log4.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd3.Image = (Image)NewBitmap;
                Cimage = 0;
                return;
            }
        }
        private void UpdateAdPictureD(object sender, System.Timers.ElapsedEventArgs e)
        {
            pictureAd4.Image = null;
            //获取根目录
            string CurrentDir = Directory.GetCurrentDirectory();
            string FileNameD = @"\AdPictureD\";

            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            string[] PictureFiles = null;
            PictureFiles = Directory.GetFiles(CurrentDir + FileNameD);
            try
            {
                PicturePath = PictureFiles[Cimage];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd4.Image = (Image)NewBitmap;
                Dimage = Dimage + 1;
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(CurrentDir + FileNameD + "Log4.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd4.Image = (Image)NewBitmap;
                Dimage = 0;
                return;
            }
        }
        private void UpdateAdPictureE(object sender, System.Timers.ElapsedEventArgs e)
        {
            pictureAd5.Image = null;
            //获取根目录
            string CurrentDir = Directory.GetCurrentDirectory();
            string FileNameE = @"\AdPictureE\";

            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            string[] PictureFiles = null;
            PictureFiles = Directory.GetFiles(CurrentDir + FileNameE);
            try
            {
                PicturePath = PictureFiles[Cimage];
                System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd5.Image = (Image)NewBitmap;
                Eimage = Eimage + 1;
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(CurrentDir + FileNameE + "Log4.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd5.Image = (Image)NewBitmap;
                Eimage = 0;
                return;
            }
        } 
        /// <summary>
        /// 实现文件读取方法
        /// </summary>
        /// <param name="CurrentDir"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        //public static Image GetPictureFile(string CurrentDir,string FileName)
        //{

        //        //存储图片路径
        //        string PicturePath = null;
        //        //以此为根目录,读取filename下面的文件,存储到PictureFiles
        //        string[] PictureFiles = null;  
        //        PictureFiles = Directory.GetFiles(CurrentDir + FileName);
        //        Random rd = new Random();
        //        //使用数组下标，取得文件名
        //        int i = rd.Next(0, PictureFiles.Length - 1);
        //        PicturePath = PictureFiles[i];
        //        ImageCount = i;
        //        return System.Drawing.Image.FromFile(PicturePath);
        //}

        //读取广告图片文件   
        private void pictureAd1_Click(object sender, EventArgs e)
        {
            ImageCount = Aimage - 1;
            ImageUrl = @"\AdPictureA\";

            FormChange();

        }

        private void pictureAd2_Click(object sender, EventArgs e)
        {
            ImageCount = Bimage - 1;
            ImageUrl = @"\AdPictureB\";

            FormChange();
        }

        private void pictureAd3_Click(object sender, EventArgs e)
        {
            ImageCount = Cimage - 1;
            ImageUrl = @"\AdPictureC\";

            FormChange();
        }

        private void pictureAd4_Click(object sender, EventArgs e)
        {
            ImageCount = Dimage - 1;
            ImageUrl = @"\AdPictureD\";

            FormChange();
        }
 
        private void pictureAd5_Click(object sender, EventArgs e)
        {
            ImageCount = Eimage - 1;
            ImageUrl = @"\AdPictureE\";

            FormChange();

        }

        #region 控制窗体状态
        private void FormChange()
        {
            if (IsForm)
            {
                pif = new PictureInfoForm();
                pif.Show();
                IsForm = false;
            }
            if (pif != null)
            {
                pif.Close();
                pif = new PictureInfoForm();
                pif.Show();
            }
        }
        #endregion
        private void pictureAd1_MouseMove(object sender, MouseEventArgs e)
        {
            tda.Stop();

        }

        private void pictureAd1_MouseLeave(object sender, EventArgs e)
        {
            tda.Start();
        }

        private void pictureAd2_MouseLeave(object sender, EventArgs e)
        {
            tdb.Start();
        }

        private void pictureAd2_MouseMove(object sender, MouseEventArgs e)
        {
            tdb.Stop();
        }

        private void pictureAd3_MouseLeave(object sender, EventArgs e)
        {
            tdc.Start();
        }

        private void pictureAd3_MouseMove(object sender, MouseEventArgs e)
        {
            tdc.Stop();
        }

        private void pictureAd4_MouseLeave(object sender, EventArgs e)
        {
            tdd.Start();
        }

        private void pictureAd4_MouseMove(object sender, MouseEventArgs e)
        {
            tdd.Stop();
        }

        private void pictureAd5_MouseLeave(object sender, EventArgs e)
        {
            tde.Start();
        }

        private void pictureAd5_MouseMove(object sender, MouseEventArgs e)
        {
            tde.Stop();
        }
    }
}