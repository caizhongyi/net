// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Browser;

namespace DeepEarth.Layers
{

    /// <summary>
    /// <para>
    /// Helper class to transform Logical, Pixel and Geographics coordinates
    /// </para>
    /// <list type="bullet">
    /// <item>Logical - essentially the object is 1 unit, halfway is 0.5</item>
    /// <item>Pixel - Screen pixels, can be relative from the top left of the screen 
    /// or absolute from the top left of the object</item>
    /// <item>Geometry - Longitude, Latitude</item>
    /// </list>
    /// <example>
    /// 
    /// <code title="Transform a Pixel coordinate to a Geographical location"> 
    /// //Within our Map class we have this as a public property:
    /// 
    ///    private CoordTransform _CoordHelper;  
    ///    public CoordTransform CoordHelper
    ///    {
    ///        get
    ///        {
    ///            if (_CoordHelper == null)
    ///            {
    ///                _CoordHelper = new CoordTransform(this);
    ///            }
    ///            return _CoordHelper;
    ///       }
    ///    }
    /// 
    /// //then your code, assuming the map is "_map"
    /// Point geo = _map.CoordHelper.PixelToGeo(new Point(300,400));
    /// </code>
    /// </example>
    /// </summary>
    public class CoordTransform
    {
        private readonly Map _Map;

        /// <summary>
        /// Helper class to transform Logical, Pixel and Geographics coordinates
        /// </summary>
        /// <param name="map">The instance of the current Map object to perform the conversions on</param>
        public CoordTransform(Map map)
        {
            _Map = map;
        }

        #region 2D Point Transformations

        /// <summary>
        /// Convert a Geographical Point (longitude, latitude) to a logic point on the current map.
        /// </summary>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <returns>The logical Point</returns>
        public Point GeoToLogical(Point geographicPoint)
        {
            Point logical = GeographicToLogical(_Map.SpatialReference, geographicPoint);
            return logical;
        }

        /// <summary>
        /// Convert a Geographical Point (longitude, latitude) to a pixel point on the current screen.
        /// </summary>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <returns>Pixel Point</returns>
        public Point GeoToPixel(Point geographicPoint)
        {
            Point logical = GeoToLogical(geographicPoint);
            Point screen = LogicalToPixel(logical);
            return screen;
        }

        /// <summary>
        /// Convert a logic point to a Geographical Point (longitude, latitude) on the current map.
        /// </summary>
        /// <param name="logicalPoint">The logical Point</param>
        /// <returns>Geographical Point (longitude, latitude)</returns>
        public Point LogicalToGeo(Point logicalPoint)
        {
            Point geographic = LogicalToGeographic(_Map.SpatialReference, logicalPoint);
            return geographic;
        }

        /// <summary>
        /// Convert a logic point to a Pixel Point on the current screen at a particular zoom level.
        /// </summary>
        /// <param name="logicalPoint">The logical Point</param>
        /// <returns>Pixel Point</returns>
        public Point LogicalToPixel(Point logicalPoint)
        {
            //Note:  Below is the calculation of avoid the rubberband effect caused by the MSI Animation.
            //The code below will keep the pins snapped to the correct location on MSI.Spring animations.
            //code used to be: return _Msi.LogicalToElementPoint(logicalPoint);

            double scaleFactor = _Map.ActualWidth;
            var offset = new Point(-_Map.LogicalOrigin.X / _Map.MapViewLogicalSize.Width, -_Map.LogicalOrigin.Y / _Map.MapViewLogicalSize.Width);
            double zoomFactor = Math.Log(1 / _Map.MapViewLogicalSize.Width, 2);

            var interimPoint = new Point(offset.X + Math.Pow(2, zoomFactor) * logicalPoint.X, offset.Y + Math.Pow(2, zoomFactor) * logicalPoint.Y);
            var pixel = new Point(interimPoint.X * scaleFactor, interimPoint.Y * scaleFactor);
            return pixel;
        }

        /// <summary>
        /// Convert a pixel point to a Geographical Point (longitude, latitude) on the current screen.
        /// </summary>
        /// <param name="screenPoint">Pixel Point</param>
        /// <returns>Geographical Point (longitude, latitude)</returns>
        public Point PixelToGeo(Point screenPoint)
        {
            Point logical = PixelToLogical(screenPoint);
            Point geographic = LogicalToGeo(logical);
            return geographic;
        }

