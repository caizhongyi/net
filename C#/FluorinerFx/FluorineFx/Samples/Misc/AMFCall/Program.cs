using System;
using FluorineFx;
using FluorineFx.Net;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.AMF3;

namespace AMFCall
{
    class Program
    {
        NetConnection _netConnection;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Connect();
            System.Console.ReadLine();
        }

        public void Connect()
        {
            // Create NetConnection client
            _netConnection = new NetConnection();
            _netConnection.ObjectEncoding = ObjectEncoding.AMF3;
            _netConnection.NetStatus += new NetStatusHandler(_netConnection_NetStatus);
            _netConnection.Connect("http://localhost:2896/WebSite/Gateway.aspx");

            System.Console.WriteLine("*** Flash RPC ***");
            _netConnection.Call("ServiceLibrary.MyDataService.GetCustomers", new GetCustomersHandler(), new object[] { "415" });
            System.Console.WriteLine("*** Flex RPC ***");
            _netConnection.Call("my-amf", "fluorine", "ServiceLibrary.MyDataService", "GetCustomers", new GetCustomersHandler(), new object[] { "415" });

            System.Console.WriteLine("Press 'Enter' to exit");
        }

        void _netConnection_NetStatus(object sender, NetStatusEventArgs e)
        {
            string level = e.Info["level"] as string;
            if (level == "error")
            {
                //received an error
                System.Console.WriteLine("Error: " + e.Info["code"] as string);
            }
            if (level == "status")
            {
                System.Console.WriteLine("Status: " + e.Info["code"] as string);
            }            
        }
    }

    public class GetCustomersHandler : IPendingServiceCallback
    {
        public void ResultReceived(IPendingServiceCall call)
        {
            object result = call.Result;
            System.Console.WriteLine("Server response: " + result);

            //DataAccess sample sends back an ArrayCollection (AMF3)
            ArrayCollection items = result as ArrayCollection;
            foreach (object item in items)
            {
                Flex.CustomerVO customer = item as Flex.CustomerVO;
                System.Console.WriteLine(customer.firstname + " " + customer.lastname);
            }
        }
    }
}
