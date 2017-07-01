// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Windows.Controls;

namespace DeepEarth.Layers
{

    /// <summary>
    /// <para>
    /// A generic Layer we use for either raster tiles or vector geography.
    /// </para>
    /// </summary>
    public abstract class Layer : Canvas, ILayer 
    {
        /// <summary>
        /// Protected backing field for the MapInstance property
        /// </summary>
        protected Map _Map;
        /// <summary>
        /// Protected backing field for the IsVisible property
        /// </summary>
        protected bool _IsVisible = true;
        /// <summary>
        /// Unique identifier for the layer.
        /// </summary>
        public string ID { get; set; }


        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                if (_IsVisible != value)
                {
                    _IsVisible = value;
                    OnIsVisibleChanged();
                }
            }
        }



        public Map MapInstance
        {
            get
            {
                if (_Map == null) _Map = Map.GetMapInstance(this);
                return _Map;
            }
            set
            {
                if (_Map != null && !ReferenceEquals(_Map, value))
                {
                    throw new NotSupportedException();
                }
                _Map = value;
            }
        }


        protected virtual void OnIsVisibleChanged()
        {
        }
    }
}

