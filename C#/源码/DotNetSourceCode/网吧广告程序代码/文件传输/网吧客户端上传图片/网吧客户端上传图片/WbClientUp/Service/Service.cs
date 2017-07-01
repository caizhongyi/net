using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace WbService
{
    public class Service:IService
    {
        //������Ϊ�������˽�����Ϣ�׽���
        public Socket socketReceive = null;

        //���������Ϣ��IP��ַ�Ͷ˿ں�
        public IPEndPoint ipReceive = null;

        //���巢����Ϣ��IP��ַ�Ͷ˿ں�
        public IPEndPoint ipSent = null;

        //���������Ϣ���׽���
        public Socket chat = null;

        ServiceInfo si = new ServiceInfo();

        private void ReceiveNews()
        {
            try
            {
                //��ʼ�������׽��֣�Ѱַ���������ַ�����ʽ��Tcpͨ��
                socketReceive = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream,
                 ProtocolType.Tcp);

                //��ȡ����IP��ַ�����ý�����Ϣ�Ķ˿�
                ipReceive = new IPEndPoint(
                  Dns.GetHostEntry(Dns.GetHostName()).AddressList[0],si.Port);

                //������IP��ַ�ͽ��ܶ˿ڰ󶨵������׽���
                socketReceive.Bind(ipReceive);

                //�����˿ڣ������ü��������СΪ1024byte
                socketReceive.Listen(1024);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            //���������Ϣʱ������
            byte[] buff = new byte[1024];

            //�������ܿͻ��˷��͹�������Ϣ

            while (true)
            {
                //����һ��chat�׽�������������Ϣ
                Socket chat = socketReceive.Accept();


                //����һ��������Ϣ�Ķ���
                ClassSession cs = new ClassSession(chat);

                //����һ���µ��߳��������������������͵���Ϣ
                Thread newThread = new Thread(new ThreadStart(cs.StartChat));

                //�����µ��߳�
                newThread.Start();
            }

        }
        public Thread ServiceStart()
        {
            Thread td = new Thread(new ThreadStart(ReceiveNews));
            td.Start();
            return td;  
        }
    }
}
