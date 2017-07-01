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

namespace Media.comm
{
    public class CreateStoryBoard
    {
        public DoubleAnimationUsingKeyFrames doubleKeyFrames;
        public LinearDoubleKeyFrame linearDoubleKeyFrame;
        /// <summary>
        /// 创建动画
        /// </summary>
        /// <param name="hours">hours</param>
        /// <param name="min">min</param>
        /// <param name="sec">sec</param>
        /// <param name="speedRatio">速度</param>
        /// <returns></returns>
        public Storyboard CreateDoubleStoryBoard(int hours,int min,int sec,int speedRatio)
        {
            Storyboard sb = new Storyboard();
            doubleKeyFrames =new DoubleAnimationUsingKeyFrames ();
            linearDoubleKeyFrame=new LinearDoubleKeyFrame ();
            Duration duration = new Duration(new TimeSpan(hours, min, sec));
            KeyTime keyTime = new KeyTime();
            keyTime= duration.TimeSpan ;
            linearDoubleKeyFrame.KeyTime = keyTime;
            doubleKeyFrames.KeyFrames.Add(linearDoubleKeyFrame);
            sb.SpeedRatio = speedRatio;
            sb.Duration = duration;
            doubleKeyFrames.Duration = duration;
            sb.Children.Add(doubleKeyFrames);
            return sb;
        }
    }
}
