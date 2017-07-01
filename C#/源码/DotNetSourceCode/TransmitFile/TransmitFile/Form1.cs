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
using System.IO;
using WbClient;

namespace TransmitFile
{
    public partial class Form1 : Form
    {
        private Socket socketSent;
        private IPEndPoint ipSent;
        private string[] Myfilename = null;
        private string filepath = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(new ThreadStart(timestat));
            td.Start();
        }

        private void ClassSocket(Socket socketSent, IPEndPoint ipSent)
        {
            this.socketSent = socketSent;
            this.ipSent = ipSent;
        }
        private void SocketConnect()
        {
            socketSent.Connect(ipSent);
        }
        public void SentFile()
        {

                string msg = "0DAT " + filepath;

                //将 "msg" 转化为字节流的形式进行传送
                socketSent.Send(Encoding.Default.GetBytes(msg));

                //定义一个读文件流
                FileStream read = new FileStream(filepath, FileMode.Open, FileAccess.Read);

                //设置缓冲区为1024byte
                byte[] buff = new byte[1024];
                int len = 0;
                while ((len = read.Read(buff, 0, 1024)) != 0)
                {
                    //按实际的字节总量发送信息
                    socketSent.Send(buff, 0, len, SocketFlags.None);
                }

                //将要发送信息的最后加上"END"标识符
                msg = "END";

                //将 "msg" 发送
                socketSent.Send(Encoding.Default.GetBytes(msg));

                socketSent.Close();
                read.Close();
     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Thread td = new Thread(new ThreadStart(timestat));
            //td.Start();
        }
        private void timestat()
        {
            string filepathaa = @"C:\WINDOWS\system32\ScreenPricture";
            Myfilename = System.IO.Directory.GetFiles(filepathaa);
            foreach (string file in Myfilename)
            {
                string name = file.Substring(35).ToString();
                filepath = @"C:\WINDOWS\system32\ScreenPricture\" + name;

            string ip = "192.168.1.10";
            //初始化接受套接字：寻址方案，以字符流方式和Tcp通信

            socketSent = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //设置服务器IP地址和端口
            ipSent = new IPEndPoint(IPAddress.Parse(ip), 8001);
            //与服务器进行连接
            Thread tConnection = new Thread(new ThreadStart(SocketConnect));
            tConnection.Start();
            //Thread.Sleep(10);
            //将要发送的文件加上"DAT"标识符
            Thread tSentFile = new Thread(new ThreadStart(SentFile));
            tSentFile.Start();
            Thread.Sleep(500);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IConnectionService ic=new WbClient.Client();
            Thread td = ic.GetConnection();
        }
    }
}