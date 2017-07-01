// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/


/* Use the OGC WKT description; example for the VE's Mercator
 * projection that is sphere-based not ellipsoid based:
 * 
    PROJCS["World_Mercator",
        GEOGCS["GCS_WGS_1984"],
            DATUM["WGS_1984"],
                SPHEROID["WGS_1984",6378137,298.257223563]],
            PRIMEM["Greenwich",0],
            UNIT["Degree",0.017453292519943295]],
        PROJECTION["Mercator_1SP"],
        PARAMETER["False_Easting",0],
        PARAMETER["False_Northing",0],
        PARAMETER["Central_Meridian",0],
        PARAMETER["latitude_of_origin",0],
        UNIT["Meter",1]]
 * 
 * And here is the straight WGS84 OGC WKT description:
 *
    GEOGCS["WGS 84", 
    DATUM["WGS_1984", 
    SPHEROID["WGS 84",6378137,298.257223563,
    AUTHORITY["EPSG","7030"]],
    AUTHORITY["EPSG","6326"]],
    PRIMEM["Greenwich",0,
    AUTHORITY["EPSG","8901"]],
    UNIT["degree",0.01745329251994328,
    AUTHORITY["EPSG","9122"]],
    AUTHORITY["EPSG","4326"]]
 * 
 */

using System.Windows;

namespace DeepEarth.Layers
{
    /// <summary>
    /// <para>
    /// The OGC Spatial Reference requirements. 
    /// </para>
    /// </summary>
    public interface ISpatialReference
    {

        /// <summary>
        /// A coordinate system based on latitude and longitude. Some geographic coordinate systems are Lat/Lon, 
        /// and some are Lon/Lat. You can find out which this is by examining the axes. 
        /// You should also check the angular units, since not all geographic coordinate systems use degrees.
        /// </summary>
        string GeoGCS { get; set; }

        /// <summary>
        /// This indicates the horizontal datum, which corresponds to the procedure used to measure positions on the surface of the Earth. 
        /// </summary>
        string Datum { get; set; }

        /// <summary>
        /// This indicates the horizontal datum, which corresponds to the procedure used to measure positions on the surface of the Earth.
        /// </summary>
        string DatumAuthority { get; set; }

        /// <summary>
        /// This describes a spheroid, which is an approximation of the Earth's surface as a squashed sphere.
        /// </summary>
        double SpheroidRadius { get; set; }

        /// <summary>
        /// This describes a spheroid, which is an approximation of the Earth's surface as a squashed sphere.
        /// </summary>
        double SpheroidFlattening { get; set; }

        /// <summary>
        /// This describes a spheroid, which is an approximation of the Earth's surface as a squashed sphere.
        /// </summary>
        string SpheroidAuthority { get; set; }

        /// <summary>
        /// This defines the meridian used to take longitude measurements from. 
        /// The units of the longitude must be inferred from the context.
        /// </summary>
        double Primem { get; set; }

        /// <summary>
        /// This defines the meridian used to take longitude measurements from. 
        /// The units of the longitude must be inferred from the context.
        /// </summary>
        string PrimemAuthority { get; set; }

        /// <summary>
        /// This describes a projection from geographic coordinates to projected coordinates.
        /// </summary>
        string ProjectionAuthority { get; set; }

        /// <summary>
        /// The value added to all "x" values in the rectangular coordinate for a map projection. 
        /// This value frequently is assigned to eliminate negative numbers. Expressed in the unit of measure identified in Planar Coordinate Units
        /// </summary>
        double FalseEasting { get; set; }

        /// <summary>
        /// The value added to all "y" values in the rectangular coordinates for a map projection. 
        /// This value frequently is assigned to eliminate negative numbers. 
        /// Expressed in the unit of measure identified in Planar Coordinate Units
        /// </summary>
        double FalseNorthing { get; set; }

        /// <summary>
        /// The line of longitude at the center of a map projection generally used as the basis for constructing the projection
        /// </summary>
        double CentralMeridian { get; set; }

        /// <summary>
        /// The line of constant latitude at which the surface of the Earth and the plane or developable surface intersect
        /// </summary>
        double StandardParallel { get; set; }

        /// <summary>
        /// The latitude chosen as the origin of rectangular coordinate for a map projection
        /// </summary>
        double LatitudeOfOrigin { get; set; }

        /// <summary>
        /// The measurement units used to define the angles of a spheroid or ellipse associated with a specific datum.
        /// For DeepEarth, the datum is usually WGS (World Geodetic System) 1984 and the unit of measurement is a degree
        /// </summary>
        double AngularUnitOfMeasurement { get; set; }

        /// <summary>
        /// The authority body that defines the unit of measurement i.e. European Petroleum Survey Group (EPSG).  For DeepEarth
        /// the unit of measurement is usually degrees and the authority for the datum the map uses, WGS 1984 is EPSG:4326
        /// </summary>
        string UnitAuthority { get; set; }

        /// <summary>
        /// The authority body that defines the standards for the spatial reference parameters.  For DeepEarth,
        /// Spatial Reference is usually WGS 1984 and the authority is EPSG:4326
        /// </summary>
        string Authority { get; set; }

        /// <summary>
        /// Converts a logical Point (0->1) to a geographical coordinate (Longitude, Latitude)
        /// </summary>
        /// <param name="logicalPoint">The logical Point</param>
        /// <returns>The geographical coordinate (Longitude, Latitude)</returns>
        Point LogicalToGeographic(Point logicalPoint);

        /// <summary>
        /// Converts a geographical coordinate (Longitude, Latitude) to a logical Point (0->1)
        /// </summary>
        /// <param name="geographyPoint">The geographical coordinate (Longitude, Latitude)</param>
        /// <returns>The logical Point</returns>
        Point GeographicToLogical(Point geographyPoint);
    }
}
