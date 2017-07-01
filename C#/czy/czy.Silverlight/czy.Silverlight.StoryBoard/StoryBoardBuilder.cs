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
using System.Collections.Generic;
using czy.Silverlight.Library;


namespace czy.Silverlight.StoryBoard
{
    public class StoryBoardBuilder
    {
    
        /// <summary>
        /// [Width][Height]放缩,位移 基于Canvas定位 
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <param name="startSize">开始大小</param>
        /// <param name="endSize">结束大小</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetZoomAndMoveStoryBoardByCanvas(DependencyObject obj, Point start, Point end, Size startSize, Size endSize)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startSize.Height, endSize.Height, obj, FrameworkElement.HeightProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startSize.Width, endSize.Width, obj, FrameworkElement.WidthProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.X, end.X, obj, Canvas.LeftProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.Y, end.Y, obj, Canvas.TopProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.4);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.4);
            sb.Children[2].Duration = TimeSpan.FromSeconds(0.2);
            sb.Children[3].Duration = TimeSpan.FromSeconds(0.2);
            //sb.SpeedRatio = 2;
            return sb;
        }

        /// <summary>
        /// [Width][Height]放缩,位移 基于Canvas定位 
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <param name="startSize">开始大小</param>
        /// <param name="offsetKeySize">中间偏移量</param>
        /// <param name="endSize">结束大小</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetZoomAndMoveStoryBoardByCanvas(DependencyObject obj, Point startKeyPoint, Point endKeyPoint, Size startKeySize, Size offsetKeySize, Size endKeySize)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimationUsingKeyFrames keyFramesX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames keyFramesY = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames keyFramesW = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames keyFramesH = new DoubleAnimationUsingKeyFrames();

