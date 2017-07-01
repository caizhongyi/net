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
using System.Windows.Browser;
using DC.FileUpload;

namespace FileUpload
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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            long tempLong = 0;
            int tempInt = 0;
            bool tempBool = false;
            
            Uri uri = new Uri(HtmlPage.Document.DocumentUri, HttpUtility.UrlDecode(e.InitParams["UploadPage"]));
            FileUploadControl uploadControl = new FileUploadControl();

            uploadControl.UploadUrl = uri;

            if (e.InitParams.Keys.Contains("UploadChunkSize") && !string.IsNullOrEmpty(e.InitParams["UploadChunkSize"]))
            {

                if (long.TryParse(e.InitParams["UploadChunkSize"], out tempLong) && tempLong > 0)
                    uploadControl.UploadChunkSize = tempLong;
            }

            if (e.InitParams.Keys.Contains("MaximumTotalUpload") && !string.IsNullOrEmpty(e.InitParams["MaximumTotalUpload"]))
            {

                if (long.TryParse(e.InitParams["MaximumTotalUpload"], out tempLong))
                    uploadControl.MaximumTotalUpload = tempLong;
            }

            if (e.InitParams.Keys.Contains("MaximumUpload") && !string.IsNullOrEmpty(e.InitParams["MaximumUpload"]))
            {

                if (long.TryParse(e.InitParams["MaximumUpload"], out tempLong))
                    uploadControl.MaximumUpload = tempLong;
            }


            if (e.InitParams.Keys.Contains("MaxConcurrentUploads") && !string.IsNullOrEmpty(e.InitParams["MaxConcurrentUploads"]))
            {
                if(int.TryParse(e.InitParams["MaxConcurrentUploads"], out tempInt))
                    uploadControl.MaxConcurrentUploads = tempInt;
            }

            if (e.InitParams.Keys.Contains("MaxNumberToUpload") && !string.IsNullOrEmpty(e.InitParams["MaxNumberToUpload"]))
            {
                if (int.TryParse(e.InitParams["MaxNumberToUpload"], out tempInt))
                    uploadControl.MaxNumberToUpload = tempInt;
            }

            if (e.InitParams.Keys.Contains("ResizeImage") && !string.IsNullOrEmpty(e.InitParams["ResizeImage"]))
            {
                if (bool.TryParse(e.InitParams["ResizeImage"], out tempBool))
                    uploadControl.ResizeImage = tempBool;
            }

            if (e.InitParams.Keys.Contains("ImageSize") && !string.IsNullOrEmpty(e.InitParams["ImageSize"]))
            {
                if (int.TryParse(e.InitParams["ImageSize"], out tempInt))
                    uploadControl.ImageSize = tempInt;
            }

            if (e.InitParams.Keys.Contains("Multiselect") && !string.IsNullOrEmpty(e.InitParams["Multiselect"]))
            {
                if (bool.TryParse(e.InitParams["Multiselect"], out tempBool))
                    uploadControl.Multiselect = tempBool;
            }

            if (e.InitParams.Keys.Contains("Filter") && !string.IsNullOrEmpty(e.InitParams["Filter"]))
            {
                uploadControl.Filter = e.InitParams["Filter"];
            }

            if (e.InitParams.Keys.Contains("AllowThumbnail") && !string.IsNullOrEmpty(e.InitParams["AllowThumbnail"]))
            {
                if (bool.TryParse(e.InitParams["AllowThumbnail"], out tempBool))
                    uploadControl.AllowThumbnail = tempBool;
            }

            if (e.InitParams.Keys.Contains("JavascriptCompleteFunction") && !string.IsNullOrEmpty(e.InitParams["JavascriptCompleteFunction"]))
            {
                uploadControl.JavascriptCompleteFunction = e.InitParams["JavascriptCompleteFunction"];
            }

            this.RootVisual = uploadControl;
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
    }
}
