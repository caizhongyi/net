/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Windows.Controls;
using System.Windows.Input;

using DeepEarth;
using DeepEarth.Layers;
using DeepEarth.Provider.CloudMade;
using DeepEarth.Provider.OpenAerialMaps;
using DeepEarth.Provider.OpenStreetMaps;
using DeepEarth.Provider.VirtualEarth;
using DeepEarth.Provider.Yahoo;
using DeepEarth.Provider.WebMapService;

namespace DeepEarthPrototype.Controls
{

    public partial class SidePanelControl : ILayer
    {
        private Map _Map;
        private bool _IsCtrlDown;

        public SidePanelControl()
        {
            // Required to initialize variables
            InitializeComponent();

            if (HtmlPage.IsEnabled)
            {
                MapInstance.Events.MapKeyDown += MapKeyDown;
                MapInstance.Events.MapKeyUp += MapKeyUp;

                SetDefaultSources();

                SidePanel.SizeChanged += SidePanel_SizeChanged;
            }
        }

#region "SidePanel Display"

        private bool SidePanelIsVisibale;
        public void ToggleSidePanel()
        {
            if (SidePanelIsVisibale)
            {
                VisualStateManager.GoToState(this, "SidePanelClose", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "SidePanelOpen", true);
            }
            SidePanelIsVisibale = !SidePanelIsVisibale;
        }

        void SidePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (Expander exp in SidePanel.Children)
            {
                if (exp.IsExpanded)
                {
                    currentExp = exp;
                    break;
                }
            }
            UpdateSidePanelLayout();
        }

        private void _Expander_Expanded(object sender, RoutedEventArgs e)
        {
            currentExp = sender as Expander;
            UpdateSidePanelLayout();
        }

        private void _Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            currentExp = sender as Expander;
            UpdateSidePanelLayout();
        }

        private Expander currentExp;
        private void UpdateSidePanelLayout()
        {
            if (SidePanel != null)
            {
                double totalHeight = 0.0;

                foreach (Expander exp in SidePanel.Children)
                {
                    if (exp != currentExp)
                    {
                        exp.IsExpanded = false;
                        exp.Height = ((ToggleButton)((TextBlock)exp.Header).Parent).ActualHeight;
                        totalHeight += (exp.Height + exp.Margin.Top + exp.Margin.Bottom);
                    }
                }
                if (currentExp != null)
                {
                    double targetHeight = (SidePanelBorder.ActualHeight + currentExp.Margin.Top + currentExp.Margin.Bottom)
                                        - (totalHeight + ((ToggleButton)((TextBlock)currentExp.Header).Parent).ActualHeight);
                    if (targetHeight >= 0)
                    {
                        currentExp.Height = targetHeight;
                    }

                    currentExp.IsExpanded = true;
                }
            }
        }

