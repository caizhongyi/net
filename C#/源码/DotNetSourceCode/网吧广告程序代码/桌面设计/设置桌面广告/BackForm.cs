using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SetWallpaper;
using System.Diagnostics;

namespace WbSystem
{
    public partial class BackForm : Form
    {
        public static int Formwidth;
        public BackForm()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Formwidth = this.Width;
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

            //显示Ad广告窗体
            AdForm bf = new AdForm();
            bf.ShowDialog();
        }
    }
}