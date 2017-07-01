/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using DeepEarth.Layers;

namespace DeepEarth.Provider.WebMapService
{
    public enum WmsMapModes
    {
        WMS
    }

    public class WmsTileSource : TileSource
    {
        public const int TILE_SIZE = 256;

        // NOAA Observing Systems - OK
        //  private const string TilePath = @"http://map.ngdc.noaa.gov/wmsconnector/com.esri.wms.Esrimap?BBOX={0},{1},{2},{3}&WIDTH={4}&HEIGHT={4}&SRS=EPSG:4326&Layers=Shaded%20Relief&version=1.1.1&service=WMS&FORMAT=PNG&TRANSPARENT=TRUE&request=getmap&ServiceName=nosa";

        // Demis WMS, World Map - OK
        private const string TilePath = @"http://www2.demis.nl/wms/wms.ashx?Service=WMS&WMS=WorldMap&Version=1.1.0&Request=GetMap&SRS=EPSG:4326&BBox={0},{1},{2},{3}&Width={4}&Height={4}&Layers=Countries,Borders,Coastlines&Format=image/png";

        private bool _IsInitialized;

        // Demis WMS, Coast Lines - OK
        // private const string TilePath = @"http://www2.demis.nl/wms/wms.asp?wms=WorldMap&LAYERS=Coastlines&FORMAT=image/png&VERSION=1.1.1&SERVICE=WMS&REQUEST=GetMap&STYLES=&EXCEPTIONS=application/vnd.ogc.se_inimage&SRS=EPSG:4326&BBOX={0},{1},{2},{3}&WIDTH={4}&HEIGHT={4}";

        // OnEarth Landsat WMS - OK
        // private const string TilePath = @"http://onearth.jpl.nasa.gov/wms.cgi?request=GetMap&width=128&height=128&layers=modis&styles=&srs=EPSG:4326&format=image/png&bbox={0},{1},{2},{3}";
        // layers: modis,global_mosaic,daily_terra,daily_planet,worldwind_dem,BMNG,srtm_mag

        // OpenLayers WMS - ok
        // private const string TilePath = @"http://labs.metacarta.com/wms/vmap0?LAYERS=basic&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=&EXCEPTIONS=application/vnd.ogc.se_inimage&FORMAT=image/jpeg&SRS=EPSG:4326&BBOX={0},{1},{2},{3}&WIDTH={4}&HEIGHT={4}";

        // NASA Global Mosaic - ok 
        // private const string TilePath = @"http://t1.hypercube.telascience.org/cgi-bin/landsat7?LAYERS=landsat7&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=&EXCEPTIONS=application/vnd.ogc.se_inimage&FORMAT=image/jpeg&SRS=EPSG:4326&BBOX={0},{1},{2},{3}&WIDTH={4}&HEIGHT={4}";

        // Civil Maps Tile Engine v0.5 - ok
        // private const string TilePath = @"http://maps.civicactions.net/cgi-bin/mapserv?map=/www/sites/maps.civicactions.net/maps/world.map&service=WMS&WMTVER=1.0.0&REQUEST=map&SRS=EPSG:4326&LAYERS=bluemarble,landsat7,lakes,rivers,cities,majorroads,minorroads,tiger_polygon,tiger_landmarks,tiger_lakes,tiger_local_roads,tiger_major_roads,lowboundaries,boundaries,coastlines&FORMAT=image/jpeg&STYLES=&TRANSPARENT=TRUE&WIDTH={4}&HEIGHT=128&BBOX={0},{1},{2},{3}";

        // DM Solutions Group's Maps for MapServer WMS - OK
        // private const string TilePath = @"http://www.mapsherpa.com/cgi-bin/wms_iodra?LAYERS=Bathymetry&FORMAT=image%2Fpng&VERSION=1.1.1&SERVICE=WMS&REQUEST=GetMap&STYLES=&EXCEPTIONS=application%2Fvnd.ogc.se_inimage&SRS=EPSG%3A4326&BBOX={0},{1},{2},{3}&WIDTH={4}&HEIGHT={4}";

        private bool _IsTileDownloadStarted;
        private WmsMapModes _MapMode;

        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public WmsTileSource() : base()
        {
        }

        public WmsTileSource(WmsMapModes mode) : base()
        {
            MapMode = mode;
        }

