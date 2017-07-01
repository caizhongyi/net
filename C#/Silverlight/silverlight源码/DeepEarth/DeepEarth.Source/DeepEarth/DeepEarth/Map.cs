// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty  Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DeepEarth.Controls;
using DeepEarth.Events;
using DeepEarth.Layers;

namespace DeepEarth
{

    /// <summary>
    /// <para>
    /// Map is the root of the DeepEarth object model.  It provides a base structure and accessors for:
    /// <list type="bullet">
    /// <item>Controls</item>
    /// <item>Events</item>
    /// <item>Geometry</item>
    /// <item>Layers</item>
    /// </list>
    /// </para>
    /// 
    /// <example>
    /// <code lang="CSharp" title="Create an instance of map and set a TileSource">   
    ///    Map map = new Map();
    ///    map.BaseLayer.Source = new VeTileSource(VeMapModes.VeHybrid);
    /// </code>
    /// </example>
    /// </summary>
    [TemplatePart(Name = PART_LicenseContainer, Type = typeof (Panel))]
    [TemplatePart(Name = PART_MapContent, Type = typeof (Grid))]
    [TemplatePart(Name = PART_MapRotation, Type = typeof (RotateTransform))]
    [TemplatePart(Name = PART_MouseControl, Type = typeof (MouseControl))]
    [TemplatePart(Name = PART_MapLayers, Type = typeof (Grid))]
    [ContentProperty("Layers")]
    public class Map : Control
    {
        #region DragBehavior enum

        /// <summary>
        /// The possible options when you click and drag your mouse over the map
        /// </summary>
        public enum DragBehavior
        {
            /// <summary>
            /// The Map will essentially stick to your mouse and move where you drag it to.
            /// </summary>
            Pan,

            /// <summary>
            /// Draw a Marque Selection Box over the map, and zoom in when mouse released.
            /// </summary>
            Select,

            /// <summary>
            /// Draw a Shape on the map, you don't pan the map when you want to draw shapes
            /// </summary>
            Draw
        }

        #endregion

        private const string PART_LicenseContainer = "PART_LicenseContainer";
        private const string PART_MapContent = "PART_MapContent";
        private const string PART_MapLayers = "PART_MapLayers";
        private const string PART_MapRotation = "PART_MapRotation";
        private const string PART_MouseControl = "PART_MouseControl";

        /// <summary>
        /// Provides a reference to an instance the last created Map.  This is be applicable for cases where only one Map instance is created.
        /// Or where map specific values are not relevent.
        /// </summary>
        public static Map DefaultInstance;

        private readonly ObservableCollection<ILayer> _Layers;
        private readonly TileLayer _TileLayer;

        private DoubleAnimation _AnimationMapAngle;
        private CoordTransform _CoordHelper;
        private DragBehavior _DragMode = DragBehavior.Pan;
        private Panel _LicenseContainer;
        private Grid _MapContent;
        private MapEvents _MapEvents;
        private Grid _MapLayerGrid;
        private RotateTransform _MapRotation;
        private MouseControl _MouseControl;
        private NavControl _Navigation;
        private ISpatialReference _SpatialReference;
        private Storyboard _StoryboardRotation;
        private Point _TargetCenter;
        internal double _TargetZoom;
        internal double _RotationAngle = 0;

        /// <summary>
        /// Create an instance of the DeepEarth Map Control
        /// </summary>
        public Map()
        {
            DefaultInstance = this;

            _SpatialReference = new MercatorProjection();
            _TileLayer = new TileLayer(this);
            _MouseControl = new MouseControl(this);
            _Layers = new ObservableCollection<ILayer>();
            _Layers.CollectionChanged += Layers_CollectionChanged;

            DefaultStyleKey = typeof (Map);
            DisplayUnit = Unit.Meter;
            DragMode = DragBehavior.Pan;

            //Wire up the keyboard events.
            TabNavigation = KeyboardNavigationMode.Once;
            IsTabStop = true;
            KeyDown += (o, e) => Events.KeyDown(this, e);
            KeyUp += (o, e) => Events.KeyUp(this, e);

            //Create event handlers for default map behavior
            Events.MapDoubleClick += MapDoubleClick;
            Events.MapZoomEnded += (o, e) => _TargetZoom = 0;
            Events.MapLoaded += (o, e) => UpdateCenterPoint();
            Events.MapTileSourceChanged += (o, e) => UpdateCopyrights();
        }


