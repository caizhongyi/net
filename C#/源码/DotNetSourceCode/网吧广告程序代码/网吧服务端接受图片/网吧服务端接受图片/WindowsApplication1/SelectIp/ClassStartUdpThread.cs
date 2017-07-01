using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using WindowsApplication1.SendInfo;

namespace 获取局域网IP
{

    class ClassStartUdpThread
    {     
        private ListView lvwDisplayUser;

        public ClassStartUdpThread(ListView lvwDisplayUser)
        {
            this.lvwDisplayUser = lvwDisplayUser;
        }
        public void StartUdpThread()
        {

            UdpClient server = new UdpClient(7999);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] buff = server.Receive(ref ep);
                string user = Encoding.Default.GetString(buff);
                string cmd = user.Substring(0, 6);
                string user1 = user.Substring(6);
                if (cmd == ":USER:")
                {
                    try
                    {
                        string[] s = user1.Split(':');
                        ListViewItem lviUserName = new ListViewItem();
                        ListViewItem.ListViewSubItem lvsiComputerName = new ListViewItem.ListViewSubItem();
                        ListViewItem.ListViewSubItem lvsiIP = new ListViewItem.ListViewSubItem();
                        lvsiComputerName.Text = s[0];
                        lvsiIP.Text = s[1];
                        lviUserName.SubItems.Add(lvsiComputerName);
                        lviUserName.SubItems.Add(lvsiIP);
                        bool flag = true;
                        for (int i = 0; i < this.lvwDisplayUser.Items.Count; i++)
                        {
                            if (lvsiIP.Text == this.lvwDisplayUser.Items[i].SubItems[2].Text)
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            this.lvwDisplayUser.Items.Add(lviUserName);
                        }
                    }
                    catch
                    {
                        return;
                    }

                }
            }

        }
    }
}
