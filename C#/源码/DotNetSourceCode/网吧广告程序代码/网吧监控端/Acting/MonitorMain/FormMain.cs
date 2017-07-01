using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MonitorMain
{
    public partial class FormMain : Form
    {
        #region  字段
        private bool initialized = false;
        private bool inprogress = false;
        private System.Threading.Thread MonitorThread = null;
        #endregion
        public FormMain()
        {
            InitializeComponent();
        }
        #region 开始监控
        private void MonitorStart()
        {
            this.monitorControl1.UpdateDisplay();
            System.Threading.Thread.Sleep(200);
        }
        #endregion

        #region 监视
        private void cbMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (!initialized)
                {
                    Cursor = Cursors.WaitCursor;
                    this.monitorControl1.Initialize(this.txtIpAddress.Text.Trim());
                    initialized = true;
                    Cursor = Cursors.Arrow;
                }
                inprogress = true;
                MonitorThread = new System.Threading.Thread(new System.Threading.ThreadStart(MonitorStart));
                MonitorThread.Start();

            }
            else
            {
                inprogress = false;
                MonitorThread.Abort();
            }
        }
        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
        #region 控制方法
        private void ComputerControl(object sender)
        {
            this.monitorControl1.SetControl(((CheckBox)sender).Checked);
        }
        #endregion

        #region 应用程序空闲时的处理过程 
        void Application_Idle(object sender, EventArgs e)
        {
            this.cbMonitor.Enabled = this.txtIpAddress.Text.Trim().Length > 0;
            this.cbControl.Enabled = this.cbMonitor.Enabled;

        }
        #endregion

        private void cbControl_CheckedChanged(object sender, EventArgs e)
        {
            ComputerControl(sender);
        }
    }
}