        /// <summary>
        /// Layers is an ObservableCollection of ILayer instances.
        /// Each layer is placed in a grid above the Map.BaseLayer
        /// </summary>
        public ObservableCollection<ILayer> Layers
        {
            get { return _Layers; }
        }

        /// <summary>
        /// The Behavior of the control while draging the mouse over the map control.
        /// </summary>
        public DragBehavior DragMode
        {
            get { return _DragMode; }
            set
            {
                if (value == DragBehavior.Pan) Cursor = Cursors.Hand;
                if (value == DragBehavior.Select) Cursor = Cursors.Arrow;
                _DragMode = value;
            }
        }

        /// <summary>
        /// Reference to an instance of the CoordTranform class.  
        /// </summary>
        public CoordTransform CoordHelper
        {
            get
            {
                if (_CoordHelper == null)
                {
                    _CoordHelper = new CoordTransform(this);
                }
                return _CoordHelper;
            }
        }

        /// <summary>
        /// The current Spatial Reference of the Map Control
        /// </summary>
        public ISpatialReference SpatialReference
        {
            get
            {
                if (_SpatialReference == null)
                {
                    _SpatialReference = new SpatialReference();
                }
                return _SpatialReference;
            }
            set { _SpatialReference = value; }
        }

        /// <summary>
        /// Access to all the events of the map including 
        /// <list type="bullet">
        /// <item>Mouse</item>
        /// <item>Keyboard</item>
        /// <item>Map Specific</item>
        /// </list>
        /// </summary>
        public MapEvents Events
        {
            get
            {
                if (_MapEvents == null)
                {
                    _MapEvents = new MapEvents(this);
                }
                return _MapEvents;
            }
        }

        /// <summary>
        /// The Main Navigation control of the map.
        /// </summary>
        public NavControl Navigation
        {
            get { return _Navigation; }
            set { _Navigation = value; }
        }

        /// <summary>
        /// Provides public accessor to the MouseControl.
        /// </summary>
        public MouseControl BoundingBox
        {
            get { return _MouseControl; }
            set { _MouseControl = value; }
        }

        /// <summary>
        /// The main raster tilelayer of the map. This is the DeepZoom Tileset that is the core of DeepEarth
        /// </summary>
        public TileLayer BaseLayer
        {
            get { return _TileLayer; }
        }

        /// <summary>
        /// The unit of measure we display information in.
        /// </summary>
        [DefaultValue(Unit.Meter)]
        public Unit DisplayUnit { get; set; }

        /// <summary>
        /// The current scale of the map at the centre of the current screen.
        /// EG 1:1000 meters, one meter of the screen would equal 1000 real meters
        /// </summary>
        public double Scale
        {
            get
            {
                if (BaseLayer.Source != null)
                {
                    return CoordHelper.GetScaleAtZoomLevel(ZoomLevel, GeoCenter, DisplayUnit);
                }
                return 0;
            }
        }

        /// <summary>
        /// The resolution of a pixel at the current zoomlevel at the equator.
        /// </summary>
        public double Resolution
        {
            get
            {
                if (BaseLayer.Source != null)
                {
                    return CoordHelper.GetResolutionAtZoomLevel(ZoomLevel, DisplayUnit);
                }
                return 0;
            }
        }

