using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

using DeepEarth.Events;

namespace DeepEarth.Controls
{

    /// <summary>
    /// <para>
    /// UI control to handle the Pan and Select behavours of the mouse on the map
    /// </para>
    /// <example>
    /// 
    /// <code lang="XAML" title="Add MouseControl via Xaml, is located in the map style/template">   
    /// <![CDATA[
    /// <Controls:MouseControl x:Name="PART_MouseControl"/>
    /// ]]> 
    /// </code>
    /// </example>
    /// </summary>
    public class MouseControl : MapControl
    {
        private Rectangle _Box;
        private Map.DragBehavior _DragModePrior;
        private bool _IsCtrlDown;
        private bool _Moved;
        private Point _OrigMousePoint;
        private bool _IsMouseDown;
        private bool _IsMouseOver;
        private Point _MouseLogicalPosition;
        private Point _MousePixelPosition;
        private Point _OriginOnClick;

        /// <summary>
        /// MouseControl Constructor, will use the default instance of the map
        /// </summary>
        public MouseControl() : this(Map.DefaultInstance) { }

        /// <summary>
        /// MouseControl Constructor, will use the default instance of the map
        /// </summary>
        /// <param name="map">Instance of Map</param>
        public MouseControl(Map map)
        {
            _Map = map;
            DefaultStyleKey = typeof(MouseControl);

            // Test IsDesignTime, to help display control in blend correctly
            if(HtmlPage.IsEnabled)
            {
                HtmlPage.Document.AttachEvent("DOMMouseScroll", OnMouseWheelFirefox);
                HtmlPage.Document.AttachEvent("onmousewheel", OnMouseWheelOther);
                IsWheelEnabled = true;
            }
        }

        /// <summary>
        /// Gets zoom level adjustment value, default is 1
        /// </summary>
        public int ZoomLevelAdjustment
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets mouse wheel zoom level adjustment value, default is 1
        /// </summary>
        public int WheelZoomLevelAdjustment
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets MouseControl Bounds, Rect with Hight and Width of the area covered by Mouse Drag when in selection DragBehavour mode
        /// </summary>
        public Rect Bounds
        {
            get
            {
                var bounds = new Rect(Location, new Size(_Box.Width, _Box.Height));
                return bounds;
            }
        }

