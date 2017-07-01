using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;

namespace czy.MyClass.Communications
{
    /// <summary>
    /// Socket发送信息
    /// </summary>
    public class SocketHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public SocketHelper()
        {
            socket = new Socket(this._addressFamily, this._stype, this._ptype);
           // socket.ReceiveBufferSize = 2046;
            //socket.SendBufferSize = 2046;
           // byteMessage = new byte[this._buffsize];
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="BufferSize">发送和接收的缓冲区大小</param>
        //public SocketHelper(int BufferSize)
        //{
        //    socket = new Socket(this._addressFamily, this._stype, this._ptype);
        //    socket.ReceiveBufferSize = BufferSize;
        //    socket.SendBufferSize = BufferSize;
        //}
        #region 事件
        /// <summary>
        /// 发送中委托
        /// </summary>
        /// <param name="res">传输的信息</param>
        public delegate void SocketFileEventHandler(AsyStateParams param);
        public delegate void SocketStringEventHandler(string s);
   
        /// <summary>
        /// 接收完成事件
        /// </summary>
        public event SocketStringEventHandler ReceivedString;
        /// <summary>
        /// 接收文件中事件
        /// </summary>
        public event SocketFileEventHandler ReceivingFile;
        /// <summary>
        /// 接收文件完成事件
        /// </summary>
        public event SocketFileEventHandler ReceivedFile;

  
        /// <summary>
        /// 发送完成事件
        /// </summary>
        public event SocketStringEventHandler SendedString;
        /// <summary>
        /// 发送文件中事件
        /// </summary>
        public event SocketFileEventHandler SendingFile;
        /// <summary>
        /// 发送文件完成事件
        /// </summary>
        public event SocketFileEventHandler SendedFile;

        #endregion

        #region 私有成员
        Socket socket;
        private ProtocolType _ptype = ProtocolType.Tcp;
        private SocketType _stype = SocketType.Stream;
        private AddressFamily _addressFamily = AddressFamily.InterNetwork;
        private int _port = 8000;
        private int _buffsize = 512;
        private SocketError _error;
        private bool startListen = true;
        private int maxConnect = 1024;
        byte[] byteMessage;
        object _obj;
      

        //private Socket newSocket;
        private string _fileName;
        #endregion

