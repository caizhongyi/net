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
    /// <summary>
    /// 侦集合
    /// </summary>
    public class AnimationKeyFramesBuilder
    {
       
        /// <summary>
        /// 获取DoubleAnimationUsingKeyFrames
        /// </summary>
        /// <param name="beginTime">动画开始时间</param>
        /// <param name="duration">动画动行时间</param>
        /// <param name="keyFrames"> </param>
        public static DoubleAnimationUsingKeyFrames GetDoubleAnimationKeyFrames(DoubleKeyFramesProperty property)
        {
            DoubleAnimationUsingKeyFrames _keyFrames = property.DoubleKeyFrames;
            _keyFrames.BeginTime = property.BeginTime;
            _keyFrames.Duration=property.Duration;
            //this.RegisterName("name",obj);
            Storyboard.SetTarget(_keyFrames, property.Obj);
            Storyboard.SetTargetProperty(_keyFrames, new PropertyPath(property.Property));
            return _keyFrames;
        }

        //public static PointAnimationUsingKeyFrames GetPointAnimationKeyFrames(AnimationKeyFramesProperty property)
        //{
        //    PointAnimationUsingKeyFrames _keyFrames = new PointAnimationUsingKeyFrames();
        //    return _keyFrames;
        //}
        //public static ColorAnimationUsingKeyFrames GetColorAnimationKeyFrames(AnimationKeyFramesProperty property)
        //{
        //    ColorAnimationUsingKeyFrames _keyFrames = new ColorAnimationUsingKeyFrames();
        //    return _keyFrames;
        //}
    }

    public class DoubleKeyFramesProperty
    {
        public TimeSpan BeginTime { get; set; }
        public Duration Duration { get; set; }
        public DependencyObject Obj { get; set; }
        public DependencyProperty Property { get; set; }
        /// <summary>
        /// [DiscreteDoubleKeyFrame][EasingDoubleKeyFrame][LinearDoubleKeyFrame][SplineDoubleKeyFrame]
        /// </summary>
        public DoubleAnimationUsingKeyFrames DoubleKeyFrames { get; set; }


        public DoubleKeyFramesProperty(TimeSpan beginTime, Duration duration, DependencyObject obj, DependencyProperty property, DoubleAnimationUsingKeyFrames doubleKeyFrames)
        {
            BeginTime=beginTime;
            Duration=duration;
            Obj=obj;
            Property=property;
            DoubleKeyFrames = doubleKeyFrames;
        }
        public DoubleKeyFramesProperty(TimeSpan beginTime,DependencyObject obj, DependencyProperty property, DoubleAnimationUsingKeyFrames doubleKeyFrames)
        {
            BeginTime = beginTime;
            Obj = obj;
            Property = property;
            DoubleKeyFrames = doubleKeyFrames;
        }
    }
   
}
