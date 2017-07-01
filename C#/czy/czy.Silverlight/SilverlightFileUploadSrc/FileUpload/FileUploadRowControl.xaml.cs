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
using System.Windows.Media.Imaging;

namespace DC.FileUpload
{
    public partial class FileUploadRowControl : UserControl
    {
        private bool imageSet;
        private Storyboard collapseStoryboard;
        private Storyboard visibleStoryboard;
        public FileUploadRowControl()
        {
            imageSet = false;
            InitializeComponent();
            removeButton.Click += new RoutedEventHandler(removeButton_Click);
            Loaded += new RoutedEventHandler(FileUploadRowControl_Loaded);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(FileUploadRowControl_MouseLeftButtonDown);
        }

        void FileUploadRowControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FileUpload fu = DataContext as FileUpload;
            if (!fu.DisplayThumbnail)
                fu.DisplayThumbnail = true;
            else
                fu.DisplayThumbnail = false;
            
           
        }

        void FileUploadRowControl_Loaded(object sender, RoutedEventArgs e)
        {
            FileUpload fu = DataContext as FileUpload;
            fu.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(fu_PropertyChanged);
            LoadImage(fu);
           // collapseStoryboard=
        }

        void fu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            FileUpload fu = sender as FileUpload;
            if (e.PropertyName == "DisplayThumbnail")
            {                
                LoadImage(fu);
            }

            else if (e.PropertyName == "Status")
            {
                bool showtimeleft = false;
                switch (fu.Status)
                {
                    case FileUploadStatus.Pending:
                        VisualStateManager.GoToState(this, "Pending", true);
                        break;
                    case FileUploadStatus.Uploading:
                        VisualStateManager.GoToState(this, "Uploading", true);
                        break;
                    case FileUploadStatus.Complete:
                        VisualStateManager.GoToState(this, "Complete", true);
                        break;
                    case FileUploadStatus.Error:
                        VisualStateManager.GoToState(this, "Error", true);
                        break;
                    case FileUploadStatus.Canceled:
                        VisualStateManager.GoToState(this, "Pending", true);
                        break;
                    case FileUploadStatus.Removed:
                        VisualStateManager.GoToState(this, "Pending", true);
                        break;
                    case FileUploadStatus.Resizing:
                        VisualStateManager.GoToState(this, "Resizing", true);
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadImage(FileUpload fu)
        {

            if (fu != null && fu.DisplayThumbnail && (fu.Name.ToLower().EndsWith("jpg") || fu.Name.ToLower().EndsWith("png")))
            {
                if (!imageSet)
                {
                    BitmapImage imageSource = new BitmapImage();
                    try
                    {
                        imageSource.SetSource(fu.File.OpenRead());
                        imagePreview.Source = imageSource;
                        imageSet = true;
                        
                        imagePreview.Visibility = Visibility.Visible;
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                }
                else
                    imagePreview.Visibility = Visibility.Visible;
            }
            else
                imagePreview.Visibility = Visibility.Collapsed;
        }

        void removeButton_Click(object sender, RoutedEventArgs e)
        {
            FileUpload fu = DataContext as FileUpload;
            if (fu != null)
                fu.RemoveUpload();
        }
    }
}
