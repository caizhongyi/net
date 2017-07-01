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
    public class MyTransFrom
    {
        /// <summary>
        /// 获取一个图片放缩对像
        /// </summary>
        /// <param name="x">x放缩比例</param>
        /// <param name="y">y放缩比例</param>
        /// <param name="centerX">放缩X中心</param>
        /// <param name="centerY">放缩Y中心</param>
        /// <returns></returns>
        public ScaleTransform GetScaleTransfrom(double x, double y, double centerX, double centerY)
        {
            ScaleTransform scale = new ScaleTransform();
            scale.CenterX = centerX;
            scale.CenterY = centerY;
            scale.ScaleX = x;
            scale.ScaleY = y;
            return scale;
        }

        /// <summary>
        /// 获取一个图片放缩旋转对像
        /// </summary>
        /// <param name="x">x放缩比例</param>
        /// <param name="y">y放缩比例</param>
        /// <param name="centerX">放缩X中心</param>
        /// <param name="centerY">放缩Y中心</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="rCenterX">旋转X座中心点</param>
        /// <param name="rCenterY">旋转Y座中心点</param>
        /// <returns></returns>
        public TransformGroup GetScaleAndRoate(double x, double y, double centerX, double centerY,double angle,double rCenterX,double rCenterY)
        {
            TransformGroup transFormGroup = new TransformGroup();
            ScaleTransform scale = new ScaleTransform();
            scale.CenterX = centerX;
            scale.CenterY = centerY;
            scale.ScaleX = x;
            scale.ScaleY = y;
            RotateTransform roate = new RotateTransform();
            roate.Angle = angle;
            roate.CenterX = rCenterX;
            roate.CenterY = rCenterY;
            transFormGroup.Children.Add(scale);
            transFormGroup.Children.Add(roate);
            return transFormGroup;
        }
    }
}
