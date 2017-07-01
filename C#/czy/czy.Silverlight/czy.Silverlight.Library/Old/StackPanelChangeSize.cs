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

namespace Media
{
    public class StackPanelChangeSize
    {
        public StackPanel sp;
        private double x, x1;//X坐标
        private double y, y1;//y坐标
        private bool flag;//判断鼠标左键是否按下true为按下,false为放开
        #region 鼠标移动
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                x1 = e.GetPosition(null).X;
                y1 = e.GetPosition(null).Y;
                sp.Width  += (x1 - x);
                sp.Height  += (y1 - y);
                x = x1;
                y = y1;
            }
        }

        #endregion

        #region 鼠标左键按下
        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            flag = true;
            x = e.GetPosition(null).X;
            y = e.GetPosition(null).Y;


        }
        #endregion

        #region 鼠标左键放开
        /// <summary>
        /// 鼠标左键放开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            flag = false;
        }
        #endregion
    }
}
