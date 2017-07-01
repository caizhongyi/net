/************************************************************/
//����Ŀ����Զ�̼��
//����������2005��10��
//�����ߡ���SmartKernel
//�����䡿��smartkernel@126.com
//��QQ  ����120018689
//��MSN ����smartkernel@hotmail.com
//����վ����www.SmartKernel.com
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
        #region Win32API������װ
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey
        (
            uint uCode,
            uint uMapType
        );
        #endregion

        #region �ֶ�
        private Monitor robj;
        private Bitmap m_Bitmap = null;
        private bool Control = false;
        #endregion

        #region ���캯��
        public MonitorUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region ��ʼ��
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

        #region ����
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

        #region ����ƶ�
        private void MonitorUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control)
            {
                robj.MoveMouse(e.X, e.Y);
            }
        }
        #endregion

        #region ��갴���ͷ�
        private void MonitorUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            robj.PressOrReleaseMouseButton(false, e.Button == MouseButtons.Left, e.X, e.Y);
        }
        #endregion

        #region ��갴������
        private void MonitorUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            robj.PressOrReleaseMouseButton(true, e.Button == MouseButtons.Left, e.X, e.Y);
        }
        #endregion

        #region ��������
        private void MonitorUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            robj.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), true, false);
        }
        #endregion

        #region �����ͷ�
        private void MonitorUserControl_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            robj.SendKeystroke((byte)e.KeyCode, (byte)MapVirtualKey((uint)e.KeyCode, 0), false, false);
        }
        #endregion

        #region ����
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
