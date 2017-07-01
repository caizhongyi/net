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
using System.Windows.Interactivity;
using System.ComponentModel;

namespace czy.Silverlight.Library
{
    public class Util
    {
        public static Point GetCanvasPoint(UIElement obj)
        {
            return new Point(Canvas.GetLeft(obj),Canvas.GetTop(obj));
        }
        public static ProjectionPoint GetCanvas3DPoint(UIElement obj)
        {
            return new ProjectionPoint(Canvas.GetLeft(obj), Canvas.GetTop(obj), Canvas.GetZIndex(obj));
        }

        public static void  AttachBehavior(Behavior behavior,DependencyObject o)
        {
            //Microsoft.Expression.Interactivity.Layout.MouseDragElementBehavior b=new Microsoft.Expression.Interactivity.Layout.MouseDragElementBehavior ();
            System.Windows.Interactivity.IAttachedObject ia = behavior;
            ia.Attach(o);
        }
  

    }
}
