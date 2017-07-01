/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DeepEarth;
using DeepEarth.Controls;
using DeepEarth.Geometry;
using DeepEarth.Provider.OpenStreetMaps;
using DeepEarth.Provider.OpenAerialMaps;
using DeepEarth.Provider.VirtualEarth;
using DeepEarthPrototype.Behaviors;
using DeepEarth.Layers;


namespace DeepEarthPrototype
{

    public partial class Page
    {

        private ReverseGeocode _ReverseGeocodeBehavior;

        public Page()
        {
            InitializeComponent();

            _ReverseGeocodeBehavior = new ReverseGeocode(map, new Geocode());

            //Choose your startup provider
            map.BaseLayer.Source = new VeTileSource(VeMapModes.VeHybrid);
            //map.BaseLayer.Source = new OsmTileSource(OsmMapModes.Mapnik);
            //map.BaseLayer.Source = new OamTileSource(OamMapModes.OAM);

            GeometryLayerTest();

            GeometryAnchorTest();

        }

        private void GeometryLayerTest()
        {


            //Bermuda Triangle is drawn on a GeometryLayer with TransformUpdate
            GeometryLayer transformLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.TransformUpdate };
            transformLayer.Opacity = 0.5;
            map.Layers.Add(transformLayer);
            Polygon polygon = new Polygon();
            transformLayer.Add(polygon);
            polygon.Points = new ObservableCollection<Point> { new Point(-80.195, 25.775), new Point(-64.75, 32.303), new Point(-66.073, 18.44) };
            polygon.FillColor = Color.FromArgb(0x7F, 0xFF, 0x00, 0x00);
            System.Windows.Controls.ToolTipService.SetToolTip(polygon, "Bermuda Triangle");


            //Equator is drawn on a PanOnly GeometryLayer
            GeometryLayer panOnlyLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.PanOnlyUpdate };
            panOnlyLayer.Opacity = 0.5;
            map.Layers.Add(panOnlyLayer);
            LineString line = new LineString();
            panOnlyLayer.Add(line);
            line.Points.Add(new Point(-180, 0));
            line.Points.Add(new Point(180, 0));
            line.LineThickness = 4;
            line.LineColor = Colors.White;
            System.Windows.Controls.ToolTipService.SetToolTip(line, "Equator");


            //Equator endpoints are drawn on an ElementUpdate Geometry Layer to test synching of layers
            GeometryLayer elementUpdateLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.ElementUpdate };
            elementUpdateLayer.Opacity = 0.5;
            map.Layers.Add(elementUpdateLayer);
            elementUpdateLayer.Add(new PointBase { Point = new Point(-180, 0) });
            elementUpdateLayer.Add(new PointBase { X = 180, Y = 0 }); // Alternative syntax for setting a PointBase
        }

        private GeometryLayer anchorTestLayer = null;
        private Style styleArrow = null;
        private void GeometryAnchorTest()
        {
            var point = new Point(0, 0);

            if (anchorTestLayer == null)
            {
                anchorTestLayer = new GeometryLayer(map) { ID = "RESULTSLAYER", UpdateMode = GeometryLayer.UpdateModes.ElementUpdate };
                styleArrow = (Application.Current.Resources["PushPinArrowStyle"]) as Style;
                map.Layers.Add(anchorTestLayer);
            }

            //Add an arrow pin whose another point is an anchor of 0.5, 1.0;
            PointBase arrowPin = new PointBase { Point = point, Style = styleArrow };
            anchorTestLayer.Add(arrowPin);

            //Add a another point with the default style which has an anchor of 0.5, 0.5;
            PointBase defaultPin = new PointBase { Point = point };
            anchorTestLayer.Add(defaultPin);

        }

        private void ToggleSidePanel_Click(object sender, RoutedEventArgs e)
        {
            sidePanelControl.ToggleSidePanel();
        }

    }
}