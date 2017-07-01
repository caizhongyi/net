using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FindFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "C:\\KupPicture";
            this.listBox1.Items.Clear();
            string[] filesInfo ={ ".gif", ".jpg", ".bmp" };
            string[] Myfiles = System.IO.Directory.GetFiles(path);
            //for (int j = 0; j < Myfiles.Length; j++)
            //{   
                //for (int i = 0; i < filesInfo.Length; i++)
                //{
                    this.listBox1.Items.AddRange(Myfiles);
                //}
            //}
          
        }
    }
}