using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Notify
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem5,
																					  this.menuItem3,
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem4});
			this.menuItem1.Text = "视图(&V)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "放置到系统托盘(&N)";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "恢复正常显示(&R)";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 3;
			this.menuItem6.Text = "-";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "托盘和窗体同时显示(&T)";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "-";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 6;
			this.menuItem4.Text = "关闭(&C)";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "这是一个演示托盘的实例程序";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(336, 241);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(336, 241);
			this.Controls.Add(this.pictureBox1);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "演示如何使用托盘控件";
			this.Load += new System.EventHandler(this.Form1_Load);
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

		
		private void menuItem4_Click(object sender, System.EventArgs e)
		{//关闭应用程序
			this.notifyIcon1.Visible=false;
			this.Close();
			Application.Exit();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{//显示托盘图标
		  this.Visible=false;
		  this.notifyIcon1.Visible=true;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{//显示主窗体
			this.Visible=true;
			this.notifyIcon1.Visible=false;		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{//复制主菜单的菜单项到上下文菜单
			//复制到“放置到系统托盘(N)”菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem2.CloneMenu());
			//复制分隔线菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem5.CloneMenu());	
			//复制到“恢复正常显示(R)”菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem3.CloneMenu());
			//复制分隔线菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem6.CloneMenu());
			//复制到“关闭(C)”菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem4.CloneMenu());	
			//复制分隔线菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem8.CloneMenu());	
			//复制到“托盘和窗体同时显示(T)”菜单项
			this.contextMenu1.MenuItems.Add(this.menuItem7.CloneMenu());	
			//this.contextMenu1.MergeMenu(this.mainMenu1.);
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{//托盘和窗体同时显示
			this.Visible=true;
			this.notifyIcon1.Visible=true;				
		}
	}
}
