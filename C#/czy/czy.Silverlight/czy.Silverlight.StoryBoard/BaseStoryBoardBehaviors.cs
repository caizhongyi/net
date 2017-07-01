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

namespace czy.Silverlight.StoryBoard
{
    public class BaseStoryBoardBehaviors
    {

        #region 横向旋转
        public static BaseStotyBoard PlaneProjectionStoryBoard(DependencyObject obj)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames((obj as UIElement).Projection, PlaneProjection.RotationXProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            BaseKeyFrames framesY = new BaseKeyFrames((obj as UIElement).Projection, PlaneProjection.RotationYProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.1), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), 90, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), -90, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            BaseKeyFrames framesZ = new BaseKeyFrames((obj as UIElement).Projection, PlaneProjection.RotationZProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesZ.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
           // BaseKeyFrames framesColor = new BaseKeyFrames((obj as FrameworkElement), PlaneProjection.RotationYProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Color);
            bsb.Add(framesX);
            bsb.Add(framesY);
            bsb.Add(framesZ);
            //bsb.Add(framesColor);
            return bsb;
        }
        public static BaseStotyBoard RePlaneProjectionStoryBoard(DependencyObject obj)
        {
            BaseStotyBoard bsb = new BaseStotyBoard();
            BaseKeyFrames framesX = new BaseKeyFrames((obj as UIElement).Projection, PlaneProjection.RotationXProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesX.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            BaseKeyFrames framesY = new BaseKeyFrames((obj as UIElement).Projection, PlaneProjection.RotationYProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.1), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), -90, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.2), 90, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            framesY.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));
            BaseKeyFrames framesZ = new BaseKeyFrames((obj as UIElement).Projection, PlaneProjection.RotationZProperty, TimeSpan.Zero, BaseKeyFramesType.Type.Double);
            framesZ.Add(new BaseKeyFrame(TimeSpan.FromSeconds(0.4), 0, BaseKeyFrame.KeyFrameType.Easing, BaseKeyFramesType.Type.Double));

            bsb.Add(framesX);
            bsb.Add(framesY);
            bsb.Add(framesZ);
            return bsb;
        }
        #endregion
    }
}
