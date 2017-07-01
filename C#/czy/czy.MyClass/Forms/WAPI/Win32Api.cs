using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace czy.MyClass.COM.Win32Api
{

        /// <summary>
        /// 对动态加载的DLL, 提供Win32 API到.NET delegate的转换器
        /// </summary>
        [Description("Win32 API转换器")]
        public class ApiInvoker
        {
            public const string KERNEL32 = "kernel32.dll";

            [DllImport(KERNEL32, EntryPoint = "LoadLibraryA", SetLastError = false, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            private extern static IntPtr LoadLibrary(string path);

            [DllImport(KERNEL32, EntryPoint = "GetProcAddress", SetLastError = false, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

            [DllImport(KERNEL32, EntryPoint = "FreeLibrary", SetLastError = false, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            private extern static bool FreeLibrary(IntPtr lib);

            private IntPtr handle = IntPtr.Zero;

            public IntPtr Handle { get { return handle; } set { handle = value; } }

            /// <summary>
            /// 加载dll
            /// </summary>
            /// <param name="dll">dll路径</param>
            public void LoadApi(String dll)
            {
                handle = LoadLibrary(dll);
            }

            /// <summary>
            /// 卸载dll
            /// </summary>
            public void CloseApi()
            {
                if (handle != IntPtr.Zero)
                {
                    FreeLibrary(handle);
                }
            }

            /// <summary>
            /// 调用dll中的方法, 将要执行的函数转换为指定类型委托
            /// </summary>
            /// <param name="api">函数名</param>
            /// <param name="t">委托类型</param>
            /// <returns>委托</returns>
            public Delegate Invoke(string api, Type t)
            {
                IntPtr function = GetProcAddress(handle, api);
                return (Delegate)Marshal.GetDelegateForFunctionPointer(function, t);
            }
        }
    }

