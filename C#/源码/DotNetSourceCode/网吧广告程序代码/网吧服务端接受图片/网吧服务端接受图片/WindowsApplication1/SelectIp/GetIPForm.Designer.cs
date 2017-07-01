namespace WindowsApplication1.SelectIp
{
    partial class GetIPForm
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
            this.lvwDisplayUser = new System.Windows.Forms.ListView();
            this.chUserName = new System.Windows.Forms.ColumnHeader();
            this.chComputerName = new System.Windows.Forms.ColumnHeader();
            this.chIP = new System.Windows.Forms.ColumnHeader();
            this.chWorkGroup = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lvwDisplayUser
            // 
            this.lvwDisplayUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDisplayUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUserName,
            this.chComputerName,
            this.chIP,
            this.chWorkGroup});
            this.lvwDisplayUser.FullRowSelect = true;
            this.lvwDisplayUser.GridLines = true;
            this.lvwDisplayUser.HideSelection = false;
            this.lvwDisplayUser.Location = new System.Drawing.Point(12, 12);
            this.lvwDisplayUser.Name = "lvwDisplayUser";
            this.lvwDisplayUser.Size = new System.Drawing.Size(499, 161);
            this.lvwDisplayUser.TabIndex = 7;
            this.lvwDisplayUser.UseCompatibleStateImageBehavior = false;
            this.lvwDisplayUser.View = System.Windows.Forms.View.Details;
            // 
            // chUserName
            // 
            this.chUserName.Text = "用户名";
            this.chUserName.Width = 71;
            // 
            // chComputerName
            // 
            this.chComputerName.Text = "主机名";
            this.chComputerName.Width = 64;
            // 
            // chIP
            // 
            this.chIP.Text = "IP";
            this.chIP.Width = 77;
            // 
            // chWorkGroup
            // 
            this.chWorkGroup.Text = "工作组";
            this.chWorkGroup.Width = 66;
            // 
            // GetIPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 195);
            this.Controls.Add(this.lvwDisplayUser);
            this.Name = "GetIPForm";
            this.Text = "GetIPForm";
            this.Load += new System.EventHandler(this.GetIPForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwDisplayUser;
        private System.Windows.Forms.ColumnHeader chUserName;
        private System.Windows.Forms.ColumnHeader chComputerName;
        private System.Windows.Forms.ColumnHeader chIP;
        private System.Windows.Forms.ColumnHeader chWorkGroup;
    }
}