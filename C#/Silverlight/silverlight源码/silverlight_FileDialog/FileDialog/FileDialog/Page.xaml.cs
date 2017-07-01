using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace FileDialog
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        void OnClick(object sender, EventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Jpeg Files (*.jpg)|*.jpg|All Files(*.*)|*.*",
                EnableMultipleSelection = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                myList.DataContext = openFileDialog.SelectedFiles;
            }
        }



        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((e.AddedItems != null) && (e.AddedItems.Count > 0))
            {
                FileDialogFileInfo fi = e.AddedItems[0] as FileDialogFileInfo;

                if (fi != null)
                {
                    using (Stream stream = fi.OpenRead())
                    {
                        BitmapImage image = new BitmapImage();
                        image.SetSource(stream);
                        myImage.Source = image;
                        myImage.Visibility = Visibility.Visible;
                        stream.Close();
                    }
                }
            }
        }
    }
}
