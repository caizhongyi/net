using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DeepEarth.Geometry
{
    /// <summary>
    /// <para>
    /// The abstract class upon which all point-line (path) geometries in DeepEarth 
    /// are derived from, specifying a series of lines connected by ponts
    /// </para>
    /// <example>
    /// 
    /// <code title="Hide (collapse) a shape if it is a PathBase object">
    /// void HideShape(GeometryBase shape) 
    /// {
    ///     if (shape is PathBase)
    ///     {
    ///         shape.Visibility = Visibility.Collapsed;
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public abstract class PathBase : GeometryBase
    {
        protected Rect _Extent;
        protected Path _Path;
        protected Color _LineColor;
        protected double _LineThickness;
        protected ObservableCollection<Point> _Points;
        protected List<Point> _LogicalCoordinates;

        /// <summary>
        /// The collection of System.Windows.Point objects that make up the paths x, y coordinates
        /// </summary>
        public ObservableCollection<Point> Points
        {
            get
            {
                if(_Points == null)
                {
                    Points = new ObservableCollection<Point>();
                }
                return _Points;
            }
            set
            {
                _Points = value;
                _Points.CollectionChanged += _Points_CollectionChanged;
                UpdatePointDependencies();
            }
        }

        /// <summary>
        /// Update the logical and geographic coordinates of the points comprising the PathBase
        /// </summary>
        private void UpdatePointDependencies()
        {
            UpdateLogicalCoords();
            UpdateExtent();
            if (Layer != null) Layer.UpdateShape(this);
        }

        /// <summary>
        /// The logical coordinates comprising the PathBase
        /// </summary>
        public List<Point> LogicalCoordinates
        {
            get { return _LogicalCoordinates; }
        }

        /// <summary>
        /// Update the logical coordinate values
        /// </summary>
        private void UpdateLogicalCoords()
        {
            _LogicalCoordinates = new List<Point>();
            foreach (Point geoPoint in Points)
            {
                _LogicalCoordinates.Add(Map.DefaultInstance.CoordHelper.GeoToLogical(geoPoint));
            }
        }

        /// <summary>
        /// Update the rectangular bounds of the object
        /// </summary>
        private void UpdateExtent()
        {
            //Calculate the new extent
            double minx = double.MaxValue;
            double miny = double.MaxValue;
            double maxx = double.MinValue;
            double maxy = double.MinValue;
            foreach(Point pt in _Points)
            {
                if(pt.X < minx) minx = pt.X;
                if(pt.Y < miny) miny = pt.Y;
                if(pt.X > maxx) maxx = pt.X;
                if(pt.Y > maxy) maxy = pt.Y;
            }
            _Extent = new Rect(new Point(minx, miny), new Point(maxx, maxy));
        }

        void _Points_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    {
                        UpdatePointDependencies();
                        break;
                    }
            }
        }

        /// <summary>
        /// The system color of the line portion of the PathBase
        /// </summary>
        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                if(_IsLoaded)
                {
                    _Path.Stroke = new SolidColorBrush(LineColor);
                    if(Layer != null) Layer.UpdateShape(this);
                }

            }
        }

        /// <summary>
        /// The thickness of the line portion of the PathBase
        /// </summary>
        public double LineThickness
        {
            get { return _LineThickness; }
            set
            {
                _LineThickness = value;
                if(_IsLoaded)
                {
                    _Path.StrokeThickness = LineThickness;
                    if(Layer != null) Layer.UpdateShape(this);
                }
            }
        }
        
        /// <summary>
        /// The adjustment scale for the entire PathBase
        /// </summary>
        public override double ScaleAdjustment
        {
            get { return _ScaleAdjustment; }
            set
            {
                if(value > double.MinValue && value < double.MaxValue)
                {
                    _ScaleAdjustment = value;
                    if(_IsLoaded && _LineThickness != 0)
                    {
                        _Path.StrokeThickness = _LineThickness * _ScaleAdjustment;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the event raised by applying the FrameworkElement template
        /// </summary>
        public override void OnApplyTemplate()
        {
            _Path = (Path)GetTemplateChild("path");
            _Path.StrokeThickness = LineThickness;
            _Path.Stroke = new SolidColorBrush(LineColor);

            _IsLoaded = true;
            if(Layer != null) Layer.UpdateChildLocation(this);

        }

        /// <summary>
        /// The System.Windows.Media.PathGeometry of the PathData object
        /// </summary>
        public PathGeometry PathData
        {
            get { return (PathGeometry)_Path.Data; }
            set
            {
                _Path.Data = value;
            }
        }

        /// <summary>
        /// Specifies whether or not the geometry is continuous
        /// </summary>
        public abstract bool IsClosed { get; }


        /// <summary>
        /// Deterines if the PathBase objects shares an overlap with a specified extent
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public override bool Intersects(Rect bounds)
        {
            var intersect = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            intersect.Intersect(_Extent);
            return (intersect.Width > 0 || intersect.Height > 0);
        }

    }
}

