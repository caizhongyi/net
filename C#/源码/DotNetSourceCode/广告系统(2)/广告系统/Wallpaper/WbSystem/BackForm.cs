using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SetWallpaper;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace WbSystem
{
    public partial class BackForm : Form
    {
        public BackForm()
        {
            InitializeComponent();
        }
    
        private void BackForm_Load(object sender, EventArgs e)
        {
            //设置Ad图片位置
            this.Top = 0;
            this.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            //调用桌面背景
            Factory.GetNewPicture().UpdateWallpaper();

            ////隐藏进程
            //hideproess.hideproess hd = new hideproess.hideproess();
            //hd.HideProess(f1);
            
        }
    }
}