        /// <summary>
        /// The current Zoom level of the map, an exponential scale where level 3 is twice the width and height of level 4
        /// Typically level 1 will be the whole world at 512px, 17 is street level.
        /// Larger value = zoom into the world. 
        /// </summary>
        public double ZoomLevel
        {
            get
            {
                if (BaseLayer.IsLoaded && BaseLayer.Source != null)
                {
                    double zoom = CoordHelper.LogicalViewToZoomLevel(MapViewLogicalSize);
                    return zoom;
                }
                return -1;
            }
            set
            {
                //Validate against zoom level being set out of bounds.
                double validatedZoom = BaseLayer.Source.GetValidatedZoomLevel(value);
                if (BaseLayer.IsLoaded)
                {
                    var center = new Point(_TileLayer.MSI.ActualWidth/2, _TileLayer.MSI.ActualHeight/2);
                    ZoomOnPixelPoint(center, validatedZoom);
                }
                else
                {
                    //Cache value to be applied post MSI.Loaded
                    _TargetZoom = validatedZoom;
                }
            }
        }


        /// <summary>
        /// The Geographic Center point of the Map (Longitude, Latitude)
        /// </summary>
        public Point GeoCenter
        {
            get
            {
                if (SpatialReference != null)
                {
                    var centerPixel = new Point(_TileLayer.MSI.ActualWidth/2, _TileLayer.MSI.ActualHeight/2);
                    return CoordHelper.PixelToGeo(centerPixel);
                }

                return new Point();
            }
            set
            {
                if (SpatialReference != null && _TileLayer.MSI != null)
                {
                    Point pixelCenter = CoordHelper.GeoToPixel(value);
                    var pixelOrigin = new Point(pixelCenter.X - _TileLayer.MSI.ActualWidth/2,
                                                pixelCenter.Y - _TileLayer.MSI.ActualHeight/2);
                    GeoOrigin = CoordHelper.PixelToGeo(pixelOrigin);
                }
                else
                {
                    //Cache value to be applied post MSI.Loaded
                    _TargetCenter = value;
                }
            }
        }

        /// <summary>
        /// The Geographical top left of the Map (Longitude, Latitude)
        /// </summary>
        public Point GeoOrigin
        {
            get
            {
                if (_TileLayer.MSI != null)
                {
                    return CoordHelper.LogicalToGeo(_TileLayer.MSI.ViewportOrigin);
                }
                return new Point();
            }
            set
            {
                if (_TileLayer.MSI != null)
                {
                    _TileLayer.MSI.ViewportOrigin = CoordHelper.GeoToLogical(value);
                }
            }
        }

        /// <summary>
        /// The current Geographical Bounds (extent) of the map (Longitude, Latitude)
        /// </summary>
        public Rect GeoBounds
        {
            get
            {
                Rect bounds;
                if (_TileLayer.MSI != null)
                {
                    Point geoOrigin = GeoOrigin;
                    var pixelExtent = new Point(PixelOrigin.X + _TileLayer.MSI.ActualWidth,
                                                PixelOrigin.Y + _TileLayer.MSI.ActualHeight);
                    Point geoExtent = CoordHelper.PixelToGeo(pixelExtent);
                    bounds = new Rect(geoOrigin, geoExtent);
                }
                else
                {
                    bounds = new Rect();
                }
                return bounds;
            }
            set
            {
                if (_TileLayer.MSI != null && BaseLayer.Source != null)
                {
                    Point pixelBoxOrigin = CoordHelper.GeoToPixel(new Point(value.Left, value.Top));
                    Point pixelBoxExtent = CoordHelper.GeoToPixel(new Point(value.Right, value.Bottom));
                    var pixelBox = new Rect(pixelBoxOrigin, pixelBoxExtent);
                    SetPixelBounds(pixelBox);
                }
            }
        }

