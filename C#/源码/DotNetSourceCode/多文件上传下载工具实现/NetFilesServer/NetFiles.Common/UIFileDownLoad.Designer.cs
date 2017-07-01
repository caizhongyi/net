namespace NetFiles.Common
{
    partial class UIFileDownLoad
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFileName = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmdComplete = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.AutoSize = true;
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFileName.Location = new System.Drawing.Point(0, 0);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(41, 12);
            this.txtFileName.TabIndex = 0;
            this.txtFileName.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(648, 11);
            this.progressBar1.TabIndex = 1;
            // 
            // cmdComplete
            // 
            this.cmdComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdComplete.Enabled = false;
            this.cmdComplete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdComplete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdComplete.Location = new System.Drawing.Point(581, 29);
            this.cmdComplete.Name = "cmdComplete";
            this.cmdComplete.Size = new System.Drawing.Size(64, 26);
            this.cmdComplete.TabIndex = 2;
            this.cmdComplete.Text = "完成";
            this.cmdComplete.UseVisualStyleBackColor = true;
            this.cmdComplete.Click += new System.EventHandler(this.cmdComplete_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCancel.Location = new System.Drawing.Point(511, 29);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(64, 26);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // UIFileDownLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdComplete);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtFileName);
            this.MaximumSize = new System.Drawing.Size(650, 2);
            this.MinimumSize = new System.Drawing.Size(2, 60);
            this.Name = "UIFileDownLoad";
            this.Size = new System.Drawing.Size(648, 58);
            this.Load += new System.EventHandler(this.UIFileDownLoad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtFileName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button cmdComplete;
        private System.Windows.Forms.Button cmdCancel;
    }
}
