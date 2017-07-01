using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FileName=this.textBox1.Text;
            string ShortFileName=FileName.Substring(FileName.LastIndexOf("\\")+1);
            //打开子键节点
            RegistryKey MyReg=Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",true);
            if(MyReg==null)
            {//如果子键节点不存在，则创建之
                MyReg=Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            //在注册表中设置自启动程序
            MyReg.SetValue(ShortFileName,FileName);
        }
    }
}