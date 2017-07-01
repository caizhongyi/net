using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace DownLoadFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownLoad("http://www.wb66.cn/MyPicture/", Directory.GetCurrentDirectory() + "\\" + "AdPicture" + "\\");
        }
        public void DownLoad(string URL, string Dir)
        {
            WebClient wc = new WebClient();
            //被下载的文件名
            string downloadfilename = URL.Substring(URL.LastIndexOf("/") + 1);
            //另存为的绝对路径＋文件名
            string keepfilenamepath = Dir + downloadfilename;

            WebRequest wr = WebRequest.Create(URL);


            wc.DownloadFile(URL,downloadfilename);

            FileStream fs = new FileStream(downloadfilename,FileMode.Open,FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);
            byte[] mbyte = br.ReadBytes((int)fs.Length);

            FileStream fstr = new FileStream(keepfilenamepath, FileMode.OpenOrCreate, FileAccess.Write);

            fstr.Write(mbyte, 0, (int)fs.Length);
            fstr.Close();

        }
    }
}