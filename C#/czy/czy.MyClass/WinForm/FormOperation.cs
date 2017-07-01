using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace czy.MyClass.WinForm
{
    public class FormOperation
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)] 
        public static extern IntPtr GetForegroundWindow(); //获得当前活动窗体的句柄 
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")] 
        public static extern bool SetForegroundWindow(IntPtr hWnd); 

        /// <summary>
        /// 设置于当前窗口为顶置
        /// </summary>
        public static void SetCurrentForeGroundWindow()
        {
        //调用 
        IntPtr handle=GetForegroundWindow();
        if (handle!= GetForegroundWindow()) //获取当前活动窗体 
        SetForegroundWindow(handle); //如果不是,强制把自己的设置为活动窗体 
        }

        #region 任务栏隐藏窗体,显示小图标
        /// <summary>
        /// 点击最小化
        /// </summary>
        /// <param name="form">form</param>
        /// <param name="notifyIcon1">小图标</param>
        public static void FormSizeChange(Form form, NotifyIcon notifyIcon1)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {

                form.Hide();

            } 

        }

        //protected override void OnFormClosing(FormClosingEventArgs e,Form form)
        //{

        //    if (!CloseTag)
        //    {

        //        form.WindowState = FormWindowState.Minimized;

        //        e.Cancel = true;

        //    }

        //    else

        //        e.Cancel = false;

        //    base.OnFormClosing(e);

        //} 

        /// <summary>
        /// 点击小图标
        /// </summary>
        /// <param name="form">form</param>
        /// <param name="notifyIcon1">小图标</param>
        public static void ClickNotifyIcon(Form form, NotifyIcon notifyIcon1)
        {
            form.Show();

            if (form.WindowState == FormWindowState.Minimized)

                form.WindowState = FormWindowState.Normal;

            else if (form.WindowState == FormWindowState.Normal)

                form.WindowState = FormWindowState.Minimized;

            form.Activate(); //激活窗体并为其赋予焦点 

        }
        /// <summary>
        /// notifyIcon加入contextMenu
        /// </summary>
        /// <param name="notifyIcon">notifyIcon</param>
        /// <param name="contextMenu">contextMenu</param>
        public static void AddContextMenu(NotifyIcon notifyIcon, ContextMenu contextMenu)
        {
            notifyIcon.ContextMenu = contextMenu;
        }
        #endregion

    }
}
