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
            this.components = new System.ComponentModel.Container();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnWhite = new System.Windows.Forms.Button();
            this.btnBule = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnBlack = new System.Windows.Forms.Button();
            this.btnPurple = new System.Windows.Forms.Button();
            this.btnNewForm = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblText = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.Color.Red;
            this.btnRed.Location = new System.Drawing.Point(822, 3);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(19, 23);
            this.btnRed.TabIndex = 1;
            this.btnRed.UseVisualStyleBackColor = false;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // btnWhite
            // 
            this.btnWhite.BackColor = System.Drawing.Color.White;
            this.btnWhite.Location = new System.Drawing.Point(847, 3);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(19, 23);
            this.btnWhite.TabIndex = 2;
            this.btnWhite.UseVisualStyleBackColor = false;
            this.btnWhite.Click += new System.EventHandler(this.btnWhite_Click);
            // 
            // btnBule
            // 
            this.btnBule.BackColor = System.Drawing.Color.Blue;
            this.btnBule.Location = new System.Drawing.Point(872, 3);
            this.btnBule.Name = "btnBule";
            this.btnBule.Size = new System.Drawing.Size(19, 23);
            this.btnBule.TabIndex = 3;
            this.btnBule.UseVisualStyleBackColor = false;
            this.btnBule.Click += new System.EventHandler(this.btnBule_Click);
            // 
            // btnGreen
            // 
            this.btnGreen.BackColor = System.Drawing.Color.Green;
            this.btnGreen.Location = new System.Drawing.Point(897, 3);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(19, 23);
            this.btnGreen.TabIndex = 4;
            this.btnGreen.UseVisualStyleBackColor = false;
            this.btnGreen.Click += new System.EventHandler(this.btnGreen_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnYellow.Location = new System.Drawing.Point(922, 3);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(19, 23);
            this.btnYellow.TabIndex = 5;
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.BackColor = System.Drawing.Color.Black;
            this.btnBlack.Location = new System.Drawing.Point(947, 3);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(19, 23);
            this.btnBlack.TabIndex = 6;
            this.btnBlack.UseVisualStyleBackColor = false;
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnPurple
            // 
            this.btnPurple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnPurple.Location = new System.Drawing.Point(972, 3);
            this.btnPurple.Name = "btnPurple";
            this.btnPurple.Size = new System.Drawing.Size(19, 23);
            this.btnPurple.TabIndex = 7;
            this.btnPurple.UseVisualStyleBackColor = false;
            this.btnPurple.Click += new System.EventHandler(this.btnPurple_Click);
            // 
            // btnNewForm
            // 
            this.btnNewForm.Location = new System.Drawing.Point(997, 2);
            this.btnNewForm.Name = "btnNewForm";
            this.btnNewForm.Size = new System.Drawing.Size(21, 23);
            this.btnNewForm.TabIndex = 9;
            this.btnNewForm.Text = ">";
            this.btnNewForm.UseVisualStyleBackColor = true;
            this.btnNewForm.Click += new System.EventHandler(this.btnNewForm_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblText
            // 
            this.lblText.ActiveLinkColor = System.Drawing.Color.Transparent;
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblText.ForeColor = System.Drawing.Color.Transparent;
            this.lblText.LinkColor = System.Drawing.Color.Black;
            this.lblText.Location = new System.Drawing.Point(0, 3);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(20, 19);
            this.lblText.TabIndex = 10;
            this.lblText.TabStop = true;
            this.lblText.Text = "X";
            this.lblText.MouseLeave += new System.EventHandler(this.lblText_MouseLeave);
            this.lblText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblText_MouseMove);
            this.lblText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblText_LinkClicked);
            // 
            // NewsInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 25);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnNewForm);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnWhite;
        private System.Windows.Forms.Button btnBule;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button btnPurple;
        private System.Windows.Forms.Button btnNewForm;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel lblText;
    }
}