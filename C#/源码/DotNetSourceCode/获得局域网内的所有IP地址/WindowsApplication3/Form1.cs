using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using System.DirectoryServices;

namespace WindowsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetIPAddress();
        }
        public void GetIPAddress()
        {
            DirectoryEntry entryPC = new DirectoryEntry("WinNT:");
            ArrayList arr = new ArrayList();
            foreach (DirectoryEntry child in entryPC.Children)
            {
                foreach (DirectoryEntry pc in child.Children)
                {
                    if (String.Compare(pc.SchemaClassName, "computer", true) == 0)
                    {
                        try
                        {
                            IPHostEntry hostent = Dns.GetHostByName(pc.Name); // 主机信息
                            Array addrs = hostent.AddressList;            // IP地址数组
                            IEnumerator it = addrs.GetEnumerator();       // 迭代器
                            while (it.MoveNext())
                            {
                                // 循环到下一个IP 地址
                                IPAddress ip = (IPAddress)it.Current;      // 获得 IP 地址
                                if (ip.ToString() == Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString())
                                {
                                    continue;
                                }
                                else
                                {
                                    MessageBox.Show(ip.ToString());
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            return;
                        }
                    }
                }
            }
        }

    }
}