using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            _netConnection.NetStatus += new NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Connect("http://localhost:1781/SilverlightApplicationWeb/Gateway.aspx");

        }

        void _netConnection_NetStatus(object sender, NetStatusEventArgs e)
        {
            string level = e.Info["level"] as string;
            if (level == "error")
            {
                //received an error
                Log("Error: " + e.Info["code"] as string);
                //System.Console.WriteLine("Error: " + e.Info["code"] as string);
            }
            if (level == "status")
            {
                //System.Console.WriteLine("Status: " + e.Info["code"] as string);
                Log("Status: " + e.Info["code"] as string);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _netConnection.Call("ServiceLibrary.MyDataService.GetCustomers", new GetCustomersHandler(this), new object[] { txtSearch.Text });
            //_netConnection.Call("my-amf", "fluorine", "ServiceLibrary.MyDataService", "GetCustomers", new GetCustomersHandler(this), new object[] { txtSearch.Text });
            //_netConnection.Call("ServiceLibrary.MyDataService.GetXDocument", new GetXDocumentHandler(this), new object[0]);
            //_netConnection.Call("ServiceLibrary.MyDataService.GetXElement", new GetXDocumentHandler(this), new object[0]);
        }

        public void Log(string msg)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                TxtLog.Text += msg + "\r\n";
            });
        }

        public void Bind(IList list)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                customersDataGrid.ItemsSource = list;
            });
        }

    }

    public class GetCustomersHandler : IPendingServiceCallback
    {
        Page _page;

        public GetCustomersHandler(Page page)
        {
            _page = page;
        }

        public void ResultReceived(IPendingServiceCall call)
        {
            object result = call.Result;

            //DataAccess sample sends back an ArrayCollection (AMF3)
            ArrayCollection items = result as ArrayCollection;
            foreach (object item in items)
            {
                Flex.CustomerVO customer = item as Flex.CustomerVO;
                _page.Log(customer.ToString());
            }
            _page.Bind(items);
        }
    }

    public class GetXDocumentHandler : IPendingServiceCallback
    {
        Page _page;

        public GetXDocumentHandler(Page page)
        {
            _page = page;
        }

        public void ResultReceived(IPendingServiceCall call)
        {
            object result = call.Result;
            _page.Log(result.ToString());
        }
    }

}
