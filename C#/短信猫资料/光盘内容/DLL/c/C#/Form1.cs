using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices; 

namespace WindowsApplication1
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox MobPort;
		private System.Windows.Forms.Label State_Show;
		private System.Windows.Forms.Button Sms_Connection_Button;
		private System.Windows.Forms.Button Sms_Disconnection_Button;
		private System.Windows.Forms.TextBox SendSms_Text;
		private System.Windows.Forms.TextBox TelNum_Text;
		private System.Windows.Forms.Button Sms_Send_Button;
		private System.Windows.Forms.Label NewSms_Show;
		private System.Windows.Forms.Button Sms_Start_Button;
		private System.Windows.Forms.Button Sms_Close_Button;
		private System.Windows.Forms.TextBox ReceiveSms_Text;
		private System.Windows.Forms.Button Sms_Receive_Button;
		private System.Windows.Forms.TextBox DeleteSms_Index;
		private System.Windows.Forms.Button Sms_Delete_Button;
		private System.Windows.Forms.Button Sms_Exit_Button;
        private System.Windows.Forms.Timer NewSms_Timer;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.MobPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Sms_Disconnection_Button = new System.Windows.Forms.Button();
            this.Sms_Connection_Button = new System.Windows.Forms.Button();
            this.State_Show = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Sms_Send_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TelNum_Text = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SendSms_Text = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Sms_Close_Button = new System.Windows.Forms.Button();
            this.Sms_Start_Button = new System.Windows.Forms.Button();
            this.NewSms_Show = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Sms_Receive_Button = new System.Windows.Forms.Button();
            this.ReceiveSms_Text = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Sms_Delete_Button = new System.Windows.Forms.Button();
            this.DeleteSms_Index = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Sms_Exit_Button = new System.Windows.Forms.Button();
            this.NewSms_Timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号：";
            // 
            // MobPort
            // 
            this.MobPort.Location = new System.Drawing.Point(64, 8);
            this.MobPort.Name = "MobPort";
            this.MobPort.Size = new System.Drawing.Size(56, 21);
            this.MobPort.TabIndex = 1;
            this.MobPort.Text = "5";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "注：0为红外接口，1,2,3,...为串口";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sms_Disconnection_Button);
            this.groupBox1.Controls.Add(this.Sms_Connection_Button);
            this.groupBox1.Controls.Add(this.State_Show);
            this.groupBox1.Location = new System.Drawing.Point(16, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接GSM MODEM";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Sms_Disconnection_Button
            // 
            this.Sms_Disconnection_Button.Enabled = false;
            this.Sms_Disconnection_Button.Location = new System.Drawing.Point(144, 48);
            this.Sms_Disconnection_Button.Name = "Sms_Disconnection_Button";
            this.Sms_Disconnection_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Disconnection_Button.TabIndex = 2;
            this.Sms_Disconnection_Button.Text = "断开";
            this.Sms_Disconnection_Button.Click += new System.EventHandler(this.Sms_Disconnection_Button_Click);
            // 
            // Sms_Connection_Button
            // 
            this.Sms_Connection_Button.Location = new System.Drawing.Point(36, 48);
            this.Sms_Connection_Button.Name = "Sms_Connection_Button";
            this.Sms_Connection_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Connection_Button.TabIndex = 1;
            this.Sms_Connection_Button.Text = "连接";
            this.Sms_Connection_Button.Click += new System.EventHandler(this.Sms_Connection_Button_Click);
            // 
            // State_Show
            // 
            this.State_Show.BackColor = System.Drawing.SystemColors.Desktop;
            this.State_Show.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.State_Show.Location = new System.Drawing.Point(8, 16);
            this.State_Show.Name = "State_Show";
            this.State_Show.Size = new System.Drawing.Size(240, 24);
            this.State_Show.TabIndex = 0;
            this.State_Show.Text = "连接状态";
            this.State_Show.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Sms_Send_Button);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TelNum_Text);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.SendSms_Text);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(16, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 232);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送短信";
            // 
            // Sms_Send_Button
            // 
            this.Sms_Send_Button.Location = new System.Drawing.Point(72, 200);
            this.Sms_Send_Button.Name = "Sms_Send_Button";
            this.Sms_Send_Button.Size = new System.Drawing.Size(112, 24);
            this.Sms_Send_Button.TabIndex = 5;
            this.Sms_Send_Button.Text = "发送";
            this.Sms_Send_Button.Click += new System.EventHandler(this.Sms_Send_Button_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 32);
            this.label6.TabIndex = 4;
            this.label6.Text = "注：发送内容最多70个汉字或180个英文字母, 超长时自动分段发送。";
            // 
            // TelNum_Text
            // 
            this.TelNum_Text.Location = new System.Drawing.Point(8, 136);
            this.TelNum_Text.Name = "TelNum_Text";
            this.TelNum_Text.Size = new System.Drawing.Size(240, 21);
            this.TelNum_Text.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "手机号码：";
            // 
            // SendSms_Text
            // 
            this.SendSms_Text.Location = new System.Drawing.Point(8, 40);
            this.SendSms_Text.Multiline = true;
            this.SendSms_Text.Name = "SendSms_Text";
            this.SendSms_Text.Size = new System.Drawing.Size(240, 72);
            this.SendSms_Text.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "短信内容：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Sms_Close_Button);
            this.groupBox3.Controls.Add(this.Sms_Start_Button);
            this.groupBox3.Controls.Add(this.NewSms_Show);
            this.groupBox3.Location = new System.Drawing.Point(280, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 88);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "自动接收短信";
            // 
            // Sms_Close_Button
            // 
            this.Sms_Close_Button.Location = new System.Drawing.Point(170, 56);
            this.Sms_Close_Button.Name = "Sms_Close_Button";
            this.Sms_Close_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Close_Button.TabIndex = 2;
            this.Sms_Close_Button.Text = "关闭";
            this.Sms_Close_Button.Click += new System.EventHandler(this.button5_Click);
            // 
            // Sms_Start_Button
            // 
            this.Sms_Start_Button.Location = new System.Drawing.Point(34, 56);
            this.Sms_Start_Button.Name = "Sms_Start_Button";
            this.Sms_Start_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Start_Button.TabIndex = 1;
            this.Sms_Start_Button.Text = "启动";
            this.Sms_Start_Button.Click += new System.EventHandler(this.button4_Click);
            // 
            // NewSms_Show
            // 
            this.NewSms_Show.BackColor = System.Drawing.SystemColors.Desktop;
            this.NewSms_Show.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.NewSms_Show.Location = new System.Drawing.Point(8, 24);
            this.NewSms_Show.Name = "NewSms_Show";
            this.NewSms_Show.Size = new System.Drawing.Size(264, 24);
            this.NewSms_Show.TabIndex = 0;
            this.NewSms_Show.Text = "自动接收短信功能处于关闭状态";
            this.NewSms_Show.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Sms_Receive_Button);
            this.groupBox4.Controls.Add(this.ReceiveSms_Text);
            this.groupBox4.Location = new System.Drawing.Point(280, 112);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 208);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "接收短信";
            // 
            // Sms_Receive_Button
            // 
            this.Sms_Receive_Button.Location = new System.Drawing.Point(88, 176);
            this.Sms_Receive_Button.Name = "Sms_Receive_Button";
            this.Sms_Receive_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Receive_Button.TabIndex = 1;
            this.Sms_Receive_Button.Text = "接收";
            this.Sms_Receive_Button.Click += new System.EventHandler(this.Sms_Receive_Button_Click);
            // 
            // ReceiveSms_Text
            // 
            this.ReceiveSms_Text.Location = new System.Drawing.Point(8, 24);
            this.ReceiveSms_Text.Multiline = true;
            this.ReceiveSms_Text.Name = "ReceiveSms_Text";
            this.ReceiveSms_Text.Size = new System.Drawing.Size(264, 144);
            this.ReceiveSms_Text.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Sms_Delete_Button);
            this.groupBox5.Controls.Add(this.DeleteSms_Index);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(280, 328);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 48);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "删除短信";
            // 
            // Sms_Delete_Button
            // 
            this.Sms_Delete_Button.Location = new System.Drawing.Point(128, 16);
            this.Sms_Delete_Button.Name = "Sms_Delete_Button";
            this.Sms_Delete_Button.Size = new System.Drawing.Size(48, 24);
            this.Sms_Delete_Button.TabIndex = 2;
            this.Sms_Delete_Button.Text = "删除";
            this.Sms_Delete_Button.Click += new System.EventHandler(this.Sms_Delete_Button_Click);
            // 
            // DeleteSms_Index
            // 
            this.DeleteSms_Index.Location = new System.Drawing.Point(80, 16);
            this.DeleteSms_Index.Name = "DeleteSms_Index";
            this.DeleteSms_Index.Size = new System.Drawing.Size(40, 21);
            this.DeleteSms_Index.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "短信索引号：";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // Sms_Exit_Button
            // 
            this.Sms_Exit_Button.Location = new System.Drawing.Point(496, 336);
            this.Sms_Exit_Button.Name = "Sms_Exit_Button";
            this.Sms_Exit_Button.Size = new System.Drawing.Size(56, 32);
            this.Sms_Exit_Button.TabIndex = 9;
            this.Sms_Exit_Button.Text = "退出";
            this.Sms_Exit_Button.Click += new System.EventHandler(this.Sms_Exit_Button_Click);
            // 
            // NewSms_Timer
            // 
            this.NewSms_Timer.Interval = 1000;
            this.NewSms_Timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(576, 390);
            this.Controls.Add(this.Sms_Exit_Button);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MobPort);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "调用短信收发二次开发接口例程源码(C#版)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		/// 
		[STAThread]

		[DllImport("sms.dll", EntryPoint="Sms_Connection")] 
