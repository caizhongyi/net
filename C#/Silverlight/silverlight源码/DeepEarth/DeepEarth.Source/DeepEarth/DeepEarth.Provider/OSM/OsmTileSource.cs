/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeepEarth.Layers;

namespace DeepEarth.Provider.OpenStreetMaps
{
    public enum OsmMapModes
    {
        Mapnik,
        Osmarend
    }

    public class OsmTileSource : TileSource 
    {
        private const string TilePathMapnik = @"http://{S}.tile.openstreetmap.org/{Z}/{X}/{Y}.png";
        private const string TilePathOsmarend = @"http://{S}.tah.openstreetmap.org/Tiles/tile/{Z}/{X}/{Y}.png";
        private readonly Random _Rand = new Random();
        private readonly string[] TilePathPrefixes = new[] {"a", "b", "c", "d", "e", "f"};
        private bool _IsTilesDownloadStarted;
        private OsmMapModes _MapMode;


        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public OsmTileSource(): base()
        {
        }

        public OsmTileSource(OsmMapModes mode): base()
        {
            MapMode = mode;
        }


        public OsmMapModes MapMode
        {
            get { return _MapMode; }
            set
            {
                if (_IsTilesDownloadStarted)
                {
                    throw new InvalidOperationException();
                }

                _MapMode = value;
                ID = value.ToString();
                _IsInitialized = true;
                if (InitializeCompleted != null) InitializeCompleted(this, null);
            }
        }

        #region TileSource Overrides

        private bool _IsInitialized;

        public override bool IsInitialized
        {
            get { return _IsInitialized; }
        }

        public override Color TileColor
        {
            get
            {
                Color baseColor = Color.FromArgb(0xFF, 0xB9, 0xD1, 0xD1);
                switch (_MapMode)
                {
                    case OsmMapModes.Mapnik: baseColor = Color.FromArgb(0xFF, 0xB4, 0xCF, 0xCF);break;
                    case OsmMapModes.Osmarend:baseColor = Color.FromArgb(0xFF, 0xB5, 0xD6, 0xF1);break;
                }
                return baseColor;
            }
        }


        public override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            if (IsInitialized)
            {
                int zoom = TileToZoom(tileLevel);
                _IsTilesDownloadStarted = true;
                string url = string.Empty;
                string prefix = string.Empty;
                switch (MapMode)
                {
                    case OsmMapModes.Mapnik:
                        prefix = TilePathPrefixes[_Rand.Next(3)];
                        url = TilePathMapnik;
                        break;
                    case OsmMapModes.Osmarend:
                        prefix = TilePathPrefixes[_Rand.Next(6)];
                        url = TilePathOsmarend;
                        break;
                }

                //Randomize to different OSM Servers based on URL prefix
                url = url.Replace("{S}", prefix);
                url = url.Replace("{Z}", zoom.ToString());
                url = url.Replace("{X}", tilePositionX.ToString());
                url = url.Replace("{Y}", tilePositionY.ToString());
                return new Uri(url);
            }
            return null;
        }

        public override event EventHandler InitializeCompleted;

        #endregion


        public override UIElement GetCopyright()
        {
            const string logoPath = "http://www.openstreetmap.org/images/osm_logo.png";
            return new Image { Source = new BitmapImage(new Uri(logoPath)), Height = 48, Width = 48 };
        }


    }
}