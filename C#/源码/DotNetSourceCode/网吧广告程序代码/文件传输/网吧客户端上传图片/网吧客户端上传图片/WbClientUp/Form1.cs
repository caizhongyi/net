using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WbClient;
using System.Threading;
using WbService;

namespace WbClientUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //InitializeComponent();
            //必填
            Form1.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IConnectionService iconn = new Client();
            Thread td = iconn.GetConnection(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //发送
            ClientInfo ci = new ClientInfo();
            ci.Prot = 8001;
            ci.Host = "192.168.1.10";

            //侦听
            ServiceInfo si = new ServiceInfo();
            si.Port = 7999;

            IService isce = new WbService.Service();
            Thread td = isce.ServiceStart();
        }
    }
}