        #region 属性
        /// <summary>
        /// 是否开始侦听
        /// </summary>
        public bool StartListen
        {
            get { return startListen; }
            set { startListen = value; }
        }
        /// <summary>
        /// Socket对像
        /// </summary>
        public Socket Socket
        {
            get { return socket; }
        }
        /// <summary>
        /// TCP最大链接数
        /// </summary>
        public int MaxConnect
        {
            get { return maxConnect; }
            set { maxConnect = value; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public SocketError Error
        {
            get { return _error; }
        }
        /// <summary>
        /// byte数组的长度
        /// </summary>
        public int Buffsize
        {
            get { return _buffsize; }
            set { _buffsize = value; }
        }
        /// <summary>
        /// 协议
        /// </summary>
        public ProtocolType Ptype
        {
            get { return _ptype; }
            set { _ptype = value; }
        }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        /// <summary>
        /// 寻址方案
        /// </summary>
        public AddressFamily AddressFamily
        {
            get { return _addressFamily; }
            set { _addressFamily = value; }
        }
        /// <summary>
        /// 套接字类型
        /// </summary>
        public SocketType Stype
        {
            get { return _stype; }
            set { _stype = value; }
        }
        #endregion

        #region 取得终端IP地址

        /// <summary>
        /// 取得终端IP地址
        /// </summary>
        /// <returns></returns>
        public IPAddress GetServerIP()
        {
            IPHostEntry ieh = Dns.GetHostByName(Dns.GetHostName());
            return ieh.AddressList[0];
        }
        #endregion

        #region 侦听
        /// <summary>
        /// 文件侦听
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="flags">SocketFlags</param>
        /// <param name="fileLength">文件总长度</param>
        public void FileListen(string fileName, SocketFlags flags, long fileLength)
        {

            IPAddress ServerIp = GetServerIP();
            IPEndPoint iep = new IPEndPoint(ServerIp, this._port);
            socket.Bind(iep);
            socket.Listen(maxConnect);
            while (this.startListen)
            {
                try
                {
                    Socket newSocket = socket.Accept();
                    newSocket.ReceiveBufferSize = this.socket.ReceiveBufferSize;
                    ReceiveFile(newSocket, fileName, flags,fileLength);
            
                }
                catch (SocketException ex)
                {
                    throw ex;

                }
            }
            socket.Close();

        }
        /// <summary>
        /// 文件侦听
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="flags">SocketFlags</param>
        /// <param name="flags">文件总长度</param>
        public void FileListen(string fileName, long fileLenght)
        {

            IPAddress ServerIp = GetServerIP();
            IPEndPoint iep = new IPEndPoint(ServerIp, this._port);
            socket.Bind(iep);
            socket.Listen(maxConnect);
            while (this.startListen)
            {
                try
                {
                    Socket newSocket = socket.Accept();
                    newSocket.ReceiveBufferSize = this.socket.ReceiveBufferSize;
                    ReceiveFile(newSocket, fileName, SocketFlags.None,fileLenght);

                }
                catch (SocketException ex)
                {
                    throw ex;

                }
            }
            socket.Close();

        }
        /// <summary>
        /// 字符窜侦听
        /// </summary>
        /// <param name="maxBuffer">最大缓存</param>
        /// <param name="flags">SocketFlags</param>
        public void StringListen(long maxBuffer, SocketFlags flags)
        {

            IPAddress ServerIp = GetServerIP();
            IPEndPoint iep = new IPEndPoint(ServerIp, this._port);
            socket.Bind(iep);
            socket.Listen(maxConnect);
            while (this.startListen)
            {
                try
                {
                    Socket newSocket = socket.Accept();
                    newSocket.ReceiveBufferSize = this.socket.ReceiveBufferSize;
                    ReceiveString(newSocket,  flags);
                }
                catch (SocketException ex)
                {
                    throw ex;

                }
            }
            socket.Close();
        }
        /// <summary>
        /// 字符窜侦听
        /// </summary>
        /// <param name="maxBuffer">最大缓存</param>
        /// <param name="flags">SocketFlags</param>
        public void StringListen()
        {

            IPAddress ServerIp = GetServerIP();
            IPEndPoint iep = new IPEndPoint(ServerIp, this._port);
            socket.Bind(iep);
            socket.Listen(maxConnect);
            while (this.startListen)
            {
                try
                {
                    Socket newSocket = socket.Accept();
                   // newSocket.ReceiveBufferSize = this.socket.ReceiveBufferSize;
                    ReceiveString(newSocket,  SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    throw ex;

                }
            }
            socket.Close();
        }
        #endregion

        #region 接收
        private void ReceiveFile(Socket newSocket, string fileName,SocketFlags flags,long fileLenght)
        {
            long curFinishLength = 0;
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            byte[] bytes = new byte[this._buffsize];
            AsyStateParams param = new AsyStateParams();
            param.Socket = newSocket;
            param.Bytes = bytes;
            param.CurrentByteLength = curFinishLength;
            param.TotalByteLength = fileLenght;
            param.Fstream = fs;
            param.Flags = flags;
            param.Index = 0;
            param.LastIndex = fileLenght % this._buffsize == 0 ? fileLenght / this._buffsize : (fileLenght / this._buffsize) + 1;
            BeginReceiveFile(param);
        }
        private void BeginReceiveFile(AsyStateParams param)
        {
            byte[] bytes = new byte[this._buffsize];

            param.Socket.BeginReceive(bytes, 0, bytes.Length, param.Flags, ReceiveFile_CallBack,param);
      
        }

        private void ReceiveFile_CallBack(IAsyncResult iar)
        {
            AsyStateParams param = ((AsyStateParams)iar.AsyncState);
            Socket newSocket = param.Socket;
            int sendLen = newSocket.EndReceive(iar, out this._error);
            bool OK = this._error == SocketError.Success;

            if (sendLen < 1)
            {
                OK = false;
            }
            if (!OK)
            {
                newSocket.Close();
                param.Fstream.Close();
            }
            else
            {
                if (param.Index <= param.LastIndex)
                {
                    param.Fstream.Write(param.Bytes, 0, param.Bytes.Length);
                    param.CurrentByteLength += sendLen;
                    param.Fstream.Seek(param.CurrentByteLength, SeekOrigin.Begin);
                    if (ReceivingFile != null) ReceivingFile(param);
                    param.Index++;
                    BeginReceiveFile(param);

                }
                else
                {
                    if (ReceivedFile != null) ReceivedFile(param);
                    param.Fstream.Close();
                    newSocket.Close();
                }

            }
        }


        private void ReceiveString(Socket newSocket,SocketFlags flags)
        {
            
            AsyStateParams param = new AsyStateParams();
            long curFinishLength = 0;
            byte[] bytes = new byte[this._buffsize];
            param.Socket = newSocket;
            param.Bytes = bytes;
            param.Flags = flags;
            param.CurrentByteLength = curFinishLength;
            BeginReceiveString(param);
        }
        private void BeginReceiveString(AsyStateParams param)
        {
            //bool start = true;
 
            //int i = 2;
            //while (start)
            //{
            //    if (param.Bytes.BytesSum() > 0)
            //    {
                param.Socket.Receive(param.Bytes, 0, param.Bytes.Length, param.Flags);
                //    btyes = new byte[this._buffsize*i];
                //    i++;
                //}
                //else
                //{ start = false; param.Bytes.CopyTo(btyes,((btyes.Length / param.Bytes.Length)-1)*this._buffsize);  }
            
            //}
            param.Socket.Close();
            if (ReceivedString != null) ReceivedString(Encoding.Default.GetString(param.Bytes));
        }

        #endregion

        #region 链接
        /// <summary>
        /// 链接
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="port">端口</param>
        public void Connect(string ip, int port)
        {
            this._port = port;
            IPAddress serverIp = IPAddress.Parse(ip);
            int serverPort = Convert.ToInt32(port);
            IPEndPoint iep = new IPEndPoint(serverIp, serverPort);
            socket.Connect(iep);
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        /// <param name="reuseSocket">关闭后是否可重用socket</param>
        public void DisConnect(bool reuseSocket)
        {
            socket.Disconnect(reuseSocket);
        }
        /// <summary>
        /// 异步链接
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="port">端口</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="o">自定义对像</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginConnect(string ip, int port, AsyncCallback callBack, object o)
        {
            this._port = port;
            IPAddress serverIp = IPAddress.Parse(ip);
            int serverPort = Convert.ToInt32(port);
            IPEndPoint iep = new IPEndPoint(serverIp, serverPort);
            return socket.BeginConnect(iep, callBack, o);
        }
        #endregion

        #region 发送消息
       
        /// <summary>
        /// 发送字符窜
        /// </summary>
        /// <param name="s">字符窜</param>
        /// <param name="maxBuffer">最在缓存</param>
        /// <param name="flags">SocketFlags</param>
        /// <returns></returns>
        public bool SendString(string s, int maxBuffer, SocketFlags flags)
        {
            if (socket.Connected)
            {
                long curFinishLength = 0;
                byte[] bytes = Encoding.Default.GetBytes(s);
                if (bytes.Length > maxBuffer)
                {
                    return false;
                }
                AsyStateParams param = new AsyStateParams();
                param.Socket = this.socket;
                param.Bytes = bytes;
                param.CurrentByteLength = curFinishLength;
                param.Flags = flags;
                BeginSendFile(param);
            }
            return true;
        }
        /// <summary>
        /// 发送字符窜
        /// </summary>
        /// <param name="s">字符窜</param>
        /// <param name="maxBuffer">最在缓存</param>
        /// <param name="flags">SocketFlags</param>
        /// <returns></returns>
        public bool SendString(string s)
        {
            byte[] bytes = Encoding.Default.GetBytes(s);
        
            if (socket.Connected)
            {
                long curFinishLength = 0;
                AsyStateParams param = new AsyStateParams();
                param.Socket = this.socket;
                param.Bytes = bytes;
                param.CurrentByteLength = curFinishLength;
                param.Flags = SocketFlags.None;
                BeginSendString(param);
            }
            return true;
        }
        private void BeginSendString(AsyStateParams param)
        {
            param.Socket.Send(param.Bytes, 0, param.Bytes.Length, param.Flags);
            if (SendedString != null) SendedString(Encoding.Default.GetString( param.Bytes));
            param.Socket.Close();
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileMode">FileMode</param>
        /// <param name="flags">SocketFlags</param>
        public void SendFile(string fileName, FileMode fileMode, SocketFlags flags)
        {
            if (socket.Connected)
            {
                FileStream fs = new FileStream(fileName, fileMode);
                long curFinishLength = 0;
                byte[] bytes = new byte[this._buffsize];
                AsyStateParams param = new AsyStateParams();
                param.Socket = this.socket;
                param.Bytes = bytes;
                param.CurrentByteLength = curFinishLength;
                param.TotalByteLength = fs.Length;
                param.Fstream = fs;
                param.Flags = flags;
                BeginSendFile(param);
            }
        }
        public void SendFile(string fileName, FileMode fileMode)
        {
            if (socket.Connected)
            {
                FileStream fs = new FileStream(fileName, fileMode);
                long curFinishLength = 0;
                byte[] bytes = new byte[this._buffsize];
                AsyStateParams param = new AsyStateParams();
                param.Socket = this.socket;
                param.Bytes = bytes;
                param.CurrentByteLength = curFinishLength;
                param.TotalByteLength = fs.Length;
                param.Fstream = fs;
                param.Index = 0;
                param.LastIndex = fs.Length % this._buffsize == 0 ? fs.Length / this._buffsize : (fs.Length / this._buffsize) + 1;
                param.Flags = SocketFlags.None;
                BeginSendFile(param);
            }
        }
        private void BeginSendFile(AsyStateParams param)
        {
            
    
                byte[] bytes = param.Index == param.LastIndex ? new byte[param.TotalByteLength - param.CurrentByteLength] : new byte[this._buffsize];
                param.Fstream.Read(bytes, 0, bytes.Length);
                param.Bytes = bytes;
                param.Socket.BeginSend(bytes, 0, bytes.Length, param.Flags, SendFile_CallBack, param);
       
        }

        #region 回调
        private void SendFile_CallBack(IAsyncResult iar)
        {
            AsyStateParams param = (AsyStateParams)iar.AsyncState;
            if (param.Socket!= null)
            {
                int sendLen = param.Socket.EndSend(iar, out this._error);
                bool OK = this._error == SocketError.Success;

                if (sendLen < 1)
                {
                    OK = false;
                }
                if (!OK)
                {
                    // if (SendFail != null) SendFail(iar);
                   /// if (ErrorCallBack != null) ErrorCallBack(iar);
                    param.Socket.Shutdown(SocketShutdown.Both);
                    param.Socket.Close();
                }
                else
                {
                    if (param.Index <= param.LastIndex)
                    {
                        param.CurrentByteLength += sendLen;
                        param.Fstream.Seek(param.CurrentByteLength, SeekOrigin.Begin);
                        if (SendingFile != null) SendingFile(param);
                        param.Index++;
                        BeginSendFile(param);
                    }
                    else
                    {
                        if (SendedFile != null) SendedFile(param);
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }

                }
            }
        }
        #endregion
        #endregion
    }
    /// <summary>
    /// 自定义参数
    /// </summary>
    public class AsyStateParams
    {
        Socket socket;
        /// <summary>
        /// 当前运行的socket
        /// </summary>
        public Socket Socket
        {
            get { return socket; }
            set { socket = value; }
        }
        byte[] bytes;
        /// <summary>
        /// 当前接收或发送的bytes值
        /// </summary>
        public byte[] Bytes
        {
            get { return bytes; }
            set { bytes = value; }
        }

        long currentByteLength;
        /// <summary>
        /// 当前已发送或已接收长度
        /// </summary>
        public long CurrentByteLength
        {
            get { return currentByteLength; }
            set { currentByteLength = value; }
        }
        long totalByteLength;
        /// <summary>
        /// 总长度
        /// </summary>
        public long TotalByteLength
        {
            get { return totalByteLength; }
            set { totalByteLength = value; }
        }
        private FileStream fstream;
        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream Fstream
        {
            get { return fstream; }
            set { fstream = value; }
        }

        private SocketFlags flags;
        /// <summary>
        /// 套接字行为
        /// </summary>
        public SocketFlags Flags
        {
            get { return flags; }
            set { flags = value; }
        }
        private long index = 0;
        /// <summary>
        /// 当前发送块
        /// </summary>
        public long Index
        {
            get { return index; }
            set { index = value; }
        }
        private long lastIndex = 0;
        /// <summary>
        /// 最后一块的index
        /// </summary>
        public long LastIndex
        {
            get { return lastIndex; }
            set { lastIndex = value; }
        }
        //private byte[] lastBytes;
        ///// <summary>
        ///// 最后一块的Byte值
        ///// </summary>
        //public byte[] LastBytes
        //{
        //    get { return lastBytes; }
        //    set { lastBytes = value; }
        //}
    }

}
