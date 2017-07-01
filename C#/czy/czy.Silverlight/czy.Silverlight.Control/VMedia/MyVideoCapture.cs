using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace czy.Silverlight.Controls
{
    public class MyVideoCapture
    {
        /// <summary>
        /// 创建设像头
        /// </summary>
        /// <param name="myVideoBrush">VideoBrush</param>
        public static void CreateVideoCapture(VideoBrush myVideoBrush)
        {
            //VideoCaptureDevice cam = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
            ////创建视频捕获源对象 
            //CaptureSource videoSource = new CaptureSource();
            ////获取用户启用本机摄像头的许可 
            //if (CaptureDeviceConfiguration.RequestDeviceAccess())
            //{
            //    //设置视频设备 
            //    videoSource.VideoCaptureDevice = cam;
            //    //设置视频来源 
            //    myVideoBrush.SetSource(videoSource);
            //    myVideoBrush.Stretch = Stretch.Fill;
            //    //启动摄像头 
            //    videoSource.Start();
            //} 
        }
        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="myBorder">VideoBrush存放在border</param>
        /// <returns>返回Image对像</returns>
        public static Image CropVideo(Border myBorder)
        {
            //创建可写入位图对象 
            WriteableBitmap wBitmap = new WriteableBitmap(myBorder, new MatrixTransform());
            //创建一个图像 
            Image img = new Image();
            img.Width = 100;
            img.Margin = new Thickness(2);
            //将wBitmap做为图像源 
            img.Source = wBitmap;
            //将图像添加到WrapPanel控件 
            return img;
        } 
    }
}
