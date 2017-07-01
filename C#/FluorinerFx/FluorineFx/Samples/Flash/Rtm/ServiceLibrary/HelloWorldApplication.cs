using System;
using FluorineFx.Context;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Messaging.Api.Service;

namespace ServiceLibrary
{
    public class HelloWorldApplication : ApplicationAdapter
    {
        public string serverHelloMsg(string helloStr)
        {
            return "Hello, " + helloStr + "!";
        }
    }
}
