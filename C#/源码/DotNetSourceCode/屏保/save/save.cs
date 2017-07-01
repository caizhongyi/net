	using System ;
	using System.Drawing ;
	using System.Collections ;
	using System.ComponentModel ;
	using System.Windows.Forms ;
	using System.Data ;
	public class ScreenSaver : Form
	{
		private System.ComponentModel.IContainer components ;
		private Timer timerSaver ;
		private Label lblMarquee ;
		private int  speed = 2 ;
		private string strMarqueeText = "用C＃制造的屏幕保护" ;
		private Font fontMarquee = new Font ( "Arial" , 20 , FontStyle.Bold ) ;        
		private Color colorMarquee = Color.BlueViolet  ;
		private int iDistance ;
		private int ixStart = 0 ;
		private int iyStart = 0 ;  
		public ScreenSaver ( )
		{            
			InitializeComponent ( ) ;
			lblMarquee.Font=fontMarquee ;
			lblMarquee.ForeColor=colorMarquee ;			
			Cursor.Hide  ( ) ;
		}
		/// 清理所有正在使用的资源。
		protected override void Dispose ( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null ) 
				{
					components.Dispose ( ) ;
				}
			}
			base.Dispose ( disposing ) ;
		}
		private void InitializeComponent ( )
		{
			components = new System.ComponentModel.Container ( ) ;
			timerSaver = new Timer ( components ) ;
			lblMarquee = new Label ( ) ;
			SuspendLayout ( ) ;
			timerSaver.Enabled = true ;
			timerSaver.Interval = 1 ;
			timerSaver.Tick += new System.EventHandler ( timerSaver_Tick ) ;
			lblMarquee.ForeColor = Color.White ;
			lblMarquee.Location = new Point ( 113 , 0 ) ;
			lblMarquee.Name = "lblMarquee" ;
			lblMarquee.Size = new Size ( 263 , 256 ) ;
			lblMarquee.TabIndex = 0 ;
			lblMarquee.Visible = false ;
			AutoScaleBaseSize = new Size ( 6 , 14 ) ;
			BackColor = Color.Black ;
			ClientSize = new Size ( 384 , 347 ) ;
			ControlBox = false ;
			this.Controls.Add ( lblMarquee) ;
			this.KeyPreview = true ;
			this.MaximizeBox = false ;
			this.MinimizeBox = false ;
			this.Name = "ScreenSaver" ;
			//窗体运行后无边界
			this.FormBorderStyle = FormBorderStyle.None ;
			//程序运行后不显示在任务栏上
			this.ShowInTaskbar = false ;
			//窗体运行后，最大化，充满整个屏幕
			this.WindowState = FormWindowState.Maximized ;
			this.StartPosition = FormStartPosition.Manual ;
			this.KeyDown += new KeyEventHandler ( Form1_KeyDown ) ;
			this.MouseDown += new MouseEventHandler ( Form1_MouseDown ) ;
			this.MouseMove += new MouseEventHandler ( Form1_MouseMove ) ;
			ResumeLayout ( false ) ;
		}
		protected void timerSaver_Tick ( object sender , System.EventArgs e )
		{			
			lblMarquee.Text = strMarqueeText ;					
			lblMarquee.Height = lblMarquee.Font.Height ;									
			lblMarquee.Width = 350 ;
			//得到计算机屏幕的工作区域
			Rectangle ssWorkArea = Screen.GetWorkingArea ( this ) ;
			lblMarquee.Location = new Point ( ssWorkArea.Width - iDistance ,
				lblMarquee.Location.Y ) ;
			//显示标签
			lblMarquee.Visible = true ;
			// 增加2个象素点,你可以通过修改speed的值来改变标签的移动速度
			iDistance += speed ;
			// 如果标签已经走出屏幕，则把标签的位置重定位到屏幕的右边
			if ( lblMarquee.Location.X <= -( lblMarquee.Width ) )
			{
				//Reset the distance to 0.
				iDistance = 0 ;
				//判断标签的位置是否在顶部，如果在，则重定位到中部
				if ( lblMarquee.Location.Y == 0)
				    lblMarquee.Location = new Point ( lblMarquee.Location.X , ( ssWorkArea.Height / 2 ) ) ;
				    //判断标签的位置是否在中部，如果在，则重定位到底部		
				    else if ( lblMarquee.Location.Y == ssWorkArea.Height / 2 )
				      lblMarquee.Location = new Point ( lblMarquee.Location.X , ssWorkArea.Height - lblMarquee.Height ) ;
				    //重定位到顶部				
				      else
				        lblMarquee.Location = new Point ( lblMarquee.Location.X , 0 ) ;
			}    
		}
		protected void Form1_MouseDown ( object sender , MouseEventArgs e )
		{
			Cursor .Show  ( ) ; 
			timerSaver.Enabled = false ;
			Application .Exit ( ) ;
		}

		protected void Form1_MouseMove ( object sender , MouseEventArgs e )
		{
			// 把鼠标刚刚开始移动的位置给记录下来
			if ( ixStart == 0 && iyStart == 0 )
			{
				ixStart = e.X ;
				iyStart = e.Y ;
				return ;
			}
			//判断自屏幕保护程序运行后，鼠标的位置是否变动
			else if ( e.X != ixStart || e.Y != iyStart )
			  {
			     Cursor .Show  ( ) ; 
			     timerSaver.Enabled = false ;
			     Application .Exit ( ) ;
			  };
		}
	protected void Form1_KeyDown ( object sender , KeyEventArgs e )
		{
			Cursor .Show  ( ) ; 
			timerSaver.Enabled = false ;
			Application .Exit ( ) ;
		}
		public static void Main (  ) //string [ ] args
		{
/*			if ( args.Length == 1 )
			{
				if (args [ 0 ].Substring ( 0 , 2 ).Equals ( "/c" ) )
				{
					MessageBox.Show("此屏幕保护没有可供设置的选项！","用C＃制造屏幕保护" ,
					MessageBoxButtons.OK ,MessageBoxIcon.Information ) ;
					Application.Exit ( ) ;
				}
				else if ( args [ 0 ] == "/a" )
				{
					MessageBox.Show("此屏幕保护没有可供设定口令的选项！","用C＃制造屏幕保护" ,
					MessageBoxButtons.OK ,MessageBoxIcon.Information ) ;
					Application.Exit ( ) ;
				}
			}			
				//对于其他的值，则运行屏幕保护
			else			     */
				Application.Run ( new ScreenSaver ( ) ) ;
		}
	}