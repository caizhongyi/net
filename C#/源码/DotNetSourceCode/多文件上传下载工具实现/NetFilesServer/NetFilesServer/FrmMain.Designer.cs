namespace NetFilesServer
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripButton();
            this.cmdStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdAdddirectory = new System.Windows.Forms.ToolStripButton();
            this.cmdDisable = new System.Windows.Forms.ToolStripButton();
            this.cmdDeldirectory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.服务目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消息日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.错误日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdExit = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listDirectory = new System.Windows.Forms.ListView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.cmdStop,
            this.toolStripSeparator1,
            this.cmdAdddirectory,
            this.cmdDisable,
            this.cmdDeldirectory,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.cmdExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(532, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdStart
            // 
            this.cmdStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdStart.Image = global::NetFilesServer.Properties.Resources._00_27_;
            this.cmdStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(23, 22);
            this.cmdStart.Text = "启动";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdStop.Enabled = false;
            this.cmdStop.Image = global::NetFilesServer.Properties.Resources._13_20_;
            this.cmdStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(23, 22);
            this.cmdStop.Text = "停止服务";
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdAdddirectory
            // 
            this.cmdAdddirectory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAdddirectory.Image = global::NetFilesServer.Properties.Resources._19_08_;
            this.cmdAdddirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAdddirectory.Name = "cmdAdddirectory";
            this.cmdAdddirectory.Size = new System.Drawing.Size(23, 22);
            this.cmdAdddirectory.Text = "添加服务目录";
            this.cmdAdddirectory.Click += new System.EventHandler(this.cmdAdddirectory_Click);
            // 
            // cmdDisable
            // 
            this.cmdDisable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDisable.Enabled = false;
            this.cmdDisable.Image = ((System.Drawing.Image)(resources.GetObject("cmdDisable.Image")));
            this.cmdDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDisable.Name = "cmdDisable";
            this.cmdDisable.Size = new System.Drawing.Size(23, 22);
            this.cmdDisable.Text = "服务目录属性设置";
            this.cmdDisable.Click += new System.EventHandler(this.cmdDisable_Click);
            // 
            // cmdDeldirectory
            // 
            this.cmdDeldirectory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeldirectory.Enabled = false;
            this.cmdDeldirectory.Image = global::NetFilesServer.Properties.Resources._19_20_;
            this.cmdDeldirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeldirectory.Name = "cmdDeldirectory";
            this.cmdDeldirectory.Size = new System.Drawing.Size(23, 22);
            this.cmdDeldirectory.Text = "删除服务目录";
            this.cmdDeldirectory.Click += new System.EventHandler(this.cmdDeldirectory_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务目录ToolStripMenuItem,
            this.连接信息ToolStripMenuItem,
            this.消息日志ToolStripMenuItem,
            this.错误日志ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::NetFilesServer.Properties.Resources._36_41_;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "信息查询";
            // 
            // 服务目录ToolStripMenuItem
            // 
            this.服务目录ToolStripMenuItem.Name = "服务目录ToolStripMenuItem";
            this.服务目录ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.服务目录ToolStripMenuItem.Text = "服务目录";
            // 
            // 连接信息ToolStripMenuItem
            // 
            this.连接信息ToolStripMenuItem.Name = "连接信息ToolStripMenuItem";
            this.连接信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.连接信息ToolStripMenuItem.Text = "连接信息";
            // 
            // 消息日志ToolStripMenuItem
            // 
            this.消息日志ToolStripMenuItem.Name = "消息日志ToolStripMenuItem";
            this.消息日志ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.消息日志ToolStripMenuItem.Text = "消息日志";
            // 
            // 错误日志ToolStripMenuItem
            // 
            this.错误日志ToolStripMenuItem.Name = "错误日志ToolStripMenuItem";
            this.错误日志ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.错误日志ToolStripMenuItem.Text = "错误日志";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdExit
            // 
            this.cmdExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdExit.Image = global::NetFilesServer.Properties.Resources._00_47_;
            this.cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(23, 22);
            this.cmdExit.Text = "退出";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 251);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(532, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 17);
            // 
            // listDirectory
            // 
            this.listDirectory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDirectory.HideSelection = false;
            this.listDirectory.Location = new System.Drawing.Point(0, 25);
            this.listDirectory.MultiSelect = false;
            this.listDirectory.Name = "listDirectory";
            this.listDirectory.ShowItemToolTips = true;
            this.listDirectory.Size = new System.Drawing.Size(532, 226);
            this.listDirectory.TabIndex = 3;
            this.listDirectory.UseCompatibleStateImageBehavior = false;
            this.listDirectory.View = System.Windows.Forms.View.Details;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 273);
            this.Controls.Add(this.listDirectory);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网络文件服务";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdStart;
        private System.Windows.Forms.ToolStripButton cmdStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdAdddirectory;
        private System.Windows.Forms.ToolStripButton cmdDeldirectory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdDisable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton cmdExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListView listDirectory;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 服务目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 连接信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消息日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 错误日志ToolStripMenuItem;
    }
}

