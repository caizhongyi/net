/// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
/// Code is provided as is and with no warrenty  Use at your own risk
/// View the project and the latest code at http://codeplex.com/deepearth/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeepEarth.Provider.VirtualEarth
{
    /// <summary>
    /// Serializable class for the VE tile path information.
    /// </summary>
    [DataContract]
    public class TilePathInfo
    {
        [DataMember]
        public List<string> SubDomains { get; set; }
        
        [DataMember]
        public string TilePath { get; set; }
    }
}
