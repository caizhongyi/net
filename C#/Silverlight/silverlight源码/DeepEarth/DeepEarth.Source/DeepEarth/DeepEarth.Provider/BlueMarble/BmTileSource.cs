/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeepEarth.Layers;

namespace DeepEarth.Provider.BlueMarble
{
    public enum BmMapModes
    {
        BlueMarbleWeb,
        BlueMarbleLocal
    }

    public class BmTileSource : TileSource 
    {
        private const string TilePathBlueMarbleWeb = @"http://s3.amazonaws.com/com.modestmaps.bluemarble/{0}-r{2}-c{1}.jpg";
        private readonly string TilePathBlueMarbleLocal = string.Empty;
        private bool _IsInitialized;

        private bool _IsTileDownloadStarted;
        private BmMapModes _MapMode;

        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public BmTileSource(): base()
        {
            // Create Url for local tiles 
            string path = HtmlPage.Document.DocumentUri.AbsolutePath;
            path = path.Substring(0, path.LastIndexOf("/")) + "/ClientBin/bluemarble/{0}-r{2}-c{1}.jpg";
            TilePathBlueMarbleLocal = string.Concat("http://", HtmlPage.Document.DocumentUri.Host, ":", HtmlPage.Document.DocumentUri.Port, path);
        }

        public BmTileSource(BmMapModes mode) : base()
        {
            MapMode = mode;
        }

        public BmMapModes MapMode
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
                Color baseColor = Colors.Black;
                switch (_MapMode)
                {
                    case BmMapModes.BlueMarbleWeb:
                        baseColor = Color.FromArgb(0xFF, 0x1A, 0x44, 0x7A);
                        break;
                    case BmMapModes.BlueMarbleLocal:
                        baseColor = Color.FromArgb(0xFF, 0x1F, 0x44, 0x7A);
                        break;
                }
                return baseColor;
            }
        }


        public override UIElement GetCopyright()
        {
            if (MapMode == BmMapModes.BlueMarbleWeb)
            {
                const string logoPath = @"http://media.amazonwebservices.com/Powered-by-Amazon-Web-Services.jpg";
                return new Image { Source = new BitmapImage(new Uri(logoPath)), Stretch = Stretch.None };
            }
            return null;
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
                    case BmMapModes.BlueMarbleWeb:
                        url = TilePathBlueMarbleWeb;
                        url = string.Format(url, zoom, tilePositionX, tilePositionY);
                        break;

                    case BmMapModes.BlueMarbleLocal:
                        url = TilePathBlueMarbleLocal;
                        int port = Application.Current.Host.Source.Port;
                        url = string.Format(url, zoom, tilePositionX, tilePositionY, port);
                        break;
                }
                return new Uri(url);
            }
            return null;
        }

        public override event EventHandler InitializeCompleted;
    }
}