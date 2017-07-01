using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FileDel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void DelFile(string dirpath)
        {
            DirectoryInfo di = new DirectoryInfo(dirpath);
            int n = di.GetFiles("*").Length;
            if (n >= 1)
            {
                foreach (FileInfo fi in di.GetFiles("*"))
                {
                    File.Delete(fi.FullName);
                }
            }
            Directory.Delete(dirpath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DelFile(Directory.GetCurrentDirectory()+"\\"+"aaaa");
        }
        public void RechristenFileName()
        {
            try
            {
                string oldfilepath = Directory.GetCurrentDirectory() + "\\" + "aaaa";
                string newfilepath = Directory.GetCurrentDirectory() + "\\" + "bbb";
                if (Directory.Exists(newfilepath))
                {
                    MessageBox.Show("你重命名的文件夹已存在！");
                    return;
                }
                Directory.Move(oldfilepath, newfilepath);
            }
            catch
            {
                MessageBox.Show("你要重命名的文件夹不存在！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RechristenFileName();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           CreateFold(Directory.GetCurrentDirectory()+"\\"+"aaaa");
        }
        public void CreateFold(string directorypath)
        {
            if (Directory.Exists(directorypath))
            {
                MessageBox.Show("你要创建的文件夹已存在！");
                return;
            }
            else
            {
                Directory.CreateDirectory(directorypath);
            } 

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists("AdvInfo.xml"))
            {
                MessageBox.Show("存在");
            }
            else
                MessageBox.Show("不存在");
        }
    }
}