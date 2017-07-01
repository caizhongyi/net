using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WbClient
{
    public interface IConnectionService
    {
        Thread GetConnection();
    }
}
