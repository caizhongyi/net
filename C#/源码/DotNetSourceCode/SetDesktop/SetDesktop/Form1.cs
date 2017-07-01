using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace SetDesktop
{
    public partial class Form1 : Form
    {
        public string strPath = "";

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
            );

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string strpath)
        {
            strPath=strpath;
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
         /// <summary>
        /// 设置桌面背景
        /// </summary>
        /// <param name="Path"></param>
        public void Desktop(string Path)
        {
            if (File.Exists(Path))
            SystemParametersInfo(20, 0, Path,   0x2); // 0x1 | 0x2 
        }
        public void Font()
        {
        //    System.IntPtr p=GetDC(0);
            //System.Drawing.Graphics g=System.Drawing.Graphics.FromHdc(p);
            
        }
        public void setDesktop()
        {
            //取得每天需要显示的图片
            string strPathJpg="";
            string strPathBmp="";
            strPathJpg=Application.StartupPath+"\\"+DateTime.Today.Day.ToString()+".jpg ";
            strPathBmp=Application.StartupPath+"\\"+DateTime.Today.Day.ToString()+".bmp";
            if (File.Exists(strPathBmp))
            {
                this.Desktop(strPathBmp);
            }
            else
            {
                if (File.Exists(strPathJpg))
                {
                    //
                                    }
                else
                {
                    MessageBox.Show(strPathJpg);
                    return;
                }
            }

            //MessageBox.Show(strPathJpg);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Desktop(@"C:\Documents and Settings\Administrator\桌面\SetDesktop\SetDesktop\bin\Debug\四叶草.bmp");
        }
    }
}