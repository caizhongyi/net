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
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Browser;

namespace DC.FileUpload
{
    public partial class FileUploadControl : UserControl
    {
        public struct FontValue
        {
            public  const string Upload = "上传";
            public  const string Clear = "取消";
            public  const string Select = "选择文件";
            public  const string Total = "文件总数";
            public  const string ExceededTotal = " 您超出文件的总允许的上载数量";
            public  const string ExceededTotalSize = " 您超出了总允许的总大小  ";
            public  const string ExceededSize = " 文件‘{0}’超出加载大小  ";
        }

        public int MaxConcurrentUploads { get; set; }
        public long UploadChunkSize { get; set; }
        public bool ResizeImage { get; set; }
        public int ImageSize { get; set; }
        private DateTime start;

        public Brush BackgroundColor
        {
            get { return controlBorder.Background; }
            set { controlBorder.Background = value; }
        }
        public CornerRadius CornerRadius
        {
            get { return controlBorder.CornerRadius; }
            set { controlBorder.CornerRadius = value; }
        }
        public Brush BorderBrushColor
        {
            get { return controlBorder.BorderBrush; }
            set { controlBorder.BorderBrush = value; }
        }
        public Thickness BorderThickness
        {
            get { return controlBorder.BorderThickness; }
            set { controlBorder.BorderThickness = value; }
        }

        public string Filter { get; set; }
        public bool Multiselect { get; set; }
        public Uri UploadUrl { get; set; }
        public string JavascriptCompleteFunction { get; set; }

        private bool uploading { get; set; }

        private long TotalUploadSize { get; set; }
        private long TotalUploaded { get; set; }

        public long MaximumTotalUpload { get; set; }
        public long MaximumUpload { get; set; }

        public int MaxNumberToUpload { get; set; }
        private int count = 0;

        public bool AllowThumbnail 
        {
            get { return displayThumbailChckBox.Visibility == Visibility.Visible; }
            set 
            {
                bool temp = value;
                if (temp) 
                    displayThumbailChckBox.Visibility = Visibility.Visible; 
                else 
                    displayThumbailChckBox.Visibility = Visibility.Collapsed; 
            }
        }

        private ScrollHelper helper;

        private ObservableCollection<FileUpload> files;


        public FileUploadControl()
        {
            InitializeComponent();
            files = new ObservableCollection<FileUpload>();
            files.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(files_CollectionChanged);
            MaxConcurrentUploads = 1;
            MaxNumberToUpload = -1;
            MaximumTotalUpload = MaximumUpload = -1;
            Filter = "All Files|*.*";
            Multiselect = true;
            uploading = false;
            UploadChunkSize = 4194304;

            addFilesButton.Click += new RoutedEventHandler(addFilesButton_Click);
            uploadButton.Click += new RoutedEventHandler(uploadButton_Click);
            clearFilesButton.Click += new RoutedEventHandler(clearFilesButton_Click);
         

            displayThumbailChckBox.Checked += new RoutedEventHandler(displayThumbailChckBox_Checked);
            displayThumbailChckBox.Unchecked += new RoutedEventHandler(displayThumbailChckBox_Checked);

            helper = new ScrollHelper(filesScrollViewer);

            Loaded += new RoutedEventHandler(FileUploadControl_Loaded);
        }

        void displayThumbailChckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkbox = sender as CheckBox;

            foreach (FileUpload fu in files)
            {
               
                if (fu.DisplayThumbnail != chkbox.IsChecked)
                    fu.DisplayThumbnail = (bool)chkbox.IsChecked;
            }
        }
        public FileUploadControl(Uri uploadUrl)
            : this()
        {
            UploadUrl = uploadUrl;
        }

