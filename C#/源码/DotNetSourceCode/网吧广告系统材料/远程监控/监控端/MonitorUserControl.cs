/************************************************************/
//【项目】：远程监控
//【创建】：2005年10月
//【作者】：SmartKernel
//【邮箱】：smartkernel@126.com
//【QQ  】：120018689
//【MSN 】：smartkernel@hotmail.com
//【网站】：www.SmartKernel.com
/************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace SmartKernel.Net
{
    public partial class MonitorUserControl : UserControl
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
        private Monitor robj;
        private Bitmap m_Bitmap = null;
        private bool Control = false;
        #endregion

        #region 构造函数
        public MonitorUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 初始化
        public void Initialize(string remoteMachine)
        {
            try
            {
                ChannelServices.RegisterChannel(new TcpChannel(),false);
                robj = (Monitor)Activator.GetObject(typeof(Monitor), "tcp://" + remoteMachine + ":8888/MonitorServerUrl");

                Size desktopWindowSize = robj.GetDesktopBitmapSize();
                m_Bitmap = new Bitmap(desktopWindowSize.Width, desktopWindowSize.Height);
                this.AutoScrollMinSize = desktopWindowSize;
                UpdateDisplay();
            }
            catch
            {

            }
        }
        #endregion

        #region 绘制
        private void MonitorUserControl_Paint(object sender, PaintEventArgs e)
        {
            System.Threading.Monitor.Enter(this);
            if (m_Bitmap != null)
            {
                Point P = new Point(AutoScrollPosition.X, AutoScrollPosition.Y);
                e.Graphics.DrawImage(m_Bitmap, P);
            }
            System.Threading.Monitor.Exit(this);
        }
        #endregion

        #region 鼠标移动
        private void MonitorUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control)
            {
                robj.MoveMouse(e.X, e.Y);
            }
        }
        #endregion

        #region 鼠标按键释放
        private void MonitorUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            robj.PressOrReleaseMouseButton(false, e.Button == MouseButtons.Left, e.X, e.Y);
        }
        #endregion

        #region 鼠标按键按下
        private void MonitorUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            robj.PressOrReleaseMouseButton(true, e.Button == MouseButtons.Left, e.X, e.Y);
        }
        #endregion

        #region 按键按下
        private void MonitorUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            robj.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), true, false);
        }
        #endregion

        #region 按键释放
        private void MonitorUserControl_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            robj.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), false, false);
        }
        #endregion

        #region 其他
        public void SetControl(bool Control)
        {
            this.Control = Control;
        }

        public void UpdateDisplay()
        {
            System.Threading.Monitor.Enter(this);

            try
            {
                byte[] BitmapBytes = robj.GetDesktopBitmapBytes();

                if (BitmapBytes.Length > 0)
                {
                    MemoryStream MS = new MemoryStream(BitmapBytes, false);
                    m_Bitmap = (Bitmap)Image.FromStream(MS);
                    Point P = new Point(AutoScrollPosition.X, AutoScrollPosition.Y);
                    CreateGraphics().DrawImage(m_Bitmap, P);
                }
            }
            catch
            {

            }
            System.Threading.Monitor.Exit(this);
            System.Threading.Thread.Sleep(350);
        }
        #endregion
    }
}
