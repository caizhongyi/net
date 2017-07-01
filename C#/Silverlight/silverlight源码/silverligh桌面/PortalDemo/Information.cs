using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Text;

namespace PortalDemo
{
    public class Information
    {
        public Information()
        {
        }

        /// <summary>
        /// 根据XML生成Information对象
        /// </summary>
        /// <param name="xml"></param>
        public Information(string xml)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream ms = new MemoryStream(bytes);

            XmlReader reader = XmlReader.Create(ms);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "a")
                {
                    this.Title = reader.GetAttribute("title");
                    this.Href = reader.GetAttribute("href");
                    this.ReadState = reader.GetAttribute("class");  
                }
            }

            reader.Close();
            ms.Close();
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 阅读状态
        /// </summary>
        public string ReadState { get; set; }
    }
}
