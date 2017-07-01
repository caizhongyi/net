using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace czy.MyClass.DrawImage
{
    /// <summary>
    /// 生成图片报表
    /// </summary>
    public class LineChart
    {
        #region 生成图型报表
        /// <summary>
        /// 生成图型报表
        /// </summary>
        /// <param name="ds">数据集</param>
        public static void CreateChart(DataSet ds)
        {
            string data = string.Empty;

            int days = ds.Tables[0].Rows.Count;
            string[] a = new string[ds.Tables[0].Columns.Count - 1];
            string[] b = new string[ds.Tables[0].Rows.Count];


            for (int i = 0; i < ds.Tables[0].Columns.Count - 1; i++)
            {
                a[i] = ds.Tables[0].Columns[i + 1].ColumnName.ToString();
            }
            string title = string.Join(",", a) + ";(kwh),N0.0,(day),Dyyyy-MM-dd;";

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                string[] temp = new string[ds.Tables[0].Columns.Count + 1];
                temp[0] = ((int)Convert.ToDateTime(ds.Tables[0].Rows[j].ItemArray[0].ToString()).ToOADate()).ToString();
                for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                {

                    temp[i] = ds.Tables[0].Rows[j].ItemArray[i].ToString();
                }
                b[j] = string.Join(",", temp);
            }
            data = title + string.Join(";", b);

            //string data1 = "Total;(kwh),N0.0,(day),Dyyyy-MM-dd;";

            //DateTime start = DateTime.Now.AddDays(-1 * days);

            //System.Random random = new Random();

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //sb.Append(data1);

            //string[] b1 = new string[days];

            //for (int i = 0; i < days; i++)
            //{
            //    string[] a1 = new string[3];

            //    a1[0] = ((int)DateTime.Now.AddDays(-1 * i).ToOADate()).ToString();

            //    for (int j = 1; j < 3; j++)
            //    {
            //        System.Threading.Thread.Sleep(1);

            //        a1[j] = random.Next(100).ToString();

            //    }

            //    b1[i] = string.Join(",", a1);
            //}

            //sb.Append(string.Join(";", b1));

            //data1 = sb.ToString();

            Report.Chart chart = new Report.Chart();

            chart.Data = data;
            chart.Width = 800;
            chart.Height = 200;
            chart.Padding = 8;
            chart.IsShowXValue = true;
            chart.Font = new System.Drawing.Font("楷体2312", 8);

            chart.Render();
        }
        #endregion
    }
      
}
namespace Report
{
    /// <summary>
    /// Chart 的摘要说明。
    /// ==================================================================================================
    /// 
    ///    ClassName  ：Report.Chart  
    ///    Intro      ：
    ///    Example    ：  
    ///    Ver        ：0.2
    ///     
    ///    Author     ：ttyp  
    ///    Email      ：ttyp@21cn.com  
    ///    Date       ：2007-7-30
    /// ==================================================================================================
    /// </summary>
    public class Chart
    {
        public Chart() { }

