/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeepEarth.Provider.VirtualEarth.VEImageryService;

using DeepEarth.Layers;

namespace DeepEarth.Provider.VirtualEarth
{
    public enum VeMapModes
    {
        VeRoad,
        VeAerial,
        VeHybrid,
    }

    public class VeTileSource : TileSource 
    {
        private string ISOKey = "VETilePath.txt";

        private string token;

        private bool _IsInitialized;
        private bool _IsInitializing;
        private bool _IsTileDownloadStarted;
        private VeMapModes _MapMode;
        private TilePathInfo tilePathInfo;



        //Constructor Called by XAML instanciation; Wait for MapMode to be set to initialize services
        public VeTileSource()
        {
            _MapMode = MapMode = VeMapModes.VeHybrid;
            InitVeService();
        }


        public VeTileSource(VeMapModes mode)
        {
            _MapMode = mode;
            ID = _MapMode.ToString();
            InitVeService();
        }


        public VeMapModes MapMode
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

                if (_IsInitialized == false)
                {
                    InitVeService();
                }
            }
        }

        #region BaseLayer Overrides

        public override bool IsInitialized
        {
            get { return _IsInitialized; }
        }

        public override Color TileColor
        {
            get
            {
                Color backColor = Colors.Black;
                switch (_MapMode)
                {
                    case VeMapModes.VeAerial:
                    case VeMapModes.VeHybrid:
                        backColor = Color.FromArgb(0xFF, 0x01, 0x04, 0x13);
                        break;
                    case VeMapModes.VeRoad:
                        backColor = Color.FromArgb(0xFF, 0xB2, 0xC5, 0xD3);
                        break;
                }
                return backColor;
            }
        }


        public override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
        {
            if (IsInitialized)
            {
                int zoom = TileToZoom(tileLevel);
                _IsTileDownloadStarted = true;
                string quadKey = TileXYToQuadKey(tilePositionX, tilePositionY, zoom);
                string url = tilePathInfo.TilePath;
                url = url.Replace("{token}", token);
                url = url.Replace("{culture}", Thread.CurrentThread.CurrentCulture.Name);
                url = url.Replace("{quadkey}", quadKey);
                url = url.Replace("{subdomain}", tilePathInfo.SubDomains[Convert.ToInt32(quadKey.Substring(quadKey.Length - 1))]);
                return new Uri(url);
            }
            return null;
        }

        public override event EventHandler InitializeCompleted;

        protected virtual void onInitialized()
        {
            _IsInitialized = true;
            if (InitializeCompleted != null) InitializeCompleted(this, null);
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

        #endregion

        #region ILicensedDataProvider Members

        public override UIElement GetCopyright()
        {
            const string logoPath = "http://dev.virtualearth.net/mapcontrol/v6.1/i/bin/6.1.20080306152009.13/logo_powered_by.png";
            var img = new Image {Source = new BitmapImage(new Uri(logoPath)), Stretch = Stretch.None};
            return img;
        }

        #endregion

        private void InitVeService()
        {
            // Test if isDesignTime to display in Blend
            if (HtmlPage.IsEnabled && !_IsInitializing)
            {
                _IsInitializing = true;
                ISOKey = _MapMode + ISOKey;
                //get token
                Token.GetToken((o, e) =>
                {
                    token = ((TokenResultArgs)e).Result;
                    //get tilePathInfo from ISO if already there.
                    tilePathInfo = IsolatedStorage.LoadData<TilePathInfo>(ISOKey);
                    if (tilePathInfo == null)
                    {
                        var mapUriRequest = new ImageryMetadataRequest
                        {
                            Credentials = new Credentials { Token = token },
                            Style = GetVEMapStyle(_MapMode)
                        };

                        var imageryService = new ImageryServiceClient();
                        imageryService.GetImageryMetadataCompleted += (oimageryService, eimageryService) =>
                        {
                            tilePathInfo = new TilePathInfo
                                               {
                                                   TilePath = eimageryService.Result.Results[0].ImageUri,
                                                   SubDomains = eimageryService.Result.Results[0].ImageUriSubdomains
                                               };
                            //Store in ISO for next visits
                            IsolatedStorage.SaveData(tilePathInfo, ISOKey);
                            onInitialized();
                        };
                        imageryService.GetImageryMetadataAsync(mapUriRequest);
                    }else
                    {
                        onInitialized();
                    }
                });
            }
        }


        private static MapStyle GetVEMapStyle(VeMapModes mode)
        {
            switch (mode)
            {
                case VeMapModes.VeAerial:
                    return MapStyle.Aerial;
                case VeMapModes.VeHybrid:
                    return MapStyle.AerialWithLabels;
                case VeMapModes.VeRoad:
                    return MapStyle.Road;
            }
            return MapStyle.AerialWithLabels;
        }
    }
}