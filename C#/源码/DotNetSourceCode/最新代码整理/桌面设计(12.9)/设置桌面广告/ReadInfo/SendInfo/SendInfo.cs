using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using WbService;
using System.Threading;
using GetIpInfo;
using UnCompress;
using System.Windows.Forms;

namespace WindowsApplication1.SendInfo
{
    class SendInfo
    {
        public static bool IsRun = false;
        IPInfo ipi = new IPInfo();
        private Socket sk;
        public void SendIpAdress()
        {
            //�������˿ںź�IP��ַ 
            try
            {
            int port = 2000;
            string host = ipi.Ipadress;
            //��þ�����������IP
            //string host = "192.168.1.12";
            //��û���IP��ַ
            string ipadress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            //��һ������ָ���Ķ˿ںźͷ�������ip����һ��EndPoint����

            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipep = new IPEndPoint(ip, port);

            //�ڶ���������һ��Socket����

           sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    
                //����������socket�����Connect()���������潨����EndPoint������Ϊ�������������������������
                sk.Connect(ipep);


                //���Ĳ���������ӳɹ�������socket�����Send()�����������������Ϣ��

                //����һ��IP��������
                string message = "YZWANTDATA:"+ipadress;
                byte[] bt = Encoding.ASCII.GetBytes(message);
                sk.Send(bt, bt.Length, 0);  
                //��������ͨ�Ž�����һ���ǵùر�socket��
                sk.Close();
            }
            catch
            {
                //���������ȡͼƬ
            }
            finally
            {
                //��������������
                IService isce = new WbService.Service();
                Thread td = isce.ServiceStart();
            }
        }
    }
}
