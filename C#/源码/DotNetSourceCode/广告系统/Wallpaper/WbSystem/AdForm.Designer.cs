namespace WbSystem
{
    partial class AdForm
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
            this.pictureAd2 = new System.Windows.Forms.PictureBox();
            this.pictureAd3 = new System.Windows.Forms.PictureBox();
            this.pictureAd4 = new System.Windows.Forms.PictureBox();
            this.pictureAd1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureAd2
            // 
            this.pictureAd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureAd2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAd2.Location = new System.Drawing.Point(0, 232);
            this.pictureAd2.Name = "pictureAd2";
            this.pictureAd2.Size = new System.Drawing.Size(150, 140);
            this.pictureAd2.TabIndex = 2;
            this.pictureAd2.TabStop = false;
            this.pictureAd2.Click += new System.EventHandler(this.pictureAd2_Click);
            // 
            // pictureAd3
            // 
            this.pictureAd3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureAd3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAd3.Location = new System.Drawing.Point(0, 395);
            this.pictureAd3.Name = "pictureAd3";
            this.pictureAd3.Size = new System.Drawing.Size(150, 140);
            this.pictureAd3.TabIndex = 3;
            this.pictureAd3.TabStop = false;
            this.pictureAd3.Click += new System.EventHandler(this.pictureAd3_Click);
            // 
            // pictureAd4
            // 
            this.pictureAd4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureAd4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAd4.Location = new System.Drawing.Point(0, 558);
            this.pictureAd4.Name = "pictureAd4";
            this.pictureAd4.Size = new System.Drawing.Size(150, 140);
            this.pictureAd4.TabIndex = 5;
            this.pictureAd4.TabStop = false;
            this.pictureAd4.Click += new System.EventHandler(this.pictureAd4_Click);
            // 
            // pictureAd1
            // 
            this.pictureAd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureAd1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAd1.Location = new System.Drawing.Point(0, 68);
            this.pictureAd1.Name = "pictureAd1";
            this.pictureAd1.Size = new System.Drawing.Size(150, 140);
            this.pictureAd1.TabIndex = 6;
            this.pictureAd1.TabStop = false;
            this.pictureAd1.Click += new System.EventHandler(this.pictureAd1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 785);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureAd1);
            this.Controls.Add(this.pictureAd4);
            this.Controls.Add(this.pictureAd3);
            this.Controls.Add(this.pictureAd2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdForm";
            this.ShowInTaskbar = false;
            this.Text = "AdForm";
            this.Load += new System.EventHandler(this.AdForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAd1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureAd2;
        private System.Windows.Forms.PictureBox pictureAd3;
        private System.Windows.Forms.PictureBox pictureAd4;
        private System.Windows.Forms.PictureBox pictureAd1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

