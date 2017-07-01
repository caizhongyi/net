using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace WbClient
{
    public class Client:IConnectionService
    {
        #region 字段
        private Socket skClient;
        private IPEndPoint ipep;
        private string[] MyFileName;
        private string filepath;
        #endregion

        //初始化类名

        ClientInfo clientinfo = new ClientInfo();
        void Socketconnction()
        {
            skClient.Connect(ipep);
        }
        void SentFile()
        {
            string mge = "ODAT" + filepath;

            //将mge转化成流进行传输
            skClient.Send(Encoding.Default.GetBytes(mge));

            //定义一个读文件流
            FileStream filestream = new FileStream(filepath,FileMode.Open,FileAccess.Read);
            //设置缓冲区为1024byte
            byte[] buff=new byte[1024];
            int lenth = 0;
            try
            {
                while ((lenth = filestream.Read(buff, 0, 1024))!=0)
                {
                    //按原本字节总量进行传送
                    skClient.Send(buff, 0, lenth, SocketFlags.None);

                }
                //将要发送信息的最后加上"END"标识符
                mge = "END";
                //发送mge开始
                skClient.Send(Encoding.Default.GetBytes(mge));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接
                skClient.Close();
                filestream.Close();
            }

        }

        void ConnectionService()
        {
            //找到ScreenPricture文件名
            string Foldpath = Directory.GetCurrentDirectory() + "\\" +"Adpictures";
            //读取所有文件
            MyFileName = Directory.GetFiles(Foldpath);
            foreach (string filename in MyFileName)
            {
                filepath = filename;
                try
                {
                    //////设置端口号
                    //int port = clientinfo.Prot;
                    ////获取ip
                    //string host = clientinfo.Host;
                    //第一步：用指定的端口号和服务器的ip建立一个EndPoint对像；
                    IPAddress ip = IPAddress.Parse(clientinfo.Host);
                    ipep = new IPEndPoint(ip, 8999);

                    //第二步：建立一个Socket对像；
                    skClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    //第三步：用socket对像的Connect()方法以上面建立的EndPoint对像做为参数，向服务器发出连接请求；
                    ClassSocket socketConnet = new ClassSocket(skClient, ipep);
                    Thread tconnection = new Thread(new ThreadStart(Socketconnction));
                    tconnection.Start();
                    Thread.Sleep(500);
                    //将要上传的文件加个DAT标识

                    Thread tsent = new Thread(new ThreadStart(SentFile));
                    tsent.Start();

                    Thread.Sleep(1000);
                }
                catch (SocketException ex)
                {
                    throw ex;
                }
            }
        }

        #region IConnectionService 成员

        public Thread GetConnection()
        {
            Thread td = new Thread(new ThreadStart(ConnectionService));
            td.IsBackground = true;
            td.Start();
            return td;
        }

        #endregion
    }
}
