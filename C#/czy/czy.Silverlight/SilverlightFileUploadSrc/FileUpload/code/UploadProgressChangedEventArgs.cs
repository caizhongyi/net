using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DC.FileUpload
{
    public delegate void ProgressChangedEvent(object sender, UploadProgressChangedEventArgs args);
    public class UploadProgressChangedEventArgs
    {
        public int ProgressPercentage { get; set; }
        public long BytesUploaded { get; set; }
        public long TotalBytesUploaded { get; set; }
        public long TotalBytes { get; set; }
        public string FileName { get; set; }

        public UploadProgressChangedEventArgs() { }

        public UploadProgressChangedEventArgs(int progressPercentage, long bytesUploaded, long totalBytesUploaded, long totalBytes, string fileName)
        {
            ProgressPercentage = progressPercentage;
            BytesUploaded = bytesUploaded;
            TotalBytes = totalBytes;
            FileName = fileName;
            TotalBytesUploaded = totalBytesUploaded;
        }
    }
}
