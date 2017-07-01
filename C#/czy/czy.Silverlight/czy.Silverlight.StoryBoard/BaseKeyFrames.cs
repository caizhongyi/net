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
    public class BaseKeyFrames
    {
        
        Timeline _keyFrames;

        public Timeline KeyFrames
        {
            get { return _keyFrames; }
            set { _keyFrames = value; }
        }

        DoubleAnimationUsingKeyFrames _doubleFrames;
        PointAnimationUsingKeyFrames _pointFrames;
        ColorAnimationUsingKeyFrames _colorFrames;

        public BaseKeyFrames(DependencyObject o, DependencyProperty p, TimeSpan beginTime,BaseKeyFramesType.Type type)
        {
            switch (type)
            {
                case BaseKeyFramesType.Type.Double: _doubleFrames = new DoubleAnimationUsingKeyFrames();
                    _keyFrames = _doubleFrames;
                    Storyboard.SetTarget(_doubleFrames, o);
                    Storyboard.SetTargetProperty(_doubleFrames, new PropertyPath(p));
                    break;
                case BaseKeyFramesType.Type.Point:
                    _pointFrames = new PointAnimationUsingKeyFrames();
                    Storyboard.SetTarget(_pointFrames, o);
                    Storyboard.SetTargetProperty(_pointFrames, new PropertyPath(p));
                    _keyFrames = _pointFrames;
                    break;
                case BaseKeyFramesType.Type.Color:
                    _colorFrames = new ColorAnimationUsingKeyFrames();
                    Storyboard.SetTarget(_colorFrames, o);
                    Storyboard.SetTargetProperty(_colorFrames, new PropertyPath(p));
                    _keyFrames = _colorFrames;
                    break;
            }
        }
        public void Add(BaseKeyFrame keyframe, bool _autoReverse)
        {
            if (_doubleFrames != null)
            {
                _doubleFrames.AutoReverse = _autoReverse;
                _doubleFrames.KeyFrames.Add((DoubleKeyFrame)keyframe.KeyFrame);
            }
            if (_pointFrames != null)
            {
                _pointFrames.AutoReverse = _autoReverse;
                _pointFrames.KeyFrames.Add((PointKeyFrame)keyframe.KeyFrame);
            }
            if (_colorFrames != null)
            {
                _colorFrames.AutoReverse = _autoReverse;
                _colorFrames.KeyFrames.Add((ColorKeyFrame)keyframe.KeyFrame);
            }
        }
        public void Add(BaseKeyFrame keyframe)
        {
            if (_doubleFrames != null)
            {
                _doubleFrames.KeyFrames.Add((DoubleKeyFrame)keyframe.KeyFrame);
            }
            if (_pointFrames != null)
            {
                _pointFrames.KeyFrames.Add((PointKeyFrame)keyframe.KeyFrame);
            }
            if (_colorFrames != null)
            {
                _colorFrames.KeyFrames.Add((ColorKeyFrame)keyframe.KeyFrame);
            }
        }
    }
}
