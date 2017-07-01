using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using GetIpInfo;
using WbSystem;
using WindowsApplication1.SendInfo;
using WindowsApplication1.ListenInfo;
using System.Threading;
using SetWallpaper;
using WbSystem.GetIp;
using System.Data;
using WbSystem.TimeManage;
using System.IO;
using System.Xml;

namespace ClassStartUdpThreadnamespace
{

    class ClassStartUdpThread
    {
        FileDateTimeInfo fdti = new FileDateTimeInfo();
        ICompareTime ict = FactoryTime.GetTimeInfo();
        public void StartUdpThread()
        {
            UdpClient server = new UdpClient(7999);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (LanTimeManageInfo.IsTrue)
            {
                try
                {
                    byte[] buff = server.Receive(ref ep);
                    string user = Encoding.Default.GetString(buff);
                    string cmd = user.Substring(0, 10);
                    string user1 = user.Substring(10);
                    if (cmd == ":YZNewData")
                    {
                        string[] s = user1.Split(':');
                        string ip = s[2].ToString();
                        string datatime = s[3].ToString()+":"+s[4]+":"+s[5];
                        //判断局域的信息与服务器的时间相比

                        //获取局域网内的IP
                        if (ip == Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString())
                        {
                            continue;
                        }
                        else
                        {
                            //增加IP信息
                            LanTimeManageInfo.Addmydr(ip,datatime);
                        }
                    }
                    else
                    {
                        GetWork();
                    }
                }
                catch
                {
                    return;
                }
            }
        }
        //启用服务下载时的工作
        void GetWork()
        {
            //下载XML文件
            GetXMLInfo();
            //服务器下载
            DownPicture.DownPictureInfo();

            //启动广播自己的IP地址信息
            ClassBroadCast broadCast = new ClassBroadCast();
            Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
            tBroadCast.IsBackground = true;
            tBroadCast.Start();

            //启动侦听别机子接收IP信息
            ListenInfo Linfo = new ListenInfo();
            Thread td = new Thread(new ThreadStart(Linfo.ListenStart));
            td.IsBackground = true;
            td.Start();

            //显示Ad广告窗体
            AdForm bf = new AdForm();
            bf.ShowDialog();
        }
        //时间比较过程
        public void GetTimeInfo()
        {
            if (ict.GetTime(fdti.Filedatetime, fdti.Lantime))
            {
                if (ict.GetTime(fdti.Filedatetime, fdti.Servicetime))
                {
                    /*直接启动程序*/

                    //定义加载本机的IP
                    ClassBroadCast broadCast = new ClassBroadCast();
                    Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
                    tBroadCast.IsBackground = true;
                    tBroadCast.Start();
                    //启动侦听
                    ListenInfo Linfo = new ListenInfo();
                    Thread td = new Thread(new ThreadStart(Linfo.ListenStart));
                    td.IsBackground = true;
                    td.Start();
                    //显示Ad广告窗体
                    AdForm bf = new AdForm();
                    bf.ShowDialog();
                }
                else
                {
                    /*从服务器下载*/
                    GetWork();
                }
            }
            else
            {
                if (ict.GetTime(fdti.Lantime, fdti.Servicetime))
                {
                    /*从局域网下载*/
                    SendInfo sinfo = new SendInfo();
                    sinfo.SendIpAdress();
                }
                else
                {
                    /*从服务器下载*/
                    GetWork();
                }
            }
        }
        void GetXMLInfo()
        {
            //------------根据网吧Id将广告信息和最新广告时间添加到AdvInfo.xml文件中------------\\
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

            byte[] TimeInfo = WebClientObj.UploadValues("http://www.wb66.cn/SelIssueTime.aspx", "POST", PostVars);
            DateTime IssueTime = Convert.ToDateTime(System.Text.Encoding.GetEncoding("UTF-8").GetString(TimeInfo).Trim());

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("AdvInfo.xml");
            XmlNode root = xmlDoc.SelectSingleNode("NewDataSet");//查找<NewDataSet>
            XmlElement xe1 = xmlDoc.CreateElement("Time");
            XmlElement xe2 = xmlDoc.CreateElement("CreateTime");
            xe2.InnerText = IssueTime.ToString();
            xe1.AppendChild(xe2);
            root.AppendChild(xe1);//添加到<NewDataSet>节点中
            xmlDoc.Save("AdvInfo.xml");


        }
    }
}
