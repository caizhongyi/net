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

namespace ImageRotatoin3D
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();

            // place the ImageRotation3D object to the center of the stage
            ImageRotation3D imageRotation3D = new ImageRotation3D();
            imageRotation3D.SetValue(Canvas.LeftProperty, this.Width / 2);
            imageRotation3D.SetValue(Canvas.TopProperty, this.Height / 2);
            LayoutRoot.Children.Add(imageRotation3D);

           
       
        }
    }
}
