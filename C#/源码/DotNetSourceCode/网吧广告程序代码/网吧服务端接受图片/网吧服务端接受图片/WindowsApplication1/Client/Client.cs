using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace WbClient
{
    public class Client:IConnectionService
    {
        #region �ֶ�
        private Socket skClient;
        private IPEndPoint ipep;
        private string[] MyFileName;
        private string filepath;
        #endregion

        //��ʼ������

        ClientInfo clientinfo = new ClientInfo();
        void Socketconnction()
        {
            skClient.Connect(ipep);
        }
        void SentFile()
        {
            string mge = "ODAT" + filepath;

            //��mgeת���������д���
            skClient.Send(Encoding.Default.GetBytes(mge));

            //����һ�����ļ���
            FileStream filestream = new FileStream(filepath,FileMode.Open,FileAccess.Read);
            //���û�����Ϊ1024byte
            byte[] buff=new byte[1024];
            int lenth = 0;
            try
            {
                while ((lenth = filestream.Read(buff, 0, 1024))!=0)
                {
                    //��ԭ���ֽ��������д���
                    skClient.Send(buff, 0, lenth, SocketFlags.None);

                }
                //��Ҫ������Ϣ��������"END"��ʶ��
                mge = "END";
                //����mge��ʼ
                skClient.Send(Encoding.Default.GetBytes(mge));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //�ر�����
                skClient.Close();
                filestream.Close();
            }

        }

        void ConnectionService()
        {
            //�ҵ�ScreenPricture�ļ���
            string Foldpath = Directory.GetCurrentDirectory() + "\\" +"Adpictures";
            //��ȡ�����ļ�
            MyFileName = Directory.GetFiles(Foldpath);
            foreach (string filename in MyFileName)
            {
                filepath = filename;
                try
                {
                    //////���ö˿ں�
                    //int port = clientinfo.Prot;
                    ////��ȡip
                    //string host = clientinfo.Host;
                    //��һ������ָ���Ķ˿ںźͷ�������ip����һ��EndPoint����
                    IPAddress ip = IPAddress.Parse(clientinfo.Host);
                    ipep = new IPEndPoint(ip, 8999);

                    //�ڶ���������һ��Socket����
                    skClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    //����������socket�����Connect()���������潨����EndPoint������Ϊ�������������������������
                    ClassSocket socketConnet = new ClassSocket(skClient, ipep);
                    Thread tconnection = new Thread(new ThreadStart(Socketconnction));
                    tconnection.Start();
                    Thread.Sleep(500);
                    //��Ҫ�ϴ����ļ��Ӹ�DAT��ʶ

                    Thread tsent = new Thread(new ThreadStart(SentFile));
                    tsent.Start();

                    Thread.Sleep(1000);
                }
                catch (SocketException ex)
                {
                    throw ex;
                }
            }
        }

        #region IConnectionService ��Ա

        public Thread GetConnection()
        {
            Thread td = new Thread(new ThreadStart(ConnectionService));
            td.IsBackground = true;
            td.Start();
            return td;
        }

        #endregion
    }
}
