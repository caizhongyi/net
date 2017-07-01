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
using System.Collections.Generic;
using System.Windows.Threading;


namespace Media
{
    public class ImagesOperation
    {
        private static int NUMBER_OF_IMAGE = 6;		    // number of images to be loaded from the folder (make sure it has enough images
        //private static String CLASS_PREFIX = "Logo";	// the class name prefix of the images
        private static double VERTICAL_RADIUS = 90;
        private static double HORIZONTAL_RADIUS = 220;
        private static double SCALE_MIN = 0.5;			// the scale of the image at the back
        private static double SCALE_MAX = 1.5;			// the scale of the image on the front
        //private static double SPEED = 4;				// the speed of the rotation

        //private DispatcherTimer _timer;
        //private int _fps = 25;
        private double _speedCounter = 0;
        private List<Image> _images = new List<Image>();
        #region 增加图片
        /// <summary>
        /// 增加图片
        /// </summary>
        /// <param name="imgNum">图片的数量</param>
        /// <param name="imgFolder">图片的跟目录</param>
        /// <param name="imgFormat">图片的格式.例如:.jpg</param>
        /// <param name="Canvas">图片加到canvas</param>
        public void  ImagesAdd(int imgNum,string imgFolder,string imgFormat,string imgName,Canvas canvas)
        {
            for (int i = 0; i < imgNum; i++)
            { 
                
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(imgFolder + "/" + imgName + i + imgFormat,UriKind.Relative));
                canvas.Children.Add(image);
            }
        }
        #endregion

        #region 创建图片
        /// <summary>
        /// 增加图片
        /// </summary>
        /// <param name="imgFolder">图片的跟目录</param>
        /// <param name="Canvas">图片加到canvas</param>
        public BitmapImage ImageSource(string imgFolder)
        {
         
                return  new BitmapImage(new Uri(imgFolder,UriKind.Relative));
             
        }
        #endregion

