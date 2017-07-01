// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace DeepEarth.Events
{

    /// <summary>
    /// <para>
    /// Provides events for the map which include the following:
    /// </para>
    /// <list type="bullet">
    /// <item>Mouse</item>
    /// <item>Keyboard</item>
    /// <item>Map Specific</item>
    /// </list>
    /// <example>
    /// 
    /// <code title="Register for the zoom changed event. When the event is raised, write out the the zoomlevel to the console">   
    /// MapInstance.Events.MapZoomChanged += Events_MapZoomChanged;
    /// 
    /// private void Events_MapZoomChanged(Map map, double zoomLevel)
    /// {
    ///   Console.WriteLine(zoomLevel.ToString());
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class MapEvents
    {
        private long _LastClick = 0;
        private double _LastZoom = 0;
        private bool _InZoom = false;
        private bool _IsMouseDown = false;
        private Map _Map;

        private const double MouseDoubleClickSpeed = 3000000;
        private const double MouseDragDelay = 2000000.0;

        /// <summary>
        /// MapEvents constructor, will use the specific instance of the map
        /// </summary>
        public MapEvents(Map map)
        {
            _Map = map;
        }

        /// <summary>
        /// Toggles keyboard, mouse click and mouse wheel events
        /// </summary>
        public bool Enable
        {
            get
            {
                return _IsEnbled;
            }
            set
            {
                _IsEnbled = value;
                EnableKeyboard = _IsEnbled;
                EnableMouseClicks = _IsEnbled;
                EnableMouseWheel = _IsEnbled;
            }
        }
        bool _IsEnbled = true;

        /// <summary>
        /// Toggles mouse down, mouse up, double click and drag events that the map will use
        /// </summary>
        public bool EnableMouseClicks
        {
            get { return _EnableMouseClicks; } 
            set { _EnableMouseClicks = value; }
        }
        bool _EnableMouseClicks = true;

        /// <summary>
        /// Toggles keyUp and keyDown events that the map will use
        /// </summary>
        public bool EnableKeyboard
        {
            get { return _EnableKeyboard; } 
            set { _EnableKeyboard = value; }
        }
        bool _EnableKeyboard = true;


        /// <summary>
        /// Toggles mouse wheel events that the map will use
        /// </summary>
        public bool EnableMouseWheel
        {
            get { return _EnableMouseWheel; } 
            set { _EnableMouseWheel = value; }
        }
        bool _EnableMouseWheel = true;


        #region Key Events

        ///<summary>
        /// Delegate for handling common keyboard events.
        ///</summary>
        ///<param name="map"></param>
        ///<param name="args"></param>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public delegate void MapKeyEvent(Map map, KeyEventArgs args);
        /// <summary>
        /// Fires when a key is pressed down
        /// </summary>
        public event MapKeyEvent MapKeyDown;
        /// <summary>
        /// Fires when a key is released
        /// </summary>
        public event MapKeyEvent MapKeyUp;
        internal void KeyDown(Map map, KeyEventArgs args)
        {
            if (EnableKeyboard)
            {
                if (MapKeyDown != null) MapKeyDown(map, args);
            }
        }
        internal void KeyUp(Map map, KeyEventArgs args)
        {
            if (EnableKeyboard)
            {
                if (MapKeyUp != null) MapKeyUp(map, args);
            }
        }

        #endregion


        #region Mouse Button Events

        ///<summary>
        /// Delegate for handling common mouse button events.
        ///</summary>
        ///<param name="map">Instance of Map</param>
        ///<param name="args">MouseButtonEventArgs</param>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public delegate void MapMouseButtonEvent(Map map, MouseButtonEventArgs args);

        /// <summary>
        /// Fires when the left mouse button is pressed down
        /// </summary>
        public event MapMouseButtonEvent MapMouseDown;

        /// <summary>
        /// Fires when the left mouse button is released
        /// </summary>
        public event MapMouseButtonEvent MapMouseUp;

        /// <summary>
        /// Fires if the delta between the mouseDown event is close enough to act as a double click
        /// </summary>
        public event MapMouseButtonEvent MapDoubleClick;
        internal void MouseDown(Map map, MouseButtonEventArgs args)
        {
            _IsMouseDown = true;
            if (EnableMouseClicks)
            {
                RaiseIfDoubleClick(map, args);
                if (MapMouseDown != null) MapMouseDown(map, args);
            }
            _LastClick = DateTime.Now.Ticks;
        }

        private void RaiseIfDoubleClick(Map map, MouseButtonEventArgs args)
        {
            if (EnableMouseClicks)
            {
                if ((DateTime.Now.Ticks - _LastClick) < MouseDoubleClickSpeed)
                {
                    if (MapDoubleClick != null) MapDoubleClick(map, args);
                }
            }
        }


        internal void MouseUp(Map map, MouseButtonEventArgs args)
        {
            _IsMouseDown = false;
            if (EnableMouseClicks)
            {
                if (MapMouseUp != null) MapMouseUp(map, args);
            }
        }




        #endregion


        #region Mouse Events

        ///<summary>
        /// Delegate for raising basic mouse events.
        ///</summary>
        ///<param name="map">Instance of Map</param>
        ///<param name="args">MouseEventArgs</param>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public delegate void MapMouseEvent(Map map, MouseEventArgs args);

        /// <summary>
        /// Fires when the mouse moves while over the map
        /// </summary>
        public event MapMouseEvent MapMouseMove;

        /// <summary>
        /// Fires when the mouse initially enters the map
        /// </summary>
        public event MapMouseEvent MapMouseEnter;

        /// <summary>
        /// Fires when the mouse initially leaves the map
        /// </summary>
        public event MapMouseEvent MapMouseLeave;

        /// <summary>
        /// Fires when the mouse left button is held down and the mouse moves
        /// </summary>
        public event MapMouseEvent MapMouseDrag;
        internal void MouseMove(Map map, MouseEventArgs args)
        {
            if (MapMouseMove != null) MapMouseMove(map, args);
            RaiseIfMouseDrag(map, args);
        }

        internal void MouseEnter(Map map, MouseEventArgs args)
        {
            if (MapMouseEnter != null) MapMouseEnter(map, args);
        }

        internal void MouseLeave(Map map, MouseEventArgs args)
        {
            if (MapMouseLeave != null) MapMouseLeave(map, args);
        }

        private void RaiseIfMouseDrag(Map map, MouseEventArgs args)
        {
            if (EnableMouseClicks && _IsMouseDown)
            {
                if (_LastClick != 0)
                {
                    //If we are not zooming, go to drag right away, otherwise delay to not conflict with a double click zoom.
                    if (_InZoom == false || (DateTime.Now.Ticks - _LastClick) > MouseDragDelay) // (TimeSpan.FromSeconds(0.15).TotalMilliseconds * 10000)	
                    {
                        if (MapMouseDrag != null) MapMouseDrag(map, args);
                    }
                }
            }
        }

        #endregion


        #region MouseWheel Events

        ///<summary>
        /// Delegate for mouse wheel events.
        ///</summary>
        ///<param name="map"></param>
        ///<param name="args"></param>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public delegate void MapMouseWheelEvent(Map map, MouseWheelEventArgs args);

        /// <summary>
        /// Fires when the mouse wheel changes its value
        /// </summary>
        public event MapMouseWheelEvent MapMouseWheel;
        internal void MouseWheel(Map map, MouseWheelEventArgs args)
        {
            if (EnableMouseWheel)
            {
                if (MapMouseWheel != null) MapMouseWheel(map, args);
            }
        }
        #endregion


        #region View Events

        ///<summary>
        /// Delegate for generic DeepEarth event.
        ///</summary>
        ///<param name="map">Instance of Map</param>
        ///<param name="args">MapEventArgs</param>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public delegate void MapEvent(Map map, MapEventArgs args);

        /// <summary>
        /// Fires when the silverlight application has finished loading
        /// </summary>
        public event MapEvent MapLoaded;

        /// <summary>
        /// Fires when the map changes its angle
        /// </summary>
        public event MapEvent MapRotationChanged;

        /// <summary>
        /// Fires when the UpdateModes changes. See GeometryLayer.cs for UpdateModes definition.
        /// </summary>
        public event MapEvent MapViewChanged;

        /// <summary>
        /// Fires when the map initially starts to zoom in
        /// </summary>
        public event MapEvent MapZoomStarted;

        /// <summary>
        /// Fires when the map has finished zooming
        /// </summary>
        public event MapEvent MapZoomEnded;

        /// <summary>
        /// Fires when the base layer changes its tile provider
        /// </summary>
        public event MapEvent MapTileSourceChanged;


        ///<summary>
        /// Delegate for defining the providing the ZoomChanged event.
        ///</summary>
        ///<param name="map">Instance of Map</param>
        ///<param name="zoomLevel">double value for map zoom level</param>
        [EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public delegate void ZoomChanged(Map map, double zoomLevel);

        /// <summary>
        /// Fires when the zoom level of the map changes to any value
        /// </summary>
        public event ZoomChanged MapZoomChanged;
        private void RaiseZoomChanged(Map map, double zoom)
        {
            if (_LastZoom != zoom)
            {
                _LastZoom = zoom;
                ZoomChanged temp = MapZoomChanged;
                if (temp != null)
                {
                    MapZoomChanged(map, zoom);
                }
            }
        }

        internal void Loaded(Map map, MapEventArgs args)
        {
            if (MapLoaded != null) MapLoaded(map, args);
        }


        internal void RotationChanged(Map map, MapEventArgs args)
        {
            if (MapRotationChanged != null) MapRotationChanged(map, args);
        }



        private Point _PriorOrigin;
        const double ChangeSignificanceThreshold = 0.05;
        internal void ViewChanged(Map map, MapEventArgs args)
        {
            //Note this operation exists to eliminate the high volume of insignificant updates from MSI.ViewUpdated
            //Uses the sum of squares to determine distance moved.
            //The MSI woble effect seeems to occur at ~ a .03% of the logical display.  
            //So seeting the change tolerance at .05 eliminates much of the woble effect especially post zoom.
            double distLogChange = Math.Sqrt(Math.Pow(_Map.LogicalOrigin.X - _PriorOrigin.X, 2) + Math.Pow(_Map.LogicalOrigin.Y - _PriorOrigin.Y, 2));
            double changeSignificance = 100 * distLogChange / _Map.MapViewLogicalSize.Width;
            if (changeSignificance > ChangeSignificanceThreshold)
            {
                _PriorOrigin = _Map.LogicalOrigin;
                RaiseZoomChanged(map, map.ZoomLevel);
                if (MapViewChanged != null) MapViewChanged(map, args);
            }
        }


        internal void ZoomStarted(Map map, MapEventArgs args)
        {
            _InZoom = true;
            if (MapZoomStarted != null) MapZoomStarted(map, args);
        }

        internal void ZoomEnded(Map map, MapEventArgs args)
        {
            _InZoom = false;
            if (MapZoomEnded != null) MapZoomEnded(map, args);
        }

        internal void TileSourceChanged(Map map, MapEventArgs args)
        {
            if (MapTileSourceChanged != null) MapTileSourceChanged(map, args);
        }

        #endregion


    }
}