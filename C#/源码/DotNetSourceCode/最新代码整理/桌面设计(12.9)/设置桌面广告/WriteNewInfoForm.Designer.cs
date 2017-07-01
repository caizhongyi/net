namespace WbSystem
{
    partial class WriteNewInfoForm
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbContent = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnKeep = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lkbWarnInfo = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(27, 49);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(65, 12);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "信息标题：";
            // 
            // lbContent
            // 
            this.lbContent.AutoSize = true;
            this.lbContent.Location = new System.Drawing.Point(27, 170);
            this.lbContent.Name = "lbContent";
            this.lbContent.Size = new System.Drawing.Size(65, 12);
            this.lbContent.TabIndex = 1;
            this.lbContent.Text = "信息内容：";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(29, 80);
            this.txtTitle.MaxLength = 50;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(424, 21);
            this.txtTitle.TabIndex = 2;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(29, 196);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(424, 289);
            this.txtContent.TabIndex = 4;
            // 
            // btnKeep
            // 
            this.btnKeep.Location = new System.Drawing.Point(101, 504);
            this.btnKeep.Name = "btnKeep";
            this.btnKeep.Size = new System.Drawing.Size(75, 23);
            this.btnKeep.TabIndex = 5;
            this.btnKeep.Text = "保存";
            this.btnKeep.UseVisualStyleBackColor = true;
            this.btnKeep.Click += new System.EventHandler(this.btnKeep_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(276, 504);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lkbWarnInfo
            // 
            this.lkbWarnInfo.AutoSize = true;
            this.lkbWarnInfo.LinkColor = System.Drawing.Color.Red;
            this.lkbWarnInfo.Location = new System.Drawing.Point(99, 49);
            this.lkbWarnInfo.Name = "lkbWarnInfo";
            this.lkbWarnInfo.Size = new System.Drawing.Size(185, 12);
            this.lkbWarnInfo.TabIndex = 6;
            this.lkbWarnInfo.TabStop = true;
            this.lkbWarnInfo.Text = "(信息标题最多输入字数为50字符)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "导航信息：";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.Red;
            this.linkLabel1.Location = new System.Drawing.Point(99, 108);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 12);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(请输入你的网址信息)";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(29, 135);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(424, 21);
            this.txtURL.TabIndex = 3;
            // 
            // WriteNewInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 539);
            this.ControlBox = false;
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lkbWarnInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnKeep);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lbContent);
            this.Controls.Add(this.lbTitle);
            this.Name = "WriteNewInfoForm";
            this.ShowIcon = false;
            this.Text = "请填写你的新信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbContent;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnKeep;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel lkbWarnInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtURL;
    }
}