// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

namespace DeepEarth
{
    /// <summary>
    /// <para>
    /// A simple Compass style navigation direction enum 
    /// Useful for panning controls
    /// </para>
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Bearing 0 or 360 Degrees
        /// </summary>
        North,

        /// <summary>
        /// Bearing 45 Degrees
        /// </summary>
        NorthEast,

        /// <summary>
        /// Bearing 315 Degrees
        /// </summary>
        NorthWest,

        /// <summary>
        /// Bearing 180 Degrees
        /// </summary>
        South,

        /// <summary>
        /// Bearing 135 Degrees
        /// </summary>
        SouthEast,

        /// <summary>
        /// Bearing 225 Degrees
        /// </summary>
        SouthWest,

        /// <summary>
        /// Bearing 90 Degrees
        /// </summary>
        East,

        /// <summary>
        /// Bearing 270 Degrees
        /// </summary>
        West
    }

    /// <summary>
    /// Standard Units of Measure
    /// </summary>
    public enum Unit
    {
        /// <summary>
        /// 2.54 centimeters
        /// </summary>
        Inch,

        /// <summary>
        /// statute mile, 1,609.344 meters
        /// </summary>
        Mile,

        /// <summary>
        /// distance travelled by light in free space in 1/299,792,458 of a second ;)
        /// </summary>
        Meter,

        /// <summary>
        /// 1000 meters
        /// </summary>
        Kilometer
    }
}