        public WmsMapModes MapMode
        {
            get { return _MapMode; }
            set
            {
                if (_IsTileDownloadStarted)
                {
                    throw new InvalidOperationException();
                }

                _MapMode = value;
                ID = value.ToString();
                _IsInitialized = true;
                if (InitializeCompleted != null) InitializeCompleted(this, null);
            }
        }

        public override bool IsInitialized
        {
            get { return _IsInitialized; }
        }

        public override Color TileColor
        {
            get
            {
                Color baseColor = Colors.White;
                switch (_MapMode)
                {
                    case WmsMapModes.WMS:
                        baseColor = Colors.White;
                        break;
                }
                return baseColor;
            }
        }


        /// <summary>
        /// Retrieves a quad key from a Virtual Earth tile specifier URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetQuadKey(string url)
        {
            var regex = new Regex(".*tiles/(.+)[.].*");
            Match match = regex.Match(url);

            return match.Groups[1].ToString();
        }

        /// <summary>
        /// Returns the bounding BBox for a grid square represented by the given quad key
        /// </summary>
        /// <param name="quadKey"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="zoomLevel"></param>
        /// <returns></returns>
        public BBox QuadKeyToBBox(string quadKey, int x, int y, int zoomLevel)
        {
            char c = quadKey[0];

            int tileSize = 2 << (18 - zoomLevel - 1);

            if (c == '0')
            {
                y = y - tileSize;
            }

            else if (c == '1')
            {
                y = y - tileSize;
                x = x + tileSize;
            }

            else if (c == '3')
            {
                x = x + tileSize;
            }

            if (quadKey.Length > 1)
            {
                return QuadKeyToBBox(quadKey.Substring(1), x, y, zoomLevel + 1);
            }
            return new BBox(x, y, tileSize, tileSize);
        }

        public BBox QuadKeyToBBox(string quadKey)
        {
            const int x = 0;
            const int y = 262144;
            return QuadKeyToBBox(quadKey, x, y, 1);
        }

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public double RadToDeg(double d)
        {
            return d/Math.PI*180.0;
        }

        /// <summary>
        /// Converts a grid row to Latitude
        /// </summary>
        /// <param name="y"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public double YToLatitudeAtZoom(int y, int zoom)
        {
            double arc = Constants.EarthCircumference/((1 << zoom)*TILE_SIZE);
            double metersY = Constants.HalfEarthCircumference - (y*arc);
            double a = Math.Exp(metersY*2/Constants.EarthRadius);
            double result = RadToDeg(Math.Asin((a - 1)/(a + 1)));
            return result;
        }

        /// <summary>
        /// Converts a grid column to Longitude
        /// </summary>
        /// <param name="x"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public double XToLongitudeAtZoom(int x, int zoom)
        {
            double arc = Constants.EarthCircumference/((1 << zoom)*TILE_SIZE);
            double metersX = (x*arc) - Constants.HalfEarthCircumference;
            double result = RadToDeg(metersX/Constants.EarthRadius);
            return result;
        }

        public override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            if (IsInitialized)
            {
                int zoom = TileToZoom(tileLevel);
                _IsTileDownloadStarted = true;
                string quadKey = TileXYToQuadKey(tilePositionX, tilePositionY, zoom);

                // Use the quadkey to determine a bounding box for the requested tile
                BBox boundingBox = QuadKeyToBBox(quadKey);

                // Get the lat longs of the corners of the box
                double lon = XToLongitudeAtZoom(boundingBox.x*TILE_SIZE, 18);
                double lat = YToLatitudeAtZoom(boundingBox.y*TILE_SIZE, 18);

                double lon2 = XToLongitudeAtZoom((boundingBox.x + boundingBox.width)*TILE_SIZE, 18);
                double lat2 = YToLatitudeAtZoom((boundingBox.y - boundingBox.height)*TILE_SIZE, 18);

                string wmsUrl = string.Format(TilePath, lon, lat, lon2, lat2, TILE_SIZE);

                return new Uri(wmsUrl);
            }
            return null;
        }

        private static string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)
        {
            var quadKey = new StringBuilder();
            for (int i = levelOfDetail; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }
                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }

        public override event EventHandler InitializeCompleted;
    }
}