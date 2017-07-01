/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Browser;
using System.Windows.Controls;
using DeepEarth;
using DeepEarth.Geometry;
using DeepEarth.Layers;
using DeepEarth.Provider.VirtualEarth;
using Polygon=DeepEarth.Geometry.Polygon;

namespace DeepEarthPrototype.Controls
{
    public partial class GeoServices : ILayer
    {
        //sample layer
        public static string Token;
        private readonly Random _Rand = new Random(DateTime.Now.Second);
        private readonly List<int> _ZOrderBucket = new List<int> {0, 1, 2, 3, 4};
        private bool _DevPinsConfigured;
        private Map _Map;
        private GeometryLayer devPinLayer;
        private GeometryLayer shapeLayer;

        public GeoServices()
        {
            // Required to initialize variables
            InitializeComponent();

            // Test for DesignTime for display in Blend
            if (HtmlPage.IsEnabled)
            {
                _Map = MapInstance;

                GeocodeService = new Geocode();
                RouteService = new Route();

                AddPolygon.Click += AddPolygon_Click;
                Rotate.Click += Rotate_Click;
                ZoomToLocation.Click += ZoomToLocation_Click;
                ToggleDevPins.Click += ToggleDevPins_Click;

                MapInstance.Events.MapLoaded += (o, e) => ConfigureDevPins();
            }
        }

        public Geocode GeocodeService { get; set; }
        public Route RouteService { get; set; }

        public Orientation Orientation
        {
            get { return _OrientationPanel.Orientation; }
            set { _OrientationPanel.Orientation = value; }
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

        private void ToggleDevPins_Click(object sender, RoutedEventArgs e)
        {
            if (devPinLayer == null)
            {
                ConfigureDevPins();
            }
            else
            {
                devPinLayer.IsVisible = !devPinLayer.IsVisible;
            }
        }

        private void ConfigureDevPins()
        {
            if (_DevPinsConfigured == false)
            {
                if (devPinLayer == null)
                {
                    devPinLayer = new GeometryLayer(MapInstance) { ID = "DEVPINLAYER" };
                    devPinLayer.UpdateMode = GeometryLayer.UpdateModes.ElementUpdate;
                    MapInstance.Layers.Add(devPinLayer);
                }

                AddPinToLayer(new Point(-77.0365, 38.897), "RoadWarriorPinStyle", devPinLayer);
                AddPinToLayer(new Point(153.430172, -27.998868), "NoobiePinStyle", devPinLayer);
                AddPinToLayer(new Point(153.025, -27.470), "SoulPinStyle", devPinLayer);
                AddPinToLayer(new Point(-83.437448, 42.352317), "SMBeckerPinStyle", devPinLayer);
                AddPinToLayer(new Point(-77.0467, 38.33096), "AquasealPinStyle", devPinLayer);

                
                _DevPinsConfigured = true;
            }
        }

        private void AddPinToLayer(Point location, string styleKey, GeometryLayer layer)
        {
            var pin = new DevPin() { Point = location, Style = (Application.Current.Resources[styleKey] as Style) };
            layer.Add(pin);
            Canvas.SetZIndex(pin, GetNextRandomZ());
        }

        private int GetNextRandomZ()
        {
            int index = _Rand.Next(0, _ZOrderBucket.Count);
            int nextZ = _ZOrderBucket[index];
            _ZOrderBucket.RemoveAt(index);
            return nextZ;
        }

        private void AddPolygon_Click(object sender, RoutedEventArgs e)
        {
            ////Sample Polygon added to the map
            //ConfigShapeLayer();
            //var points = new List<Point> {new Point(0, 0), new Point(20, 0), new Point(20, 20), new Point(0, 20)};
            //var polygon = new Polygon {Points = points};
            //shapeLayer.Add(polygon);

            if (shapeLayer == null)
            {
                shapeLayer = new GeometryLayer(MapInstance) { ID = "POLYGONLAYER" };
                shapeLayer.UpdateMode = GeometryLayer.UpdateModes.PanOnlyUpdate; //Old Method
                //shapeLayer.UpdateMode = GeometryLayer.UpdateModes.TransformUpdate; 
                shapeLayer.Opacity = 0.4;

                MapInstance.Layers.Insert(1, shapeLayer);
            }

            var points = new ObservableCollection<Point> { new Point(0, 0), new Point(20, 0), new Point(20, 20), new Point(0, 20) };
            //var polygon = new Polygon(shapeLayer) { Points = points };
            var polygon = new Polygon() { Points = points };
            polygon.FillColor = Colors.Green;
            shapeLayer.Add(polygon);


            var linePoints = new ObservableCollection<Point> { new Point(-88, 42), new Point(-75, 39) };
            //LineString line = new LineString(shapeLayer) { Points = linePoints };
            LineString line = new LineString() { Points = linePoints };
            line.LineThickness = 4;
            line.LineColor = Colors.White;
            shapeLayer.Add(line);

            //Add points at each end of the 
            shapeLayer.Add(new PointBase { Point = new Point(-88, 42) });
            shapeLayer.Add(new PointBase { Point = new Point(-75, 39) });
            //new PointBase(shapeLayer) { Point = new Point(-88, 42) };
            //new PointBase(shapeLayer) { Point = new Point(-75, 39) };


        }



        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            Point center = MapInstance.GeoCenter;
            double zoom = MapInstance.ZoomLevel;
            MapInstance.RotationAngle += -20;
            MapInstance.SetViewCenter(center, zoom);
        }

        private void ZoomToLocation_Click(object sender, RoutedEventArgs e)
        {
            MapInstance.SetViewCenter(new Point(153.02633, -27.47036), 16);
        }


    }
}