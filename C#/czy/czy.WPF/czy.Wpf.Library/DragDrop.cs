using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Controls;

namespace czy.Wpf.Library
{
    public class DragDrop
    {
        bool IsMouseDown = false;
        Point mousePoint;
        object mouseCtrl = null;
        //drag and drop items between data bound ItemsControls
        private UIElement _element;

        public UIElement Element
        {
            get { return _element; }
            set { _element = value; }
        }
        private Canvas _canvas;

        public Canvas Canvas
        {
            get { return _canvas; }
            set { _canvas = value; }
        }

        public DragDrop(UIElement el, Canvas cav)
        {
            this._element = el;
            this._canvas = cav;
            this._element.MouseDown += (sender, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        IsMouseDown = true;
                        mousePoint = e.GetPosition(this._canvas);
                        mouseCtrl = sender;
                    }
                };
            this._element.MouseMove += (sender, e) =>
                {
                    if (IsMouseDown)
                    {
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            Point theMousePoint = e.GetPosition(this._canvas);
                            Canvas.SetLeft((UIElement)mouseCtrl, theMousePoint.X - (mousePoint.X - Canvas.GetLeft(((UIElement)mouseCtrl))));
                            Canvas.SetTop((UIElement)mouseCtrl, theMousePoint.Y - (mousePoint.Y - Canvas.GetTop(((UIElement)mouseCtrl))));

                            mousePoint = theMousePoint;
                        }
                    }
                };
            this._element.MouseUp += (sender, e) =>
            {
                if (IsMouseDown)
                {
                    IsMouseDown = false;
                }
            };

        }

  

 

        //public void ShowDraggedAdorner(Point currentPosition)
        //{
        //    var adornerLayer = AdornerLayer.GetAdornerLayer(this.sourceItemsControl);
        //    this.draggedAdorner = new DraggedAdorner(this.draggedData, GetDragDropTemplate(this.sourceItemsControl), this.sourceItemContainer, adornerLayer);
        //}
    }
}
