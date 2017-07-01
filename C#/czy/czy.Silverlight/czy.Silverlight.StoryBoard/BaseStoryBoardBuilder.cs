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

namespace czy.Silverlight.StoryBoard
{
    public class BaseStoryBoardBuilder
    {
        /// <summary>
        /// 放大缩小Scale
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="offset"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static BaseStotyBoard ZoomSizeByScale(DependencyObject obj, double offset, double end)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames(obj, ScaleTransform.ScaleXProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), offset, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.3), end, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
           // BaseKeyFrames framesXC = new BaseKeyFrames(obj, ScaleTransform.CenterXProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
           // framesXC.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), ((ScaleTransform)obj).CenterX, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            //framesXC.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.3), ((ScaleTransform)obj).CenterX, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
         
            BaseKeyFrames framesY = new BaseKeyFrames(obj, ScaleTransform.ScaleYProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), offset, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.3), end, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            //BaseKeyFrames framesYC = new BaseKeyFrames(obj, ScaleTransform.CenterYProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            //framesYC.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), ((ScaleTransform)obj).CenterY, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            //framesYC.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.3), ((ScaleTransform)obj).CenterY, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            
            bsb.Add(framesX);
            bsb.Add(framesY);
            //bsb.Add(framesXC);
            //bsb.Add(framesYC);
            return bsb;
        }

        
        public static BaseStotyBoard PlaneProjectionStoryBoard(DependencyObject obj, Library.ProjectionPoint offset, Library.ProjectionPoint end)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames(obj, PlaneProjection.RotationXProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), offset.X, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), end.X, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
   
            BaseKeyFrames framesY = new BaseKeyFrames(obj, PlaneProjection.RotationYProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), offset.Y, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), end.Y, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));


            BaseKeyFrames framesZ = new BaseKeyFrames(obj, PlaneProjection.RotationZProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesZ.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), offset.Z, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesZ.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), end.Z, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
   
            bsb.Add(framesX);
            bsb.Add(framesY);
            bsb.Add(framesZ);
            return bsb;
        }
        
        public static BaseStotyBoard MoveStoryBoardByCanvs(DependencyObject obj,Point end)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames(obj, Canvas.LeftProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.1), end.X, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Point));

            BaseKeyFrames framesY = new BaseKeyFrames(obj, Canvas.TopProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), end.Y, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));

            bsb.Add(framesX);
            bsb.Add(framesY);
            return bsb;
        }

        public static BaseStotyBoard MoveStoryBoardByTrans(DependencyObject obj, Point end)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames(obj, TranslateTransform.XProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.1), end.X, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Point));

            BaseKeyFrames framesY = new BaseKeyFrames(obj, TranslateTransform.YProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.1), end.Y, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));

            bsb.Add(framesX);
            bsb.Add(framesY);
            return bsb;
        }

        public static BaseStotyBoard OpacityStoryBoard(DependencyObject obj, double end)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames(obj, FrameworkElement.OpacityProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.1), end, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));

            bsb.Add(framesX);
     ;
            return bsb;
        }
    }
}
