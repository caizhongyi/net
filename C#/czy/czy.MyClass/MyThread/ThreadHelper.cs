using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Web.UI;

namespace czy.MyClass.MyThread
{
    /// <summary>
    /// 线程帮助类
    /// </summary>
    public sealed partial class ThreadHelper
    {
        #region 成员
        public delegate  void TimersDEvent(object obj, EventArgs e);
        //委托
        public delegate void SetWinCallback(ControlEvent myEvent, System.Windows.Forms.Control control, Form form,string param);
        public delegate void Callback(string s, System.Windows.Forms.Control control, Form form);
        public delegate void ControlEvent(System.Windows.Forms.Control control ,string param);

        private static object controlLock=new object ();
        #endregion

        #region 创建timer中的线程timer
        /// <summary>
        /// 创建timer中的线程timer
        /// </summary>
        /// <param name="interval">间隔时间(毫秒)</param>
        /// <param name="autoReset">是否循环</param>
        /// <param name="timer1_Tick">Timer事件名称</param>
        /// <returns>timer对像</returns>
        public static  System.Timers.Timer CreateTimer(double interval, bool autoReset, TimersDEvent timer1_Tick)
        {
     
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(new TimersDEvent(timer1_Tick));
            timer.AutoReset = autoReset;
            timer.Enabled = true;
            timer.Interval =interval;
            return timer;
        }
        #endregion

        #region WinForm控件线程访问
        /// <summary>
        /// WinForm控件线程访问
        /// </summary>
        /// <param name="s">附值字符窜</param>
        /// <param name="Control">控件</param>
        /// <param name="Form">窗体</param>
        public static  void SetText(string s, System.Windows.Forms.Control control, Form form)
        {
            //lock (control.GetType())
            //{
                if (control.InvokeRequired)
                {
                    Callback _myInvoke = new Callback(SetText);
                    form.Invoke(_myInvoke, new object[] { s, control, form });
                }
                else
                {
                    control.Text = s;
                }
            //}
        }
        #endregion

        #region WinForm控件线程访问
        /// <summary>
        /// WinForm控件线程访问
        /// </summary>
        /// <param name="controlEvent">附值字符窜</param>
        /// <param name="Control">控件</param>
        /// <param name="Form">窗体</param>
        /// <param name="Form">传入值</param>
        public static void ControlThread(ControlEvent controlEvent, System.Windows.Forms.Control control, Form form,string param)
        {
            //lock (controlLock)
            //{
                if (control.InvokeRequired)
                {
                    SetWinCallback _myInvoke = new SetWinCallback(ControlThread);
                    form.Invoke(_myInvoke, new object[] { controlEvent, control, form, param });
                }
                else
                {
                    controlEvent(control, param);
                }
            //}
        } 
        #endregion

    }
}
