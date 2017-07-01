using System;
using System.Collections;
using FluorineFx.Context;
using FluorineFx.Exceptions;

using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Messaging.Api.Stream;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for AppHandler.
	/// </summary>
    public class MyApp : ApplicationAdapter, IPendingServiceCallback
	{
		public MyApp()
		{
		}
		
		public override bool AppConnect(IConnection connection, object[] parameters)
		{
            Object[] args = new Object[] { connection.Client.Id };

            InvokeClients("clientConnected", args, this, true, this.Scope);
            //Notify client about his ID
            if (connection is IServiceCapableConnection)
                ((IServiceCapableConnection)connection).Invoke("setClientId", args);
            return true;
        }

		public override void AppDisconnect(IConnection connection)
		{
            Object[] args = new Object[] { connection.Client.Id };
            InvokeClients("clientDisconnected", args, this, true, connection.Scope);
        }

        public ArrayList GetConnectedClients()
        {
            IServiceCapableConnection connection = FluorineContext.Current.Connection as IServiceCapableConnection;
            IEnumerator connectionsEnumerator = connection.Scope.GetConnections();

            ArrayList clients = new ArrayList();

            while (connectionsEnumerator.MoveNext())
            {
                IConnection connectionTmp = connectionsEnumerator.Current as IConnection;

                if ((connectionTmp is IServiceCapableConnection) && (connectionTmp != connection))
                {
                    clients.Add(connectionTmp.Client.Id);
                }
            }
            return clients;
        }

        #region IPendingServiceCallback Members

        public void ResultReceived(IPendingServiceCall call)
        {
        }

        #endregion
    }
}
