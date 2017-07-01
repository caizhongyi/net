namespace 随机变化桌面背景图片
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnChangeWallpaper = new System.Windows.Forms.Button();
            this.lblCopyRightInfo = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblmusicBox = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.lblmusicList = new System.Windows.Forms.Label();
            this.btnAttribute = new System.Windows.Forms.Button();
            this.btnCloseMediaPlayer = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lstmusicList = new System.Windows.Forms.ListBox();
            this.btnUp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChangeWallpaper
            // 
            this.btnChangeWallpaper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeWallpaper.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChangeWallpaper.Location = new System.Drawing.Point(66, 111);
            this.btnChangeWallpaper.Name = "btnChangeWallpaper";
            this.btnChangeWallpaper.Size = new System.Drawing.Size(136, 25);
            this.btnChangeWallpaper.TabIndex = 0;
            this.btnChangeWallpaper.Text = "更换桌面背景图片(&C)";
            this.btnChangeWallpaper.UseVisualStyleBackColor = true;
            this.btnChangeWallpaper.Click += new System.EventHandler(this.BtnChangeWallpaperClick);
            // 
            // lblCopyRightInfo
            // 
            this.lblCopyRightInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCopyRightInfo.ForeColor = System.Drawing.Color.Ivory;
            this.lblCopyRightInfo.Location = new System.Drawing.Point(66, 30);
            this.lblCopyRightInfo.Name = "lblCopyRightInfo";
            this.lblCopyRightInfo.Size = new System.Drawing.Size(136, 27);
            this.lblCopyRightInfo.TabIndex = 1;
            this.lblCopyRightInfo.Text = "To:佐掱邊的兲使";
            this.lblCopyRightInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCopyRightInfo.MouseLeave += new System.EventHandler(this.LblCopyRightInfoMouseLeave);
            this.lblCopyRightInfo.MouseHover += new System.EventHandler(this.LblCopyRightInfoMouseHover);
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Monotype Corsiva", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.HotPink;
            this.txtMessage.Location = new System.Drawing.Point(21, 69);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(232, 25);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "Do spends each day with a smile.";
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // lblmusicBox
            // 
            this.lblmusicBox.Image = ((System.Drawing.Image)(resources.GetObject("lblmusicBox.Image")));
            this.lblmusicBox.Location = new System.Drawing.Point(235, 111);
            this.lblmusicBox.Name = "lblmusicBox";
            this.lblmusicBox.Size = new System.Drawing.Size(36, 34);
            this.lblmusicBox.TabIndex = 3;
            this.lblmusicBox.Click += new System.EventHandler(this.lblmusicBoxClick);
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(303, 46);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(439, 349);
            this.axWindowsMediaPlayer.TabIndex = 4;
            // 
            // lblmusicList
            // 
            this.lblmusicList.Location = new System.Drawing.Point(12, 139);
            this.lblmusicList.Name = "lblmusicList";
            this.lblmusicList.Size = new System.Drawing.Size(109, 15);
            this.lblmusicList.TabIndex = 7;
            this.lblmusicList.Text = "播放清单";
            this.lblmusicList.Visible = false;
            // 
            // btnAttribute
            // 
            this.btnAttribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttribute.ForeColor = System.Drawing.Color.Ivory;
            this.btnAttribute.Location = new System.Drawing.Point(303, 404);
            this.btnAttribute.Name = "btnAttribute";
            this.btnAttribute.Size = new System.Drawing.Size(105, 28);
            this.btnAttribute.TabIndex = 8;
            this.btnAttribute.Text = "文件属性(&A)";
            this.btnAttribute.UseVisualStyleBackColor = true;
            this.btnAttribute.Click += new System.EventHandler(this.btnAttributeClick);
            // 
            // btnCloseMediaPlayer
            // 
            this.btnCloseMediaPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseMediaPlayer.ForeColor = System.Drawing.Color.Ivory;
            this.btnCloseMediaPlayer.Location = new System.Drawing.Point(463, 404);
            this.btnCloseMediaPlayer.Name = "btnCloseMediaPlayer";
            this.btnCloseMediaPlayer.Size = new System.Drawing.Size(105, 28);
            this.btnCloseMediaPlayer.TabIndex = 10;
            this.btnCloseMediaPlayer.Text = "返回初始状态(&B)";
            this.btnCloseMediaPlayer.UseVisualStyleBackColor = true;
            this.btnCloseMediaPlayer.Click += new System.EventHandler(this.BtnCloseMediaPlayerClick);
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.Color.Ivory;
            this.btnOpen.Location = new System.Drawing.Point(620, 404);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(105, 28);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Text = "打开(&O)";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(227, 294);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(27, 26);
            this.btnDown.TabIndex = 12;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Visible = false;
            this.btnDown.Click += new System.EventHandler(this.BtnDownClick);
            // 
            // lstmusicList
            // 
            this.lstmusicList.FormattingEnabled = true;
            this.lstmusicList.HorizontalScrollbar = true;
            this.lstmusicList.ItemHeight = 12;
            this.lstmusicList.Location = new System.Drawing.Point(21, 170);
            this.lstmusicList.Name = "lstmusicList";
            this.lstmusicList.ScrollAlwaysVisible = true;
            this.lstmusicList.Size = new System.Drawing.Size(190, 232);
            this.lstmusicList.TabIndex = 13;
            this.lstmusicList.Visible = false;
            this.lstmusicList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstmusicListMouseDoubleClick);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(227, 218);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(27, 26);
            this.btnUp.TabIndex = 12;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Visible = false;
            this.btnUp.Click += new System.EventHandler(this.BtnUpClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(278, 257);
            this.Controls.Add(this.lstmusicList);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnCloseMediaPlayer);
            this.Controls.Add(this.btnAttribute);
            this.Controls.Add(this.lblmusicList);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Controls.Add(this.lblmusicBox);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblCopyRightInfo);
            this.Controls.Add(this.btnChangeWallpaper);
            this.Name = "MainForm";
            this.Text = "随机变化桌面背景图片";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ListBox lstmusicList;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnAttribute;
		private System.Windows.Forms.Button btnCloseMediaPlayer;
		private System.Windows.Forms.Label lblmusicList;
		private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label lblmusicBox;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label lblCopyRightInfo;
		private System.Windows.Forms.Button btnChangeWallpaper;
	}
}
