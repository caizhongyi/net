using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using GetIpInfo;
using WbSystem.FileManage;
using WindowsApplication1.ListenInfo;
using WbSystem;

namespace WbService
{
    class ClassSession
    {
        private Socket chat;
        private string pack;
        private static int filecount=0;
        private IFileOperate ifo = FileFactory.GetFileInfo();
        public ClassSession(Socket chat)
        {
            this.chat = chat;           
        }
        public void StartChat()
        {
                //��ȡԶ��������IP��ַ�Ͷ˿ں�
                IPEndPoint ep = (IPEndPoint)chat.RemoteEndPoint;
                //����
                byte[] buff = new byte[1024];
                int len;
                while ((len = chat.Receive(buff)) != 0)
                {
                    string msg = Encoding.Default.GetString(buff, 0, len);
                    pack = msg.Substring(0, 1);
                    string cmd = msg.Substring(1, 5);
                    if (cmd == "YZDAT")
                    {
                        string[] s = msg.Split(':');
                        int count = Convert.ToInt32(s[1]);
                        NewMethod(ep, buff, ref len, ref msg);
                        filecount=filecount+1;
                        if (count == filecount)
                        {
                            //ִ��ɾ��������
                            if (File.Exists("Adpictures"))
                            {
                                ifo.DelFolder(Directory.GetCurrentDirectory() + "\\" + "Adpictures");
                            }
                            string oldname = Directory.GetCurrentDirectory() + "\\" + "NewAdpictures";
                            string newname = Directory.GetCurrentDirectory() + "\\" + "Adpictures";
                            ifo.RechristenFolderName(oldname, newname);

                            //������ر�����IP
                            ClassBroadCast broadCast = new ClassBroadCast();
                            Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
                            tBroadCast.IsBackground = true;
                            tBroadCast.Start();
                            //��������
                            ListenInfo Linfo = new ListenInfo();
                            Thread td = new Thread(new ThreadStart(Linfo.ListenStart));
                            td.IsBackground = true;
                            td.Start();
                            //��ʾAd��洰��
                            AdForm bf = new AdForm();
                            bf.ShowDialog();
                        }
                    }
                }  
                chat.Close();
        }
        private void NewMethod(IPEndPoint ep, byte[] buff, ref int len, ref string msg)
        {
            string filename = Directory.GetCurrentDirectory() + "\\" + "NewAdpictures" + msg.Substring(msg.LastIndexOf('\\'));
            FileStream writer = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            while ((len = chat.Receive(buff)) != 0)
            {
                msg = Encoding.Default.GetString(buff, 0, len);
                if (msg == "YZEND")
                {
                    break;
                }
                writer.Write(buff, 0, len);
            }
            writer.Write(buff, 0, len);
            writer.Close();
        }
    }
}