        void files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            countTextBlock.Text = FontValue.Total +": " + files.Count.ToString();
            TotalUploadSize = files.Sum(f => f.FileLength);
            TotalUploaded = files.Sum(f => f.BytesUploaded);
            totalSizeTextBlock.Text = string.Format("{0} of {1}",
                new ByteConverter().Convert(TotalUploaded, this.GetType(), null, null).ToString(),
                new ByteConverter().Convert(TotalUploadSize, this.GetType(), null, null).ToString());
            progressBar.Maximum = TotalUploadSize;
            progressBar.Value = TotalUploaded;
        }

        void clearFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var q = files.Where(f => f.Status == FileUploadStatus.Uploading);
            foreach (FileUpload fu in q)
                fu.CancelUpload();
            timeLeftTextBlock.Text = "";
            files.Clear();
        }

        void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string)uploadButton.Content == FontValue.Upload)
            {
                uploadButton.Content = FontValue.Clear;
                start = DateTime.Now;
                UploadFiles();
            }
            else
            {
                var q = files.Where(f => f.Status == FileUploadStatus.Uploading);
                foreach (FileUpload fu in q)
                    fu.CancelUpload();
                uploading = false;
                uploadButton.Content = FontValue.Upload;
            }
        }

        void addFilesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Filter;
            dlg.Multiselect = Multiselect;

            if ((bool)dlg.ShowDialog())
            {
                foreach (FileInfo file in dlg.Files)
                {
                    FileUpload upload = new FileUpload(this.Dispatcher, UploadUrl, file);
                    if (UploadChunkSize > 0)
                        upload.ChunkSize = UploadChunkSize;
                    if (ResizeImage)
                    {
                        upload.ResizeImage = ResizeImage;
                        upload.ImageSize = ImageSize;
                    }

                    if (MaxNumberToUpload > -1)
                    {
                        count++;
                        if (count > MaxNumberToUpload)
                        {
                            MessageBox.Show(FontValue.ExceededTotal);
                            break;
                        }
                    }

                    if (MaximumTotalUpload >= 0 && TotalUploadSize + upload.FileLength > MaximumTotalUpload)
                    {
                        MessageBox.Show(FontValue.ExceededTotalSize);
                        break;
                    }
                    if (MaximumUpload >= 0 && upload.FileLength > MaximumUpload)
                    {
                        MessageBox.Show(string.Format(FontValue.ExceededSize, upload.Name));
                        continue;
                    }
                    upload.DisplayThumbnail = (bool)displayThumbailChckBox.IsChecked;
                    upload.StatusChanged += new EventHandler(upload_StatusChanged);
                    upload.UploadProgressChanged += new ProgressChangedEvent(upload_UploadProgressChanged);
                    upload.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(upload_PropertyChanged);
                    files.Add(upload);
                }
            }
        }

        void upload_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {            
            if (e.PropertyName == "FileLength")
            {
                files_CollectionChanged(null, null);
            }
        }

        void upload_UploadProgressChanged(object sender, UploadProgressChangedEventArgs args)
        {
            

            TotalUploaded += args.BytesUploaded;
            progressBar.Value = TotalUploaded;
            totalSizeTextBlock.Text = string.Format("{0} of {1}",
                 new ByteConverter().Convert(TotalUploaded, this.GetType(), null, null).ToString(),
                new ByteConverter().Convert(TotalUploadSize, this.GetType(), null, null).ToString());

            double ByteProcessTime = 0;
            double EstimatedTime = 0;

            try
            {
                TimeSpan timeSpan = DateTime.Now - start;
                ByteProcessTime = TotalUploaded / timeSpan.TotalSeconds;
                EstimatedTime = (TotalUploadSize - TotalUploaded) / ByteProcessTime;
                timeSpan = TimeSpan.FromSeconds(EstimatedTime);
                timeLeftTextBlock.Text = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            catch { }
        }

        void upload_StatusChanged(object sender, EventArgs e)
        {
            FileUpload fu = sender as FileUpload;
            if (fu.Status == FileUploadStatus.Complete)
            {
                if (uploading)
                    UploadFiles();
            }
            else if (fu.Status == FileUploadStatus.Removed)
            {
                files.Remove(fu);
                if (uploading)
                    UploadFiles();
            }
            else if (fu.Status == FileUploadStatus.Uploading && files.Count(f => f.Status == FileUploadStatus.Uploading) < MaxConcurrentUploads)
                UploadFiles();
        }

        void FileUploadControl_Loaded(object sender, RoutedEventArgs e)
        {
            fileList.ItemsSource = files;
          
        }

        private void UploadFiles()
        {

            uploading = true;
            while (files.Count(f => f.Status == FileUploadStatus.Uploading || f.Status == FileUploadStatus.Resizing) < MaxConcurrentUploads && uploading)
            {
                if (files.Count(f => f.Status != FileUploadStatus.Complete && f.Status != FileUploadStatus.Uploading && f.Status != FileUploadStatus.Resizing) > 0)
                {
                    FileUpload fu = files.First(f => f.Status != FileUploadStatus.Complete && f.Status != FileUploadStatus.Uploading && f.Status != FileUploadStatus.Resizing);
                    fu.Upload();
                }
                else if (files.Count(f => f.Status == FileUploadStatus.Uploading || f.Status == FileUploadStatus.Resizing) == 0)
                {
                    uploading = false;
                    uploadButton.Content = FontValue.Upload;
                    if (!string.IsNullOrEmpty(JavascriptCompleteFunction))
                    {
                        try
                        {
                            HtmlPage.Window.CreateInstance(JavascriptCompleteFunction);
                        }
                        catch { }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
