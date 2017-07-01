using System;
using System.Collections;
using System.Diagnostics;

using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Context;
using FluorineFx.Exceptions;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for AppHandler.
	/// </summary>
    [FluorineFx.RemotingService]
	public class ChatApp : ApplicationAdapter
	{
		public ChatApp()
		{
		}
		
		public override bool AppStart(IScope application)
		{
            Trace.WriteLine("App started");
			return true;
		}

		public override bool RoomStart(IScope room) 
		{
            Trace.WriteLine("Room started " + room.Name);
            if (!base.RoomStart(room))
				return false;
			return true;		
		}

		public override bool AppConnect(IConnection connection, object[] parameters)
		{
			string userName = parameters[0] as string;
            string password = parameters[1] as string;

            if (password == null || password == string.Empty)
                throw new ClientRejectedException(null);

            connection.Client.SetAttribute("userName", userName);
			ISharedObject users_so = GetSharedObject(connection.Scope, "ChatUsers");
			if( users_so == null )
			{
				CreateSharedObject(connection.Scope, "ChatUsers", false);
				users_so = GetSharedObject(connection.Scope, "ChatUsers");
			}
			users_so.SetAttribute(userName, userName);
			return true;
		}

		public override bool RoomJoin(IClient client, IScope room)
		{
            Trace.WriteLine("Room join " + room.Name);
			return true;
		}

		public override void RoomLeave(IClient client, IScope room)
		{
            Trace.WriteLine("Room leave " + room.Name);
            base.RoomLeave(client, room);
		}

		public override void AppDisconnect(IConnection connection)
		{
			string userName = connection.Client.GetAttribute("userName") as string;
            ISharedObject users_so = GetSharedObject(connection.Scope, "ChatUsers");
            if (users_so != null)
            {
                users_so.RemoveAttribute(userName);
            }
			base.AppDisconnect (connection);
		}

		public string getServerTime(object msg)
		{
			return DateTime.Now.ToString();
		}

		// The client will call this function to get the server
		// to accept the message, add the user's name to it, and
		// send it back out to all connected clients.
		public void msgFromClient(IConnection connection, string msg)
		{
			string userName = connection.Client.GetAttribute("userName") as string;
			msg = userName + ": " + msg;
			ISharedObject users_so = GetSharedObject(connection.Scope, "ChatUsers");
			users_so.SendMessage("msgFromSrvr", new object[]{msg});
		}
	}
}
