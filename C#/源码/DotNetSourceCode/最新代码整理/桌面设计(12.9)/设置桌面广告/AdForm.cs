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
using System.Threading;
using Microsoft.Win32;
using UnCompress;
using WbSystem.FileManage;
using System.Collections;
using SetWallpaper;
using GetIpInfo;
using WindowsApplication1.ListenInfo;
namespace WbSystem
{
    public partial class AdForm : Form
    {
        string CurrentDir = Directory.GetCurrentDirectory();
        //控制图片转换
        public System.Timers.Timer tda;
        public System.Timers.Timer tdb;
        public System.Timers.Timer tdc;
        public System.Timers.Timer tdd;
        public System.Timers.Timer tde;

        //保存图片信息
        public static string ImageUrl = null;
        public static string ImageTitle = null;
        public static string ImageContent = null;
        public static string CompanyURL = null;

        private static int Aimage = 0;
        private static int Bimage = 0;
        private static int Cimage = 0;
        private static int Dimage = 0;
        private static int Eimage = 0;
        //图片速度  
        private int Sd = 5000;
        //保存图片地址
        private static ArrayList alA;
        private static ArrayList alB;
        private static ArrayList alC;
        private static ArrayList alD;
        private static ArrayList alE;

        private static ArrayList AdTitleA;
        private static ArrayList AdTitleB;
        private static ArrayList AdTitleC;
        private static ArrayList AdTitleD;
        private static ArrayList AdTitleE;

        private static ArrayList AdContentA;
        private static ArrayList AdContentB;
        private static ArrayList AdContentC;
        private static ArrayList AdContentD;
        private static ArrayList AdContentE;

        private static ArrayList CompanyURLA;
        private static ArrayList CompanyURLB;
        private static ArrayList CompanyURLC;
        private static ArrayList CompanyURLD;
        private static ArrayList CompanyURLE;

