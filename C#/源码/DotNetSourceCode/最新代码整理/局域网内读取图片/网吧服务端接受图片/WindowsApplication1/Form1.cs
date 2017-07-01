using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WbService;
using System.Threading;
using WbClient;
using System.Net;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //必填
            Form1.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //IConnectionService iconn = new Client();
            //Thread td = iconn.GetConnection(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //发送IP地址
            SendInfo.SendInfo sinfo = new SendInfo.SendInfo();
            sinfo.SendIpAdress();
            //接收IP
            ListenInfo.ListenInfo Linfo = new WindowsApplication1.ListenInfo.ListenInfo();
            Thread td = new Thread(new ThreadStart(Linfo.ListenStart));
            td.IsBackground = true;
            td.Start();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}