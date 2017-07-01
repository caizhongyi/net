using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Files
{
	/// <summary>
	/// Form1 的摘要说明。
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
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
			this.label1.Text = "文件夹全路径名： ";
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
			this.button1.Text = "浏览文件夹";
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
			this.button2.Text = "开始监视";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "文件系统变化情况： ";
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
			this.Text = "演示监视文件系统变化情况";
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		private void button1_Click_1(object sender, System.EventArgs e)
		{//浏览文件夹
			if(this.folderBrowserDialog1.ShowDialog()==DialogResult.OK)
			{
				if(this.folderBrowserDialog1.SelectedPath.Trim()!="")
				{
					this.textBox1.Text=this.folderBrowserDialog1.SelectedPath.Trim();
				}
			}		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{//开始监视文件系统变化情况
			if(!System.IO.Directory.Exists(this.textBox1.Text))
			{
				MessageBox.Show("选择的不是一个文件夹","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;			
			}
			this.fileSystemWatcher1.Path=this.textBox1.Text;	
			MessageBox.Show(this.textBox1.Text+"已经处于被监视状态！","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
		
		}

		private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
		{//发生改变
		   this.richTextBox1.Text+=e.FullPath.ToString()+"  发生改变！\n";
		}

		private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
		{//新增
			this.richTextBox1.Text+="\n刚刚新增："+e.FullPath.ToString();
		}

		private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{//删除
			this.richTextBox1.Text+="\n刚刚删除："+e.FullPath.ToString();
		}

		private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
		{//更名
			this.richTextBox1.Text+="\n刚刚更名："+e.FullPath.ToString();
		}
	}
}