　　    public static extern uint Sms_Connection(string CopyRight,uint Com_Port,uint Com_BaudRate ,out string Mobile_Type,out string CopyRightToCOM); 

		[DllImport("sms.dll", EntryPoint="Sms_Disconnection")] 
　　    public static extern uint Sms_Disconnection(); 

		[DllImport("sms.dll", EntryPoint="Sms_Send")] 
　　    public static extern uint Sms_Send(string Sms_TelNum,string Sms_Text); 

		[DllImport("sms.dll", EntryPoint="Sms_Receive")] 
　　    public static extern uint Sms_Receive(string Sms_Type,out string Sms_Text); 

		[DllImport("sms.dll", EntryPoint="Sms_Delete")] 
　　    public static extern uint Sms_Delete(string Sms_Index); 

		[DllImport("sms.dll", EntryPoint="Sms_AutoFlag")] 
　　    public static extern uint Sms_AutoFlag(); 

		[DllImport("sms.dll", EntryPoint="Sms_NewFlag")] 
　　    public static extern uint Sms_NewFlag(); 

		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (Sms_AutoFlag()==1) 
			{
				NewSms_Show.Text = "未收到新短信";
				NewSms_Timer.Enabled = true;
		    }
            else
            {
				NewSms_Show.Text = "该短信猫不支持自动接收短信功能";
	         }

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			NewSms_Show.Text = "自动接收短信功能处于关闭状态";
            NewSms_Timer.Enabled = false;
		}

		private void label9_Click(object sender, System.EventArgs e)
		{
		
		}

		private void Sms_Connection_Button_Click(object sender, System.EventArgs e)
		{

           String TypeStr="";
		   String CopyRightToCOM="";
           String CopyRightStr = "//深圳市国爵电子有限公司,网址www.gprscat.com //";

			if( Sms_Connection(CopyRightStr,uint.Parse(MobPort.Text), 9600,out TypeStr,out CopyRightToCOM) == 1) ///5为串口号，0为红外接口，1,2,3,...为串口
			{
				State_Show.Text=TypeStr;
				Sms_Connection_Button.Enabled=false;
                Sms_Disconnection_Button.Enabled=true;

	      
			}
			else
			{
				State_Show.Text="连接失败！";
				Sms_Connection_Button.Enabled=true;
				Sms_Disconnection_Button.Enabled=false;
			}
			

		}

		private void Sms_Disconnection_Button_Click(object sender, System.EventArgs e)
		{
			Sms_Disconnection();
			Sms_Connection_Button.Enabled=true;
			Sms_Disconnection_Button.Enabled=false;
		}

		private void Sms_Send_Button_Click(object sender, System.EventArgs e)
		{
			if( Sms_Send(TelNum_Text.Text,SendSms_Text.Text) == 1)
			{
				MessageBox.Show ("发送成功!");
			}
			else
			{
				MessageBox.Show ("发送失败!");
			}
		}

		private void Sms_Receive_Button_Click(object sender, System.EventArgs e)
		{
			String ReceiveSmsStr="";
			if (Sms_Receive("4", out ReceiveSmsStr) == 1)
		   {
			ReceiveSms_Text.Text = ReceiveSmsStr;
		   }
			else
			{
				ReceiveSms_Text.Text = "读取短信失败";
			}
		}

		private void Sms_Delete_Button_Click(object sender, System.EventArgs e)
		{
			Sms_Delete (DeleteSms_Index.Text);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			  if (Sms_NewFlag() == 1)
			  {
                 NewSms_Show.Text = "收到新短信,请查收!";
		       }
		}

		private void Sms_Exit_Button_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}
	}
}
