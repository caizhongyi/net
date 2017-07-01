using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;

namespace FlyMessage
{
    public partial class FrmMain : Form
    {
        //定义作为服务器端接受信息套接字
        public Socket socketReceive = null;

        //定义作为客户端发送信息套接字
        public Socket socketSent = null;

        //定义接受信息的IP地址和端口号
        public IPEndPoint ipReceive = null;

        //定义发送信息的IP地址和端口号
        public IPEndPoint ipSent = null;

        //定义接受信息的套接字
        public Socket chat = null;
       
        //定义是否封装
        public string  pack = "0";

        public static Thread tBroadCast;
        public FrmMain()
        {
            FrmMain.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ClassStartUdpThread startUdpThread = new ClassStartUdpThread(this.lvwDisplayUser,this.lblUserCount);
            Thread tUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            tUdpThread.IsBackground = true;
            tUdpThread.Start();

            ClassBroadCast broadCast = new ClassBroadCast();
            Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
            tBroadCast.IsBackground=true;
            tBroadCast.Start();
           
            Thread receive = new Thread(new ThreadStart(ReceiveNews));
            receive.IsBackground = true;
            receive.Start();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < this.lvwDisplayPerson.Items.Count; i++)
            //{
            //    this.lvwDisplayPerson.Items.RemoveAt(i);
            //}
            this.lvwDisplayUser.Items.Clear();

        }

        private void btnSentMessage_Click(object sender, EventArgs e)
        {
            string msg = "MSG " + txtMessage.Text;


            for (int i = 0; i < lvwDisplayUser.SelectedItems.Count; i++)
            {
                try
                {
                    string ip = this.lvwDisplayUser.SelectedItems[i].SubItems[2].Text;
                    
                    //初始化接受套接字：寻址方案，以字符流方式和Tcp通信
                    socketSent = new Socket(AddressFamily.InterNetwork,
                                   SocketType.Stream,
                                   ProtocolType.Tcp);

                    //设置服务器IP地址和端口
                    ipSent = new IPEndPoint(IPAddress.Parse(ip), 8001);


                    //与服务器进行连接
                    socketSent.Connect(ipSent);
                    
                    //是否封装
                    socketSent.Send(Encoding.Default.GetBytes(pack));


                    //将要发送的消息转化为字节流，然后发送

                    socketSent.Send(Encoding.Default.GetBytes(msg));

                    socketSent.Close();

                }
                catch
                {
                    MessageBox.Show(this.lvwDisplayUser.SelectedItems[i].SubItems[0].Text + "已经下线！");
                }
            }
            
          
        }
        /// <summary>
        /// 处理接受到的信息，分别对文件和普通消息进行处理
        /// </summary>
        private void ReceiveNews()
        {
            try
            {
                //初始化接受套接字：寻址方案，以字符流方式和Tcp通信
                socketReceive = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream,
                 ProtocolType.Tcp);

                //获取本机IP地址并设置接受信息的端口
                ipReceive = new IPEndPoint(
                  Dns.GetHostEntry(Dns.GetHostName()).AddressList[0],
                  8001);

                //将本机IP地址和接受端口绑定到接受套接字
                socketReceive.Bind(ipReceive);

                //监听端口，并设置监听缓存大小为1024byte
                socketReceive.Listen(1024);
            }
            catch(Exception err) 
            {
                MessageBox.Show(err.Message);
            }

            //定义接受信息时缓冲区
            byte[] buff = new byte[1024];

            //连续接受客户端发送过来的信息
           
            while (true)
            {
                //定义一个chat套接字用来接受信息
                Socket chat = socketReceive.Accept();

               
                //定义一个处理信息的对象
                ChatSession cs = new ChatSession(chat);

                //定义一个新的线程用来接受其他主机发送的信息
                Thread newThread = new Thread(new ThreadStart(cs.StartChat));

                //启动新的线程
                newThread.Start();
            }

        }

        private void btnSentFile_Click(object sender, EventArgs e)
        {
           
            //打开文件
            OpenFileDialog dlg = new OpenFileDialog();
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                for (int i = 0; i < lvwDisplayUser.SelectedItems.Count; i++)
                {

                       string ip = this.lvwDisplayUser.SelectedItems[i].SubItems[2].Text;
                      //初始化接受套接字：寻址方案，以字符流方式和Tcp通信
                       socketSent = new Socket(AddressFamily.InterNetwork,
                              SocketType.Stream,
                              ProtocolType.Tcp);

                       //设置服务器IP地址和端口
                       ipSent = new IPEndPoint(IPAddress.Parse(ip), 8001);
                       //与服务器进行连接

                       ClassSocket socketConnet = new ClassSocket(socketSent, ipSent);
                       Thread tConnection = new Thread(new ThreadStart(socketConnet.SocketConnect));
                       tConnection.Start();

                       Thread.Sleep(100);
                       //将要发送的文件加上"DAT"标识符

                       ClassSentFile sentFile = new ClassSentFile(dlg, socketSent);
                       Thread tSentFile=new Thread(new ThreadStart(sentFile.SentFile));
                       tSentFile.Start();
                }
            }
        }



        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }

        private void SetUp_Click(object sender, EventArgs e)
        {
            FrmSetUp setUp = new FrmSetUp();
            setUp.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
        }

        private void cbPack_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbPack.Checked == true)
                pack = "1";
            else
                pack = "0";
        }

        private void Help_Click(object sender, EventArgs e)
        {
            FrmHelp help = new FrmHelp();
            help.Show();
        }

       
     
    }
}