        /// <summary>
        /// The relative Pixel of the centre of the map. (X,Y)
        /// Always the half the width, half the right
        /// Useful if you want to pan X pixels.
        /// </summary>
        public Point PixelCenter
        {
            get
            {
                if (_TileLayer.MSI != null)
                {
                    return new Point(_TileLayer.MSI.ActualWidth/2, _TileLayer.MSI.ActualHeight/2);
                }

                return new Point();
            }
            set
            {
                if (SpatialReference != null && _TileLayer.MSI != null)
                {
                    Point pixelCenter = CoordHelper.GeoToPixel(value);
                    var pixelOrigin = new Point(pixelCenter.X - _TileLayer.MSI.ActualWidth/2,
                                                pixelCenter.Y - _TileLayer.MSI.ActualHeight/2);
                    GeoOrigin = CoordHelper.PixelToGeo(pixelOrigin);
                }
                else
                {
                    //Cache value to be applied post MSI.Loaded
                    _TargetCenter = value;
                }
            }
        }

        /// <summary>
        /// The absolute Pixel of the top left of the map. (X,Y)
        /// </summary>
        public Point PixelOrigin
        {
            get
            {
                if (_TileLayer.MSI != null)
                {
                    return CoordHelper.LogicalToPixel(LogicalOrigin);
                }
                return new Point();
            }
            set
            {
                if (_TileLayer.MSI != null)
                {
                    _TileLayer.MSI.ViewportOrigin = CoordHelper.PixelToLogical(value);
                }
            }
        }

        /// <summary>
        /// The absolute Pixel bounds of the map. (X,Y)
        /// </summary>
        public Rect PixelBounds
        {
            get
            {
                Rect bounds;
                if (_TileLayer.MSI != null)
                {
                    Point pixelOrgin = PixelOrigin;
                    var pixelExtent = new Point(PixelOrigin.X + _TileLayer.MSI.ActualWidth,
                                                PixelOrigin.Y + _TileLayer.MSI.ActualHeight);
                    bounds = new Rect(pixelOrgin, pixelExtent);
                }
                else
                {
                    bounds = new Rect();
                }
                return bounds;
            }
            set
            {
                if (_TileLayer.MSI != null && BaseLayer.Source != null)
                {
                    SetPixelBounds(value);
                }
            }
        }

        /// <summary>
        /// The Logical (0->1) centre of the Map.
        /// </summary>
        public Point LogicalCenter
        {
            get
            {
                if (_TileLayer.MSI != null)
                {
                    double aspectRatio = _TileLayer.MSI.ActualHeight/_TileLayer.MSI.ActualWidth;
                    double viewPortHight = _TileLayer.MSI.ViewportWidth*aspectRatio;
                    var logCenter = new Point(_TileLayer.MSI.ViewportOrigin.X + _TileLayer.MSI.ViewportWidth/2,
                                              _TileLayer.MSI.ViewportOrigin.Y + viewPortHight/2);
                    return logCenter;
                }

                return new Point();
            }
            set
            {
                if (SpatialReference != null && _TileLayer.MSI != null)
                {
                    Point pixelCenter = CoordHelper.GeoToPixel(value);
                    var pixelOrigin = new Point(pixelCenter.X - _TileLayer.MSI.ActualWidth/2,
                                                pixelCenter.Y - _TileLayer.MSI.ActualHeight/2);
                    GeoOrigin = CoordHelper.PixelToGeo(pixelOrigin);
                }
                else
                {
                    //Cache value to be applied post MSI.Loaded
                    _TargetCenter = value;
                }
            }
        }

        /// <summary>
        /// The Logical (0->1) top left of the Map.
        /// </summary>
        public Point LogicalOrigin
        {
            get
            {
                if (_TileLayer.MSI != null)
                {
                    return _TileLayer.MSI.ViewportOrigin;
                }
                return new Point();
            }
            set
            {
                if (_TileLayer.MSI != null)
                {
                    _TileLayer.MSI.ViewportOrigin = value;
                }
            }
        }

