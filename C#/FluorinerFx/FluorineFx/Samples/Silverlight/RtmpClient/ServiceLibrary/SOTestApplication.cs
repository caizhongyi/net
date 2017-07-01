using System;
using FluorineFx;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Messaging.Api.Stream;

namespace ServiceLibrary
{
    public class SOTestApplication : ApplicationAdapter
    {
        int _usersID;
        string _history;

        public SOTestApplication()
		{
		}

        public override bool AppStart(IScope application)
        {
            _usersID = 1;
            _history = string.Empty;
            this.CreateSharedObject(this.Scope, "users", false);
            return base.AppStart(application);
        }

        public override bool AppConnect(IConnection connection, object[] parameters)
        {
            lock (typeof(SOTestApplication))
            {
                connection.SetAttribute("uniqueUserID", _usersID);
                connection.SetAttribute("connectStartTime", DateTime.Now);
                //set the shared object
                ISharedObject usersSO = GetSharedObject(this.Scope, "users");
                ASObject aso = new ASObject();
                aso.Add("uniqueUserID", _usersID);
                aso.Add("connectStartTime", DateTime.Now);
                usersSO.SetAttribute("user" + _usersID, aso);
                _usersID++;

                // Call the client function 'setHistory,' and pass 
                // the initial history
                if( connection is IServiceCapableConnection )
                    (connection as IServiceCapableConnection).Invoke("setHistory", new object[]{_history}, null);
            }
            return base.AppConnect(connection, parameters);
        }

        public override void AppDisconnect(IConnection connection)
        {
            int uniqueUserID = (int)connection.GetAttribute("uniqueUserID");
            ISharedObject usersSO = GetSharedObject(this.Scope, "users");
            if( usersSO != null )
                usersSO.SetAttribute("user" + uniqueUserID, null);
            base.AppDisconnect(connection);
        }

        public void msgFromClient(IConnection connection, string msg)
        {
            lock (typeof(SOTestApplication))
            {
                int uniqueUserID = (int)connection.GetAttribute("uniqueUserID");
                msg = "user" + uniqueUserID + ": " + msg + "\n";
                _history += msg;
                ISharedObject usersSO = GetSharedObject(this.Scope, "users");
                usersSO.SendMessage("msgFromSrvr", new object[]{msg});
            }
        }
    }
}
