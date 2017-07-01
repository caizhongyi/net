using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace czy.MyClass.Communications
{
  

    public class SocketAsyServer
    {
        #region 私有成员
        private Socket socket;
        private SocketAsyncEventArgs acceptEventArgs;
        System.IO.MemoryStream MSWriter;
        System.IO.MemoryStream MSReader;
        private SocketAsyEventArgs socketAsyEventArgs;
        private UserObject _serverUserObject;
        private bool _isStart = true;
        private int _maxCount = 1024;
        private object lock_obj=new object ();
        #endregion

        #region 事件
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="o">当前对像</param>
        /// <param name="e">SocketAsyEventArgs</param>
        public delegate void SocketAsyEventHandle(object o, SocketAsyEventArgs e);
        /// <summary>
        /// 发送中事件
        /// </summary>
        public event SocketAsyEventHandle Sending;
        /// <summary>
        /// 发送完成事件
        /// </summary>
        public event SocketAsyEventHandle Sended;
        /// <summary>
        /// 接收中事件
        /// </summary>
        public event SocketAsyEventHandle Receiving;
        /// <summary>
        /// 接收完成事件
        /// </summary>
        public event SocketAsyEventHandle Received;
        /// <summary>
        /// 绑定事件
        /// </summary>
        public event SocketAsyEventHandle IPBinding;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="port">端口</param>
        public SocketAsyServer(int port)
        {
          //  socketAsyEventArgs = new SocketAsyEventArgs(port,IPAddress.Any.ToString ());
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
         
        }
        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="maxConnectCount"></param>
        public void Listen(int maxConnectCount)
        {
            _maxCount=maxConnectCount;
            socket.Listen(_maxCount);
            if (IPBinding != null) { IPBinding(this, socketAsyEventArgs); }
            //  Console.WriteLine("Server is bind on {0}", socket.LocalEndPoint);
            acceptEventArgs = new SocketAsyncEventArgs();//创建接入Socket上下文对象
        
            acceptEventArgs.Completed += acceptCompleted;//注册接入完成事件处理程序
           
            socket.AcceptAsync(acceptEventArgs);//投递接入操作

            //  Console.WriteLine("Server is started");
        }
        public void Stop()
        {
            this._isStart = false;
        }
   

        //接入事件处理程序
        void acceptCompleted(object sender, SocketAsyncEventArgs e)
        {
          
            var client = new UserObject(e.AcceptSocket);//创建用户对象实例
            e.AcceptSocket.SendBufferSize = Convert .ToInt32(client.BuffSize);
            e.AcceptSocket.ReceiveBufferSize = Convert.ToInt32(client.BuffSize);
            client.ReceiveEventArgs.Completed += Receives_Completed;//注册接收数据完成事件处理程序
            client.SendEventArgs.Completed += Send_Completed;//注册发送数据完成事件处理程序
            client.Socket.ReceiveAsync(client.ReceiveEventArgs);//投递接收数据操作
            if (this._isStart)
            {
                Listen(this._maxCount);
            }
        }

        //发送数据完成事件处理程序
        void Send_Completed(object sender, SocketAsyncEventArgs e)
        {
            var client = e.UserToken as UserObject; 
            if (e.BytesTransferred == 0)//如果传输的数据量为0，则表示链接已经断开
            {
              //  Console.WriteLine("Socket:{0} is closed", client.Socket.Handle);
                  SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                  MyIPAddress.IPHelper.GetRemoteEndPointAddress(client.Socket.RemoteEndPoint),
                  MyIPAddress.IPHelper.GetRemoteEndPointPort(client.Socket.RemoteEndPoint),
                  IPAddress.Any.ToString(),
                  client.ContentLength,
                  client.CurrentLength,
                  e.Buffer,
                  client.Socket
                  );
                  client.Socket.Close ();
                 
                  client.orderType = SocketEnum.OrderType.None;
                  if (Sended != null) { Sended(this, socketAsyEventArgs); }
            }
            else
            {

                  SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                  MyIPAddress.IPHelper.GetRemoteEndPointAddress(client.Socket.RemoteEndPoint),
                  MyIPAddress.IPHelper.GetRemoteEndPointPort(client.Socket.RemoteEndPoint),
                  IPAddress.Any.ToString(),
                  client.ContentLength,
                  client.CurrentLength,
                  e.Buffer,
                  client.Socket
                  );
                  if (Sending != null) { Sended(this, socketAsyEventArgs); }
                  client.Socket.ReceiveAsync(client.ReceiveEventArgs);//投递接收数据操作
             //   Console.WriteLine("Sent {0} bytes data to socket:{1}", e.BytesTransferred, client.Socket.Handle);
            }
        }

        //接收数据完成事件处理程序
        void Receives_Completed(object sender, SocketAsyncEventArgs e)
        {
            var client = e.UserToken as UserObject;
            Message msg = new Message();
            string message = string.Empty;
            msg.Buffer = e.Buffer;
            client.Param = Encoding.Unicode.GetString(msg.Param).Replace("\0","");
            client.ContentLength = BitConverter.ToInt64(msg.FileLength, 0);
            FileStream fileWriteStream=null;
            FileStream fileTempWrite = null;


            if (e.BytesTransferred == 0)//如果传输的数据量为0，则表示链接已经断开
            {
                //Console.WriteLine("Socket:{0} is closed", client.Socket.Handle);
                SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                   MyIPAddress.IPHelper.GetRemoteEndPointAddress(client.Socket.RemoteEndPoint) ,
                   MyIPAddress.IPHelper.GetRemoteEndPointPort(client.Socket.RemoteEndPoint),
                   IPAddress.Any.ToString(),
                   client.ContentLength,
                   client.CurrentLength,
                   e.Buffer,
                   client.Socket
                   );
               
                if (Received != null) { Received(this, socketAsyEventArgs); }
                client.Socket.Close ();
                if (fileWriteStream!=null) fileWriteStream.Close();
                if (fileTempWrite != null) fileTempWrite.Close();
                client.orderType = SocketEnum.OrderType.None;
            }
            else
            {
             
                //Console.WriteLine("Socket:{0} send message:{1}", client.Socket.Handle, message);
              
                client.orderType = BitConverter.ToUInt32(msg.Command,0);
                switch (client.orderType)
                {
                    case SocketEnum.OrderType.None:  break;
                    case SocketEnum.OrderType.DownLoadFile:break;
                    case SocketEnum.OrderType.SendString: 
             
                        message = Encoding.Unicode.GetString(msg.BodyBuffer);
                        break;
                    case SocketEnum.OrderType.SendFile:
                            Message sendMsg = new Message();
                            sendMsg.Command=BitConverter.GetBytes(SocketEnum.OrderType.BeginSendFile);
                            sendMsg.FileLength=msg.FileLength;
                            sendMsg.Param = msg.Param;
                            //节点记录流
                            fileTempWrite = fileTempWrite == null ? new FileStream(client.Param.Substring(0, client.Param.LastIndexOf('.')) + ".tmp", FileMode.OpenOrCreate) : fileTempWrite;
                               
                            if (fileTempWrite.Length != 0)
                            {
                                byte[] bytes = new byte[8];
                                fileTempWrite.Read(bytes, 0, bytes.Length);
                                client.CurrentLength = BitConverter.ToInt64(bytes, 0);
                            }
                            else
                            {
                                client.CurrentLength = 0;
                            }
                            sendMsg.FileCurrentLength =BitConverter.GetBytes(client.CurrentLength);
                            client.SendEventArgs.UserToken = client;
                            client.SendEventArgs.SetBuffer(sendMsg.Buffer, 0, sendMsg.Buffer.Length);
                            client.Socket.SendAsync(client.SendEventArgs);//投递发送数据操作
                            if(fileTempWrite!=null)fileTempWrite.Close();
                            break;
                    case SocketEnum.OrderType.BeginSendFile:
         
                           // if (MSWriter == null) { MSWriter = new MemoryStream(50); }
                                  //文件写入流
                                if (client.CurrentLength >= client.ContentLength)
                                {
                                    if (File.Exists(client.Param.Substring(0, client.Param.LastIndexOf('.')) + ".tmp"))
                                    {
                                        File.Delete(client.Param.Substring(0, client.Param.LastIndexOf('.')) + ".tmp");
                                    }
                                    client.orderType = SocketEnum.OrderType.None;
                                }
                                else
                                {
                                
                                    fileWriteStream = fileWriteStream == null ? new FileStream(client.Param, FileMode.OpenOrCreate) : fileWriteStream;
                                    //节点记录流
                                    fileTempWrite = fileTempWrite == null ? new FileStream(client.Param.Substring(0, client.Param.LastIndexOf('.')) + ".tmp", FileMode.OpenOrCreate) : fileTempWrite;
                                    //if (fileTempWrite.Length != 0)
                                    //{
                                    //    byte[] bytes = new byte[8];
                                    //    fileTempWrite.Read(bytes, 0, bytes.Length);
                                    //    client.CurrentLength = BitConverter.ToInt64(bytes, 0);
                                    //}
                                    //else
                                    //{
                                    //    client.CurrentLength = 0;
                                    //}

                                    if (msg.BodyBuffer.Length + client.CurrentLength > client.ContentLength)
                                    {
                                        msg.BodyBuffer = msg.BodyBuffer.SubBytes(0, Convert .ToInt32(  client.ContentLength - client.CurrentLength));
                                    }

                                    //设定读取点
                                    fileWriteStream.Seek(client.CurrentLength, SeekOrigin.Begin);
                                    fileWriteStream.Write(msg.BodyBuffer, 0, msg.BodyBuffer.Length);
                                    client.CurrentLength += msg.BodyBuffer.Length;
                                    //写入记录节点
                                    byte[] cbytes = new byte[8];
                                    cbytes = BitConverter.GetBytes(client.CurrentLength);
                                    fileTempWrite.Seek(0, SeekOrigin.Begin);
                                    fileTempWrite.Write(cbytes, 0, cbytes.Length);
                                    //释放
                                    if (fileWriteStream != null) fileWriteStream.Close();
                                    if (fileTempWrite != null) fileTempWrite.Close();
                                    //当长度大于等总长时

                                    Message beginSendMsg = new Message();
                                    beginSendMsg.Command = BitConverter.GetBytes(SocketEnum.OrderType.BeginSendFile);
                                    beginSendMsg.FileLength = msg.FileLength;
                                    beginSendMsg.Param = msg.Param;
                                    beginSendMsg.FileCurrentLength = BitConverter.GetBytes(client.CurrentLength);
                                    client.SendEventArgs.UserToken = client;
                                    client.SendEventArgs.SetBuffer(beginSendMsg.Buffer, 0, beginSendMsg.Buffer.Length);
                                    client.Socket.SendAsync(client.SendEventArgs);//投递发送数据操作
                                  
                                }
                            
                        break;
                    default:  break;
                }


                SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                        MyIPAddress.IPHelper.GetRemoteEndPointAddress(client.Socket.RemoteEndPoint),
                        MyIPAddress.IPHelper.GetRemoteEndPointPort(client.Socket.RemoteEndPoint),
                        IPAddress.Any.ToString(),
                        client.ContentLength,
                        client.CurrentLength,
                        e.Buffer,
                        client.Socket
                        );
                    if (Receiving != null) {Receiving(this, socketAsyEventArgs);  }
                   
                 
                   
                

            }
        }



        /// <summary>
        /// 服务器Socket关闭
        /// </summary>
        public void Close()
        {
            socket.Close();
            //socket.Shutdown(SocketShutdown.Both);
        }


    }
}
