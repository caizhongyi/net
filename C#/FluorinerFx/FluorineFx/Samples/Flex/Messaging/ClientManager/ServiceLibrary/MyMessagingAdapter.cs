using System;
using System.Collections.Generic;
using System.Text;

using FluorineFx.Context;
using FluorineFx.Messaging;
using FluorineFx.Messaging.Services;
using FluorineFx.Messaging.Messages;
using FluorineFx.Messaging.Services.Messaging;

namespace ServiceLibrary
{
    class MyMessagingAdapter : MessagingAdapter
    {

        public override bool HandlesSubscriptions
        {
            get
            {
                return true;
            }
        }

        public override object Manage(CommandMessage commandMessage)
        {
            string clientId = FluorineFx.Context.FluorineContext.Current.ClientId;
            return null;
        }
    }
}
