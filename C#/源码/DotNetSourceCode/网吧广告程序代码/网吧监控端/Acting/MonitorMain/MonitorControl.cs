using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SmartKernel.Net;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.IO;
using System.Runtime.InteropServices;

namespace MonitorMain
{
    public partial class MonitorControl : UserControl
    {
        #region Win32API方法包装
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey
        (
            uint uCode,
            uint uMapType
        );
        #endregion
        #region 字段
        private Monitor monitor;
        private Bitmap bitmap=null;
        private bool control = false; 
        #endregion

        public MonitorControl()
        {
            InitializeComponent();
        }
        public void Initialize(string remoteComputer)
        {
            try
            {
                ChannelServices.RegisterChannel(new TcpChannel(),false);
                monitor = (Monitor)Activator.GetObject(typeof(Monitor), "tcp://" + remoteComputer + ":8888/MonitorServerUrl");

                Size desketopwindowsize = monitor.GetDesktopBitmapSize();
                bitmap = new Bitmap(desketopwindowsize.Width,desketopwindowsize.Height);
                this.AutoScrollMinSize = desketopwindowsize;
                UpdateDisplay();
            }
            catch { }
        }

        public void UpdateDisplay()
        {
            System.Threading.Monitor.Enter(this);
            try
            {
                byte[] BitmapByte = monitor.GetDesktopBitmapBytes();
                if (BitmapByte.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(BitmapByte,false);
                    bitmap = (Bitmap)Image.FromStream(ms);
                    Point pt = new Point(AutoScrollPosition.X,AutoScrollPosition.Y);
                    CreateGraphics().DrawImage(bitmap,pt);
                }
            }
            catch { }
            System.Threading.Monitor.Exit(this);
            System.Threading.Thread.Sleep(350);
        }

        private void MonitorControl_Paint(object sender, PaintEventArgs e)
        {
            System.Threading.Monitor.Enter(this);
            if (this.bitmap != null)
            {
                Point pt = new Point(AutoScrollPosition.X,AutoScrollPosition.Y);
                e.Graphics.DrawImage(bitmap,pt);
            }
            System.Threading.Monitor.Exit(this);
        }

        private void MonitorControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (control)
            {
                //调用api
                monitor.MoveMouse(e.X,e.Y);
            }
        }

        private void MonitorControl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                monitor.PressOrReleaseMouseButton(false, e.Button == MouseButtons.Left, e.X, e.Y);
            }
            catch {
                return;
            }
        }

        private void MonitorControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            monitor.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), true, false);
        }

        private void MonitorControl_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            monitor.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), false, false);
        }

        public void SetControl(bool control)
        {
            this.control = control;
        }
    }
}
