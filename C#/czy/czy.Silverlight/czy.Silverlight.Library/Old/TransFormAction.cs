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

namespace FromDemo.comm
{
    public class TransFormAction
    {
        #region  设定TranslateTransform
        /// <summary>
        /// 设定TranslateTransform的X座标动画
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">TranslateTransform</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        ///  <param name="x">x</param>
        public void SetTranslateTransformX(LinearDoubleKeyFrame ldf, TranslateTransform targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams,double x)
        {  
            
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(TranslateTransform.XProperty));
            ldf.Value = x;
        
        }
        /// <summary>
        /// 设定TranslateTransform的Y座标动画
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">TranslateTransform</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SetTranslateTransformY(LinearDoubleKeyFrame ldf, TranslateTransform targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams,double y)
        {  
            
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(TranslateTransform.YProperty));
            ldf.Value = y;

        }
        #endregion

        #region  设定ScaleTransform
        /// <summary>
        /// 设定ScaleTransformX
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">ScaleTransform</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SetScaleTransformX(LinearDoubleKeyFrame ldf, ScaleTransform targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams,double x)
        {
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(ScaleTransform.ScaleXProperty));
            ldf.Value = x;
        }
        /// <summary>
        /// 设定ScaleTransformY
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">ScaleTransform</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SetScaleTransformY(LinearDoubleKeyFrame ldf, ScaleTransform targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams,double  y)
        {
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(ScaleTransform.ScaleYProperty));
            ldf.Value = y;
        }
        /// <summary>
        /// 设定ScaleTransformCenterX
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">ScaleTransform</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SetScaleTransformCenterX(LinearDoubleKeyFrame ldf, ScaleTransform targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams,double centerX)
        {
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(ScaleTransform.CenterXProperty));
            ldf.Value = centerX;
        }
        /// <summary>
        /// 设定ScaleTransformCenterY
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">ScaleTransform</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        public void SetScaleTransformCenterY(LinearDoubleKeyFrame ldf, ScaleTransform targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams,double centerY)
        {
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(ScaleTransform.CenterYProperty));
            ldf.Value = centerY;
        }
        #endregion
    }
}
