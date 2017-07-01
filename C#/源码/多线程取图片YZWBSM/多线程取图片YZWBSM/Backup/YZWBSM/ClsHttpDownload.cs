using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;

namespace YZWBSM
{
    class ClsHttpDownload
    {
       /* public bool[] threadw; //每个进程结束标志
        public string[] filenamew;//每个进程接收文件的文件名
        public int[] filestartw;//每个进程接收文件的起始位置
        public int[] filesizew;//每个进程接收文件的大小
        public string strurl;//接受文件的URL
        public string thnum = 5;//接受文件的接收线程数
        public string thfilename = 5;//接受文件的文件名
        public bool hb;//文件合并标志
        public int thread;//进程数
        private void sHttpDownloadFile(string strurl)
        {
            //DateTime dt = DateTime.Now;
            //textBox1.Text = dt.ToString();
            //strurl = textBox2.Text.Trim().ToString();
            HttpWebRequest request;
            long filesize = 0;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(strurl);
                filesize = request.GetResponse().ContentLength;//取得目标文件的长度
                request.Abort();
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
            // 接收线程数
            thread = Convert.ToInt32(thnum, 10);
            //初始化数组
            threadw = new bool[thread];
            filenamew = new string[thread];
            filestartw = new int[thread];
            filesizew = new int[thread];

            //计算每个进程接收文件的大小
            int filethread = (int)filesize / thread;
            int filethreade = filethread + (int)filesize % thread;
            //为数组赋值
            for (int i = 0; i < thread; i++)
            {
                threadw[i] = false;
                filenamew[i] = i.ToString() + ".dat";
                if (i < thread - 1)
                {
                    filestartw[i] = filethread * i;
                    filesizew[i] = filethread - 1;

                }
                else
                {
                    filestartw[i] = filethread * i;
                    filesizew[i] = filethreade - 1;
                }
            }
            //定义线程数组
            Thread[] threadk = new Thread[thread];
            HttpFile[] httpfile = new HttpFile[thread];
            for (int j = 0; j < thread; j++)
            {
                httpfile[j] = new HttpFile(this, j);
                threadk[j] = new Thread(new ThreadStart(httpfile[j].receive));
                threadk[j].Start();
            }
            //合并各线程接收的文件
            Thread hbth = new Thread(new ThreadStart(hbfile));
            hbth.Start();

        }
        //合并文件线程
        public void hbfile()
        {
            while (true)//等待
            {
                hb = true;
                for (int i = 0; i < thread; i++)
                {
                    if (threadw[i] == false)
                    {
                        hb = false;
                        Thread.Sleep(100);
                        break;
                    }
                }
                if (hb == true)
                {
                    break;
                }
            }
            FileStream fs;//开始合并
            FileStream fstemp;
            int readfile;
            byte[] bytes = new byte[512];
            fs = new FileStream(thfilename, System.IO.FileMode.Create);
            for (int k = 0; k < thread; k++)
            {
                fstemp = new FileStream(filenamew[k], System.IO.FileMode.Open);
                while (true)
                {
                    readfile = fstemp.Read(bytes, 0, 512);
                    if (readfile > 0)
                    {
                        fs.Write(bytes, 0, readfile);
                    }
                    else
                    {
                        break;
                    }
                }
                fstemp.Close();
            }
            fs.Close();
           // DateTime dt = DateTime.Now;
            //textBox1.Text =dt.ToString();
           // MessageBox.Show("接收完毕!!!");
        }
    }
    //接收线程类	
    public class HttpFile
    {
       // public Form1 formm;
        /*     public int threadh;
             public string filename;
             public string strUrl;
             public FileStream fs;
             public HttpWebRequest request;
             public System.IO.Stream ns;
             public byte[] nbytes;
             public int nreadsize;
             public HttpFile(Form1 form, int thread)//构造方法
             {
                 formm = form;
                 threadh = thread;
             }
             ~HttpFile()//析构方法
             {
                 formm.Dispose();
             }
             public void receive()
             {
                 filename = formm.filenamew[threadh];
                 strUrl = formm.strurl;
                 ns = null;
                 nbytes = new byte[512];
                 nreadsize = 0;
                 //		formm.listBox1.Items.Add ("线程"+threadh.ToString()+"开始接收");
                 fs = new FileStream(filename, System.IO.FileMode.Create);
                 try
                 {
                     request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                     request.AddRange(formm.filestartw[threadh], formm.filestartw[threadh] + formm.filesizew[threadh]);
                     ns = request.GetResponse().GetResponseStream();
                     nreadsize = ns.Read(nbytes, 0, 512);
                     while (nreadsize > 0)
                     {
                         fs.Write(nbytes, 0, nreadsize);
                         nreadsize = ns.Read(nbytes, 0, 512);
                         //				formm.listBox1.Items.Add ("线程"+threadh.ToString()+"正在接收");
                     }
                     fs.Close();
                     ns.Close();
                 }
                 catch (Exception er)
                 {
                     MessageBox.Show(er.Message);
                     fs.Close();
                 }
                 //		formm.listBox1.Items.Add ("进程"+threadh.ToString()+"接收完毕!");
                 formm.threadw[threadh] = true;
             }
             */
    }
}
