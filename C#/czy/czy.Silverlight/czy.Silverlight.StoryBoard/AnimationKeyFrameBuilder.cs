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
    public class AnimationKeyFrameBuilder
    {
        public static EasingDoubleKeyFrame GetEasingDoubleKeyFrame(EasingKeyFrameProperty property)
        {
            EasingDoubleKeyFrame keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = property.KeyTime;
            keyFrame.Value = property.Value; 
            return keyFrame;
        }
        public static LinearDoubleKeyFrame GetLinearDoubleKeyFrame(LinearKeyFrameProperty property)
        {
            LinearDoubleKeyFrame keyFrame = new LinearDoubleKeyFrame();
            keyFrame.KeyTime = property.KeyTime;
            keyFrame.Value = property.Value;
            return keyFrame;
        }
        public static SplineDoubleKeyFrame GetSplineDoubleKeyFrame(SplineKeyFrameProperty property)
        {
            SplineDoubleKeyFrame keyFrame = new SplineDoubleKeyFrame();
            keyFrame.KeyTime = property.KeyTime;
            keyFrame.Value = property.Value;
            keyFrame.KeySpline = property.KeySpline;
            return keyFrame;
        }
        public static KeySpline GetKeySpline(KeySplinePorperty keySplinePorperty)
        {
            KeySpline keySpline = new KeySpline();
            keySpline.ControlPoint1 = keySplinePorperty.ControlPoint1;
            keySpline.ControlPoint2 = keySplinePorperty.ControlPoint2;
            return keySpline;
        }
    }
    public class EasingKeyFrameProperty
    {
        public double Value { get; set; }
        public KeyTime KeyTime { get; set; }
        public EasingKeyFrameProperty(double value, KeyTime keyTime)
        {
            Value = value;
            KeyTime = keyTime;
        }

    }
    public class LinearKeyFrameProperty
    {
        public double Value { get; set; }
        public KeyTime KeyTime { get; set; }

        public LinearKeyFrameProperty(double value, KeyTime keyTime)
        {
            Value = value;
            KeyTime = keyTime;
        }
    }
    public class SplineKeyFrameProperty
    {
        public double Value { get; set; }
        public KeyTime KeyTime { get; set; }
        public KeySpline KeySpline { get; set; }

        public SplineKeyFrameProperty(double value, KeyTime keyTime, KeySpline keySpline)
        {
            Value = value;
            KeyTime = keyTime;
            KeySpline = keySpline;
        }
    }
    public class KeySplinePorperty
    {
        public Point ControlPoint1 { get; set; }
        public Point ControlPoint2 { get; set; }
        public KeySplinePorperty(Point controlPoint1, Point controlPoint2)
        {
            ControlPoint1 = controlPoint1;
            ControlPoint2 = controlPoint2;
        }
    }


}
