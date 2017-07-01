using System.Runtime.InteropServices;    
using System.Drawing;    
using System.Drawing.Imaging;    
using System;   
     
namespace czy.MyClass.Media
{    
    /// <summary>    
    /// 一个控制摄像头的类    
    /// </summary>    
    public class CameraPick    
    {    
        private const int WM_USER = 0x400;    
        private const int WS_CHILD = 0x40000000;    
        private const int WS_VISIBLE = 0x10000000;    
        private const int WM_CAP_START = WM_USER;    
        private const int WM_CAP_STOP = WM_CAP_START + 68;    
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;    
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;    
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;    
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;    
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;    
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;    
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;    
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;    
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;    
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;    
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;    
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;    
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;    
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;    
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;    
        private const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;    
        private const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;    
        private const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;    
        private const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;    
   
        private IntPtr hWndC;    
        private bool bStat = false;    
   
        private IntPtr mControlPtr;    
        private int mWidth;    
        private int mHeight;    
        private int mLeft;    
        private int mTop;    
   
        /// <summary>    
        /// 初始化摄像头    
        /// </summary>    
        /// <param name="handle">控件的句柄</param>    
        /// <param name="left">开始显示的左边距</param>    
        /// <param name="top">开始显示的上边距</param>    
        /// <param name="width">要显示的宽度</param>    
        /// <param name="height">要显示的长度</param>    
        public CameraPick(IntPtr handle, int left, int top, int width, int height)    
        {    
            mControlPtr = handle;    
            mWidth = width;    
            mHeight = height;    
            mLeft = left;    
            mTop = top;    
        }    
   
        [DllImport("avicap32.dll")]    
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);    
   
        [DllImport("avicap32.dll")]    
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);    
        [DllImport("User32.dll")]    
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, long lParam);    
           
   
        public void capDlgVideoFormat()    
        {    
            Boolean capDlgVideoFormat = SendMessage(hWndC, WM_CAP_DLG_VIDEOFORMAT, 0, 0);    
   
        }    
        public void capDlgVideoSource()    
        {    
            Boolean capDlgVideoSource = SendMessage(hWndC, WM_CAP_DLG_VIDEOSOURCE, 0, 0);    
   
        }    
        public void capDlgVideoDisplay()    
        {    
            Boolean capDlgVideoDisplay = SendMessage(hWndC, WM_CAP_DLG_VIDEODISPLAY, 0, 0);    
        }    
        public void capDlgVideoCompression()    
        {    
            Boolean capDlgVideoCompression = SendMessage(hWndC, WM_CAP_DLG_VIDEOCOMPRESSION, 0, 0);    
               
        }    
   
        /// <summary>    
        /// 开始显示图像    
        /// </summary>    
        public void Start()    
        {    
            if (bStat)    
                return;    
   
            bStat = true;    
            byte[] lpszName = new byte[100];    
   
            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);    
   
            if (hWndC.ToInt32() != 0)    
            {    
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);    
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, 0, 0);    
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, 0, 0);    
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);    
                SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);    
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);    
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);    
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);    
            }    
   
            return;    
   
        }    
   
        /// <summary>    
        /// 停止显示    
        /// </summary>    
        public void Stop()    
        {    
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);    
            bStat = false;    
        }    
   
        /// <summary>    
        /// 抓图    
        /// </summary>    
        /// <param name="path">要保存bmp文件的路径</param>    
        public void GrabImage(string path)    
        {    
                
        IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);    
           SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt64());    
   
   
        }    
   
        /// <summary>    
        /// 录像    
        /// </summary>    
        /// <param name="path">要保存avi文件的路径</param>    
        public void Kinescope(string path)    
        {    
           IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);    
               
   
              
   
            SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, 0, hBmp.ToInt64());    
             
   
             SendMessage(hWndC, WM_CAP_SEQUENCE, 0, 0);    
                
        }    
   
         
   
        /// <summary>    
        /// 停止录像    
        /// </summary>    
        public void StopKinescope()    
        {    
            SendMessage(hWndC, WM_CAP_STOP, 0, 0);    
        }    
       /* public void cap()   
        {   
            CAPTUREPARMS s;   
     capCaptureGetSetup(m_caphwnd,&s,sizeof(CAPTUREPARMS));//取得采集参数   
     s.dwRequestMicroSecPerFrame = 33333;//采集一帧花费1/30秒   
    s.fAbortLeftMouse = FALSE;//压下鼠标左键不终止采集   
    s.fAbortRightMouse = FALSE;//压下鼠标右键不终止采集   
   s.fCaptureAudio = TRUE;//c采集音频   
   s.fYield = TRUE;//使用一个独立的线程来采集视频，不使用View窗口线程   
   capCaptureSetSetup(m_caphwnd,&s,sizeof(CAPTUREPARMS));//设定采集参数   
  
  
        }*/   
    }    
}    
   
