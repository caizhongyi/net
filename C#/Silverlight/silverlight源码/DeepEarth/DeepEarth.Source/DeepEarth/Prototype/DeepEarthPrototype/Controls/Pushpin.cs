using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DeepEarth.Geometry;

namespace DeepEarthPrototype.Controls
{
    public delegate void PushPinSelectedHandler(Point location, Size pushPinSize, bool isSelected, PointBase pushPin);


    public class Pushpin : PointBase
    {
        public event PushPinSelectedHandler PushPinSelected;
        public string Description { get; set; }
        public bool IsSelected { get; set; }

        //public Pushpin(GeometryLayer layer) : base(layer)
        public Pushpin()
        {
            Style = Application.Current.Resources["PushpinStyle"] as Style;
            MouseLeftButtonDown += Pushpin_MouseLeftButtonDown;
        }


        private void Pushpin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PushPinSelected != null)
                PushPinSelected(Point, _OriginalSize, IsSelected, this);

            IsSelected = !IsSelected;
        }

    }
}



//#region IInfoBoxShape Members

//public object InfoBoxContent { get; set; }

//public Point? InfoBoxOffset { get; set; }

//#endregion

//public override void UpdateLocation()
//{
//    if (InView)
//    {
//        var scaly = RenderTransform as ScaleTransform;

//        if (scaly != null)
//        {
//            // power law
//            double zoomFactor = Math.Log(1/(MapInstance.MapViewLogicalSize.Width), 2);
//            double w = Math.Pow(0.02*(zoomFactor + 1), 2) + 0.01;

//            scaly.ScaleX = w*_OriginalSize.Width;
//            scaly.ScaleY = w*_OriginalSize.Height;
//        }

//        Point calculatedAnchor;
//        if (AnchorPoint == new Point())
//        {
//            calculatedAnchor = new Point((Width/2), (Height/2));
//        }
//        else
//        {
//            calculatedAnchor = new Point(
//                Width*AnchorPoint.X,
//                Height*AnchorPoint.Y
//                );
//        }

//        Point pixel = MapInstance.CoordHelper.GeoToPixel(Point);
//        bool isAnchorValid = (calculatedAnchor.X > 0 && calculatedAnchor.Y > 0);
//        if (isAnchorValid == false)
//        {
//            Canvas.SetLeft(this, pixel.X);
//            Canvas.SetTop(this, pixel.Y);
//        }
//        else
//        {
//            Canvas.SetLeft(this, pixel.X - calculatedAnchor.X);
//            Canvas.SetTop(this, pixel.Y - calculatedAnchor.Y);
//        }
//    }
//}