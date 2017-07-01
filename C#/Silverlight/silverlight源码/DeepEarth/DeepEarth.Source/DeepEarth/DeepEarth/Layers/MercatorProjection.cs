// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

namespace DeepEarth.Layers
{

    /// <summary>
    /// <para>
    /// Mercator is the projection used by most online maps including Virtual Earth, GMaps and Yahoo
    /// It maintains Vertical resolution while expanding the earth horizontally to fill
    /// The effect is that items at the poles appear much larger then those of equal size at the equator
    /// </para>
    /// <example>
    /// 
    /// <code title="Set the map to this projection (although it is the default anyway)">   
    /// //assume the DeepEarth control is called "map"
    /// map.SpatialReference = new MercatorProjection();
    /// </code>
    /// </example>
    /// </summary>
    public class MercatorProjection : SpatialReference
    {
        /// <summary>
        /// Mercator is the projection used by most online maps including Virtual Earth, GMaps and Yahoo
        /// </summary>
        public MercatorProjection()
        {
            GeoGCS = "GCS_WGS_1984";
            Datum = "WGS_1984";
            SpheroidRadius = 6378137.00D;
            SpheroidFlattening = 298.257223563D;
            Primem = 0.0D;
            AngularUnitOfMeasurement = 0.017453292519943295D;
            FalseEasting = 0.0D;
            FalseNorthing = 0.0D;
            CentralMeridian = 0.0D;
            LatitudeOfOrigin = 0.0D;
            UnitAuthority = "Meter";

            ScaleX = HalfPi;
            ScaleY = -HalfPi;
            OffsetX = 0.5;
            OffsetY = 0.5;
        }

    }
}