using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace czy.MyClass.Communications
{
    /// <summary>
    /// 发送的类型
    /// </summary>
    public class SocketEnum
    {
        public struct OrderType
        {
            public const uint None=0x00000000;
            public const uint SendString=0x00000001;
            public const uint BeginString = 0x00000002;
            public const uint SendFile=0x00000003;
            public const uint BeginSendFile = 0x00000004;
            public const uint DownLoadFile = 0x00000005;
            public const uint BeginDownLoadFile = 0x00000006;
            public const uint Login = 0x00000007;
            public const uint BeginLogin = 0x00000008;
        }

    }

    public class Message
    {
        byte[] _fileLength=new  byte[8];

        public byte[] FileLength
        {
            get { return _fileLength; }
            set { _fileLength = value; }
        }
        byte[] _fileCurrentLength = new byte[8];

        public byte[] FileCurrentLength
        {
            get { return _fileCurrentLength; }
            set { _fileCurrentLength = value; }
        }

        byte[] _param = new byte[40];
        /// <summary>
        /// 文件名称或其它字符窜
        /// </summary>
        public byte[] Param
        {
            get { return _param; }
            set { _param = value; }
        }
        byte[] _command = new byte[4];

        public byte[] Command
        {
            get { return _command; }
            set { _command = value; }
        }
  
        public byte[] Buffer
        {
            set { _command = value.SubBytes(0, 4); _fileCurrentLength = value.SubBytes(4, 8); _fileLength = value.SubBytes(12, 8); _param = value.SubBytes(20, 40); _body = value.SubBytes(60, value.Length - 60); }
            get { return HeadBuffer.JoinBytes(BodyBuffer); }
        } 
        /// <summary>
        /// 消息头
        /// 前4字节为命令符,后8字节为消息总长度,40字节为消息名称
        /// </summary>
        public byte[] HeadBuffer
        {
            get { return _command.JoinBytes(_fileCurrentLength).JoinBytes(_fileLength).JoinBytes(_param); }
        }
        byte[] _body;
        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] BodyBuffer
        {
            get { return _body==null?new byte[1]:_body; }
            set { _body = value; }
        }
    }
    
    /// <summary>
    /// 异步参数
    /// </summary>
    public class SocketAsyEventArgs : EventArgs
    {
        #region 私有成员
        private decimal _pencent = 0;
        private string _cilentIP="";
        private int _port=0;
        private string _serverIP="";
        private string _buffString="";
        private long _totalLength=0;
        private long _currentLength=0;
        private byte[] _bytes;
        private Socket _currentSocket;
        #endregion

        #region 属性
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP { get { return _cilentIP; } }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get { return _port; } }
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP { get { return _cilentIP; } }
        /// <summary>
        /// 发送或接收的字节
        /// </summary>
        public byte[] Bytes { get { return _bytes; } }
        /// <summary>
        /// 当前发送或接收的字节长度
        /// </summary>
        public long CurrentLength { get { return _currentLength; } }
        /// <summary>
        /// 当前发送或接收总长度
        /// </summary>
        public long TotalLength { get { return _totalLength; } }
        /// <summary>
        /// 当前链接的Socket
        /// </summary>
        public Socket CurrentSocket{ get { return _currentSocket; }set { _currentSocket = value; } }
        /// <summary>
        /// 当前发送占总的百分比
        /// </summary>
        public decimal Pencent
        {
            get
            {
                //this._pencent = TotalLength == 0 ? 0 :decimal.Round( Convert.ToDecimal(CurrentLength) / Convert.ToDecimal(TotalLength),2);

                this._pencent = TotalLength == 0 ? 0 : Convert.ToDecimal(CurrentLength) / Convert.ToDecimal(TotalLength);
                return this._pencent;
            }
            private set { value = this._pencent; }
        }
        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        public SocketAsyEventArgs() { }
      
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="serverIP">服务器IP</param>
        public SocketAsyEventArgs(int port, string serverIP)
        {

            _port = port;
            _serverIP = serverIP;

        }
       
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="clientIP">客户端IP</param>
        /// <param name="port">端口</param>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="buffString">发送的字符</param>
        /// <param name="totalLength">总长度</param>
        /// <param name="currentLength">当前发度</param>
        /// <param name="bytes">当前的字节</param>
        public SocketAsyEventArgs(string clientIP, int port, string serverIP,  long totalLength, long currentLength, byte[] bytes,Socket socket)
        {
            _currentSocket = socket;
            _cilentIP = clientIP;
            _port = port;
            _serverIP = serverIP;
            _totalLength = totalLength;
            _currentLength = currentLength;
            _bytes = bytes;
        }
    }

    /// <summary>
    /// 自定义对象
    /// </summary>
    public class UserObject
    {
        #region 私有成员
        private long _buffSize = 5024;
        #endregion

        #region 属性
        /// <summary>
        /// 发送数据的类型
        /// </summary>
        public uint orderType { get; set; }
        /// <summary>
        /// 发送的文件名或字符窜
        /// </summary>
        public string Param { get; set; }
        /// <summary>
        /// 文件总长度
        /// </summary>
        public long ContentLength { get; set; }
        /// <summary>
        /// 文件当前已发送的长度
        /// </summary>
        public long CurrentLength { get; set; }
        /// <summary>
        /// 接收数据的缓冲区
        /// </summary>
        public byte[] ReceiveBuffer { get; private set; }
        /// <summary>
        /// 发送数据的缓冲区
        /// </summary>
        public byte[] SendBuffer { get; private set; }
        /// <summary>
        /// 客户端Socket对象
        /// </summary>
        public Socket Socket { get; private set; }
        /// <summary>
        /// 发送数据上下文对象
        /// </summary>
        public SocketAsyncEventArgs SendEventArgs { get; private set; }
        /// <summary>
        /// 接收数据上下文对象
        /// </summary>
        public SocketAsyncEventArgs ReceiveEventArgs { get; private set; }
        /// <summary>
        /// 发送数据缓存大小
        /// </summary>
        public long BuffSize { get { return this._buffSize; } set { value = this._buffSize; } }
        #endregion

        public UserObject(Socket socket)
        {
             
            ReceiveBuffer = new byte[_buffSize];//定义接收缓冲区
            SendBuffer = new byte[_buffSize];//定义发送缓冲区
            this.Socket = socket;
            SendEventArgs = new SocketAsyncEventArgs();
            ReceiveEventArgs = new SocketAsyncEventArgs();
            SendEventArgs.UserToken = this;
            ReceiveEventArgs.UserToken = this;
            ReceiveEventArgs.SetBuffer(ReceiveBuffer, 0, ReceiveBuffer.Length);//设置接收缓冲区
            SendEventArgs.SetBuffer(SendBuffer, 0, SendBuffer.Length);//设置发送缓冲区
            

        }
    }
}
