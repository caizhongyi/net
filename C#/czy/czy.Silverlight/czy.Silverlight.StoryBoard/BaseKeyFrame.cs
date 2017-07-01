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
    public class BaseKeyFrame
    {

        object _keyFrame;

        public object KeyFrame
        {
            get { return _keyFrame; }
            set { _keyFrame = value; }
        }

        //object _value;

        //public object Value
        //{
        //    get { return _value; }
        //    set { _value = value; }
        //}
        //KeyTime _keyTime;

        //public KeyTime KeyTime
        //{
        //    get { return _keyTime; }
        //    set { _keyTime = value; }
        //}

        public enum KeyFrameType
        {
            Easing,
            Linear,
            Spline,
            Discrete

        }
        public BaseKeyFrame(KeyTime keyTime, object value, KeyFrameType ktype, BaseKeyFramesType.Type type)
        {
            switch (type)
            {
                case BaseKeyFramesType.Type.Double:
                    CreateDoubleKeyFrame(ktype, keyTime, value);
                    break;
                case BaseKeyFramesType.Type.Point:
                    CreatePointKeyFrame(ktype, keyTime, value);
                    break;
                case BaseKeyFramesType.Type.Color:
                    CreateColorKeyFrame(ktype, keyTime, value);
                    break;
                default: CreateDoubleKeyFrame(ktype, keyTime, value);
                    break;
            }


        }

        private void CreatePointKeyFrame(KeyFrameType ktype, KeyTime keyTime, object value)
        {
            switch (ktype)
            {
                case BaseKeyFrame.KeyFrameType.Easing:
                    EasingPointKeyFrame e = new EasingPointKeyFrame();
                    e.KeyTime = keyTime;
                    e.Value = (Point)(value);
                    _keyFrame = e;
                    break;
                case BaseKeyFrame.KeyFrameType.Linear:
                    LinearPointKeyFrame l = new LinearPointKeyFrame();
                    l.KeyTime = keyTime;
                    l.Value = (Point)(value);
                    _keyFrame = l;
                    break;
                case BaseKeyFrame.KeyFrameType.Spline: SplinePointKeyFrame s = new SplinePointKeyFrame(); break;
                case BaseKeyFrame.KeyFrameType.Discrete: DiscretePointKeyFrame d = new DiscretePointKeyFrame(); break;
                default: break;
            }
        }
        private void CreateColorKeyFrame(KeyFrameType ktype, KeyTime keyTime, object value)
        {
            switch (ktype)
            {
                case BaseKeyFrame.KeyFrameType.Easing:
                    EasingColorKeyFrame e = new EasingColorKeyFrame();
                    e.KeyTime = keyTime;
                    e.Value = (Color)value;
                    _keyFrame = e;
                    break;
                case BaseKeyFrame.KeyFrameType.Linear:
                    LinearColorKeyFrame l = new LinearColorKeyFrame();
                    l.KeyTime = keyTime;
                    l.Value = (Color)(value);
                    _keyFrame = l;
                    break;
                case BaseKeyFrame.KeyFrameType.Spline: SplineColorKeyFrame s = new SplineColorKeyFrame(); break;
                case BaseKeyFrame.KeyFrameType.Discrete: DiscreteColorKeyFrame d = new DiscreteColorKeyFrame(); break;
                default: break;
            }
        }
        private void CreateDoubleKeyFrame(KeyFrameType ktype, KeyTime keyTime, object value)
        {
            switch (ktype)
            {
                case BaseKeyFrame.KeyFrameType.Easing:
                    EasingDoubleKeyFrame e = new EasingDoubleKeyFrame();
                    e.KeyTime = keyTime;
                    e.Value = Convert.ToDouble(value);
                    _keyFrame = e;
                    break;
                case BaseKeyFrame.KeyFrameType.Linear:
                    LinearDoubleKeyFrame l = new LinearDoubleKeyFrame();
                    l.KeyTime = keyTime;
                    l.Value = Convert.ToDouble(value);
                    _keyFrame = l;
                    break;
                case BaseKeyFrame.KeyFrameType.Spline: SplineDoubleKeyFrame s = new SplineDoubleKeyFrame(); break;
                case BaseKeyFrame.KeyFrameType.Discrete: DiscreteDoubleKeyFrame d = new DiscreteDoubleKeyFrame(); break;
                default: break;
            }
        }

    }
}
