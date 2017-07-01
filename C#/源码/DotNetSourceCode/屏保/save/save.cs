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
		private string strMarqueeText = "��C���������Ļ����" ;
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
		/// ������������ʹ�õ���Դ��
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
			//�������к��ޱ߽�
			this.FormBorderStyle = FormBorderStyle.None ;
			//�������к���ʾ����������
			this.ShowInTaskbar = false ;
			//�������к���󻯣�����������Ļ
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
			//�õ��������Ļ�Ĺ�������
			Rectangle ssWorkArea = Screen.GetWorkingArea ( this ) ;
			lblMarquee.Location = new Point ( ssWorkArea.Width - iDistance ,
				lblMarquee.Location.Y ) ;
			//��ʾ��ǩ
			lblMarquee.Visible = true ;
			// ����2�����ص�,�����ͨ���޸�speed��ֵ���ı��ǩ���ƶ��ٶ�
			iDistance += speed ;
			// �����ǩ�Ѿ��߳���Ļ����ѱ�ǩ��λ���ض�λ����Ļ���ұ�
			if ( lblMarquee.Location.X <= -( lblMarquee.Width ) )
			{
				//Reset the distance to 0.
				iDistance = 0 ;
				//�жϱ�ǩ��λ���Ƿ��ڶ���������ڣ����ض�λ���в�
				if ( lblMarquee.Location.Y == 0)
				    lblMarquee.Location = new Point ( lblMarquee.Location.X , ( ssWorkArea.Height / 2 ) ) ;
				    //�жϱ�ǩ��λ���Ƿ����в�������ڣ����ض�λ���ײ�		
				    else if ( lblMarquee.Location.Y == ssWorkArea.Height / 2 )
				      lblMarquee.Location = new Point ( lblMarquee.Location.X , ssWorkArea.Height - lblMarquee.Height ) ;
				    //�ض�λ������				
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
			// �����ոտ�ʼ�ƶ���λ�ø���¼����
			if ( ixStart == 0 && iyStart == 0 )
			{
				ixStart = e.X ;
				iyStart = e.Y ;
				return ;
			}
			//�ж�����Ļ�����������к�����λ���Ƿ�䶯
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
					MessageBox.Show("����Ļ����û�пɹ����õ�ѡ�","��C��������Ļ����" ,
					MessageBoxButtons.OK ,MessageBoxIcon.Information ) ;
					Application.Exit ( ) ;
				}
				else if ( args [ 0 ] == "/a" )
				{
					MessageBox.Show("����Ļ����û�пɹ��趨�����ѡ�","��C��������Ļ����" ,
					MessageBoxButtons.OK ,MessageBoxIcon.Information ) ;
					Application.Exit ( ) ;
				}
			}			
				//����������ֵ����������Ļ����
			else			     */
				Application.Run ( new ScreenSaver ( ) ) ;
		}
	}