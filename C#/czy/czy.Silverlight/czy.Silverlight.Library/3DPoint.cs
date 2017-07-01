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

namespace czy.Silverlight.Library
{
    public class ProjectionPoint
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public ProjectionPoint(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;

        }
        public ProjectionPoint()
        {

        }
    }
}
