using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
namespace NetworkScan
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label getlocalipaddress;
		private System.Windows.Forms.Label getlocalname;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.Label label11;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.getlocalipaddress = new System.Windows.Forms.Label();
            this.getlocalname = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.getlocalipaddress);
            this.panel1.Controls.Add(this.getlocalname);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 510);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(368, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(184, 24);
            this.label11.TabIndex = 27;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(524, 139);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(32, 21);
            this.textBox8.TabIndex = 26;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(328, 139);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(32, 21);
            this.textBox7.TabIndex = 25;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(440, 139);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(72, 21);
            this.textBox6.TabIndex = 24;
            this.textBox6.Text = "192.168.1.";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(376, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "��ʼIP:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(245, 139);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(76, 21);
            this.textBox5.TabIndex = 22;
            this.textBox5.Text = "192.168.1.";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(189, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "��ʼIP:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(96, 139);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 24);
            this.button3.TabIndex = 20;
            this.button3.Text = "BeginScan";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(576, 344);
            this.panel2.TabIndex = 19;
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox4.Size = new System.Drawing.Size(576, 344);
            this.textBox4.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(24, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "����ɨ��";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(368, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "��ǰ����״̬��";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(528, 8);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(21, 21);
            this.textBox3.TabIndex = 16;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(304, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "IP-->Name";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(376, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 24);
            this.label5.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(216, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 24);
            this.button2.TabIndex = 13;
            this.button2.Text = "IP-->Name";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(112, 104);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(96, 21);
            this.textBox2.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "IP:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(314, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "IP:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "��������ƣ�";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(376, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 24);
            this.label1.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 72);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(96, 21);
            this.textBox1.TabIndex = 6;
            // 
            // getlocalipaddress
            // 
            this.getlocalipaddress.Location = new System.Drawing.Point(32, 40);
            this.getlocalipaddress.Name = "getlocalipaddress";
            this.getlocalipaddress.Size = new System.Drawing.Size(320, 24);
            this.getlocalipaddress.TabIndex = 5;
            this.getlocalipaddress.Text = "label1";
            // 
            // getlocalname
            // 
            this.getlocalname.Location = new System.Drawing.Point(32, 16);
            this.getlocalname.Name = "getlocalname";
            this.getlocalname.Size = new System.Drawing.Size(320, 24);
            this.getlocalname.TabIndex = 4;
            this.getlocalname.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Name-->IP";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(576, 510);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			timer1.Enabled=true;
			IPHostEntry myHost = new IPHostEntry();
			try
			{
				getlocalipaddress.Text="";
				//�õ�����������DNS��Ϣ
				myHost = Dns.GetHostByName(Dns.GetHostName());
				//��ʾ����������
				getlocalname.Text = "��ļ�������ƣ�"+myHost.HostName.ToString();
				//��ʾ����������IP��ַ��
				getlocalipaddress.Text="";
				for(int i=0; i<myHost.AddressList.Length;i++)
				{
					getlocalipaddress.Text="��������IP��ַ->"+myHost.AddressList[i].ToString();
					
				}
			}
			catch
			{
			}
			
			
		}
		private void GetLocalIP()
		{
			
		}
		

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			//name--->IP
			label1.Text="";
			try
			{
                
				IPHostEntry myDnsToIP = new IPHostEntry();
				//Dns.Resolve ����: �� DNS ���������Ե�ָ����Ĳ��ֱ�ʾ����ʽ�� //  IP ��ַ����Ϊ IPHostEntryʵ��
				myDnsToIP =Dns.Resolve(textBox1.Text.ToString());
				//��ʾ��������IP��ַ���б�
				for(int i=0;i<myDnsToIP.AddressList.Length;i++)
				{
					label1.Text+=myDnsToIP.AddressList[i].ToString();
				}
				
			}
			catch 
			{
				if (label1.Text.Trim()=="")
				{
					label1.Text="û���ҵ���ӦIP";
				}

			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//IP--->Name
			label5.Text="";
			try
			{
				IPAddress test1 = IPAddress.Parse(textBox2.Text.Trim());
				IPHostEntry iphe = Dns.GetHostByAddress(test1);
				label5.Text=iphe.HostName;
			}
			catch
			{
				if (label5.Text.Trim()=="")
				{
					label5.Text="û���ҵ���Ӧ�ļ��������";
				}
			}
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			label11.Text=DateTime.Now.Hour.ToString()+":"+ DateTime.Now.Minute.ToString()+":"+DateTime.Now.Second.ToString();
			if (textBox3.Text!="")
			{
				button3.Enabled=false;
			}
			else
			{
                button3.Enabled=true;
			}
			bool b_IsConnection=InternetCS.IsConnectedToInternet();
			if (b_IsConnection)
			{
				//
				textBox3.BackColor=Color.Red;//����״̬
			}
			else
			{
				textBox3.BackColor=Color.Yellow;  //������״̬
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
            
			int i_IpBegin=0;
			int i_IpEnd=0;
			try
			{
				i_IpBegin=int.Parse(textBox7.Text.Trim());
                i_IpEnd=int.Parse(textBox8.Text.Trim());
				if ((i_IpBegin<0)&&(i_IpEnd>255))
				{
                    MessageBox.Show("���������Χ");
					return;
				}
			}
			catch 
			{
				MessageBox.Show("�������");
				return;
			}

			textBox4.Text="";
			//Thread ��: �����������߳�
			Thread thScan = new Thread(new ThreadStart(ScanTarget));
			//Thread.Start ����:�����߳�
			thScan.Start();
           
			
		}
		private void ScanTarget()
		{
		    //����IP��ַ��31-8BIT λ��Ҳ���ǹ̶���IP��ַ��ǰ��
		    // numericUpDown1�Ƕ����System.Windows.Forms.NumericUpDown�ؼ�
			int i_IpBegin=int.Parse(textBox7.Text.Trim());
			int i_IpEnd=int.Parse(textBox8.Text.Trim());
            string strIPAddress=textBox5.Text;
			//ɨ��Ĳ���
			for(int i=i_IpBegin;i<=i_IpEnd;i++)
			{
				string strScanIPAdd = strIPAddress +i.ToString();
				//ת����IP��ַ
				IPAddress myScanIP = IPAddress.Parse(strScanIPAdd);
				textBox3.Text=i.ToString();
				try
				{    	
					//Dns.GetHostByAddress ����: ���� IP ��
					//ַ��ȡ DNS ������Ϣ��
					IPHostEntry myScanHost = Dns.GetHostByAddress(myScanIP);
					//��ȡ��������
					string strHostName =myScanHost.HostName.ToString();
                    textBox4.Text+=strScanIPAdd+"->"+strHostName+"\r\n";
				}
				catch //(Exception error)
				{
					//MessageBox.Show(error.Message);					
				}
			} 

            textBox3.Text="";  
            MessageBox.Show("ɨ�����");
		}

		private void Form1_Closed(object sender, System.EventArgs e)
		{
			Application.Exit(); 
		}

		private void textBox3_TextChanged(object sender, System.EventArgs e)
		{
		
		}
        
		private void ShowHideWindow(bool isShow)
		{
			if (isShow)
			{
				if (this.ShowInTaskbar==false)
				{
					this.ShowInTaskbar=true;
					this.Show();
					this.WindowState=FormWindowState.Normal;
				}
				else
				{
					if(this.WindowState == FormWindowState.Minimized)
					{
						this.WindowState = FormWindowState.Normal;
					}
				}
				this.Activate();
			}
			else
			{
				if (this.ShowInTaskbar==true)
				{
					this.Hide();
					this.ShowInTaskbar=false;
				}
			}
		}
	}
}
