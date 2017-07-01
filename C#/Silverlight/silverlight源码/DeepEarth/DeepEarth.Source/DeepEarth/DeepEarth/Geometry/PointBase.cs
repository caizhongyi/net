// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System.Windows;
using System.Windows.Media;

namespace DeepEarth.Geometry
{

    /// <summary>
    /// The abstract class upon which all point geometries in DeepEarth are derived.
    /// <example>
    /// 
    /// <code title="Draw two points on either end of the Equator">   
    ///    //Equator endpoints are drawn on an ElementUpdate Geometry Layer
    ///    GeometryLayer elementUpdateLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.ElementUpdate };
    ///    elementUpdateLayer.Opacity = 0.5;
    ///    map.Layers.Add(elementUpdateLayer);
    ///    elementUpdateLayer.Add(new PointBase { Point = new Point(-180, 0) });
    ///    elementUpdateLayer.Add(new PointBase { Point = new Point(180, 0)});         
    /// </code>
    /// </example>
    /// </summary>
    public class PointBase : GeometryBase
    {
        private Point _Point;
        private Point _LogicalCoordinate;
        private ScaleTransform _ScaleTransform;
        /// <summary>
        /// Protected backing field for the TargetScale property
        /// </summary>
        protected double _TargetScale;


        ///<summary>
        /// Initializes an instance of the Pointbase class.
        ///</summary>
        public PointBase()
        {
            DefaultStyleKey = typeof (PointBase);
        }

        /// <summary>
        /// The System.Windows.Point class
        /// </summary>
        public Point Point
        {
            get
            {
                if (_Point == null)
                {
                    _Point = new Point();
                    _LogicalCoordinate = Map.DefaultInstance.CoordHelper.GeoToLogical(_Point);
                }
                return _Point;
            }
            set
            {
                _Point = value;
                _LogicalCoordinate = Map.DefaultInstance.CoordHelper.GeoToLogical(_Point);
                Refresh();
            }
        }

        /// <summary>
        /// The logical coordinate of the point
        /// </summary>
        public Point LogicalCoordinate
        {
            get{return _LogicalCoordinate;}
        }

        /// <summary>
        /// The X coordinate of the point
        /// </summary>
        public double X
        {
            get { return Point.X; }
            set 
            {
                _Point.X = value;
                Point = _Point;
            }
        }

        /// <summary>
        /// The Y coordinate of the point
        /// </summary>
        public double Y
        {
            get { return Point.Y; }
            set 
            { 
                _Point.Y = value;
                Point = _Point;
            }
        }

        /// <summary>
        /// The adjustment scale for the Point
        /// </summary>
        public override double ScaleAdjustment
        {
            get { return _ScaleAdjustment; }
            set
            {
                _ScaleAdjustment = value;
                if (_IsLoaded)
                {
                    _ScaleTransform.ScaleX = _ScaleAdjustment;
                    _ScaleTransform.ScaleY = _ScaleAdjustment;
                }
            }
        }

        /// <summary>
        /// Handles the event raised by applying the FrameworkElement template
        /// </summary>
        public override void OnApplyTemplate()
        {
            _IsLoaded = true;
            ForceMeasure();

            _ScaleTransform = (ScaleTransform) GetTemplateChild("_ScaleTransform");
            _ScaleTransform.CenterX = Anchor.X;
            _ScaleTransform.CenterY = Anchor.Y;

            if (Layer != null)
            {
                Layer.UpdateChildLocation(this);
            }
        }

        /// <summary>
        /// Determines whether or not the geometry shares an overlap with another geometry
        /// </summary>
        /// <param name="bounds">A rectangle to intersect with</param>
        /// <returns>Boolean</returns>
        public override bool Intersects(Rect bounds)
        {
            return bounds.Contains(_Point);
        }
    }
}