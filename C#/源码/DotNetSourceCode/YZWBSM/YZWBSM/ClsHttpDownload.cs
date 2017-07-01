using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;

namespace YZWBSM
{
    class ClsHttpDownload
    {
       /* public bool[] threadw; //ÿ�����̽�����־
        public string[] filenamew;//ÿ�����̽����ļ����ļ���
        public int[] filestartw;//ÿ�����̽����ļ�����ʼλ��
        public int[] filesizew;//ÿ�����̽����ļ��Ĵ�С
        public string strurl;//�����ļ���URL
        public string thnum = 5;//�����ļ��Ľ����߳���
        public string thfilename = 5;//�����ļ����ļ���
        public bool hb;//�ļ��ϲ���־
        public int thread;//������
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
                filesize = request.GetResponse().ContentLength;//ȡ��Ŀ���ļ��ĳ���
                request.Abort();
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
            // �����߳���
            thread = Convert.ToInt32(thnum, 10);
            //��ʼ������
            threadw = new bool[thread];
            filenamew = new string[thread];
            filestartw = new int[thread];
            filesizew = new int[thread];

            //����ÿ�����̽����ļ��Ĵ�С
            int filethread = (int)filesize / thread;
            int filethreade = filethread + (int)filesize % thread;
            //Ϊ���鸳ֵ
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
            //�����߳�����
            Thread[] threadk = new Thread[thread];
            HttpFile[] httpfile = new HttpFile[thread];
            for (int j = 0; j < thread; j++)
            {
                httpfile[j] = new HttpFile(this, j);
                threadk[j] = new Thread(new ThreadStart(httpfile[j].receive));
                threadk[j].Start();
            }
            //�ϲ����߳̽��յ��ļ�
            Thread hbth = new Thread(new ThreadStart(hbfile));
            hbth.Start();

        }
        //�ϲ��ļ��߳�
        public void hbfile()
        {
            while (true)//�ȴ�
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
            FileStream fs;//��ʼ�ϲ�
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
           // MessageBox.Show("�������!!!");
        }
    }
    //�����߳���	
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
             public HttpFile(Form1 form, int thread)//���췽��
             {
                 formm = form;
                 threadh = thread;
             }
             ~HttpFile()//��������
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
                 //		formm.listBox1.Items.Add ("�߳�"+threadh.ToString()+"��ʼ����");
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
                         //				formm.listBox1.Items.Add ("�߳�"+threadh.ToString()+"���ڽ���");
                     }
                     fs.Close();
                     ns.Close();
                 }
                 catch (Exception er)
                 {
                     MessageBox.Show(er.Message);
                     fs.Close();
                 }
                 //		formm.listBox1.Items.Add ("����"+threadh.ToString()+"�������!");
                 formm.threadw[threadh] = true;
             }
             */
    }
}
