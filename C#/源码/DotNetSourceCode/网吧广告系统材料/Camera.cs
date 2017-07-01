using System;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DongFengHealth
{
	/// 
	/// avicap 的摘要说明。
	/// 
	public class showVideo
	{
		// showVideo calls
		[DllImport("avicap32.dll")] public static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
		[DllImport("avicap32.dll")] public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
		[DllImport("User32.dll")] public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam); 
		[DllImport("User32.dll")] public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam); 
		[DllImport("User32.dll")] public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam); 
		[DllImport("User32.dll")] public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);
		[DllImport("User32.dll")] public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
		[DllImport("avicap32.dll")]public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize );
 
		// Constants
　	　	public const int WM_USER = 0x400;
　	　	public const int WS_CHILD = 0x40000000;
　	　	public const int WS_VISIBLE = 0x10000000;
　	　	public const int WM_CAP_START = WM_USER;
　	　	public const int WM_CAP_STOP = WM_CAP_START + 68;
　	　	public const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
　　	public const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
　　	public const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
　　	public const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
　　	public const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
　　	public const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
　　	public const int WM_CAP_SEQUENCE_NOFILE =WM_CAP_START+ 63;
　　	public const int WM_CAP_SET_OVERLAY =WM_CAP_START+ 51; 
　　	public const int WM_CAP_SET_PREVIEW =WM_CAP_START+ 50; 
　　	public const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START +6;
　　	public const int WM_CAP_SET_CALLBACK_ERROR=WM_CAP_START +2;
　　	public const int WM_CAP_SET_CALLBACK_STATUSA= WM_CAP_START +3;
　　	public const int WM_CAP_SET_CALLBACK_FRAME= WM_CAP_START +5;
　　	public const int WM_CAP_SET_SCALE=WM_CAP_START+ 53;
　　	public const int WM_CAP_SET_PREVIEWRATE=WM_CAP_START+ 52; 
		public const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;  
		// Structures
		[StructLayout(LayoutKind.Sequential)] public struct VIDEOHDR
		{
			[MarshalAs(UnmanagedType.I4)] public int lpData;
			[MarshalAs(UnmanagedType.I4)] public int dwBufferLength;
			[MarshalAs(UnmanagedType.I4)] public int dwBytesUsed;
			[MarshalAs(UnmanagedType.I4)] public int dwTimeCaptured;
			[MarshalAs(UnmanagedType.I4)] public int dwUser;
			[MarshalAs(UnmanagedType.I4)] public int dwFlags;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)] public int[] dwReserved;
		}
 
		[StructLayout(LayoutKind.Sequential)] public struct BITMAPINFOHEADER
		{
			[MarshalAs(UnmanagedType.I4)] public Int32 biSize ;
			[MarshalAs(UnmanagedType.I4)] public Int32 biWidth ;
			[MarshalAs(UnmanagedType.I4)] public Int32 biHeight ;
			[MarshalAs(UnmanagedType.I2)] public short biPlanes;
			[MarshalAs(UnmanagedType.I2)] public short biBitCount ;
			[MarshalAs(UnmanagedType.I4)] public Int32 biCompression;
			[MarshalAs(UnmanagedType.I4)] public Int32 biSizeImage;
			[MarshalAs(UnmanagedType.I4)] public Int32 biXPelsPerMeter;
			[MarshalAs(UnmanagedType.I4)] public Int32 biYPelsPerMeter;
			[MarshalAs(UnmanagedType.I4)] public Int32 biClrUsed;
			[MarshalAs(UnmanagedType.I4)] public Int32 biClrImportant;
		} 
 
		[StructLayout(LayoutKind.Sequential)] public struct BITMAPINFO
		{
			[MarshalAs(UnmanagedType.Struct, SizeConst=40)] public BITMAPINFOHEADER bmiHeader;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=1024)] public Int32[] bmiColors;
		}
     
		public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);
     
		// Public methods
		public static object GetStructure(IntPtr ptr,ValueType structure)
		{
			return Marshal.PtrToStructure(ptr,structure.GetType());
		}
  
		public static object GetStructure(int ptr,ValueType structure)
		{
			return GetStructure(new IntPtr(ptr),structure);
		}
     
		public static void Copy(IntPtr ptr,byte[] data)
		{
			Marshal.Copy(ptr,data,0,data.Length);
		}
     
		public static void Copy(int ptr,byte[] data)
		{
			Copy(new IntPtr(ptr),data);
		}
     
		public static int SizeOf(object structure)
		{
			return Marshal.SizeOf(structure); 
		}
	}

	//Web Camera Class
	public class Camera
	{
		// Constructur
		public Camera(IntPtr handle, int width,int height)
		{
			mControlPtr = handle;
			mWidth = width;
			mHeight = height;
		}
     
		// delegate for frame callback
		public delegate void RecievedFrameEventHandler(byte[] data);
		public event RecievedFrameEventHandler RecievedFrame;
     
		public  IntPtr lwndC; // Holds the unmanaged handle of the control
		private IntPtr mControlPtr; // Holds the managed pointer of the control
		private int mWidth;
		private int mHeight;
     
		private showVideo.FrameEventHandler mFrameEventHandler; // Delegate instance for the frame callback - must keep alive! gc should NOT collect it
     
		// Close the web camera
		public void CloseCamera()
		{
			this.capDriverDisconnect(this.lwndC);
		}
     
		// start the web camera
		public void StartCamera()
		{
			byte[] lpszName = new byte[100];
			byte[] lpszVer = new byte[100];
          
			showVideo.capGetDriverDescriptionA(0, lpszName, 100,lpszVer, 100);
			this.lwndC = showVideo.capCreateCaptureWindowA(lpszName, showVideo.WS_VISIBLE + showVideo.WS_CHILD, 0, 0, mWidth, mHeight, mControlPtr, 0);
          
			if (this.capDriverConnect(this.lwndC, 0))
			{
				this.capPreviewRate(this.lwndC, 66);
				this.capPreview(this.lwndC, true);
				showVideo.BITMAPINFO bitmapinfo = new showVideo.BITMAPINFO(); 
				bitmapinfo.bmiHeader.biSize = showVideo.SizeOf(bitmapinfo.bmiHeader);
				bitmapinfo.bmiHeader.biWidth = 352;
				bitmapinfo.bmiHeader.biHeight = 288;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 24;
				this.capSetVideoFormat(this.lwndC, ref bitmapinfo, showVideo.SizeOf(bitmapinfo));
				this.mFrameEventHandler = new showVideo.FrameEventHandler(FrameCallBack);
				this.capSetCallbackOnFrame(this.lwndC, this.mFrameEventHandler);
				showVideo.SetWindowPos(this.lwndC, 0, 0, 0, mWidth , mHeight , 6);
			} 
		}
 
		public void bitMapToJPG(string bmpFileName,string jpgFileName)
		{
			System.Drawing.Image img;
			img=ReturnPhoto(bmpFileName);
			img.Save(jpgFileName,ImageFormat.Jpeg);
		}
		private Image ReturnPhoto(string bmpFileName)
		{
			System.IO.FileStream stream ;
			stream=File.OpenRead(bmpFileName);
			Bitmap bmp = new Bitmap(stream);
			System.Drawing.Image image = bmp;//得到原图

			//创建指定大小的图
			System.Drawing.Image newImage = image.GetThumbnailImage(bmp.Width, bmp.Height, null, new IntPtr());
			Graphics g=Graphics.FromImage(newImage);
			g.DrawImage(newImage,0,0, newImage.Width, newImage.Height); //将原图画到指定的图上
			g.Dispose();
			stream.Close();
			return newImage;
		}
		public void capImage(IntPtr lwnd,string path)//抓图
		{
			showVideo.BITMAPINFO bitmapinfo = new showVideo.BITMAPINFO();
			IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
			showVideo.SendMessage(lwnd,showVideo.WM_CAP_SAVEDIB,0,hBmp.ToInt32());

		}
		public void capScope(IntPtr lwnd,string path)// 录像,保存avi文件的路径
		{
			IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
			showVideo.SendMessage(lwnd,showVideo.WM_CAP_FILE_SET_CAPTURE_FILEA,0,hBmp.ToInt32());
			showVideo.SendMessage(lwnd, showVideo.WM_CAP_SEQUENCE, 0, 0);
		}
		public void stopCapScope(IntPtr lwnd)// 停止录像
		{
			showVideo.SendMessage(lwnd, showVideo.WM_CAP_STOP, 0, 0);
		}
		private bool capDriverConnect(IntPtr lwnd, short i)
		{
			return showVideo.SendMessage(lwnd, showVideo.WM_CAP_DRIVER_CONNECT, i, 0);
		}
 		private bool capDriverDisconnect(IntPtr lwnd)
		{
			return showVideo.SendMessage(lwnd, showVideo.WM_CAP_DRIVER_DISCONNECT, 0, 0);
		}
  		private bool capPreview(IntPtr lwnd, bool f)
		{
			return showVideo.SendMessage(lwnd, showVideo.WM_CAP_SET_PREVIEW , f, 0);
		}
 
		private bool capPreviewRate(IntPtr lwnd, short wMS)
		{
			return showVideo.SendMessage(lwnd, showVideo.WM_CAP_SET_PREVIEWRATE, wMS, 0);
		}
     
		private bool capSetCallbackOnFrame(IntPtr lwnd, showVideo.FrameEventHandler lpProc)
		{     
			return showVideo.SendMessage(lwnd, showVideo.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
		}
 		private bool capSetVideoFormat(IntPtr hCapWnd, ref showVideo.BITMAPINFO BmpFormat, int CapFormatSize)
		{
			return showVideo.SendMessage(hCapWnd, showVideo.WM_CAP_SET_VIDEOFORMAT, CapFormatSize, ref BmpFormat);
		}
 		private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
		{
			showVideo.VIDEOHDR videoHeader = new showVideo.VIDEOHDR();
			byte[] VideoData;
			videoHeader = (showVideo.VIDEOHDR)showVideo.GetStructure(lpVHdr,videoHeader);
			VideoData = new byte[videoHeader.dwBytesUsed];
			showVideo.Copy(videoHeader.lpData ,VideoData);
			if (this.RecievedFrame != null)		this.RecievedFrame (VideoData);
		}

	}

}




