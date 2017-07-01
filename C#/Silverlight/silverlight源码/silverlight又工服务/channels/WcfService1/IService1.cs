using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;

namespace DuplexService
{
    [ServiceContract(Namespace = "Silverlight", CallbackContract = typeof(IDuplexClient))]
    public interface IDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void Order(Message receivedMessage);
    }

    [ServiceContract]
    public interface IDuplexClient
    {
        [OperationContract(IsOneWay = true)]
        void Receive(Message returnMessage);
    }



}
