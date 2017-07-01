using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;


namespace MyClass.Message
{
    public class MsgAndFileTransmission
    {
        /*
            #region ����
            private Thread t;
            private UdpClient receivingUdpClient;
            private string stFlag = null;
            System.Timers.Timer timer = new System.Timers.Timer(1000);//����UDP�㲥ʱ����
            System.Timers.Timer timerA = new System.Timers.Timer(1000);

            private string filePathSend = null;
            private string fileNameSend;
            private string fileNameAccept;
            private string filePathAccept = null;
            private Thread fileThreadSend;
            private TcpClient fileTcpClientSend;
            private BinaryReader brSend=null;
            private TcpListener fileListen;
            private Thread fileListenThread;
            private TcpClient fileClientAccept;
            private Thread fileThreadAccept;
            private BinaryWriter bwAccept;
            private Int64 recSize = 0,size=0;//�����ļ���С,�ļ������С
            private float recFloat = 0, recFile = 0;
            private float sendFloat = 0, sendFile = 0;
            private bool acceptFlag = true;
            private int timeFlagSend = 0;
            private float sendfloat = 0,acceptfloat=0;
            private int timeFlagAccept = 0;
            private bool sendFlag = true;
            #endregion

            #region �����з��͵�ʵ��Send_Click

             //<summary>
             //�����з��͵�ʵ��
             //</summary>
             //<param name="txtMessage">Ҫ���͵���Ϣ</param>
             //<param name="listView1">listview��ʾ�û�</param>
             //<param name="txtShowMessage">��ʾ����Ϣ</param>
            private void SendClick(string txtMessage,ListView listView1,string txtShowMessage)
            {
                int FlagChat = 0;
                if (txtMessage=="") 
                {
                    MessageBox.Show("��Ϣ����Ϊ��");
                }
                else
                {
                UdpClient udpClient = new UdpClient();
                Byte[] sendBytes = Encoding.GetEncoding("gb2312").GetBytes("chat" + txtMessage);
                ListView.CheckedListViewItemCollection checkedItemsChat = listView1.CheckedItems;

                foreach (ListViewItem item in checkedItemsChat)
                foreach (string item in list)
                {
                    try
                    {
                        if (item.SubItems[0].Text.ToString().Equals(Dns.GetHostName().ToString() + "-����"))
                        {
                            txtShowMessage += "�����Լ�˵:" + "  " + DateTime.Now.ToString() + "\r\n" + "  " + txtMessage + "\r\n";

                        }
                        else

                            txtShowMessage += "����" + item.SubItems[0].Text.ToString() + "˵:" + "  " + DateTime.Now.ToString() + "\r\n" + "  " + txtMessage+ "\r\n";
                            udpClient.Send(sendBytes, sendBytes.Length, new IPEndPoint(IPAddress.Parse(item.SubItems[1].Text.ToString()), 10000));

                            FlagChat = 1;

                    }

                    catch (Exception f)
                    {
                        MessageBox.Show(f.ToString());
                    }

                }
                if (FlagChat == 0)
                    MessageBox.Show("��ѡ�������û�");
                else
                    txtMessage = string.Empty;
               udpClient.Close();

            }
        }
            #endregion

        #region Fromload
        private void Form1Load()
        {

            sendFlag = true;
            fileListen = new TcpListener(IPAddress.Any,10001);
            fileListen.Start();
            fileListenThread = new Thread(new ThreadStart(listenFileConnect));
            fileListenThread.Start();
            this.DragEnter+=new DragEventHandler(Form1_DragEnter);
            t = new Thread(new ThreadStart(TCall));
            t.Start();
            this.Disposed+=new EventHandler(Form1_Disposed);
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed+=new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            this.FormClosing+=new FormClosingEventHandler(Form1_Closing);
        }
        #endregion

        #region   �����㲥 ��  ������Ϣ

         //<summary>
         //�����㲥 ��  ������Ϣ
         //</summary>
         //<param name="listView1">��ʾ���û�</param>
        public void TCall(ListView listView1)
        {
            �����˿�
            Creates a UdpClient for reading incoming data.
            receivingUdpClient = new UdpClient(10000);
            Creates an IPEndPoint to record the IP Address and port number of the sender. 
             The IPEndPoint will allow you to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                while (true)
                {

                    ���չ㲥
                     Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                    string returnData = Encoding.GetEncoding("gb2312").GetString(receiveBytes);

                    for (int k = 0; k < 4; k++)
                    stFlag += returnData[k];

                    string newreturnData="";
                    for (int n = 4; n < returnData.Length;n++ )
                        newreturnData +=returnData[n];
                    MessageBox.Show(newreturnData);
                    if (stFlag.Equals("cast"))
                    {
                        stFlag = "";
                        string newreturnData="";
                        newreturnData = returnData.Remove(0, 3);
                        if (listView1.Items.ContainsKey(newreturnData + "-����") == true)
                        {
                        }
                        else
                        {
                            if (listView1.Items.ContainsKey(newreturnData) == true)
                            {
                                MessageBox.Show("the same HostName");
                            }

                            else
                            {



                                ListViewItem pp = new ListViewItem();
                                ListViewItem.ListViewSubItem qq = new ListViewItem.ListViewSubItem();
                                if (newreturnData.Equals(Dns.GetHostName().ToString()))
                                    pp.Text = newreturnData + "-����";
                                else pp.Text = newreturnData;
                                pp.Name = pp.Text;
                                qq.Text = RemoteIpEndPoint.Address.ToString();
                                listView1.Items.Add(pp);
                                pp.SubItems.Add(qq);
                            }
                        }

                    }
                    else if (stFlag.Equals("chat"))
                    {
                        stFlag = "";
                        GetHostName(RemoteIpEndPoint.Address.ToString());
                        textBox1.Text += Dns.GetHostName().ToString() + "����˵" + "   " + DateTime.Now.ToString() + "\r\n" + "  " + newreturnData + "\r\n";
                        Dns.GetHostByAddress(   "192.168.3.115"   ).HostName
                        foreach (ListViewItem item in listView1.Items)
                            if (item.SubItems[1].Text.ToString().Equals(RemoteIpEndPoint.Address.ToString()))
                            {
                                string _hostName = null;
                                _hostName = item.SubItems[0].Text;

                                if (_hostName.Equals(Dns.GetHostName().ToString()+"-����"))
                                {
                                    break;
                                }
                                else

                                    textBox1.Text += _hostName + "����˵:" + "   " + DateTime.Now.ToString() + "\r\n" + "  " + newreturnData + "\r\n";

                                textBox1.Text += Dns.GetHostEntry(RemoteIpEndPoint.Address.ToString()).HostName.ToString() + "����˵" + "   " + DateTime.Now.ToString() + "\r\n" + "  " + newreturnData + "\r\n";
                                MessageBox.Show("���ˡ���������");
                            }
                    }
                    else if (stFlag.Equals("quit"))
                    {
                        stFlag = "";
                         foreach (ListViewItem item in listView1.Items)
                             if (item.SubItems[1].Text.ToString().Equals(RemoteIpEndPoint.Address.ToString()))
                             {
                                 this.listView1.Items.Remove(item);
                             }

                    }



               }
               }
                    }
                    catch (Exception g)
                    {
                        MessageBox.Show(g.ToString());
                   }


               }
        #endregion

        #region ���͹㲥�������û�����ʾ��������

         //<summary>
         // ���͹㲥�������û�����ʾ��������
         //</summary>
         //<param name="ip">�㲥��ַ</param>
        private void timer_Elapsed(string ip)
        {


            UdpClient udpClient = new UdpClient();
            Byte[] sendBytes = Encoding.ASCII.GetBytes("cast" + Dns.GetHostName());
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length, new IPEndPoint(IPAddress.Parse(ip), 10000));
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        #endregion

        #region   Form1Disposed �ͷ������Դ
          //�ͷ������Դ
        public void Form1Disposed()
        {
            if (t != null && t.ThreadState != ThreadState.Aborted)
            {
                t.Abort();
            }

            if (receivingUdpClient != null)
            {
                receivingUdpClient.Close();
            }
            if (timer != null)
                timer.Stop();
             if (timerA != null)
               timerA.Stop();
            if (fileThreadAccept != null && fileThreadAccept.ThreadState != ThreadState.Aborted)
                fileThreadAccept.Abort();
            if (fileListenThread != null && fileListenThread.ThreadState != ThreadState.Aborted)
                fileListenThread.Abort();
            if (fileListen != null)
                fileListen.Stop();
            if (receivingUdpClient1 != null) 
            {
                receivingUdpClient1.Close();

            }

        }
        #endregion

        #region ��������¼ClearMessageClick

         //<summary>
         //��������¼
         //</summary>
         //<param name="msg">Ҫ��յ���Ϣ</param>
        private void ClearMessageClick(string msg)
        {
            msg = string.Empty;
            MessageBox.Show("�����¼�����");
        }
        #endregion

        #region ���������¼SaveMessageClick

         //<summary>
         //���������¼
         //</summary>
         //<param name="chatMsg">������Ϣ</param>
        private void SaveMessageClick(string chatMsg)
        {
            string st1 = "";
            DateTime dt = DateTime.Now;
            string st = dt.Date.ToShortDateString();
            string path = Directory.GetCurrentDirectory() + st + ".txt";

            if (File.Exists(path))
            {
                FileStream fs = File.Open(path, FileMode.Open);

                StreamReader sr = new StreamReader(fs);
                st1 = sr.ReadToEnd();
                fs.Close();
                sr.Close();
                File.Delete(path);
            }

            FileStream fs1 = File.Open(path, FileMode.Create);

            StreamWriter sw = new StreamWriter(fs1);
            sw.WriteLine(chatMsg);
            sw.WriteLine("-----------------------------------");
            sw.WriteLine(st1);

            sw.Close();
            MessageBox.Show("����ɹ�");
        }
        #endregion

          #region �رմ�����ʾ  �����û�������Ϣ
        //�رմ�����ʾ  �����û�������Ϣ
         private void Form1Closing(object sender, System.ComponentModel.CancelEventArgs e)
          {


              UdpClient udpClient = new UdpClient();
              Byte[] sendBytes = Encoding.GetEncoding("gb2312").GetBytes("quit" + textBox2.Text);
              try
              {
                  udpClient.Send(sendBytes, sendBytes.Length, new IPEndPoint(IPAddress.Parse("172.17.196.255"), 10000));
              }
              catch (Exception f)
              {
                  MessageBox.Show(f.ToString());
              }
              timer.Stop();

              if (MessageBox.Show("�����Ƿ�Ҫ�ر�Send����", "��Ϣ��ʾ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
              {
                  e.Cancel = true;
                  timer.Start();
              }
          }
        #endregion

        #region  ѡ��Ҫ���͵��ļ�

         //<summary>
         //ѡ��Ҫ���͵��ļ�
         //</summary>
         //<param name="sendOrClear">�����ļ���ȡ���ļ�</param>
         //<param name="txtShowMessage">��ʾ����״̬</param>
         //<returns>trueΪ�������,falseΪ���ȡ��</returns>
        private bool OpenFileClick(string sendOrClear,string txtShowMessage)

        {

         /////////////////////////////
            if (sendOrClear.Equals("�����ļ�"))
            {
                OpenFileDialog openFile = new OpenFileDialog();

                openFile.ShowDialog();
                filePathSend = openFile.FileName;
                fileNameSend = Path.GetFileName(filePathSend);
                fileThreadSend = new Thread(new ThreadStart(fileBeginSend));
                fileThreadSend.Start();
                return true;
            }
            if (button2.Text .Equals("ȡ������"))
            {
                sendFlag = false;
                button2.Text = "�����ļ�";
                label1.Text = "û�д����ļ�";
                label3.Text = "";

                textBox1.Text += "----ȡ�����͡�" + fileNameSend+"��\r\n";
            }
            if (sendOrClear.Equals("ȡ������"))
                {
                    acceptFlag = false;
                    sendOrClear = "�����ļ�";
                    txtShowMessage += "----ȡ�����ա�" + fileNameSend + "��\r\n";
                    return false;
                }


            }
        #endregion

        #region �����ļ�

        // <summary>
        // �����ļ�
        // </summary>
        //<param name="listView1">listeView�е��û�</param>
        // <param name="showMessage">��ʾ����Ϣ</param>
        // <returns>trueΪ���ܾ�,falseΪ�ܾ�����</returns>
        private string  fileBeginSend(ListView listView1,string showMessage)
        {
            int FlagFile = 0;
            string filenamesend = null;
            if (File.Exists(filePathSend) == false)
            {
                MessageBox.Show("û��ѡ���ļ�");
                return;
            }
            ////////////////////////////////////

            ListView.CheckedListViewItemCollection checkedItemsFile = listView1.CheckedItems;

            foreach (ListViewItem item in checkedItemsFile)
            {
                if (item.SubItems[0].Text.ToString().Equals(Dns.GetHostName()+"-����"))
                {
                    MessageBox.Show("������Լ������ļ�");
                    return;
                }
                fileTcpClientSend = new TcpClient();
                fileTcpClientSend.Connect(IPAddress.Parse(item.SubItems[1].Text.ToString()), 10001);
                if (fileTcpClientSend.Connected == true)
                {
                    NetworkStream nsSend = fileTcpClientSend.GetStream();
                    byte[] writeSend = new byte[2048];
                    byte[] readSend = new byte[1];
                    brSend = null;
                    brSend = new BinaryReader(File.Open(filePathSend, FileMode.Open));
                    writeSend = Encoding.GetEncoding("gb2312").GetBytes("1" + brSend.BaseStream.Length.ToString() + ":" + fileNameSend+":");
                    nsSend.Write(writeSend, 0, writeSend.Length);
                    int nreadSend = nsSend.Read(readSend, 0, readSend.Length);
                    if (fileNameSend.Length > 11)
                        filenamesend = fileNameSend.Substring(0, 8) + "...";
                    else
                        filenamesend = fileNameSend;

                    if (readSend[0].Equals(53))//�Է�ȡ������
                    {
                        MessageBox.Show("�������ļ�");
                        showMessage += item.SubItems[0].Text.ToString() + "�ܾ����ļ�\r\n";
                        label3.Text = item.SubItems[0].Text.ToString() + "�ܾ����ļ�";
                        Thread.CurrentThread.Abort();
                        MessageBox.Show("�������ļ�");
                        return;
                    }
                    else if (readSend[0].Equals(51))
                    {
                        int nread = 0, sendSize = 0;
                        byte[] readData = new byte[2048];

                        nread = brSend.Read(readData, 0, 2048);
                        textBox1.Text += ">>>>���ڷ���:��" + fileNameSend + "��.....\r\n";

                        while (nread != 0)
                        {

                            sendSize += nread;


                            nsSend.Write(readData, 0, 2048);
                            readData = new byte[2048];
                            nread = brSend.Read(readData, 0, 2048);
                            sendFile = (float)brSend.BaseStream.Length / (1024 * 1024);
                            sendFloat = (float)sendSize / (1024 * 1024);
                            int nowtime = DateTime.Now.Second;
                            if (nowtime > timeFlagSend && nowtime - timeFlagSend >= 1)
                            {

                                timeFlagSend = nowtime;


                                string a = (decimal.Round(decimal.Parse(sendFile.ToString()), 2)).ToString();

                                string b = (decimal.Round(decimal.Parse(sendFloat.ToString()), 2)).ToString();
                                string ab = (decimal.Round(decimal.Parse((sendFloat / sendFile * 100).ToString()), 2)).ToString();

                                string speedSend = (decimal.Round(decimal.Parse(((sendFloat - sendfloat) / 1).ToString()), 2)).ToString();

                                sendfloat = sendFloat;

                                label1.Text = "���ڷ��͡�" + filenamesend + "��(" + a + "M)" + b + "M--" + ab + "%--" + speedSend + "M/S";
                            }
                            else if (nowtime < timeFlagSend && nowtime + 60 - timeFlagSend >= 1)
                            {
                                timeFlagSend = nowtime;


                                string a = (decimal.Round(decimal.Parse(sendFile.ToString()), 2)).ToString();

                                string b = (decimal.Round(decimal.Parse(sendFloat.ToString()), 2)).ToString();
                                string ab = (decimal.Round(decimal.Parse((sendFloat / sendFile * 100).ToString()), 2)).ToString();

                                string speedSend = (decimal.Round(decimal.Parse(((sendFloat - sendfloat) / 1).ToString()), 2)).ToString();

                                sendfloat = sendFloat;

                                 label1.Text = "���ڷ��͡�" + filenamesend + "��(" + a + "M)" + b + "M--" + ab + "%--" + speedSend + "M/S";

                            }
                        }

                    }
                    byte[] readEnd = new byte[4];

                    nsSend.Read(readEnd, 0, 1);

                    if (readEnd[0].Equals(52))
                    {
                        MessageBox.Show(dd.ToString());
                        textBox1.Text += ">>>>��" + fileNameSend + "�����ͳɹ�\r\n";
                        label3.Text = "��" + filenamesend + "�����ͳɹ�";
                        label1.Text = "û�д����ļ�";
                        timeFlagSend = 0;
                        sendfloat = 0;
                        button2.Text = "�����ļ�";
                        sendFlag = true;
                        writeSend = new byte[1];
                        writeSend = Encoding.GetEncoding("gb2312").GetBytes("2");
                        nsSend.Write(writeSend, 0, 1);

                    }


                    brSend.Close();
                    nsSend.Close();
                    fileTcpClientSend.Close();



                }
                label1.Text =  fileNameSend+"���ͳɹ�";
                Thread.CurrentThread.Abort();
                FlagFile = 1;
            }
            if (FlagFile == 0)
                MessageBox.Show("û��ѡ���û�");

        }
        #endregion

        #region ������������
        // ������������
        private void listenFileConnect()
        {
            while (true)
            {
                fileClientAccept = fileListen.AcceptTcpClient();
                timerA = new System.Timers.Timer(1000);
                fileThreadAccept = new Thread(new ThreadStart(acceptData));
                fileThreadAccept.Start();
            }


        }
        #endregion

         #region �����ļ�
         //�����ļ�
         private void acceptData(string sendOrClear,string acceptStatic,string txtShowMessage)
         {

             bool begin = true;
             bool end = false;

             string filenameaccept="";
             bwAccept = new BinaryWriter(File.Open(Environment.SystemDirectory + "\\temp.tmp", FileMode.OpenOrCreate));
            bool newFileSecond =true;
             NetworkStream nsAccept = fileClientAccept.GetStream();
             if (nsAccept.CanRead)
             {
                 while (acceptFlag)
                 {
                     byte[] read = new byte[2048];
                     int nread = nsAccept.Read(read, 0, read.Length);
                     MessageBox.Show(read[0].ToString());
                     if (read[0].Equals(49) == true && begin)
                     {
                         begin = false;
                         string temp = Encoding.GetEncoding("gb2312").GetString(read, 1, nread);
                         string[] sizeAndname = temp.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                         fileNameAccept = sizeAndname[1];
                         size = Convert.ToInt64(sizeAndname[0]);
                         if (fileNameAccept.Length > 11)
                             filenameaccept = fileNameAccept.Substring(0, 8) + "...";
                         else
                             filenameaccept = fileNameAccept;

                         // size = Convert.ToInt64(sizeAndname[0]);
                         SaveFileDialog saveFile = new SaveFileDialog();
                         saveFile.OverwritePrompt = true;
                         saveFile.FileName = fileNameAccept;
                         if (saveFile.ShowDialog() == DialogResult.OK)
                         {
                             filePathAccept = saveFile.FileName;

                         }
                         else
                         {
                             byte[] cancelWrite = new byte[1];
                             cancelWrite = Encoding.GetEncoding("gb2312").GetBytes("5");
                             nsAccept.Write(cancelWrite, 0, cancelWrite.Length);
                             bwAccept.Close();
                             MessageBox.Show("��û���ո��ļ�");
                             return;

                         }
                         byte[] write = new byte[1];//׼�����ļ�
                         write = Encoding.GetEncoding("gb2312").GetBytes("3");
                         nsAccept.Write(write, 0, write.Length);
                         timerA.Start();
                     }

                     else if (read[0].Equals(50) && end)
                     {

                         end = false;
                         bwAccept.Close();

                         MessageBox.Show("����");
                        Thread.CurrentThread.Abort();

                         return;
                     }
                     else
                     {
                         if (newFileSecond)
                         {
                             sendOrClear = "ȡ������";
                             bwAccept.Close();
                             bwAccept = new BinaryWriter(File.Open(filePathAccept, FileMode.Create));
                             newFileSecond = false;

                             acceptStatic = "���ڽ��ա�" + filenameaccept + "��.....";
                             txtShowMessage += "<<<<���ڽ���:��" + fileNameAccept + "��.....\r\n";
                         }
                         recSize += nread;
                         bwAccept.Write(read, 0, nread);
                         recFloat =(float)recSize/(1024 * 1024);

                         recFile=(float )size/(1024*1024);
                         int nowtime = DateTime.Now.Second;
                         if (nowtime > timeFlagAccept && nowtime - timeFlagAccept >= 1)
                         {
                             timeFlagAccept = nowtime;
                             string a = (decimal.Round(decimal.Parse(recFile.ToString()), 2)).ToString();
                             string b = (decimal.Round(decimal.Parse(recFloat.ToString()), 2)).ToString();
                             string ab = (decimal.Round(decimal.Parse((recFloat / recFile * 100).ToString()), 2)).ToString();
                             string acceptspeed = (decimal.Round(decimal.Parse((recFloat - acceptfloat / 1).ToString()), 2)).ToString();
                             acceptStatic = "���ڽ��ա�" + filenameaccept + "��(" + a + "M)" + b + "M--" + ab + "%--" + acceptspeed + "M/S";
                             acceptfloat = recFloat;

                         }
                         else 
                         {
                             if (nowtime < timeFlagAccept && nowtime+60 - timeFlagAccept >= 1)
                             {
                                 timeFlagAccept = nowtime;
                                 string a = (decimal.Round(decimal.Parse(recFile.ToString()), 2)).ToString();
                                 string b = (decimal.Round(decimal.Parse(recFloat.ToString()), 2)).ToString();
                                 string ab = (decimal.Round(decimal.Parse((recFloat / recFile * 100).ToString()), 2)).ToString();
                                 string acceptspeed = (decimal.Round(decimal.Parse((recFloat - acceptfloat / 1).ToString()), 2)).ToString();
                                 acceptStatic = "���ڽ��ա�" + filenameaccept + "��(" + a + "M)" + b + "M--" + ab + "%--" + acceptspeed + "M/S";
                                 acceptfloat = recFloat;

                             }

                         }

                         if (recSize>= size)
                         {

                             acceptStatic = "û�д����ļ�";
                             MessageBox.Show(recSize.ToString());
                             txtShowMessage += "<<<<��" + fileNameAccept + "�����ճɹ�\r\n";
                             label3.Text = "��" + filenameaccept + "�����ճɹ�";
                             acceptfloat = 0;

                            timeFlagAccept = 0;
                             byte[] endwrite = new byte[1];
                             endwrite = Encoding.GetEncoding("gb2312").GetBytes("4");//WillEnd 50
                             nsAccept.Write(endwrite, 0, endwrite.Length);
                             MessageBox.Show(recSize.ToString());
                             recSize = 0;


                             end = true;

                         }
                     }


                 }


             }
             bwAccept.Close();
             nsAccept.Close();



         }
        #endregion 
        */
    }   
       
}


