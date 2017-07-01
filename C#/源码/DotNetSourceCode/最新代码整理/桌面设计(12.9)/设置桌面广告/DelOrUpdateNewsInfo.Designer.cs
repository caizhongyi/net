namespace WbSystem
{
    partial class DelOrUpdateNewsInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbNewsInfo = new System.Windows.Forms.ComboBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbContent = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnMod = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择操作内容：";
            // 
            // cbNewsInfo
            // 
            this.cbNewsInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewsInfo.FormattingEnabled = true;
            this.cbNewsInfo.Items.AddRange(new object[] {
            "请选择",
            "信息标题",
            "导航信息",
            "信息内容"});
            this.cbNewsInfo.Location = new System.Drawing.Point(123, 21);
            this.cbNewsInfo.Name = "cbNewsInfo";
            this.cbNewsInfo.Size = new System.Drawing.Size(121, 20);
            this.cbNewsInfo.TabIndex = 1;
            this.cbNewsInfo.SelectedIndexChanged += new System.EventHandler(this.cbNewsInfo_SelectedIndexChanged);
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(16, 60);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(65, 12);
            this.lbTitle.TabIndex = 3;
            this.lbTitle.Text = "信息标题：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "导航信息：";
            // 
            // lbContent
            // 
            this.lbContent.AutoSize = true;
            this.lbContent.Location = new System.Drawing.Point(14, 178);
            this.lbContent.Name = "lbContent";
            this.lbContent.Size = new System.Drawing.Size(65, 12);
            this.lbContent.TabIndex = 8;
            this.lbContent.Text = "信息内容：";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(18, 78);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(396, 21);
            this.txtTitle.TabIndex = 10;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(18, 137);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(396, 21);
            this.txtURL.TabIndex = 11;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(18, 204);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ReadOnly = true;
            this.txtContent.Size = new System.Drawing.Size(396, 240);
            this.txtContent.TabIndex = 12;
            // 
            // btnMod
            // 
            this.btnMod.Location = new System.Drawing.Point(68, 478);
            this.btnMod.Name = "btnMod";
            this.btnMod.Size = new System.Drawing.Size(75, 23);
            this.btnMod.TabIndex = 13;
            this.btnMod.Text = "修 改";
            this.btnMod.UseVisualStyleBackColor = true;
            this.btnMod.Click += new System.EventHandler(this.btnMod_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(180, 478);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 14;
            this.btnDel.Text = "删 除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(293, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DelOrUpdateNewsInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 523);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnMod);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbContent);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.cbNewsInfo);
            this.Controls.Add(this.label1);
            this.Name = "DelOrUpdateNewsInfo";
            this.ShowIcon = false;
            this.Text = "选择你的操作...";
            this.Load += new System.EventHandler(this.DelOrUpdateNewsInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNewsInfo;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbContent;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnMod;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnClose;
    }
}