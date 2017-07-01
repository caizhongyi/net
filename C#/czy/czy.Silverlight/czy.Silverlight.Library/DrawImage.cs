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
using System.Windows.Media.Imaging;
using System.IO;

namespace czy.Silverlight.Library
{
    public class DrawImage
    {
        public static WriteableBitmap DrawStringImage(UIElement element)
        {
            Image img = new Image();
            WriteableBitmap W = new WriteableBitmap(element, new TransformGroup());
            W.Render(element, new TransformGroup());
            return W;
        }
    }
}
