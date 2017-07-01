using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace czy.MyClass.Communications
{
    public class SocketAsyClient
    {
        #region 私有成员
       
        private uint orderType = SocketEnum.OrderType.SendString;
        private string clientIP = "127.0.0.1";
        private string serverIP = "0.0.0.0";
        private int port = 2000;
        /// <summary>
        /// 自定义对像
        /// </summary>
        private UserObject sendUserObject;
        private UserObject receiveUserObject;
        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream fileStreamRead=null;
        private FileStream fileStreamWrite = null;
        private FileStream fileTempWrite = null;
        private FileStream fileTempRead = null;
        private Message msg;
        #endregion

        #region 事件
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="o">SocketAsyClient对像</param>
        /// <param name="e">SocketAsyEventArgs参数</param>
        public delegate void SocketAsyEventHandle(object o, SocketAsyEventArgs e);
        /// <summary>
        /// 正在发送
        /// </summary>
        public event SocketAsyEventHandle Sending;
        /// <summary>
        /// 发送完成
        /// </summary>
        public event SocketAsyEventHandle Sended;
        /// <summary>
        /// 接收完成
        /// </summary>
        public event SocketAsyEventHandle Receiving;
        /// <summary>
        /// 发送完成
        /// </summary>
        public event SocketAsyEventHandle Received;
        /// <summary>
        /// 断开链接
        /// </summary>
        public event SocketAsyEventHandle DisContented;
        /// <summary>
        /// 已链接
        /// </summary>
        public event SocketAsyEventHandle Contented;
        #endregion

        #region 属性
        
        /// <summary>
        /// 链接状态
        /// </summary>
        public bool Closed { get; private set; }
        /// <summary>
        /// 发送缓存
        /// </summary>
        public byte[] ReceiveBuffer { get; private set; }
        /// <summary>
        /// 接收缓存
        /// </summary>
        public byte[] SendBuffer { get; private set; }
        /// <summary>
        /// Socket对像
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// 异步发送参数
        /// </summary>
        public SocketAsyncEventArgs SendEventArgs { get; private set; }
        /// <summary>
        /// 异步接收参数
        /// </summary>
        public SocketAsyncEventArgs ReceiveEventArgs { get; private set; }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ip">链接的服务器IP</param>
        /// <param name="port">端口</param>
        /// <param name="messageType">类型</param>
        public SocketAsyClient(string ip, int port)
        {
            this.serverIP = ip;
            this.port = port;
            Closed = false;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SendEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs = new SocketAsyncEventArgs();

            sendUserObject = new UserObject(Socket);
            receiveUserObject = new UserObject(Socket);
            ReceiveBuffer = new byte[receiveUserObject.BuffSize];
            SendBuffer = new byte[sendUserObject.BuffSize];
            Socket.SendBufferSize = Convert.ToInt32(sendUserObject.BuffSize);
            Socket.ReceiveBufferSize = Convert.ToInt32(receiveUserObject.BuffSize);
            ReceiveEventArgs.Completed += Connect_Completed;
            SendEventArgs.Completed += Send_Completed;
            SendEventArgs.SetBuffer(SendBuffer, 0, SendBuffer.Length);
            ReceiveEventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        }
        /// <summary>
        /// 链接
        /// </summary>
        public void Content()
        {
            if (!Socket.Connected)
            {
                Socket.ConnectAsync(ReceiveEventArgs);

            }



        }

        void Connect_Completed(object send, SocketAsyncEventArgs e)
        {
            // Console.WriteLine("Is connected to server {0}", e.RemoteEndPoint);
            SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs();
            if (Socket.Connected)
            {
                if (Contented != null) { Contented(this, socketAsyEventArgs); }

                ReceiveEventArgs.Completed -= Connect_Completed;
                ReceiveEventArgs.SetBuffer(ReceiveBuffer, 0, ReceiveBuffer.Length);
                ReceiveEventArgs.Completed += Receive_Completed;
                
            }
            else
            {
                if (DisContented != null) { DisContented(this, socketAsyEventArgs); }
            }


        }

        void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            var client = e.UserToken as UserObject;
            Message msg = new Message();
            Message sendMsg = new Message();
            string message = string.Empty;
            msg.Buffer = e.Buffer;
            sendUserObject.Param = Encoding.Unicode.GetString(msg.Param).Replace("\0","");
            sendUserObject.CurrentLength = BitConverter.ToInt64(msg.FileCurrentLength, 0);
            sendUserObject.ContentLength = BitConverter.ToInt64(msg.FileLength, 0);
            if (e.BytesTransferred == 0)
            {

                //  Console.WriteLine("Receive_Completed", Socket.Handle);
                //  Console.WriteLine("Socket is closed", Socket.Handle);
                SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                IPAddress.Any.ToString(),
                port,
                serverIP,
                 sendUserObject.ContentLength,
                sendUserObject.CurrentLength,
                e.Buffer,
                client.Socket
                );


                if (Received != null) { Received(this, socketAsyEventArgs); }


                if (fileStreamRead != null) { fileStreamRead.Close(); }
                if (client.Socket != null) { client.Socket.Close(); }
                Closed = true;

            }
            else
            {
                //  string message = Encoding.Unicode.GetString(e.Buffer, 0, e.BytesTransferred);
                //  Console.WriteLine("Server send message:{0}", message);
               
                SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                IPAddress.Any.ToString(),
                MyIPAddress.IPHelper.GetRemoteEndPointPort(Socket.RemoteEndPoint),
                MyIPAddress.IPHelper.GetRemoteEndPointAddress(Socket.RemoteEndPoint),
                sendUserObject.ContentLength,
                sendUserObject.CurrentLength,
                e.Buffer,
                Socket
                );
                if (Receiving != null) Receiving(this, socketAsyEventArgs);
                orderType = BitConverter.ToUInt32(msg.Command, 0);
                switch (orderType)
                {
                    case SocketEnum.OrderType.BeginSendFile:
                        if (sendUserObject.CurrentLength <= sendUserObject.ContentLength)
                            SendFile(sendUserObject.Param);
                        else
                        {
                            if (fileStreamRead != null) { fileStreamRead.Close(); }
                            if (client.Socket != null) { client.Socket.Close(); }
                        }
                        break;
                    case SocketEnum.OrderType.BeginDownLoadFile: break;
                    case SocketEnum.OrderType.BeginString: SendString(sendUserObject.Param); break;
                    default: break;
                }

            }
            
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="param">发送的内容文件地址或字符窜</param>
        public void Send(string param, uint Type)
        {
            orderType = Type;
            SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs();
            if (!Socket.Connected)
            {
                if (DisContented != null) { DisContented(this, socketAsyEventArgs); }
                return;
            }
            sendUserObject.ContentLength = 0;
            sendUserObject.orderType = orderType;
            sendUserObject.Param = param;
            receiveUserObject.ContentLength = 0;
            receiveUserObject.orderType = orderType;
            receiveUserObject.Param = param;
            switch (orderType)
            {
                case SocketEnum.OrderType.SendString:
                    SendString(sendUserObject.Param);
                    break;
                case SocketEnum.OrderType.SendFile:
                    SendFile(sendUserObject.Param);
                    break;
            }
           
            


        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            Socket.Close();
        }
        
        void SendString(string param)
        {
          
            int length = Encoding.Unicode.GetBytes(param, 0, param.Length, SendBuffer, 0);
            sendUserObject.ContentLength = param.Length; 
            sendUserObject.CurrentLength = length;
            SendEventArgs.SetBuffer(SendBuffer,0, length);
            SendEventArgs.UserToken = sendUserObject;
            Socket.SendAsync(SendEventArgs);
            Socket.ReceiveAsync(ReceiveEventArgs);
        }
       
        void SendFile(string param)
        {

            fileStreamRead = fileStreamRead == null ? new FileStream(param, FileMode.Open) : fileStreamRead; //当前读取文件流
            sendUserObject.ContentLength = fileStreamRead.Length; //长度
            sendUserObject.Param = fileStreamRead.Name.Substring(fileStreamRead.Name.LastIndexOf('\\') + 1);//文件名称
            Message msg = new Message();
            Buffer.BlockCopy( Encoding.Unicode.GetBytes(sendUserObject.Param),0, msg.Param,0,Encoding.Unicode.GetBytes(sendUserObject.Param).Length);
            Buffer.BlockCopy(BitConverter.GetBytes(sendUserObject.ContentLength), 0, msg.FileLength, 0, BitConverter.GetBytes(sendUserObject.ContentLength).Length);
            switch (orderType) //命令类型
            {
                case SocketEnum.OrderType.SendFile:
                    msg.Command = BitConverter.GetBytes(SocketEnum.OrderType.SendFile);
                    break;
                case SocketEnum.OrderType.BeginSendFile:
                    msg.Command = BitConverter.GetBytes(SocketEnum.OrderType.BeginSendFile);
                    fileStreamRead.Seek(sendUserObject.CurrentLength, SeekOrigin.Begin);
                    msg.BodyBuffer = new byte[sendUserObject.BuffSize - msg.HeadBuffer.Length];
                    if (sendUserObject.CurrentLength + msg.BodyBuffer.Length > sendUserObject.ContentLength)
                    {
                        msg.BodyBuffer = new byte[sendUserObject.ContentLength - sendUserObject.CurrentLength ];
                    }
                    fileStreamRead.Read(msg.BodyBuffer, 0, msg.BodyBuffer.Length);
                    sendUserObject.CurrentLength += msg.BodyBuffer.Length;
                    Buffer.BlockCopy( BitConverter.GetBytes(sendUserObject.CurrentLength), 0, msg.FileCurrentLength, 0,  BitConverter.GetBytes(sendUserObject.CurrentLength).Length);
                    break;
                default: break;
            }
            SendEventArgs.SetBuffer(msg.Buffer, 0, msg.Buffer.Length);
            SendEventArgs.UserToken = sendUserObject;
            ReceiveEventArgs.UserToken = receiveUserObject;
            Socket.SendAsync(SendEventArgs);  
        }

        void Send_Completed(object sender, SocketAsyncEventArgs e)
        {
            UserObject client = e.UserToken as UserObject;
            if (e.BytesTransferred == 0)
            {
                // Console.WriteLine("Socket is closed", Socket.Handle);
                SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                IPAddress.Any.ToString(),
                port,
                serverIP,
                client.ContentLength,
                client.CurrentLength,
                e.Buffer,
                Socket
                );
                if (Sended != null) { Sended(this, socketAsyEventArgs); }
                if (fileStreamRead != null) { fileStreamRead.Close(); }
                if (client.Socket != null) { client.Socket.Close(); }
                Closed = true;
            }
            else
            {
                //Console.WriteLine("sent {0} byte(s) data to server.", e.BytesTransferred);
                SocketAsyEventArgs socketAsyEventArgs = new SocketAsyEventArgs(
                IPAddress.Any.ToString(),
                port,
                serverIP,
                client.ContentLength,
                client.CurrentLength,
                e.Buffer,
                Socket
                );
                if (Sending != null) { Sending(this, socketAsyEventArgs); }
                
            }
           Socket.ReceiveAsync(ReceiveEventArgs);
            
        }

    }
}