        /// <summary>
        /// The Logical (0->1) bounds of the Map.
        /// </summary>
        public Rect LogicalBounds
        {
            get
            {
                Rect bounds;
                if (_TileLayer.MSI != null)
                {
                    var pixelExtent = new Point(PixelOrigin.X + _TileLayer.MSI.ActualWidth,
                                                PixelOrigin.Y + _TileLayer.MSI.ActualHeight);
                    Point logicalExtent = CoordHelper.PixelToLogical(pixelExtent);
                    bounds = new Rect(LogicalOrigin, logicalExtent);
                }
                else
                {
                    bounds = new Rect();
                }
                return bounds;
            }
            set
            {
                if (_TileLayer.MSI != null && BaseLayer.Source != null)
                {
                    Point pixelBoxOrigin = CoordHelper.LogicalToPixel(new Point(value.Left, value.Top));
                    Point pixelBoxExtent = CoordHelper.LogicalToPixel(new Point(value.Right, value.Bottom));
                    var pixelBox = new Rect(pixelBoxOrigin, pixelBoxExtent);
                    SetPixelBounds(pixelBox);
                }
            }
        }

        /// <summary>
        /// The current view of the map's logical (0->1) size.
        /// </summary>
        public Size MapViewLogicalSize
        {
            get { return new Size(_TileLayer.MSI.ViewportWidth, _TileLayer.MSI.ViewportWidth*AspectRatio); }
        }

        /// <summary>
        /// The current size of the map in Pixels, eg 800,600.
        /// </summary>
        public Size MapViewPixelSize
        {
            get
            {
                if (_TileLayer.MSI != null)
                {
                    return new Size(_TileLayer.MSI.ActualWidth, _TileLayer.MSI.ActualHeight);
                }
                return new Size(ActualWidth, ActualHeight);
            }
        }

        /// <summary>
        /// The current size of the map regardless of the ViewPort, 
        /// (e.g. If viewing only half the map (e.g. eastern hemisphere), a ViewPort of 400,400 pixels, the MapExtent is 800, 800  /// </summary>
        public Size MapExtentPixelSize
        {
            get
            {
                Size extent;

                if (_TileLayer != null && _TileLayer.Source != null)
                {
                    double mapWidth = MapViewPixelSize.Width/MapViewLogicalSize.Width;
                    double mapHeight = mapWidth*(BaseLayer.Source.TileSize.Height/BaseLayer.Source.TileSize.Width);
                    extent = new Size(mapWidth, mapHeight);
                }
                else
                {
                    if (ActualHeight > 0 && ActualHeight > 0)
                    {
                        extent = new Size(ActualWidth, ActualHeight);
                    }
                    else
                    {
                        extent = new Size(0, 0);
                    }
                }
                return extent;
            }
        }

        /// <summary>
        /// The Ratio of Width to Height, of the screen ViewPort
        /// Important to be able to calculate the actual height only given the width.
        /// </summary>
        public double AspectRatio
        {
            get { return (_TileLayer.MSI.ActualHeight/_TileLayer.MSI.ActualWidth); }
        }

        /// <summary>
        /// Has the Map been loaded yet? If it hasn't then many properties and methods will not yet be useable.
        /// Consider suscribing to the Map.Events.MapLoaded event
        /// </summary>
        public bool IsMapLoaded
        {
            get { return BaseLayer.IsLoaded; }
        }

        /// <summary>
        /// The current rotation angle of the map in degrees. For example, setting of 180 would make south at the top.
        /// </summary>
        public double RotationAngle
        {
            get{return _RotationAngle;}
            set
            {
                _RotationAngle = value;
                if (_AnimationMapAngle != null)
                {
                    _AnimationMapAngle.To = RotationAngle;
                    _StoryboardRotation.Begin();
                    Events.RotationChanged(this, new MapEventArgs());
                }
                else
                {
                    _MapRotation.Angle = _RotationAngle;
                }

            }
        }

        #region Map Public Methods