        /// <summary>
        /// Convert a pixel point to a Logical Point on the current screen.
        /// </summary>
        /// <param name="pixel">Pixel Point</param>
        /// <returns>The logical Point</returns>
        public Point PixelToLogical(Point pixel)
        {
            Point offset = _Map.LogicalOrigin;
            double pixelFactorX = _Map.MapViewLogicalSize.Width  / _Map.MapViewPixelSize.Width;
            double pixelFactorY = _Map.MapViewLogicalSize.Height / _Map.MapViewPixelSize.Height;

            Point logical = new Point((pixel.X * pixelFactorX) + offset.X, (pixel.Y * pixelFactorY) + offset.Y);
            return logical;
        }

        internal Point PixelToLogicalIncRotation(Point pixel)
        {
            if (_Map.IsMapLoaded)
            {
                //consider rotation
                return PixelToLogical(RotatePixelbyMapRotation(pixel));
            }
            return new Point(0.5, 0.5);
        }

        /// <summary>
        /// Rotates a Pixel coordinate by the current map rotation
        /// </summary>
        /// <param name="point">Pixel Point</param>
        /// <returns>Pixel Point</returns>
        public Point RotatePixelbyMapRotation(Point point)
        {
            double a = (Math.PI / 180) * _Map.RotationAngle;
            double x = point.X * Math.Cos(a) + point.Y * Math.Sin(a);
            double y = -point.X * Math.Sin(a) + point.Y * Math.Cos(a);
            return new Point(x, y);
        }

        #endregion

        #region 3D Point Transformations

        /// <summary>
        /// Converts a pixel point to a Geographical Point (longitude, latitude) at the supplied Zoom Level.
        /// </summary>
        /// <param name="screenPoint">Pixel Point</param>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <returns>Geographical Point (longitude, latitude)</returns>
        public Point PixelToGeoAtZoom(Point screenPoint, int zoomLevel)
        {
            const double projectionOffset = Constants.ProjectionOffset;
            double c = Constants.EarthCircumference/((1 << zoomLevel)*TileSize.Width);
            double f = screenPoint.X*c - projectionOffset;
            double g = projectionOffset - screenPoint.Y*c;

            double latitude = RadiansToDegrees(Math.PI/2 - 2*Math.Atan(Math.Exp(-g/Constants.EarthRadius)));
            double longitude = RadiansToDegrees(f/Constants.EarthRadius);

            return Clip(new Point(longitude, latitude));
        }

        /// <summary>
        /// Convert a Geographical Point (longitude, latitude) to a pixel point at the supplied Zoom Level.
        /// </summary>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <returns>Pixel Point</returns>
        public Point GeoToPixelAtZoom(Point geographicPoint, int zoomLevel)
        {
            const double projectionOffset = Constants.ProjectionOffset;
            double e = Math.Sin(DegreesToRadians(geographicPoint.Y));
            double g = Constants.EarthRadius*DegreesToRadians(geographicPoint.X);
            double h = Constants.EarthRadius/2*Math.Log((1 + e)/(1 - e));
            double c = Constants.EarthCircumference/((1 << zoomLevel)*TileSize.Width);

            double pixelX = (projectionOffset + g)/c;
            double pixelY = (projectionOffset - h)/c;

            return new Point(pixelX, pixelY);
        }

        #endregion

        #region LogicalView to ZoomLevel Conversions

        /// <summary>
        /// Gets the Logical Viewport for the supplied zoomlevel maintaining the centre point of the map
        /// </summary>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <returns>The Logical Viewport</returns>
        public Size ZoomLevelToLogicalView(double zoomLevel)
        {
            //If the zoom is invalid, do nothing. 
            Map map = _Map;
            TileSource bl = map.BaseLayer.Source;
            double targetWidth = map.MapViewPixelSize.Width/bl.TileSize.Width/Math.Pow(2.0, zoomLevel);
            double targetHeight = targetWidth*(map.MapViewPixelSize.Height/map.MapViewPixelSize.Width);
            Size viewPort = new Size(targetWidth, targetHeight);
            return viewPort;
        }

