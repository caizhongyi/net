/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty – Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System.Windows;
using System.Windows.Input;

using DeepEarth;
using DeepEarth.Geometry;
using DeepEarthPrototype.Controls;

namespace DeepEarthPrototype.Behaviors
{
    public class Prototype 
    {
        private Map _Map;
        private bool isMouseDown;
        private bool isMouseDrag;
        private GeometryLayer shapeLayer;

        public Prototype(Map map) 
        {
            _Map = map;
            map.Events.MapMouseUp += this.MouseUp;
            map.Events.MapMouseDown += this.MouseDown;
            map.Events.MapMouseMove += this.MouseMove;
            map.Events.MapKeyDown += Events_MapKeyDown;
            map.Events.MapKeyUp += Events_MapKeyUp;
        }



        bool _IsAKeyDown = false;
        void Events_MapKeyDown(Map map, KeyEventArgs args)
        {
            if (args.Key == Key.A) _IsAKeyDown = true;
        }
        void Events_MapKeyUp(Map map, KeyEventArgs args)
        {
            if (args.Key == Key.A) _IsAKeyDown = false;
        }

        private void MouseDown(Map map, MouseButtonEventArgs args)
        {
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                args.Handled = true;
                isMouseDown = true;
                isMouseDrag = false;
            }
        }

        private void MouseMove(Map map, MouseEventArgs args)
        {
            if (isMouseDown)
            {
                isMouseDrag = true;
            }
        }

        private void MouseUp(Map map, MouseButtonEventArgs args)
        {
            if (isMouseDown && !isMouseDrag)
            {
                Point coord = map.CoordHelper.PixelToGeo(args.GetPosition(map));

                if (shapeLayer == null)
                {
                    ///TODO: RoadWarrior, to demo this, create a subclass of ShapeLayer for specialty behavior.
                    //shapeLayer = new ShapeLayer(map)
                    //{
                    //    ID = "PROTOTYPE",
                    //    InfoBoxOffset = new Point(-20, 0),
                    //    InfoBoxContentProvider = (shape, callback) =>
                    //    {
                    //        var pin = (PointBase)shape;
                    //        var point = pin.Point;
                    //        var description = string.Format("Created point at {0:N6}, {1:N6}", point.X, point.Y);
                    //        callback(description);
                    //    }
                    //};
                    shapeLayer = new GeometryLayer(map){ID = "PROTOTYPE"};

                    map.Layers.Add(shapeLayer);

                    //create the info box
					//infoBox = new InfoBox();
					//shapeLayer.Shapes.Add(infoBox);
					//infoBox.HideInfoBox();  // initially hide the infobox
                }

                // create the pushpin if the "A" key is held down;  We don't want this behaviour with normal clicks.

                if (_IsAKeyDown)
                {
                    //var pushpin = new Pushpin(shapeLayer) { Point = coord };
                    var pushpin = new Pushpin() { Point = coord };
                    //pushpin.MouseLeftButtonDown += OnPushpinMouseDown;
                    shapeLayer.Add(pushpin);

                    args.Handled = true;
                }
            }

            isMouseDown = false;
            isMouseDrag = false;
        }

        //static void OnPushpinMouseDown(object sender, System.EventArgs e)
        //{
        //    var shape = (GeometryBase)sender;

        //    if (ReferenceEquals(InfoBox.Instance.ActiveShape, shape)) {
        //        InfoBox.HideInfoBox(true);
        //    } else {
        //        InfoBox.ShowInfoBox(shape);
        //    }
        //}

    }
}