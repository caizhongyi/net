/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeepEarth.Layers;

namespace DeepEarth.Provider.CloudMade
{
    public enum CmMapModes
    {
        CmWeb,
        CmMobile,
        CmNoNames,
        CmCycle
    }


    public class CmTileSource : TileSource 
    {
        private const string TilePathCycle = "http://{S}.andy.sandbox.cloudmade.com/tiles/cycle/{Z}/{X}/{Y}.png";
        private const string TilePathMobile = "http://{S}.tile.cloudmade.com/BC9A493B41014CAABB98F0471D759707/2/256/{Z}/{X}/{Y}.png";
        private const string TilePathNoNames = "http://{S}.tile.cloudmade.com/BC9A493B41014CAABB98F0471D759707/3/256/{Z}/{X}/{Y}.png";
        private const string TilePathWeb = "http://{S}.tile.cloudmade.com/BC9A493B41014CAABB98F0471D759707/1/256/{Z}/{X}/{Y}.png";
        private readonly Random _Rand = new Random();
        private readonly string[] TilePathPrefixes = new[] {"a", "b", "c"};
        private bool _IsTilesDownloadStarted;
        private CmMapModes _MapMode;


        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public CmTileSource(): base()
        {
        }

        public CmTileSource(CmMapModes mode): base()
        {
            MapMode = mode;
        }


        public CmMapModes MapMode
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

        #region BaseLayer Overrides

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
                    case CmMapModes.CmWeb:
                        baseColor = Color.FromArgb(0xFF, 0x98, 0xBC, 0xDA);
                        break;
                    case CmMapModes.CmMobile:
                        baseColor = Color.FromArgb(0xFF, 0x98, 0xBC, 0xDA);
                        break;
                    case CmMapModes.CmNoNames:
                        baseColor = Color.FromArgb(0xFF, 0x98, 0xBC, 0xDA);
                        break;
                    case CmMapModes.CmCycle:
                        baseColor = Color.FromArgb(0xFF, 0xB4, 0xCF, 0xCF);
                        break;
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
                    case CmMapModes.CmWeb:
                        prefix = TilePathPrefixes[_Rand.Next(3)];
                        url = TilePathWeb;
                        break;
                    case CmMapModes.CmMobile:
                        prefix = TilePathPrefixes[_Rand.Next(3)];
                        url = TilePathMobile;
                        break;
                    case CmMapModes.CmNoNames:
                        prefix = TilePathPrefixes[_Rand.Next(3)];
                        url = TilePathNoNames;
                        break;
                    case CmMapModes.CmCycle:
                        prefix = TilePathPrefixes[_Rand.Next(3)];
                        url = TilePathCycle;
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
            return new Image { Source = new BitmapImage(new Uri(logoPath)), Height = 48 };
        }


    }
}