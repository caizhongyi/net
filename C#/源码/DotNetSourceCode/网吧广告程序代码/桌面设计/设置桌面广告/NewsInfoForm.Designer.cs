namespace WbSystem
{
    partial class NewsInfoForm
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
            this.btnRed = new System.Windows.Forms.Button();
            this.btnWhite = new System.Windows.Forms.Button();
            this.btnBule = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnBlack = new System.Windows.Forms.Button();
            this.btnPurple = new System.Windows.Forms.Button();
            this.noticeBar1 = new GunDongDeZi.NoticeBar();
            this.SuspendLayout();
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.Color.Red;
            this.btnRed.Location = new System.Drawing.Point(822, 1);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(19, 23);
            this.btnRed.TabIndex = 1;
            this.btnRed.UseVisualStyleBackColor = false;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // btnWhite
            // 
            this.btnWhite.BackColor = System.Drawing.Color.White;
            this.btnWhite.Location = new System.Drawing.Point(847, 1);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(19, 23);
            this.btnWhite.TabIndex = 2;
            this.btnWhite.UseVisualStyleBackColor = false;
            this.btnWhite.Click += new System.EventHandler(this.btnWhite_Click);
            // 
            // btnBule
            // 
            this.btnBule.BackColor = System.Drawing.Color.Blue;
            this.btnBule.Location = new System.Drawing.Point(872, 1);
            this.btnBule.Name = "btnBule";
            this.btnBule.Size = new System.Drawing.Size(19, 23);
            this.btnBule.TabIndex = 3;
            this.btnBule.UseVisualStyleBackColor = false;
            this.btnBule.Click += new System.EventHandler(this.btnBule_Click);
            // 
            // btnGreen
            // 
            this.btnGreen.BackColor = System.Drawing.Color.Green;
            this.btnGreen.Location = new System.Drawing.Point(897, 1);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(19, 23);
            this.btnGreen.TabIndex = 4;
            this.btnGreen.UseVisualStyleBackColor = false;
            this.btnGreen.Click += new System.EventHandler(this.btnGreen_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnYellow.Location = new System.Drawing.Point(922, 1);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(19, 23);
            this.btnYellow.TabIndex = 5;
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.BackColor = System.Drawing.Color.Black;
            this.btnBlack.Location = new System.Drawing.Point(947, 1);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(19, 23);
            this.btnBlack.TabIndex = 6;
            this.btnBlack.UseVisualStyleBackColor = false;
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnPurple
            // 
            this.btnPurple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnPurple.Location = new System.Drawing.Point(972, 1);
            this.btnPurple.Name = "btnPurple";
            this.btnPurple.Size = new System.Drawing.Size(19, 23);
            this.btnPurple.TabIndex = 7;
            this.btnPurple.UseVisualStyleBackColor = false;
            this.btnPurple.Click += new System.EventHandler(this.btnPurple_Click);
            // 
            // noticeBar1
            // 
            this.noticeBar1.BackColor = System.Drawing.Color.Transparent;
            this.noticeBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.noticeBar1.GetNewColor = System.Drawing.Color.Red;
            this.noticeBar1.GetText = "XXX";
            this.noticeBar1.Location = new System.Drawing.Point(0, 0);
            this.noticeBar1.Name = "noticeBar1";
            this.noticeBar1.Size = new System.Drawing.Size(816, 25);
            this.noticeBar1.TabIndex = 8;
            this.noticeBar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.noticeBar1_MouseMove);
            this.noticeBar1.MouseLeave += new System.EventHandler(this.noticeBar1_MouseLeave);
            // 
            // NewsInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 25);
            this.Controls.Add(this.noticeBar1);
            this.Controls.Add(this.btnPurple);
            this.Controls.Add(this.btnBlack);
            this.Controls.Add(this.btnYellow);
            this.Controls.Add(this.btnGreen);
            this.Controls.Add(this.btnBule);
            this.Controls.Add(this.btnWhite);
            this.Controls.Add(this.btnRed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewsInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NewsInfoForm";
            this.Load += new System.EventHandler(this.NewsInfoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnWhite;
        private System.Windows.Forms.Button btnBule;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button btnPurple;
        private GunDongDeZi.NoticeBar noticeBar1;
    }
}