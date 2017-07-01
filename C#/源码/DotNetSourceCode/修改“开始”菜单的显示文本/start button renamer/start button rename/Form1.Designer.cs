namespace start_button_rename
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Options = new TestSample.RoundedRectangularGroupBoxWithToolbar();
            this.skinRadioButton2 = new DotNetSkin.SkinControls.SkinRadioButton();
            this.skinRadioButton1 = new DotNetSkin.SkinControls.SkinRadioButton();
            this.skinImage1 = new DotNetSkin.SkinControls.SkinImage();
            this.roundedRectangularGroupBoxWithToolbar1 = new TestSample.RoundedRectangularGroupBoxWithToolbar();
            this.skinButton1 = new DotNetSkin.SkinControls.SkinButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Options.SuspendLayout();
            this.roundedRectangularGroupBoxWithToolbar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(39, 68);
            this.textBox1.MaxLength = 20;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter start menu text here";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(35, 189);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(316, 30);
            this.status.TabIndex = 3;
            this.status.Text = "Start button has been renamed.\r\nChanges will take effect after restarting windows" +
                "";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.status.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(36, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "(Not more then 20 simbols)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(17, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 30);
            this.label4.TabIndex = 14;
            this.label4.Text = "Change\r\nstart menu text for:";
            // 
            // Options
            // 
            this.Options.BorderWidth = 6;
            this.Options.ColorScheme = TestSample.EnmColorScheme.Green;
            this.Options.Controls.Add(this.skinRadioButton2);
            this.Options.Controls.Add(this.skinRadioButton1);
            this.Options.Controls.Add(this.label4);
            this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Options.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Options.Location = new System.Drawing.Point(236, 6);
            this.Options.Name = "Options";
            this.Options.ShadowColor = System.Drawing.Color.White;
            this.Options.Size = new System.Drawing.Size(140, 114);
            this.Options.TabIndex = 18;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
            this.Options.ToolbarWidth = 50;
            // 
            // skinRadioButton2
            // 
            this.skinRadioButton2.AutoSize = true;
            this.skinRadioButton2.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton2.Location = new System.Drawing.Point(23, 80);
            this.skinRadioButton2.Name = "skinRadioButton2";
            this.skinRadioButton2.Size = new System.Drawing.Size(68, 19);
            this.skinRadioButton2.TabIndex = 16;
            this.skinRadioButton2.Text = "only me";
            this.skinRadioButton2.UseVisualStyleBackColor = true;
            // 
            // skinRadioButton1
            // 
            this.skinRadioButton1.AutoSize = true;
            this.skinRadioButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinRadioButton1.Checked = true;
            this.skinRadioButton1.Location = new System.Drawing.Point(23, 54);
            this.skinRadioButton1.Name = "skinRadioButton1";
            this.skinRadioButton1.Size = new System.Drawing.Size(74, 19);
            this.skinRadioButton1.TabIndex = 15;
            this.skinRadioButton1.TabStop = true;
            this.skinRadioButton1.Text = "everyone";
            this.skinRadioButton1.UseVisualStyleBackColor = false;
            // 
            // skinImage1
            // 
            this.skinImage1.Scheme = DotNetSkin.SkinControls.Schemes.MacOs;
            // 
            // roundedRectangularGroupBoxWithToolbar1
            // 
            this.roundedRectangularGroupBoxWithToolbar1.BorderWidth = 6;
            this.roundedRectangularGroupBoxWithToolbar1.ColorScheme = TestSample.EnmColorScheme.Green;
            this.roundedRectangularGroupBoxWithToolbar1.Controls.Add(this.textBox1);
            this.roundedRectangularGroupBoxWithToolbar1.Controls.Add(this.label3);
            this.roundedRectangularGroupBoxWithToolbar1.Controls.Add(this.label1);
            this.roundedRectangularGroupBoxWithToolbar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roundedRectangularGroupBoxWithToolbar1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.roundedRectangularGroupBoxWithToolbar1.Location = new System.Drawing.Point(4, 6);
            this.roundedRectangularGroupBoxWithToolbar1.Name = "roundedRectangularGroupBoxWithToolbar1";
            this.roundedRectangularGroupBoxWithToolbar1.ShadowColor = System.Drawing.Color.White;
            this.roundedRectangularGroupBoxWithToolbar1.Size = new System.Drawing.Size(222, 114);
            this.roundedRectangularGroupBoxWithToolbar1.TabIndex = 19;
            this.roundedRectangularGroupBoxWithToolbar1.TabStop = false;
            this.roundedRectangularGroupBoxWithToolbar1.Text = "Start menu text";
            this.roundedRectangularGroupBoxWithToolbar1.ToolbarWidth = 15;
            // 
            // skinButton1
            // 
            this.skinButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(204)));
            this.skinButton1.ForeColor = System.Drawing.Color.Black;
            this.skinButton1.Location = new System.Drawing.Point(45, 125);
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.Size = new System.Drawing.Size(297, 59);
            this.skinButton1.TabIndex = 20;
            this.skinButton1.Text = "RENAME!";
            this.skinButton1.UseVisualStyleBackColor = true;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.Location = new System.Drawing.Point(332, 219);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 15);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "About";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AcceptButton = this.skinButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.ClientSize = new System.Drawing.Size(386, 236);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.roundedRectangularGroupBoxWithToolbar1);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.status);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(420, 270);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Button Renamer";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.roundedRectangularGroupBoxWithToolbar1.ResumeLayout(false);
            this.roundedRectangularGroupBoxWithToolbar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private TestSample.RoundedRectangularGroupBoxWithToolbar Options;
        private DotNetSkin.SkinControls.SkinRadioButton skinRadioButton1;
        private DotNetSkin.SkinControls.SkinImage skinImage1;
        private DotNetSkin.SkinControls.SkinRadioButton skinRadioButton2;
        private TestSample.RoundedRectangularGroupBoxWithToolbar roundedRectangularGroupBoxWithToolbar1;
        private DotNetSkin.SkinControls.SkinButton skinButton1;
        private System.Windows.Forms.LinkLabel linkLabel1;


    }
}

