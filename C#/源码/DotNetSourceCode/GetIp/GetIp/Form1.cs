using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using System.Threading;
using System.DirectoryServices;

namespace GetIp
{
    public partial class Form1 : Form
    {      
        public Form1() 
        {
            InitializeComponent();   
        }   
        private void button1_Click(object sender, EventArgs e)   
        {
            listBox1.Items.Clear();
            //获取局域网内IP
            DirectoryEntry entryPC = new DirectoryEntry("WinNT:");
            ArrayList arr = new ArrayList();
            foreach (DirectoryEntry child in entryPC.Children)
            {
                foreach (DirectoryEntry pc in child.Children)
                {
                    try
                    {
                        IPHostEntry hostent = Dns.GetHostByName(pc.Name); // 主机信息
                        Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
                        Array addrs = hostent.AddressList;            // IP地址数组
                        IEnumerator it = addrs.GetEnumerator();       // 迭代器
                        while (it.MoveNext())
                        {
                            // 循环到下一个IP 地址
                            IPAddress ip = (IPAddress)it.Current;
                            // 显示 IP地址
                            this.listBox1.Items.Add(ip.ToString());
                        }
                    }
                    catch
                    {
                    }
                }

                //获取本机IP
                IPHostEntry iph = new IPHostEntry();
                iph = Dns.GetHostByName(Dns.GetHostName());
                string ddd = iph.AddressList[0].ToString();
                label1.Text = ddd;
            } 
        }    
    }
}