        /// <summary>
        /// Gets the Zoomlevel required to fit the supplied Logical Viewport
        /// </summary>
        /// <param name="viewPort">Logical Viewport</param>
        /// <returns>The zoom level to fit</returns>
        public double LogicalViewToZoomLevel(Size viewPort)
        {
            Map map = _Map;
            TileSource baseLayer = map.BaseLayer.Source;
            double targetZoom = Math.Log(map.MapViewPixelSize.Width/baseLayer.TileSize.Width/viewPort.Width, 2);
            return targetZoom;
        }

        #endregion

        #region GetScaleAtZoomLevel

        /// <summary>
        /// Gets the scale of the map at the supplied zoom level at the equator in the unit provided
        /// </summary>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The scale value</returns>
        public double GetScaleAtZoomLevel(double zoomLevel, Unit unit)
        {
            return GetScaleAtZoomLevel(0, zoomLevel, unit);
        }

        /// <summary>
        /// Gets the scale of the map at the supplied zoom level at the supplied Geographical Point in the unit provided
        /// </summary>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The scale value</returns>
        public double GetScaleAtZoomLevel(double zoomLevel, Point geographicPoint, Unit unit)
        {
            return GetScaleAtZoomLevel(geographicPoint.Y, zoomLevel, unit);
        }

        /// <summary>
        /// Gets the scale of the map at the supplied zoom level at the supplied Latitude in the unit provided
        /// </summary>
        /// <param name="latitude">The Line of Latitude to calculate at</param>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The scale value</returns>
        public double GetScaleAtZoomLevel(double latitude, double zoomLevel, Unit unit)
        {
            return GetScaleAtZoomLevel(latitude, zoomLevel, GetScreenDPI(), unit);
        }

        /// <summary>
        /// Gets the scale of the map at the supplied zoom level at the supplied Latitude 
        /// at the supplied Sceen DPI in the unit provided
        /// </summary>
        /// <param name="latitude">The Line of Latitude to calculate at</param>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="screenDpi">The Dots Per Inch of the current Screen</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The scale value</returns>
        public double GetScaleAtZoomLevel(double latitude, double zoomLevel, double screenDpi, Unit unit)
        {
            double resolution = GetResolutionAtZoomLevel(latitude, zoomLevel, unit);
            double scale = resolution*screenDpi;

            double unitPerInch = ConvertUnit(1, Unit.Inch, unit);
            return scale/unitPerInch;
        }

        private static double GetScreenDPI()
        {
            if (HtmlPage.IsEnabled)
            {
                var screen = HtmlPage.Window.GetProperty("screen") as ScriptObject;
                if (screen != null)
                {
                    object o = screen.GetProperty("deviceYDPI");
                    if (o != null)
                    {
                        return (double) o;
                    }
                }
            }
            return 96;
        }

        #endregion

        #region GetResolutionAtZoomLevel

        /// <summary>
        /// Gets the resolution of a pixel at the equator at the supplied zoom level in the unit provided
        /// </summary>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The resolution value</returns>
        public double GetResolutionAtZoomLevel(double zoomLevel, Unit unit)
        {
            return GetResolutionAtZoomLevel(0, zoomLevel, unit);
        }

        /// <summary>
        /// Gets the resolution of a pixel at the supplied Geographical Point (longitude, latitude) 
        /// at the supplied zoom level in the unit provided
        /// </summary>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The resolution value</returns>
        public double GetResolutionAtZoomLevel(double zoomLevel, Point geographicPoint, Unit unit)
        {
            return GetResolutionAtZoomLevel(geographicPoint.Y, zoomLevel, unit);
        }

        /// <summary>
        /// Gets the resolution of a pixel at the supplied latitude at the supplied zoom level in the unit provided
        /// </summary>
        /// <param name="latitude">The Line of Latitude to calculate at</param>
        /// <param name="zoomLevel">The zoom level of the map</param>
        /// <param name="unit">The unit of measure to use</param>
        /// <returns>The resolution value</returns>
        public double GetResolutionAtZoomLevel(double latitude, double zoomLevel, Unit unit)
        {
            double mapWidth = _Map.BaseLayer.Source.MapSizeAtTileLevel(Convert.ToInt32(zoomLevel)).Width;
            double earthRadius = ConvertUnit(Constants.EarthRadius, Unit.Meter, unit);
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            return Math.Cos(latitude*Math.PI/180)*2*Math.PI*earthRadius/mapWidth;
        }

        #endregion

