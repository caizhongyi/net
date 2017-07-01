namespace FtpClient_cs
{
    partial class FTPClientForm
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.textHost = new System.Windows.Forms.TextBox();
            this.listLocal = new System.Windows.Forms.ListView();
            this.listRemote = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textUser = new System.Windows.Forms.TextBox();
            this.textPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.textLocal = new System.Windows.Forms.TextBox();
            this.textRemote = new System.Windows.Forms.TextBox();
            this.textMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 21);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(623, 13);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 21);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "断开连接";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // textHost
            // 
            this.textHost.Location = new System.Drawing.Point(146, 13);
            this.textHost.Name = "textHost";
            this.textHost.Size = new System.Drawing.Size(94, 21);
            this.textHost.TabIndex = 2;
            this.textHost.Text = "\r\n202.75.219.210";
            // 
            // listLocal
            // 
            this.listLocal.FullRowSelect = true;
            this.listLocal.GridLines = true;
            this.listLocal.Location = new System.Drawing.Point(14, 79);
            this.listLocal.Name = "listLocal";
            this.listLocal.Size = new System.Drawing.Size(220, 240);
            this.listLocal.TabIndex = 3;
            this.listLocal.UseCompatibleStateImageBehavior = false;
            this.listLocal.View = System.Windows.Forms.View.Details;
            this.listLocal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listLocal_MouseDoubleClick);
            // 
            // listRemote
            // 
            this.listRemote.FullRowSelect = true;
            this.listRemote.GridLines = true;
            this.listRemote.Location = new System.Drawing.Point(478, 79);
            this.listRemote.Name = "listRemote";
            this.listRemote.Size = new System.Drawing.Size(220, 240);
            this.listRemote.TabIndex = 4;
            this.listRemote.UseCompatibleStateImageBehavior = false;
            this.listRemote.View = System.Windows.Forms.View.Details;
            this.listRemote.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listRemote_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "用户名：";
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(414, 13);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(94, 21);
            this.textUser.TabIndex = 7;
            this.textUser.Text = "sq_dbggcmxt";
            // 
            // textPass
            // 
            this.textPass.Location = new System.Drawing.Point(561, 13);
            this.textPass.Name = "textPass";
            this.textPass.Size = new System.Drawing.Size(56, 21);
            this.textPass.TabIndex = 9;
            this.textPass.Text = "yunzhou168";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(514, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "密码：";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(293, 13);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(56, 21);
            this.textPort.TabIndex = 11;
            this.textPort.Text = "21";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "端口：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "本地目录：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(476, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "远程目录：";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(359, 52);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(113, 21);
            this.btnDownload.TabIndex = 14;
            this.btnDownload.Text = "<--<--下载--<--<";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(240, 52);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(113, 21);
            this.btnUpload.TabIndex = 15;
            this.btnUpload.Text = ">-->--上传-->-->";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // textLocal
            // 
            this.textLocal.Location = new System.Drawing.Point(85, 53);
            this.textLocal.Name = "textLocal";
            this.textLocal.Size = new System.Drawing.Size(149, 21);
            this.textLocal.TabIndex = 17;
            this.textLocal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textLocal_KeyPress);
            // 
            // textRemote
            // 
            this.textRemote.Location = new System.Drawing.Point(547, 53);
            this.textRemote.Name = "textRemote";
            this.textRemote.Size = new System.Drawing.Size(151, 21);
            this.textRemote.TabIndex = 18;
            // 
            // textMsg
            // 
            this.textMsg.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textMsg.Location = new System.Drawing.Point(240, 79);
            this.textMsg.Multiline = true;
            this.textMsg.Name = "textMsg";
            this.textMsg.ReadOnly = true;
            this.textMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textMsg.Size = new System.Drawing.Size(232, 240);
            this.textMsg.TabIndex = 19;
            this.textMsg.TabStop = false;
            // 
            // FTPClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 329);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.textRemote);
            this.Controls.Add(this.textLocal);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listRemote);
            this.Controls.Add(this.listLocal);
            this.Controls.Add(this.textHost);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "FTPClientForm";
            this.Text = "FTP客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox textHost;
        private System.Windows.Forms.ListView listLocal;
        private System.Windows.Forms.ListView listRemote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textUser;
        private System.Windows.Forms.TextBox textPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox textLocal;
        private System.Windows.Forms.TextBox textRemote;
        private System.Windows.Forms.TextBox textMsg;
    }
}

