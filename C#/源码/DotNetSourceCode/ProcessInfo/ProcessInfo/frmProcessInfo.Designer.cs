namespace ProcessInfo
{
    partial class frmProcessInfo
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
            this.btnGetProcessList = new System.Windows.Forms.Button();
            this.btnGetProcessByID = new System.Windows.Forms.Button();
            this.txtProcessID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetProcessList
            // 
            this.btnGetProcessList.Location = new System.Drawing.Point(28, 164);
            this.btnGetProcessList.Name = "btnGetProcessList";
            this.btnGetProcessList.Size = new System.Drawing.Size(183, 23);
            this.btnGetProcessList.TabIndex = 0;
            this.btnGetProcessList.Text = "获得当前激活进程列表";
            this.btnGetProcessList.UseVisualStyleBackColor = true;
            this.btnGetProcessList.Click += new System.EventHandler(this.btnGetProcessList_Click);
            // 
            // btnGetProcessByID
            // 
            this.btnGetProcessByID.Location = new System.Drawing.Point(28, 218);
            this.btnGetProcessByID.Name = "btnGetProcessByID";
            this.btnGetProcessByID.Size = new System.Drawing.Size(183, 23);
            this.btnGetProcessByID.TabIndex = 1;
            this.btnGetProcessByID.Text = "获得指定ID 的进程信息";
            this.btnGetProcessByID.UseVisualStyleBackColor = true;
            this.btnGetProcessByID.Click += new System.EventHandler(this.btnGetProcessByID_Click);
            // 
            // txtProcessID
            // 
            this.txtProcessID.Location = new System.Drawing.Point(28, 64);
            this.txtProcessID.Name = "txtProcessID";
            this.txtProcessID.Size = new System.Drawing.Size(100, 21);
            this.txtProcessID.TabIndex = 2;
            // 
            // frmProcessInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 266);
            this.Controls.Add(this.txtProcessID);
            this.Controls.Add(this.btnGetProcessByID);
            this.Controls.Add(this.btnGetProcessList);
            this.Name = "frmProcessInfo";
            this.Text = "Process Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetProcessList;
        private System.Windows.Forms.Button btnGetProcessByID;
        private System.Windows.Forms.TextBox txtProcessID;
    }
}

