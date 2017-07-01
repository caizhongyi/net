using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileAge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filepath = Directory.GetCurrentDirectory() + "\\" + "aaa";
            FileInfo fi = new FileInfo(filepath);
            //string createtime = fi.CreationTime.ToString();
            string modtime = fi.LastAccessTime.ToString();
            MessageBox.Show(modtime);
        }
    }
}