using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Channels;
using System.Threading;

namespace DuplexService
{
    // 注意: 如果更改此处的类名“Service1”，也必须更新 Web.config 和关联的 .svc 文件中对“Service1”的引用。
    public class OrderService : IDuplexService
    {
        IDuplexClient client;

        public void Order(Message receivedMessage)
        {
            // Grab the client callback channel.
            client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();

            // Pretend that the service is processing and will call the client back in 5 seconds.
            using (Timer timer = new Timer(new TimerCallback(CallClient),
                receivedMessage.GetBody<string>(), 5000, 5000))
            {
                Thread.Sleep(11000);

            }
        }

        bool processed = false;

        private void CallClient(object order)
        {
            string text;

            // Process order
            if (processed)
            {
                text = (string)order + " order complete";
            }
            else
            {
                text = "Processing " + (string)order + " order";
                processed = true;
            }

            // Construct the message to return to the client.
            Message returnMessage = Message.CreateMessage(MessageVersion.Soap11,
                "Silverlight/IDuplexService/Receive", text);

            // Call the client back.
            client.Receive(returnMessage);

        }

    }
}
