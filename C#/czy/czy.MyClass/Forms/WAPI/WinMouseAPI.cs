using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;

namespace czy.MyClass.WAPI
{
    public sealed partial class WinMouseAPI
    {
        delegate  void  MouseEvent(object sender,EventArgs e);
        //MouseEvent MouseMoveEvent;
        internal const byte SM_MOUSEPRESENT = 19;
        internal const byte SM_CMOUSEBUTTONS = 43;
        internal const byte SM_MOUSEWHEELPRESENT = 75;
        const int WM_SYSTEMDOWN = 0x104;//系统功能按键
        const int WM_KEYDOWN = 0x100;//普通按键

        public  struct POINTAPI
        {
            public int x;
            public int y;
        }

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
        }

        //交换按键
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SwapMouseButton")]
        public  extern static int SwapMouseButton(int bSwap);
        //点击鼠标
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "ClipCursor")]
        public extern static int ClipCursor(ref   RECT lpRect);
        /// <summary>
        ///获取鼠标座标
        /// </summary>
        /// <param name="lpPoint">POINTAPI</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public extern static int GetCursorPos(ref   POINTAPI lpPoint);
        /// <summary>
        ///   //显示鼠标
        /// </summary>
        /// <param name="bShow">true或flase</param>
        /// <returns>true为显示,false为隐藏</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ShowCursor")]
        public extern static bool ShowCursor(bool bShow);
        //设置是否可点击
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "EnableWindow")]
        public extern static int EnableWindow(int hwnd, int fEnable);
        //获取鼠标方块座标
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public extern static int GetWindowRect(int hwnd, ref   RECT lpRect);
        //设置当前鼠标座标
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public extern static int SetCursorPos(int x, int y);
        //获取当前鼠标度量
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public extern static int GetSystemMetrics(int nIndex);
        /// <summary>
        /// 获取设置双击间隔时间
        /// </summary>
        /// <param name="wCount"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetDoubleClickTime")]
        public extern static int SetDoubleClickTime(int wCount);
        /// <summary>
        /// 获取双击间隔时间
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public extern static int GetDoubleClickTime();
        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="dwMilliseconds">等待时间(单位秒)</param>
        [System.Runtime.InteropServices.DllImport("kernel32.DLL", EntryPoint = "Sleep")]
        public extern static void Sleep(int dwMilliseconds);

        /// <summary>
        ///鼠标事件
        /// </summary>
        /// <param name="dwFlags">MouseEventFlags类型 例:int(MouseEventFlags.LeftDown | MouseEventFlags.Absolute)</param>
        /// <param name="dx">默认为0</param>
        /// <param name="dy">默认为0</param>
        /// <param name="dwData">默认为0</param>
        /// <param name="dwExtraInfo">IntPtr.Zero</param>
        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

        //得到鼠标相对与全屏的坐标，不是相对与你的Form的，且与你的分辨率有关系 

        public static int FullScreenPosition_X
        {
            get
            {
                POINTAPI _POINTAPI = new POINTAPI();

                GetCursorPos(ref   _POINTAPI);

                return _POINTAPI.x;
            }
        }

        public static int FullScreenPosition_Y
        {
            get
            {
                POINTAPI _POINTAPI = new POINTAPI();

                GetCursorPos(ref   _POINTAPI);

                return _POINTAPI.y;
            }
        }

        //隐藏 显示 鼠标 
        public static void Hide()
        {
            ShowCursor(false);
        }

        public static void Show()
        {
            ShowCursor(true);
        }

        //将鼠标锁定在你的Form里   不过你得将你的Form先锁了,Form   Resize   就失效了 
        public static void Lock(System.Windows.Forms.Form ObjectForm)
        {
            RECT _FormRect = new RECT();

            GetWindowRect(ObjectForm.Handle.ToInt32(), ref   _FormRect);

            ClipCursor(ref   _FormRect);
        }

        public static void UnLock()
        {
            RECT _ScreenRect = new RECT();

            _ScreenRect.top = 0;
            _ScreenRect.left = 0;
            _ScreenRect.bottom = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom;
            _ScreenRect.right = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right;

            ClipCursor(ref   _ScreenRect);
        }

        //鼠标失效,不过失效的好像不只是鼠标,小心哦 
        public static void Disable(System.Windows.Forms.Form ObjectForm)
        {
            EnableWindow(ObjectForm.Handle.ToInt32(), 0);
        }

        public static void Enable(System.Windows.Forms.Form ObjectForm)
        {
            EnableWindow(ObjectForm.Handle.ToInt32(), 1);
        }
        //   得到你的鼠标类型 
        public static string Type
        {
            get
            {
                if (GetSystemMetrics(SM_MOUSEPRESENT) == 0)
                {
                    return "本计算机尚未安装鼠标";
                }
                else
                {
                    if (GetSystemMetrics(SM_MOUSEWHEELPRESENT) != 0)
                    {
                        return GetSystemMetrics(SM_CMOUSEBUTTONS) + "键滚轮鼠标";
                    }
                    else
                    {
                        return GetSystemMetrics(SM_CMOUSEBUTTONS) + "键鼠标";
                    }
                }
            }
        }

        //   设置鼠标双击时间
        public static void DoubleClickTime_Set(int MouseDoubleClickTime)
        {
            SetDoubleClickTime(MouseDoubleClickTime);
        }

        public static string DoubleClickTime_Get()
        {
            return GetDoubleClickTime().ToString();
        }

        //设置鼠标默认主键   一般都习惯用右手用鼠标 
        public static void DefaultRightButton()
        {
            SwapMouseButton(1);
        }

        public static void DefaultLeftButton()
        {
            SwapMouseButton(0);
         }

        
/// //////////////////////////////////////////////////////////////////////

        }
}


