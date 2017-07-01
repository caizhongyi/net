using System;
namespace MySocket
{
   public interface Imysocket
    {
        void BeginListen();
        void BeginSend(string ip, string port, string message);
    }
}
