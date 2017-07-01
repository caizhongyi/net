using System;
namespace SocketClass
{
    public  interface ISocketclass
    {
        void BeginListen();
        void BeginSend(string ip, string port, string message);
    }
}
