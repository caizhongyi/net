using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Files
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "�ļ���ȫ·������ ";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(320, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "C:\\";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(232, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "����ļ���";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(8, 88);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(320, 136);
			this.richTextBox1.TabIndex = 3;
			this.richTextBox1.Text = "";
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.SynchronizingObject = this;
			this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
			this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
			this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
			this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(232, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "��ʼ����";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "�ļ�ϵͳ�仯����� ";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(336, 230);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "��ʾ�����ļ�ϵͳ�仯���";
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
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
		private void button1_Click_1(object sender, System.EventArgs e)
		{//����ļ���
			if(this.folderBrowserDialog1.ShowDialog()==DialogResult.OK)
			{
				if(this.folderBrowserDialog1.SelectedPath.Trim()!="")
				{
					this.textBox1.Text=this.folderBrowserDialog1.SelectedPath.Trim();
				}
			}		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{//��ʼ�����ļ�ϵͳ�仯���
			if(!System.IO.Directory.Exists(this.textBox1.Text))
			{
				MessageBox.Show("ѡ��Ĳ���һ���ļ���","��Ϣ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;			
			}
			this.fileSystemWatcher1.Path=this.textBox1.Text;	
			MessageBox.Show(this.textBox1.Text+"�Ѿ����ڱ�����״̬��","��Ϣ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
		
		}

		private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
		{//�����ı�
		   this.richTextBox1.Text+=e.FullPath.ToString()+"  �����ı䣡\n";
		}

		private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
		{//����
			this.richTextBox1.Text+="\n�ո�������"+e.FullPath.ToString();
		}

		private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{//ɾ��
			this.richTextBox1.Text+="\n�ո�ɾ����"+e.FullPath.ToString();
		}

		private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
		{//����
			this.richTextBox1.Text+="\n�ոո�����"+e.FullPath.ToString();
		}
	}
}
