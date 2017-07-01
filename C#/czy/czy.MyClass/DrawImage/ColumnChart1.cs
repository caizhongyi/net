using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Drawing;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        private string[,] data = new string[6, 2];

        protected void Page_Load(object sender, EventArgs e)
        {
            DrawingAPic();
        }

        private void DrawingAPic()
        {
            int i;
            // 实例化Bitmap对象
            Bitmap objbitmap;
            objbitmap = new Bitmap(400, 300);
            Graphics objGraphics;
 
            // 实例化Graphics类
            objGraphics = Graphics.FromImage(objbitmap);
 
            // 填充背景色            
            objGraphics.Clear(Color.White);
 
            // 画圆
            objGraphics.DrawRectangle(Pens.Black, 1, 1, 398, 298);
 
            // 写标题
            objGraphics.DrawString("本公司上半年营业额统计图", new Font("宋体", 16, FontStyle.Bold), Brushes.Black, new PointF(60, 5));
 
            // 获取数据，这里模拟出6个月的公司业务数据，实际应用可以从数据库读取
            getdata();
 
            PointF monthcolor = new PointF(260, 40);
            PointF fontinfor = new PointF(285, 36);
 
            for (i = 0; i <= 5; i++)
            {
                //  画出填充矩形
                objGraphics.FillRectangle(new SolidBrush(getcolor(i)), monthcolor.X, monthcolor.Y, 20, 10);
 
                //画出矩形边框。
                objGraphics.DrawRectangle(Pens.Black, monthcolor.X, monthcolor.Y, 20, 10);
 
                //画出图例说明文字－－data(i, 0)
                objGraphics.DrawString(data[i, 0], new Font("宋体", 10), Brushes.Black, fontinfor);
 
                //移动坐标位置，只移动Y方向的值即可。
                monthcolor.Y += 15;
                fontinfor.Y += 15;
            }
 
            // 遍历数据源的每一项数据，并根据数据的大小画出矩形图（即柱形图的柱）。
            for (i = 0; i <= 5; i++)
            {
                //画出填充矩形。
                objGraphics.FillRectangle(new SolidBrush(getcolor(i)), (i * 25) + 35, 270 - System.Convert.ToInt32(data[i, 1]), 15, System.Convert.ToInt32(data[i, 1]));
 
                //'画出矩形边框线。
                objGraphics.DrawRectangle(Pens.Black, (i * 25) + 35, 270 - System.Convert.ToInt32(data[i, 1]), 15, System.Convert.ToInt32(data[i, 1]));
            }
 
            //画出示意坐标
            objGraphics.DrawLine(new Pen(Color.Blue, 1), 10, 0, 10, 320);
            objGraphics.DrawLine(new Pen(Color.Blue, 1), 10, 270, 200, 270);
 
            // 在示意坐标上添加数值标志，注意坐标的计算
            for (i = 0; i <= 5; i++)
            {
                objGraphics.DrawLine(new Pen(Color.Blue, 1), 10, i * 50 + 20, 20, i * 50 + 20);
                objGraphics.DrawString((250 - i * 50).ToString(), new Font("宋体", 10), Brushes.Black, 12, i * 50 + 8);
            }
            //统计总销售额
            float scount = 0;
            for (i = 0; i <= 5; i++)
            {
                scount += float.Parse((data[i, 1]));
            }
 
            //定义画出扇形角度变量
            float scg = 0;
            float stg = 0;
            for (i = 0; i <= 5; i++)
            {
                //计算当前角度值：当月销售额 / 总销售额 * 360，得到饼图中当月所占的角度大小。
                float num = float.Parse(data[i, 1]);
                scg = (num / scount) * 360;
 
                //画出填充圆弧。
                objGraphics.FillPie(new SolidBrush(getcolor(i)), 220, 150, 120, 120, stg, scg);
 
                //画出圆弧线。
                objGraphics.DrawPie(Pens.Black, 220, 150, 120, 120, stg, scg);
 
                //  把当前圆弧角度加到总角度上。
                stg += scg;
            }
 
            // 画出说明文字
            objGraphics.DrawString("柱状图", new Font("宋体", 15, FontStyle.Bold), Brushes.Blue, 50, 272);
            objGraphics.DrawString("饼状图", new Font("宋体", 15, FontStyle.Bold), Brushes.Blue, 250, 272);
 
            // 输出到客户端
            objbitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
 
        }
        // 为数组赋值
        // 即生成模拟业务数据
        private void getdata()
        {
            data[0, 0] = "一月份";
            data[1, 0] = "二月份";
            data[2, 0] = "三月份";
            data[3, 0] = "四月份";
            data[4, 0] = "五月份";
            data[5, 0] = "六月份";
            data[0, 1] = "85";
            data[1, 1] = "135";
            data[2, 1] = "85";
            data[3, 1] = "110";
            data[4, 1] = "130";
            data[5, 1] = "200";
        }

        // 产生色彩值，便于显示区别
        private Color getcolor(int i)
        {
            Color newcolor;
            i += 1;
            if (i == 1)
            {
                newcolor = Color.Blue;
            }
            else if (i == 2)
            {
                newcolor = Color.ForestGreen;
            }
            else if (i == 3)
            {
                newcolor = Color.Gainsboro;
            }
            else if (i == 4)
            {
                newcolor = Color.Moccasin;
            }
            else if (i == 5)
            {
                newcolor = Color.Indigo;
            }
            else if (i == 6)
            {
                newcolor = Color.BurlyWood;
            }
            else
                newcolor = Color.Goldenrod;
            return newcolor;
        }
    }
}