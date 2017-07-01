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
    public class AnimationBuilder
    {
        /// <summary>
        /// 获取DoubleAnimationUsingKeyFrames
        /// </summary>
        /// <param name="beginTime">动画开始时间</param>
        /// <param name="duration">动画动行时间</param>
        /// <param name="keyFrames"> </param>
        public static DoubleAnimation GetDoubleAnimation(DoubleAnimationProperty property)
        {
            DoubleAnimation _animation = new DoubleAnimation();
            _animation.BeginTime = property.BeginTime;
            _animation.Duration = property.Duration;
            _animation.FillBehavior = property.FillBehavior;
            _animation.From = property.From; 
            _animation.To = property.To;
            //this.RegisterName("name",obj);
            Storyboard.SetTarget(_animation, property.Obj);
            Storyboard.SetTargetProperty(_animation, new PropertyPath(property.Property));
            return _animation;
        }
    }

    public class DoubleAnimationProperty
    {
        public TimeSpan BeginTime { get; set; }
        public Duration Duration { get; set; }
        public DependencyObject Obj { get; set; }
        public DependencyProperty Property { get; set; }
        public FillBehavior FillBehavior { get; set; }
        public double From { get; set; }
        public double To { get; set; }

        public DoubleAnimationProperty(double from, double to, DependencyObject obj, DependencyProperty property)
        {
            From = from;
            To = to;
            Obj = obj;
            Property = property;
        }
        public DoubleAnimationProperty(double from, double to, DependencyObject obj, DependencyProperty property, TimeSpan beginTime, Duration duration)
        {
            From = from;
            To = to;
            Obj = obj;
            Property = property;

            BeginTime = beginTime;
            Duration = duration;
        }
    }
}
