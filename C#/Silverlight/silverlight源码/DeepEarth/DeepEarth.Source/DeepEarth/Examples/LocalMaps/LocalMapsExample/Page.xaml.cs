using System.Collections;
using System.Collections.Generic;
using System.Windows.Browser;
using System.Windows.Input;
using DeepEarth;
using DeepEarth.Provider.BlueMarble;

namespace LocalMapsExample
{
    public partial class Page
    {
        private readonly BmTileSource _BlueMarbleLocalTileLayer;
        private readonly BmTileSource _BlueMarbleWebTileLayer;
        private bool _IsCtrlDown;
        private MapSourceControl _SourceControl;

        public Page()
        {
            InitializeComponent();

            if (HtmlPage.IsEnabled)
            {
                map.Events.MapKeyDown += Events_MapKeyDown;
                map.Events.MapKeyUp += Events_MapKeyUp;
                map.Events.MapLoaded += this.MapLoaded;

                _BlueMarbleWebTileLayer = new BmTileSource(BmMapModes.BlueMarbleWeb);
                _BlueMarbleLocalTileLayer = new BmTileSource(BmMapModes.BlueMarbleLocal);
                SetMapSource(BmMapModes.BlueMarbleWeb);
            }
        }

        private void SetMapSource(BmMapModes opts)
        {
            switch (opts)
            {
                case BmMapModes.BlueMarbleWeb:map.BaseLayer.Source = _BlueMarbleWebTileLayer;break;
                case BmMapModes.BlueMarbleLocal:map.BaseLayer.Source = _BlueMarbleLocalTileLayer;break;
            }
        }

        void MapLoaded(Map map, DeepEarth.Events.MapEventArgs args)
        {
            _SourceControl = (MapSourceControl)map.Navigation.Toolbar;
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();
            srcList.Add(MapSourceControl.TileSourceIds.BlueMarbleLocal);
            srcList.Add(MapSourceControl.TileSourceIds.BlueMarbleWeb);
            _SourceControl.TileSources = srcList;           
        }


        private void Events_MapKeyUp(Map _map, KeyEventArgs args)
        {
            if (args.Key == Key.Ctrl) _IsCtrlDown = false;
        }

        private void Events_MapKeyDown(Map _map, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.M:
                    if (_IsCtrlDown) SetMapSource(BmMapModes.BlueMarbleWeb);
                    break;
                case Key.D:
                    if (_IsCtrlDown) SetMapSource(BmMapModes.BlueMarbleLocal);
                    break;
                case Key.Ctrl:
                    _IsCtrlDown = true;
                    break;
            }
        }
    }
}