        /// <summary>
        /// Zoom the map by an amount of ZoomLevel on a specific point (in relative pixels from top left)
        /// This point is zooms in on a pivot point, not necessesarily center screen
        /// Useful for interactive zooming such as zoom in/out holding the geo location under the mouse stable. 
        /// </summary>
        /// <param name="pixelPoint">Point to zoom on, relative pixels from top left</param>
        /// <param name="adjustment">The amount of zoomlevel to zoom in (+) or zoom out (-)</param>
        public void ZoomOnPixelPoint(Point pixelPoint, double adjustment)
        {
            if (BaseLayer.Source != null && _TileLayer.MSI != null && BaseLayer.Source.IsInitialized)
            {
                //Do not update the targetzoom until the requested zoom level has been validated to be in range.
                double requestedZoom = (_TargetZoom == 0)
                                           ? Math.Round(ZoomLevel) + adjustment
                                           : _TargetZoom + adjustment;
                if (BaseLayer.Source.IsValidZoomLevel(requestedZoom))
                {
                    _TargetZoom = requestedZoom;
                    double factorX = pixelPoint.X/MapViewPixelSize.Width;
                    double factorY = pixelPoint.Y/MapViewPixelSize.Height;
                    Point logicalPoint = CoordHelper.PixelToLogical(pixelPoint);

                    Size viewSize = CoordHelper.ZoomLevelToLogicalView(_TargetZoom);

                    var targetLogicalOrigin = new Point();
                    targetLogicalOrigin.X = logicalPoint.X - (viewSize.Width*factorX);
                    targetLogicalOrigin.Y = logicalPoint.Y - (viewSize.Height*factorY);
                    _TileLayer.MSI.ViewportOrigin = targetLogicalOrigin;

                    _TileLayer.MSI.ViewportWidth = viewSize.Width;
                }
            }
        }

        /// <summary>
        /// Pan the map (move it) the default amount (10%) in a standard direction 
        /// </summary>
        /// <param name="direction">The direction to pan, tradional 8 point of the compass</param>
        public void Pan(Direction direction)
        {
            Size mapSize = MapViewPixelSize;
            double offset = mapSize.Width/10;

            switch (direction)
            {
                case Direction.North:
                    Pan(0, -offset);
                    break;

                case Direction.NorthEast:
                    Pan(offset, -offset);
                    break;

                case Direction.NorthWest:
                    Pan(-offset, -offset);
                    break;

                case Direction.South:
                    Pan(0, offset);
                    break;

                case Direction.SouthEast:
                    Pan(offset, offset);
                    break;

                case Direction.SouthWest:
                    Pan(-offset, offset);
                    break;

                case Direction.East:
                    Pan(offset, 0);
                    break;

                case Direction.West:
                    Pan(-offset, 0);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        }

        /// <summary>
        /// Pan the Map (Move it) by the supplied pixels in both X and or Y planes
        /// </summary>
        /// <param name="deltaXInPixels">The number of pixels to pan up (-) or down (+)</param>
        /// <param name="deltaYInPixels">The number of pixels to pan left (-) or right (+)</param>
        public void Pan(double deltaXInPixels, double deltaYInPixels)
        {
            Point offset = PixelOrigin;

            //consider rotation
            Point deltaRotated = CoordHelper.RotatePixelbyMapRotation(new Point(deltaXInPixels, deltaYInPixels));
            offset.X += deltaRotated.X;
            offset.Y += deltaRotated.Y;

            PixelOrigin = offset;
        }

        /// <summary>
        /// Allows client applications to forces a refresh of tiles.  This is useful when a client application has dependent layers which need to 
        /// be  requeried.  For example, you might have a data driven Geometry Layer in which a parameter change requires a refetch.  Since the layer is
        /// driven by the ITileSource GetTile calls, a RefreshSource would allow a refetch to be made based on the current ViewPort.
        /// </summary>
        public void RefreshSource()
        {
            MultiScaleTileSource source = _TileLayer.MSI.Source;
            _TileLayer.MSI.Source = source;
        }

        /// <summary>
        /// Centre the map on the specified Long,Lat at the specified ZoomLevel
        /// </summary>
        /// <param name="geoCenter">Geographical Longitude, Latitude Point</param>
        /// <param name="zoom">The Map ZoomLevel required</param>
        public void SetViewCenter(Point geoCenter, double zoom)
        {
            if (_TileLayer.MSI != null && _TileLayer.MSI.ActualWidth != 0)
            {
                Size viewSize = CoordHelper.ZoomLevelToLogicalView(zoom);
                Point logicalCenter = CoordHelper.GeoToLogical(geoCenter);

                var logicalOrigin = new Point();
                logicalOrigin.X = logicalCenter.X - (viewSize.Width/2);
                logicalOrigin.Y = logicalCenter.Y - (viewSize.Height/2);

                _TileLayer.MSI.ViewportOrigin = logicalOrigin;

                _TileLayer.MSI.ViewportWidth = viewSize.Width;
            }
            else
            {
                _TargetCenter = geoCenter;
                _TargetZoom = zoom;
            }
        }

        /// <summary>
        /// Set the view of the map to be centered on 0,0 and fit the Map extent width to the screen
        /// </summary>
        public void SetViewFullMap()
        {
            double zoom = CoordHelper.LogicalViewToZoomLevel(new Size(1, 1));
            SetViewCenter(new Point(0, 0), zoom);
        }


        internal void UpdateCenterPoint()
        {
            //If center is not set, let center on Map Center.
            bool isCenterSet = _TargetCenter != new Point();
            bool isZoomSet = _TargetZoom != 0;

            if (isCenterSet || isZoomSet)
            {
                //If center value was not set, set center to the logical center point.
                if (isCenterSet == false)
                {
                    _TargetCenter = CoordHelper.LogicalToGeo(new Point(0.5, 0.5));
                }

                SetViewCenter(_TargetCenter, _TargetZoom);
            }
            else
            {
                SetViewFullMap();
            }
        }

        #endregion

        /// <summary>
        /// Overrides the default OnApplyTemplate to configure child FrameworkElement references from the applied template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //Get instances from the template
            _MapContent = (Grid) GetTemplateChild(PART_MapContent);
            _MapLayerGrid = (Grid) GetTemplateChild(PART_MapLayers);
            _MapRotation = (RotateTransform) GetTemplateChild(PART_MapRotation);
            _LicenseContainer = (Panel)GetTemplateChild(PART_LicenseContainer);
            _MouseControl = (MouseControl) GetTemplateChild(PART_MouseControl);
            _MouseControl.MapInstance = this;

            _MapContent.Children.Insert(0, _TileLayer);

            //Perform any necessary intialization to the elements
            RotationAngle = _MapRotation.Angle;
            ConfigMapLayers();
            ConfigMapRotation();

            LayoutUpdated += (o, e) => UpdateClippingArea();
        }

