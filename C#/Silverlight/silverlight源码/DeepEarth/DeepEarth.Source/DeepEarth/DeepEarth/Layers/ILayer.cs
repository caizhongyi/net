// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

namespace DeepEarth.Layers
{
    /// <summary>
    /// <para>
    /// A generic Layer we use for either raster tiles or vector geography.
    /// </para>
    /// </summary>
    public interface ILayer 
    {
        /// <summary>
        /// A unique ID to idenitify the layer
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// Is the Layer visible to the user?
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// An instance of the Map for this layer.
        /// </summary>
        Map MapInstance { get; set; }
    }
}