        public PictureInfoForm pif = null;
        public static bool IsForm = true;
        public AdForm()
        {       
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void AdForm_Load(object sender, EventArgs e)
        {

            //调用桌面背景
            Factory.GetNewPicture().UpdateWallpaper();

           //读取图片信息
            UpdateAdPicture();
            //设置Ad图片位置
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            //设置背景为透明
            this.BackColor = Color.FromArgb(255, 200, 0);
            this.TransparencyKey = BackColor;

            //显示Ad广告窗体
            NewsInfoForm nif = new NewsInfoForm();
            nif.Show();
        }
        private void TimeMenthA()
        {
            tda = new System.Timers.Timer(Sd);
            tda.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureA);
            tda.AutoReset = true;
            tda.Start();

        }
        private void TimeMenthB()
        {
            tdb = new System.Timers.Timer(Sd);
            tdb.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureB);
            tdb.AutoReset = true;
            tdb.Start();
        }
        private void TimeMenthC()
        {
            tdc = new System.Timers.Timer(Sd);
            tdc.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureC);
            tdc.AutoReset = true;
            tdc.Start();
        }
        private void TimeMenthD()
        {
            tdd = new System.Timers.Timer(Sd);
            tdd.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureD);
            tdd.AutoReset = true;
            tdd.Start();
        }
        private void TimeMenthE()
        {
            tde = new System.Timers.Timer(Sd);
            tde.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPictureE);
            tde.AutoReset = true;
            tde.Start();
        }
        private void UpdateAdPictureA(object sender, System.Timers.ElapsedEventArgs e)
        {
            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            try
            {
                //获取根目录
                if (Aimage < alA.Count)
                {
                    PicturePath = CurrentDir + @"\Adpictures\" + alA[Aimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd1.Image = (Image)NewBitmap;
                    Aimage = Aimage + 1;
                }
                else
                {
                    Aimage = 0;
                    PicturePath = CurrentDir + @"\Adpictures\" + alA[Aimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd1.Image = (Image)NewBitmap;
                    Aimage++;
                }
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(Application.StartupPath + @"\Adpictures(YunZhou)\右侧广告1\YunZhou.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd1.Image = (Image)NewBitmap;
            }
        }
        private void UpdateAdPictureB(object sender, System.Timers.ElapsedEventArgs e)
        {
            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            try
            {
                //获取根目录
                if (Bimage < alB.Count)
                {
                    PicturePath = CurrentDir + @"\Adpictures\" + alB[Bimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd2.Image = (Image)NewBitmap;
                    Bimage = Bimage + 1;
                }
                else
                {
                    Bimage = 0;
                    PicturePath = CurrentDir + @"\Adpictures\" + alB[Bimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd2.Image = (Image)NewBitmap;
                    Bimage++;
                }
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(Application.StartupPath + @"\Adpictures(YunZhou)\右侧广告1\YunZhou.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd2.Image = (Image)NewBitmap;
            }
        }
        private void UpdateAdPictureC(object sender, System.Timers.ElapsedEventArgs e)
        {
            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            try
            {
                //获取根目录
                if (Cimage < alC.Count)
                {
                    PicturePath = CurrentDir + @"\Adpictures\" + alC[Cimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd3.Image = (Image)NewBitmap;
                    Cimage =Cimage + 1;
                }
                else
                {
                    Cimage = 0;
                    PicturePath = CurrentDir + @"\Adpictures\" + alC[Cimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd3.Image = (Image)NewBitmap;
                    Cimage++;
                }
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(Application.StartupPath + @"\Adpictures(YunZhou)\右侧广告1\YunZhou.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd3.Image = (Image)NewBitmap;
            }
        }
        private void UpdateAdPictureD(object sender, System.Timers.ElapsedEventArgs e)
        {
            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            try
            {
                //获取根目录
                if (Dimage < alD.Count)
                {
                    PicturePath = CurrentDir + @"\Adpictures\" + alD[Dimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd4.Image = (Image)NewBitmap;
                    Dimage = Dimage + 1;
                }
                else
                {
                    Dimage = 0;
                    PicturePath = CurrentDir + @"\Adpictures\" + alD[Dimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd4.Image = (Image)NewBitmap;
                    Dimage++;
                }
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(Application.StartupPath + @"\Adpictures(YunZhou)\右侧广告1\YunZhou.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd4.Image = (Image)NewBitmap;
            }
        }
        private void UpdateAdPictureE(object sender, System.Timers.ElapsedEventArgs e)
        {
            string PicturePath = null;
            //以此为根目录,读取filename下面的文件,存储到PictureFiles
            try
            {
                //获取根目录
                if (Eimage < alE.Count)
                {
                    PicturePath = CurrentDir + @"\Adpictures\" + alE[Eimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd5.Image = (Image)NewBitmap;
                    Eimage = Eimage + 1;
                }
                else
                {
                    Eimage = 0;
                    PicturePath = CurrentDir + @"\Adpictures\" + alE[Eimage].ToString().Substring(2);
                    System.Drawing.Bitmap MyBitmap = new Bitmap(PicturePath);
                    Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                    pictureAd5.Image = (Image)NewBitmap;
                    Eimage++;
                }
            }
            catch
            {
                System.Drawing.Bitmap MyBitmap = new Bitmap(Application.StartupPath + @"\Adpictures(YunZhou)\右侧广告1\YunZhou.jpg");
                Bitmap NewBitmap = new Bitmap(MyBitmap, 178, 110);
                pictureAd5.Image = (Image)NewBitmap;
            }
        } 

        //读取广告图片文件   
        private void pictureAd1_Click(object sender, EventArgs e)
        {
            ImageUrl =CurrentDir+@"\Adpictures\" + alA[Aimage-1].ToString().Substring(2);
            ImageTitle = AdTitleA[Aimage - 1].ToString();
            ImageContent = AdContentA[Aimage - 1].ToString();
            CompanyURL = CompanyURLA[Aimage - 1].ToString();
            FormChange();

        }

        private void pictureAd2_Click(object sender, EventArgs e)
        {
            ImageUrl = CurrentDir + @"\Adpictures\" + alB[Bimage - 1].ToString().Substring(2);
            ImageTitle = AdTitleB[Bimage - 1].ToString();
            ImageContent = AdContentB[Bimage - 1].ToString();
            CompanyURL = CompanyURLB[Bimage - 1].ToString();
            FormChange();
        }

        private void pictureAd3_Click(object sender, EventArgs e)
        {
            ImageUrl = CurrentDir + @"\Adpictures\" + alC[Cimage - 1].ToString().Substring(2);
            ImageTitle = AdTitleC[Cimage - 1].ToString();
            ImageContent = AdContentC[Cimage - 1].ToString();
            CompanyURL = CompanyURLC[Cimage - 1].ToString();
            FormChange();
        }

        private void pictureAd4_Click(object sender, EventArgs e)
        {
            ImageUrl = CurrentDir + @"\Adpictures\" + alD[Dimage - 1].ToString().Substring(2);
            ImageTitle = AdTitleD[Dimage - 1].ToString();
            ImageContent = AdContentD[Dimage - 1].ToString();
            CompanyURL = CompanyURLD[Dimage - 1].ToString();

            FormChange();
        }
 
        private void pictureAd5_Click(object sender, EventArgs e)
        {
            ImageUrl = CurrentDir + @"\Adpictures\" + alE[Eimage - 1].ToString().Substring(2);
            ImageTitle = AdTitleE[Eimage - 1].ToString();
            ImageContent = AdContentE[Eimage - 1].ToString();
            CompanyURL = CompanyURLE[Eimage - 1].ToString();
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
        //读取图片
        private void UpdateAdPicture()
        {
            alA = new ArrayList();
            alB= new ArrayList();
            alC = new ArrayList();
            alD = new ArrayList();
            alE = new ArrayList();

            AdContentA = new ArrayList();
            AdContentB= new ArrayList();
            AdContentC= new ArrayList();
            AdContentD = new ArrayList();
            AdContentE = new ArrayList();

            AdTitleA = new ArrayList();
            AdTitleB = new ArrayList();
            AdTitleC= new ArrayList();
            AdTitleD = new ArrayList();
            AdTitleE = new ArrayList();

            CompanyURLA = new ArrayList();
            CompanyURLB = new ArrayList();
            CompanyURLC = new ArrayList();
            CompanyURLD = new ArrayList();
            CompanyURLE = new ArrayList();


            DataSet ds = new DataSet();
            ds.ReadXml("AdvInfo.xml");
            for (int i = 0;  i < ds.Tables[0].Rows.Count;  i++)
            {
                string type = ds.Tables[0].Rows[i][6].ToString();
                switch (type)
                {
                    case "6":
                        alA.Add(ds.Tables[0].Rows[i][2].ToString());//右侧广告1 
                        AdTitleA.Add(ds.Tables[0].Rows[i][1].ToString());
                        AdContentA.Add(ds.Tables[0].Rows[i][4].ToString());
                        CompanyURLA.Add(ds.Tables[0].Rows[i][7].ToString());
                        break;
                    case "7":
                        alB.Add(ds.Tables[0].Rows[i][2].ToString());//右侧广告2 
                        AdTitleB.Add(ds.Tables[0].Rows[i][1].ToString());
                        AdContentB.Add(ds.Tables[0].Rows[i][4].ToString());
                        CompanyURLB.Add(ds.Tables[0].Rows[i][7].ToString());
                        break;
                    case "8":
                        alC.Add(ds.Tables[0].Rows[i][2].ToString());//右侧广告3 
                        AdTitleC.Add(ds.Tables[0].Rows[i][1].ToString());
                        AdContentC.Add(ds.Tables[0].Rows[i][4].ToString());
                        CompanyURLC.Add(ds.Tables[0].Rows[i][7].ToString());
                        break;
                    case "9":
                        alD.Add(ds.Tables[0].Rows[i][2].ToString());//右侧广告4 
                        AdTitleD.Add(ds.Tables[0].Rows[i][1].ToString());
                        AdContentD.Add(ds.Tables[0].Rows[i][4].ToString());
                        CompanyURLD.Add(ds.Tables[0].Rows[i][7].ToString());
                        break;
                    case "10":
                        alE.Add(ds.Tables[0].Rows[i][2].ToString());//右侧广告5
                        AdTitleE.Add(ds.Tables[0].Rows[i][1].ToString());
                        AdContentE.Add(ds.Tables[0].Rows[i][4].ToString());
                        CompanyURLE.Add(ds.Tables[0].Rows[i][7].ToString());
                        break;
                }
            }
            //调用timer事件
            TimeMenthA();
            TimeMenthB();
            TimeMenthC();
            TimeMenthD();
            TimeMenthE();
        }
    }
}