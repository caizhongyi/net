using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
namespace FlyMessage
{
    class ChatSession
    {
        private static string fileName = "";
        private Socket chat;
        private string pack;
        private static int i=0;
        /// <summary>
        /// 初始化构造方法
        /// </summary>
        /// <param name="chat">套接字</param>
        /// <param name="form">主窗体</param>
        public ChatSession(Socket chat)
        {
            this.chat = chat;
            
        }

        /// <summary>
        /// 对信息进行处理
        /// </summary>
        public void StartChat()
        {

            //获取远程主机的IP地址和端口号
            IPEndPoint ep = (IPEndPoint)chat.RemoteEndPoint;


            //设置
            byte[] buff = new byte[1024];
            int len;
            while ((len = chat.Receive(buff)) != 0)
                {              
                    string msg = Encoding.Default.GetString(buff, 0, len);
                    pack = msg.Substring(0, 1);
                    string cmd = msg.Substring(1, 3);
                    if (cmd == "DAT")
                    {
                        //if (DialogResult.Yes == MessageBox.Show(ep.Address.ToString() + "向你发送文件？", "发送文件", MessageBoxButtons.YesNoCancel))
                        //{
                            SaveFileDialog sfdlg = new SaveFileDialog();
                            sfdlg.Title = "文件保存在";
                            sfdlg.Filter = "文件(*.*)|*.*";
                            sfdlg.InitialDirectory = @"D:\ScreenPricture";
                            sfdlg.FileName = "save"+i.ToString();
                            i++;
                            if ((sfdlg.ShowDialog()==DialogResult.OK))
                            {
                             
                                fileName = sfdlg.FileName;
                                NewMethod(ep, buff, ref len, ref msg);
                            }
                        //}
                    }
                    //else if (cmd == "MSG")
                    //{
                    //    msg = Encoding.Default.GetString(buff);
                    //    FrmMessage message = new FrmMessage(msg, pack);
                    //    message.ShowDialog();
                    //}
 

                }
           
          
            chat.Close();

        }


        private void NewMethod(IPEndPoint ep, byte[] buff, ref int len, ref string msg)
        {
            string extName = msg.Substring(msg.LastIndexOf('.'));
            FileStream writer = new FileStream(fileName + extName, FileMode.OpenOrCreate, FileAccess.Write);
            while ((len = chat.Receive(buff)) != 0)
            {
                msg = Encoding.Default.GetString(buff, 0, len);
                if (msg == "END")
                    break;
                writer.Write(buff, 0, len);
            }
            writer.Write(buff, 0, len);
            writer.Close();
        }
       
    }
}