namespace DongFengHealth
{
	/// 
	/// Form1 的摘要说明。
	/// 
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panelPreview;
		private System.Windows.Forms.Button b_play;
		private System.Windows.Forms.Button b_stop;
		/// 
		/// 必需的设计器变量。
		/// 
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		Camera wc;

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

		/// 
		/// 清理所有正在使用的资源。
		/// 
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
		/// 
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// 
		private void InitializeComponent()
		{
			this.b_play = new System.Windows.Forms.Button();
			this.panelPreview = new System.Windows.Forms.Panel();
			this.b_stop = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// b_play
			// 
			this.b_play.Location = new System.Drawing.Point(276, 368);
			this.b_play.Name = "b_play";
			this.b_play.TabIndex = 0;
			this.b_play.Text = "&Play";
			this.b_play.Click += new System.EventHandler(this.button1_Click);
			// 
			// panelPreview
			// 
			this.panelPreview.Location = new System.Drawing.Point(52, 16);
			this.panelPreview.Name = "panelPreview";
			this.panelPreview.Size = new System.Drawing.Size(360, 260);
			this.panelPreview.TabIndex = 1;
			// 
			// b_stop
			// 
			this.b_stop.Enabled = false;
			this.b_stop.Location = new System.Drawing.Point(360, 368);
			this.b_stop.Name = "b_stop";
			this.b_stop.TabIndex = 2;
			this.b_stop.Text = "&Stop";
			this.b_stop.Click += new System.EventHandler(this.b_stop_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(192, 368);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "Grab";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(24, 368);
			this.button2.Name = "button2";
			this.button2.TabIndex = 4;
			this.button2.Text = "Record";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(108, 368);
			this.button3.Name = "button3";
			this.button3.TabIndex = 5;
			this.button3.Text = "Store";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(464, 413);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.b_stop);
			this.Controls.Add(this.panelPreview);
			this.Controls.Add(this.b_play);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "GoodView test Web Camera";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// 
		/// 应用程序的主入口点。
		/// 
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			b_play.Enabled = false;
			b_stop.Enabled = true;
			//panelPreview.Size = new Size(330,330);
			wc = new Camera( panelPreview.Handle,panelPreview.Width,panelPreview.Height);
			wc.StartCamera();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			b_play.Enabled = false;
			b_stop.Enabled = true;
			panelPreview.Size = new Size(330,330);
			wc = new Camera( panelPreview.Handle,panelPreview.Width,panelPreview.Height);
			wc.StartCamera();
		}

		private void b_stop_Click(object sender, System.EventArgs e)
		{
			b_play.Enabled = true;
			b_stop.Enabled = false;
			wc.CloseCamera();
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			wc.capImage(wc.lwndC, "d:\\xxxx.bmp");
			wc.bitMapToJPG("d:\\xxxx.bmp","d:\\xxxx.jpg");
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			wc.capScope(wc.lwndC,"d:\\xxxx.avi");
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			wc.stopCapScope(wc.lwndC);
		}
	}
}



