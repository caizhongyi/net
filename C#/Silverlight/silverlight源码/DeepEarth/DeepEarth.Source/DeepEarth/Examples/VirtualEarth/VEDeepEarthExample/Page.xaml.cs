
using System.Collections.Generic;
using System.Windows.Input;
using DeepEarth;
using DeepEarth.Controls;
using DeepEarth.Provider.VirtualEarth;

namespace VEDeepEarthExample
{
    public partial class Page
    {
        private bool _IsCtrlDown;
        MapSourceControl _SourceControl;

        public Page()
        {
            InitializeComponent();

            map.Events.MapKeyDown += MapKeyDown;
            map.Events.MapKeyUp += MapKeyUp;
            map.Events.MapLoaded += this.MapLoaded;

        }

        private void MapKeyUp(Map _map, KeyEventArgs args)
        {
            if (args.Key == Key.Ctrl) _IsCtrlDown = false;
        }

        private void MapKeyDown(Map _map, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.A:if (_IsCtrlDown) SetMapSource(VeMapModes.VeAerial);break;
                case Key.R: if (_IsCtrlDown) SetMapSource(VeMapModes.VeRoad); break;
                case Key.H: if (_IsCtrlDown) SetMapSource(VeMapModes.VeHybrid); break;
                case Key.Ctrl:_IsCtrlDown = true;break;
            }
        }


        private void SetMapSource(VeMapModes opts)
        {
            if (map.Navigation.Toolbar is MapSourceControl)
            {
                var tsTool = (MapSourceControl)map.Navigation.Toolbar;
                string sourceKey = string.Empty;
                switch (opts)
                {
                    case VeMapModes.VeAerial: tsTool.SelectedSource = MapSourceControl.TileSourceIds.VeAerial; break;
                    case VeMapModes.VeHybrid: tsTool.SelectedSource = MapSourceControl.TileSourceIds.VeHybrid; break;
                    case VeMapModes.VeRoad: tsTool.SelectedSource = MapSourceControl.TileSourceIds.VeRoad; break;
                }
            }
        }


        void MapLoaded(Map map, DeepEarth.Events.MapEventArgs args)
        {
            _SourceControl = (MapSourceControl)map.Navigation.Toolbar;
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();
            srcList.Add(MapSourceControl.TileSourceIds.VeAerial);
            srcList.Add(MapSourceControl.TileSourceIds.VeHybrid);
            srcList.Add(MapSourceControl.TileSourceIds.VeRoad);
            _SourceControl.TileSources = srcList;


            map.BaseLayer.Source = new VeTileSource(VeMapModes.VeHybrid);
            //SetMapSource(VeMapModes.VeHybrid);
        }
    }
}