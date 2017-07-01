using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


public static class UElementExtender
{
   
    public static void DragDrop(this UIElement el, Canvas cav)
    {
        bool IsMouseDown = false;
        Point mousePoint=new Point ();
        object mouseCtrl = null;
        UIElement _element = el;
        Canvas _canvas = cav;
        _element.MouseDown += (sender, e) =>
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsMouseDown = true;
                mousePoint = e.GetPosition(_canvas);
                mouseCtrl = sender;
            }
        };
        _element.MouseMove += (sender, e) =>
        {
            if (IsMouseDown)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(_canvas);
                    Canvas.SetLeft((UIElement)mouseCtrl, theMousePoint.X - (mousePoint.X - Canvas.GetLeft(((UIElement)mouseCtrl))));
                    Canvas.SetTop((UIElement)mouseCtrl, theMousePoint.Y - (mousePoint.Y - Canvas.GetTop(((UIElement)mouseCtrl))));

                    mousePoint = theMousePoint;
                }
            }
        };
        _element.MouseUp += (sender, e) =>
        {
            if (IsMouseDown)
            {
                IsMouseDown = false;
            }
        };
    }
}