/*
   //摄像头编程大全(源码)(c#) 
namespace czy.MyClass.Media
{
    /// <summary>    
    /// 一个控制摄像头的类    
    /// </summary>    
    public class PickHead
    {
        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_CAP_START + 68;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
        private const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
        private const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
        private const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_CAP_START + 20;
        private const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
        private const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
        private const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_CAP_START + 3;
        private const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
        private const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
        private IntPtr hWndC;
        private bool bStat = false;

        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;
        private int mLeft;
        private int mTop;

        /// <summary>    
        /// 初始化摄像头    
        /// </summary>    
        /// <param name="handle">控件的句柄</param>    
        /// <param name="left">开始显示的左边距</param>    
        /// <param name="top">开始显示的上边距</param>    
        /// <param name="width">要显示的宽度</param>    
        /// <param name="height">要显示的长度</param>    
        public PickHead(IntPtr handle, int left, int top, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
            mLeft = left;
            mTop = top;
        }

        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, long lParam);

        /// <summary>    
        /// 开始显示图像    
        /// </summary>    
        public void Start()
        {
            if (bStat)
                return;

            bStat = true;
            byte[] lpszName = new byte[100];

            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, mLeft, mTop, mWidth, mHeight, mControlPtr, 0);

            if (hWndC.ToInt32() != 0)
            {
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA, 0, 0);
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
            }

            return;


        }

        /// <summary>    
        /// 停止显示    
        /// </summary>    
        public void Stop()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
            bStat = false;
        }

        /// <summary>    
        /// 抓图    
        /// </summary>    
        /// <param name="path">要保存bmp文件的路径</param>    
        public void GrabImage(string path)
        {

            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt64());

        }

        /// <summary>    
        /// 录像    
        /// </summary>    
        /// <param name="path">要保存avi文件的路径</param>    
        public void Kinescope(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, 0, hBmp.ToInt64());
            SendMessage(hWndC, WM_CAP_SEQUENCE, 0, 0);
        }

        /// <summary>    
        /// 停止录像    
        /// </summary>    
        public void StopKinescope()
        {
            SendMessage(hWndC, WM_CAP_STOP, 0, 0);
        }

    }
}  
   */
//C#捕捉视频头（源码）        



namespace czy.MyClass.Media
{
    ///   <summary>      
    ///   VedioCapture   的摘要说明。      
    ///   </summary>      
    public class VedioCapture
    {
        private int hCaptureM;
        private bool isUnLoad = false;
        public VedioCapture()
        {
        }
        [DllImport("avicap32.dll")]
        private static extern int capCreateCaptureWindow(string strWindowName, int dwStyle, int x, int y, int width, int

height, int hwdParent, int nID);
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, string lParam);
        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(int hObject);
        public bool Initialize(System.Windows.Forms.Control aContainer, int intWidth, int intHeight)
        {
            hCaptureM = capCreateCaptureWindow("", 0x40000000 | 0x10000000, 0, 0, intWidth, intHeight, aContainer.Handle.ToInt32(), 1);
            if (hCaptureM == 0) return false;

            int ret = SendMessage(hCaptureM, 1034, 0, 0);
            if (ret == 0)
            {
                CloseHandle(hCaptureM);
                return false;
            }
            //WM_CAP_SET_PREVIEW      
            ret = SendMessage(hCaptureM, 1074, 1, 0);
            if (ret == 0)
            {
                this.UnLoad();
                return false;
            }
            //WM_CAP_SET_SCALE      
            ret = SendMessage(hCaptureM, 1077, 1, 0);
            if (ret == 0)
            {
                this.UnLoad();
                return false;
            }
            //WM_CAP_SET_PREVIEWRATE      
            ret = SendMessage(hCaptureM, 1076, 66, 0);
            if (ret == 0)
            {
                this.UnLoad();
                return false;
            }
            return true;
        }

        public void SingleFrameBegin()
        {
            //      
            int ret = SendMessage(hCaptureM, 1094, 0, 0);
        }
        public void SingleFrameEnd()
        {
            //      
            int ret = SendMessage(hCaptureM, 1095, 0, 0);
        }

        public void SingleFrameMode()
        {
            //WM_CAP_GRAB_FRAME      
            int ret = SendMessage(hCaptureM, 1084, 0, 0);
            //WM_CAP_SET_PREVIEW        
            //int   ret   =   SendMessage(     hCaptureM,   1074   ,   0,   0   );      
            //WM_CAP_SINGLE_FRAME      
            //ret   =   SendMessage(   hCaptureM,   1096   ,   0,   0   );      
        }
        public void PreviewMode()
        {
            int ret = SendMessage(hCaptureM, 1074, 1, 0);
        }

        public void UnLoad()
        {
            int ret = SendMessage(hCaptureM, 1035, 0, 0);
            CloseHandle(this.hCaptureM);
            isUnLoad = true;
        }

        public void CopyToClipBorad()
        {
            int ret = SendMessage(hCaptureM, 1054, 0, 0);
        }

        public void ShowFormatDialog()
        {
            int ret = SendMessage(hCaptureM, 1065, 0, 0);
        }
        public void SaveToDIB(string fileName)
        {
            int ret = SendMessage(hCaptureM, 1049, 0, fileName);
        }

        public void ShowDisplayDialog()
        {
            int ret = SendMessage(hCaptureM, 1067, 0, 0);
        }
        public System.Drawing.Image getCaptureImage()
        {
            System.Windows.Forms.IDataObject iData = System.Windows.Forms.Clipboard.GetDataObject();
            System.Drawing.Image retImage = null;
            if (iData != null)
            {
                if (iData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                {
                    retImage = (System.Drawing.Image)iData.GetData(System.Windows.Forms.DataFormats.Bitmap);
                }
                else if (iData.GetDataPresent(System.Windows.Forms.DataFormats.Dib))
                {
                    retImage = (System.Drawing.Image)iData.GetData(System.Windows.Forms.DataFormats.Dib);
                }
            }
            return retImage;
        }

        ~VedioCapture()
        {
            if (!isUnLoad)
            {
                this.UnLoad();
            }
        }
    }
}