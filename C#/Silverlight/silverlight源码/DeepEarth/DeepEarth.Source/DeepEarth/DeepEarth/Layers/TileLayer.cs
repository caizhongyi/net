// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DeepEarth.Events;

namespace DeepEarth.Layers
{

    /// <summary>
    /// <para>
    /// A Raster Tile Layer for the map
    /// Creates a MultiScaleImage control to the map control and links up to a tilesource.
    /// The layer has one TileSource and can contain many Overlays (also TileSource)
    /// </para>
    /// <example>
    /// 
    /// <code title="Change the Maps's TileLayer's source to VE Road">   
    /// //Assume that your DeepEarth control is called "map"
    /// map.BaseLayer.Source = new VeTileSource(VeMapModes.VeRoad); 
    /// </code>
    /// </example>
    /// </summary>
    public class TileLayer : Grid, ILayer 
    {
        private MultiScaleImage _Msi;
        private TileSource _TileSource;
        private double _LastViewWidth;

        protected Map _Map;
        protected bool _IsVisible = true;

        /// <summary>
        /// A collection of additional Tilesources to be overlaid on the base TileSource
        /// Unfortunately there is no opacity support for these.
        /// </summary>
        public ObservableCollection<ITileSource> Overlays { get; set; }


        public string ID { get; set; }

        /// <summary>
        /// A Raster Tile Layer for the map
        /// </summary>
        /// <param name="map">Instance of the map in which to create the layer</param>
        public TileLayer(Map map)
        {
            _Map = map;
            _Msi = new MultiScaleImage();
            Children.Add(_Msi);
            _Msi.Loaded += _Msi_Loaded;
            _Msi.LayoutUpdated += (o, e) => CheckViewBounds();
            _Msi.MotionFinished += (o, e) => _Map.Events.ZoomEnded(_Map, new MapEventArgs());
            _LastViewWidth = _Msi.ViewportWidth;
            _Msi.ViewportChanged += _Msi_ViewportChanged;
            Overlays = new ObservableCollection<ITileSource>();
           
            //This null TileSource allows the MSI to size itself correctly and earlier
            _Msi.Source = new TileSource(map);            
        }

        void _Msi_ViewportChanged(object sender, RoutedEventArgs e)
        {            
            CheckViewBounds();
            CheckForZoomStart();
            _Map.Events.ViewChanged(_Map, new MapEventArgs());

        }

        private void CheckForZoomStart()
        {
            double width = Math.Round(_Msi.ViewportWidth, 7);
            if (_LastViewWidth != width)
            {
                _Map.Events.ZoomStarted(_Map, new MapEventArgs());
                _LastViewWidth = width;
            }
        }

        void _Msi_Loaded(object sender, RoutedEventArgs e)
        {
            IsLoaded = true;
            _Map.Events.Loaded(_Map, new MapEventArgs());
        }

        /// <summary>
        /// The actual base TileSource for the Layer.
        /// </summary>
        public TileSource Source 
        {
            get 
            { 
                return (TileSource)_Msi.Source; 
            }
            set
            {
                _TileSource = value;

                if (value == null)
                {
                    _Msi.Source = null;
                }
                else
                {

                    Background = new SolidColorBrush(_TileSource.TileColor);

                    if (_TileSource.IsInitialized)
                    {
                        _Msi.Source = _TileSource;
                        MapInstance.Events.TileSourceChanged(MapInstance, new MapEventArgs());
                    }
                    else
                    {
                        _TileSource.InitializeCompleted += (o, ev) => 
                        { 
                            _Msi.Source = _TileSource;
                            MapInstance.Events.TileSourceChanged(MapInstance, new MapEventArgs());
                        };
                    }

                }
            }
        }

        /// <summary>
        /// Has the TileLayer been Loaded?
        /// Until the TileLayer is loaded some functionlaity will not be available, 
        /// consider subscribing to the Map.Event.MapLoaded event.
        /// </summary>
        public bool IsLoaded { get; set; }

        internal MultiScaleImage MSI { get { return _Msi; } }


        private void CheckViewBounds()
        {
            double currZoom = _Map.ZoomLevel;
            Point geoPoint = _Map.CoordHelper.LogicalToGeo(_Map.LogicalCenter);
            if(Source != null)
            {
                if((Source.IsValidZoomLevel(currZoom) && Source.IsValidGeoPoint(geoPoint)) == false)
                {
                    double bestZoom = Source.GetValidatedZoomLevel(currZoom);
                    Point bestCenter = Source.GetValidatedGeoPoint(geoPoint);
                    _Map.SetViewCenter(bestCenter, bestZoom);
                }
            }
        }


        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                if (_IsVisible != value)
                {
                    _IsVisible = value;
                    OnIsVisibleChanged();
                }
            }
        }



        public Map MapInstance
        {
            get
            {
                if (_Map == null) _Map = Map.GetMapInstance(this);
                return _Map;
            }
            set
            {
                if (_Map != null && !ReferenceEquals(_Map, value))
                {
                    throw new NotSupportedException();
                }
                _Map = value;
            }
        }


        protected virtual void OnIsVisibleChanged()
        {
        }



    }
}

