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
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
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
			this.button1 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "�ļ������� ";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(280, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "C:\\";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(5, 144);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "����ļ���";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox1
			// 
			this.listBox1.HorizontalScrollbar = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(88, 40);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(280, 148);
			this.listBox1.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "�ļ��б� ";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(5, 168);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "��ʾ�ļ�";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(376, 198);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "��ʾָ���ļ����µ������ļ�";
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

		private void button1_Click(object sender, System.EventArgs e)
		{//����ļ���
			this.folderBrowserDialog1.ShowDialog();
			if(this.folderBrowserDialog1.SelectedPath.Trim()!="")
				this.textBox1.Text=this.folderBrowserDialog1.SelectedPath.Trim();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{//��ʾָ���ļ����µ��ļ�
			if(this.textBox1.Text.Trim()=="")
				return;
			this.listBox1.Items.Clear();
	 	    string[] MyFiles=System.IO.Directory.GetFiles(this.textBox1.Text);
            //this.listBox1.Items.AddRange(MyFiles);
            foreach (string File in MyFiles)
            {
                string name = File.Substring(35).ToString();
                this.listBox1.Items.Add(name);
            }
		}
	}
}
