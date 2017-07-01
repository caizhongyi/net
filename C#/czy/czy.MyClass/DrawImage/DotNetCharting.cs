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
using System.Web.UI;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;

namespace czy.MyClass.DrawImage
{
    public class DotNetChartingEventArgs : EventArgs
    {
        private DataSet _ds;
        public DataSet DS
        {
            get { return _ds; }
        }
        public DotNetChartingEventArgs()
        {

        }
        public DotNetChartingEventArgs(DataSet ds)
        {
            this._ds = ds;
        }
    }

    /// <summary>
    /// Charting 的摘要说明
    /// </summary>
    public class DotNetCharting
    {
        #region 成员
        public delegate void DotNetChartingEventHandler(object o, EventArgs e);
        /// <summary>
        /// 初始化事件
        /// </summary>
        public DotNetChartingEventHandler Init;
        /// <summary>
        /// 获取数据后事件
        /// </summary>
        public DotNetChartingEventHandler AfterGetData;
        /// <summary>
        /// 获取数据前事件
        /// </summary>
        public DotNetChartingEventHandler BeforeGetData;

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
        private DataSet _ds; //数据集
        private DataTable _dataTable;
        private Series _seriesSource;
        private Bitmap objBitmap = null; //位图对象
        private Graphics objGraphics = null; //Graphics 类提供将对象绘制到显示设备的方法# 44 44 44
        private string _baseRoot; //图片存入在目录
        private string _fileFormat = ".png";
        private double total;


        /// <summary>
        /// 文件格式
        /// </summary>
        public string FileFormat
        {
         
            get { return _fileFormat; }
            set { _fileFormat = value; }
        }

        /// <summary>
        /// 图片保存的目录路径
        /// </summary>
        public string BaseRoot
        {
            get { return _baseRoot; }
            set { _baseRoot = value; }
        }
        /// <summary>
        /// Series数据源
        /// </summary>
        public Series SeriesSource
        {
            get { return _seriesSource; }
            set { _seriesSource = value; }
        }

        /// <summary>
        /// 数据表
        /// </summary>
        public DataTable Dt
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }
        /// <summary>
        /// //数据集
        /// </summary>
        public DataSet Ds
        {
            get { return _ds; }
            set { _ds = value; }
        }
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

