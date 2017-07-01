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

namespace SilverlightApplication
{
    public partial class Page : UserControl
    {
        NetConnection _netConnection;

        public Page()
        {
            InitializeComponent();

            // Create NetConnection client
            _netConnection = new NetConnection();
            _netConnection.ObjectEncoding = ObjectEncoding.AMF3;
            _netConnection.OnConnect += new ConnectHandler(_netConnection_OnConnect);
            _netConnection.NetStatus += new NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Connect("rtmp://localhost:4502/HelloWorld");
        }

        void _netConnection_OnConnect(object sender, EventArgs e)
        {
        }

        void _netConnection_NetStatus(object sender, NetStatusEventArgs e)
        {
            string level = e.Info["level"] as string;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Log("Calling server method 'serverHelloMsg'");
            _netConnection.Call("serverHelloMsg", new ServerHelloMsgHandler(this), "some text");
        }

        public void Log(string msg)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                TxtLog.Text += msg + "\r\n";
            });
        }
    }

    public class ServerHelloMsgHandler : IPendingServiceCallback
    {
        Page _page;

        public ServerHelloMsgHandler(Page page)
        {
            _page = page;
        }

        public void ResultReceived(IPendingServiceCall call)
        {
            object result = call.Result;
            _page.Log("Server response: " + result);
        }
    }
}
