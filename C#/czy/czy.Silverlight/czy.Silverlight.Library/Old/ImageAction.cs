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

namespace Media
{
    public class ImageAction
    {
        #region  X座标交换(TranslateTransform)
        /// <summary>
        /// X座标交换(TranslateTransform)
        /// </summary>
        /// <param name="tr1">TranslateTransform</param>
        /// <param name="tr2">TranslateTransform</param>
        /// <param name="ldf1">LinearDoubleKeyFrame</param>
        /// <param name="ldf2">LinearDoubleKeyFrame</param>
        /// <param name="targetName">StoryBoard.TargetName</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void PointXExchange(TranslateTransform tr1,TranslateTransform tr2, LinearDoubleKeyFrame ldf1,LinearDoubleKeyFrame ldf2,string targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {   
            Point point1 = new Point();
            Point point2 = new Point();
            point1.X = tr1.X;
            point2.X = tr2.X;        
            Storyboard .SetTargetName(doubledKeyFrams,targetName );
            Storyboard .SetTargetProperty(doubledKeyFrams ,new PropertyPath (TranslateTransform .XProperty) );
            ldf1.Value = point2.X;
            ldf2.Value = point1.X;

        }
        #endregion

        #region  Y座标交换(TranslateTransform)
        /// <summary>
        ///  Y座标交换(TranslateTransform)
        /// </summary>
        /// <param name="tr1">TranslateTransform</param>
        /// <param name="tr2">TranslateTransform</param>
        /// <param name="ldf1">LinearDoubleKeyFrame</param>
        /// <param name="ldf2">LinearDoubleKeyFrame</param>
        /// <param name="targetName">StoryBoard.TargetName</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void PointYExchange(TranslateTransform tr1, TranslateTransform tr2, LinearDoubleKeyFrame ldf1, LinearDoubleKeyFrame ldf2, string targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            Point point1 = new Point();
            Point point2 = new Point();
            point1.Y = tr1.Y;
            point2.Y = tr2.Y;
            Storyboard.SetTargetName(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(TranslateTransform.YProperty));
            ldf1.Value = point2.Y;
            ldf2.Value = point1.Y;

        }

        #endregion

        #region 图片宽度座标交换
        /// <summary>
        /// 图片宽度座标交换
        /// </summary>
        /// <param name="img1">img1</param>
        /// <param name="img2">img2</param>
        /// <param name="ldf1">LinearDoubleKeyFrame</param>
        /// <param name="ldf2">LinearDoubleKeyFrame</param>
        /// <param name="targetName">StoryBoard.TargetName</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SizeWidthExchange(Image  img1, Image img2, LinearDoubleKeyFrame ldf1, LinearDoubleKeyFrame ldf2, string targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            double width1 = img1.Width;
            double width2 = img2.Width;
            Storyboard.SetTargetName(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Image .WidthProperty));
            ldf1.Value = width2;
            ldf2.Value = width1;
        }
        #endregion

        #region 图片高度座标交换
        /// <summary>
        /// 图片高度座标交换
        /// </summary>
        /// <param name="img1">img1</param>
        /// <param name="img2">img2</param>
        /// <param name="ldf1">LinearDoubleKeyFrame</param>
        /// <param name="ldf2">LinearDoubleKeyFrame</param>
        /// <param name="targetName">StoryBoard.TargetName</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SizeHeightExchange(Image img1, Image img2, LinearDoubleKeyFrame ldf1, LinearDoubleKeyFrame ldf2, string targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            double height1 = img1.Height;
            double height2 = img2.Height;
            Storyboard.SetTargetName(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Image.WidthProperty));
            ldf1.Value = height2;
            ldf2.Value = height1;
        }
        #endregion

        #region 图像的旋转动画
        /// <summary>
        /// 图像的旋转动画
        /// </summary>
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageRotateTransform(LinearDoubleKeyFrame ldf, double value, RotateTransform target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
          
        
            Storyboard.SetTarget(doubledKeyFrams, target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(RotateTransform.AngleProperty));
           
            ldf.Value = value;

        }
        #endregion

        #region 图像的X座标移动
        /// <summary>
        /// 图像的X座标移动
        /// </summary>
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageTranslateTransformX(LinearDoubleKeyFrame ldf, double value, TranslateTransform target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {


            Storyboard.SetTarget(doubledKeyFrams, target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(TranslateTransform.XProperty));

            ldf.Value = value;

        }
        #endregion

        #region 图像Y座标的扭曲动画
        /// <summary>
        /// 图像Y座标的扭曲动画
        /// </summary>
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageSkewTransformY(LinearDoubleKeyFrame ldf, double value, SkewTransform target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            Storyboard.SetTarget(doubledKeyFrams,target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(SkewTransform.AngleYProperty));
            ldf.Value = value;

        }
        #endregion

        #region 图像Y座标的扭曲动画
        /// <summary>
        /// 图像Y座标的扭曲动画
        /// </summary>
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageSkewTransformX(LinearDoubleKeyFrame ldf, double value, SkewTransform target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            Storyboard.SetTarget(doubledKeyFrams, target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(SkewTransform.AngleXProperty));
            ldf.Value = value;

        }
        #endregion

        #region 图像宽度变化动画
        /// <summary>
        /// 图像宽度变化动画
        /// </summary>  
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageWithAction(LinearDoubleKeyFrame ldf, double value, Image target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            Storyboard.SetTarget(doubledKeyFrams, target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Image .WidthProperty));
            ldf.Value = value;

        }

        #endregion

        #region 图像高度变化动画
        /// <summary>
        /// 图像高度变化动画
        /// </summary>
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageHeightAction(LinearDoubleKeyFrame ldf, double value, Image target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            Storyboard.SetTarget(doubledKeyFrams, target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Image.HeightProperty));
            ldf.Value = value;

        }
        #endregion

        #region 图像平移变化动画(canvasleft属性)
        /// <summary>
        /// 图像平移变化动画(canvasleft属性)
        /// </summary>
        /// <param name="ldf1">LinearDoubleKeyFrame对像</param>
        /// <param name="value">LinearDoubleKeyFrame值</param>
        /// <param name="targetName">属性值</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames对像</param>
        public void ImageMoveActionByCanvasleft(LinearDoubleKeyFrame ldf, double value, Image target, DoubleAnimationUsingKeyFrames doubledKeyFrams)
        {
            Storyboard.SetTarget(doubledKeyFrams, target);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Canvas.LeftProperty));
            ldf.Value = value;

        }
        #endregion
    }
}
