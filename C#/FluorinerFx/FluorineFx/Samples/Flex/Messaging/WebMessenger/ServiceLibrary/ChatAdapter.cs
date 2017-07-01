using System;
using System.Collections;

using FluorineFx.Context;
using FluorineFx.Messaging;
using FluorineFx.Messaging.Services;
using FluorineFx.Messaging.Messages;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Services.Messaging;

namespace ServiceLibrary
{
    class ChatAdapter : MessagingAdapter, ISessionListener
    {
        private Hashtable _clients;

	    public ChatAdapter()
        {
            _clients = new Hashtable();
            ClientManager.AddSessionCreatedListener(this);
        }

        #region ISessionListener Members

        public void SessionCreated(IClient client)
        {
            client.AddSessionDestroyedListener(this);
        }

        public void SessionDestroyed(IClient client)
        {
            lock (_clients)
            {
                string userId = _clients[client.Id] as string;
                if (userId != null)
                {
                    // Notify interested parties
                    PushStatus(userId, "disconnected");
                    // Remove Session Id / User Id mapping
                    _clients.Remove(client.Id);
                }
            }
        }

        #endregion

        public override object Invoke(IMessage message)
        {
            lock (_clients)
            {
                // Check if Client Id is already registered
                if (!_clients.ContainsKey(FluorineContext.Current.ClientId))
                {
                    // Get User Id from message body 
                    IDictionary body = message.body as IDictionary;
                    string userId = body["userId"] as string;
                    // Register client / userId mapping
                    _clients.Add(FluorineContext.Current.ClientId, userId);
                }
            }
            MessageService messageService = this.Destination.Service as MessageService;
            messageService.PushMessageToClients(message);
            return null;
        }

        private void PushStatus(string userId, string status)
        {
            MessageBroker msgBroker = MessageBroker.GetMessageBroker(null);
            AsyncMessage msg = new AsyncMessage();
            msg.destination = "chat";
            msg.headers.Add(AsyncMessage.SubtopicHeader, "status." + userId);
            msg.clientId = Guid.NewGuid().ToString("D");
            msg.messageId = Guid.NewGuid().ToString("D");
            msg.timestamp = Environment.TickCount;
            Hashtable body = new Hashtable();
            body.Add("userId", userId);
            body.Add("status", status);
            msg.body = body;
            msgBroker.RouteMessage(msg);
        }

    }
}
