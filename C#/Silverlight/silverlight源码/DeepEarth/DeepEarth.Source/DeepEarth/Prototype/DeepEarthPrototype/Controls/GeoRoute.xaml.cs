/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DeepEarth;
using DeepEarth.Geometry;
using DeepEarth.Layers;
using DeepEarth.Provider.VirtualEarth;
using DeepEarth.Provider.VirtualEarth.VEGeocodeService;
using DeepEarth.Provider.VirtualEarth.VERouteService;
using Location=DeepEarth.Provider.VirtualEarth.VERouteService.Location;


namespace DeepEarthPrototype.Controls
{
    public partial class GeoRoute : ILayer
    {
        private Map _Map;
        private GeometryLayer routeLayer;

        public GeoRoute()
        {
            InitializeComponent();

            // Test for DesignTime for display in Blend
            if (HtmlPage.IsEnabled)
            {
                _Map = MapInstance;

                GeocodeService = new Geocode();
                RouteService = new Route();

                Find.Click += Find_Click;
                Clear.Click += Clear_Click;
                AddRoute.Click += AddRoute_Click;
                ToggleRoute.Click += ToggleRoute_Click;
            }
        }

        public Geocode GeocodeService { get; set; }
        public Route RouteService { get; set; }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearSearchFields();
        }

        private void ClearSearchFields()
        {
            StreetAddressText.Text = string.Empty;
            TownCityText.Text = string.Empty;
            CountryRegionText.Text = string.Empty;
        }

        private void CloseGeoFindListBoxStoryboard_Completed(object sender, EventArgs e)
        {
            GeocodeFindResults.ItemsSource = null;
            GeocodeFindResults.SelectedIndex = -1;
        }

        private void CloseGeoRouteListBoxStoryboard_Completed(object sender, EventArgs e)
        {
            GeocodeRoutePoints.ItemsSource = null;
            GeocodeRoutePoints.SelectedIndex = -1;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            GeocodeFind();
        }

        private void GeocodeFind()
        {
            VisualStateManager.GoToState(this, "CloseGeoFindListBox", true);

            string query = string.Empty;

            if (StreetAddressText.Text.Length > 0) query += StreetAddressText.Text + ", ";
            if (TownCityText.Text.Length > 0) query += TownCityText.Text + ", ";
            if (CountryRegionText.Text.Length > 0) query += CountryRegionText.Text;

            query = query.Trim();

            if (query.Length > 0)
            {
                GeocodeService.Find(query, (o, ev) =>
                                               {
                                                   List<GeocodeResult> results = ((GeocodeResultArgs) ev).Results;

                                                   if (results.Count > 0)
                                                   {
                                                       ListLocations(results);
                                                       VisualStateManager.GoToState(this, "OpenGeoFindListBox", true);
                                                   }
                                               });
            }
        }

        private void ListLocations(List<GeocodeResult> results)
        {
            GeocodeFindResults.ItemsSource = results;
        }

        private void GeocodeFindResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = sender as ListBox;
            if (lb != null)
            {
                object result = lb.SelectedItem;
                if (result != null)
                {
                    GeocodeRoutePoints.Items.Add(result);
                    VisualStateManager.GoToState(this, "OpenGeoRouteListBox", true);
                    ClearSearchFields();
                }
            }
        }

        /// <summary>
        /// Key event handler to automatically start the geocode search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GeocodeFind();
            }
        }

        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            var locations = new List<Point>();

            foreach (GeocodeResult p in GeocodeRoutePoints.Items)
            {
                locations.Add(new Point {X = p.Locations[0].Longitude, Y = p.Locations[0].Latitude});
            }

            RouteService.GetDirections(locations, (o, ev) =>
                                                      {
                                                          RouteResult result = ((RouteResultArgs) ev).Result;
                                                          var points = new ObservableCollection <Point>();
                                                          foreach (Location loc in result.RoutePath.Points)
                                                          {
                                                              points.Add(new Point(loc.Longitude, loc.Latitude));
                                                          }
                                                          if (routeLayer == null)
                                                          {
                                                              routeLayer = new GeometryLayer(MapInstance) {ID = "ROUTELAYER"};
                                                              routeLayer.UpdateMode = GeometryLayer.UpdateModes.TransformUpdate;
                                                              MapInstance.Layers.Add(routeLayer);
                                                          }
                                                          else
                                                          {
                                                              routeLayer.Clear();
                                                          }
                                                          routeLayer.Add(new LineString {Points = points, LineThickness = 2, LineColor = Colors.Blue});
                                                          //new LineString(routeLayer) { Points = points, LineThickness = 2, LineColor = Colors.Blue };
                                                          //add pins for the locations specified
                                                          foreach (Point point in locations)
                                                          {
                                                              routeLayer.Add(new Pushpin{Point = point});
                                                              //new Pushpin(routeLayer) { Point = point };
                                                          }
                                                      });

            ClearSearchFields();

            VisualStateManager.GoToState(this, "CloseGeoRouteListBox", true);
            VisualStateManager.GoToState(this, "CloseGeoFindListBox", true);
        }

        private void ToggleRoute_Click(object sender, RoutedEventArgs e)
        {
            if (routeLayer != null)
            {
                routeLayer.IsVisible = !routeLayer.IsVisible;
                routeLayer.Visibility = Visibility.Collapsed;
            }
        }

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