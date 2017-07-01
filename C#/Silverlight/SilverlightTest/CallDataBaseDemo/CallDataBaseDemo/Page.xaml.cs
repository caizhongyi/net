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
using System.ServiceModel;
using System.Windows.Data;



namespace CallDataBaseDemo
{
    public partial class Page : UserControl
    {

        List<ServiceReference1.UserInfo> list;
        ServiceReference1.WebService1SoapClient client;
        public Page()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
            
        }

        protected void Page_Loaded(object sender, RoutedEventArgs args)
        {
        
            //BasicHttpBinding binding = new BasicHttpBinding();
            //EndpointAddress address = new EndpointAddress("http://localhost/wcfCallDataBase.svc");
            //_client = new ServiceReference1.Service1Client(binding, address);
            //ChannelFactory<ServiceReference1.IService1> cf = new ChannelFactory<CallDataBaseDemo.ServiceReference1.IService1>(binding ,address );
            //ServiceReference1.IService1 sv1 = cf.CreateChannel();
            //_client.SelectUserInfoCompleted += new EventHandler<CallDataBaseDemo.ServiceReference1.SelectUserInfoCompletedEventArgs>(_client_SelectUserInfoCompleted);
            //_client.SelectUserInfoAsync();
            client = new CallDataBaseDemo.ServiceReference1.WebService1SoapClient();
            client.SelectUserInfoCompleted+=new EventHandler<CallDataBaseDemo.ServiceReference1.SelectUserInfoCompletedEventArgs>(client_SelectUserInfoCompleted);
            client.SelectUserInfoAsync();
           
        }

        protected void client_SelectUserInfoCompleted(object sender, CallDataBaseDemo.ServiceReference1.SelectUserInfoCompletedEventArgs args)
        {
           
            listbox.ItemsSource = args.Result;
            list = args.Result.ToList<ServiceReference1.UserInfo>();
            
        }

      

       
    }

    

}