            keyFramesX.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(startKeyPoint.X, TimeSpan.FromSeconds(0))));
            keyFramesX.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(endKeyPoint.X, TimeSpan.FromSeconds(0.4))));
            keyFramesY.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(startKeyPoint.Y, TimeSpan.FromSeconds(0))));
            keyFramesY.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(endKeyPoint.Y, TimeSpan.FromSeconds(0.4))));

            keyFramesW.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(startKeySize.Width , TimeSpan.FromSeconds(0))));
            keyFramesW.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(offsetKeySize.Width, TimeSpan.FromSeconds(0.1))));
            keyFramesW.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(endKeySize.Width, TimeSpan.FromSeconds(0.4))));
            keyFramesH.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(startKeySize.Height, TimeSpan.FromSeconds(0))));
            keyFramesH.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(offsetKeySize.Height, TimeSpan.FromSeconds(0.1))));
            keyFramesH.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(endKeySize.Height, TimeSpan.FromSeconds(0.4))));


            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, Canvas.LeftProperty, keyFramesX)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, Canvas.TopProperty, keyFramesY)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, FrameworkElement.WidthProperty, keyFramesW)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, FrameworkElement.HeightProperty, keyFramesH)));
            return sb;
        }


        /// <summary>
        /// 放缩基于Scale
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <param name="startSize">开始大小</param>
        /// <param name="endSize">结束大小</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetZoomStoryBoardByScale(DependencyObject obj,double start,double end)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start, end, obj, ScaleTransform.ScaleXProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start, end, obj, ScaleTransform.ScaleYProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.2);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.2);
            //sb.SpeedRatio = 2;
            return sb;
        }
        /// <summary>
        /// [Width][Height]放缩 
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="startSize">开始大小</param>
        /// <param name="offsetKeySize">中间偏移量</param>
        /// <param name="endSize">结束大小</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetZoomStoryBoard(DependencyObject obj,Size startKeySize, Size offsetKeySize, Size endKeySize)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimationUsingKeyFrames keyFramesW = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames keyFramesH = new DoubleAnimationUsingKeyFrames();

 
            keyFramesW.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(startKeySize.Width, TimeSpan.FromSeconds(0))));
            keyFramesW.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(offsetKeySize.Width, TimeSpan.FromSeconds(0.1))));
            keyFramesW.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(endKeySize.Width, TimeSpan.FromSeconds(0.2))));
            keyFramesH.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(startKeySize.Height, TimeSpan.FromSeconds(0))));
            keyFramesH.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(offsetKeySize.Height, TimeSpan.FromSeconds(0.1))));
            keyFramesH.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(endKeySize.Height, TimeSpan.FromSeconds(0.2))));

            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, FrameworkElement.WidthProperty, keyFramesW)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, FrameworkElement.HeightProperty, keyFramesH)));
            return sb;
        }

   
        /// <summary>
        /// BlurEffect 模糊特效
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="startRadius">开始位置</param>
        /// <param name="endRadius">结束位置</param>
        /// <param name="beginTime">开始时间</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetBlurEffectStoryBoard(DependencyObject obj,  double startRadius, double endRadius, TimeSpan beginTime)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startRadius, endRadius, obj, System.Windows.Media.Effects.BlurEffect.RadiusProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.5);
          //  sb.SpeedRatio = 2;
            sb.BeginTime = beginTime;
            return sb;
        }
        /// <summary>
        /// 位移 基于Canvas定位
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetMoveStoryBoardByCanvas(DependencyObject obj, Point start, Point end)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.X, end.X, obj, Canvas.LeftProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.Y, end.Y, obj, Canvas.TopProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.5);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.5);
            return sb;
        }
        /// <summary>
        /// 位移 基于TranslateTransform定位
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetMoveStoryBoardByTrans(DependencyObject obj, Point start, Point end)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.X, end.X, obj, TranslateTransform.XProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.Y, end.Y, obj, TranslateTransform.YProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.5);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.5);
            return sb;
        }
        /// <summary>
        /// 加速运动
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">KeyPoint</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetMoveStoryBoardByTrans(DependencyObject obj, Point KeyPoint)
        {
            Storyboard sb = new Storyboard();
         
            DoubleAnimationUsingKeyFrames keyFramesX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames keyFramesY = new DoubleAnimationUsingKeyFrames();

            keyFramesX.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(KeyPoint.X, TimeSpan.FromSeconds(0.5))));
            keyFramesY.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(KeyPoint.Y, TimeSpan.FromSeconds(0.5))));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, TranslateTransform.XProperty, keyFramesX)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, TranslateTransform.YProperty, keyFramesY)));
            return sb;
        }
     
        /// <summary>
        /// 透明度变化
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始透明度</param>
        /// <param name="end">结束透明度</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetOpactiyStoryBoard(DependencyObject obj, double start, double end)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start, end, obj, UIElement.OpacityProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.2);
            return sb;
        }
        /// <summary>
        /// 透明度变化
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始透明度</param>
        /// <param name="end">结束透明度</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetOpactiyStoryBoard(DependencyObject obj, double start, double offset,double end)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimationUsingKeyFrames keyFramesOpacity = new DoubleAnimationUsingKeyFrames();

            keyFramesOpacity.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(start, TimeSpan.FromSeconds(0.2))));
            keyFramesOpacity.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(offset, TimeSpan.FromSeconds(0.5))));
            keyFramesOpacity.KeyFrames.Add(AnimationKeyFrameBuilder.GetEasingDoubleKeyFrame(new EasingKeyFrameProperty(end, TimeSpan.FromSeconds(0.8))));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, obj, FrameworkElement.OpacityProperty, keyFramesOpacity)));

            return sb;
        }
        /// <summary>
        /// 透明度并大小变化
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始透明度</param>
        /// <param name="end">结束透明度</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetOpactiySizeStoryBoard(DependencyObject obj, double start, double end, Size startSize, Size endSize)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start, end, obj, FrameworkElement.OpacityProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startSize.Height, endSize.Height, obj, FrameworkElement.HeightProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startSize.Width, endSize.Width, obj, FrameworkElement.WidthProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.7);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.4);
            sb.Children[2].Duration = TimeSpan.FromSeconds(0.4);
            return sb;
        }
        /// <summary>
        /// 透明度并位置变化(Canvas)
        /// </summary>
        /// <param name="obj">依赖对像</param>
        /// <param name="start">开始透明度</param>
        /// <param name="end">结束透明度</param>
        /// <returns>Storyboard</returns>
        public static Storyboard GetOpactiyPointStoryBoard(DependencyObject obj, double start, double end, Point startPoint, Point endPoint)
        {
            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start, end, obj, FrameworkElement.OpacityProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startPoint.X, endPoint.X, obj, Canvas.LeftProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(startPoint.Y, endPoint.Y, obj, Canvas.TopProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.7);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.4);
            sb.Children[2].Duration = TimeSpan.FromSeconds(0.4);
            return sb;
        }

       /// <summary>
       /// 三维变换
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <returns></returns>
        public static Storyboard GetProjectionStoryBoard(DependencyObject obj, ProjectionPoint start, ProjectionPoint end)
        {

            Storyboard sb = new Storyboard();
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.X, end.X, obj, PlaneProjection.RotationXProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.Y, end.Y, obj, PlaneProjection.RotationYProperty)));
            sb.Children.Add(AnimationBuilder.GetDoubleAnimation(new DoubleAnimationProperty(start.Z, end.Z, obj, PlaneProjection.RotationZProperty)));
            sb.Children[0].Duration = TimeSpan.FromSeconds(0.5);
            sb.Children[1].Duration = TimeSpan.FromSeconds(0.5);
            sb.Children[2].Duration = TimeSpan.FromSeconds(0.5);
           
            return sb;
        }

        /// <summary>
        /// 加速运动
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static Storyboard GetSplineStoryBoard(DependencyObject obj,Point KeyPoint)
        {
            Storyboard sb = new Storyboard();
            KeySpline spline=new KeySpline();
            spline.ControlPoint1 = new Point(0.6, 0.0);
            spline.ControlPoint2 = new Point(0.9, 0.00);
            DoubleAnimationUsingKeyFrames keyFramesX=new DoubleAnimationUsingKeyFrames ();
            DoubleAnimationUsingKeyFrames keyFramesY = new DoubleAnimationUsingKeyFrames();

            keyFramesX.KeyFrames.Add(AnimationKeyFrameBuilder.GetSplineDoubleKeyFrame(new SplineKeyFrameProperty(KeyPoint.X, TimeSpan.FromSeconds(0.5), spline)));
            keyFramesY.KeyFrames.Add(AnimationKeyFrameBuilder.GetSplineDoubleKeyFrame(new SplineKeyFrameProperty(KeyPoint.Y, TimeSpan.FromSeconds(0.5), spline)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, TimeSpan.FromSeconds(0.5), obj, Canvas.LeftProperty, keyFramesX)));
            sb.Children.Add(AnimationKeyFramesBuilder.GetDoubleAnimationKeyFrames(new DoubleKeyFramesProperty(TimeSpan.Zero, TimeSpan.FromSeconds(0.5), obj, Canvas.TopProperty, keyFramesY)));
            return sb;
        }

   
    }
}
