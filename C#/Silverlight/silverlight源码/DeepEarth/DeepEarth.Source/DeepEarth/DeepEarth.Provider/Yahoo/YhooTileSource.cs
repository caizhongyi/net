/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeepEarth.Layers;

namespace DeepEarth.Provider.Yahoo
{
    public enum YhooMapModes
    {
        YahooHybrid,
        YahooAerial,
        YahooStreet
    }

    public class YhooTileSource : TileSource 
    {
        private const string TilePathAerial = @"http://us.maps3.yimg.com/aerial.maps.yimg.com/tile?v=1.7&t=a&x={x}&y={y}&z={z}";
        private const string TilePathHybrid = @"http://us.maps3.yimg.com/aerial.maps.yimg.com/png?v=2.2&t=h&x={x}&y={y}&z={z}";
        private const string TilePathStreet = @"http://us.maps2.yimg.com/us.png.maps.yimg.com/png?v=3.52&t=m&x={x}&y={y}&z={z}";
        private bool _IsInitialized;

        private bool _IsTileDownloadStarted;
        private YhooMapModes _MapMode;

        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public YhooTileSource() : base()
        {
        }

        public YhooTileSource(YhooMapModes mode) : base()
        {
            MapMode = mode;
        }


        public YhooMapModes MapMode
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

                if (_MapMode == YhooMapModes.YahooHybrid)
                {
                }

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
                Color baseColor = Colors.Black;
                switch (_MapMode)
                {
                    case YhooMapModes.YahooHybrid:
                        baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A);
                        break;
                    case YhooMapModes.YahooAerial:
                        baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A);
                        break;
                    case YhooMapModes.YahooStreet:
                        baseColor = Color.FromArgb(0xFF, 0x80, 0xB0, 0xE1);
                        break;
                }
                return baseColor;
            }
        }



        public override UIElement GetCopyright()
        {
            //const string logoPath = "http://us.i1.yimg.com/us.yimg.com/i/us/map/aj/yahoo.png";
            const string logoPath = "http://upload.wikimedia.org/wikipedia/en/thumb/a/ad/Yahoo_Logo.svg/201px-Yahoo_Logo.svg.png";
            return new Image { Source = new BitmapImage(new Uri(logoPath)), MaxHeight = 48, MaxWidth = 96, Stretch = Stretch.Uniform };
        }


        public override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            if (IsInitialized)
            {
                int zoom = TileToZoom(tileLevel);
                _IsTileDownloadStarted = true;

                int zoomLevel = 18 - zoom;
                double num4 = Math.Pow(2.0, zoom)/2.0;
                double y;
                if (tilePositionY < num4)
                {
                    y = (num4 - tilePositionY) - 1.0;
                }
                else
                {
                    y = ((tilePositionY + 1) - num4)*-1.0;
                }

                string url = string.Empty;

                switch (MapMode)
                {
                    case YhooMapModes.YahooHybrid:
                        url = TilePathHybrid;
                        break;
                    case YhooMapModes.YahooAerial:
                        url = TilePathAerial;
                        break;
                    case YhooMapModes.YahooStreet:
                        url = TilePathStreet;
                        break;
                }

                url = url.Replace("{z}", zoomLevel.ToString());
                url = url.Replace("{x}", tilePositionX.ToString());
                url = url.Replace("{y}", y.ToString());

                return new Uri(url);
                ;
            }
            return null;
        }

        public override event EventHandler InitializeCompleted;
    }
}