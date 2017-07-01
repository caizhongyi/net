using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace czy.MyClass.DrawImage
{
    /// <summary>
    /// 绘画直线
    /// </summary>
    public class DrawLine
    {
        #region 成员
        System.Drawing.Graphics g;
         int  x1, y1, x2, y2 ;
         int flag;
        #endregion

        #region 鼠标事件
         /// <summary>
         /// 鼠标移动
         /// </summary>
         /// <param name="e">MouseEventArgs</param>
         /// <param name="PictureBox1">PictureBox</param>
        public void MouseMoveEvent(System.Windows.Forms.MouseEventArgs e,PictureBox PictureBox1)
        {
            
            if (flag == 0 )
            {return ;}
            else
            {
            System.Drawing.Pen pen1= new System.Drawing.Pen(Color.CadetBlue);
            x2 = e.X;
            y2 = e.Y;
            g = PictureBox1.CreateGraphics();
            g.DrawLine(pen1, x1, y1, x2, y2);
            x1 = x2;
            y1 = y2;
            }
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        public void MouseDownEvent(System.Windows.Forms.MouseEventArgs e)
        {
             flag = 1;
             x1 = e.X;
             y1 = e.Y;
        }
        /// <summary>
        /// 鼠标放开
        /// </summary>
        public void MouseUpEvent()
        {
            flag = 0;
        }
        #endregion
     
    }
}
