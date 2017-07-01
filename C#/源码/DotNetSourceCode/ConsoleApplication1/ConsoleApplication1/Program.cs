using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            adsf();
        }

        public static void adsf()
        {
            try 
            {
                System.Net.WebClient WebClientObj = new System.Net.WebClient();
                System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
                PostVars.Add("Wb_Id", "10102120");

                byte[] byRemoteInfo = WebClientObj.UploadValues("http://www.wb66.cn/SelAdvInfoByWbId.aspx", "POST", PostVars);
                //下面都没用啦，就上面一句话就可以了
                string sRemoteInfo = System.Text.Encoding.GetEncoding("UTF-8").GetString(byRemoteInfo).Trim();

                Console.Write(sRemoteInfo);

                string path = Environment.CurrentDirectory;
                StreamWriter sw = File.CreateText(path + @"/AdvInfo.xml");
                sw.Write(sRemoteInfo);
                sw.Close();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("AdvInfo.xml");
                XmlNode root = xmlDoc.SelectSingleNode("NewDataSet");//查找<NewDataSet>
                XmlElement xe1 = xmlDoc.CreateElement("Time");
                XmlElement xe2 = xmlDoc.CreateElement("CreateTime");
                xe2.InnerText = "2008-7-9 00:00:00";
                xe1.AppendChild(xe2);
                root.AppendChild(xe1);//添加到<NewDataSet>节点中
                xmlDoc.Save("AdvInfo.xml");                   
                
            }
            catch (WebException webEx)
            {
                Console.Write(webEx.Message);
            }
        }
    }
}

