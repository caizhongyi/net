using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PortalDemo
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        Page rootPage = new Page();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Consts.BaseUrl = e.InitParams["BaseUrl"];
            Consts.HostWidth = this.Host.Content.ActualWidth;
            this.RootVisual = rootPage;
            //当宿主环境的大小发生变化时，会重绘所有的区域
            this.Host.Content.Resized += new EventHandler(PaintAllArea);
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 重绘所有的区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaintAllArea(Object sender, EventArgs e)
        {
            try
            {
                Consts.HostWidth = this.Host.Content.ActualWidth;
                rootPage.BorderToolBar.Width = this.Host.Content.ActualWidth;
                rootPage.PaintAllArea();
            }
            catch
            {
            }
        }
    }
}
