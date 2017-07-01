// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System.Windows.Media;

namespace DeepEarth.Geometry
{
    /// <summary>
    /// <para>
    /// Inherets from the DeepEarth PathBase abstract class and represents a
    /// closed series of points and corresponding line segments that make up a
    /// complete shape
    /// </para>
    /// <example>
    /// 
    /// <code title="This is the DeepEarth example for the Bermuda Triangle">
    ///        GeometryLayer transformLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.TransformUpdate };
    ///        transformLayer.Opacity = 0.5;
    ///        map.Layers.Add(transformLayer);
    ///        Polygon polygon = new Polygon();
    ///        transformLayer.Add(polygon);
    ///        polygon.Points = new ObservableCollection&lt;Point&gt; { new Point(-80.195, 25.775), new Point(-64.75, 32.303), new Point(-66.073, 18.44) };
    ///        polygon.FillColor = Color.FromArgb(0x7F, 0xFF, 0x00, 0x00);
    /// </code>
    /// </example>
    /// </summary>
    public class Polygon : PathBase
    {
        protected internal Color _FillColor;


        ///<summary>
        /// Initializes a new instance of the Polygon class
        ///</summary>
        public Polygon()
        {
            FillColor = Color.FromArgb(128, 255, 255, 255);
            LineColor = Colors.Black;
            LineThickness = 1;
        }

        
        ///<summary>
        /// Initializes an instance of Polygon with parameters.
        ///</summary>
        ///<param name="lineColor"></param>
        ///<param name="fillColor"></param>
        ///<param name="lineThickness"></param>
        public Polygon(Color lineColor, Color fillColor, double lineThickness)
        {
            LineColor = lineColor;
            LineThickness = lineThickness;
            FillColor = fillColor;
        }

        /// <summary>
        /// Overrides the default OnApplyTemplate to configure configure child FrameworkElement references from template
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _Path.Fill = new SolidColorBrush(FillColor);
        }

        /// <summary>
        /// Specifies whether or not the geometry is continuous
        /// </summary>
        public override bool IsClosed
        {
            get { return true; }
        }

        ///<summary>
        /// Sets or gets the fill color for the shape.
        ///</summary>
        public Color FillColor
        {
            get { return _FillColor; }
            set
            {
                _FillColor = value;
                if (_IsLoaded)
                {
                    _Path.Fill = new SolidColorBrush(FillColor);
                    if (Layer != null) Layer.UpdateShape(this);
                }
            }
        }

    }
}
