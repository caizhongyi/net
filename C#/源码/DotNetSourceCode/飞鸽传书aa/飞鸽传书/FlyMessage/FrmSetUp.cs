using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FlyMessage
{   

    public partial class FrmSetUp : Form
    {
        public FrmSetUp()
        {
            InitializeComponent();
            try
            {
                FileStream read = new FileStream(@"UserInformation.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(read);
                string userInfo = sr.ReadLine();
                string[] info = userInfo.Split(':');
                this.txtUserName.Text = info[0];
                this.txtWorkGroup.Text = info[1];
                sr.Close();
                read.Close();
            }
            catch
            {
                FileStream write = new FileStream(@"UserInformation.txt", FileMode.Create, FileAccess.Write);
                string data = "金保涛";
                StreamWriter sw = new StreamWriter(write);
                sw.Write(data);
                sw.Close();
                write.Close();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "" || this.txtWorkGroup.Text == "")
            {
                MessageBox.Show("请正确输入用户名和工作组！");
            }
            else
            {
                FileStream write = new FileStream(@"UserInformation.txt", FileMode.Create, FileAccess.Write);
                string data =this.txtUserName.Text + ":" + this.txtWorkGroup.Text;
                StreamWriter sw = new StreamWriter(write);
                sw.Write(data);
                sw.Close();
                write.Close();
            }

        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            this.Update();
            this.Close();
        }

    }
}