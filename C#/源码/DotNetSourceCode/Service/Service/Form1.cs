using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Service
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(NewMethod));
            t.Start();

        }

        private void NewMethod()
        {
            //只能用UDP协议发送广播，所以ProtocolType设置为UDP
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //让其自动提供子网中的IP地址
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 8899);
            //设置broadcast值为1，允许套接字发送广播信息
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            //将发送内容转换为字节数组
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(this.textBox1.Text);
            //向子网发送信息
            socket.SendTo(bytes, iep);
            socket.Close();
        }
    }
}