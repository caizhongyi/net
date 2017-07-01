using System;
using System.Runtime.InteropServices;
using czy.MyClass.Media;
using System.Drawing;


namespace czy.MyClass.Media
{
    /// 
    /// avicap 的摘要说明。
    /// 

     class Camera
    {
        // showVideo calls
        [DllImport("avicap32.dll")]
        public static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);
        [DllImport("User32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [DllImport("avicap32.dll")]
        public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);

        // Constants
        public const int WM_USER = 0x400;
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int SWP_NOMOVE = 0x2;
        public const int SWP_NOZORDER = 0x4;
        public const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;
        public const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
        public const int WM_CAP_SET_CALLBACK_FRAME = WM_USER + 5;
        public const int WM_CAP_SET_PREVIEW = WM_USER + 50;
        public const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;

        // Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct VIDEOHDR
        {
            [MarshalAs(UnmanagedType.I4)]
            public int lpData;
            [MarshalAs(UnmanagedType.I4)]
            public int dwBufferLength;
            [MarshalAs(UnmanagedType.I4)]
            public int dwBytesUsed;
            [MarshalAs(UnmanagedType.I4)]
            public int dwTimeCaptured;
            [MarshalAs(UnmanagedType.I4)]
            public int dwUser;
            [MarshalAs(UnmanagedType.I4)]
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dwReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSize;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biWidth;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biHeight;
            [MarshalAs(UnmanagedType.I2)]
            public short biPlanes;
            [MarshalAs(UnmanagedType.I2)]
            public short biBitCount;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biCompression;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSizeImage;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biXPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biYPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrUsed;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 40)]
            public BITMAPINFOHEADER bmiHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public Int32[] bmiColors;
        }

        public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);

        // Public methods
        public static object GetStructure(IntPtr ptr, ValueType structure)
        {
            return Marshal.PtrToStructure(ptr, structure.GetType());
        }

        public static object GetStructure(int ptr, ValueType structure)
        {
            return GetStructure(new IntPtr(ptr), structure);
        }

        public static void Copy(IntPtr ptr, byte[] data)
        {
            Marshal.Copy(ptr, data, 0, data.Length);
        }

        public static void Copy(int ptr, byte[] data)
        {
            Copy(new IntPtr(ptr), data);
        }

        public static int SizeOf(object structure)
        {
            return Marshal.SizeOf(structure);
        }
    }

    //Web Camera Class
    class WebCamera
    {
        // Constructur
        public WebCamera(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
        }

        // delegate for frame callback
        public delegate void RecievedFrameEventHandler(byte[] data);
        public event RecievedFrameEventHandler RecievedFrame;

        private IntPtr lwndC; // Holds the unmanaged handle of the control
        private IntPtr mControlPtr; // Holds the managed pointer of the control
        private int mWidth;
        private int mHeight;

        private Camera.FrameEventHandler mFrameEventHandler; // Delegate instance for the frame callback - must keep alive! gc should NOT collect it

        // Close the web camera
        public void CloseWebcam()
        {
            this.capDriverDisconnect(this.lwndC);
        }

        // start the web camera
        public void StartWebCam()
        {
            byte[] lpszName = new byte[100];
            byte[] lpszVer = new byte[100];

            Camera.capGetDriverDescriptionA(0, lpszName, 100, lpszVer, 100);
            this.lwndC = Camera.capCreateCaptureWindowA(lpszName, Camera.WS_VISIBLE + Camera.WS_CHILD, 0, 0, mWidth, mHeight, mControlPtr, 0);

            if (this.capDriverConnect(this.lwndC, 0))
            {
                this.capPreviewRate(this.lwndC, 66);
                this.capPreview(this.lwndC, true);
                Camera.BITMAPINFO bitmapinfo = new Camera.BITMAPINFO();
                bitmapinfo.bmiHeader.biSize = Camera.SizeOf(bitmapinfo.bmiHeader);
                bitmapinfo.bmiHeader.biWidth = 352;
                bitmapinfo.bmiHeader.biHeight = 288;
                bitmapinfo.bmiHeader.biPlanes = 1;
                bitmapinfo.bmiHeader.biBitCount = 24;
                this.capSetVideoFormat(this.lwndC, ref bitmapinfo, Camera.SizeOf(bitmapinfo));
                this.mFrameEventHandler = new Camera.FrameEventHandler(FrameCallBack);
                this.capSetCallbackOnFrame(this.lwndC, this.mFrameEventHandler);
                Camera.SetWindowPos(this.lwndC, 0, 0, 0, mWidth, mHeight, 6);
                
            }
        }

        // private functions
        private bool capDriverConnect(IntPtr lwnd, short i)
        {
            return Camera.SendMessage(lwnd, Camera.WM_CAP_DRIVER_CONNECT, i, 0);
        }

        private bool capDriverDisconnect(IntPtr lwnd)
        {
            return Camera.SendMessage(lwnd, Camera.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        private bool capPreview(IntPtr lwnd, bool f)
        {
            return Camera.SendMessage(lwnd, Camera.WM_CAP_SET_PREVIEW, f, 0);
        }

        private bool capPreviewRate(IntPtr lwnd, short wMS)
        {
            return Camera.SendMessage(lwnd, Camera.WM_CAP_SET_PREVIEWRATE, wMS, 0);
        }

        private bool capSetCallbackOnFrame(IntPtr lwnd, Camera.FrameEventHandler lpProc)
        {
            return Camera.SendMessage(lwnd, Camera.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
        }

        private bool capSetVideoFormat(IntPtr hCapWnd, ref Camera.BITMAPINFO BmpFormat, int CapFormatSize)
        {
            return Camera.SendMessage(hCapWnd, Camera.WM_CAP_SET_VIDEOFORMAT, CapFormatSize, ref BmpFormat);
        }

        private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
        {
            Camera.VIDEOHDR videoHeader = new Camera.VIDEOHDR();
            byte[] VideoData;
            videoHeader = (Camera.VIDEOHDR)Camera.GetStructure(lpVHdr, videoHeader);
            VideoData = new byte[videoHeader.dwBytesUsed];
            Camera.Copy(videoHeader.lpData, VideoData);
            if (this.RecievedFrame != null)
                this.RecievedFrame(VideoData);
        }
    }


    public class WinFromCamera
    {
        WebCamera wc;
        System.Windows.Forms.Panel _panel;

        public System.Windows.Forms.Panel Panel
        {
            get { return _panel; }
        }

        int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        int _height;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public WinFromCamera(int width, int height, System.Windows.Forms.Panel panelPreview)
        {
            this._width = width;
            this._height = height;
            this._panel = panelPreview;
            this._panel.Size = new Size(this._width, this._height);
        }

        public void start()
        {
            
            wc = new WebCamera(this._panel.Handle, this._panel.Width, this._panel.Height);
            wc.StartWebCam();
        }

        public void stop()
        {
            wc.CloseWebcam();
        }
    }

  
}


/*

具体调用如下：

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using webcam;

namespace webcam
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
  WebCamera wc;

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
   this.SuspendLayout();
   // 
   // b_play
   // 
   this.b_play.Location = new System.Drawing.Point(280, 368);
   this.b_play.Name = "b_play";
   this.b_play.TabIndex = 0;
   this.b_play.Text = "&Play";
   this.b_play.Click += new System.EventHandler(this.button1_Click);
   // 
   // panelPreview
   // 
   this.panelPreview.Location = new System.Drawing.Point(8, 8);
   this.panelPreview.Name = "panelPreview";
   this.panelPreview.Size = new System.Drawing.Size(344, 272);
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
   // Form1
   // 
   this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
   this.ClientSize = new System.Drawing.Size(464, 413);
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
   panelPreview.Size = new Size(330,330);
   wc = new WebCamera( panelPreview.Handle,panelPreview.Width,panelPreview.Height);
   wc.StartWebCam();
  }

  private void button1_Click(object sender, System.EventArgs e)
  {
   b_play.Enabled = false;
   b_stop.Enabled = true;
   panelPreview.Size = new Size(330,330);
   wc = new WebCamera( panelPreview.Handle,panelPreview.Width,panelPreview.Height);
   wc.StartWebCam();
  }

  private void b_stop_Click(object sender, System.EventArgs e)
  {
   b_play.Enabled = true;
   b_stop.Enabled = false;
   wc.CloseWebcam();
  }
 }
} 

/*
另外的一些资料：

Motion Detection Algorithms http://www.codeproject.com/cs/media/Motion_Detection.asp （英文）
Motion detection using web cam http://www.codeproject.com/cs/media/motion_detection_wc.asp （英文）
AVPhone Controls  http://www.banasoft.net/avphone3/avphone3.htm（英文）
Visual C#使用DirectX实现视频播放 http://www.chinaitpower.net/2006Aug/2006-08-19/212417.html (C#)
使用.NET实现视频播放 http://www.chinaitpower.net/A200507/2005-07-27/167331.html
嵌入式Web视频点播系统实现方法 http://www.java-asp.net/java/200504/t_14439.html(JAVA)

以下是FLASH方面的解决方案:
关于FMS视频在线录制的学习记录... http://pigz.cn/blog/article.asp?id=131
[教程]利用FMS做在线视频录制 http://www.cincn.com/article.asp?id=15&page=2
在线录制系统 http://blog.chinaunix.net/u/17508/showart.php?id=202778
WPF中的解决方案:(使用WPF解决视频的相关问题最方便!)
Building an Interactive 3D Video Player http://www.contentpresenter.com/contentpresenter_3DTools.wmv）(视频教程)
*/