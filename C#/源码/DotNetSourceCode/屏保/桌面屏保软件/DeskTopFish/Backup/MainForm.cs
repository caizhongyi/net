using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.Threading;
namespace DeskTopFish
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class MainForm :CustomForm.CustomForm //System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.Left=Screen.PrimaryScreen.Bounds.Right;
			int s=r.Next(1,3);
			bLeft=(Bitmap)Image.FromFile(Application.StartupPath+"\\"+s+"l.png");
			bRight=(Bitmap)Image.FromFile(Application.StartupPath+"\\"+s+"r.png");
			aa=(bLeft.Width/20);
			
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		public MainForm(int p)
		{
			InitializeComponent();
			this.Left=Screen.PrimaryScreen.Bounds.Right;
			bLeft=(Bitmap)Image.FromFile(Application.StartupPath+"\\"+p+"l.png");
			bRight=(Bitmap)Image.FromFile(Application.StartupPath+"\\"+p+"r.png");
			aa=(bLeft.Width/20);
		}

		
		private System.Windows.Forms.Timer tmrS;
		private System.Windows.Forms.Timer trmpic;
		private System.Windows.Forms.Timer timer;
		static Random r=new Random();
		Bitmap bLeft=null;
		Bitmap bRight=null;
		Bitmap[] lbit=new Bitmap[20];
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		Bitmap[] rbit=new Bitmap[20];

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
			this.tmrS = new System.Windows.Forms.Timer(this.components);
			this.trmpic = new System.Windows.Forms.Timer(this.components);
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			// 
			// tmrS
			// 
			this.tmrS.Enabled = true;
			this.tmrS.Interval = 20;
			this.tmrS.Tick += new System.EventHandler(this.tmrS_Tick);
			// 
			// trmpic
			// 
			this.trmpic.Enabled = true;
			this.trmpic.Interval = 50;
			this.trmpic.Tick += new System.EventHandler(this.trmpic_Tick);
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 6000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "退出";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(256, 134);
			this.ContextMenu = this.contextMenu1;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
			this.Load += new System.EventHandler(this.MainForm_Load);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			for(int i=0;i<5;i++)
			{
				Thread th=new Thread(new ThreadStart(run));
				th.Start();
			}
			Application.Run(new MainForm(3));
		}
		private static void run()
		{
			Application.Run(new MainForm(r.Next(1,4)));
		}
		int n=0;
		bool isGoLeft=true;
		int movePoint=2;
		int aa=0;
		private void tmrS_Tick(object sender, System.EventArgs e)
		{
			switch(t)
			{
				case 2:
					if(this.Top>0)
					{
						this.Top--;
					}
					else
					{
						t=3;
					}
					break;
				case 3:
					if(this.Top<Screen.PrimaryScreen.Bounds.Height)
					{
						this.Top++;
					}
					else
					{
						t=2;
					}
					break;
			}
			
			if(isGoLeft)
			{
				if(this.Left>=0-this.Width)
				{
					this.Left-=movePoint;
				}
				else
				{
					isGoLeft=false;
					//movePoint=2;
				}
			}
			else
			{
				if(this.Left<Screen.PrimaryScreen.Bounds.Right)
				{
					this.Left+=movePoint;
				}
				else
				{
					isGoLeft=true;
					//movePoint=2;
				}
			}
}
			

		private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
				movePoint=20;
				trmpic.Interval=10;
				timer.Start();
			}
		}

		private void trmpic_Tick(object sender, System.EventArgs e)
		{
			if(isGoLeft)
			{
				this.BackgroundImage=lbit[n];
			}
			else
			{
				this.BackgroundImage=rbit[n];
			}
			if((n+2)*aa>=this.bLeft.Width)
			{
				n=0;
			}
			else
			{
				n++;
			}
			
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			if(movePoint>2)
			{
				movePoint-=1;
			}
			else
			{
				trmpic.Interval=50;
				this.timer.Stop();
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			for(int i=0;i<20;i++)
			{
				lbit[i]=bLeft.Clone(new Rectangle((i)*aa,0,aa,bLeft.Height),PixelFormat.Format32bppArgb);
				rbit[i]=bRight.Clone(new Rectangle((i)*aa,0,aa,bRight.Height),PixelFormat.Format32bppArgb);
			}
		}
			int t=1;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			movePoint=r.Next(1,3);
			t=r.Next(0,5);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