        private void SetPixelBounds(Rect pixelBox)
        {
            //Check to make sure this wasn't a simple click by checking the distance moved by mouse
            double area = pixelBox.Width*pixelBox.Height;

            if (area > 100)
            {
                var pixelCenter = new Point();
                pixelCenter.X = pixelBox.X + pixelBox.Width/2;
                pixelCenter.Y = pixelBox.Y + pixelBox.Height/2;

                //Get the minimum of the bounding box proprotions to the actual screen.
                double percWidth = pixelBox.Width/MapViewPixelSize.Width;
                double percHeight = pixelBox.Height/MapViewPixelSize.Height;
                double maxPerc = Math.Max(percWidth, percHeight);

                double targetWidth = _TileLayer.MSI.ViewportWidth*maxPerc;
                double targetZoom = CoordHelper.LogicalViewToZoomLevel(new Size(targetWidth, targetWidth));

                //Try to round the zoom level and redo the size off the new value;
                var roundZoom = (int) Math.Round(targetZoom, 0);
                Size roundedView = CoordHelper.ZoomLevelToLogicalView(roundZoom);

                var logicalOrigin = new Point();
                Point logicalCenter = CoordHelper.PixelToLogical(pixelCenter);
                logicalOrigin.X = logicalCenter.X - (roundedView.Width/2);
                logicalOrigin.Y = logicalCenter.Y - (roundedView.Height/2);
                _TileLayer.MSI.ViewportOrigin = logicalOrigin;
                _TileLayer.MSI.ViewportWidth = roundedView.Width;
            }
        }


