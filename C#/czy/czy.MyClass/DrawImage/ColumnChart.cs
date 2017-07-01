using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace czy.MyClass.DrawImage
{
    public class DrawHelper
    {
        /**/
        /// <summary>
        ///渐变颜色的函数
        /// </summary>
        /// <returns>LinearGradientBrush 对象</returns>
        private LinearGradientBrush GetColor(int ylong)
        {
          

            LinearGradientBrush linGrBrush = new LinearGradientBrush(
            new Point(0, 0),
            new Point(0, ylong),
            Color.FromArgb(255, 255, 0, 0),   // Opaque red
            Color.FromArgb(255, 0, 0, 255));
            return linGrBrush;
        }
        /**/
        /// <summary>
        /// 刻度线生成函数
        /// </summary>
        /// <param name="objGraphics">Graphics对象</param>
        /// <param name="intxLong">图像x大小</param>
        /// <param name="intyLong">图像y大小</param>
        /// <param name="intxLeft">图像左边巨</param>
        /// <param name="intEnd">图像下边距</param>
        /// <param name="intyMax">刻度数</param>
        /// <param name="kdvalue">刻度值</param>
        /// <returns></returns>
        public Graphics DwLine(Graphics objGraphics, int intxLong, int intyLong, int intxLeft, int intEnd, int intyMax, float kdvalue, string title)
        {
            objGraphics.Clear(Color.White);
            int intyScale = intyLong / intyMax;
            objGraphics.DrawString(title, new Font("宋体", 9), Brushes.Black, new PointF(5 + intxLeft, 5));
            Point p1 = new Point(intxLeft - 10, intyLong);
            Pen p = new Pen(Color.Silver);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = 0; i < intyMax; i++)
            {
                p1.Y = intyLong - i * intyScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                objGraphics.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));//刻度线y轴
                objGraphics.DrawLine(p, intxLeft, intyLong - intEnd - i * intyScale, intxLong, intyLong - intEnd - i * intyScale);
                objGraphics.DrawString(Convert.ToString(kdvalue * i), new Font("宋体", 9), Brushes.Black, new Point(p1.X - 20, p1.Y - 30));
            }
            objGraphics.DrawLine(new Pen(Color.Black, 2), intxLeft, intyLong - intEnd, intxLeft, 0);//绘制y轴
            objGraphics.DrawLine(new Pen(Color.Black, 2), intxLeft, intyLong - intEnd, intxLong, intyLong - intEnd);//绘制x轴
            return objGraphics;
        }
        /**/
        /// <summary>
        /// 生成矩形图
        /// </summary>
        /// <param name="objGraphics">Graphics 对象</param>
        /// <param name="intxLong">图像x大小</param>
        /// <param name="intyLong">图像y大小</param>
        /// <param name="intxLeft">图像左边距</param>
        /// <param name="intEnd">图像下边距</param>
        /// <param name="intyMax">刻度数</param>
        /// <param name="kdvalue">刻度值</param>
        /// <param name="title">图片显示名</param>
        /// <param name="mydt">DataTable的值</param>
        /// <returns>Graphics</returns>
        public Graphics DwMonthjxt(Graphics objGraphics, int intxLong, int intyLong, int intxLeft, int intEnd, int intyMax, float kdvalue, DataTable mydt, int jdwidth)
        {
            int intyScale = intyLong / intyMax;
            float jx = Convert.ToInt32(jdwidth * 0.618);
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                float values = Convert.ToInt32(mydt.Rows[i]["co"].ToString());
                if (values != 0)
                {

                    objGraphics.FillRectangle(GetColor(intyLong), (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd, jx, values / kdvalue * intyScale);//画矩形内部图案
                    objGraphics.DrawRectangle(Pens.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd, jx, values / kdvalue * intyScale);//矩形外部图案
                    objGraphics.DrawString(mydt.Rows[i]["co"].ToString(), new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 15);//矩形上的数值
                    objGraphics.DrawString(mydt.Rows[i]["months"].ToString() + "月", new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - intEnd + 3);//x轴下的文字
                }
                else
                {
                    objGraphics.FillRectangle(GetColor(intyLong), (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 2, jx, values / kdvalue * intyScale + 2);//画矩形内部图案
                    objGraphics.DrawRectangle(Pens.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 2, jx, values / kdvalue * intyScale + 2);//矩形外部图案
                    objGraphics.DrawString(mydt.Rows[i]["co"].ToString(), new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 15);//矩形上的数值
                    objGraphics.DrawString(mydt.Rows[i]["months"].ToString() + "月", new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - intEnd + 3);//x轴下的文字
                }
            }
            return objGraphics;
        }
        /**/
        /// <summary>
        /// 生成矩形图
        /// </summary>
        /// <param name="objGraphics">Graphics 对象</param>
        /// <param name="intxLong">图像x大小</param>
        /// <param name="intyLong">图像y大小</param>
        /// <param name="intxLeft">图像左边距</param>
        /// <param name="intEnd">图像下边距</param>
        /// <param name="intyMax">刻度数</param>
        /// <param name="kdvalue">刻度值</param>
        /// <param name="title">图片显示名</param>
        /// <param name="mydt">DataTable的值</param>
        /// <returns>Graphics</returns>
        public Graphics DwHourjxt(Graphics objGraphics, int intxLong, int intyLong, int intxLeft, int intEnd, int intyMax, float kdvalue, DataTable mydt, int jdwidth)
        {
            int intyScale = intyLong / intyMax;
            float jx = Convert.ToInt32(jdwidth * 0.618);
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                float values = Convert.ToInt32(mydt.Rows[i]["co"].ToString());
                if (values != 0)
                {

                    objGraphics.FillRectangle(GetColor(intyLong), (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd, jx, values / kdvalue * intyScale);//画矩形内部图案
                    objGraphics.DrawRectangle(Pens.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd, jx, values / kdvalue * intyScale);//矩形外部图案
                    objGraphics.DrawString(mydt.Rows[i]["co"].ToString(), new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 15);//矩形上的数值
                    objGraphics.DrawString(mydt.Rows[i]["Hours"].ToString() + "时", new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - intEnd + 3);//x轴下的文字
                }
                else
                {
                    objGraphics.FillRectangle(GetColor(intyLong), (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 2, jx, values / kdvalue * intyScale + 2);//画矩形内部图案
                    objGraphics.DrawRectangle(Pens.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 2, jx, values / kdvalue * intyScale + 2);//矩形外部图案
                    objGraphics.DrawString(mydt.Rows[i]["co"].ToString(), new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - values / kdvalue * intyScale - intEnd - 15);//矩形上的数值
                    objGraphics.DrawString(mydt.Rows[i]["Hours"].ToString() + "时", new Font("宋体", 9), Brushes.Black, (i * jdwidth) + intxLeft + 20, intyLong - intEnd + 3);//x轴下的文字
                }
            }
            return objGraphics;
        }
    }
}

