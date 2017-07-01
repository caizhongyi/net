using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using DeepEarth;
using DeepEarth.Provider.OpenStreetMaps;

namespace OSMDeepEarthExample
{
    public partial class Page
    {
        private readonly OsmTileSource _OsmMapnikTileLayer;
        private readonly OsmTileSource _OsmOsmarendTileLayer;
        private bool _IsCtrlDown;
        private MapSourceControl _SourceControl;

        public Page()
        {
            InitializeComponent();

            map.Events.MapKeyDown += MapKeyDown;
            map.Events.MapKeyUp += MapKeyUp;

            map.Events.MapLoaded += this.Events_MapLoaded;

            _OsmMapnikTileLayer = new OsmTileSource(OsmMapModes.Mapnik);
            _OsmOsmarendTileLayer = new OsmTileSource(OsmMapModes.Osmarend);

            SetMapSource(OsmMapModes.Mapnik);

        }


        private void SetMapSource(OsmMapModes opts)
        {
            switch (opts)
            {
                case OsmMapModes.Mapnik:map.BaseLayer.Source = _OsmMapnikTileLayer; break;
                case OsmMapModes.Osmarend:map.BaseLayer.Source = _OsmOsmarendTileLayer; break;
            }
        }


        void Events_MapLoaded(Map map, DeepEarth.Events.MapEventArgs args)
        {
            _SourceControl = (MapSourceControl)map.Navigation.Toolbar;
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();
            srcList.Add(MapSourceControl.TileSourceIds.Osmarend);
            srcList.Add(MapSourceControl.TileSourceIds.Mapnik);
            _SourceControl.TileSources = srcList;

        }

        private void MapKeyUp(Map _map, KeyEventArgs args)
        {
            if (args.Key == Key.Ctrl) _IsCtrlDown = false;
        }


        private void MapKeyDown(Map _map, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.M:
                    if (_IsCtrlDown) SetMapSource(OsmMapModes.Mapnik);
                    break;
                case Key.D:
                    if (_IsCtrlDown) SetMapSource(OsmMapModes.Osmarend);
                    break;
                case Key.Ctrl:
                    _IsCtrlDown = true;
                    break;
            }
        }
    }
}