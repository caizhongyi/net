using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
namespace FlyMessage
{
    class ClassStartUdpThread
    {
        private ListView lvwDisplayUser;
        private Label lblUserCount;
        public ClassStartUdpThread(ListView lvwDisplayUser,Label lblUserCount)
        {
            this.lvwDisplayUser = lvwDisplayUser;
            this.lblUserCount = lblUserCount;
        }
        public  void StartUdpThread()
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
                        ListViewItem.ListViewSubItem lvsiWorkGroup = new ListViewItem.ListViewSubItem();
                        lviUserName.Text = s[0];
                        lvsiComputerName.Text = s[1];
                        lvsiIP.Text = s[2];
                        lvsiWorkGroup.Text = s[3];
                        lviUserName.SubItems.Add(lvsiComputerName);
                        lviUserName.SubItems.Add(lvsiIP);
                        lviUserName.SubItems.Add(lvsiWorkGroup);

                        //if (this.lvwDisplayUser.FindItemWithText(lvsiIP.Text) == null)
                        //{
                        //    this.lvwDisplayUser.Items.Add(lviUserName);
                        //}
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
                        lblUserCount.Text = "在线人数：" + this.lvwDisplayUser.Items.Count;
                    }
                    catch
                    {
                        MessageBox.Show("有位朋友下线了");
                    }

                }
            }

        }
    }
}
