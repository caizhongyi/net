/*
 * author: peace
 * email: peacechzh@126.com
 * date : 2009-4-19
 */
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
using dotnetCHARTING;
using ChartExtents;

namespace Statistics
{
    public partial class Chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Drawing();
            }
        }

        private void Drawing()
        {
            Charting c = new Charting();

            c.Title = "2008年各月载客量";
            c.XTitle = "月份";
            c.YTitle = "载客量(万人)";
            c.PicHight = 350;
            c.PicWidth = 650;
            c.SeriesName = "合计";//仅对于DataTable类型做数据源时，此属性有效
            c.PhaysicalImagePath = "ChartImages";//统计图片存放的文件夹名称，缺少对应的文件夹生成不了统计图片
            c.FileName = "Statistics";
            c.Type = SeriesType.Cylinder;
            c.Use3D = false;
            c.DataSource = GetDataSource();
            c.CreateStatisticPic(this.Chart1);
            this.Chart1.XAxis.LabelMarker = new ElementMarker("ChartImages/ChartImages");
        }


        /// <summary>
        /// 生成单一图形时的数据源模型
        /// </summary>
        /// <returns></returns>
        //private DataTable GetDataSource()
        //{
        //    DataTable newTable = new DataTable();
        //    Random myR = new Random();
        //    newTable.Columns.Add("Month", typeof(int));//月份
        //    newTable.Columns.Add("Count", typeof(float));//载客量

        //    for (int i = 1; i <= 12; i++)
        //    {
        //        newTable.Rows.Add(new object[] { i, myR.Next(50) });
        //    }
        //    return newTable;
        //}

        /// <summary>
        /// 生成统计图片的数据源模型(单一或对比图都可以)
        /// </summary>
        /// <returns></returns>
        private SeriesCollection GetDataSource()
        {
            SeriesCollection SC = new SeriesCollection();
            Random rd = new Random();

            //DataTable newTable = new DataTable();
            //newTable.Columns.Add("Month", typeof(int));//月份
            //newTable.Columns.Add("Count", typeof(float));//载客量
            //for (int i = 1; i <= 12; i++)
            //{
            //    newTable.Rows.Add(new object[] { i, rd.Next(50) });
            //}
            ////生成单一图,将返回的DataTable数据处理成序列集合类型的数据，以便保持数据源类型的统一
            //Series s = new Series();
            //s.Name = "载客量合计";
            //for (int b = 1; b <= 12; b++) //X轴尺度个数，如一年12个月表示有12个尺度数
            //{
            //    Element e = new Element();
            //    e.Name = b.ToString();//对应于X轴个尺度的名称
            //    e.YValue = rd.Next(50);//与X轴对应的Y轴的数值
            //    s.Elements.Add(e);
            //}
            //SC.Add(s);

            // 生成对比图
            for (int a = 1; a <= 2; a++) //对比的项数，如2008年各月的公交和地铁载客量数据对比就相当于有两个数据项
            {
                Series s = new Series();
                s.Name = (a == 1 ? "公交载客量合计" : "地铁载客量合计");//各个数据项代表的名称，如公交和地铁12个月载客量走势图，则一条表示公交，一条表示地铁
                for (int b = 1; b <= 12; b++) //X轴尺度个数，如12个月表示有12个尺度数
                {
                    Element e = new Element();
                    e.Name = b.ToString();//对应于X轴个尺度的名称
                    e.YValue = rd.Next(50);//与X轴对应的Y轴的数值
                    s.Elements.Add(e);
                }
                SC.Add(s);
            }
  

            //可自定义填充图的填充色，系统采取默认分配各数据项的填充色
            //SC[0].DefaultElement.Color = Color.Blue;
            //SC[1].DefaultElement.Color = Color.Red;
            //SC[2].DefaultElement.Color = Color.FromArgb(255, 99, 49);
            //SC[3].DefaultElement.Color = Color.FromArgb(0, 156, 255);
            return SC;
        }

        
    }
}
