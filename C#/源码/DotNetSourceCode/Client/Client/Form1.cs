using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //AcceptMessage(),在未接收广播信息之前，处于阻塞状态，不会生成form
            AcceptMessage();

        }

        //接收信息
        private void AcceptMessage()
        {
            //d定义socket对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 8899);
            socket.Bind(iep);
            ep = (EndPoint)iep;
            byte[] bytes = new byte[1024];
            while (true)
            {
                socket.ReceiveFrom(bytes, ref ep);
                receiveData = System.Text.Encoding.Unicode.GetString(bytes);
                receiveData = receiveData.TrimEnd('\u0000');
                Thread th = new Thread(new ThreadStart(Acc));
                th.Start();
                //th.Abort();

            }
            socket.Close();
        }
        private void Acc()
        {
            string message = "来自" + ep.ToString() + "的消息";
            DialogResult result = MessageBox.Show(receiveData, message, MessageBoxButtons.AbortRetryIgnore);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}