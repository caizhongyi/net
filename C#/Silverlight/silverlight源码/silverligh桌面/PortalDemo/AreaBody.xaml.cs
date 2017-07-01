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
using System.Collections.ObjectModel;
using System.Windows.Browser;
using System.IO;
using System.Text;
using System.Xml;
//５1aｓｐｘ
namespace PortalDemo
{
    public partial class AreaBody : UserControl
    {
        public AreaBody()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 信息列表
        /// </summary>
        ObservableCollection<Information> Infos = new ObservableCollection<Information>();

        /// <summary>
        /// 为此Body提供数据服务的Url
        /// </summary>
        public string DataServiceUrl { get; set; }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DownloadData();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 下载数据
        /// </summary>
        public void DownloadData()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringAsync(new Uri(Consts.BaseUrl + DataServiceUrl));
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDataCompleted);
        }

        /// <summary>
        /// 加载数据完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadDataCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                int p = e.Result.IndexOf("&&");
                string result = e.Result.Substring(p + 2);
                result = result.Replace("<p>", "<p>\r\n").Replace("</a>", "</a>\r\n").Replace("</p>", "</p>\r\n");
                StringBuilder sb = new StringBuilder();
                sb.Append("<informations>\r\n");
                sb.AppendFormat("{0}\r\n", result);
                sb.Append("</informations>\r\n");

                GenerateInfos(sb.ToString());

                lstInformation.ItemsSource = Infos;
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// 列表选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Information info = lstInformation.SelectedItem as Information;
                HtmlPage.Window.Eval(info.Href);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 根据XML生成Information列表
        /// </summary>
        /// <param name="xml"></param>
        private void GenerateInfos(string xml)
        {
            Infos.Clear();
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream ms = new MemoryStream(bytes);

            XmlReader reader = XmlReader.Create(ms);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "p")
                {
                    Information info = new Information(reader.ReadOuterXml());
                    Infos.Add(info);
                }
            }

            reader.Close();
            ms.Close();
        }
    }
}
