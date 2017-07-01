// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows;

namespace DeepEarth.Layers
{

    /// <summary>
    /// <para>
    /// The OGC Spatial Reference requirements. 
    /// </para>
    /// <example>
    /// 
    /// <code title="Inherit from SpatialReference and create the Mercator Projection">   
    /// public class MercatorProjection : SpatialReference
    ///    {
    ///    public MercatorProjection()
    ///    {
    ///        GeoGCS = "GCS_WGS_1984";
    ///        Datum = "WGS_1984";
    ///        SpheroidRadius = 6378137.00D;
    ///        SpheroidFlattening = 298.257223563D;
    ///        Primem = 0.0D;
    ///        AngularUnitOfMeasurement = 0.017453292519943295D;
    ///        FalseEasting = 0.0D;
    ///        FalseNorthing = 0.0D;
    ///        CentralMeridian = 0.0D;
    ///        LatitudeOfOrigin = 0.0D;
    ///        UnitAuthority = "Meter";
    ///
    ///        ScaleX = HalfPi;
    ///        ScaleY = -HalfPi;
    ///        OffsetX = 0.5;
    ///        OffsetY = 0.5;
    ///    }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class SpatialReference : ISpatialReference
    {

        public const double HalfPi = 0.159154943091895;
        public const double RadiansToDegrees = 57.2957795130823;

        public string GeoGCS { get; set; }
        public string Datum { get; set; }
        public string DatumAuthority { get; set; }
        public double SpheroidRadius { get; set; }
        public double SpheroidFlattening { get; set; } 
        public string SpheroidAuthority { get; set; }
        public double Primem { get; set; }
        public string PrimemAuthority { get; set; }
        public string ProjectionAuthority { get; set; }
        public double FalseEasting { get; set; }
        public double FalseNorthing { get; set; }
        public double CentralMeridian { get; set; }
        public double StandardParallel { get; set; }
        public double LatitudeOfOrigin { get; set; }
        public double AngularUnitOfMeasurement { get; set; }
        public string UnitAuthority { get; set; }
        public string Authority { get; set; }

        /// <summary>
        /// The real world coordinate scale at a given longitude
        /// </summary>
        public double ScaleX { get; set; }

        /// <summary>
        /// The real world coordinate scale at a given latitude
        /// </summary>
        public double ScaleY { get; set; }

        /// <summary>
        /// Logical X offset to centre of earth
        /// </summary>
        public double OffsetX { get; set; }

        /// <summary>
        /// Logical Y offset to centre of earth
        /// </summary>
        public double OffsetY { get; set; }

        /// <summary>
        /// Converts Map UI coordinates to real world (geographic) coordinates for a point
        /// </summary>
        /// <param name="logicalPoint">The logical UI point</param>
        /// <returns>Geographic Point</returns>
        public Point LogicalToGeographic(Point logicalPoint)
        {
            return new Point((logicalPoint.X - OffsetX) * RadiansToDegrees / ScaleX, Math.Atan(Math.Sinh((logicalPoint.Y - OffsetY) / ScaleY)) * RadiansToDegrees);
        }

        /// <summary>
        /// Converts real world (geographic) point coordinates to Map UI coordinates
        /// </summary>
        /// <param name="geographicPoint"></param>
        /// <returns>Logical Point</returns>
        public Point GeographicToLogical(Point geographicPoint)
        {
            double d = Math.Sin(geographicPoint.Y * AngularUnitOfMeasurement);
            return new Point((geographicPoint.X * AngularUnitOfMeasurement * ScaleX) + OffsetX, (0.5 * Math.Log((1.0 + d) / (1.0 - d)) * ScaleY) + OffsetY);
        }
    }
}