#endregion



        private void MapKeyDown(Map _map, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.A: if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.VeAerial; break;
                case Key.R: if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.VeRoad; break;
                case Key.H:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.VeHybrid; break;
                case Key.M:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.Mapnik; break;
                case Key.D:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.Osmarend; break;
                case Key.O:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.OAM; break;
                case Key.W:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.CmWeb; break;
                case Key.B:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.CmMobile; break;
                case Key.N:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.CmNoNames; break;
                case Key.C:if (_IsCtrlDown) mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.CmCycle; break;
                case Key.Ctrl:_IsCtrlDown = true;
                    break;
            }
        }

        private void MapKeyUp(Map _map, KeyEventArgs args)
        {
            if (args.Key == Key.Alt) _IsCtrlDown = false;
        }


        #region "Set TileLayers Dynamically"

        private void SetDefaultSources()
        {
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();

            srcList.Add(MapSourceControl.TileSourceIds.VeAerial);
            srcList.Add(MapSourceControl.TileSourceIds.VeHybrid);
            srcList.Add(MapSourceControl.TileSourceIds.VeRoad);
            srcList.Add(MapSourceControl.TileSourceIds.YahooAerial);
            srcList.Add(MapSourceControl.TileSourceIds.YahooStreet);
            srcList.Add(MapSourceControl.TileSourceIds.Mapnik);
            srcList.Add(MapSourceControl.TileSourceIds.Osmarend);
            srcList.Add(MapSourceControl.TileSourceIds.OAM);
            srcList.Add(MapSourceControl.TileSourceIds.CmWeb);
            srcList.Add(MapSourceControl.TileSourceIds.CmMobile);
            srcList.Add(MapSourceControl.TileSourceIds.CmNoNames);
            srcList.Add(MapSourceControl.TileSourceIds.CmCycle);
            srcList.Add(MapSourceControl.TileSourceIds.WMS);

            this.mapSourceControl.ApplyTemplate();
            this.mapSourceControl.TileSources = srcList;
        }




        private void SetVE_Click(object sender, RoutedEventArgs e)
        {
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();

            srcList.Add(MapSourceControl.TileSourceIds.VeAerial);
            srcList.Add(MapSourceControl.TileSourceIds.VeHybrid);
            srcList.Add(MapSourceControl.TileSourceIds.VeRoad);

            this.mapSourceControl.ApplyTemplate();
            this.mapSourceControl.TileSources = srcList;
            this.mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.VeHybrid;
        }

        private void SetCM_Click(object sender, RoutedEventArgs e)
        {
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();

            srcList.Add(MapSourceControl.TileSourceIds.CmWeb);
            srcList.Add(MapSourceControl.TileSourceIds.CmMobile);
            srcList.Add(MapSourceControl.TileSourceIds.CmNoNames);
            srcList.Add(MapSourceControl.TileSourceIds.CmCycle);

            this.mapSourceControl.ApplyTemplate();
            this.mapSourceControl.TileSources = srcList;
            this.mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.CmWeb;
        }

        private void SetOSM_Click(object sender, RoutedEventArgs e)
        {
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();

            srcList.Add(MapSourceControl.TileSourceIds.Mapnik);
            srcList.Add(MapSourceControl.TileSourceIds.Osmarend);

            this.mapSourceControl.ApplyTemplate();
            this.mapSourceControl.TileSources = srcList;
            this.mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.Mapnik;
        }

        private void SetYahoo_Click(object sender, RoutedEventArgs e)
        {
            List<MapSourceControl.TileSourceIds> srcList = new List<MapSourceControl.TileSourceIds>();

            srcList.Add(MapSourceControl.TileSourceIds.YahooAerial);
            srcList.Add(MapSourceControl.TileSourceIds.YahooStreet);

            this.mapSourceControl.ApplyTemplate();
            this.mapSourceControl.TileSources = srcList;
            this.mapSourceControl.SelectedSource = MapSourceControl.TileSourceIds.YahooAerial;

            // Another way to set the BaseLayers, Just access the Baselayer directly

            //this.mapSourceControl.BaseLayers.Clear();
            //this.mapSourceControl.BaseLayers.Add(new DeepEarth.Provider.Yahoo.TileLayer(DeepEarth.Provider.Yahoo.MapMode.YahooAerial));
            //this.mapSourceControl.BaseLayers.Add(new DeepEarth.Provider.Yahoo.TileLayer(DeepEarth.Provider.Yahoo.MapMode.YahooStreet));
            //this.mapSourceControl.ApplyTemplate();
            //this.mapSourceControl.SelectedSource = DeepEarth.Provider.Yahoo.MapMode.YahooAerial.ToString();
        }


        #endregion



        #region ILayer APIs

        public string ID { get; set; }
        public bool IsVisible { get; set; }

        public Map MapInstance
        {
            get
            {
                if (_Map == null) _Map = Map.GetMapInstance(this);
                return _Map;
            }
            set
            {
                if (ReferenceEquals(_Map, value))
                {
                    return;
                }

                _Map = value;
            }
        }

        #endregion
    }
}