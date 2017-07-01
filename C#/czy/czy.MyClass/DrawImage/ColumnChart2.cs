using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using System.Drawing;

namespace czy.MyClass.DrawImage
{
    public class ColumnChart2
    {

        /// <summary>
        /// DrawingCurve 的摘要说明
        /// </summary>
        public class DrawingCurve
        {

            public int intXLong = 800;   //图片大小 长
            public int intYLong = 600;   //图片大小 高
            public int intXMultiple = 1;    //零刻度的值 X
            public int intYMultiple = 0;    //零刻度的值 Y
            public int intXMax = 12;    //最大刻度(点数) X
            public int intYMax = 30;    //最大刻度(点数) Y

            public int intLeft = 50;   //左边距
            public int intRight = 120; //右边距
            public int intTop = 30;    //上边距
            public int intEnd = 50;    //下边距

            public string strXText = "时间(单位:月)";    //单位 X
            public string strYText = "数量(单位:个)";    //单位 Y
            public string strTitle = "趋势线图";    //标题
            public DataTable tbData;    //要统计的数据


            private int intXScale = 30;    //一刻度长度 X
            private int intYScale = 30;    //一刻度高度 Y
            //private int intX = 0;   //0点 X坐标
            //private int intY = 0;   //0点 Y坐标
            public int intData = 0;    //记录数

            public DrawingCurve()
            {

                intXScale = (intXLong - intLeft - intRight) / (intXMax + 1);//一刻度长度 X
                intYScale = (intYLong - intTop - intEnd) / (intYMax + 1);//一刻度高度 Y

                //intX = intXLong - intLeft;   //0点 X坐标
                //intY = intYLong - intEnd;   //0点 Y坐标
            }

            public Bitmap DrawingImg()
            {

                Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
                Graphics g = Graphics.FromImage(img);
                g.Clear(Color.Snow);
                g.DrawString(strTitle, new Font("宋体", 14), Brushes.Black, new Point(5, 5));
                g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向
                g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向

                //绘制纵坐标
                g.DrawString(strYText, new Font("宋体", 12), Brushes.Black, new Point(intLeft, intTop));//Y 单位
                Point p1 = new Point(intLeft - 10, intYLong - intEnd);
                for (int j = 0; j <= intYMax; j++)
                {
                    p1.Y = intYLong - intEnd - j * intYScale;
                    Point pt = new Point(p1.X + 10, p1.Y);
                    //绘制纵坐标的刻度和直线
                    g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                    //绘制纵坐标的文字说明
                    g.DrawString(Convert.ToString(j + intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
                }

                //绘制横坐标
                g.DrawString(strXText, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X 单位
                Point p = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    //绘制横坐标的文字说明
                    g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);
                }

                intData = tbData.Rows.Count;
                if (intData > 0)
                {
                    //趋势线图
                    for (int i = 0; i < intData - 1; i++)
                    {
                        DataRow Row1 = tbData.Rows[i];
                        DataRow Row2 = tbData.Rows[i + 1];
                        //定义起点
                        Point rec = new Point(Convert.ToInt32(intLeft + ((TurnNumber(Row1[0].ToString()) - intXMultiple) * intXScale)), Convert.ToInt32(intYLong - intEnd - (TurnNumber(Row1[1].ToString()) - intYMultiple) * intYScale));
                        //定义终点
                        Point dec = new Point(Convert.ToInt32(intLeft + ((TurnNumber(Row2[0].ToString()) - intXMultiple) * intXScale)), Convert.ToInt32(intYLong - intEnd - (TurnNumber(Row2[1].ToString()) - intYMultiple) * intYScale));
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red), rec, dec);
                    }
                }

                return img;
            }

            //转换数字
            private double TurnNumber(string str)
            {
                double dubReturn;
                try
                {
                    dubReturn = Convert.ToDouble(str);
                }
                catch
                {
                    dubReturn = 0;
                }
                return dubReturn;

            }

        }


    }
}