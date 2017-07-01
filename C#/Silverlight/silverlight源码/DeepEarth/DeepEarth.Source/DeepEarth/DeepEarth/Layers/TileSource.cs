// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DeepEarth.Layers
{

    /// <summary>
    /// <para>
    /// A generic TileSource for a TileLayer
    /// The Class is currently modelled around the Majority of TileSources available on the web, for exmaple Virtual Earth
    /// You need to inherit from this class to creat your own custom TileSource provider for DeepEarth
    /// </para>
    /// <example>
    /// 
    /// <code title="Inherit from this class and create a Blue Marble TileSource using hosted tiles on Amazon S3">   
    /// public class BmTileSource : TileSource 
    /// {
    ///    private const string TilePathBlueMarbleWeb = @"http://s3.amazonaws.com/com.modestmaps.bluemarble/{0}-r{2}-c{1}.jpg";
    ///
    ///    private bool _IsTileDownloadStarted;
    ///
    ///    //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
    ///    public BmTileSource(): base()
    ///    {
    ///    }
    ///
    ///    public override bool IsInitialized
    ///    {
    ///        get { return true; }
    ///    }
    ///
    ///    public override Color TileColor
    ///    {
    ///        get
    ///        {
    ///            return Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A);
    ///        }
    ///    }
    ///
    ///    public override UIElement GetCopyright()
    ///    {
    ///        const string logoPath = @"http://media.amazonwebservices.com/Powered-by-Amazon-Web-Services.jpg";
    ///        return new Image { Source = new BitmapImage(new Uri(logoPath)), Stretch = Stretch.None };
    ///    }
    ///
    ///
    ///    public override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
    ///    {
    ///        if (IsInitialized)
    ///        {
    ///            int zoom = TileToZoom(tileLevel);
    ///            _IsTileDownloadStarted = true;
    ///
    ///            string url = TilePathBlueMarbleWeb;
    ///            url = string.Format(url, zoom, tilePositionX, tilePositionY);
    ///            return new Uri(url);
    ///        }
    ///        return null;
    ///    }
    ///
    ///    public override event EventHandler InitializeCompleted;
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class TileSource : MultiScaleTileSource, ITileSource
    {
        private const int DefaultMinZoomLevel = 1;
        private const int DefaultMaxZoomLevel = 19;
        private const int DefaultTileSize = 256;
        private int maxZoomLevel = DefaultMaxZoomLevel;
        private int minZoomLevel = DefaultMinZoomLevel;
        protected Map _Map;

        /// <summary>
        /// A generic TileSource for a TileLayer
        /// </summary>
        public TileSource() : this(Map.DefaultInstance)
        {
        }

        /// <summary>
        /// A generic TileSource for a TileLayer, specific instance of the Map
        /// </summary>
        /// <param name="map">The instance of the Map control for this TileSource</param>
        public TileSource(Map map): base(
                   (int)Math.Pow(2,DefaultMaxZoomLevel) * DefaultTileSize,
                   (int)Math.Pow(2, DefaultMaxZoomLevel) * DefaultTileSize,
                   DefaultTileSize,
                   DefaultTileSize, 
                    0)
        {
            _Map = map;

        }

        /// <summary>
        /// The MultiScaleImage control calls this method to get the URI's for the base layer and all Overlays.
        /// </summary>
        /// <param name="tileLevel">The MSI tile Level</param>
        /// <param name="tilePositionX">The number of tiles from the left (0 based) for this tile level</param>
        /// <param name="tilePositionY">The number of tiles from the top (0 based) for this tile level</param>
        /// <param name="tileSources">A reference to the object to add the layer and Overlay's URI's too</param>
        protected override void GetTileLayers(int tileLevel, int tilePositionX, int tilePositionY, IList<object> tileSources)
        {
            if (IsValidTileLevel(tileLevel))
            {
                tileSources.Add(GetTile(tileLevel, tilePositionX, tilePositionY));


                //Check the TileLayer.Overlays for additional datasources
                if (_Map.BaseLayer.Overlays != null && _Map.BaseLayer.Overlays.Count > 0)
                {
                    foreach (ITileSource source in _Map.BaseLayer.Overlays)
                    {
                        //Add additional images sources if available
                        Uri uri = source.GetTile(tileLevel, tilePositionX, tilePositionY);
                        if (uri != null) tileSources.Add(uri);
                    }
                }


                //Check the Map.Layers collection for ITileSource objects.
                if (_Map.Layers != null && _Map.Layers.Count > 0)
                {
                    foreach (ILayer me in _Map.Layers)
                    {
                        if (me is ITileSource)
                        {
                            //Add additional images sources if available
                            Uri uri = (me as ITileSource).GetTile(tileLevel, tilePositionX, tilePositionY);
                            if (uri != null) tileSources.Add(uri);
                        }
                    }
                }
            }
        }


        public virtual Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            return null;
        }

        public virtual UIElement GetCopyright()
        {
            return null;
        }

        public virtual event EventHandler InitializeCompleted;

        /// <summary>
        /// Flag to say the Source is ready, default true.
        /// This is useful where you must make another call to get the required data for the Source before it is ready, 
        /// eg Virtual Earth's tokens.
        /// </summary>
        public virtual bool IsInitialized { get { if (InitializeCompleted != null) InitializeCompleted(this, null); return true; } }

        /// <summary>
        /// The background Color before tiles are rendered and where tiles won't be rendered.
        /// </summary>
        public virtual Color TileColor { get { return Colors.Transparent; } }


        #region ILayer Members

        /// <summary>
        /// A unique identifier for the Layer
        /// </summary>
        public string ID{get; set;}

        /// <summary>
        /// The full geographical extent supported by this tile source. (Longitude, Latitude) 
        /// </summary>
        public virtual Rect CoordinateBounds
        {
            get { return new Rect(new Point(MinLongitude, MinLatitude), new Point(MaxLongitude, MaxLatitude)); }
        }

        /// <summary>
        /// The size of the tiles used in this layer, default is 256px square.
        /// </summary>
        public virtual Size TileSize
        {
            get { return new Size(DefaultTileSize, DefaultTileSize); }
        }

        /// <summary>
        /// The minimum Latitude this tile source supports.
        /// </summary>
        public virtual double MinLatitude
        {
            get { return Constants.EarthMinLatitude; }
        }

        /// <summary>
        /// The maximum Latitude this tile source supports.
        /// </summary>
        public virtual double MaxLatitude
        {
            get { return Constants.EarthMaxLatitude; }
        }

        /// <summary>
        /// The minimum Longitude this tile source supports.
        /// </summary>
        public virtual double MinLongitude
        {
            get { return Constants.EarthMinLongitude; }
        }

        /// <summary>
        /// The maximum Longitude this tile source supports.
        /// </summary>
        public virtual double MaxLongitude
        {
            get { return Constants.EarthMaxLongitude; }
        }

        /// <summary>
        /// The Maximum Pixel width and height of the Tile Source when fully zoomed in.
        /// </summary>
        public Size MaxMapSize
        {
            get { return MapSizeAtTileLevel(MaxZoomLevel); }
        }

        /// <summary>
        /// Does the supplied tile Level fall within the range of valid levels?
        /// </summary>
        /// <param name="tileLevel">The proposed level</param>
        /// <returns>True if it is valid else false</returns>
        public virtual bool IsValidTileLevel(int tileLevel)
        {
            int zoomLevel = TileToZoom(tileLevel);
            return zoomLevel >= MinZoomLevel && zoomLevel <= MaxZoomLevel;
        }

        /// <summary>
        /// Does the supplied zoom Level fall within the range of valid levels?
        /// </summary>
        /// <param name="zoomLevel">The proposed level</param>
        /// <returns>True if it is valid else false</returns>
        public virtual bool IsValidZoomLevel(double zoomLevel)
        {
            bool isValidZoom = (zoomLevel < MinZoomLevel || zoomLevel > MaxZoomLevel) == false;
            return isValidZoom;
        }

        /// <summary>
        /// Does the supplied geographical point fall within the range of the Tile Source. 
        /// </summary>
        /// <param name="geoPoint">The geographical point to test (Longitude, Latitude)</param>
        /// <returns>True if it is valid else false</returns>
        public virtual bool IsValidGeoPoint(Point geoPoint)
        {
            return CoordinateBounds.Contains(geoPoint);
        }

        /// <summary>
        /// Converts a tile level to a zoom level
        /// </summary>
        /// <param name="tileLevelDetail">The tile level (2^n = pixel width)</param>
        /// <returns>The zoom level</returns>
        public virtual int TileToZoom(int tileLevelDetail)
        {
            return tileLevelDetail - 8;
        }

        /// <summary>
        /// Converts a tile level to a zoom level
        /// </summary>
        /// <param name="zoomLevel">The zoom level</param>
        /// <returns>The tile level (2^n = pixel width)</returns>
        public virtual int ZoomToTile(double zoomLevel)
        {
            return Convert.ToInt32(zoomLevel) + 8;
        }

        /// <summary>
        /// The pixel size of the Tile Source at the supplied Zoomlevel
        /// For example 256px tiles at tilelevel 2 (4 tiles wide) is 1024px
        /// </summary>
        /// <param name="tileLevel">The tileLevel required</param>
        /// <returns>The Width and Height of the Tile Source</returns>
        public Size MapSizeAtTileLevel(int tileLevel)
        {
            uint u = (uint)TileSize.Width << tileLevel;
            return new Size(u, u);
        }

        /// <summary>
        /// Gets the closest valid Zoom Level for the one supplied
        /// </summary>
        /// <param name="zoomLevel">The proposed zoom level</param>
        /// <returns>The closest valid zoom level</returns>
        public virtual double GetValidatedZoomLevel(double zoomLevel)
        {
            if (zoomLevel < MinZoomLevel)
            {
                return MinZoomLevel;
            }
            if (zoomLevel > MaxZoomLevel)
            {
                return MaxZoomLevel;
            }
            return zoomLevel;
        }

        /// <summary>
        /// Gets the closest valid geographical point for the one supplied (longitude, latitude)
        /// </summary>
        /// <param name="geoPoint">The proposed geographical point (longitude, latitude)</param>
        /// <returns>The closest valid geographical point (longitude, latitude)</returns>
        public virtual Point GetValidatedGeoPoint(Point geoPoint)
        {
            Point bestGeoPoint;
            if (IsValidGeoPoint(geoPoint))
            {
                bestGeoPoint = geoPoint;
            }
            else
            {
                //Additional buffer makes the an exeeded view bounce back on animation and fight back on drags.
                bestGeoPoint = new Point();
                bestGeoPoint.X = Math.Max(geoPoint.X, MinLongitude);
                bestGeoPoint.Y = Math.Max(geoPoint.Y, MinLatitude);
                bestGeoPoint.X = Math.Min(bestGeoPoint.X, MaxLongitude);
                bestGeoPoint.Y = Math.Min(bestGeoPoint.Y, MaxLatitude);


                if (double.IsNaN(bestGeoPoint.X) || double.IsNaN(bestGeoPoint.Y))
                {
                    bestGeoPoint = new Point(0, 0);
                }
            }
            return bestGeoPoint;
        }

        /// <summary>
        /// The minimum ZoomLevel this tile source supports.
        /// </summary>
        public virtual int MinZoomLevel
        {
            get { return minZoomLevel; }
            set{ minZoomLevel = value;}
        }

        /// <summary>
        /// The maximum ZoomLevel this tile source supports.
        /// </summary>
        public virtual int MaxZoomLevel
        {
            get { return maxZoomLevel; }
            set{maxZoomLevel = value;}
        }


        #endregion

    }
}