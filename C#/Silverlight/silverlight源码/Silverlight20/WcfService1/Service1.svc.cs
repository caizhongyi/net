using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfService1
{
    // 注意: 如果更改此处的类名“Service1”，也必须更新 Web.config 和关联的 .svc 文件中对“Service1”的引用。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


    }
}
