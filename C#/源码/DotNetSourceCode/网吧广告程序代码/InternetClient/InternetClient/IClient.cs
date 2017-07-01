using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace InternetClient
{
    public interface IClient
    {
        Socket GetConnetion();
    }
}
