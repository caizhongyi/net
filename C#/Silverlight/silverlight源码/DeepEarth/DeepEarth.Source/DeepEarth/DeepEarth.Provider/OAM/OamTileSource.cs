/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows.Media;
using DeepEarth.Layers;

namespace DeepEarth.Provider.OpenAerialMaps
{
    public enum OamMapModes
    {
        OAM
    }

    public class OamTileSource : TileSource
    {
        private const string TilePathWms = "http://tile.openaerialmap.org/tiles/1.0.0/openaerialmap-900913/{Z}/{X}/{Y}.jpg";
        private bool _IsTilesDownloadStarted;
        private OamMapModes _MapMode;


        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public OamTileSource() : base()
        {
        }

        public OamTileSource(OamMapModes mode): base()
        {
            MapMode = mode;
        }


        public OamMapModes MapMode
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
                Color baseColor = Colors.Black;
                switch (_MapMode)
                {
                    case OamMapModes.OAM:
                        baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A);
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
                string url = TilePathWms;

                url = url.Replace("{Z}", zoom.ToString());
                url = url.Replace("{X}", tilePositionX.ToString());
                url = url.Replace("{Y}", tilePositionY.ToString());
                return new Uri(url);
            }
            return null;
        }

        public override event EventHandler InitializeCompleted;

        #endregion
    }
}