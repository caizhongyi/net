using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Files;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IFiles ifs = FilesFactory.GetDownLoadFiles();
            ifs.DownloadFiles("http://www.wb66.cn/Adpictures/11e2adc3860.jpg", Application.StartupPath);
        }
    }
}