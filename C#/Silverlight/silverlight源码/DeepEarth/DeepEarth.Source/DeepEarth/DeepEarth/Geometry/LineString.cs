// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System.Windows.Media;

namespace DeepEarth.Geometry
{
    /// <summary>
    /// <para>
    /// A class representing a series of points and the lines connecting them
    /// </para>
    /// <example>
    /// 
    /// <code title="Draw a LineString along the the Equator">
    ///    //Equator will be drawn on a PanOnly GeometryLayer
    ///    GeometryLayer panOnlyLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.PanOnlyUpdate };
    ///    panOnlyLayer.Opacity = 0.5;
    ///    map.Layers.Add(panOnlyLayer);
    ///    LineString line = new LineString();
    ///    panOnlyLayer.Add(line);
    ///    line.Points.Add(new Point(-180, 0));
    ///    line.Points.Add(new Point(180, 0));
    ///    line.LineThickness = 4;
    ///    line.LineColor = Colors.White;
    ///    System.Windows.Controls.ToolTipService.SetToolTip(line, "Equator");
    /// </code>
    /// </example>
    /// </summary>
    public class LineString : PathBase
    {
        ///<summary>
        /// Initializes an instance of the Linestring class.
        ///</summary>
        public LineString()
        {
            LineColor = Colors.Black;
            LineThickness = 1;
        }


        ///<summary>
        /// Initializes an instance of the Linestring class with parameters.
        ///</summary>
        ///<param name="lineColor"></param>
        ///<param name="lineThickness"></param>
        public LineString(Color lineColor, double lineThickness)
        {
            LineColor = lineColor;
            LineThickness = lineThickness;
        }

        /// <summary>
        /// Specifies whether or not the geometry is continuous
        /// </summary>
        public override bool IsClosed
        {
            get { return false; }
        }


    }
}
