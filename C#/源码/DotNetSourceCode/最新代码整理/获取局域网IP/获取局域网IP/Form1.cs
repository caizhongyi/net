using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ClassStartUdpThreadnamespace;

namespace DataKeepInfo
{
    public partial class Form1 : Form
    {
        public static Thread tUdpThread;
        public Form1()
        {
            Form1.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //获取其他机子IP
            ClassStartUdpThread startUdpThread = new ClassStartUdpThread();
            tUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            tUdpThread.IsBackground = true;
            tUdpThread.Start();

            //定义加载本机的IP
            ClassBroadCast broadCast = new ClassBroadCast();
            Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
            tBroadCast.IsBackground = true;
            tBroadCast.Start();
        }
    }
}