        #region Static Math Calculations

        /// <summary>
        /// Converts a unit of measure to another unit of measure
        /// </summary>
        /// <param name="sourceValue">The value to convert</param>
        /// <param name="sourceUnit">The source unit of measure</param>
        /// <param name="destinationUnit">The unit of measure to convert to</param>
        /// <returns>The converted unit value</returns>
        public static double ConvertUnit(double sourceValue, Unit sourceUnit, Unit destinationUnit)
        {
            if (sourceUnit != destinationUnit)
            {
                switch (sourceUnit)
                {
                    case Unit.Kilometer:
                        sourceValue *= 1000;
                        break;

                    case Unit.Inch:
                        sourceValue *= Constants.INCH_TO_METER;
                        break;

                    case Unit.Mile:
                        sourceValue *= Constants.MILE_TO_METER;
                        break;
                }

                switch (destinationUnit)
                {
                    case Unit.Kilometer:
                        sourceValue /= 1000;
                        break;

                    case Unit.Inch:
                        sourceValue *= Constants.METER_TO_INCH;
                        break;

                    case Unit.Mile:
                        sourceValue *= Constants.METER_TO_MILE;
                        break;
                }
            }

            return sourceValue;
        }

        /// <summary>
        /// Converts Radians to Degrees
        /// </summary>
        /// <param name="r">Value in Radians</param>
        /// <returns>Value in Degrees</returns>
        public static double RadiansToDegrees(double r)
        {
            return (180/Math.PI)*r;
        }

        /// <summary>
        /// Converts Degrees to Radians
        /// </summary>
        /// <param name="d">Value in Degrees</param>
        /// <returns>Value in Radians</returns>
        public static double DegreesToRadians(double d)
        {
            return (Math.PI/180)*d;
        }

        /// <summary>
        /// Reduces the number to fit within the supplied bounds
        /// </summary>
        /// <param name="n">The number to clip</param>
        /// <param name="minValue">The minimal allowed value</param>
        /// <param name="maxValue">The maximum allowed value</param>
        /// <returns>The clipped value</returns>
        public static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>
        /// Reduces the Geographical Point to fit within the bounds of the Earth.
        /// </summary>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <returns>Cliped Geographical Point (longitude, latitude)</returns>
        public static Point Clip(Point geographicPoint)
        {
            geographicPoint.X = Clip(geographicPoint.X, Constants.EarthMinLongitude, Constants.EarthMaxLongitude);
            geographicPoint.Y = Clip(geographicPoint.Y, Constants.EarthMinLatitude, Constants.EarthMaxLatitude);
            return geographicPoint;
        }

        #endregion

        #region BaseLayer accessors


        private double MinLatitude
        {
            get { return _Map.BaseLayer.Source.MinLatitude; }
        }

        private double MaxLatitude
        {
            get { return _Map.BaseLayer.Source.MaxLatitude; }
        }


        private Size TileSize
        {
            get { return _Map.BaseLayer.Source.TileSize; }
        }

        #endregion

        #region General Coordinate Transformations
        /*
         * Future work:  Add transformation support from one datum & projection to another.
         * This will become very important with upcoming support for geometric shapes and other,
         * non-WGS WMS data providers.  Currently, all sources are in WGS, so default to Virtual
         * Earth's WGS Mercator Projection
         */

        /// <summary>
        /// Convert a logic point to a Geographical Point (longitude, latitude) for the specified spatial reference.
        /// </summary>
        /// <param name="srs">The spatial reference to use</param>
        /// <param name="logicalPoint">Logical Point</param>
        /// <returns>Geographical Point (longitude, latitude)</returns>
        public Point LogicalToGeographic(ISpatialReference srs, Point logicalPoint)
        {
            return srs.LogicalToGeographic(logicalPoint);
        }

        /// <summary>
        /// Convert a Geographical Point (longitude, latitude) to a logic point for the specified spatial reference.
        /// </summary>
        /// <param name="srs">The spatial reference to use</param>
        /// <param name="geographicPoint">Geographical Point (longitude, latitude)</param>
        /// <returns>Logical Point</returns>
        public Point GeographicToLogical(ISpatialReference srs, Point geographicPoint)
        {
            return srs.GeographicToLogical(geographicPoint);
        }

        #endregion General Coordinate Transformations

    }
}