        #region 成员
        private string _data = "";
        private int _width = 100;
        private int _height = 100;
        private int _padding = 8;
        private Color _grid_color = Color.FromArgb(0x93, 0xbe, 0xe2);
        private Color _border_color = Color.FromArgb(0x93, 0xbe, 0xe2);
        private Font _font = new Font("Arial", 8);
        private bool _IsShowXValue = true;
        /// <summary>
        /// 字体
        /// </summary>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _border_color; }
            set { _border_color = value; }
        }
        /// <summary>
        /// 表格线颜色
        /// </summary>
        public Color GridColor
        {
            get { return _grid_color; }
            set { _grid_color = value; }
        }
        /// <summary>
        /// 刻度间距
        /// </summary>
        public int Padding
        {
            get { return _padding; }
            set { _padding = Math.Max(0, value); }
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = Math.Max(0, value); }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = Math.Max(0, value); }
        }
        /// <summary>
        /// 数据源（字符窜型）
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
        /// <summary>
        /// 是否显示X轴的值
        /// </summary>
        public bool IsShowXValue
        {
            get { return _IsShowXValue; }
            set { _IsShowXValue = value; }
        }
        #endregion

        #region  画图
        /// <summary>
        /// 画图
        /// </summary>
        public void Render()
        {
            int width = this.Width;
            int height = this.Height;
            int padding = this.Padding;


            System.Drawing.Bitmap image = new System.Drawing.Bitmap(width, height);

            Graphics g = Graphics.FromImage(image);

            //清空图片背景色
            g.Clear(Color.White);

            //虚线画笔
            Pen dot = new Pen(this.GridColor);
            dot.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            //实线画笔
            Pen solid = new Pen(this.BorderColor);

            //文字字体
            Font font = this.Font;
            try
            {

                //冗余，去除最后的数据分割标记，防止空数据
                if (this.Data.EndsWith(";"))
                {
                    this.Data = this.Data.Substring(0, this.Data.Length - 1);
                }

                string[] info = this.Data.Split(';');        //数据信息

                if (info.Length >= 2)
                {


                    string[] lines = info[0].Split(',');    //图例
                    string[] units = info[1].Split(',');    //单位和标题格式，a,b,c,d  a 纵坐标单位 b 纵坐标格式 N 数字 D 时间 后面是具体格式，c 横坐标单位 d 横坐标格式（同b）

                    //曲线颜色表
                    Color[] color = new Color[]
                    { 
                        Color.FromArgb(0,184,191),
                        Color.FromArgb (136,138,133),
                        Color.Red,
                        Color.Gray,
                        Color.Black,
                        Color.Magenta,
                        Color.Cyan,
                        Color.Yellow, 
                        Color.DeepPink,
                        Color.BurlyWood,
                        Color.DarkRed,
                        Color.Gold
                    };


                    //图例文字的大小
                    SizeF sFont = GetMaxSize(lines, g, font);

                    //获得刻度文字高度
                    int textHeight = (int)(sFont.Height * 3 / 2);

                    //曲线点的个数
                    int points = info.Length - 2;

                    //得到曲线点数组集合
                    string[,] curve = new string[info.Length - 2, lines.Length + 1];
                    for (int i = 0; i < points; i++)
                    {
                        string[] l = info[i + 2].Split(',');
                        int len = l.Length;

                        for (int j = 0; j <= lines.Length; j++)
                        {
                            if (j < len)
                            {
                                curve[i, j] = l[j];
                            }
                            else
                            {
                                curve[i, j] = "N";            //非数据，不画线
                            }
                        }
                    }

                    //获得最大，最小值
                    double maxY, minY, maxX, minX;

                    GetMaxMin(curve, out maxY, out minY, out maxX, out minX);
                    //冗余最大最小值
                    if (maxY == minY)
                    {
                        if (maxY == 0)
                        {
                            maxY = 10;
                            minY = -10;
                        }
                        else
                        {
                            if (maxY > 0)
                            {
                                maxY = maxY * 2;
                                minY = 0;
                            }
                            else
                            {
                                maxY = 0;
                                minY = maxY * 2;
                            }
                        }
                    }

                    if (maxX == minX)
                    {
                        if (maxX == 0)
                        {
                            maxX = 10;
                            minX = -10;
                        }
                        else
                        {
                            if (maxX > 0)
                            {
                                maxX = maxX * 2;
                                minY = 0;
                            }
                            else
                            {
                                maxX = 0;
                                minX = maxX * 2;
                            }
                        }
                    }

                    //获取坐标框的上下左右
                    float left = (padding * 2 + sFont.Height + 2 + sFont.Width + padding + GetMaxSize(units[1], g, font).Width + padding);
                    float bottom = height - padding - textHeight;
                    float top = padding;
                    float right = width - padding;

                    //获取曲线框的宽度和高度（比坐标框略小）
                    float yWidth = bottom - top - GetMaxSize(units[0], g, font).Height * 3 / 2 - padding;
                    float xWidth = right - left - GetMaxSize(units[3], g, font).Width / 2 - sFont.Width - padding;


                    //---------------------------------------------------------------------------------

                    //获取最大行
                    int maxrow = (int)(yWidth / (sFont.Height / 2 * 3));
                    maxrow = Math.Max(maxrow, 1);

                    //获取Y步进值
                    float stepYv = (float)((maxY - minY) / (maxrow));

                    if (units[1].Length > 1)
                    {
                        //整数分割，调整最大行和最大最小值
                        if (units[1].Substring(0, 1).ToLower() == "d")
                        {
                            maxY = Math.Ceiling(maxY);
                            minY = Math.Floor(minY);
                            stepYv = (float)Math.Ceiling((maxY - minY) / maxrow);
                            maxrow = (int)((maxY - minY) / stepYv);
                        }
                    }

                    float stepy = (float)((yWidth / (maxY - minY)) * stepYv);


                    //---------------------------------------------------------------------------------


                    //得到最大的网格列（最多10列）
                    int maxcol = points;
                    maxcol = Math.Min(points, maxcol);
                    maxcol = Math.Max(maxcol, 1);

                    //获取X步进值
                    float stepXv = (float)((maxX - minX) / (maxcol));

                    if (units[3].Length > 1)
                    {
                        //整数分割，调整最大和最小值，以及步进
                        if (units[3].Substring(0, 1).ToLower() == "d")
                        {
                            maxX = Math.Ceiling(maxX);
                            minX = Math.Floor(minX);
                            stepXv = (float)Math.Ceiling((maxX - minX) / maxcol);
                            maxcol = (int)((maxX - minX) / stepXv);
                        }
                    }

                    //获得最大显示列数
                    int dispcol = (int)((xWidth) / (GetMaxSize(units[3].Substring(1), g, font).Width + padding));
                    dispcol = Math.Max(dispcol, 1);

                    //如果最大显示列小于最大列，则应该缩减
                    if (dispcol < maxcol)
                    {
                        stepXv = (float)Math.Ceiling((maxX - minX) / dispcol);
                        maxcol = (int)((maxX - minX) / stepXv);
                    }


                    float stepx = (float)((xWidth / (maxX - minX)) * stepXv);


                    //获得最大的曲线数目
                    int maxline = color.Length;
                    maxline = Math.Min(maxline, lines.Length);


                    //画图例边框
                    g.DrawRectangle(solid, padding, (height - ((sFont.Height + 5) * maxline + 2 * padding)) / 2, padding * 2 + sFont.Height + 2 + sFont.Width, (sFont.Height + 5) * maxline + 2 * padding);

                    //画图例
                    for (int i = 0; i < maxline; i++)
                    {
                        SolidBrush fb = new SolidBrush(color[i]);
                        SolidBrush bl = new SolidBrush(Color.Black);
                        //画图例方框
                        g.FillRectangle(fb, padding * 2, (height - ((sFont.Height + 5) * maxline + 2 * padding)) / 2 + (sFont.Height + 5) * i + padding, sFont.Height, sFont.Height);
                        //画图例文字
                        g.DrawString(lines[i], font, bl, padding * 2 + sFont.Height + 2, (height - ((sFont.Height + 5) * maxline + 2 * padding)) / 2 + (sFont.Height + 5) * i + padding);

                    }


                    //画坐标
                    g.DrawLine(solid, left, top, left, bottom);        //Y
                    g.DrawLine(solid, left, bottom, right, bottom);    //X

                    //画坐标箭头
                    g.DrawLine(solid, left, top, left - padding / 3, top + padding / 2);            //Y箭头
                    g.DrawLine(solid, left, top, left + padding / 3, top + padding / 2);

                    g.DrawLine(solid, right, bottom, right - padding / 2, bottom - padding / 3);    //X箭头
                    g.DrawLine(solid, right, bottom, right - padding / 2, bottom + padding / 3);



                    //画X刻度
                    for (int i = 0; i <= maxcol; i++)
                    {
                        SolidBrush bl = new SolidBrush(Color.Black);
                        if (i > 0)
                        {
                            g.DrawLine(dot, left + i * stepx, top + padding, left + i * stepx, bottom);
                        }

                        string text = "";

                        switch (units[3].Substring(0, 1).ToString())
                        {
                            case "N":
                                text = (minX + stepXv * i).ToString(units[3].Substring(1));
                                break;
                            case "D":
                                text = DateTime.FromOADate((int)(minX + stepXv * i)).ToString(units[3].Substring(1));
                                break;
                        }

                        SizeF xf = GetMaxSize(text, g, font);

                        g.DrawString(text, font, bl, left + i * stepx - xf.Width / 2, bottom + xf.Height / 2);

                        if (i == 0)
                        {
                            g.DrawString(units[2], font, bl, right - GetMaxSize(units[2], g, font).Width, bottom);
                        }

                        if (points <= 1)
                        {
                            break;
                        }
                    }

                    //画Y刻度
                    for (int i = 0; i <= maxrow; i++)
                    {
                        SolidBrush bl = new SolidBrush(Color.Black);

                        if (i > 0)
                        {
                            g.DrawLine(dot, left, bottom - i * stepy, right - padding, bottom - i * stepy);
                        }


                        string text = "";

                        switch (units[1].Substring(0, 1).ToString())
                        {
                            case "N":
                                text = (minY + i * stepYv).ToString(units[1].Substring(1));
                                break;
                            case "D":
                                text = DateTime.FromOADate(int.Parse(curve[i, 0])).ToString(units[1].Substring(1));
                                break;
                        }

                        SizeF xf = GetMaxSize(text, g, font);

                        g.DrawString(text, font, bl, left - xf.Width, bottom - stepy * i - xf.Height / 2);

                        if (i == 0)
                        {
                            g.DrawString(units[0], font, bl, left - GetMaxSize(units[0], g, font).Width - 3, top);
                        }
                    }

                    //画图片的边框线
                    g.DrawRectangle(solid, 0, 0, image.Width - 1, image.Height - 1);

                    float[] px = new float[maxline];
                    float[] py = new float[maxline];

                    bool[] ps = new bool[maxline];

                    //画曲线
                    for (int j = 0; j < points; j++)
                    {
                        float v = float.Parse(curve[j, 0]);
                        float cx = (float)(left + (xWidth) * (v - minX) / (maxX - minX));

                        for (int i = 0; i < maxline; i++)
                        {
                            try
                            {
                                float w = float.Parse(curve[j, i + 1]);
                                float cy = (float)(bottom - (yWidth) * (w - minY) / (maxY - minY));
                                Pen cp = new Pen(color[i], 2);
                                g.FillEllipse(cp.Brush, px[i] - 3, py[i] - 3, 6, 6);
                                if (_IsShowXValue)
                                {
                                    g.DrawString(w.ToString(), _font, cp.Brush, cx, cy - 13);
                                }
                                if (ps[i])
                                {
                                    g.DrawLine(cp, px[i], py[i], cx, cy);

                                }

                                px[i] = cx;
                                py[i] = cy;
                                ps[i] = true;

                                if (points == 1)
                                {
                                    image.SetPixel((int)cx, (int)cy, color[i]);
                                }
                            }
                            catch
                            {
                                ps[i] = false;
                            }
                        }
                    }
                }
                else
                {
                    string msg = "no data";
                    g.DrawString(msg, font, new SolidBrush(Color.Black), (width - GetMaxSize(msg, g, font).Width) / 2, (height - GetMaxSize(msg, g, font).Height) / 2);
                }

                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ContentType = "image/Gif";

                image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

        #region 获取最大值
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        private SizeF GetMaxSize(string[] s, Graphics g, Font f)
        {
            string max = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > max.Length)
                {
                    max = s[i];
                }
            }

            return g.MeasureString(max, f);
        }

       /// <summary>
        /// 获取最大值
       /// </summary>
       /// <param name="s"></param>
       /// <param name="g"></param>
       /// <param name="f"></param>
       /// <returns></returns>
        private SizeF GetMaxSize(string s, Graphics g, Font f)
        {
            return g.MeasureString(s, f);
        }
        #endregion

        #region 获取最小值
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxY"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="minX"></param>
        private void GetMaxMin(string[,] data, out double maxY, out double minY, out double maxX, out double minX)
        {
            int row = 0;
            int col = 0;

            double m = double.MinValue, n = double.MaxValue;
            double p = double.MinValue, q = double.MaxValue;

            row = data.GetLength(0);
            col = data.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    double v = 0;

                    try
                    {
                        v = double.Parse(data[i, j]);

                        if (j > 0)
                        {
                            if (v > m)
                            {
                                m = v;
                            }
                            if (v < n)
                            {
                                n = v;
                            }
                        }
                        else
                        {
                            if (v > p)
                            {
                                p = v;
                            }
                            if (v < q)
                            {
                                q = v;
                            }
                        }
                    }
                    catch { }
                }
            }

            maxY = m;
            minY = n;

            maxX = p;
            minX = q;
        }
        #endregion
    }
}