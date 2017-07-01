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
using czy.Silverlight.Controls;

namespace czy.Silverlight.Controls
{
    public partial class ImageRotation3DControl : UserControl
    {
        public ImageRotation3DControl(double width,double height)
        {
            InitializeComponent();
            ImageRotation3D img3D = new ImageRotation3D(ImageContent);
            content.Width = width;
            content.Height = height;
            Canvas.SetLeft(ImageContent, width * 0.5);
            Canvas.SetTop(ImageContent, height * 0.5);

            
        }

    }


}
    