        private void ConfigMapLayers()
        {
            foreach (ILayer mapItem in Layers)
            {
                mapItem.MapInstance = this;
                if (mapItem is NavControl) _Navigation = (NavControl) mapItem;
                if (_MapLayerGrid.Children.Contains((UIElement) mapItem) == false)
                    _MapLayerGrid.Children.Add((UIElement) mapItem);
            }
        }

        private void ConfigMapRotation()
        {
            _StoryboardRotation = new Storyboard();
            _AnimationMapAngle = new DoubleAnimation {Duration = new Duration(TimeSpan.FromMilliseconds(250))};
            Storyboard.SetTarget(_AnimationMapAngle, _MapContent);
            Storyboard.SetTargetProperty(_AnimationMapAngle,
                                         new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            _StoryboardRotation.Children.Add(_AnimationMapAngle);
        }


        private static void MapDoubleClick(Map map, MouseButtonEventArgs args)
        {
            bool isShiftDown = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
            int adjustment = isShiftDown ? -1 : 1;

            Point pixel = args.GetPosition(map);
            map.ZoomOnPixelPoint(pixel, adjustment);
        }


        private void UpdateCopyrights()
        {
            if (_LicenseContainer != null)
            {
                _LicenseContainer.Children.Clear();

                //var copyrights = new List<UIElement>();
                if (BaseLayer.Source != null)
                {
                    UIElement copyright = BaseLayer.Source.GetCopyright();
                    if (copyright != null)
                    {
                        _LicenseContainer.Children.Add(new ContentControl {Content = copyright});
                    }
                }

                foreach (ITileSource  overlay in BaseLayer.Overlays)
                {
                    UIElement copyright = overlay.GetCopyright();
                    if (copyright != null)
                    {
                        _LicenseContainer.Children.Add(new ContentControl {Content = copyright});
                    }
                }
            }
        }

        /// <summary>
        /// Walks up the visual tree from the element supplied and gets the actual Map Instance
        /// Used by any control in the template that need to have access to the Map
        /// </summary>
        /// <param name="element">The current context element, typically "this"</param>
        /// <returns>The actual instance of the Map control</returns>
        public static Map GetMapInstance(FrameworkElement element)
        {
            Map mapInst;
            FrameworkElement parent = element;
            while (parent != null && parent is Map == false)
            {
                parent = (FrameworkElement) VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
            {
                mapInst = (Map) parent;
            }
            else
            {
                //Try secondary method to get instance
                mapInst = DefaultInstance;
            }
            return mapInst;
        }


        private void UpdateClippingArea()
        {
            if (ActualHeight > 0 && ActualWidth > 0)
            {
                var clip = new RectangleGeometry {Rect = new Rect(new Point(0, 0), new Size(ActualWidth, ActualHeight))};
                Clip = clip;
            }
        }

        private void Layers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsMapLoaded)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            int index = e.NewStartingIndex;
                            foreach (object obj in e.NewItems)
                                if (obj is ILayer)
                                {
                                    //Set the layers MapInstance just in case the layer is created outside the Maps Visual Tree.
                                    //This is a performance improvement because layers do not have to walk the visual tree for MapInstance
                                    ILayer layer = (ILayer)e.NewItems[0];
                                    layer.MapInstance = this;
                                    _MapLayerGrid.Children.Insert(index, obj as UIElement);
                                    index++;
                                }
                            break;
                        }
                    case NotifyCollectionChangedAction.Remove:
                        {
                            foreach (object obj in e.OldItems)
                            {
                                if (obj is UIElement)
                                {
                                    var removalItem = obj as UIElement;
                                    if (_MapLayerGrid.Children.Contains(removalItem))
                                    {
                                        _MapLayerGrid.Children.RemoveAt(e.OldStartingIndex);
                                    }
                                }
                            }
                            break;
                        }
                }
            }
            UpdateCopyrights();
        }
    }
}