using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace YZWBSM
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ClsConstant.sWb_Id = "10102120";
            ClsConstant.GetXmlInfo();
            //初始化数据表表头
            ClsConstant.ColumnInit();

            //接收局域网内所有电脑的数据日期，保存于数据表中
            Thread tUdpThreadServer;
            ClsInfoListen sGetAllDay = new ClsInfoListen();
            tUdpThreadServer = new Thread(new ThreadStart(sGetAllDay.StartListenNewDay));
            tUdpThreadServer.IsBackground = true;
            tUdpThreadServer.Start();

            //程序数据更新流程

            //1、从服务器下载最新广告发布日期
            ClsConstant.Getnewday();

            //2、与本机数据日期进行比较，如果相同，则跳到显示广告画面，不同继续执行以下步骤
            ClsConstant.CompareLocalNewDay();
            if (ClsConstant.bCompareNewD)//本机数据非最新
            {
                ClsConstant.bCompareNewD = false;
                ClsConstant.CompareLanNewDay();//比较局域网数据
                if (ClsConstant.bCompareNewD)
                {
                    //从局域网下载数据


                }
                else
                {
                   //从联盟服务器下载数据
                    ClsConstant.GetXmlInfo();   //下载数据信息文件及图片
                }
            }

            //向局域网广播自己的最新数据
            Thread tUdpThreadClient;
            ClsBroadCast sSendAllDay = new ClsBroadCast();
            tUdpThreadClient = new Thread(new ThreadStart(sSendAllDay.StartSendNewDay));
            tUdpThreadClient.IsBackground = true;
            tUdpThreadClient.Start();

            //启动广告显示画面
            Application.Run(new Form1());
        }
    }
}