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

namespace FileUpload
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(HtmlPage.Document.DocumentUri, "FileUpload.ashx");
            uploadControl.UploadUrl = uri;
            uploadControl.UploadChunkSize = 25000000;
            //uploadControl.ResizeImage = true;
            //uploadControl.ImageSize = 1024;
        }
    }
}
