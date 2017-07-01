namespace MonitorMain
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbMonitor = new System.Windows.Forms.CheckBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.lbIp = new System.Windows.Forms.Label();
            this.cbControl = new System.Windows.Forms.CheckBox();
            this.monitorControl1 = new MonitorMain.MonitorControl();
            this.SuspendLayout();
            // 
            // cbMonitor
            // 
            this.cbMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMonitor.AutoSize = true;
            this.cbMonitor.Location = new System.Drawing.Point(173, 610);
            this.cbMonitor.Name = "cbMonitor";
            this.cbMonitor.Size = new System.Drawing.Size(66, 16);
            this.cbMonitor.TabIndex = 1;
            this.cbMonitor.Text = "Monitor";
            this.cbMonitor.UseVisualStyleBackColor = true;
            this.cbMonitor.CheckedChanged += new System.EventHandler(this.cbMonitor_CheckedChanged);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtIpAddress.Location = new System.Drawing.Point(56, 608);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(100, 21);
            this.txtIpAddress.TabIndex = 2;
            this.txtIpAddress.Text = "192.168.1.10";
            // 
            // lbIp
            // 
            this.lbIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbIp.AutoSize = true;
            this.lbIp.Location = new System.Drawing.Point(21, 614);
            this.lbIp.Name = "lbIp";
            this.lbIp.Size = new System.Drawing.Size(29, 12);
            this.lbIp.TabIndex = 3;
            this.lbIp.Text = "IP：";
            // 
            // cbControl
            // 
            this.cbControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbControl.AutoSize = true;
            this.cbControl.Location = new System.Drawing.Point(267, 610);
            this.cbControl.Name = "cbControl";
            this.cbControl.Size = new System.Drawing.Size(66, 16);
            this.cbControl.TabIndex = 4;
            this.cbControl.Text = "Control";
            this.cbControl.UseVisualStyleBackColor = true;
            this.cbControl.CheckedChanged += new System.EventHandler(this.cbControl_CheckedChanged);
            // 
            // monitorControl1
            // 
            this.monitorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.monitorControl1.BackColor = System.Drawing.Color.White;
            this.monitorControl1.Location = new System.Drawing.Point(0, 0);
            this.monitorControl1.Name = "monitorControl1";
            this.monitorControl1.Size = new System.Drawing.Size(834, 592);
            this.monitorControl1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 638);
            this.Controls.Add(this.cbControl);
            this.Controls.Add(this.lbIp);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.cbMonitor);
            this.Controls.Add(this.monitorControl1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MonitorControl monitorControl1;
        private System.Windows.Forms.CheckBox cbMonitor;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label lbIp;
        private System.Windows.Forms.CheckBox cbControl;
    }
}

