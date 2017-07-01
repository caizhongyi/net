//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Web;
//using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
//using wsChart;

//namespace czy.MyClass.DrawImage
//{
//    /// <summary>
//    /// NormalPole 的摘要说明。
//    /// </summary>
//    public class WsChart
//    {
//        private void DrawChart(Page page)
//        {
//            // 在此处放置用户代码以初始化页面
//            //注意：

//            cChart obj = new cChart();
//            obj = (wsChart.cChart)page.Server.CreateObject("wsChart.cChart");   //创建Com对象


//            obj.Width = 500;   //图表宽度
//            obj.Height = 350;  //图表高度
//            obj.BackGrid = true; //是否显示图表背景网格
//            obj.BorderOffset = 5; //图表画布边界距离
//            obj.LegendBackgroundColor = (int)ColorTranslator.ToWin32(Color.FromArgb(210, 210, 210));   //图例背景色

//            obj.LegendWidth = 80;   //图例宽度
//            obj.LegendTitle = "产品";  //图例标题
//            //obj.LegendTitleBond=false;  //图例标题是否是粗体
//            obj.SerialBars = 4;    //项目数
//            obj.SerialItems = 4;   //一个项目的子项数
//            obj.BarSpaceRate = 0.7F;  //柱型占一个项目的空间比率
//            obj.DrawBarMode = (wsChart.iDrawBarMode)1;   //图表模式

//            obj.AxisXoutValue = 10;   //图表横轴线延长距离
//            obj.LeftRulerHeight = 5;   //左轴刻度线厚度，可为负值
//            obj.BottomSRulerHeight = 5;  //横轴刻度线厚度，可为负值


//            obj.MarginTop = 35; //图表顶边距
//            obj.MarginLeft = 50; //图表左边距
//            obj.MarginBottom = 40; //图表底边距
//            obj.MarginRight = 20; //图表右边距

//            obj.Chart_3D = true;  //以3D形式显示
//            obj.Angle_3D = 3.1415926F / 4F;  //3D显示角度
//            obj.Depth_3D = 10;  //3D显示厚度

//            string[] arrBarName = { "2001", "2002", "2003", "2004" };  //横轴项目文字



//            obj.InitChart();   //初始化图表
//            obj.DrawBarChart(300, 10, 0F, 0, "", true,0,0);  //画左轴
//            obj.DrawChartTiTle("家电产品销售统计分析图表", "宋体", 24, true, (int)ColorTranslator.ToWin32(Color.FromArgb(0, 0, 0)), 0, 0F, 0F); //画图表标题
//            obj.DrawAreaText(arrBarName, 0, "宋体", 14, false, (int)ColorTranslator.ToWin32(Color.FromArgb(0, 0, 0)), 0, 0, 5); //画横轴项目文字
//            obj.DrawText("销售量(万台)", "宋体", 14, true, (int)ColorTranslator.ToWin32(Color.FromArgb(0, 0, 0)), 90, 5, 125, 1); //画左轴文字
//            obj.DrawText("年份", "宋体", 14, true, (int)ColorTranslator.ToWin32(Color.FromArgb(0, 0, 0)), 0, 200, 330, 1); ////画横轴文字


//            obj.FillColor = (int)ColorTranslator.ToWin32(Color.FromArgb(255, 0, 0));  //第一小子项填充色
//            obj.AddBarData("彩电", 200F, 1, 1, "", true);   //增加数据，第一大项目第一小子项
//            obj.AddBarData("彩电", 250F, 2, 1, "", true);   //增加数据，第二大项目第一小子项
//            obj.AddBarData("彩电", 270F, 3, 1, "", true);   //增加数据，第三大项目第一小子项
//            obj.AddBarData("彩电", 290F, 4, 1, "", true);   //增加数据，第四大项目第一小子项

//            obj.FillColor = (int)ColorTranslator.ToWin32(Color.FromArgb(255, 100, 255)); //第二小子项填充色
//            obj.AddBarData("冰箱", 150F, 1, 2, "", true);   //增加数据，第一大项目第二小子项
//            obj.AddBarData("冰箱", 220F, 2, 2, "", true);
//            obj.AddBarData("冰箱", 180F, 3, 2, "", true);
//            obj.AddBarData("冰箱", 270F, 4, 2, "", true);

//            obj.FillColor = (int)ColorTranslator.ToWin32(Color.FromArgb(0, 255, 255));
//            obj.AddBarData("空调", 240F, 1, 3, "", true);
//            obj.AddBarData("空调", 180F, 2, 3, "", true);
//            obj.AddBarData("空调", 50F, 3, 3, "", true);
//            obj.AddBarData("空调", 190F, 4, 3, "", true);

//            obj.FillColor = (int)ColorTranslator.ToWin32(Color.FromArgb(255, 255, 0));
//            obj.AddBarData("洗衣机", 150F, 1, 4, "", true);
//            obj.AddBarData("洗衣机", 120F, 2, 4, "", true);
//            obj.AddBarData("洗衣机", 190F, 3, 4, "", true);
//            obj.AddBarData("洗衣机", 50F, 4, 4, "",true);

//            string strPath;
//            strPath =page. Server.MapPath("wsChart_bar.gif");   //输出图片路径
//            obj.makeChart(strPath);   //输出图片

//            obj.Destroy();  //回收系统资源
//            obj = null;  //清除对象


//        }
//    }
//}
