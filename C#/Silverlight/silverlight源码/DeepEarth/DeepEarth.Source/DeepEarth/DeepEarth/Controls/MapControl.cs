using System.Windows.Controls;
using DeepEarth.Layers;

namespace DeepEarth.Controls
{

    /// <summary>
    /// <para>
    /// Non visual/UI control, inherited by custom controls, provides access to the map,
    /// Inherits ContentControl to provide a container to add your custom control, 
    /// Implements DeepEarth.Layers.ILayer a generic Layer we use for either raster tiles or vector geography.
    /// </para>
    /// <example>
    /// <code title="Simple example of a control class that needs access to the map">   
    /// public class YourMapControl : MapControl
    /// { 
    ///     public YourMapControl() : this(Map.DefaultInstance) { }
    /// 
    ///     public YourMapControl(Map map)
    ///     {
    ///         _Map = map;
    ///         DefaultStyleKey = typeof(YourMapControl);
    ///     }
    /// 
    ///     public override void OnApplyTemplate()
    ///     {
    ///         base.OnApplyTemplate();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class MapControl : ContentControl, ILayer
    {
        /// <summary>
        /// Protected backing field for the MapInstance property
        /// </summary>
        protected Map _Map;

        #region ILayer Members

        /// <summary>
        /// Access to instance of the Map for this layer.
        /// </summary>
        public virtual Map MapInstance
        {
            get
            {
                if (_Map == null)
                {
                    _Map = Map.GetMapInstance(this);
                }

                return _Map;
            }
            set
            {
                if (ReferenceEquals(_Map, value))
                {
                    return;
                }

                _Map = value;
            }
        }

        /// <summary>
        /// A unique ID to idenitify the layer
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Indicates whether the Layer is visible to the user.
        /// </summary>
        public bool IsVisible { get; set; }

        #endregion
    }
}