using System;
using System.Collections.Generic;
using System.Text;

namespace SocketClass
{
    public static class Factory
    {
        public ISocketclass GetSocketclass()
        {
            return SocketClass.Socketclass();
        }
    }
}