        #endregion


        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="XTitle">X轴标题</param>
        /// <param name="YTitle">Y轴标题</param>
        /// <param name="baseRoot">文件夹目录[root/]</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <param name="Title">标题</param>
        public DotNetCharting(Page page, DataSet ds, string XTitle, string YTitle, string baseRoot, string fileName, string Title)
        {
            // AddScript(page);
            this._ds = ds;
            this.XTitle = XTitle;
            this.YTitle = YTitle;
            this._title = Title;
            this._use3d = true;
            this._picwidth = 650;
            this._pichight = 350;
            this._seriesname = "";//仅对于DataTable类型做数据源时，此属性有效
            this._filename = fileName;
            this.Type = SeriesType.Column;
            this._phaysicalimagepath = AppDomain.CurrentDomain.BaseDirectory + baseRoot + this._filename + this._fileFormat;
            this._baseRoot = baseRoot;

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="XTitle">X轴标题</param>
        /// <param name="YTitle">Y轴标题</param>
        /// <param name="baseRoot">文件夹目录[root/]</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <param name="Title">标题</param>
        /// <param name="seriesType">显式SeriesType样式</param>
        public DotNetCharting(Page page, DataSet ds, string XTitle, string YTitle, string baseRoot, string fileName, string Title, dotnetCHARTING.SeriesType seriesType)
        {
            /// AddScript(page);
            this._ds = ds;
            this.XTitle = XTitle;
            this.YTitle = YTitle;
            this._title = Title;
            this._use3d = true;
            this._picwidth = 650;
            this._pichight = 350;
            this._seriesname = "";//仅对于DataTable类型做数据源时，此属性有效
            this._filename = fileName;
            this.Type = seriesType;
            this._phaysicalimagepath = AppDomain.CurrentDomain.BaseDirectory + baseRoot + this._filename + this._fileFormat;
            this._baseRoot = baseRoot;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="XTitle">X轴标题</param>
        /// <param name="YTitle">Y轴标题</param>
        /// <param name="baseRoot">文件夹目录[root/]</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <param name="Title">标题</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public DotNetCharting(Page page, DataSet ds, string XTitle, string YTitle, string baseRoot, string fileName, string Title, int width, int height)
        {
            //AddScript(page);
            this._ds = ds;
            this.XTitle = XTitle;
            this.YTitle = YTitle;
            this._title = Title;
            this._use3d = true;
            this._picwidth = width;
            this._pichight = height;
            this._seriesname = "";//仅对于DataTable类型做数据源时，此属性有效
            this._filename = fileName;
            this.Type = SeriesType.Column;
            this._phaysicalimagepath = AppDomain.CurrentDomain.BaseDirectory + baseRoot + this._filename + this._fileFormat;
            this._baseRoot = baseRoot;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="XTitle">X轴标题</param>
        /// <param name="YTitle">Y轴标题</param>
        /// <param name="baseRoot">文件夹目录[root/]</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <param name="Title">标题</param>
        /// <param name="seriesType">显式SeriesType样式</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public DotNetCharting(Page page, DataSet ds, string XTitle, string YTitle, string baseRoot, string fileName, string Title, dotnetCHARTING.SeriesType seriesType, int width, int height)
        {
            //AddScript(page);
            this._ds = ds;
            this.XTitle = XTitle;
            this.YTitle = YTitle;
            this._title = Title;
            this._use3d = true;
            this._picwidth = width;
            this._pichight = height;
            this._seriesname = "";//仅对于DataTable类型做数据源时，此属性有效
            this._filename = fileName;
            this.Type = seriesType;
            this._phaysicalimagepath = AppDomain.CurrentDomain.BaseDirectory + baseRoot + this._filename + this._fileFormat;
            this._baseRoot = baseRoot;
        }

        #endregion

        //private void AddScript(Page page)
        //{
        //    System.Web.UI.WebControls.Literal literal = new System.Web.UI.WebControls.Literal();
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(" <script type='text/javascript'>");
        //    sb.AppendLine("var body = document.getElementById('form1');");
        //    sb.AppendLine("var list = document.getElementsByTagName('area');");
        //    sb.AppendLine(" var list1 = document.getElementsByTagName('map');");
        //    sb.AppendLine("var list2 = document.getElementsByTagName('MAP');");
        //    sb.AppendLine("for (var i = 0; i < list1.length; i++)");
        //    sb.AppendLine(" { list1[i].parentNode.removeChild(list1[i]);}");
        //    sb.AppendLine(" for (var i = 0; i < list.length; i++)");
        //    sb.AppendLine("{ list[i].parentNode.removeChild(list[i]);}");
        //    sb.AppendLine("for (var i = 0; i < list2.length; i++)");
        //    sb.AppendLine("{  list2[i].parentNode.removeChild(list2[i]);}");
        //    sb.AppendLine(" </script>");
        //    literal.Text = sb.ToString();
        //    page.Controls.Add(literal);
        //}



        private void InitChart(dotnetCHARTING.Chart chart)
        {
            chart.XAxis.SpacingPercentage = (float)30;
            chart.Title = this.Title;
            chart.XAxis.Label.Text = this.XTitle;
            chart.YAxis.Label.Text = this.YTitle;
            chart.TempDirectory = "~/" + this._baseRoot;
            chart.FileManager.FileName = this.FileName;
            chart.Width = this.PicWidth;
            chart.Height = this.PicHight;
            //chart.Series.Type = this.Type;//生成对比的线型图时不适用
            chart.DefaultSeries.Type = this.Type; //统一使用默认的序列图类型属性
            chart.Series.Name = this.SeriesName;
            chart.DefaultSeries.DefaultElement.ShowValue = true;
            chart.ShadingEffect = true;
            chart.Use3D = this.Use3D;
            chart.DefaultSeries.DefaultElement.Transparency = 20; //透明设置
            chart.Series.DefaultElement.ShowValue = true;
            chart.PieLabelMode = PieLabelMode.Automatic;//设置值的显示方式

            //chart.ShowDateInTitle = true;
            // chart.AutoNameLabels = true;
            //chart.RadarLabelMode = dotnetCHARTING.RadarLabelMode.Angled;
            if(Init!=null)Init(this, new EventArgs());


        }

        #region 生成图型
        Page p;
        Chart c;
        private void ResponseWrite(Page page)
        {
            page.SaveStateComplete += new EventHandler(AreaSalePic_GetSalesArea_SaveStateComplete);
            p = page;

        }



        protected void AreaSalePic_GetSalesArea_SaveStateComplete(object o, EventArgs e)
        {

            //objBitmap = new Bitmap(this._picwidth, this._pichight);
            ////从指定的 objBitmap 对象创建 objGraphics 对象 (即在objBitmap对象中画图)
            //objGraphics = Graphics.FromImage(objBitmap);


            //p.Response.ContentType = "image/jpeg";

            //StreamReader sr = new StreamReader(p.Server.MapPath(this._filename + ".png"));
            //objGraphics.DrawImage(new Bitmap(sr.BaseStream), 0, 0);
            //objGraphics.Dispose();
            //objGraphics = null;
            //objBitmap.Save(p.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //objBitmap.Dispose();
            //objBitmap.Save(this._filename);

            p.Response.WriteFile(this._phaysicalimagepath);


        }
        /// <summary>
        /// 设置数据
        /// </summary>
        private void SetDataSource()
        {
            if (BeforeGetData != null) BeforeGetData(this, new DotNetChartingEventArgs());
            this._dt = GetDataSources();
            if (AfterGetData != null) AfterGetData(this, new DotNetChartingEventArgs(this._ds));
        }

        #region 生成柱状图
        /// <summary>
        /// 生成柱状对比图
        /// </summary>
        /// <param name="chart">dotnetCHARTING.Chart</param>
        public void CreateColumn(dotnetCHARTING.Chart chart, Page page, bool IsReponseImage)
        {
            SetDataSource();
            InitChart(chart);
            chart.Type = ChartType.Combo;
            chart.SeriesCollection.Add(this._dt);

            c = chart;
            if (IsReponseImage)
                ResponseWrite(page);
        }
        /// <summary>
        /// 生成归类柱状图
        /// </summary>
        /// <param name="chart">dotnetCHARTING.Chart</param>
        public void CreateColumnBySide(dotnetCHARTING.Chart chart, Page page, bool IsReponseImage)
        {
            SetDataSource();
            InitChart(chart);
            chart.Type = ChartType.ComboSideBySide;
            chart.SeriesCollection.Add(this._dt);

            c = chart;
            if (IsReponseImage)
                ResponseWrite(page);
        }
        /// <summary>
        /// 生成横向柱状图
        /// </summary>
        /// <param name="chart">dotnetCHARTING.Chart</param>
        public void CreateColumnHorizontal(dotnetCHARTING.Chart chart, Page page, bool IsReponseImage)
        {
            SetDataSource();
            InitChart(chart);
            chart.Type = ChartType.ComboHorizontal;
            chart.SeriesCollection.Add(this._dt);

            c = chart;
            if (IsReponseImage)
                ResponseWrite(page);
        }
        #endregion

        #region  生成饼图
        /// <summary>
        /// 生成饼图
        /// </summary>
        /// <param name="chart">dotnetCHARTING.Chart</param>
        public void CreatePic(dotnetCHARTING.Chart chart, Page page, bool IsReponseImage)
        {
            SetDataSource();
            InitChart(chart);
            chart.Type = ChartType.Pie;
            chart.SeriesCollection.Add(this._dt);
            chart.Use3D = this.Use3D;
            chart.DefaultSeries.DefaultElement.Transparency = 0; //透明设置

            c = chart;
            if (IsReponseImage)
                ResponseWrite(page);
        }
        /// <summary>
        /// 生成饼图
        /// </summary>
        /// <param name="chart">dotnetCHARTING.Chart</param>
        public void CreatePics(dotnetCHARTING.Chart chart, Page page, bool IsReponseImage)
        {
            SetDataSource();
            InitChart(chart);
            chart.Type = ChartType.Pies;
            chart.SeriesCollection.Add(this._dt);
            chart.Use3D = this.Use3D;
            chart.DefaultSeries.DefaultElement.Transparency = 0; //透明设置

            for (int i = 0; i < chart.SeriesCollection[0].Elements.Count; i++)
            {
                chart.SeriesCollection[0].Elements[i].SmartLabel = new SmartLabel();
                chart.SeriesCollection[0].Elements[i].SmartLabel.Text = chart.SeriesCollection[0].Elements[i].Name + " " + Convert.ToInt16((chart.SeriesCollection[0].Elements[i].YValue / total * 100)).ToString() + "%";

                //this._ds.Tables[0].Rows[i]["pencent1"] = chart.SeriesCollection[0].Elements[i].SmartLabel.Text;
                //e.SmartLabel = new SmartLabel();
                // e.SmartLabel.Text = ds.Tables[a].Rows[b][0].ToString();
            }

            c = chart;
            if (IsReponseImage)
                ResponseWrite(page);
        }
        #endregion
        #endregion



        #region  数据源

        /// <summary>
        /// 生成统计图片的数据源模型(单一或对比图都可以)
        /// </summary>
        /// <returns></returns>
        private Series GetDataSource()
        {
            //SeriesCollection SC = new SeriesCollection();
            //Random rd = new Random();
            DataSet ds = this._ds;


            //生成单一图,将返回的DataTable数据处理成序列集合类型的数据，以便保持数据源类型的统一
            Series s = new Series();
            for (int b = 0; b < ds.Tables[0].Rows.Count; b++) //X轴尺度个数，如一年12个月表示有12个尺度数
            {

                s.Name = ds.Tables[0].Rows[b][0].ToString();
                s.Type = this._type;
                s.Data = ds.Tables[0];
                Element e = new Element();
                e.Name = ds.Tables[0].Rows[b][0].ToString();//对应于X轴个尺度的名称

                e.YValue = Convert.ToInt32(ds.Tables[0].Rows[b][1]);//与X轴对应的Y轴的数值

                s.Elements.Add(e);


            }
            //  chart.Series.Data = this._ds.Tables[0];
            // chart.SeriesCollection.Add();

            // 生成对比图
            //for (int a = 0; a < ds.Tables.Count; a++) //对比的项数，如2008年各月的公交和地铁载客量数据对比就相当于有两个数据项
            //{
            //    Series s = new Series();
            //    s.Name = ds.Tables[a].TableName;//各个数据项代表的名称，如公交和地铁12个月载客量走势图，则一条表示公交，一条表示地铁
            //    for (int b = 0; b < ds.Tables[a].Rows.Count; b++) //X轴尺度个数，如12个月表示有12个尺度数
            //    {
            //        Element e = new Element();
            //        e.Name = ds.Tables[a].Rows[b][0].ToString();//对应于X轴个尺度的名称
            //        e.YValue = Convert.ToInt32(ds.Tables[a].Rows[b][1]);//与X轴对应的Y轴的数值
            //        s.Elements.Add(e);
            //    }
            //    SC.Add(s);
            //}


            //可自定义填充图的填充色，系统采取默认分配各数据项的填充色
            //SC[0].DefaultElement.Color = Color.Blue;
            //SC[1].DefaultElement.Color = Color.Red;
            //SC[2].DefaultElement.Color = Color.FromArgb(255, 99, 49);
            //SC[3].DefaultElement.Color = Color.FromArgb(0, 156, 255);
            return s;
        }



        /// <summary>
        /// 生成统计图片的数据源模型(单一或对比图都可以)
        /// </summary>
        /// <returns></returns>
        private SeriesCollection GetDataSources()
        {
            SeriesCollection SC = new SeriesCollection();
            // Random rd = new Random();
            DataSet ds = this._ds;
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
            for (int a = 0; a < ds.Tables.Count; a++) //对比的项数，如2008年各月的公交和地铁载客量数据对比就相当于有两个数据项
            {
                Series s = new Series();
                s.Name = ds.Tables[a].TableName;//各个数据项代表的名称，如公交和地铁12个月载客量走势图，则一条表示公交，一条表示地铁

                for (int b = 0; b < ds.Tables[a].Rows.Count; b++) //X轴尺度个数，如12个月表示有12个尺度数
                {
                    Element e = new Element();
                    e.Name = ds.Tables[a].Rows[b][0].ToString();//对应于X轴个尺度的名称
                    e.YValue = Convert.ToInt32(ds.Tables[a].Rows[b][1]);//与X轴对应的Y轴的数值
                    //e.Annotation = new Annotation(ds.Tables[a].Rows[b][0].ToString());
                    total += e.YValue;
                    e.ShowValue = true;
                    //e.Marker = new ElementMarker(ElementMarkerType.Circle,100);
                    s.Elements.Add(e);
                }

                SC.Add(s);
               // GetDataed(this,(EventArgs)this._ds );
            }


            //可自定义填充图的填充色，系统采取默认分配各数据项的填充色
            //SC[0].DefaultElement.Color = Color.Blue;
            //SC[1].DefaultElement.Color = Color.Red;
            //SC[2].DefaultElement.Color = Color.FromArgb(255, 99, 49);
            //SC[3].DefaultElement.Color = Color.FromArgb(0, 156, 255);
            return SC;
        }
        #endregion



    }
}