/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeepEarth.Layers;

namespace DeepEarth.Provider.GooglePlanets
{
    public enum GpMapModes
    {
        GoogleMoon,
        GoogleMoonClemBw,
        GoogleMoonTerrain,
        GoogleMarsInfraRed,
        GoogleMarsElevation,
        GoogleMarsVisible
    }

    public class GpTileSource : TileSource 
    {
        private const string TilePathGoogleMarsE = @"http://mw1.google.com/mw-planetary/mars/elevation/t{0}.jpg";
        private const string TilePathGoogleMarsIR = @"http://mw1.google.com/mw-planetary/mars/infrared/t{0}.jpg";
        private const string TilePathGoogleMarsV = @"http://mw1.google.com/mw-planetary/mars/visible/t{0}.jpg";
        private const string TilePathGoogleMoon = @"http://mw1.google.com/mw-planetary/moon/t{0}.jpg";
        private const string TilePathGoogleMoonC = "http://mw1.google.com/mw-planetary/lunar/lunarmaps_v1/clem_bw/{0}/{1}/{2}.jpg";
        private const string TilePathGoogleMoonT = "http://mw1.google.com/mw-planetary/lunar/lunarmaps_v1/terrain/{0}/{1}/{2}.jpg";
        private bool _IsInitialized;

        private bool _IsTileDownloadStarted;
        private GpMapModes _MapMode;

        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public GpTileSource(): base()
        {
        }

        public GpTileSource(GpMapModes mode) : base()
        {
            MapMode = mode;
        }

        public GpMapModes MapMode
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
                Color baseColor = Colors.Gray;
                switch (_MapMode)
                {
                        //case MapMode.GoogleMoon: baseColor = Colors.Gray;
                        //case MapMode.GoogleMoonClemBw: baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A); break;
                    case GpMapModes.GoogleMoonTerrain:
                        baseColor = Color.FromArgb(0xFF, 0x8F, 0x91, 0xD0);
                        break;
                        //case MapMode.GoogleMarsInfraRed: baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A); break;
                    case GpMapModes.GoogleMarsElevation:
                        baseColor = Color.FromArgb(0xFF, 0x36, 0xA4, 0xD9);
                        break;
                        //case MapMode.GoogleMarsVisible: baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A); break;
                }
                return baseColor;
            }
        }


        public override UIElement GetCopyright()
        {
            const string logoPath = "http://upload.wikimedia.org/wikipedia/en/9/9a/Google_maps_logo.png";
            return new Image { Source = new BitmapImage(new Uri(logoPath)), MaxHeight = 48, MaxWidth = 96, Stretch = Stretch.Uniform };
        }


        protected static string QuadKeyNumberToAlpha(string base4)
        {
            return base4.Replace('0', 'q').Replace('1', 'r').Replace('2', 't').Replace('3', 's');
        }

        public override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            if (IsInitialized)
            {
                int zoom = TileToZoom(tileLevel);
                _IsTileDownloadStarted = true;

                string url = string.Empty;

                switch (MapMode)
                {
                    case GpMapModes.GoogleMoon:
                        url = QuadKeyNumberToAlphaUrl(TilePathGoogleMoon, tilePositionX, tilePositionY, zoom);
                        break;
                    case GpMapModes.GoogleMoonClemBw:
                        url = XYZUrl(TilePathGoogleMoonC, tilePositionX, tilePositionY, zoom);
                        break;
                    case GpMapModes.GoogleMoonTerrain:
                        url = XYZUrl(TilePathGoogleMoonT, tilePositionX, tilePositionY, zoom);
                        break;
                    case GpMapModes.GoogleMarsInfraRed:
                        url = QuadKeyNumberToAlphaUrl(TilePathGoogleMarsIR, tilePositionX, tilePositionY, zoom);
                        break;
                    case GpMapModes.GoogleMarsElevation:
                        url = QuadKeyNumberToAlphaUrl(TilePathGoogleMarsE, tilePositionX, tilePositionY, zoom);
                        break;
                    case GpMapModes.GoogleMarsVisible:
                        url = QuadKeyNumberToAlphaUrl(TilePathGoogleMarsV, tilePositionX, tilePositionY, zoom);
                        break;
                }

                return new Uri(url);
            }
            return null;
        }

        private static string XYZUrl(string url, int tilePositionX, int tilePositionY, int zoom)
        {
            tilePositionY = (Convert.ToInt32(Math.Pow(2.0, zoom)) - tilePositionY) - 1;
            url = string.Format(url, zoom, tilePositionX, tilePositionY);

            return url;
        }

        private static string QuadKeyNumberToAlphaUrl(string url, int tilePositionX, int tilePositionY, int zoom)
        {
            string quadKey = TileXYToQuadKey(tilePositionX, tilePositionY, zoom);
            string str3 = quadKey.Substring(quadKey.Length - 1, 1);
            string str4 = QuadKeyNumberToAlpha(quadKey);

            url = string.Format(url, str4, str3);
            return url;
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