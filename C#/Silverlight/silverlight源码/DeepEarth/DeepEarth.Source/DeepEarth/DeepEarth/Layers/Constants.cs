// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;

namespace DeepEarth.Layers
{
    /// <summary>
    /// <para>
    /// The various constant values that will never change throughout the layers
    /// The name of the value is self describing
    /// </para>
    /// </summary>
    public static class Constants
    {
        //These are Earth specific, not any projection specific
        //See http://en.wikipedia.org/wiki/Geographic_coordinate_system.
        public const double EarthMinLatitude = -85.05112878D;
        public const double EarthMaxLatitude = 85.05112878D;
        public const double EarthMinLongitude = -180D;
        public const double EarthMaxLongitude = 180D;
        public const double EarthCircumference = EarthRadius * 2 * Math.PI;
        public const double HalfEarthCircumference = EarthCircumference / 2;
        public const double EarthRadius = 6378137;

        public const double ProjectionOffset = EarthCircumference * 0.5;

        public const double INCH_TO_METER = 0.0254D;
        public const double METER_TO_INCH = 39.3700787D;
        public const double METER_TO_MILE = 0.000621371192D;
        public const double MILE_TO_METER = 1609.344D;
    }
}
