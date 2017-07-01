// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace DeepEarth.Geometry
{

    /// <summary>
    /// <para>
    /// The abstract class upon which all DeepEarth geometry or 'shapes' are derived.
    /// All DeepEarth points (PointBase), paths (i.e. lines; PathBase),
    /// and Polygons derive from this class.
    /// </para>
    /// <example>
    /// 
    /// <code title="Iterate through the potentially heterogeneous geometries of a geometry layer">
    ///foreach (GeometryBase shape in geometryLayer)
    ///{
    ///    if (shape is Polygon) Console.WriteLine("I am a Polygon!");
    ///    if (shape is LineString) Console.WriteLine("I am a LineString!");
    ///    if (shape is PointBase) Console.WriteLine("I am a PointBase!");
    ///}
    /// </code>
    /// </example>
    /// </summary>
    public abstract class GeometryBase : Control
    {
        /// <summary>
        /// Static DependencyProperty for recieving the geometry anchor value from XAML instanciation.
        /// </summary>
        public static readonly DependencyProperty AnchorPointProperty = DependencyProperty.Register("AnchorPoint", typeof (Point), typeof (GeometryBase),new PropertyMetadata(new PropertyChangedCallback(OnAnchorPointChanged)));
        /// <summary>
        /// Protected backing field for the AnchorPoint property
        /// </summary>
        protected Point _AnchorPoint;
        /// <summary>
        /// Protected backing field for the Anchor property
        /// </summary>
        protected Point _Anchor;
        /// <summary>
        /// Protected backing field for the IsAnchorCalculated property
        /// </summary>
        protected bool _IsAnchorCalculated;
        /// <summary>
        /// Protected backing field for the Id property
        /// </summary>
        protected Guid _Id = Guid.Empty;
        /// <summary>
        /// Protected backing field for the InView property
        /// </summary>
        protected bool _InView = true;
        /// <summary>
        /// Protected backing field for the IsInitialViewSet property
        /// </summary>
        protected bool _IsInitialViewSet;
        /// <summary>
        /// Protected backing field for the IsInitialVisibilitySet property
        /// </summary>
        protected bool _IsInitialVisibilitySet;
        /// <summary>
        /// Protected backing field for the IsVisible property
        /// </summary>
        protected bool _IsVisible = true;
        /// <summary>
        /// Protected backing field for the OriginalSize property
        /// </summary>
        protected Size _OriginalSize;
        /// <summary>
        /// Protected backing field for the ScaleAdjustment property
        /// </summary>
        protected double _ScaleAdjustment = 1;
        /// <summary>
        /// Protected backing field for the IsLoaded property
        /// </summary>
        protected bool _IsLoaded = false;


        ///<summary>
        /// Initializes the an instance of the GeometryBase.
        ///</summary>
        public GeometryBase()
        {
            DefaultStyleKey = typeof(GeometryBase);
            _Id = Guid.NewGuid();
        }

        /// <summary>
        /// The globally unique identifier for the geometry
        /// </summary>
        public Guid ID
        {
            get
            {
                if (_Id == Guid.Empty) _Id = Guid.NewGuid();
                return _Id;
            }

        }

        /// <summary>
        /// Does the geometry intersect with the bounds of the current screen's view port.
        /// </summary>
        public bool InView { get; set; }

        /// <summary>
        /// Has the template been applied to the shape.
        /// </summary>
        public bool IsLoaded
        {
            get 
            {
                return _IsLoaded; 
            }
        }

        /// <summary>
        /// The parent GeometryLayer reposible for the positioning layout and control.
        /// </summary>
        public GeometryLayer Layer { get; internal set; }

        /// <summary>
        /// Indicates whether the geometry currently visible.
        /// </summary>
        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                if (_IsInitialVisibilitySet == false || _IsVisible != value)
                {
                    _IsVisible = value;
                    _IsInitialVisibilitySet = true;
                }
            }
        }

        /// <summary>
        /// A point within the goemetry extent to anchored it's position on the canvas.
        /// </summary>
        public Point AnchorPoint
        {
            get { return _AnchorPoint; }
            set
            {
                _IsAnchorCalculated = false;
                _AnchorPoint = value;
                Refresh();
            }
        }

        /// <summary>
        /// A Point ranging from (0,0) upperleft to (1,1) bottomright within the extent if the geometry item to anchor to the canvas.
        /// </summary>
        public Point Anchor
        {
            get 
            {
                if (_IsAnchorCalculated == false)
                {
                    if(IsDimensionsConfigured())
                    {
                        if (AnchorPoint == new Point())
                        {
                            _Anchor = new Point((Width / 2), (Height / 2));
                        }
                        else
                        {
                            _Anchor = new Point(Width * AnchorPoint.X, Height * AnchorPoint.Y);
                        }

                        _IsAnchorCalculated = true;
                    }
                }


                if (double.IsNaN(_Anchor.X) || double.IsNaN(_Anchor.Y)) _Anchor = new Point();

                return _Anchor; 
            }
        }

        /// <summary>
        /// Specifies whether or not the dimensions of the Geometry are configured
        /// </summary>
        /// <returns>Boolean</returns>
        private bool IsDimensionsConfigured()
        {
            bool isConfigured = false;
            if((double.IsNaN(Width) == false) &&  (double.IsNaN(Height) == false))
            {
                if(Width > 0 && Height > 0)
                {
                    isConfigured = true;
                }
            }
            return isConfigured;
        }

        /// <summary>
        /// Map Instance accessor
        /// </summary>
        public Map MapInstance
        {
            get
            {
                if (Layer == null)
                {
                    return null;
                }
                return Layer.MapInstance;
            }
            
        }

        /// <summary>
        /// Handles the event raised by applying the FrameworkElement template
        /// </summary>
        public override void OnApplyTemplate()
        {
            bool isDesignTime = (HtmlPage.IsEnabled == false);
            if (isDesignTime == false)
            {
                _IsLoaded = true;
                ForceMeasure();
                Layer.UpdateChildLocation(this);
            }
        }

        /// <summary>
        /// Force a resizing measure to determine the width and height
        /// </summary>
        public void ForceMeasure()
        {
            // force measure if the width or height is NaN to determine what the width or height should be
            if (double.IsNaN(Width) || double.IsNaN(Height))
            {
                Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _OriginalSize = new Size(DesiredSize.Width, DesiredSize.Height);
            }
            else
            {
                _OriginalSize = new Size(Width, Height);
            }

            Width = _OriginalSize.Width;
            Height = _OriginalSize.Height;
        }

        private static void OnAnchorPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GeometryBase) d).AnchorPoint = (Point) e.NewValue;
        }

        /// <summary>
        /// For the ScaleTransform method the entire canvas is scaled to sale the child objects.  
        /// For elements which should not scales, such as the border
        /// thickness we use the ScaleAdjustment as an opportunity to offset the scaling and keeps visual element size constant.
        /// </summary>
        public virtual double ScaleAdjustment
        {
            get { return _ScaleAdjustment; }
            set
            {
                _ScaleAdjustment = value;
            }
        }

        /// <summary>
        /// Refresh the location of the Geometry within its containing layer
        /// </summary>
        protected void Refresh()
        {
            if (Layer != null && MapInstance != null)
            {
                Layer.UpdateShape(this);
            }
        }

        /// <summary>
        /// Determines whether or not the geometry shares an overlap with another geometry
        /// </summary>
        /// <param name="bounds">A rectangle to intersect with</param>
        /// <returns>Boolean</returns>
        public abstract bool Intersects(Rect bounds);

    }
}