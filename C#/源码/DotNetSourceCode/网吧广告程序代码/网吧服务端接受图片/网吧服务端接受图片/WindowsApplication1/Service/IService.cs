using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WbService
{
    public interface IService
    {
        Thread ServiceStart();
    }
}