        #region 增加图片(无父级目录)
        /// <summary>
        /// 增加图片(无父级目录)
        /// </summary>
        /// <param name="imgNum">图片的数量</param>
        /// <param name="imgFormat">图片的格式.例如:.jpg</param>
        /// <param name="Canvas">图片加到canvas</param>
        public void ImagesAdd(int imgNum,string imgFormat, string imgName, Canvas canvas)
        {
            for (int i = 0; i < imgNum; i++)
            {

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(imgName + i + "imgFormat", UriKind.Relative));
                canvas.Children.Add(image);
            }
        }
        #endregion

        #region 用于放缩图像
        /// <summary>
        /// 用于放缩图像
        /// </summary>
        /// <param name="index">图像放缩大小比例( 1-6)</param>
        /// <param name="image">图像</param>
        public void ImageScaleTransform(int index,Image image)
        {
            double angle = (_speedCounter / 180 + (double)index / NUMBER_OF_IMAGE * 2) * Math.PI;
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = (SCALE_MAX + SCALE_MIN) / 2 + (SCALE_MAX - SCALE_MIN) / 2 * Math.Cos(angle);
            scaleTransform.ScaleY = scaleTransform.ScaleX;
            image.RenderTransform = scaleTransform;
        }

        #endregion

        #region  旋转动画函数
        // reposition the images according to their adding sequence
        /// <summary>
        /// 旋转动画函数
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="index">图片的index</param>
        public void posImage(Image image, int index)
        {
            // calculate the angle of the image
            double angle = (_speedCounter / 180 + (double)index / NUMBER_OF_IMAGE * 2) * Math.PI;

            // scale the image
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = (SCALE_MAX + SCALE_MIN) / 2 + (SCALE_MAX - SCALE_MIN) / 2 * Math.Cos(angle);
            scaleTransform.ScaleY = scaleTransform.ScaleX;
            image.RenderTransform = scaleTransform;


            // position the image
            image.SetValue(Canvas.LeftProperty, Math.Sin(angle) * HORIZONTAL_RADIUS - (double)image.GetValue(FrameworkElement.ActualWidthProperty) / 2 * scaleTransform.ScaleX);
            image.SetValue(Canvas.TopProperty, Math.Cos(angle) * VERTICAL_RADIUS - (double)image.GetValue(FrameworkElement.ActualHeightProperty) / 2 * scaleTransform.ScaleY);

            // sort the children according to their y position
            image.SetValue(Canvas.ZIndexProperty, (int)((double)image.GetValue(Canvas.TopProperty)));

        }
        #endregion

        #region 图像扭曲
        /// <summary>
        /// 图像扭曲
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="angleX">X座标扭曲度(以度为单位)</param>
        /// <param name="centerX">变换中心的X座标</param>
        /// <param name="angleY">Y座标扭曲度(以度为单位)</param>
        /// <param name="centerY">变换中心的Y座标</param>
        public void ImageSkewTransform(Image image, double angleX, double centerX,double angleY,double centerY)
        {
            
            SkewTransform skewTransform = new SkewTransform();
            skewTransform.AngleX = angleX;
            skewTransform.CenterX = centerX;
            skewTransform.AngleY = angleY;
            skewTransform.CenterY = centerY;
            image.RenderTransform = skewTransform;
        }
        #endregion

        #region 图像旋转
        /// <summary>
        /// 图像旋转
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="angle">顺时旋转度数</param>
        public void ImageRoateTranform(Image image, double angle)
        {
            RotateTransform roate = new RotateTransform();
            roate.Angle = angle;
            image.RenderTransform = roate;
        }
        #endregion

        #region 图像扭曲和旋转
        public SkewTransform skewTransform;
        public RotateTransform roate;
        /// <summary>
        /// 图像扭曲和旋转
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="angle">顺时旋转度数</param>
        /// <param name="angleX">X座标扭曲度(以度为单位)</param>
        /// <param name="centerX">变换中心的X座标</param>
        /// <param name="angleY">Y座标扭曲度(以度为单位)</param>
        /// <param name="centerY">变换中心的Y座标</param>
        public void ImageRoateAndSkew(Image image, double angle, double angleX, double centerX, double angleY, double centerY)
        {
            TransformGroup transformGroup = new TransformGroup();
            //旋转
            roate = new RotateTransform();
            roate.Angle = angle;
            transformGroup.Children.Add(roate);
            //扭曲
            skewTransform = new SkewTransform();
            skewTransform.AngleX = angleX;
            skewTransform.CenterX = centerX;
            skewTransform.AngleY = angleY;
            skewTransform.CenterY = centerY;
            transformGroup.Children.Add(skewTransform);
            image.RenderTransform = transformGroup;
        }
        #endregion

        #region 倒影效果
        /// <summary>
        /// 倒影效果
        /// </summary>
        /// <param name="imageReflection">倒影的图片</param>
        /// <param name="x">图片的X座标</param>
        /// <param name="y">图片的Y座标</param>
        /// <param name="angle">顺时旋转度数</param>
        /// <param name="angleX">X座标扭曲度(以度为单位)</param>
        /// <param name="centerX">变换中心的X座标</param>
        /// <param name="angleY">Y座标扭曲度(以度为单位)</param>
        /// <param name="centerY">变换中心的Y座标</param>
        public void ImageReflection(Image imageReflection, double angle, double angleX, double centerX, double angleY, double centerY)
        {
         
            
            //image的变图像变换
            TransformGroup tranformGroup = new TransformGroup();
            //旋转
            RotateTransform roate = new RotateTransform();
            roate.Angle = angle;
            tranformGroup.Children.Add(roate);
            //扭曲
            SkewTransform  skewTransform = new SkewTransform();
            skewTransform.AngleX = angleX;
            skewTransform.CenterX = centerX;
            skewTransform.AngleY = angleY;
            skewTransform.CenterY = centerY;
            tranformGroup.Children.Add(skewTransform);
            //旋转180度
            RotateTransform roate1 = new RotateTransform();
            roate1.Angle = 180;
            tranformGroup.Children.Add(roate1);

            imageReflection.RenderTransform = tranformGroup;
          
            //渐变
            LinearGradientBrush linGbrush = new LinearGradientBrush();
            linGbrush.StartPoint = new Point (0,0);
            linGbrush.EndPoint = new Point(0, 0.9);

            //渐变集合
            GradientStopCollection gradientCollection=new GradientStopCollection ();
            GradientStop gradientStop = new GradientStop() ;
            gradientStop.Color = Color .FromArgb(0,0,0,0);
            gradientStop.Offset =0;
            gradientCollection.Add(gradientStop);
            GradientStop gradientStop1 = new GradientStop();
            gradientStop1.Offset =3;
            gradientStop1.Color = Color.FromArgb(255,255,255,255);
            gradientCollection.Add(gradientStop1);
            //MessageBox.Show(gradientCollection.Count.ToString ());
          
            linGbrush.GradientStops = gradientCollection;
            //linGbrush.Opacity = 0.4;
            imageReflection.OpacityMask = linGbrush;
        
        }
        #endregion


    }
}
