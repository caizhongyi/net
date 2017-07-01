using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using FluorineFx;
using FluorineFx.Net;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.AMF3;

namespace SilverlightChatApplication
{
    public partial class Page : UserControl
    {
        NetConnection _netConnection;
        RemoteSharedObject _sharedObject;

        public Page()
        {
            InitializeComponent();

            // Create NetConnection client
            _netConnection = new NetConnection();
            _netConnection.ObjectEncoding = ObjectEncoding.AMF0;
            _netConnection.OnConnect += new ConnectHandler(_netConnection_OnConnect);
            _netConnection.OnDisconnect += new DisconnectHandler(_netConnection_OnDisconnect);
            _netConnection.NetStatus += new NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Client = this;
            //FMS test
            _netConnection.Connect("rtmp://localhost:4502/sotest");
        }

        void _netConnection_NetStatus(object sender, NetStatusEventArgs e)
        {
            string level = null;
            if (e.Info.ContainsKey("level"))
                level = e.Info["level"] as string;
            if (level == "error")
            {
                //received an error
                Log("Error: " + e.Info["code"] as string);
                Log("Client not connected.");
            }
            if (level == "status")
            {
                Log("Status: " + e.Info["code"] as string);
            }

        }

        void _netConnection_OnDisconnect(object sender, EventArgs e)
        {
            Log("Connection disconnected.");
        }

        void _netConnection_OnConnect(object sender, EventArgs e)
        {
            Log("Connected to server. Connecting to RSO...");
            _sharedObject = RemoteSharedObject.GetRemote(typeof(UsersRSO), "users", _netConnection.Uri.ToString(), false);
            (_sharedObject as UsersRSO).Page = this;

            _sharedObject.OnConnect += new ConnectHandler(_sharedObject_OnConnect);
            _sharedObject.OnDisconnect += new DisconnectHandler(_sharedObject_OnDisconnect);
            _sharedObject.NetStatus += new NetStatusHandler(_sharedObject_NetStatus);
            _sharedObject.Sync += new SyncHandler(_sharedObject_Sync);
            _sharedObject.Connect(_netConnection);            
        }

        void _sharedObject_Sync(object sender, SyncEventArgs e)
        {
            ASObject[] changeList = e.ChangeList;
            for (int i = 0; i < changeList.Length; i++)
            {
                ASObject info = changeList[i];
                if (info.ContainsKey("name") && info["name"] != null)
                    Log(info["name"].ToString() + " - " + info["code"].ToString());
                else
                    Log(info["code"].ToString());
            }
        }

        void _sharedObject_NetStatus(object sender, NetStatusEventArgs e)
        {
            string level = e.Info["level"] as string;
            if (level == "error")
            {
                //received an error
                Log("Error: " + e.Info["code"] as string);
            }
            if (level == "status")
            {
                Log("Status: " + e.Info["code"] as string);
            }
        }

        void _sharedObject_OnDisconnect(object sender, EventArgs e)
        {
            Log("Disconnected RSO.");
        }

        void _sharedObject_OnConnect(object sender, EventArgs e)
        {
            Log("Connected to RSO.");
        }

        public void setHistory(string history)
        {
            Log(history);
        }

        /// <summary>
        /// Submit text to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_sharedObject != null && _sharedObject.Connected)
            {
                _netConnection.Call("msgFromClient", null, InputTextBox.Text);
            }
            else
            {
                Log("Not connected to the server, reload page and try again.");
            }
        }

        public void Log(string msg)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                ChatConsole.Text += msg + "\n";
                ScrollBar.ScrollToVerticalOffset(ScrollBar.ScrollableHeight);
            });
        }

        public void LogMessage(string msg)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                ChatConsole.Text += msg;
                ScrollBar.ScrollToVerticalOffset(ScrollBar.ScrollableHeight);
            });
        }
    }
}
