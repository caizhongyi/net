using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;

namespace czy.MyClass.Forms
{

    public class WebFromScreenPrint
    {
        /*
        private System.ComponentModel.Container components = null;
        private Icon mNetTrayIcon = new Icon("Tray.ico");
        private Bitmap MyImage = null;
        private NotifyIcon TrayIcon;
        private ContextMenu notifyiconMnu;

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr CreateDC(
        string lpszDriver, // 驱动名称
        string lpszDevice, // 设备名称
        string lpszOutput, // 无用，可以设定位"NULL"
        IntPtr lpInitData // 任意的打印机数据
        );

        public void capture(Page page, string saveUrl)
        {
            page.Visible = false;
            IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
            //创建显示器的DC
            Graphics g1 = Graphics.FromHdc(dc1);
            //由一个指定设备的句柄创建一个新的Graphics对象
            MyImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, g1);
            //根据屏幕大小创建一个与之相同大小的Bitmap对象
            Graphics g2 = Graphics.FromImage(MyImage);
            //获得屏幕的句柄
            IntPtr dc3 = g1.GetHdc();
            //获得位图的句柄
            IntPtr dc2 = g2.GetHdc();
            //把当前屏幕捕获到位图对象中
            BitBlt(dc2, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, dc3, 0, 0, 13369376);
            //把当前屏幕拷贝到位图中
            g1.ReleaseHdc(dc3);
            //释放屏幕句柄
            g2.ReleaseHdc(dc2);
            //释放位图句柄
            MyImage.Save(saveUrl, ImageFormat.Jpeg);
            MessageBox.Show("已经把当前屏幕保存到" + saveUrl + "文件中！");
            page.Visible = true;
        }
        public void ExitSelect(object sender, System.EventArgs e)
        {
            //隐藏托盘程序中的图标
            TrayIcon.Visible = false;
            //关闭系统
            this.Close();
        }
        //清除程序中使用过的资源
        public override void Dispose(Page page)
        {
            base.Dispose();
            if (components != null)
                components.Dispose();
        }
        private void InitializeComponent(Page page)
        {
            //设定托盘程序的各个属性
            TrayIcon = new NotifyIcon();
            TrayIcon.Icon = mNetTrayIcon;
            TrayIcon.Text = "用C#做Screen Capture程序";
            TrayIcon.Visible = true;
            //定义一个MenuItem数组，并把此数组同时赋值给ContextMenu对象
            MenuItem[] mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem();
            mnuItms[0].Text = "捕获当前屏幕！";
            mnuItms[0].Click += new System.EventHandler(capture);
            mnuItms[1] = new MenuItem("-");
            mnuItms[2] = new MenuItem();
            mnuItms[2].Text = "退出系统";
            mnuItms[2].Click += new System.EventHandler(ExitSelect);
            mnuItms[2].DefaultItem = true;
            notifyiconMnu = new ContextMenu(mnuItms);
            TrayIcon.ContextMenu = notifyiconMnu;
            //为托盘程序加入设定好的ContextMenu对象
            page.SuspendLayout();
            page.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            page.ClientSize = new System.Drawing.Size(320, 56);
            page.ControlBox = false;
            page.MaximizeBox = false;
            page.MinimizeBox = false;
            page.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            page.Name = "capture";
            page.ShowInTaskbar = false;
            page.Text = "用C#做Screen Capture程序！";
            page.ResumeLayout(false);
        }
        */
    }
}

