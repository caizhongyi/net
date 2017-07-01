using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace czy.MyDAL
{
    public class DataBaseEvent
    {
        public delegate void DataReadEvent(object DataReader);
    }
}
