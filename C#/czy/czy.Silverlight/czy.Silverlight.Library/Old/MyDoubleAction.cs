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

namespace SilverlightClassLibrary
{
    public class MyDoubleAction
    {
    
     

        /// <summary>
        /// 获取一个DoubleAnimationUsingKeyFrames
        /// </summary>
        /// <returns>DoubleAnimationUsingKeyFrames</returns>
        public static DoubleAnimationUsingKeyFrames GetDoubleAnimationUsingKeyFrames()
        {
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            return doubleAnimationUsingKeyFrames;
        }

        /// <summary>
        /// 获取一个LinearDoubleKeyFrame
        /// </summary>
        /// <param name="hours">LinearDoubleKeyFrame在第几小时运动</param>
        /// <param name="min">LinearDoubleKeyFrame在第几分运动</param>
        /// <param name="sec">LinearDoubleKeyFrame在第几秒运动</param>
        /// <param name="value">运动的位置</param>
        /// <returns>LinearDoubleKeyFrame</returns>
        public static LinearDoubleKeyFrame GetLinearDoubleKeyFrame(int hours, int min, int sec,double value)
        {
            LinearDoubleKeyFrame linearDoubleKeyFarme = new LinearDoubleKeyFrame();
            linearDoubleKeyFarme.KeyTime = new Duration(new TimeSpan(hours, min, sec)).TimeSpan;
            linearDoubleKeyFarme.Value = value;
            return linearDoubleKeyFarme;
        }

        /// <summary>
        /// 设定动画的属性值
        /// </summary>
        /// <param name="ldf">DoubleAnimationUsingKeyFrames</param>
        /// <param name="doubledKeyFrams">LinearDoubleKeyFrame</param>
        /// <param name="targetName">属性的名称</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue(DoubleAnimationUsingKeyFrames doubledKeyFrams, string targetName, object property)
        {
            Storyboard.SetTargetName(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(property));
           
        }
    }
}
