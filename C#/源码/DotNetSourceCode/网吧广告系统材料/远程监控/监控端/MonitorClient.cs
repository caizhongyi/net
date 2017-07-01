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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartKernel.Net
{
    public partial class MonitorClient : Form
    {
        #region 字段
        private bool Initialized = false;
        private bool InProgress = false;
        private System.Threading.Thread MonitorThread = null;
        #endregion

        #region 构造函数
        public MonitorClient()
        {
            InitializeComponent();
            Application.Idle += new EventHandler(Application_Idle);
        }
        #endregion

        #region 应用程序空闲状态的处理过程
        void Application_Idle(object sender, EventArgs e)
        {
            this.checkBox1.Enabled = this.textBox1.Text.Trim().Length > 0;
            this.checkBox2.Enabled = this.checkBox1.Enabled;
        }
        #endregion

        #region 监控开始
        private void Monitor()
        {
            while (true)
            {
                this.monitorUserControl1.UpdateDisplay();
                System.Threading.Thread.Sleep(200);
            }
        }
        #endregion

        #region 监视
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                if (!Initialized)
                {
                    Cursor = Cursors.WaitCursor;

                    this.monitorUserControl1.Initialize(this.textBox1.Text.Trim());
                    Initialized = true;

                    Cursor = Cursors.Arrow;
                }

                InProgress = true;

                MonitorThread = new System.Threading.Thread(new System.Threading.ThreadStart(Monitor));
                MonitorThread.Start();
            }
            else
            {
                InProgress = false;
                MonitorThread.Abort();
            }
        }
        #endregion

        #region 控制
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.monitorUserControl1.SetControl(((CheckBox)sender).Checked);
        }
        #endregion
    }
}