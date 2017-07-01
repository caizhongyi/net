
using System;

using System.Collections.Generic;

using System.Linq;

using System.Net;

using System.Windows;

using System.Windows.Controls;

using System.Windows.Data;

using System.Windows.Documents;

using System.Windows.Input;

using System.Windows.Media;

using System.Windows.Media.Animation;

using System.Windows.Shapes;

using System.Data.Services.Client;
using System.ComponentModel;
using System.ServiceModel;
using System.Xml.Linq;



namespace czy.Silverlight.Demo
{

    public partial class Demo_DataGrid : UserControl
    {

        int PageSize = 2;//设定分页大小
        List<int> itemCount = new List<int>();//用于DataPager的数据提供
        int cur = 1;

        public Demo_DataGrid()
        {

            InitializeComponent();

            //注册事件触发处理
           // this.Loaded += new RoutedEventHandler(MainPage_Loaded);

           // Binding bind = new Binding();
          //  EndpointAddress endPoint = new EndpointAddress("");

            ServiceReference1.WebService1SoapClient clinet = new ServiceReference1.WebService1SoapClient();
            clinet.GetPagedUserInfoInfoAsync(1, cur);
            clinet.GetPagedUserInfoInfoCompleted+=new EventHandler<ServiceReference1.GetPagedUserInfoInfoCompletedEventArgs>(clinet_GetPagedUserInfoInfoCompleted);
            clinet.GetTotalPagersAsync(1);
            clinet.GetTotalPagersCompleted+=new EventHandler<ServiceReference1.GetTotalPagersCompletedEventArgs>(clinet_GetTotalPagersCompleted);
        }
        public void clinet_GetPagedUserInfoInfoCompleted(object o, ServiceReference1.GetPagedUserInfoInfoCompletedEventArgs e)
        {

            XDocument document = XDocument.Parse(e.Result.Nodes[1].ToString ());
            var categories = from datatable in document.Descendants("Table")
                             select new UserInfo
                             {
                                 ID = datatable.Element("userInfo_id").Value,
                                 Name = datatable.Element("userInfo_user").Value
                             };

            dataGrid1.ItemsSource = categories;
        }
        public void clinet_GetTotalPagersCompleted(object o, ServiceReference1.GetTotalPagersCompletedEventArgs e)
        {
            dataPager1.SetDataPager(e.Result);
            dataPager1.PageIndexChanging+=new EventHandler<System.ComponentModel.CancelEventArgs>(dataPager1_PageIndexChanging);
        }
        private void dataPager1_PageIndexChanging(object o, CancelEventArgs e)
        {
            ServiceReference1.WebService1SoapClient clinet = new ServiceReference1.WebService1SoapClient();
            cur = (o as DataPager).PageIndex;
            clinet.GetPagedUserInfoInfoAsync(1, cur);
            clinet.GetPagedUserInfoInfoCompleted += new EventHandler<ServiceReference1.GetPagedUserInfoInfoCompletedEventArgs>(clinet_GetPagedUserInfoInfoCompleted);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
       
    }
    public class UserInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

}