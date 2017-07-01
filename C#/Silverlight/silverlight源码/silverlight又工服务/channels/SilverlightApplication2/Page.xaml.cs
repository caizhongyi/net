using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows.Controls;

namespace DuplexClient
{
    public partial class Page : UserControl
    {
        SynchronizationContext uiThread;

        public Page()
        {
            InitializeComponent();

            // Grab a reference to the UI thread.
            uiThread = SynchronizationContext.Current;

            // Instantiate the binding and set the time-outs.
            PollingDuplexHttpBinding binding = new PollingDuplexHttpBinding()
            {
                InactivityTimeout = TimeSpan.FromMinutes(1)
            };

            // Instantiate and open channel factory from binding.
            IChannelFactory<IDuplexSessionChannel> factory =
                binding.BuildChannelFactory<IDuplexSessionChannel>(new BindingParameterCollection());

            IAsyncResult factoryOpenResult =
                factory.BeginOpen(new AsyncCallback(OnOpenCompleteFactory), factory);
            if (factoryOpenResult.CompletedSynchronously)
            {
                CompleteOpenFactory(factoryOpenResult);
            }
        }

        void OnOpenCompleteFactory(IAsyncResult result)
        {
            if (result.CompletedSynchronously)
                return;
            else
                CompleteOpenFactory(result);

        }

        void CompleteOpenFactory(IAsyncResult result)
        {
            IChannelFactory<IDuplexSessionChannel> factory =
                (IChannelFactory<IDuplexSessionChannel>)result.AsyncState;

            factory.EndOpen(result);

            // The factory is now open. Create and open a channel from the channel factory.
            IDuplexSessionChannel channel =
                factory.CreateChannel(new EndpointAddress("http://localhost:19021/Service1.svc"));

            IAsyncResult channelOpenResult =
                channel.BeginOpen(new AsyncCallback(OnOpenCompleteChannel), channel);
            if (channelOpenResult.CompletedSynchronously)
            {
                CompleteOpenChannel(channelOpenResult);
            }

        }

        void OnOpenCompleteChannel(IAsyncResult result)
        {
            if (result.CompletedSynchronously)
                return;
            else
                CompleteOpenChannel(result);

        }

        string order = "Widgets";

        void CompleteOpenChannel(IAsyncResult result)
        {
            IDuplexSessionChannel channel = (IDuplexSessionChannel)result.AsyncState;

            channel.EndOpen(result);

            // The channel is now open. Send a message.
            Message  message =
                Message.CreateMessage(channel.GetProperty<MessageVersion>(),
                "Silverlight/IDuplexService/Order", order);
            IAsyncResult resultChannel =
                channel.BeginSend(message, new AsyncCallback(OnSend), channel);
            if (resultChannel.CompletedSynchronously)
            {
                CompleteOnSend(resultChannel);
            }

            // Also start the receive loop to listen for callbacks from the service.
            ReceiveLoop(channel);
        }

        void OnSend(IAsyncResult result)
        {
            if (result.CompletedSynchronously)
                return;
            else
                CompleteOnSend(result);
        }


        void CompleteOnSend(IAsyncResult result)
        {
            IDuplexSessionChannel channel = (IDuplexSessionChannel)result.AsyncState;

            channel.EndSend(result);

            // The message is now sent. Notify the user.

            uiThread.Post(WriteText, "Client says: Sent order of " + order + Environment.NewLine);
            uiThread.Post(WriteText, "Service will call back with updates" + Environment.NewLine);

        }


        void ReceiveLoop(IDuplexSessionChannel channel)
        {
            // Start listening for callbacks.
            IAsyncResult result = channel.BeginReceive(new AsyncCallback(OnReceiveComplete), channel);
            if (result.CompletedSynchronously)
                CompleteReceive(result);
        }

        void OnReceiveComplete(IAsyncResult result)
        {
            if (result.CompletedSynchronously)
                return;
            else
                CompleteReceive(result);
        }

        void CompleteReceive(IAsyncResult result)
        {
            // A callback was received.

            IDuplexSessionChannel channel = (IDuplexSessionChannel)result.AsyncState;

            try
            {
                Message receivedMessage = channel.EndReceive(result);

                if (receivedMessage == null)
                {
                    // Server closed its output session, can close client channel, 
                    // or continue sending messages to start a new session.
                }
                else
                {
                    // Show the service response in the UI.

                    string text = receivedMessage.GetBody<string>();
                    uiThread.Post(WriteText, "Service says: " + text + Environment.NewLine);

                    // Check whether the order is complete.
                    if (text == order + " order complete")
                    {
                        // If the order is complete, close the client channel. 

                        IAsyncResult resultFactory =
                            channel.BeginClose(new AsyncCallback(OnCloseChannel), channel);
                        if (resultFactory.CompletedSynchronously)
                        {
                            CompleteCloseChannel(result);
                        }

                    }
                    else
                    {
                        // If the order is not complete, continue listening. 
                        ReceiveLoop(channel);
                    }

                }
            }
            catch (CommunicationObjectFaultedException)
            {
                // The channel inactivity time-out was reached.
            }

        }

        void WriteText(object text)
        {
            reply.Text += (string)text;
        }


        void OnCloseChannel(IAsyncResult result)
        {
            if (result.CompletedSynchronously)
                return;
            else
                CompleteCloseChannel(result);

        }

        void CompleteCloseChannel(IAsyncResult result)
        {
            IDuplexSessionChannel channel = (IDuplexSessionChannel)result.AsyncState;

            channel.EndClose(result);

            // The client channel is now closed.
        }

    }
}
