/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using DeepEarth;
using DeepEarth.Geometry;
using DeepEarth.Layers;
using DeepEarth.Provider.VirtualEarth;
using DeepEarth.Provider.VirtualEarth.VEGeocodeService;

namespace DeepEarthPrototype.Controls
{
    public partial class GeoFind : ILayer
    {
        private Map _Map;
        private GeometryLayer resultsShapeLayer;

        public GeoFind()
        {
            InitializeComponent();


            // Test for DesignTime for display in Blend
            if (HtmlPage.IsEnabled)
            {
                _Map = MapInstance;

                GeocodeService = new Geocode();
                Find.Click += Find_Click;
                Clear.Click += Clear_Click;
            }
        }

        public Geocode GeocodeService { get; set; }



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

                                                       if (results.Count == 1)
                                                       {
                                                           GeocodeResult result = results[0];
                                                           AddLocationPin(result);
                                                       }
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
                    AddLocationPin(result as GeocodeResult);
                }
            }
        }

        private void AddLocationPin(GeocodeResult result)
        {
            var point = new Point(result.Locations[0].Longitude, result.Locations[0].Latitude);

            if (resultsShapeLayer == null)
            {
                resultsShapeLayer = new GeometryLayer(MapInstance) { ID = "RESULTSLAYER" };
                Style style = (Application.Current.Resources["PushPinArrowStyle"]) as Style;
                resultsShapeLayer.ItemsStyle = style;
                MapInstance.Layers.Add(resultsShapeLayer);
            }
            else
            {
                resultsShapeLayer.Clear();
            }

            MapInstance.SetViewCenter(point, 12);

            //Add an arrow pin whose another point is an anchor of 0.5, 1.0;
            PointBase arrowPin = new PointBase { Point = point};
            resultsShapeLayer.Add(arrowPin);
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
    }
}