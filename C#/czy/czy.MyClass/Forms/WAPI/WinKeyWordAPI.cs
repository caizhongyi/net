using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices ;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace czy.MyClass.WAPI
{
    public sealed partial class WinKeyWordAPI
    {

        public delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        //private HookHandlerDelegate proc;
        private IntPtr hookID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 13;
        //private int nCode=1;
        private IntPtr wParam=IntPtr .Zero ;
        static int hKeyboardHook = 0;
        public   delegate int HookProc(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
        HookProc hookp;
       // nCode：根据MSDN，如果CallNextHookEx的结果值小于0，回调函数应该返回这个值。正常的键盘事件将返回0或大于0的nCode。
       //wParam：这个值表明发生了什么类型的事件：KeyDown或KeyUp，或者按下的键是否为一个系统键（左边还是右边的Alt键）。
       //IParam：存储键击精确信息的一个结构体，如按下键的键盘码。此结构声明在KeyboardHook中：
         
        public struct KBDLLHOOKSTRUCT
        { 
         public int vkCode;
         int scanCode;
         public int flags;
         int time;
         int dwExtraInfo;
        }

        /// <summary>
        /// 设置键盘钩子。
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        /// <summary>
        ///  取消键盘钩子。
        /// </summary>
        /// <param name="hhk">勾子ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)][return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        /// <summary>
        /// / Ø ：这个函数把键击消息传递给下一下监听键盘事件的应用程序。
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

        public WinKeyWordAPI()
        {
            //proc = new HookHandlerDelegate(HookCallback);
            //using (Process curProcess = Process.GetCurrentProcess())
            //using (ProcessModule curModule = curProcess.MainModule)
            //{
            //    //hookID = SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr .Zero, 0);
            //}
        }

        /// <summary>
        /// 委拖
        /// </summary>
        /// <param name="nCode">nCode代表钩子类型0为全局钩子1为线程钩子</param>
        /// <param name="wParam">KeyMSG</param>
        /// <param name="lParam"> </param>
        /// <returns></returns>
        public int HookCallback(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
        {
         //只过滤KeyDown事件中的wParam，否则代码会对每次键击执行两次，
         //分别是KeyDown和KeyUp。
         //WM_SYSKEYDOWN用于截取Alt键和其他键的组合。
         
         if (nCode >= 0 &&  (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
         {

            //返回一个伪值以截取键击。
             return 1;
         }
         //事件未被处理，把它传递给下一个程序。
         return 0;
        }

        //private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        //{
        //    if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
        //    {
        //        MessageBox.Show("clicked");
        //        return 1;
        //    }
        //    return 0;



        //}
        public struct KeyMSG
        {
            public int vkCode;//键符虚拟码 
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        } 

        public string GetKeyMessage()
        {
            try
            {
                //  hookp = new HookProc(HookCallback);
                // hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, hookp, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule), 0);
                KeyMSG keys = (KeyMSG)Marshal.PtrToStructure(wParam, typeof(KeyMSG));
                int keyCode = keys.vkCode;

                return keyCode.ToString();
            }
            catch { return string.Empty; }
        }



 
    }
}
