using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace WindowsApplication1.SelectIp
{
    public partial class GetIPForm : Form
    {
        SelectIPInfo sipi = new SelectIPInfo();
        public GetIPForm()
        {
            InitializeComponent();
        }

        private void GetIPForm_Load(object sender, EventArgs e)
        {
            int sum = this.lvwDisplayUser.Items.Count;
            
            for (int i = 0; i < sum; i++)
            {
                string ip = this.lvwDisplayUser.Items[i].SubItems[2].Text;
                if (ip.ToString() == Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString())
                {
                    continue;
                }
                else
                {
                    sipi.Host = ip.ToString();
                    return;
                }
            }
        }
    }
}