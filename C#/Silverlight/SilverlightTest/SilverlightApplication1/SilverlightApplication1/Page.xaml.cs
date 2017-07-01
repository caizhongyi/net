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

namespace SilverlightApplication1
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();



            //image.Source = new BitmapImage(new Uri("fg1.jpg", UriKind.Relative));
            //LayoutRoot.Children.Add(myImage3);
           

          
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mystoryboard.Begin();
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mystoryboard.Begin();
        }
    }
}