        /// <summary>
        /// Gets MouseControl Size, Hight and Width of the area covered by Mouse Drag when in selection DragBehavour mode
        /// </summary>
        public Size Size
        {
            get { return new Size(_Box.Width, _Box.Height); }
            set
            {
                _Box.Width = value.Width;
                _Box.Height = value.Height;

                Visibility = value.IsEmpty == false ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Gets MouseControl Location, Point X,Y for Top Left relitive to the Canvas containning the MouseControl
        /// </summary>
        public Point Location
        {
            get { return new Point(Canvas.GetLeft(_Box), Canvas.GetTop(_Box)); }
            set
            {
                Canvas.SetTop(_Box, value.Y);
                Canvas.SetLeft(_Box, value.X);
            }
        }

        /// <summary>
        /// Overrides the default OnApplyTemplate to configure child FrameworkElement references from the applied template
        /// </summary>
        public override void OnApplyTemplate()
        {
            _Box = (Rectangle)GetTemplateChild("PART_PixelBox");

            // Test IsDesignTime, to help display control in blend correctly
            if(HtmlPage.IsEnabled)
            {
                //Wire up the mouse events to fire the Map.Events.Map*Events.
                MapInstance.MouseEnter += MapInstance_MouseEnter;
                MapInstance.MouseLeave += MapInstance_MouseLeave;

                //MapInstance.MouseEnter += (o, e) => MapInstance.Events.MouseEnter(MapInstance, e);
                //MapInstance.MouseLeave += (o, e) => MapInstance.Events.MouseLeave(MapInstance, e);

                MapInstance.MouseLeftButtonDown += (o, e) => { IsWheelEnabled = false; MapInstance.Events.MouseDown(MapInstance, e); };
                MapInstance.MouseLeftButtonUp += (o, e) => { IsWheelEnabled = true; MapInstance.Events.MouseUp(MapInstance, e); };
                MapInstance.MouseMove += (o, e) => MapInstance.Events.MouseMove(MapInstance, e);

                //Create event handlers for standard MouseControl behavior.
                MapInstance.Events.MapMouseDown += Events_MapMouseDown;
                MapInstance.Events.MapMouseMove += Events_MapMouseMove;
                MapInstance.Events.MapMouseDrag += Events_MapMouseDrag;
                MapInstance.Events.MapMouseUp += Events_MapMouseUp;
                MapInstance.Events.MapKeyDown += Events_MapKeyDown;
                MapInstance.Events.MapKeyUp += Events_MapKeyUp;
                MapInstance.Events.MapMouseEnter += Events_MouseEnter;
                MapInstance.Events.MapMouseLeave += Events_MouseLeave;
                MapInstance.Events.MapMouseWheel += Events_MapMouseWheel;
            }
            else
            {
                // Is DesignTime, so set some dummy Height & Widths for editing control in Blend
                Width = 200;
                Height = 200;
                _Box.Width = 200;
                _Box.Height = 200;
            }
        }

        Control _FocusElement;
        void MapInstance_MouseEnter(object sender, MouseEventArgs e)
        {
            object focusObject = FocusManager.GetFocusedElement();
            if(focusObject != this)
            {
                if(focusObject != null && focusObject is Control)
                {
                    _FocusElement = focusObject as Control;
                }
                else
                {
                    _FocusElement = null;
                }
            }
            else
            {
                _FocusElement = null;
            }
            MapInstance.Focus();
            MapInstance.Events.MouseEnter(MapInstance, e);
        }


        void MapInstance_MouseLeave(object sender, MouseEventArgs e)
        {
            if(_FocusElement != null)
            {
                _FocusElement.Focus();
            }
            MapInstance.Events.MouseLeave(MapInstance, e);
        }



        void Events_MapMouseWheel(Map map, MouseWheelEventArgs args)
        {
            if(!_IsMouseOver || args.Delta == 0)
            {
                return;
            }

            int adjustment = args.Delta > 0 ? WheelZoomLevelAdjustment : -WheelZoomLevelAdjustment;
            map.ZoomOnPixelPoint(_MousePixelPosition, adjustment);
            args.Handled = true;
        }



        private void Events_MapKeyDown(Map map, KeyEventArgs args)
        {
            if(args.Key == Key.Ctrl && _IsCtrlDown == false)
            {
                _DragModePrior = MapInstance.DragMode;
                _IsCtrlDown = true;
                map.DragMode = Map.DragBehavior.Select;
            }
            else
            {
                switch(args.Key)
                {
                    //For convenience, hold Control to zoom when already navigating with the arrows
                    case Key.Left: map.Pan(Direction.West); break;
                    case Key.Right: map.Pan(Direction.East); break;
                    case Key.Up: if(_IsCtrlDown) map.ZoomLevel += 1; else map.Pan(Direction.North); break;
                    case Key.Down: if(_IsCtrlDown) map.ZoomLevel -= 1; else map.Pan(Direction.South); break;
                }
            }
        }

        private void Events_MapKeyUp(Map map, KeyEventArgs args)
        {
            if(args.Key == Key.Ctrl)
            {
                _IsCtrlDown = false;
                map.DragMode = _DragModePrior;
            }
        }

        private void Events_MapMouseDown(Map map, MouseButtonEventArgs args)
        {
            CaptureMouse();
            _IsMouseDown = true;


            _OrigMousePoint = args.GetPosition(map);

            switch(MapInstance.DragMode)
            {
                case Map.DragBehavior.Select:
                    Size = new Size();
                    Location = _OrigMousePoint;
                    break;
                case Map.DragBehavior.Pan:
                    _OriginOnClick = map.LogicalOrigin;
                    break;
            }
        }


        private void Events_MapMouseMove(Map map, MouseEventArgs args)
        {
            if(_OrigMousePoint == new Point()) _OrigMousePoint = args.GetPosition(map);
            _MousePixelPosition = args.GetPosition(map);
            _MouseLogicalPosition = map.CoordHelper.PixelToLogicalIncRotation(_MousePixelPosition);

        }


        private void Events_MapMouseDrag(Map map, MouseEventArgs args)
        {
            switch(MapInstance.DragMode)
            {
                case Map.DragBehavior.Pan:
                    {
                        if(_IsMouseDown)
                        {
                            //Check to see that we are not exceeding the Map Boundaries.
                            Point geoPoint = _Map.CoordHelper.LogicalToGeo(_Map.LogicalCenter);
                            bool inBounds = MapInstance.BaseLayer.Source.IsValidGeoPoint(geoPoint);
                            if(inBounds)
                            {
                                Point newPoint = _OriginOnClick;
                                Point _OrigMousePointLogical = map.CoordHelper.PixelToLogicalIncRotation(_OrigMousePoint);
                                newPoint.X += (_OrigMousePointLogical.X - _MouseLogicalPosition.X);
                                newPoint.Y += (_OrigMousePointLogical.Y - _MouseLogicalPosition.Y);
                                map.LogicalOrigin = newPoint;
                            }
                        }
                        break;
                    }
                case Map.DragBehavior.Select:
                    {
                        if(_IsMouseDown)
                        {
                            _Moved = true;
                            Point loc = args.GetPosition(map);
                            Point boundedLoc = GetBoundedPixel(map, ref loc);

                            Location = new Point(Math.Min(_OrigMousePoint.X, boundedLoc.X), Math.Min(_OrigMousePoint.Y, boundedLoc.Y));
                            var newSize = new Size(Math.Abs(_OrigMousePoint.X - boundedLoc.X), Math.Abs(_OrigMousePoint.Y - boundedLoc.Y));
                            Size = newSize;
                        }
                        break;
                    }
            }

        }

        private static Point GetBoundedPixel(Map map, ref Point loc)
        {

            //Incorp Provider Source boundaries to not go outside of Map bounds
            Rect bounds = map.BaseLayer.Source.CoordinateBounds;
            Point mouseGeoPoint = map.CoordHelper.PixelToGeo(loc);
            mouseGeoPoint.X = Math.Min(mouseGeoPoint.X, bounds.Right);
            mouseGeoPoint.X = Math.Max(mouseGeoPoint.X, bounds.Left);
            mouseGeoPoint.Y = Math.Max(mouseGeoPoint.Y, bounds.Top);
            mouseGeoPoint.Y = Math.Min(mouseGeoPoint.Y, bounds.Bottom);
            Point boundedLoc = map.CoordHelper.GeoToPixel(mouseGeoPoint);
            return boundedLoc;
        }

        private void Events_MapMouseUp(Map map, MouseButtonEventArgs args)
        {
            ReleaseMouseCapture();

            switch(MapInstance.DragMode)
            {
                case Map.DragBehavior.Select:
                    if(_Moved) MapInstance.PixelBounds = Bounds;
                    break;
                case Map.DragBehavior.Pan:
                    (map).ReleaseMouseCapture();
                    break;
            }

            _IsMouseDown = false;
            _Moved = false;
            _IsMouseDown = false;
            Size = new Size();

        }

        private void Events_MouseEnter(Map map, MouseEventArgs args)
        {
            _IsMouseOver = true;
        }

        private void Events_MouseLeave(Map map, MouseEventArgs args)
        {
            _IsMouseOver = false;
        }


        #region MouseWheelListener APIs



        /// <summary>
        /// Indicates whether or not the mouse wheel is enabled.
        /// </summary>
        public bool IsWheelEnabled { get; set; }


        /// <summary>
        /// Handles mouse wheel events for Firefox.
        /// </summary>
        /// <param name="sender">The HTML element for the plug-in.</param>
        /// <param name="e">The HTML event arguments.</param>
        private void OnMouseWheelFirefox(object sender, HtmlEventArgs e)
        {
            if(_IsMouseOver == false)
            {
                return;
            }

            if(IsWheelEnabled == false)
            {
                e.PreventDefault();
                return;
            }

            double delta = (double)e.EventObject.GetProperty("detail") / -3;
            var args = new MouseWheelEventArgs(delta);
            MapInstance.Events.MouseWheel(MapInstance, args);

            if(args.Handled)
            {
                e.PreventDefault();
            }
        }

        /// <summary>
        /// Handles mouse wheel events for browsers other than Firefox.
        /// </summary>
        /// <param name="sender">The HTML element for the plug-in.</param>
        /// <param name="e">The HTML event arguments.</param>
        private void OnMouseWheelOther(object sender, HtmlEventArgs e)
        {
            if(_IsMouseOver == false)
            {
                return;
            }

            if(IsWheelEnabled == false)
            {
                e.EventObject.SetProperty("returnValue", false);
                return;
            }

            double delta = (double)e.EventObject.GetProperty("wheelDelta") / 120;
            var args = new MouseWheelEventArgs(delta);
            MapInstance.Events.MouseWheel(MapInstance, args);

            if(args.Handled)
            {
                e.EventObject.SetProperty("returnValue", false);
            }
        }



        #endregion
    }
}




