/*
 * author: peace
 * email: peacechzh@126.com
 * date : 2009-4-18
 */
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using dotnetCHARTING;

namespace ChartExtents
{
    /// <summary>
    /// Charting 的摘要说明
    /// </summary>
    public class Charting
    {
        private string _phaysicalimagepath;//图片存放路径
        private string _title; //图片标题
        private string _xtitle;//图片x座标名称
        private string _ytitle;//图片y座标名称
        private string _seriesname;//图例名称
        private int _picwidth;//图片宽度
        private int _pichight;//图片高度
        private SeriesType _type;//统计图类型(柱形,线形等)
        private bool _use3d;//是否显示成3维图片
        private SeriesCollection _dt;//统计图数据源
        private string _filename;//统计图片的名称(不包括后缀名)

        /**/
        /// <summary>
        /// 图片存放路径
        /// </summary>
        public string PhaysicalImagePath
        {
            set { _phaysicalimagepath = value; }
            get { return _phaysicalimagepath; }
        }
        /**/
        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /**/
        /// <summary>
        /// 图片x座标名称
        /// </summary>
        public string XTitle
        {
            set { _xtitle = value; }
            get { return _xtitle; }
        }
        /**/
        /// <summary>
        /// 图片y座标名称
        /// </summary>
        public string YTitle
        {
            set { _ytitle = value; }
            get { return _ytitle; }
        }

        /**/
        /// <summary>
        /// 图例名称
        /// </summary>
        public string SeriesName
        {
            set { _seriesname = value; }
            get { return _seriesname; }
        }
        /**/
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int PicWidth
        {
            set { _picwidth = value; }
            get { return _picwidth; }
        }
        /**/
        /// <summary>
        /// 图片高度
        /// </summary>
        public int PicHight
        {
            set { _pichight = value; }
            get { return _pichight; }
        }

        /// <summary>
        /// 统计图类型(柱形,线形等)
        /// </summary>
        public SeriesType Type
        {
            set { _type = value; }
            get { return _type; }
        }

        /// <summary>
        /// 是否将输出的图片显示成三维
        /// </summary>
        public bool Use3D
        {
            set { _use3d = value; }
            get { return _use3d; }
        }

        /// <summary>
        /// 对比图形数据源
        /// </summary>
        public SeriesCollection DataSource
        {

            set { _dt = value; }
            get { return _dt; }
        }

        /// <summary>
        /// 生成统计图片的名称
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        

        /// <summary>
        /// 生成统计图片
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="type">图形类别,如柱状，折线型</param>
        public void CreateStatisticPic(dotnetCHARTING.Chart chart)
        {
            chart.Title = this.Title;
            chart.XAxis.Label.Text = this.XTitle;
            chart.YAxis.Label.Text = this.YTitle;
            chart.TempDirectory = this.PhaysicalImagePath;
            chart.FileManager.FileName = this.FileName;
            chart.Width = this.PicWidth;
            chart.Height = this.PicHight;
            chart.Type = ChartType.Combo;
            //chart.Series.Type = this.Type;//生成对比的线型图时不适用
            chart.DefaultSeries.Type = this.Type; //统一使用默认的序列图类型属性
            chart.Series.Name = this.SeriesName;
            chart.SeriesCollection.Add(this.DataSource);
            chart.DefaultSeries.DefaultElement.ShowValue = true;
            chart.ShadingEffect = true;
            chart.Use3D = this.Use3D;
            chart.Series.DefaultElement.ShowValue = true;
        }

    }
}
